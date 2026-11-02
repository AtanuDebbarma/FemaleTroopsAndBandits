using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;
using TaleWorlds.ObjectSystem;

namespace FemaleTroopsAndBandits
{
    /// <summary>
    /// Camel and marine links are upgrade buttons on one shared troop, so a percent cannot
    /// roll them per soldier. The Aserai and Khuzait recruit settings keep or remove the
    /// camel upgrade. Each War Sails marine setting keeps or removes that culture's extra
    /// marine upgrade. Sword Sister tier 6 stays on the XSLT and is never touched here.
    /// </summary>
    internal static class FemaleTroopBranches
    {
        private static readonly MethodInfo UpgradeTargetsSetter = FindUpgradeTargetsSetter();
        private static bool _reportedMissingSetter;

        internal static void Apply(FemaleTroopsSettings settings)
        {
            try
            {
                ApplyLinks(settings);
            }
            catch (Exception)
            {
                // The options menu can open before troops exist. Session launch tries again.
            }
        }

        private static void ApplyLinks(FemaleTroopsSettings settings)
        {
            MBObjectManager manager = MBObjectManager.Instance;
            if (manager == null)
            {
                return;
            }

            if (manager.GetObject<CharacterObject>("aserai_female_mameluke_regular") == null)
            {
                return;
            }

            bool aseraiCamels = GroupOn(settings, FemaleTroopsSettings.GroupAserai)
                && manager.GetObject<CharacterObject>("camel_cavalry_aserai_camel_rider") != null;
            SetExtraUpgrade(
                "aserai_female_mameluke_regular",
                "ftr_camel_cavalry_aserai_camel_rider",
                aseraiCamels);

            bool khuzaitCamels = GroupOn(settings, FemaleTroopsSettings.GroupKhuzait)
                && manager.GetObject<CharacterObject>("camel_cavalry_khuzait_camel_torguud") != null;
            SetExtraUpgrade(
                "khuzait_female_qanqli",
                "ftr_camel_cavalry_khuzait_camel_torguud",
                khuzaitCamels);

            // Same land troops War Sails sends into marines. The land upgrade stays.
            // The link is removed when that marine switch is off or War Sails is not loaded.
            SetMarine(settings, "imperial_infantrywoman", "empire_marine_t3", "ftr_empire_marine_t3", FemaleTroopsSettings.GroupMarineEmpire);
            SetMarine(settings, "imperial_trained_infantrywoman", "empire_marine_t4", "ftr_empire_marine_t4", FemaleTroopsSettings.GroupMarineEmpire);
            SetMarine(settings, "imperial_veteran_infantrywoman", "empire_marine_t5", "ftr_empire_marine_t5", FemaleTroopsSettings.GroupMarineEmpire);
            SetMarine(settings, "sturgian_warrioress", "sturgia_marine_t3", "ftr_sturgia_marine_t3", FemaleTroopsSettings.GroupMarineSturgia);
            SetMarine(settings, "sturgian_female_soldier", "sturgia_marine_t4", "ftr_sturgia_marine_t4", FemaleTroopsSettings.GroupMarineSturgia);
            SetMarine(settings, "sturgian_spearwoman", "sturgia_marine_t5", "ftr_sturgia_marine_t5", FemaleTroopsSettings.GroupMarineSturgia);
            SetMarine(settings, "aserai_female_skirmisher", "aserai_marine_t4", "ftr_aserai_marine_t4", FemaleTroopsSettings.GroupMarineAserai);
            SetMarine(settings, "aserai_archeress", "aserai_marine_t5", "ftr_aserai_marine_t5", FemaleTroopsSettings.GroupMarineAserai);
            SetMarine(settings, "battanian_female_skirmisher", "battanian_marine_t4", "ftr_battanian_marine_t4", FemaleTroopsSettings.GroupMarineBattania);
            SetMarine(settings, "battanian_female_veteran_skirmisher", "battanian_marine_t5", "ftr_battanian_marine_t5", FemaleTroopsSettings.GroupMarineBattania);
            SetMarine(settings, "vlandian_crossbowwoman", "vlandian_marine_t4", "ftr_vlandian_marine_t4", FemaleTroopsSettings.GroupMarineVlandia);
            SetMarine(settings, "vlandian_hardened_crossbowwoman", "vlandian_marine_t5", "ftr_vlandian_marine_t5", FemaleTroopsSettings.GroupMarineVlandia);

            // Follow the live male exits. War Sails replaces Sea Raider upgrades with Nord troops.
            // Horse Bandits replaces Pillager, Ransacker, and Highwayman upgrades when it is installed.
            MirrorUpgrades("sea_raiders_bandit");
            MirrorUpgrades("sea_raiders_raider");
            MirrorUpgrades("sea_raiders_chief");
            MirrorUpgrades("mounted_pillager");
            MirrorUpgrades("mounted_ransacker");
            MirrorUpgrades("mountain_bandits_chief");
            FemaleTroopEncyclopedia.Apply(settings);
        }

