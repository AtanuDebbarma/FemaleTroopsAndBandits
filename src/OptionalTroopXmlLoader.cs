using System;
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
    // LoadOneXmlFromFile can "succeed" without leaving usable CharacterObjects (early load
    // before Culture is ready, silent schema/path issues). Only mark a pack settled after
    // sentinel troops resolve with Culture + IsReady so session/daily hooks can retry.
    internal static class OptionalTroopXmlLoader
    {
        private static bool _navalSettled;
        private static bool _camelSettled;
        private static bool _horseSettled;

        private static readonly string[] NavalFiles = new string[]
        {
            "ftr_warsails_nord_troops.xml",
            "ftr_warsails_extra_troops.xml",
            "ftr_warsails_corsair_troops.xml",
            "ftr_warsails_quest_caravan_troops.xml",
            "ftr_naval_caravan_troops.xml",
        };

        // One sentinel per Naval file — all must be usable before the pack settles.
        private static readonly string[] NavalSentinels = new string[]
        {
            "ftr_nord_youngling",
            "ftr_empire_marine_t3",
            "ftr_southern_pirates_bandit",
            "ftr_armed_trader_nord",
            "ftr_naval_armed_trader_aserai",
        };

        private static readonly string[] CamelFiles = new string[]
        {
            "ftr_camel_cavalry_troops.xml",
        };

        private static readonly string[] CamelSentinels = new string[]
        {
            "ftr_camel_cavalry_aserai_camel_rider",
        };

        private static readonly string[] HorseBanditsFiles = new string[]
        {
            "ftr_horse_bandits_troops.xml",
        };

        private static readonly string[] HorseBanditsSentinels = new string[]
        {
            "ftr_mounted_pillager",
            "ftr_horse_bandits_chief",
        };

        // True when every active optional pack has verified usable troops (or its dependency is off).
        internal static bool IsSettled()
        {
            if (FemaleTroopBranches.NavalDlcModuleSelected() && !_navalSettled)
            {
                return false;
            }

            if (FemaleTroopBranches.CamelCavalryModuleSelected() && !_camelSettled)
            {
                return false;
            }

            if (FemaleTroopBranches.HorseBanditsModuleSelected() && !_horseSettled)
            {
                return false;
            }

            return true;
        }

        internal static void TryLoadForCampaign()
        {
            if (IsSettled())
            {
                return;
            }

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

            if (FemaleTroopBranches.NavalDlcModuleSelected())
            {
                if (!_navalSettled)
                {
                    _navalSettled = LoadPack(manager, moduleRoot, "War Sails", NavalFiles, NavalSentinels);
                }
            }
            else
            {
                _navalSettled = true;
                Debug.Print("[FemaleTroopsAndBandits] NavalDLC not active; War Sails female troops skipped.");
            }

            if (FemaleTroopBranches.CamelCavalryModuleSelected())
            {
                if (!_camelSettled)
                {
                    _camelSettled = LoadPack(manager, moduleRoot, "Camel Cavalry", CamelFiles, CamelSentinels);
                }
            }
            else
            {
                _camelSettled = true;
                Debug.Print("[FemaleTroopsAndBandits] CamelCavalry not active; female camel troops skipped.");
            }

            if (FemaleTroopBranches.HorseBanditsModuleSelected())
            {
                if (!_horseSettled)
                {
                    _horseSettled = LoadPack(manager, moduleRoot, "Horse Bandits", HorseBanditsFiles, HorseBanditsSentinels);
                }
            }
            else
            {
                _horseSettled = true;
                Debug.Print("[FemaleTroopsAndBandits] HorseBandits not active; female Horse Bandits troops skipped.");
            }
        }

        internal static void ResetForNewGameSession()
        {
            _navalSettled = false;
            _camelSettled = false;
            _horseSettled = false;
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

        // Returns true only when every sentinel CharacterObject is usable after load.
        private static bool LoadPack(
            MBObjectManager manager,
            string moduleRoot,
            string packName,
            string[] fileNames,
            string[] sentinelIds)
        {
            int filesOpened = 0;
            for (int i = 0; i < fileNames.Length; i++)
            {
                string path = moduleRoot + "ModuleData" + Path.DirectorySeparatorChar + fileNames[i];
                if (!File.Exists(path))
                {
                    Debug.Print("[FemaleTroopsAndBandits] missing " + packName + " troop XML: " + fileNames[i]);
                    continue;
                }

                try
                {
                    // forceSkipValidation: XSD misses do not throw; still verify sentinels below.
                    manager.LoadOneXmlFromFile(path, string.Empty, true);
                    filesOpened++;
                }
                catch (Exception exception)
                {
                    Debug.Print("[FemaleTroopsAndBandits] failed loading " + fileNames[i] + ": " + exception.Message);
                }
            }

            string missing = null;
            if (!AllSentinelsUsable(manager, sentinelIds, out missing))
            {
                Debug.Print("[FemaleTroopsAndBandits] " + packName
                    + " female troop XML opened (" + filesOpened + " files) but sentinel '"
                    + missing + "' is not usable yet; will retry.");
                return false;
            }

            Debug.Print("[FemaleTroopsAndBandits] " + packName
                + " female troop XML loaded and verified (" + filesOpened + " files).");
            return true;
        }

        private static bool AllSentinelsUsable(MBObjectManager manager, string[] sentinelIds, out string missingId)
        {
            missingId = null;
            if (sentinelIds == null || sentinelIds.Length == 0)
            {
                return true;
            }

            for (int i = 0; i < sentinelIds.Length; i++)
            {
                string id = sentinelIds[i];
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
    }
}
