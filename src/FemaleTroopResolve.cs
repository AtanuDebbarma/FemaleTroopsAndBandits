using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace FemaleTroopsAndBandits
{
    internal static class FemaleTroopResolve
    {
        private static readonly HashSet<string> ReportedMissing = new HashSet<string>();

        internal static CharacterObject GetOrWarn(MBObjectManager manager, string femaleId)
        {
            if (manager == null || string.IsNullOrEmpty(femaleId))
            {
                return null;
            }

            CharacterObject female = manager.GetObject<CharacterObject>(femaleId);
            if (female != null && IsUsable(female))
            {
                return female;
            }

            if (ReportedMissing.Add(femaleId))
            {
                Debug.Print("[FemaleTroopsAndBandits] mapped female troop '" + femaleId + "' was not found. Keeping the male troop.");
            }

            return null;
        }

        private static bool IsUsable(CharacterObject troop)
        {
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
