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
        private static bool _finished;

        internal static void ResetForNewGameSession()
        {
            _finished = false;
        }

        internal static void TryRepairAfterOptionalLoad()
        {
            if (_finished || Campaign.Current == null)
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
            int skippedPending = 0;

            foreach (Settlement settlement in Settlement.All)
            {
                if (settlement == null || settlement.Notables == null)
                {
                    continue;
                }

                foreach (Hero notable in settlement.Notables)
                {
                    int skipped;
                    volunteersFixed += RepairVolunteerSlots(manager, notable, out skipped);
                    skippedPending += skipped;
                }
            }

            foreach (MobileParty party in MobileParty.All)
            {
                if (party == null)
                {
                    continue;
                }

                int skippedMembers;
                int skippedPrisoners;
                rosterFixed += RepairRoster(manager, party.MemberRoster, out skippedMembers);
                rosterFixed += RepairRoster(manager, party.PrisonRoster, out skippedPrisoners);
                skippedPending += skippedMembers + skippedPrisoners;
            }

            if (volunteersFixed > 0 || rosterFixed > 0)
            {
                Debug.Print("[FemaleTroopsAndBandits] rematerialized " + volunteersFixed
                    + " volunteer slot(s) and " + rosterFixed + " roster stack(s) with empty ftr_* shells.");
            }

            // Finish only when no optional pack is still Pending. Ready packs are repaired even if
            // another pack is still loading; Pending shells are left alone until that pack settles.
            if (OptionalTroopXmlLoader.IsSettled() && skippedPending == 0)
            {
                _finished = true;
            }
        }

        private static int RepairVolunteerSlots(MBObjectManager manager, Hero notable, out int skippedPending)
        {
            skippedPending = 0;
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
                RepairAction action = TryReplaceEmptyFemale(manager, slots[i], out replaced);
                if (action == RepairAction.SkipPending)
                {
                    skippedPending++;
                    continue;
                }

                if (action == RepairAction.None)
                {
                    continue;
                }

                slots[i] = replaced;
                fixedCount++;
            }

            return fixedCount;
        }

        private static int RepairRoster(MBObjectManager manager, TroopRoster roster, out int skippedPending)
        {
            skippedPending = 0;
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
                RepairAction action = TryReplaceEmptyFemale(manager, troop, out replaced);
                if (action == RepairAction.SkipPending)
                {
                    skippedPending++;
                    continue;
                }

                if (action == RepairAction.None)
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

        private enum RepairAction
        {
            None,
            Rewrite,
            SkipPending,
        }

        // Rewrite: replaced may be null (= clear shell). SkipPending: wait for that optional pack.
        private static RepairAction TryReplaceEmptyFemale(
            MBObjectManager manager,
            CharacterObject troop,
            out CharacterObject replaced)
        {
            replaced = null;
            if (troop == null || string.IsNullOrEmpty(troop.StringId))
            {
                return RepairAction.None;
            }

            string id = troop.StringId;
            if (!id.StartsWith("ftr_", StringComparison.Ordinal))
            {
                return RepairAction.None;
            }

            if (IsUsable(troop))
            {
                return RepairAction.None;
            }

            if (!OptionalTroopXmlLoader.CanRepairTroopId(id))
            {
                return RepairAction.SkipPending;
            }

            CharacterObject live = manager.GetObject<CharacterObject>(id);
            if (live != null && !object.ReferenceEquals(live, troop) && IsUsable(live))
            {
                replaced = live;
                return RepairAction.Rewrite;
            }

            // Pack Ready/Abandoned/Inactive/NotOptional and still empty: drop the shell.
            replaced = null;
            return RepairAction.Rewrite;
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
