using System;
using System.Collections.Generic;
using System.IO;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;
using TaleWorlds.ObjectSystem;

namespace FemaleTroopsAndBandits
{
    // Optional integration troop XML ships in this module's ModuleData but is not listed in
    // SubModule.xml (always-load crashes when the dependency is missing). Load at campaign
    // start only when that dependency is active.
    //
    // LoadOneXmlFromFile can open a file while later NPCCharacter nodes never register.
    // Packs settle only after every expected ftr_* id in that pack is usable (Culture + IsReady).
    // Settlement is per-pack so one stuck dependency cannot block the others.
    internal static class OptionalTroopXmlLoader
    {
        internal enum TroopLoadState
        {
            NotOptional,
            Inactive,
            Pending,
            Ready,
            Abandoned,
        }

        private const int MaxLoadAttempts = 12;

        private static readonly string[] NavalFiles = new string[]
        {
            "ftr_warsails_nord_troops.xml",
            "ftr_warsails_extra_troops.xml",
            "ftr_warsails_corsair_troops.xml",
            "ftr_warsails_quest_caravan_troops.xml",
            "ftr_naval_caravan_troops.xml",
        };

        private static readonly string[] NavalExpectedIds = new string[]
        {
            "ftr_nord_youngling",
            "ftr_nord_drengr",
            "ftr_nord_axe_warrior",
            "ftr_nord_spear_warrior",
            "ftr_nord_boandi",
            "ftr_nord_berserkr",
            "ftr_nord_ungmann",
            "ftr_nord_thegn",
            "ftr_nord_jarlsmann",
            "ftr_nord_hirdmann",
            "ftr_nord_huscarl",
            "ftr_nord_huntsman",
            "ftr_nord_freeman_archer",
            "ftr_nord_marksman",
            "ftr_nord_skathi",
            "ftr_nord_hew-bearer",
            "ftr_nord_vargr",
            "ftr_nord_skjaldbrestir",
            "ftr_nord_ulfhednar",
            "ftr_empire_marine_t3",
            "ftr_empire_marine_t4",
            "ftr_empire_marine_t5",
            "ftr_sturgia_marine_t3",
            "ftr_sturgia_marine_t4",
            "ftr_sturgia_marine_t5",
            "ftr_aserai_marine_t4",
            "ftr_aserai_marine_t5",
            "ftr_battanian_marine_t4",
            "ftr_battanian_marine_t5",
            "ftr_vlandian_marine_t4",
            "ftr_vlandian_marine_t5",
            "ftr_nord_militia_archer",
            "ftr_nord_militia_veteran_archer",
            "ftr_nord_militia_spearman",
            "ftr_nord_militia_veteran_spearman",
            "ftr_southern_pirates_bandit",
            "ftr_southern_pirates_raider",
            "ftr_southern_pirates_chief",
            "ftr_armed_trader_nord",
            "ftr_caravan_guard_nord",
            "ftr_veteran_caravan_guard_nord",
            "ftr_naval_armed_trader_aserai",
            "ftr_naval_caravan_guard_aserai",
            "ftr_naval_veteran_caravan_guard_aserai",
            "ftr_naval_armed_trader_battania",
            "ftr_naval_caravan_guard_battania",
            "ftr_naval_veteran_caravan_guard_battania",
            "ftr_naval_armed_trader_empire",
            "ftr_naval_caravan_guard_empire",
            "ftr_naval_veteran_caravan_guard_empire",
            "ftr_naval_armed_trader_khuzait",
            "ftr_naval_caravan_guard_khuzait",
            "ftr_naval_veteran_caravan_guard_khuzait",
            "ftr_naval_armed_trader_sturgia",
            "ftr_naval_caravan_guard_sturgia",
            "ftr_naval_veteran_caravan_guard_sturgia",
            "ftr_naval_armed_trader_vlandia",
            "ftr_naval_caravan_guard_vlandia",
            "ftr_naval_veteran_caravan_guard_vlandia",
            "ftr_naval_armed_trader_nord",
            "ftr_naval_caravan_guard_nord",
            "ftr_naval_veteran_caravan_guard_nord",
        };

        private static readonly string[] CamelFiles = new string[]
        {
            "ftr_camel_cavalry_troops.xml",
        };

