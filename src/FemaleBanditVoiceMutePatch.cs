using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Localization;

namespace FemaleTroopsAndBandits
{
    // Female bandit party leaders have no matching female VO yet. Returning "" makes
    // MapConversationTableau skip PlayConversationSoundEvent while the cutscene continues.
    // Males always leave this postfix immediately so vanilla voice paths stay untouched.
    [HarmonyPatch(typeof(DefaultVoiceOverModel), "GetSoundPathForCharacter")]
    [HarmonyPriority(Priority.Last)]
    internal static class FemaleBanditVoiceMutePatch
    {
        static void Postfix(CharacterObject character, VoiceObject voiceObject, ref string __result)
        {
            if (character == null || !character.IsFemale)
            {
                return;
            }

            if (!ShouldMute(character))
            {
                return;
            }

            __result = string.Empty;
        }

        private static bool ShouldMute(CharacterObject character)
        {
            if (character.Occupation == Occupation.Bandit)
            {
                return true;
            }

            if (FemaleTroopCatalog.IsBanditLineFemaleId(character.StringId))
            {
                return true;
            }

            // Horse Bandits and similar templates may keep Occupation.Soldier; still mute when
            // the map conversation is with a bandit/hideout party and the speaker id is a bandit line.
            MobileParty party = MobileParty.ConversationParty;
            if (party == null || (!party.IsBandit && !party.IsBanditBossParty))
            {
                return false;
            }

            return LooksLikeBanditUnitId(character.StringId);
        }

        private static bool LooksLikeBanditUnitId(string stringId)
        {
            if (string.IsNullOrEmpty(stringId))
            {
                return false;
            }

            return stringId.IndexOf("looter", System.StringComparison.Ordinal) >= 0
                || stringId.IndexOf("sea_raiders", System.StringComparison.Ordinal) >= 0
                || stringId.IndexOf("mountain_bandits", System.StringComparison.Ordinal) >= 0
                || stringId.IndexOf("forest_bandits", System.StringComparison.Ordinal) >= 0
                || stringId.IndexOf("desert_bandits", System.StringComparison.Ordinal) >= 0
                || stringId.IndexOf("steppe_bandits", System.StringComparison.Ordinal) >= 0
                || stringId.IndexOf("southern_pirates", System.StringComparison.Ordinal) >= 0
                || stringId.IndexOf("poacher", System.StringComparison.Ordinal) >= 0
                || stringId.IndexOf("horse_bandits", System.StringComparison.Ordinal) >= 0
                || stringId.IndexOf("mounted_pillager", System.StringComparison.Ordinal) >= 0
                || stringId.IndexOf("mounted_ransacker", System.StringComparison.Ordinal) >= 0;
        }
    }
}
