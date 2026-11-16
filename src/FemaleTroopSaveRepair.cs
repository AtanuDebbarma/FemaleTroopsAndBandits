using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace FemaleTroopsAndBandits
{
    // Old saves (and War Sails optional packs that loaded after deserialize) leave empty
    // ftr_* CharacterObject shells in volunteer slots and rosters. Replace with the live
    // registered troop when optional XML has loaded, or clear / male-fallback when not.
    internal static class FemaleTroopSaveRepair
    {
        private static bool _ran;

        internal static void ResetForNewGameSession()
        {
            _ran = false;
        }

        internal static void TryRepairAfterOptionalLoad()
        {
            if (_ran || Campaign.Current == null)
            {
                return;
            }

            // Do not clear empty ftr_* shells until optional packs have verified usable troops
            // (or their dependency is off). Clearing earlier permanently drops War Sails / Horse Bandits stacks.
            if (!OptionalTroopXmlLoader.IsSettled())
            {
                return;
            }

            MBObjectManager manager = MBObjectManager.Instance;
            if (manager == null)
            {
                return;
            }

            int volunteersFixed = 0;
            int rosterFixed = 0;

            foreach (Settlement settlement in Settlement.All)
            {
                if (settlement == null || settlement.Notables == null)
                {
                    continue;
                }

                foreach (Hero notable in settlement.Notables)
                {
                    volunteersFixed += RepairVolunteerSlots(manager, notable);
                }
            }

            foreach (MobileParty party in MobileParty.All)
            {
                if (party == null)
                {
                    continue;
                }

                rosterFixed += RepairRoster(manager, party.MemberRoster);
                rosterFixed += RepairRoster(manager, party.PrisonRoster);
            }

            _ran = true;
            if (volunteersFixed > 0 || rosterFixed > 0)
            {
                Debug.Print("[FemaleTroopsAndBandits] rematerialized " + volunteersFixed
                    + " volunteer slot(s) and " + rosterFixed + " roster stack(s) with empty ftr_* shells.");
            }
        }

        private static int RepairVolunteerSlots(MBObjectManager manager, Hero notable)
        {
            if (notable == null)
            {
                return 0;
            }

            CharacterObject[] slots = notable.VolunteerTypes;
            if (slots == null)
            {
                return 0;
            }

            int fixedCount = 0;
            for (int i = 0; i < slots.Length; i++)
            {
                CharacterObject replaced;
                if (!TryReplaceEmptyFemale(manager, slots[i], out replaced))
                {
                    continue;
                }

                slots[i] = replaced;
                fixedCount++;
            }

            return fixedCount;
        }

        private static int RepairRoster(MBObjectManager manager, TroopRoster roster)
        {
            if (roster == null || roster.Count <= 0)
            {
                return 0;
            }

            int fixedCount = 0;
            for (int i = roster.Count - 1; i >= 0; i--)
            {
                TroopRosterElement element = roster.GetElementCopyAtIndex(i);
                CharacterObject troop = element.Character;
                CharacterObject replaced;
                if (!TryReplaceEmptyFemale(manager, troop, out replaced))
                {
                    continue;
                }

                int number = element.Number;
                int wounded = element.WoundedNumber;
                int xp = element.Xp;
                roster.AddToCounts(troop, -number, false, -wounded, -xp, true, -1);
                if (replaced != null && number > 0)
                {
                    roster.AddToCounts(replaced, number, false, wounded, xp, true, -1);
                }

                fixedCount++;
            }

            return fixedCount;
        }

        // Returns true when the slot/stack should be rewritten (replaced may be null = clear).
        private static bool TryReplaceEmptyFemale(MBObjectManager manager, CharacterObject troop, out CharacterObject replaced)
        {
            replaced = null;
            if (troop == null || string.IsNullOrEmpty(troop.StringId))
            {
                return false;
            }

            string id = troop.StringId;
            if (!id.StartsWith("ftr_", StringComparison.Ordinal))
            {
                return false;
            }

            if (IsUsable(troop))
            {
                return false;
            }

            CharacterObject live = manager.GetObject<CharacterObject>(id);
            if (live != null && !object.ReferenceEquals(live, troop) && IsUsable(live))
            {
                replaced = live;
                return true;
            }

            // Still empty after optional XML: drop the shell so vanilla recruit/combat cannot NRE.
            replaced = null;
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
