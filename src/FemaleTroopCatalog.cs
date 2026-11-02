using System.Collections.Generic;

namespace FemaleTroopsAndBandits
{
    internal static class FemaleTroopCatalog
    {
        internal sealed class Swap
        {
            public readonly string FemaleId;
            public readonly string GroupId;

            public Swap(string femaleId, string groupId)
            {
                FemaleId = femaleId;
                GroupId = groupId;
            }
        }

        private static readonly Dictionary<string, Swap> Volunteers = new Dictionary<string, Swap>
        {
            { "imperial_recruit", new Swap("ftr_imperial_recruit", FemaleTroopsSettings.GroupEmpire) },
            { "imperial_vigla_recruit", new Swap("ftr_imperial_vigla_recruit", FemaleTroopsSettings.GroupEmpire) },
            { "aserai_recruit", new Swap("ftr_aserai_recruit", FemaleTroopsSettings.GroupAserai) },
            { "aserai_youth", new Swap("ftr_aserai_youth", FemaleTroopsSettings.GroupAserai) },
            { "battanian_volunteer", new Swap("ftr_battanian_volunteer", FemaleTroopsSettings.GroupBattania) },
            { "battanian_highborn_youth", new Swap("ftr_battanian_highborn_youth", FemaleTroopsSettings.GroupBattania) },
            { "khuzait_nomad", new Swap("ftr_khuzait_nomad", FemaleTroopsSettings.GroupKhuzait) },
            { "khuzait_noble_son", new Swap("khuzait_noble_daughter", FemaleTroopsSettings.GroupKhuzait) },
            { "sturgian_recruit", new Swap("ftr_sturgian_recruit", FemaleTroopsSettings.GroupSturgia) },
            { "sturgian_warrior_son", new Swap("sturgian_warrior_daughter", FemaleTroopsSettings.GroupSturgia) },
            { "vlandian_recruit", new Swap("ftr_vlandian_recruit", FemaleTroopsSettings.GroupVlandia) },
            { "vlandian_squire", new Swap("ftr_vlandian_squire", FemaleTroopsSettings.GroupVlandia) },
            { "nord_youngling", new Swap("ftr_nord_youngling", FemaleTroopsSettings.GroupNord) },
            { "nord_ungmann", new Swap("ftr_nord_ungmann", FemaleTroopsSettings.GroupNord) },
        };