        internal static bool HorseBanditsPresent()
        {
            MBObjectManager manager = MBObjectManager.Instance;
            if (manager == null)
            {
                return false;
            }

            return manager.GetObject<CharacterObject>("horse_bandits_mounted_huntsman") != null;
        }

        // Launcher / module list. Used so MCM can hide Horse Bandits options when that mod is not enabled.
        internal static bool HorseBanditsModuleSelected()
        {
            try
            {
                return ModuleHelper.IsModuleActive("HorseBandits");
            }
            catch
            {
                return false;
            }
        }

        internal static bool HorseBanditsSupportAvailable()
        {
            if (HorseBanditsModuleSelected())
            {
                return true;
            }

            return HorseBanditsPresent();
        }

        private static readonly Dictionary<string, string> UpgradeToFemale = new Dictionary<string, string>
        {
            { "sea_raiders_raider", "ftr_sea_raiders_raider" },
            { "sturgian_soldier", "sturgian_female_soldier" },
            { "nord_axe_warrior", "ftr_nord_axe_warrior" },
            { "sea_raiders_chief", "ftr_sea_raiders_chief" },
            { "varyag_veteran", "female_varyag" },
            { "nord_boandi", "ftr_nord_boandi" },
            { "druzhinnik", "female_druzhinnik" },
            { "nord_berserkr", "ftr_nord_berserkr" },
            { "horse_bandits_mounted_huntsman", "ftr_horse_bandits_mounted_huntsman" },
            { "horse_bandits_imperial_bandit_horseman", "ftr_horse_bandits_imperial_bandit_horseman" },
            { "vlandian_champion", "vlandian_female_champion" },
            { "horse_bandits_vlandian_brigand_champion", "ftr_horse_bandits_vlandian_brigand_champion" },
        };

        private static void MirrorUpgrades(string maleId)
        {
            MBObjectManager manager = MBObjectManager.Instance;
            if (manager == null)
            {
                return;
            }

            if (UpgradeTargetsSetter == null)
            {
                if (!_reportedMissingSetter)
                {
                    _reportedMissingSetter = true;
                    Debug.Print("[FemaleTroopsAndBandits] Could not change an upgrade link. The base game setter was not found.");
                }

                return;
            }

            CharacterObject male = manager.GetObject<CharacterObject>(maleId);
            CharacterObject female = manager.GetObject<CharacterObject>("ftr_" + maleId);
            if (male == null || female == null)
            {
                return;
            }

            List<CharacterObject> next = new List<CharacterObject>();
            CharacterObject[] current = male.UpgradeTargets;
            if (current != null)
            {
                for (int i = 0; i < current.Length; i++)
                {
                    CharacterObject troop = current[i];
                    if (troop == null || string.IsNullOrEmpty(troop.StringId))
                    {
                        continue;
                    }

                    string femaleId;
                    if (!UpgradeToFemale.TryGetValue(troop.StringId, out femaleId))
                    {
                        continue;
                    }

                    CharacterObject mapped = FemaleTroopResolve.GetOrWarn(manager, femaleId);
                    if (mapped != null)
                    {
                        next.Add(mapped);
                    }
                }
            }

            CharacterObject[] nextArray = next.ToArray();
            if (SameTargets(female.UpgradeTargets, nextArray))
            {
                return;
            }

            UpgradeTargetsSetter.Invoke(female, new object[] { nextArray });
        }

