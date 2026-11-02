using System;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;

namespace FemaleTroopsAndBandits
{
    // Raise Your Banner's bearer record has no battle equipment. Auto-resolve reads
    // that record and crashes on the empty set. The bearer itself is still theirs:
    // they already copy the soldier, including female soldiers. If their character
    // is not loaded, this does nothing.
    internal static class RaiseYourBannerCompat
    {
        private const string BearerId = "ryb_banner_bearer";

        internal static void Apply()
        {
            try
            {
                EnsureBattleEquipment();
            }
            catch (Exception)
            {
            }
        }

        private static void EnsureBattleEquipment()
        {
            MBObjectManager manager = MBObjectManager.Instance;
            if (manager == null)
            {
                return;
            }

            CharacterObject bearer = manager.GetObject<CharacterObject>(BearerId);
            if (bearer == null || bearer.FirstBattleEquipment != null)
            {
                return;
            }

            Type equipmentType = typeof(Equipment).GetNestedType(
                "EquipmentType",
                BindingFlags.Public | BindingFlags.NonPublic);
            if (equipmentType == null)
            {
                return;
            }

            object battleKind = Enum.Parse(equipmentType, "Battle");
            object equipment = Activator.CreateInstance(
                typeof(Equipment),
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new object[] { battleKind },
                null);
            if (equipment == null)
            {
                return;
            }

            Type rosterType = typeof(MBEquipmentRoster);
            object roster = Activator.CreateInstance(rosterType, true);
            FieldInfo equipmentList = rosterType.GetField(
                "_equipments",
                BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo rosterField = typeof(BasicCharacterObject).GetField(
                "_equipmentRoster",
                BindingFlags.Instance | BindingFlags.NonPublic);
            if (roster == null || equipmentList == null || rosterField == null)
            {
                return;
            }

            object list = Activator.CreateInstance(equipmentList.FieldType);
            MethodInfo add = equipmentList.FieldType.GetMethod("Add");
            if (list == null || add == null)
            {
                return;
            }

            add.Invoke(list, new object[] { equipment });
            equipmentList.SetValue(roster, list);

            object existing = rosterField.GetValue(bearer);
            if (existing == null)
            {
                rosterField.SetValue(bearer, roster);
                return;
            }

            FieldInfo existingList = existing.GetType().GetField(
                "_equipments",
                BindingFlags.Instance | BindingFlags.NonPublic);
            if (existingList == null)
            {
                return;
            }

            object existingItems = existingList.GetValue(existing);
            if (existingItems == null)
            {
                existingList.SetValue(existing, list);
                return;
            }

            MethodInfo existingAdd = existingItems.GetType().GetMethod("Add");
            if (existingAdd != null)
            {
                existingAdd.Invoke(existingItems, new object[] { equipment });
            }
        }
    }
}