        private static readonly string[] CamelExpectedIds = new string[]
        {
            "ftr_camel_cavalry_aserai_camel_rider",
            "ftr_camel_cavalry_aserai_camel_lancer",
            "ftr_camel_cavalry_aserai_camel_archer",
            "ftr_camel_cavalry_khuzait_camel_torguud",
            "ftr_camel_cavalry_khuzait_camel_kheshig",
            "ftr_camel_cavalry_khuzait_heavy_camel_archer",
        };

        private static readonly string[] HorseBanditsFiles = new string[]
        {
            "ftr_horse_bandits_troops.xml",
        };

        // ftr_mounted_pillager / ransacker live in always-load ftr_bandit_troops.xml, not this pack.
        private static readonly string[] HorseBanditsExpectedIds = new string[]
        {
            "ftr_horse_bandits_mounted_huntsman",
            "ftr_horse_bandits_mounted_packmaster",
            "ftr_horse_bandits_imperial_bandit_horseman",
            "ftr_horse_bandits_imperial_elite_bandit_horseman",
            "ftr_horse_bandits_chief",
            "ftr_horse_bandits_boss",
            "ftr_horse_bandits_vlandian_brigand_champion",
            "ftr_horse_bandits_vlandian_brigand_banner_knight",
        };

        private static readonly PackStatus NavalPack = PackStatus.Create(
            "War Sails",
            NavalFiles,
            NavalExpectedIds,
            FemaleTroopBranches.NavalDlcModuleSelected);

        private static readonly PackStatus CamelPack = PackStatus.Create(
            "Camel Cavalry",
            CamelFiles,
            CamelExpectedIds,
            FemaleTroopBranches.CamelCavalryModuleSelected);

        private static readonly PackStatus HorsePack = PackStatus.Create(
            "Horse Bandits",
            HorseBanditsFiles,
            HorseBanditsExpectedIds,
            FemaleTroopBranches.HorseBanditsModuleSelected);

        private static Dictionary<string, PackStatus> _idToPack;

        // True when every pack is Inactive, Ready, or Abandoned (none still Pending).
        internal static bool IsSettled()
        {
            return !NavalPack.IsPending && !CamelPack.IsPending && !HorsePack.IsPending;
        }

        internal static TroopLoadState GetTroopLoadState(string stringId)
        {
            if (string.IsNullOrEmpty(stringId))
            {
                return TroopLoadState.NotOptional;
            }

            EnsureIdMap();
            PackStatus pack;
            if (!_idToPack.TryGetValue(stringId, out pack) || pack == null)
            {
                return TroopLoadState.NotOptional;
            }

            return pack.State;
        }

        // Shells may be rematerialized or cleared only when not waiting on a Pending pack.
        internal static bool CanRepairTroopId(string stringId)
        {
            return GetTroopLoadState(stringId) != TroopLoadState.Pending;
        }

        internal static void TryLoadForCampaign()
        {
            MBObjectManager manager = MBObjectManager.Instance;
            if (manager == null)
            {
                return;
            }

            string moduleRoot = GetModuleRoot();
            if (string.IsNullOrEmpty(moduleRoot))
            {
                return;
            }

            TickPack(manager, moduleRoot, NavalPack);
            TickPack(manager, moduleRoot, CamelPack);
            TickPack(manager, moduleRoot, HorsePack);
        }

        internal static void ResetForNewGameSession()
        {
            NavalPack.Reset();
            CamelPack.Reset();
            HorsePack.Reset();
            _idToPack = null;
        }

        private static void TickPack(MBObjectManager manager, string moduleRoot, PackStatus pack)
        {
            if (!pack.IsDependencyActive())
            {
                if (pack.State != TroopLoadState.Inactive)
                {
                    pack.MarkInactive();
                    Debug.Print("[FemaleTroopsAndBandits] " + pack.Name
                        + " not active; female troops for that pack skipped.");
                }

                return;
            }

            if (pack.State == TroopLoadState.Ready || pack.State == TroopLoadState.Abandoned)
            {
                return;
            }

            pack.Attempts++;
            int filesOpened = LoadPackFiles(manager, moduleRoot, pack);

            string missing;
            if (AllExpectedUsable(manager, pack.ExpectedIds, out missing))
            {
                pack.MarkReady();
                Debug.Print("[FemaleTroopsAndBandits] " + pack.Name
                    + " female troop XML loaded and verified (" + filesOpened + " files, "
                    + pack.ExpectedIds.Length + " troops).");
                return;
            }

            if (pack.Attempts >= MaxLoadAttempts)
            {
                pack.MarkAbandoned();
                Debug.Print("[FemaleTroopsAndBandits] " + pack.Name
                    + " female troop XML still missing '" + missing
                    + "' after " + pack.Attempts + " attempts; abandoning pack verification.");
                return;
            }

            if (pack.Attempts == 1 || (pack.Attempts % 3) == 0)
            {
                Debug.Print("[FemaleTroopsAndBandits] " + pack.Name
                    + " female troop XML opened (" + filesOpened + " files) but '"
                    + missing + "' is not usable yet; will retry ("
                    + pack.Attempts + "/" + MaxLoadAttempts + ").");
            }

            pack.MarkPending();
        }