        private static bool GroupOn(FemaleTroopsSettings settings, string groupId)
        {
            if (settings == null)
            {
                return true;
            }

            return settings.GroupOn(groupId);
        }

        private static void SetMarine(FemaleTroopsSettings settings, string parentId, string maleMarineId, string femaleMarineId, string groupId)
        {
            MBObjectManager manager = MBObjectManager.Instance;
            bool enabled = GroupOn(settings, groupId)
                && manager != null
                && manager.GetObject<CharacterObject>(maleMarineId) != null;
            SetExtraUpgrade(parentId, femaleMarineId, enabled);
        }

        private static void SetExtraUpgrade(string parentId, string extraId, bool enabled)
        {
            MBObjectManager manager = MBObjectManager.Instance;
            if (manager == null)
            {
                return;
            }

            if (UpgradeTargetsSetter == null)
            {
                if (!_reportedMissingSetter)
                {
                    _reportedMissingSetter = true;
                    Debug.Print("[FemaleTroopsAndBandits] Could not change an upgrade link. The base game setter was not found.");
                }

                return;
            }

            CharacterObject parent = manager.GetObject<CharacterObject>(parentId);
            CharacterObject extra = manager.GetObject<CharacterObject>(extraId);
            if (parent == null || extra == null)
            {
                return;
            }

            bool present = Contains(parent.UpgradeTargets, extra);
            if (present == enabled)
            {
                return;
            }

            CharacterObject[] next = enabled
                ? With(parent.UpgradeTargets, extra)
                : Without(parent.UpgradeTargets, extra);
            UpgradeTargetsSetter.Invoke(parent, new object[] { next });
        }

        private static MethodInfo FindUpgradeTargetsSetter()
        {
            PropertyInfo property = typeof(CharacterObject).GetProperty(
                "UpgradeTargets",
                BindingFlags.Instance | BindingFlags.Public);
            if (property == null)
            {
                return null;
            }

            return property.GetSetMethod(true);
        }

        private static bool SameTargets(CharacterObject[] current, CharacterObject[] next)
        {
            if (current == null)
            {
                current = new CharacterObject[0];
            }

            if (next == null)
            {
                next = new CharacterObject[0];
            }

            if (current.Length != next.Length)
            {
                return false;
            }

            for (int i = 0; i < current.Length; i++)
            {
                if (current[i] != next[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static bool Contains(CharacterObject[] current, CharacterObject extra)
        {
            if (current == null)
            {
                return false;
            }

            for (int i = 0; i < current.Length; i++)
            {
                if (current[i] == extra)
                {
                    return true;
                }
            }

            return false;
        }

        private static CharacterObject[] With(CharacterObject[] current, CharacterObject extra)
        {
            List<CharacterObject> kept = CopyWithout(current, extra);
            kept.Add(extra);
            return kept.ToArray();
        }

        private static CharacterObject[] Without(CharacterObject[] current, CharacterObject extra)
        {
            return CopyWithout(current, extra).ToArray();
        }

        private static List<CharacterObject> CopyWithout(CharacterObject[] current, CharacterObject extra)
        {
            List<CharacterObject> kept = new List<CharacterObject>();
            if (current == null)
            {
                return kept;
            }

            for (int i = 0; i < current.Length; i++)
            {
                CharacterObject troop = current[i];
                if (troop == null || troop == extra)
                {
                    continue;
                }

                kept.Add(troop);
            }

            return kept;
        }
    }

    internal sealed class FemaleTroopBranchBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, OnSessionLaunched);
            CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, OnDailyTick);
        }

        public override void SyncData(IDataStore dataStore)
        {
        }

        private void OnSessionLaunched(CampaignGameStarter starter)
        {
            FemaleTroopBranches.Apply(FemaleTroopsSettings.Instance);
            RaiseYourBannerCompat.Apply();
        }

        private void OnDailyTick()
        {
            FemaleTroopBranches.Apply(FemaleTroopsSettings.Instance);
            RaiseYourBannerCompat.Apply();
        }
    }
}