        private static readonly Dictionary<string, Swap> PartyTroops = new Dictionary<string, Swap>
        {
            { "ghilman_tier_1", new Swap("ftr_ghilman_tier_1", FemaleTroopsSettings.GroupGhilman) },
            { "ghilman_tier_2", new Swap("ghilwoman_tier_2", FemaleTroopsSettings.GroupGhilman) },
            { "ghilman_tier_3", new Swap("ghilwoman_tier_3", FemaleTroopsSettings.GroupGhilman) },
            { "legion_of_the_betrayed_tier_1", new Swap("ftr_legion_of_the_betrayed_tier_1", FemaleTroopsSettings.GroupLegion) },
            { "legion_of_the_betrayed_tier_2", new Swap("female_legion_of_the_betrayed_tier_2", FemaleTroopsSettings.GroupLegion) },
            { "legion_of_the_betrayed_tier_3", new Swap("female_legion_of_the_betrayed_tier_3", FemaleTroopsSettings.GroupLegion) },
            { "skolderbrotva_tier_1", new Swap("ftr_skolderbrotva_tier_1", FemaleTroopsSettings.GroupSkolderbroda) },
            { "skolderbrotva_tier_2", new Swap("female_skolderbrotva_tier_2", FemaleTroopsSettings.GroupSkolderbroda) },
            { "skolderbrotva_tier_3", new Swap("female_skolderbrotva_tier_3", FemaleTroopsSettings.GroupSkolderbroda) },
            { "company_of_the_boar_tier_1", new Swap("ftr_company_of_the_boar_tier_1", FemaleTroopsSettings.GroupBoar) },
            { "company_of_the_boar_tier_2", new Swap("female_company_of_the_boar_tier_2", FemaleTroopsSettings.GroupBoar) },
            { "company_of_the_boar_tier_3", new Swap("female_company_of_the_boar_tier_3", FemaleTroopsSettings.GroupBoar) },
            { "beni_zilal_tier_1", new Swap("ftr_beni_zilal_tier_1", FemaleTroopsSettings.GroupBeniZilal) },
            { "beni_zilal_tier_2", new Swap("female_beni_zilal_tier_2", FemaleTroopsSettings.GroupBeniZilal) },
            { "beni_zilal_tier_3", new Swap("female_beni_zilal_tier_3", FemaleTroopsSettings.GroupBeniZilal) },
            { "wolfskins_tier_1", new Swap("ftr_wolfskins_tier_1", FemaleTroopsSettings.GroupWolfskins) },
            { "wolfskins_tier_2", new Swap("female_wolfskins_tier_2", FemaleTroopsSettings.GroupWolfskins) },
            { "wolfskins_tier_3", new Swap("female_wolfskins_tier_3", FemaleTroopsSettings.GroupWolfskins) },
            { "hidden_hand_tier_1", new Swap("ftr_hidden_hand_tier_1", FemaleTroopsSettings.GroupHiddenHand) },
            { "hidden_hand_tier_2", new Swap("female_hidden_hand_tier_2", FemaleTroopsSettings.GroupHiddenHand) },
            { "hidden_hand_tier_3", new Swap("female_hidden_hand_tier_3", FemaleTroopsSettings.GroupHiddenHand) },
            { "lakepike_tier_1", new Swap("ftr_lakepike_tier_1", FemaleTroopsSettings.GroupLakeRats) },
            { "lakepike_tier_2", new Swap("female_lakepike_tier_2", FemaleTroopsSettings.GroupLakeRats) },
            { "lakepike_tier_3", new Swap("female_lakepike_tier_3", FemaleTroopsSettings.GroupLakeRats) },
            { "brotherhood_of_woods_tier_1", new Swap("ftr_brotherhood_of_woods_tier_1", FemaleTroopsSettings.GroupBrotherhood) },
            { "brotherhood_of_woods_tier_2", new Swap("female_brotherhood_of_woods_tier_2", FemaleTroopsSettings.GroupBrotherhood) },
            { "brotherhood_of_woods_tier_3", new Swap("female_brotherhood_of_woods_tier_3", FemaleTroopsSettings.GroupBrotherhood) },
            { "embers_of_flame_tier_1", new Swap("ftr_embers_of_flame_tier_1", FemaleTroopsSettings.GroupEmbers) },
            { "embers_of_flame_tier_2", new Swap("female_embers_of_flame_tier_2", FemaleTroopsSettings.GroupEmbers) },
            { "embers_of_flame_tier_3", new Swap("female_embers_of_flame_tier_3", FemaleTroopsSettings.GroupEmbers) },
            { "jawwal_tier_1", new Swap("ftr_jawwal_tier_1", FemaleTroopsSettings.GroupJawwal) },
            { "jawwal_tier_2", new Swap("female_jawwal_tier_2", FemaleTroopsSettings.GroupJawwal) },
            { "jawwal_tier_3", new Swap("female_jawwal_tier_3", FemaleTroopsSettings.GroupJawwal) },
            { "karakhuzaits_tier_1", new Swap("ftr_karakhuzaits_tier_1", FemaleTroopsSettings.GroupKarakhergit) },
            { "karakhuzaits_tier_2", new Swap("female_karakhuzaits_tier_2", FemaleTroopsSettings.GroupKarakhergit) },
            { "karakhuzaits_tier_3", new Swap("female_karakhuzaits_tier_3", FemaleTroopsSettings.GroupKarakhergit) },
            { "forest_people_tier_1", new Swap("ftr_forest_people_tier_1", FemaleTroopsSettings.GroupForestPeople) },
            { "forest_people_tier_2", new Swap("female_forest_people_tier_2", FemaleTroopsSettings.GroupForestPeople) },
            { "forest_people_tier_3", new Swap("female_forest_people_tier_3", FemaleTroopsSettings.GroupForestPeople) },
            { "eleftheroi_tier_1", new Swap("female_eleftheroi_tier_1", FemaleTroopsSettings.GroupEleftheroi) },
            { "eleftheroi_tier_2", new Swap("female_eleftheroi_tier_2", FemaleTroopsSettings.GroupEleftheroi) },
            { "eleftheroi_tier_3", new Swap("female_eleftheroi_tier_3", FemaleTroopsSettings.GroupEleftheroi) },
            { "gangster_1", new Swap("ftr_gangster_1", FemaleTroopsSettings.GroupGangsters) },
            { "gangster_2", new Swap("female_gangster_2", FemaleTroopsSettings.GroupGangsters) },
            { "gangster_3", new Swap("female_gangster_3", FemaleTroopsSettings.GroupGangsters) },
            { "empire_marine_t3", new Swap("ftr_empire_marine_t3", FemaleTroopsSettings.GroupMarineEmpire) },
            { "empire_marine_t4", new Swap("ftr_empire_marine_t4", FemaleTroopsSettings.GroupMarineEmpire) },
            { "empire_marine_t5", new Swap("ftr_empire_marine_t5", FemaleTroopsSettings.GroupMarineEmpire) },
            { "sturgia_marine_t3", new Swap("ftr_sturgia_marine_t3", FemaleTroopsSettings.GroupMarineSturgia) },
            { "sturgia_marine_t4", new Swap("ftr_sturgia_marine_t4", FemaleTroopsSettings.GroupMarineSturgia) },
            { "sturgia_marine_t5", new Swap("ftr_sturgia_marine_t5", FemaleTroopsSettings.GroupMarineSturgia) },
            { "aserai_marine_t4", new Swap("ftr_aserai_marine_t4", FemaleTroopsSettings.GroupMarineAserai) },
            { "aserai_marine_t5", new Swap("ftr_aserai_marine_t5", FemaleTroopsSettings.GroupMarineAserai) },
            { "battanian_marine_t4", new Swap("ftr_battanian_marine_t4", FemaleTroopsSettings.GroupMarineBattania) },
            { "battanian_marine_t5", new Swap("ftr_battanian_marine_t5", FemaleTroopsSettings.GroupMarineBattania) },
            { "vlandian_marine_t4", new Swap("ftr_vlandian_marine_t4", FemaleTroopsSettings.GroupMarineVlandia) },
            { "vlandian_marine_t5", new Swap("ftr_vlandian_marine_t5", FemaleTroopsSettings.GroupMarineVlandia) },
            { "imperial_militia_archer", new Swap("ftr_imperial_militia_archer", FemaleTroopsSettings.GroupMilitiaEmpire) },
            { "imperial_militia_veteran_archer", new Swap("ftr_imperial_militia_veteran_archer", FemaleTroopsSettings.GroupMilitiaEmpire) },
            { "imperial_militia_spearman", new Swap("ftr_imperial_militia_spearman", FemaleTroopsSettings.GroupMilitiaEmpire) },
            { "imperial_militia_veteran_spearman", new Swap("ftr_imperial_militia_veteran_spearman", FemaleTroopsSettings.GroupMilitiaEmpire) },
            { "aserai_militia_archer", new Swap("ftr_aserai_militia_archer", FemaleTroopsSettings.GroupMilitiaAserai) },
            { "aserai_militia_veteran_archer", new Swap("ftr_aserai_militia_veteran_archer", FemaleTroopsSettings.GroupMilitiaAserai) },
            { "aserai_militia_spearman", new Swap("ftr_aserai_militia_spearman", FemaleTroopsSettings.GroupMilitiaAserai) },
            { "aserai_militia_veteran_spearman", new Swap("ftr_aserai_militia_veteran_spearman", FemaleTroopsSettings.GroupMilitiaAserai) },
            { "battanian_militia_archer", new Swap("ftr_battanian_militia_archer", FemaleTroopsSettings.GroupMilitiaBattania) },
            { "battanian_militia_veteran_archer", new Swap("ftr_battanian_militia_veteran_archer", FemaleTroopsSettings.GroupMilitiaBattania) },
            { "battanian_militia_spearman", new Swap("ftr_battanian_militia_spearman", FemaleTroopsSettings.GroupMilitiaBattania) },
            { "battanian_militia_veteran_spearman", new Swap("ftr_battanian_militia_veteran_spearman", FemaleTroopsSettings.GroupMilitiaBattania) },
            { "khuzait_militia_archer", new Swap("ftr_khuzait_militia_archer", FemaleTroopsSettings.GroupMilitiaKhuzait) },
            { "khuzait_militia_veteran_archer", new Swap("ftr_khuzait_militia_veteran_archer", FemaleTroopsSettings.GroupMilitiaKhuzait) },
            { "khuzait_militia_spearman", new Swap("ftr_khuzait_militia_spearman", FemaleTroopsSettings.GroupMilitiaKhuzait) },
            { "khuzait_militia_veteran_spearman", new Swap("ftr_khuzait_militia_veteran_spearman", FemaleTroopsSettings.GroupMilitiaKhuzait) },
            { "sturgian_militia_archer", new Swap("ftr_sturgian_militia_archer", FemaleTroopsSettings.GroupMilitiaSturgia) },
            { "sturgian_militia_veteran_archer", new Swap("ftr_sturgian_militia_veteran_archer", FemaleTroopsSettings.GroupMilitiaSturgia) },
            { "sturgian_militia_spearman", new Swap("ftr_sturgian_militia_spearman", FemaleTroopsSettings.GroupMilitiaSturgia) },
            { "sturgian_militia_veteran_spearman", new Swap("ftr_sturgian_militia_veteran_spearman", FemaleTroopsSettings.GroupMilitiaSturgia) },
            { "vlandian_militia_archer", new Swap("ftr_vlandian_militia_archer", FemaleTroopsSettings.GroupMilitiaVlandia) },
            { "vlandian_militia_veteran_archer", new Swap("ftr_vlandian_militia_veteran_archer", FemaleTroopsSettings.GroupMilitiaVlandia) },
            { "vlandian_militia_spearman", new Swap("ftr_vlandian_militia_spearman", FemaleTroopsSettings.GroupMilitiaVlandia) },
            { "vlandian_militia_veteran_spearman", new Swap("ftr_vlandian_militia_veteran_spearman", FemaleTroopsSettings.GroupMilitiaVlandia) },
            { "nord_militia_archer", new Swap("ftr_nord_militia_archer", FemaleTroopsSettings.GroupMilitiaNord) },
            { "nord_militia_veteran_archer", new Swap("ftr_nord_militia_veteran_archer", FemaleTroopsSettings.GroupMilitiaNord) },
            { "nord_militia_spearman", new Swap("ftr_nord_militia_spearman", FemaleTroopsSettings.GroupMilitiaNord) },
            { "nord_militia_veteran_spearman", new Swap("ftr_nord_militia_veteran_spearman", FemaleTroopsSettings.GroupMilitiaNord) },
            { "eastern_mercenary", new Swap("ftr_eastern_mercenary", FemaleTroopsSettings.GroupMercenaries) },
            { "eastern_mercenary_t4", new Swap("ftr_eastern_mercenary_t4", FemaleTroopsSettings.GroupMercenaries) },
            { "eastern_mercenary_t5", new Swap("ftr_eastern_mercenary_t5", FemaleTroopsSettings.GroupMercenaries) },
            { "eastern_mounted_mercenary_t4", new Swap("ftr_eastern_mounted_mercenary_t4", FemaleTroopsSettings.GroupMercenaries) },
            { "eastern_mounted_mercenary_t5", new Swap("ftr_eastern_mounted_mercenary_t5", FemaleTroopsSettings.GroupMercenaries) },
            { "western_mercenary", new Swap("ftr_western_mercenary", FemaleTroopsSettings.GroupMercenaries) },
            { "western_mercenary_t4", new Swap("ftr_western_mercenary_t4", FemaleTroopsSettings.GroupMercenaries) },
            { "western_mercenary_t5", new Swap("ftr_western_mercenary_t5", FemaleTroopsSettings.GroupMercenaries) },
            { "western_crossbow_t4", new Swap("ftr_western_crossbow_t4", FemaleTroopsSettings.GroupMercenaries) },
            { "western_crossbow_t5", new Swap("ftr_western_crossbow_t5", FemaleTroopsSettings.GroupMercenaries) },
            { "looter", new Swap("ftr_looter", FemaleTroopsSettings.GroupLooters) },
            { "sea_raiders_bandit", new Swap("ftr_sea_raiders_bandit", FemaleTroopsSettings.GroupSeaRaiders) },
            { "sea_raiders_raider", new Swap("ftr_sea_raiders_raider", FemaleTroopsSettings.GroupSeaRaiders) },
            { "sea_raiders_chief", new Swap("ftr_sea_raiders_chief", FemaleTroopsSettings.GroupSeaRaiders) },
            { "sea_raiders_boss", new Swap("ftr_sea_raiders_boss", FemaleTroopsSettings.GroupSeaRaiders) },
            { "mountain_bandits_bandit", new Swap("ftr_mountain_bandits_bandit", FemaleTroopsSettings.GroupMountain) },
            { "mountain_bandits_raider", new Swap("ftr_mountain_bandits_raider", FemaleTroopsSettings.GroupMountain) },
            { "mountain_bandits_chief", new Swap("ftr_mountain_bandits_chief", FemaleTroopsSettings.GroupMountain) },
            { "mountain_bandits_boss", new Swap("ftr_mountain_bandits_boss", FemaleTroopsSettings.GroupMountain) },
            { "forest_bandits_bandit", new Swap("ftr_forest_bandits_bandit", FemaleTroopsSettings.GroupForest) },
            { "forest_bandits_raider", new Swap("ftr_forest_bandits_raider", FemaleTroopsSettings.GroupForest) },
            { "forest_bandits_chief", new Swap("ftr_forest_bandits_chief", FemaleTroopsSettings.GroupForest) },
            { "forest_bandits_boss", new Swap("ftr_forest_bandits_boss", FemaleTroopsSettings.GroupForest) },
            { "desert_bandits_bandit", new Swap("ftr_desert_bandits_bandit", FemaleTroopsSettings.GroupDesert) },
            { "desert_bandits_raider", new Swap("ftr_desert_bandits_raider", FemaleTroopsSettings.GroupDesert) },
            { "desert_bandits_chief", new Swap("ftr_desert_bandits_chief", FemaleTroopsSettings.GroupDesert) },
            { "desert_bandits_boss", new Swap("ftr_desert_bandits_boss", FemaleTroopsSettings.GroupDesert) },
            { "steppe_bandits_bandit", new Swap("ftr_steppe_bandits_bandit", FemaleTroopsSettings.GroupSteppe) },
            { "steppe_bandits_raider", new Swap("ftr_steppe_bandits_raider", FemaleTroopsSettings.GroupSteppe) },
            { "steppe_bandits_chief", new Swap("ftr_steppe_bandits_chief", FemaleTroopsSettings.GroupSteppe) },
            { "steppe_bandits_boss", new Swap("ftr_steppe_bandits_boss", FemaleTroopsSettings.GroupSteppe) },
            { "southern_pirates_bandit", new Swap("ftr_southern_pirates_bandit", FemaleTroopsSettings.GroupCorsairs) },
            { "southern_pirates_raider", new Swap("ftr_southern_pirates_raider", FemaleTroopsSettings.GroupCorsairs) },
            { "southern_pirates_chief", new Swap("ftr_southern_pirates_chief", FemaleTroopsSettings.GroupCorsairs) },
            { "conspiracy_wilder", new Swap("ftr_conspiracy_wilder", FemaleTroopsSettings.GroupConspiracyAnti) },
            { "conspiracy_warmonger", new Swap("ftr_conspiracy_warmonger", FemaleTroopsSettings.GroupConspiracyAnti) },
            { "conspiracy_trained_bowman", new Swap("ftr_conspiracy_trained_bowman", FemaleTroopsSettings.GroupConspiracyAnti) },
            { "conspiracy_longbowman", new Swap("ftr_conspiracy_longbowman", FemaleTroopsSettings.GroupConspiracyAnti) },
            { "conspiracy_kern", new Swap("ftr_conspiracy_kern", FemaleTroopsSettings.GroupConspiracyAnti) },
            { "conspiracy_battlerider", new Swap("ftr_conspiracy_battlerider", FemaleTroopsSettings.GroupConspiracyAnti) },
            { "conspiracy_horse_archer", new Swap("ftr_conspiracy_horse_archer", FemaleTroopsSettings.GroupConspiracyAnti) },
            { "conspiracy_fighter", new Swap("ftr_conspiracy_fighter", FemaleTroopsSettings.GroupConspiracyImperial) },
            { "conspiracy_veteran_fighter", new Swap("ftr_conspiracy_veteran_fighter", FemaleTroopsSettings.GroupConspiracyImperial) },
            { "conspiracy_warworn_crossbowman", new Swap("ftr_conspiracy_warworn_crossbowman", FemaleTroopsSettings.GroupConspiracyImperial) },
            { "conspiracy_trained_huntsman", new Swap("ftr_conspiracy_trained_huntsman", FemaleTroopsSettings.GroupConspiracyImperial) },
            { "conspiracy_noble_horseman", new Swap("ftr_conspiracy_noble_horseman", FemaleTroopsSettings.GroupConspiracyImperial) },
            { "conspiracy_mounted_fighter", new Swap("ftr_conspiracy_mounted_fighter", FemaleTroopsSettings.GroupConspiracyImperial) },
            { "conspiracy_knight", new Swap("ftr_conspiracy_knight", FemaleTroopsSettings.GroupConspiracyImperial) },
            { "mounted_pillager", new Swap("ftr_mounted_pillager", FemaleTroopsSettings.GroupHorseBandits) },
            { "mounted_ransacker", new Swap("ftr_mounted_ransacker", FemaleTroopsSettings.GroupHorseBandits) },
            { "horse_bandits_mounted_huntsman", new Swap("ftr_horse_bandits_mounted_huntsman", FemaleTroopsSettings.GroupHorseBandits) },
            { "horse_bandits_mounted_packmaster", new Swap("ftr_horse_bandits_mounted_packmaster", FemaleTroopsSettings.GroupHorseBandits) },
            { "horse_bandits_imperial_bandit_horseman", new Swap("ftr_horse_bandits_imperial_bandit_horseman", FemaleTroopsSettings.GroupHorseBandits) },
            { "horse_bandits_imperial_elite_bandit_horseman", new Swap("ftr_horse_bandits_imperial_elite_bandit_horseman", FemaleTroopsSettings.GroupHorseBandits) },
            { "horse_bandits_chief", new Swap("ftr_horse_bandits_chief", FemaleTroopsSettings.GroupHorseBandits) },
            { "horse_bandits_boss", new Swap("ftr_horse_bandits_boss", FemaleTroopsSettings.GroupHorseBandits) },
            { "horse_bandits_vlandian_brigand_champion", new Swap("ftr_horse_bandits_vlandian_brigand_champion", FemaleTroopsSettings.GroupHorseBandits) },
            { "horse_bandits_vlandian_brigand_banner_knight", new Swap("ftr_horse_bandits_vlandian_brigand_banner_knight", FemaleTroopsSettings.GroupHorseBandits) },
            { "poacher", new Swap("ftr_poacher", FemaleTroopsSettings.GroupPoachers) },
            { "armed_trader_aserai", new Swap("ftr_armed_trader_aserai", FemaleTroopsSettings.GroupCaravan) },
            { "armed_trader_battania", new Swap("ftr_armed_trader_battania", FemaleTroopsSettings.GroupCaravan) },
            { "armed_trader_empire", new Swap("ftr_armed_trader_empire", FemaleTroopsSettings.GroupCaravan) },
            { "armed_trader_khuzait", new Swap("ftr_armed_trader_khuzait", FemaleTroopsSettings.GroupCaravan) },
            { "armed_trader_sturgia", new Swap("ftr_armed_trader_sturgia", FemaleTroopsSettings.GroupCaravan) },
            { "armed_trader_vlandia", new Swap("ftr_armed_trader_vlandia", FemaleTroopsSettings.GroupCaravan) },
            { "armed_trader_nord", new Swap("ftr_armed_trader_nord", FemaleTroopsSettings.GroupCaravan) },
            { "caravan_guard_aserai", new Swap("ftr_caravan_guard_aserai", FemaleTroopsSettings.GroupCaravan) },
            { "caravan_guard_battania", new Swap("ftr_caravan_guard_battania", FemaleTroopsSettings.GroupCaravan) },
            { "caravan_guard_empire", new Swap("ftr_caravan_guard_empire", FemaleTroopsSettings.GroupCaravan) },
            { "caravan_guard_khuzait", new Swap("ftr_caravan_guard_khuzait", FemaleTroopsSettings.GroupCaravan) },
            { "caravan_guard_sturgia", new Swap("ftr_caravan_guard_sturgia", FemaleTroopsSettings.GroupCaravan) },
            { "caravan_guard_vlandia", new Swap("ftr_caravan_guard_vlandia", FemaleTroopsSettings.GroupCaravan) },
            { "caravan_guard_nord", new Swap("ftr_caravan_guard_nord", FemaleTroopsSettings.GroupCaravan) },
            { "veteran_caravan_guard_aserai", new Swap("ftr_veteran_caravan_guard_aserai", FemaleTroopsSettings.GroupCaravan) },
            { "veteran_caravan_guard_battania", new Swap("ftr_veteran_caravan_guard_battania", FemaleTroopsSettings.GroupCaravan) },
            { "veteran_caravan_guard_empire", new Swap("ftr_veteran_caravan_guard_empire", FemaleTroopsSettings.GroupCaravan) },
            { "veteran_caravan_guard_khuzait", new Swap("ftr_veteran_caravan_guard_khuzait", FemaleTroopsSettings.GroupCaravan) },
            { "veteran_caravan_guard_sturgia", new Swap("ftr_veteran_caravan_guard_sturgia", FemaleTroopsSettings.GroupCaravan) },
            { "veteran_caravan_guard_vlandia", new Swap("ftr_veteran_caravan_guard_vlandia", FemaleTroopsSettings.GroupCaravan) },
            { "veteran_caravan_guard_nord", new Swap("ftr_veteran_caravan_guard_nord", FemaleTroopsSettings.GroupCaravan) },
            // War Sails naval/convoy caravan templates (separate IDs from land caravans).
            { "naval_armed_trader_aserai", new Swap("ftr_naval_armed_trader_aserai", FemaleTroopsSettings.GroupCaravan) },
            { "naval_armed_trader_battania", new Swap("ftr_naval_armed_trader_battania", FemaleTroopsSettings.GroupCaravan) },
            { "naval_armed_trader_empire", new Swap("ftr_naval_armed_trader_empire", FemaleTroopsSettings.GroupCaravan) },
            { "naval_armed_trader_khuzait", new Swap("ftr_naval_armed_trader_khuzait", FemaleTroopsSettings.GroupCaravan) },
            { "naval_armed_trader_sturgia", new Swap("ftr_naval_armed_trader_sturgia", FemaleTroopsSettings.GroupCaravan) },
            { "naval_armed_trader_vlandia", new Swap("ftr_naval_armed_trader_vlandia", FemaleTroopsSettings.GroupCaravan) },
            { "naval_armed_trader_nord", new Swap("ftr_naval_armed_trader_nord", FemaleTroopsSettings.GroupCaravan) },
            { "naval_caravan_guard_aserai", new Swap("ftr_naval_caravan_guard_aserai", FemaleTroopsSettings.GroupCaravan) },
            { "naval_caravan_guard_battania", new Swap("ftr_naval_caravan_guard_battania", FemaleTroopsSettings.GroupCaravan) },
            { "naval_caravan_guard_empire", new Swap("ftr_naval_caravan_guard_empire", FemaleTroopsSettings.GroupCaravan) },
            { "naval_caravan_guard_khuzait", new Swap("ftr_naval_caravan_guard_khuzait", FemaleTroopsSettings.GroupCaravan) },
            { "naval_caravan_guard_sturgia", new Swap("ftr_naval_caravan_guard_sturgia", FemaleTroopsSettings.GroupCaravan) },
            { "naval_caravan_guard_vlandia", new Swap("ftr_naval_caravan_guard_vlandia", FemaleTroopsSettings.GroupCaravan) },
            { "naval_caravan_guard_nord", new Swap("ftr_naval_caravan_guard_nord", FemaleTroopsSettings.GroupCaravan) },
            { "naval_veteran_caravan_guard_aserai", new Swap("ftr_naval_veteran_caravan_guard_aserai", FemaleTroopsSettings.GroupCaravan) },
            { "naval_veteran_caravan_guard_battania", new Swap("ftr_naval_veteran_caravan_guard_battania", FemaleTroopsSettings.GroupCaravan) },
            { "naval_veteran_caravan_guard_empire", new Swap("ftr_naval_veteran_caravan_guard_empire", FemaleTroopsSettings.GroupCaravan) },
            { "naval_veteran_caravan_guard_khuzait", new Swap("ftr_naval_veteran_caravan_guard_khuzait", FemaleTroopsSettings.GroupCaravan) },
            { "naval_veteran_caravan_guard_sturgia", new Swap("ftr_naval_veteran_caravan_guard_sturgia", FemaleTroopsSettings.GroupCaravan) },
            { "naval_veteran_caravan_guard_vlandia", new Swap("ftr_naval_veteran_caravan_guard_vlandia", FemaleTroopsSettings.GroupCaravan) },
            { "naval_veteran_caravan_guard_nord", new Swap("ftr_naval_veteran_caravan_guard_nord", FemaleTroopsSettings.GroupCaravan) },
        };

