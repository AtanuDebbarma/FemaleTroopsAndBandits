using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.ObjectSystem;

namespace FemaleTroopsAndBandits
{
    /// <summary>
    /// Shows a female troop in the encyclopedia while her group is on, using the same
    /// hidden flag as the male troop. Off, or 0 percent, hides that female line.
    /// Camel troops also require Camel Cavalry. Horse Bandits troops also require that mod.
    /// Sword Sister tier 6 is left alone.
    /// </summary>
    internal static class FemaleTroopEncyclopedia
    {
        private sealed class Entry
        {
            public string MaleId;
            public string GroupId;
            public bool SourceMissing;
        }

        internal static void Apply(FemaleTroopsSettings settings)
        {
            MBObjectManager manager = MBObjectManager.Instance;
            if (manager == null || manager.GetObject<CharacterObject>("ftr_imperial_recruit") == null)
            {
                return;
            }

            Dictionary<string, Entry> entries = Build(manager);
            foreach (KeyValuePair<string, Entry> pair in entries)
            {
                CharacterObject female = manager.GetObject<CharacterObject>(pair.Key);
                if (female == null)
                {
                    continue;
                }

                bool hidden = ShouldHide(manager, settings, pair.Value);
                if (female.HiddenInEncyclopedia != hidden)
                {
                    female.HiddenInEncyclopedia = hidden;
                }
            }

            ApplyCamel(manager, settings, "camel_cavalry_aserai_camel_rider", FemaleTroopsSettings.GroupAserai);
            ApplyCamel(manager, settings, "camel_cavalry_khuzait_camel_torguud", FemaleTroopsSettings.GroupKhuzait);
        }

        private static bool ShouldHide(MBObjectManager manager, FemaleTroopsSettings settings, Entry entry)
        {
            if (entry.SourceMissing)
            {
                return true;
            }

            if (entry.GroupId == FemaleTroopsSettings.GroupHorseBandits)
            {
                CharacterObject horseMale = string.IsNullOrEmpty(entry.MaleId)
                    ? null
                    : manager.GetObject<CharacterObject>(entry.MaleId);
                if (horseMale == null)
                {
                    return true;
                }

                if (settings != null && !settings.GroupOn(FemaleTroopsSettings.GroupHorseBandits))
                {
                    return true;
                }

                return horseMale.HiddenInEncyclopedia;
            }

            bool groupOn = settings == null || settings.GroupOn(entry.GroupId);
            if (!groupOn)
            {
                return true;
            }

            if (string.IsNullOrEmpty(entry.MaleId))
            {
                return false;
            }

            CharacterObject male = manager.GetObject<CharacterObject>(entry.MaleId);
            if (male == null)
            {
                return true;
            }

            return male.HiddenInEncyclopedia;
        }

        private static void ApplyCamel(MBObjectManager manager, FemaleTroopsSettings settings, string maleCamelId, string groupId)
        {
            bool show = manager.GetObject<CharacterObject>(maleCamelId) != null
                && (settings == null || settings.GroupOn(groupId));
            string[] femaleIds = groupId == FemaleTroopsSettings.GroupAserai
                ? AseraiCamels
                : KhuzaitCamels;
            for (int i = 0; i < femaleIds.Length; i++)
            {
                CharacterObject troop = manager.GetObject<CharacterObject>(femaleIds[i]);
                if (troop == null)
                {
                    continue;
                }

                bool hidden = !show;
                if (troop.HiddenInEncyclopedia != hidden)
                {
                    troop.HiddenInEncyclopedia = hidden;
                }
            }
        }

        private static readonly string[] AseraiCamels = new string[]
        {
            "ftr_camel_cavalry_aserai_camel_rider",
            "ftr_camel_cavalry_aserai_camel_lancer",
            "ftr_camel_cavalry_aserai_camel_archer"
        };

        private static readonly string[] KhuzaitCamels = new string[]
        {
            "ftr_camel_cavalry_khuzait_camel_torguud",
            "ftr_camel_cavalry_khuzait_camel_kheshig",
            "ftr_camel_cavalry_khuzait_heavy_camel_archer"
        };

