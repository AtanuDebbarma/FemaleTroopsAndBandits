using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;

namespace FemaleTroopsAndBandits
{
    // When a party is spawned or refilled from a template, swap mapped male troops to
    // independent female counterparts. The chance comes from that group's MCM slider.
    // SpawnFillSwap is the only thing that turns this on. Quest hand-overs, the party
    // screen, recruitment, loot, and upgrades add the soldier the caller already chose.
    // Covers minor factions, gangsters, militia, current mercenaries, War Sails marines,
    // bandits, corsairs, and conspiracy parties. Sword Sisters are not in this map.
    // Mounted Pillagers and the Horse Bandits line follow the Horse Bandits MCM group when that mod is available.
    [HarmonyPatch(typeof(TroopRoster), "AddToCounts", new[]
    {
        typeof(CharacterObject),
        typeof(int),
        typeof(bool),
        typeof(int),
        typeof(int),
        typeof(bool),
        typeof(int)
    })]
    internal static class FemalePartyTroopPatch
    {
        [System.ThreadStatic]
        private static int _allowSwap;

        internal static void AllowSwap()
        {
            _allowSwap++;
        }

        internal static void EndAllowSwap()
        {
            if (_allowSwap > 0)
            {
                _allowSwap--;
            }
        }

        static void Prefix(ref CharacterObject character, int count)
        {
            if (_allowSwap <= 0 || count <= 0 || character == null || string.IsNullOrEmpty(character.StringId))
            {
                return;
            }

            string femaleId;
            string groupId;
            if (!FemaleTroopCatalog.TryPartyTroop(character.StringId, out femaleId, out groupId))
            {
                return;
            }

            if (!FemaleTroopsSettings.RollFemale(groupId))
            {
                return;
            }

            CharacterObject female = FemaleTroopResolve.GetOrWarn(MBObjectManager.Instance, femaleId);
            if (female != null)
            {
                character = female;
            }
        }
    }
}
