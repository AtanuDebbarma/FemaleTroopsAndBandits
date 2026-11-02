using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using TaleWorlds.Library;

namespace FemaleTroopsAndBandits
{
    // Female troops are rolled only while one of these methods is filling a party
    // from a template, raising militia, adding a bandit boss, stocking an alley,
    // or spawning a quest, conspiracy, or War Sails party. A quest screen that
    // moves a soldier the player already chose is not in this list, so the roster
    // keeps that exact soldier.
    [HarmonyPatch]
    internal static class SpawnFillSwap
    {
        private static readonly string[] Required =
        {
            "TaleWorlds.CampaignSystem.GameComponents.DefaultPartySizeLimitModel", "FindAppropriateInitialRosterForMobileParty",
            "Helpers.MobilePartyHelper", "FillPartyManuallyAfterCreation",
            "TaleWorlds.CampaignSystem.Settlements.Settlement", "AddTroopToMilitiaParty",
            "TaleWorlds.CampaignSystem.CampaignBehaviors.BanditSpawnCampaignBehavior", "CheckForSpawningBanditBoss",
            "TaleWorlds.CampaignSystem.GameComponents.DefaultAlleyModel", "GetTroopsOfAlleyInternal",
            "TaleWorlds.CampaignSystem.GameComponents.DefaultAlleyModel", "GetTroopsToRecruitFromAlleyDependingOnAlleyRandom",
        };

        // Absent when that module or quest is not loaded. Parties those quests
        // spawn then stay vanilla, and the quest itself still runs.
        private static readonly string[] Optional =
        {
            "EscortMerchantCaravanIssueQuest", "ActivateBanditParty",
            "ExtortionByDesertersIssueQuest", "CreateDeserterParty",
            "ExtortionByDesertersIssueQuest", "CreateDefenderParty",
            "SmugglersIssueQuest", "CreateSmugglerParty",
            "GangLeaderNeedsWeaponsIssueQuest", "CreateGuardsParty",
            "LandlordNeedsAccessToVillageCommonsIssueQuest", "SpawnRivalParty",
            "LandlordNeedsAccessToVillageCommonsIssueQuest", "SpawnHerdersParty",
            "LesserNobleRevoltIssueQuest", "SpawnLesserNobleParty",
            "MerchantArmyOfPoachersIssueQuest", "CreatePoachersParty",
            "LandLordCompanyOfTroubleIssueQuest", "CreateCompanyEnemyParty",
            "RivalGangMovingInIssueQuest", "CreateRivalGangLeaderParty",
            "RivalGangMovingInIssueQuest", "CreateAllyGangLeaderParty",
            "SnareTheWealthyIssueQuest", "SpawnQuestParties",
            "StoryMode.Quests.SecondPhase.ConspiracyQuestBase", "DistributeConspiracyRaiderTroopsByLevel",
            "ConspiracyBaseOfOperationsDiscoveredConspiracyQuest", "CreateRaiderParty",
            "ConspiracyBaseOfOperationsDiscoveredConspiracyQuest", "ChangeHideoutParties",
            "DestroyRaidersConspiracyQuest", "SpawnRaiderPartyAtHideout",
            "FindHideoutTutorialQuest", "CreateRaiderParty",
            "FindHideoutTutorialQuest", "InitializeHideout",
            "CaptureTheImperialMerchantPrusas", "SpawnMainCorsairParty",
            "FreeTheSeaHoundsCaptivesQuest", "FillParty",
            "DefeatThePiratesQuest", "RefreshPiratePartyForces",
            "NavalDLC.CampaignBehaviors.PiratesCampaignBehavior", "SpawnPirateParty",
        };

        static IEnumerable<MethodBase> TargetMethods()
        {
            List<MethodBase> found = new List<MethodBase>();
            AddAll(found, Required, true);
            AddAll(found, Optional, false);
            return found;
        }

        static void Prefix()
        {
            FemalePartyTroopPatch.AllowSwap();
        }

        static void Finalizer()
        {
            FemalePartyTroopPatch.EndAllowSwap();
        }

        private static void AddAll(List<MethodBase> found, string[] pairs, bool required)
        {
            for (int i = 0; i + 1 < pairs.Length; i += 2)
            {
                Add(found, pairs[i], pairs[i + 1], required);
            }
        }

        private static void Add(List<MethodBase> found, string typeName, string methodName, bool required)
        {
            Type type = AccessTools.TypeByName(typeName);
            if (type == null)
            {
                if (required)
                {
                    Debug.Print("[FemaleTroopsAndBandits] spawn fill hook missing type " + typeName + ".");
                }

                return;
            }

            MethodInfo method = AccessTools.Method(type, methodName);
            if (method == null)
            {
                Debug.Print("[FemaleTroopsAndBandits] spawn fill hook missing " + typeName + "." + methodName + ".");
                return;
            }

            found.Add(method);
        }
    }
}