        private static Dictionary<string, Entry> Build(MBObjectManager manager)
        {
            Dictionary<string, Entry> entries = new Dictionary<string, Entry>();
            List<string> maleIds = new List<string>();
            List<string> femaleIds = new List<string>();
            List<string> groupIds = new List<string>();
            FemaleTroopCatalog.Visit(maleIds, femaleIds, groupIds);
            for (int i = 0; i < femaleIds.Count; i++)
            {
                if (entries.ContainsKey(femaleIds[i]))
                {
                    continue;
                }

                Entry entry = new Entry();
                entry.MaleId = maleIds[i];
                entry.GroupId = groupIds[i];
                entry.SourceMissing = manager.GetObject<CharacterObject>(maleIds[i]) == null;
                entries[femaleIds[i]] = entry;
            }

            Queue<string> pending = new Queue<string>(entries.Keys);
            while (pending.Count > 0)
            {
                string femaleId = pending.Dequeue();
                Entry parent = entries[femaleId];
                CharacterObject female = manager.GetObject<CharacterObject>(femaleId);
                if (female == null || female.UpgradeTargets == null)
                {
                    continue;
                }

                for (int i = 0; i < female.UpgradeTargets.Length; i++)
                {
                    CharacterObject target = female.UpgradeTargets[i];
                    if (target == null || string.IsNullOrEmpty(target.StringId))
                    {
                        continue;
                    }

                    if (entries.ContainsKey(target.StringId) || IsSpecial(target.StringId) || !IsModFemale(target))
                    {
                        continue;
                    }

                    if (IsRestricted(parent.GroupId) && !SameLine(femaleId, target.StringId))
                    {
                        continue;
                    }

                    Entry child = new Entry();
                    child.MaleId = null;
                    child.GroupId = parent.GroupId;
                    child.SourceMissing = parent.SourceMissing;
                    entries[target.StringId] = child;
                    pending.Enqueue(target.StringId);
                }
            }

            return entries;
        }

        private static bool IsSpecial(string femaleId)
        {
            return femaleId.StartsWith("ftr_camel_cavalry_")
                || femaleId.StartsWith("ftr_horse_bandits_")
                || femaleId == "ftr_mounted_pillager"
                || femaleId == "ftr_mounted_ransacker";
        }

        private static bool IsModFemale(CharacterObject troop)
        {
            if (troop == null || !troop.IsFemale || string.IsNullOrEmpty(troop.StringId))
            {
                return false;
            }

            string id = troop.StringId;
            if (id.StartsWith("ftr_") || id.StartsWith("female_") || id.StartsWith("ghilwoman_"))
            {
                return true;
            }

            if (id.IndexOf("_female_") >= 0)
            {
                return true;
            }

            return id == "khuzait_noble_daughter" || id == "sturgian_warrior_daughter";
        }

        private static bool IsRestricted(string groupId)
        {
            return groupId == FemaleTroopsSettings.GroupLooters
                || groupId == FemaleTroopsSettings.GroupSeaRaiders
                || groupId == FemaleTroopsSettings.GroupMountain
                || groupId == FemaleTroopsSettings.GroupForest
                || groupId == FemaleTroopsSettings.GroupDesert
                || groupId == FemaleTroopsSettings.GroupSteppe
                || groupId == FemaleTroopsSettings.GroupCorsairs
                || groupId == FemaleTroopsSettings.GroupPoachers
                || groupId == FemaleTroopsSettings.GroupCaravan
                || groupId == FemaleTroopsSettings.GroupConspiracyAnti
                || groupId == FemaleTroopsSettings.GroupConspiracyImperial
                || groupId == FemaleTroopsSettings.GroupHorseBandits;
        }

        private static bool SameLine(string parentFemaleId, string targetId)
        {
            string token = LineToken(parentFemaleId);
            return token != null && targetId.IndexOf(token) >= 0;
        }

        private static string LineToken(string femaleId)
        {
            if (femaleId.IndexOf("sea_raiders") >= 0)
            {
                return "sea_raiders";
            }

            if (femaleId.IndexOf("mountain_bandits") >= 0)
            {
                return "mountain_bandits";
            }

            if (femaleId.IndexOf("forest_bandits") >= 0)
            {
                return "forest_bandits";
            }

            if (femaleId.IndexOf("desert_bandits") >= 0)
            {
                return "desert_bandits";
            }

            if (femaleId.IndexOf("steppe_bandits") >= 0)
            {
                return "steppe_bandits";
            }

            if (femaleId.IndexOf("southern_pirates") >= 0)
            {
                return "southern_pirates";
            }

            if (femaleId.IndexOf("conspiracy_") >= 0)
            {
                return "conspiracy_";
            }

            if (femaleId.IndexOf("looter") >= 0)
            {
                return "looter";
            }

            return null;
        }
    }
}