        internal static void Visit(List<string> maleIds, List<string> femaleIds, List<string> groupIds)
        {
            VisitMap(Volunteers, maleIds, femaleIds, groupIds);
            VisitMap(PartyTroops, maleIds, femaleIds, groupIds);
        }

        private static void VisitMap(Dictionary<string, Swap> map, List<string> maleIds, List<string> femaleIds, List<string> groupIds)
        {
            foreach (KeyValuePair<string, Swap> pair in map)
            {
                maleIds.Add(pair.Key);
                femaleIds.Add(pair.Value.FemaleId);
                groupIds.Add(pair.Value.GroupId);
            }
        }

        internal static bool TryVolunteer(string maleId, out string femaleId, out string groupId)
        {
            return Try(Volunteers, maleId, out femaleId, out groupId);
        }

        internal static bool TryPartyTroop(string maleId, out string femaleId, out string groupId)
        {
            return Try(PartyTroops, maleId, out femaleId, out groupId);
        }

        private static bool Try(Dictionary<string, Swap> map, string maleId, out string femaleId, out string groupId)
        {
            femaleId = null;
            groupId = null;
            Swap swap;
            if (string.IsNullOrEmpty(maleId) || !map.TryGetValue(maleId, out swap))
            {
                return false;
            }

            femaleId = swap.FemaleId;
            groupId = swap.GroupId;
            return true;
        }
    }
}
