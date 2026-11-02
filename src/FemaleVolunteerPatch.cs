using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;

namespace FemaleTroopsAndBandits
{
    // After CMT (castle.market.towns) and other Normal-priority postfixes.
    // Maps vanilla regular/elite roots chosen by the volunteer model to independent female roots.
    [HarmonyPatch(typeof(DefaultVolunteerModel), "GetBasicVolunteer")]
    [HarmonyPriority(Priority.Last)]
    internal static class FemaleVolunteerPatch
    {
        static void Postfix(Hero sellerHero, ref CharacterObject __result)
        {
            if (sellerHero == null || __result == null || string.IsNullOrEmpty(__result.StringId))
            {
                return;
            }

            string femaleId;
            string groupId;
            if (!FemaleTroopCatalog.TryVolunteer(__result.StringId, out femaleId, out groupId))
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
                __result = female;
            }
        }
    }
}