        private static int LoadPackFiles(MBObjectManager manager, string moduleRoot, PackStatus pack)
        {
            int filesOpened = 0;
            for (int i = 0; i < pack.Files.Length; i++)
            {
                string path = moduleRoot + "ModuleData" + Path.DirectorySeparatorChar + pack.Files[i];
                if (!File.Exists(path))
                {
                    Debug.Print("[FemaleTroopsAndBandits] missing " + pack.Name + " troop XML: " + pack.Files[i]);
                    continue;
                }

                try
                {
                    manager.LoadOneXmlFromFile(path, string.Empty, true);
                    filesOpened++;
                }
                catch (Exception exception)
                {
                    Debug.Print("[FemaleTroopsAndBandits] failed loading " + pack.Files[i] + ": " + exception.Message);
                }
            }

            return filesOpened;
        }

        private static bool AllExpectedUsable(MBObjectManager manager, string[] expectedIds, out string missingId)
        {
            missingId = null;
            for (int i = 0; i < expectedIds.Length; i++)
            {
                string id = expectedIds[i];
                CharacterObject troop = manager.GetObject<CharacterObject>(id);
                if (!IsUsable(troop))
                {
                    missingId = id;
                    return false;
                }
            }

            return true;
        }

        private static bool IsUsable(CharacterObject troop)
        {
            if (troop == null)
            {
                return false;
            }

            try
            {
                return troop.IsReady && troop.Culture != null;
            }
            catch
            {
                return false;
            }
        }

        private static string GetModuleRoot()
        {
            try
            {
                return ModuleHelper.GetModuleFullPath("FemaleTroopsAndBandits");
            }
            catch (Exception exception)
            {
                Debug.Print("[FemaleTroopsAndBandits] optional troop XML path failed: " + exception.Message);
                return null;
            }
        }

        private static void EnsureIdMap()
        {
            if (_idToPack != null)
            {
                return;
            }

            Dictionary<string, PackStatus> map = new Dictionary<string, PackStatus>(StringComparer.Ordinal);
            AddIds(map, NavalPack);
            AddIds(map, CamelPack);
            AddIds(map, HorsePack);
            _idToPack = map;
        }

        private static void AddIds(Dictionary<string, PackStatus> map, PackStatus pack)
        {
            for (int i = 0; i < pack.ExpectedIds.Length; i++)
            {
                map[pack.ExpectedIds[i]] = pack;
            }
        }

        private sealed class PackStatus
        {
            public string Name;
            public string[] Files;
            public string[] ExpectedIds;
            public Func<bool> IsDependencyActive;
            public TroopLoadState State;
            public int Attempts;

            public bool IsPending
            {
                get { return State == TroopLoadState.Pending; }
            }

            public static PackStatus Create(string name, string[] files, string[] expectedIds, Func<bool> isActive)
            {
                PackStatus pack = new PackStatus();
                pack.Name = name;
                pack.Files = files;
                pack.ExpectedIds = expectedIds;
                pack.IsDependencyActive = isActive;
                pack.State = TroopLoadState.Pending;
                pack.Attempts = 0;
                return pack;
            }

            public void Reset()
            {
                State = TroopLoadState.Pending;
                Attempts = 0;
            }

            public void MarkInactive()
            {
                State = TroopLoadState.Inactive;
                Attempts = 0;
            }

            public void MarkPending()
            {
                State = TroopLoadState.Pending;
            }

            public void MarkReady()
            {
                State = TroopLoadState.Ready;
            }

            public void MarkAbandoned()
            {
                State = TroopLoadState.Abandoned;
            }
        }
    }
}
