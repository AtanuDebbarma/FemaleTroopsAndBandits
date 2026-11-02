using System.Collections.Generic;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace FemaleTroopsAndBandits
{
    public sealed class FemaleTroopsSettings : AttributeGlobalSettings<FemaleTroopsSettings>
    {
        internal const int DefaultPercent = 25;

        internal const string GroupEmpire = "empire";
        internal const string GroupAserai = "aserai";
        internal const string GroupBattania = "battania";
        internal const string GroupKhuzait = "khuzait";
        internal const string GroupSturgia = "sturgia";
        internal const string GroupVlandia = "vlandia";
        internal const string GroupNord = "nord";
        internal const string GroupMilitiaEmpire = "militia_empire";
        internal const string GroupMilitiaAserai = "militia_aserai";
        internal const string GroupMilitiaBattania = "militia_battania";
        internal const string GroupMilitiaKhuzait = "militia_khuzait";
        internal const string GroupMilitiaSturgia = "militia_sturgia";
        internal const string GroupMilitiaVlandia = "militia_vlandia";
        internal const string GroupMilitiaNord = "militia_nord";
        internal const string GroupGhilman = "ghilman";
        internal const string GroupLegion = "legion";
        internal const string GroupSkolderbroda = "skolderbroda";
        internal const string GroupBoar = "boar";
        internal const string GroupBeniZilal = "beni_zilal";
        internal const string GroupWolfskins = "wolfskins";
        internal const string GroupHiddenHand = "hidden_hand";
        internal const string GroupLakeRats = "lake_rats";
        internal const string GroupBrotherhood = "brotherhood";
        internal const string GroupEmbers = "embers";
        internal const string GroupJawwal = "jawwal";
        internal const string GroupKarakhergit = "karakhergit";
        internal const string GroupForestPeople = "forest_people";
        internal const string GroupEleftheroi = "eleftheroi";
        internal const string GroupMercenaries = "mercenaries";
        internal const string GroupGangsters = "gangsters";
        internal const string GroupMarineEmpire = "marine_empire";
        internal const string GroupMarineSturgia = "marine_sturgia";
        internal const string GroupMarineAserai = "marine_aserai";
        internal const string GroupMarineBattania = "marine_battania";
        internal const string GroupMarineVlandia = "marine_vlandia";
        internal const string GroupLooters = "looters";
        internal const string GroupSeaRaiders = "sea_raiders";
        internal const string GroupMountain = "mountain_bandits";
        internal const string GroupForest = "forest_bandits";
        internal const string GroupDesert = "desert_bandits";
        internal const string GroupSteppe = "steppe_bandits";
        internal const string GroupCorsairs = "corsairs";
        internal const string GroupPoachers = "poachers";
        internal const string GroupCaravan = "caravan";
        internal const string GroupConspiracyAnti = "conspiracy_anti";
        internal const string GroupConspiracyImperial = "conspiracy_imperial";
        internal const string GroupHorseBandits = "horse_bandits";

        private const string RootTroops = "{=ftr_mcm_root_troops}Troops";
        private const string RootBandits = "{=ftr_mcm_root_bandits}Bandits";
        private const string Kingdom = RootTroops + "/{=ftr_mcm_group_kingdom}Kingdom Recruits";
        private const string Militia = RootTroops + "/{=ftr_mcm_group_militia}Militia";
        private const string Factions = RootTroops + "/{=ftr_mcm_group_factions}Minor Factions";
        private const string Marines = RootTroops + "/{=ftr_mcm_group_marines}War Sails Marines";
        private const string MercenariesGroup = RootTroops + "/{=ftr_mcm_mercenaries}Mercenaries";
        private const string GangstersGroup = RootTroops + "/{=ftr_mcm_gangsters}Gangsters";
        private const string CaravanGroup = RootTroops + "/{=ftr_mcm_caravan}Caravan Troops";
        private const string Conspiracy = RootTroops + "/{=ftr_mcm_group_conspiracy}Conspiracy";
        private const string Bandits = RootBandits;
        private const string EmpireName = "{=ftr_mcm_empire}Empire";
        private const string AseraiName = "{=ftr_mcm_aserai}Aserai";
        private const string BattaniaName = "{=ftr_mcm_battania}Battania";
        private const string KhuzaitName = "{=ftr_mcm_khuzait}Khuzait";
        private const string SturgiaName = "{=ftr_mcm_sturgia}Sturgia";
        private const string VlandiaName = "{=ftr_mcm_vlandia}Vlandia";
        private const string NordName = "{=ftr_mcm_nord}Nord";
        private const string RecruitHint = "{=ftr_mcm_recruit_hint}Chance a new town, village, or castle recruit of this culture is female (regular and noble). Default 25. Off or 0 keeps new recruits male.";
        private const string RecruitBranchHint = "{=ftr_mcm_recruit_branch_hint}Chance a new town, village, or castle recruit of this culture is female (regular and noble). Default 25. Off or 0 keeps new recruits male. Reopen the encyclopedia after changing this.";
        private const string MilitiaHint = "{=ftr_mcm_militia_hint}Chance a new militia troop of this culture is female. Default 25. Off or 0 keeps new militia male.";
        private const string FactionHint = "{=ftr_mcm_faction_hint}Chance a new troop of this faction is female. Default 25. Off or 0 keeps new troops male.";
        private const string MercHint = "{=ftr_mcm_merc_hint}Chance a new Sellsword, Outrider, Mercenary Maceman, Hired Spear, Hired Pike, or Hired Crossbow is female. Default 25. Sword Sisters stay vanilla. Off or 0 keeps those lines male.";
        private const string GangHint = "{=ftr_mcm_gang_hint}Chance a new gangster is female. Default 25. Off or 0 keeps new gangsters male.";
        private const string MarineHint = "{=ftr_mcm_marine_hint}Chance a new marine of this culture is female, and whether female land troops can upgrade into that marine line. Default 25. Off or 0 keeps marines male and removes that upgrade. Reopen the encyclopedia after changing this.";
        private const string BanditHint = "{=ftr_mcm_bandit_hint}Chance a new troop of this bandit line is female, including bosses and hideouts. Default 25. Off or 0 keeps new troops male.";
        private const string LootersHint = "{=ftr_mcm_looters_hint}Chance a new Looter is female, including hideouts. Deserters use Looters, so they follow this. Default 25. Off or 0 keeps new Looters male.";
        private const string SeaRaidersHint = "{=ftr_mcm_sea_raiders_hint}Chance a new Sea Raider is female, including chiefs, bosses, and hideouts. Northern pirates use these troops. Default 25. With War Sails, upgrades follow the Nord line.";
        private const string CorsairHint = "{=ftr_mcm_corsair_hint}Chance a new Corsair is female. Default 25. Off or 0 keeps them male. Needs War Sails.";
        private const string HorseBanditsHint = "{=ftr_mcm_horse_bandits_hint}Chance a new HorseBandits troop is female (pillager/ransacker lines, Chief, Boss). Default 25. Off or 0 keeps them male. Only shown when HorseBandits is enabled.";
        private const string PoachersHint = "{=ftr_mcm_poachers_hint}Chance a new Poacher in the Army of Poachers quest is female. Default 25. Off or 0 keeps them male.";
        private const string CaravanHint = "{=ftr_mcm_caravan_hint}Chance a new Armed Trader or Caravan Guard is female. Also covers Smugglers parties and War Sails naval/convoy caravans. Default 25. Nord/naval need War Sails.";
        private const string ConspiracyHint = "{=ftr_mcm_conspiracy_hint}Chance a new troop in these Story Mode conspiracy parties is female. Default 25. Off or 0 keeps them male.";
        private const string TroopsRootHint = "{=ftr_mcm_root_troops_hint}Kingdom recruits, militia, factions, mercenaries, gangsters, marines, caravans, and conspiracy. Off disables every Troops group.";
        private const string BanditsRootHint = "{=ftr_mcm_root_bandits_hint}Looters, sea/mountain/forest/desert/steppe bandits, corsairs, Army of Poachers, and HorseBandits when that mod is enabled. Off disables every Bandits group.";
        internal const string HorseBanditsGroupMarker = "ftr_mcm_horse_bandits";
        private const string HorseBanditsGroupName = "{=ftr_mcm_horse_bandits}HorseBandits (Mod Support)";
        private const string PercentName = "{=ftr_mcm_percent_name}Female troops percent";
        private const string PercentHint = "{=ftr_mcm_percent_hint}0 to 100. Default 25. 0 keeps male troops; 100 makes every new troop of this group female.";

        private readonly Dictionary<string, bool> _enabled = new Dictionary<string, bool>();
        private readonly Dictionary<string, int> _percent = new Dictionary<string, int>();
        private bool _troopsSection = true;
        private bool _banditsSection = true;

        public override string Id { get { return "FemaleTroopsAndBandits"; } }
        public override string DisplayName { get { return new TextObject("{=ftr_mcm_title}Female Troops and Bandits").ToString(); } }
        public override string FolderName { get { return "FemaleTroopsAndBandits"; } }
        public override string FormatType { get { return "json2"; } }

        public FemaleTroopsSettings()
        {
            Enable(_enabled, _percent, GroupEmpire);
            Enable(_enabled, _percent, GroupAserai);
            Enable(_enabled, _percent, GroupBattania);
            Enable(_enabled, _percent, GroupKhuzait);
            Enable(_enabled, _percent, GroupSturgia);
            Enable(_enabled, _percent, GroupVlandia);
            Enable(_enabled, _percent, GroupNord);
            Enable(_enabled, _percent, GroupMilitiaEmpire);
            Enable(_enabled, _percent, GroupMilitiaAserai);
            Enable(_enabled, _percent, GroupMilitiaBattania);
            Enable(_enabled, _percent, GroupMilitiaKhuzait);
            Enable(_enabled, _percent, GroupMilitiaSturgia);
            Enable(_enabled, _percent, GroupMilitiaVlandia);
            Enable(_enabled, _percent, GroupMilitiaNord);
            Enable(_enabled, _percent, GroupGhilman);
            Enable(_enabled, _percent, GroupLegion);
            Enable(_enabled, _percent, GroupSkolderbroda);
            Enable(_enabled, _percent, GroupBoar);
            Enable(_enabled, _percent, GroupBeniZilal);
            Enable(_enabled, _percent, GroupWolfskins);
            Enable(_enabled, _percent, GroupHiddenHand);
            Enable(_enabled, _percent, GroupLakeRats);
            Enable(_enabled, _percent, GroupBrotherhood);
            Enable(_enabled, _percent, GroupEmbers);
            Enable(_enabled, _percent, GroupJawwal);
            Enable(_enabled, _percent, GroupKarakhergit);
            Enable(_enabled, _percent, GroupForestPeople);
            Enable(_enabled, _percent, GroupEleftheroi);
            Enable(_enabled, _percent, GroupMercenaries);
            Enable(_enabled, _percent, GroupGangsters);
            Enable(_enabled, _percent, GroupMarineEmpire);
            Enable(_enabled, _percent, GroupMarineSturgia);
            Enable(_enabled, _percent, GroupMarineAserai);
            Enable(_enabled, _percent, GroupMarineBattania);
            Enable(_enabled, _percent, GroupMarineVlandia);
            Enable(_enabled, _percent, GroupLooters);
            Enable(_enabled, _percent, GroupSeaRaiders);
            Enable(_enabled, _percent, GroupMountain);
            Enable(_enabled, _percent, GroupForest);
            Enable(_enabled, _percent, GroupDesert);
            Enable(_enabled, _percent, GroupSteppe);
            Enable(_enabled, _percent, GroupCorsairs);
            Enable(_enabled, _percent, GroupHorseBandits);
            Enable(_enabled, _percent, GroupPoachers);
            Enable(_enabled, _percent, GroupCaravan);
            Enable(_enabled, _percent, GroupConspiracyAnti);
            Enable(_enabled, _percent, GroupConspiracyImperial);
        }

        internal static bool RollFemale(string groupId)
        {
            int percent = PercentFor(groupId);
            if (percent <= 0)
            {
                return false;
            }

            if (percent >= 100)
            {
                return true;
            }

            return MBRandom.RandomFloat * 100f < percent;
        }

        internal static int PercentFor(string groupId)
        {
            if (groupId == GroupHorseBandits && !FemaleTroopBranches.HorseBanditsSupportAvailable())
            {
                return 0;
            }

            FemaleTroopsSettings settings = Instance;
            if (settings == null)
            {
                return DefaultPercent;
            }

            if (!settings.SectionAllows(groupId))
            {
                return 0;
            }

            if (!settings.Enabled(groupId))
            {
                return 0;
            }

            return settings.ClampedPercent(groupId);
        }

        private bool SectionAllows(string groupId)
        {
            if (IsBanditGroup(groupId))
            {
                return _banditsSection;
            }

            return _troopsSection;
        }

        private static bool IsBanditGroup(string groupId)
        {
            return groupId == GroupLooters
                || groupId == GroupSeaRaiders
                || groupId == GroupMountain
                || groupId == GroupForest
                || groupId == GroupDesert
                || groupId == GroupSteppe
                || groupId == GroupCorsairs
                || groupId == GroupPoachers
                || groupId == GroupHorseBandits;
        }

        internal bool GroupOn(string groupId)
        {
            if (groupId == GroupHorseBandits && !FemaleTroopBranches.HorseBanditsSupportAvailable())
            {
                return false;
            }

            return SectionAllows(groupId) && Enabled(groupId) && ClampedPercent(groupId) > 0;
        }

        private static void Enable(Dictionary<string, bool> enabled, Dictionary<string, int> percent, string groupId)
        {
            enabled[groupId] = true;
            percent[groupId] = DefaultPercent;
        }

        private bool Enabled(string groupId)
        {
            bool value;
            if (_enabled.TryGetValue(groupId, out value))
            {
                return value;
            }

            return true;
        }

        private int ClampedPercent(string groupId)
        {
            int value;
            if (!_percent.TryGetValue(groupId, out value))
            {
                return DefaultPercent;
            }

            if (value < 0)
            {
                return 0;
            }

            if (value > 100)
            {
                return 100;
            }

            return value;
        }

        private void SetEnabled(string groupId, bool value)
        {
            _enabled[groupId] = value;
            FemaleTroopBranches.Apply(this);
        }

        private void SetPercent(string groupId, int value)
        {
            _percent[groupId] = value;
            FemaleTroopBranches.Apply(this);
        }

        [SettingPropertyBool(RootTroops, IsToggle = true, Order = 0, RequireRestart = false, HintText = TroopsRootHint)]
        [SettingPropertyGroup(RootTroops, GroupOrder = 0)]
        public bool TroopsSection
        {
            get { return _troopsSection; }
            set
            {
                _troopsSection = value;
                FemaleTroopBranches.Apply(this);
            }
        }

        [SettingPropertyBool(RootBandits, IsToggle = true, Order = 0, RequireRestart = false, HintText = BanditsRootHint)]
        [SettingPropertyGroup(RootBandits, GroupOrder = 1)]
        public bool BanditsSection
        {
            get { return _banditsSection; }
            set
            {
                _banditsSection = value;
                FemaleTroopBranches.Apply(this);
            }
        }

        [SettingPropertyBool(EmpireName, IsToggle = true, Order = 0, RequireRestart = false, HintText = RecruitHint)]
        [SettingPropertyGroup(Kingdom + "/" + EmpireName, GroupOrder = 0)]
        public bool Empire
        {
            get { return Enabled(GroupEmpire); }
            set { SetEnabled(GroupEmpire, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Kingdom + "/" + EmpireName)]
        public int EmpirePercent
        {
            get { return ClampedPercent(GroupEmpire); }
            set { SetPercent(GroupEmpire, value); }
        }

        [SettingPropertyBool(AseraiName, IsToggle = true, Order = 0, RequireRestart = false, HintText = RecruitBranchHint)]
        [SettingPropertyGroup(Kingdom + "/" + AseraiName, GroupOrder = 1)]
        public bool Aserai
        {
            get { return Enabled(GroupAserai); }
            set { SetEnabled(GroupAserai, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Kingdom + "/" + AseraiName)]
        public int AseraiPercent
        {
            get { return ClampedPercent(GroupAserai); }
            set { SetPercent(GroupAserai, value); }
        }

        [SettingPropertyBool(BattaniaName, IsToggle = true, Order = 0, RequireRestart = false, HintText = RecruitHint)]
        [SettingPropertyGroup(Kingdom + "/" + BattaniaName, GroupOrder = 2)]
        public bool Battania
        {
            get { return Enabled(GroupBattania); }
            set { SetEnabled(GroupBattania, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Kingdom + "/" + BattaniaName)]
        public int BattaniaPercent
        {
            get { return ClampedPercent(GroupBattania); }
            set { SetPercent(GroupBattania, value); }
        }

        [SettingPropertyBool(KhuzaitName, IsToggle = true, Order = 0, RequireRestart = false, HintText = RecruitBranchHint)]
        [SettingPropertyGroup(Kingdom + "/" + KhuzaitName, GroupOrder = 3)]
        public bool Khuzait
        {
            get { return Enabled(GroupKhuzait); }
            set { SetEnabled(GroupKhuzait, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Kingdom + "/" + KhuzaitName)]
        public int KhuzaitPercent
        {
            get { return ClampedPercent(GroupKhuzait); }
            set { SetPercent(GroupKhuzait, value); }
        }

        [SettingPropertyBool(SturgiaName, IsToggle = true, Order = 0, RequireRestart = false, HintText = RecruitHint)]
        [SettingPropertyGroup(Kingdom + "/" + SturgiaName, GroupOrder = 4)]
        public bool Sturgia
        {
            get { return Enabled(GroupSturgia); }
            set { SetEnabled(GroupSturgia, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Kingdom + "/" + SturgiaName)]
        public int SturgiaPercent
        {
            get { return ClampedPercent(GroupSturgia); }
            set { SetPercent(GroupSturgia, value); }
        }

        [SettingPropertyBool(VlandiaName, IsToggle = true, Order = 0, RequireRestart = false, HintText = RecruitHint)]
        [SettingPropertyGroup(Kingdom + "/" + VlandiaName, GroupOrder = 5)]
        public bool Vlandia
        {
            get { return Enabled(GroupVlandia); }
            set { SetEnabled(GroupVlandia, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Kingdom + "/" + VlandiaName)]
        public int VlandiaPercent
        {
            get { return ClampedPercent(GroupVlandia); }
            set { SetPercent(GroupVlandia, value); }
        }

        [SettingPropertyBool(NordName, IsToggle = true, Order = 0, RequireRestart = false, HintText = RecruitHint)]
        [SettingPropertyGroup(Kingdom + "/" + NordName, GroupOrder = 6)]
        public bool Nord
        {
            get { return Enabled(GroupNord); }
            set { SetEnabled(GroupNord, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Kingdom + "/" + NordName)]
        public int NordPercent
        {
            get { return ClampedPercent(GroupNord); }
            set { SetPercent(GroupNord, value); }
        }

        [SettingPropertyBool(EmpireName, IsToggle = true, Order = 0, RequireRestart = false, HintText = MilitiaHint)]
        [SettingPropertyGroup(Militia + "/" + EmpireName, GroupOrder = 0)]
        public bool MilitiaEmpire
        {
            get { return Enabled(GroupMilitiaEmpire); }
            set { SetEnabled(GroupMilitiaEmpire, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Militia + "/" + EmpireName)]
        public int MilitiaEmpirePercent
        {
            get { return ClampedPercent(GroupMilitiaEmpire); }
            set { SetPercent(GroupMilitiaEmpire, value); }
        }

        [SettingPropertyBool(AseraiName, IsToggle = true, Order = 0, RequireRestart = false, HintText = MilitiaHint)]
        [SettingPropertyGroup(Militia + "/" + AseraiName, GroupOrder = 1)]
        public bool MilitiaAserai
        {
            get { return Enabled(GroupMilitiaAserai); }
            set { SetEnabled(GroupMilitiaAserai, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Militia + "/" + AseraiName)]
        public int MilitiaAseraiPercent
        {
            get { return ClampedPercent(GroupMilitiaAserai); }
            set { SetPercent(GroupMilitiaAserai, value); }
        }

        [SettingPropertyBool(BattaniaName, IsToggle = true, Order = 0, RequireRestart = false, HintText = MilitiaHint)]
        [SettingPropertyGroup(Militia + "/" + BattaniaName, GroupOrder = 2)]
        public bool MilitiaBattania
        {
            get { return Enabled(GroupMilitiaBattania); }
            set { SetEnabled(GroupMilitiaBattania, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Militia + "/" + BattaniaName)]
        public int MilitiaBattaniaPercent
        {
            get { return ClampedPercent(GroupMilitiaBattania); }
            set { SetPercent(GroupMilitiaBattania, value); }
        }

        [SettingPropertyBool(KhuzaitName, IsToggle = true, Order = 0, RequireRestart = false, HintText = MilitiaHint)]
        [SettingPropertyGroup(Militia + "/" + KhuzaitName, GroupOrder = 3)]
        public bool MilitiaKhuzait
        {
            get { return Enabled(GroupMilitiaKhuzait); }
            set { SetEnabled(GroupMilitiaKhuzait, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Militia + "/" + KhuzaitName)]
        public int MilitiaKhuzaitPercent
        {
            get { return ClampedPercent(GroupMilitiaKhuzait); }
            set { SetPercent(GroupMilitiaKhuzait, value); }
        }

        [SettingPropertyBool(SturgiaName, IsToggle = true, Order = 0, RequireRestart = false, HintText = MilitiaHint)]
        [SettingPropertyGroup(Militia + "/" + SturgiaName, GroupOrder = 4)]
        public bool MilitiaSturgia
        {
            get { return Enabled(GroupMilitiaSturgia); }
            set { SetEnabled(GroupMilitiaSturgia, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Militia + "/" + SturgiaName)]
        public int MilitiaSturgiaPercent
        {
            get { return ClampedPercent(GroupMilitiaSturgia); }
            set { SetPercent(GroupMilitiaSturgia, value); }
        }

        [SettingPropertyBool(VlandiaName, IsToggle = true, Order = 0, RequireRestart = false, HintText = MilitiaHint)]
        [SettingPropertyGroup(Militia + "/" + VlandiaName, GroupOrder = 5)]
        public bool MilitiaVlandia
        {
            get { return Enabled(GroupMilitiaVlandia); }
            set { SetEnabled(GroupMilitiaVlandia, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Militia + "/" + VlandiaName)]
        public int MilitiaVlandiaPercent
        {
            get { return ClampedPercent(GroupMilitiaVlandia); }
            set { SetPercent(GroupMilitiaVlandia, value); }
        }

        [SettingPropertyBool(NordName, IsToggle = true, Order = 0, RequireRestart = false, HintText = MilitiaHint)]
        [SettingPropertyGroup(Militia + "/" + NordName, GroupOrder = 6)]
        public bool MilitiaNord
        {
            get { return Enabled(GroupMilitiaNord); }
            set { SetEnabled(GroupMilitiaNord, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Militia + "/" + NordName)]
        public int MilitiaNordPercent
        {
            get { return ClampedPercent(GroupMilitiaNord); }
            set { SetPercent(GroupMilitiaNord, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_ghilman}Ghilman", IsToggle = true, Order = 0, RequireRestart = false, HintText = FactionHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_ghilman}Ghilman", GroupOrder = 0)]
        public bool Ghilman
        {
            get { return Enabled(GroupGhilman); }
            set { SetEnabled(GroupGhilman, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_ghilman}Ghilman")]
        public int GhilmanPercent
        {
            get { return ClampedPercent(GroupGhilman); }
            set { SetPercent(GroupGhilman, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_legion}Legion of the Betrayed", IsToggle = true, Order = 0, RequireRestart = false, HintText = FactionHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_legion}Legion of the Betrayed", GroupOrder = 1)]
        public bool LegionOfTheBetrayed
        {
            get { return Enabled(GroupLegion); }
            set { SetEnabled(GroupLegion, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_legion}Legion of the Betrayed")]
        public int LegionOfTheBetrayedPercent
        {
            get { return ClampedPercent(GroupLegion); }
            set { SetPercent(GroupLegion, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_skolderbroda}Skolderbroda", IsToggle = true, Order = 0, RequireRestart = false, HintText = FactionHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_skolderbroda}Skolderbroda", GroupOrder = 2)]
        public bool Skolderbroda
        {
            get { return Enabled(GroupSkolderbroda); }
            set { SetEnabled(GroupSkolderbroda, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_skolderbroda}Skolderbroda")]
        public int SkolderbrodaPercent
        {
            get { return ClampedPercent(GroupSkolderbroda); }
            set { SetPercent(GroupSkolderbroda, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_boar}Company of the Golden Boar", IsToggle = true, Order = 0, RequireRestart = false, HintText = FactionHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_boar}Company of the Golden Boar", GroupOrder = 3)]
        public bool CompanyOfTheGoldenBoar
        {
            get { return Enabled(GroupBoar); }
            set { SetEnabled(GroupBoar, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_boar}Company of the Golden Boar")]
        public int CompanyOfTheGoldenBoarPercent
        {
            get { return ClampedPercent(GroupBoar); }
            set { SetPercent(GroupBoar, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_beni_zilal}Beni Zilal", IsToggle = true, Order = 0, RequireRestart = false, HintText = FactionHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_beni_zilal}Beni Zilal", GroupOrder = 4)]
        public bool BeniZilal
        {
            get { return Enabled(GroupBeniZilal); }
            set { SetEnabled(GroupBeniZilal, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_beni_zilal}Beni Zilal")]
        public int BeniZilalPercent
        {
            get { return ClampedPercent(GroupBeniZilal); }
            set { SetPercent(GroupBeniZilal, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_wolfskins}Wolfskins", IsToggle = true, Order = 0, RequireRestart = false, HintText = FactionHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_wolfskins}Wolfskins", GroupOrder = 5)]
        public bool Wolfskins
        {
            get { return Enabled(GroupWolfskins); }
            set { SetEnabled(GroupWolfskins, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_wolfskins}Wolfskins")]
        public int WolfskinsPercent
        {
            get { return ClampedPercent(GroupWolfskins); }
            set { SetPercent(GroupWolfskins, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_hidden_hand}Hidden Hand", IsToggle = true, Order = 0, RequireRestart = false, HintText = FactionHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_hidden_hand}Hidden Hand", GroupOrder = 6)]
        public bool HiddenHand
        {
            get { return Enabled(GroupHiddenHand); }
            set { SetEnabled(GroupHiddenHand, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_hidden_hand}Hidden Hand")]
        public int HiddenHandPercent
        {
            get { return ClampedPercent(GroupHiddenHand); }
            set { SetPercent(GroupHiddenHand, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_lake_rats}Lake Rats", IsToggle = true, Order = 0, RequireRestart = false, HintText = FactionHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_lake_rats}Lake Rats", GroupOrder = 7)]
        public bool LakeRats
        {
            get { return Enabled(GroupLakeRats); }
            set { SetEnabled(GroupLakeRats, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_lake_rats}Lake Rats")]
        public int LakeRatsPercent
        {
            get { return ClampedPercent(GroupLakeRats); }
            set { SetPercent(GroupLakeRats, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_brotherhood}Brotherhood of the Woods", IsToggle = true, Order = 0, RequireRestart = false, HintText = FactionHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_brotherhood}Brotherhood of the Woods", GroupOrder = 8)]
        public bool BrotherhoodOfTheWoods
        {
            get { return Enabled(GroupBrotherhood); }
            set { SetEnabled(GroupBrotherhood, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_brotherhood}Brotherhood of the Woods")]
        public int BrotherhoodOfTheWoodsPercent
        {
            get { return ClampedPercent(GroupBrotherhood); }
            set { SetPercent(GroupBrotherhood, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_embers}Embers of the Flame", IsToggle = true, Order = 0, RequireRestart = false, HintText = FactionHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_embers}Embers of the Flame", GroupOrder = 9)]
        public bool EmbersOfTheFlame
        {
            get { return Enabled(GroupEmbers); }
            set { SetEnabled(GroupEmbers, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_embers}Embers of the Flame")]
        public int EmbersOfTheFlamePercent
        {
            get { return ClampedPercent(GroupEmbers); }
            set { SetPercent(GroupEmbers, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_jawwal}Jawwal", IsToggle = true, Order = 0, RequireRestart = false, HintText = FactionHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_jawwal}Jawwal", GroupOrder = 10)]
        public bool Jawwal
        {
            get { return Enabled(GroupJawwal); }
            set { SetEnabled(GroupJawwal, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_jawwal}Jawwal")]
        public int JawwalPercent
        {
            get { return ClampedPercent(GroupJawwal); }
            set { SetPercent(GroupJawwal, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_karakhergit}Karakhergit", IsToggle = true, Order = 0, RequireRestart = false, HintText = FactionHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_karakhergit}Karakhergit", GroupOrder = 11)]
        public bool Karakhergit
        {
            get { return Enabled(GroupKarakhergit); }
            set { SetEnabled(GroupKarakhergit, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_karakhergit}Karakhergit")]
        public int KarakhergitPercent
        {
            get { return ClampedPercent(GroupKarakhergit); }
            set { SetPercent(GroupKarakhergit, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_forest_people}Forest People", IsToggle = true, Order = 0, RequireRestart = false, HintText = FactionHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_forest_people}Forest People", GroupOrder = 12)]
        public bool ForestPeople
        {
            get { return Enabled(GroupForestPeople); }
            set { SetEnabled(GroupForestPeople, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_forest_people}Forest People")]
        public int ForestPeoplePercent
        {
            get { return ClampedPercent(GroupForestPeople); }
            set { SetPercent(GroupForestPeople, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_eleftheroi}Eleftheroi", IsToggle = true, Order = 0, RequireRestart = false, HintText = FactionHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_eleftheroi}Eleftheroi", GroupOrder = 13)]
        public bool Eleftheroi
        {
            get { return Enabled(GroupEleftheroi); }
            set { SetEnabled(GroupEleftheroi, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Factions + "/{=ftr_mcm_eleftheroi}Eleftheroi")]
        public int EleftheroiPercent
        {
            get { return ClampedPercent(GroupEleftheroi); }
            set { SetPercent(GroupEleftheroi, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_mercenaries}Mercenaries", IsToggle = true, Order = 0, RequireRestart = false, HintText = MercHint)]
        [SettingPropertyGroup(MercenariesGroup, GroupOrder = 3)]
        public bool Mercenaries
        {
            get { return Enabled(GroupMercenaries); }
            set { SetEnabled(GroupMercenaries, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(MercenariesGroup)]
        public int MercenariesPercent
        {
            get { return ClampedPercent(GroupMercenaries); }
            set { SetPercent(GroupMercenaries, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_gangsters}Gangsters", IsToggle = true, Order = 0, RequireRestart = false, HintText = GangHint)]
        [SettingPropertyGroup(GangstersGroup, GroupOrder = 4)]
        public bool Gangsters
        {
            get { return Enabled(GroupGangsters); }
            set { SetEnabled(GroupGangsters, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(GangstersGroup)]
        public int GangstersPercent
        {
            get { return ClampedPercent(GroupGangsters); }
            set { SetPercent(GroupGangsters, value); }
        }

        [SettingPropertyBool(EmpireName, IsToggle = true, Order = 0, RequireRestart = false, HintText = MarineHint)]
        [SettingPropertyGroup(Marines + "/" + EmpireName, GroupOrder = 0)]
        public bool MarineEmpire
        {
            get { return Enabled(GroupMarineEmpire); }
            set { SetEnabled(GroupMarineEmpire, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Marines + "/" + EmpireName)]
        public int MarineEmpirePercent
        {
            get { return ClampedPercent(GroupMarineEmpire); }
            set { SetPercent(GroupMarineEmpire, value); }
        }

        [SettingPropertyBool(SturgiaName, IsToggle = true, Order = 0, RequireRestart = false, HintText = MarineHint)]
        [SettingPropertyGroup(Marines + "/" + SturgiaName, GroupOrder = 1)]
        public bool MarineSturgia
        {
            get { return Enabled(GroupMarineSturgia); }
            set { SetEnabled(GroupMarineSturgia, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Marines + "/" + SturgiaName)]
        public int MarineSturgiaPercent
        {
            get { return ClampedPercent(GroupMarineSturgia); }
            set { SetPercent(GroupMarineSturgia, value); }
        }

        [SettingPropertyBool(AseraiName, IsToggle = true, Order = 0, RequireRestart = false, HintText = MarineHint)]
        [SettingPropertyGroup(Marines + "/" + AseraiName, GroupOrder = 2)]
        public bool MarineAserai
        {
            get { return Enabled(GroupMarineAserai); }
            set { SetEnabled(GroupMarineAserai, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Marines + "/" + AseraiName)]
        public int MarineAseraiPercent
        {
            get { return ClampedPercent(GroupMarineAserai); }
            set { SetPercent(GroupMarineAserai, value); }
        }

        [SettingPropertyBool(BattaniaName, IsToggle = true, Order = 0, RequireRestart = false, HintText = MarineHint)]
        [SettingPropertyGroup(Marines + "/" + BattaniaName, GroupOrder = 3)]
        public bool MarineBattania
        {
            get { return Enabled(GroupMarineBattania); }
            set { SetEnabled(GroupMarineBattania, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Marines + "/" + BattaniaName)]
        public int MarineBattaniaPercent
        {
            get { return ClampedPercent(GroupMarineBattania); }
            set { SetPercent(GroupMarineBattania, value); }
        }

        [SettingPropertyBool(VlandiaName, IsToggle = true, Order = 0, RequireRestart = false, HintText = MarineHint)]
        [SettingPropertyGroup(Marines + "/" + VlandiaName, GroupOrder = 4)]
        public bool MarineVlandia
        {
            get { return Enabled(GroupMarineVlandia); }
            set { SetEnabled(GroupMarineVlandia, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Marines + "/" + VlandiaName)]
        public int MarineVlandiaPercent
        {
            get { return ClampedPercent(GroupMarineVlandia); }
            set { SetPercent(GroupMarineVlandia, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_looters}Looters", IsToggle = true, Order = 0, RequireRestart = false, HintText = LootersHint)]
        [SettingPropertyGroup(Bandits + "/{=ftr_mcm_looters}Looters", GroupOrder = 0)]
        public bool Looters
        {
            get { return Enabled(GroupLooters); }
            set { SetEnabled(GroupLooters, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Bandits + "/{=ftr_mcm_looters}Looters")]
        public int LootersPercent
        {
            get { return ClampedPercent(GroupLooters); }
            set { SetPercent(GroupLooters, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_sea_raiders}Sea Raiders", IsToggle = true, Order = 0, RequireRestart = false, HintText = SeaRaidersHint)]
        [SettingPropertyGroup(Bandits + "/{=ftr_mcm_sea_raiders}Sea Raiders", GroupOrder = 1)]
        public bool SeaRaiders
        {
            get { return Enabled(GroupSeaRaiders); }
            set { SetEnabled(GroupSeaRaiders, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Bandits + "/{=ftr_mcm_sea_raiders}Sea Raiders")]
        public int SeaRaidersPercent
        {
            get { return ClampedPercent(GroupSeaRaiders); }
            set { SetPercent(GroupSeaRaiders, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_mountain}Mountain Bandits", IsToggle = true, Order = 0, RequireRestart = false, HintText = BanditHint)]
        [SettingPropertyGroup(Bandits + "/{=ftr_mcm_mountain}Mountain Bandits", GroupOrder = 2)]
        public bool MountainBandits
        {
            get { return Enabled(GroupMountain); }
            set { SetEnabled(GroupMountain, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Bandits + "/{=ftr_mcm_mountain}Mountain Bandits")]
        public int MountainBanditsPercent
        {
            get { return ClampedPercent(GroupMountain); }
            set { SetPercent(GroupMountain, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_forest_bandits}Forest Bandits", IsToggle = true, Order = 0, RequireRestart = false, HintText = BanditHint)]
        [SettingPropertyGroup(Bandits + "/{=ftr_mcm_forest_bandits}Forest Bandits", GroupOrder = 3)]
        public bool ForestBandits
        {
            get { return Enabled(GroupForest); }
            set { SetEnabled(GroupForest, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Bandits + "/{=ftr_mcm_forest_bandits}Forest Bandits")]
        public int ForestBanditsPercent
        {
            get { return ClampedPercent(GroupForest); }
            set { SetPercent(GroupForest, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_desert_bandits}Desert Bandits", IsToggle = true, Order = 0, RequireRestart = false, HintText = BanditHint)]
        [SettingPropertyGroup(Bandits + "/{=ftr_mcm_desert_bandits}Desert Bandits", GroupOrder = 4)]
        public bool DesertBandits
        {
            get { return Enabled(GroupDesert); }
            set { SetEnabled(GroupDesert, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Bandits + "/{=ftr_mcm_desert_bandits}Desert Bandits")]
        public int DesertBanditsPercent
        {
            get { return ClampedPercent(GroupDesert); }
            set { SetPercent(GroupDesert, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_steppe_bandits}Steppe Bandits", IsToggle = true, Order = 0, RequireRestart = false, HintText = BanditHint)]
        [SettingPropertyGroup(Bandits + "/{=ftr_mcm_steppe_bandits}Steppe Bandits", GroupOrder = 5)]
        public bool SteppeBandits
        {
            get { return Enabled(GroupSteppe); }
            set { SetEnabled(GroupSteppe, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Bandits + "/{=ftr_mcm_steppe_bandits}Steppe Bandits")]
        public int SteppeBanditsPercent
        {
            get { return ClampedPercent(GroupSteppe); }
            set { SetPercent(GroupSteppe, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_corsairs}Corsairs", IsToggle = true, Order = 0, RequireRestart = false, HintText = CorsairHint)]
        [SettingPropertyGroup(Bandits + "/{=ftr_mcm_corsairs}Corsairs", GroupOrder = 6)]
        public bool Corsairs
        {
            get { return Enabled(GroupCorsairs); }
            set { SetEnabled(GroupCorsairs, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Bandits + "/{=ftr_mcm_corsairs}Corsairs")]
        public int CorsairsPercent
        {
            get { return ClampedPercent(GroupCorsairs); }
            set { SetPercent(GroupCorsairs, value); }
        }

        [SettingPropertyBool(HorseBanditsGroupName, IsToggle = true, Order = 0, RequireRestart = false, HintText = HorseBanditsHint)]
        [SettingPropertyGroup(Bandits + "/" + HorseBanditsGroupName, GroupOrder = 8)]
        public bool HorseBandits
        {
            get { return Enabled(GroupHorseBandits); }
            set { SetEnabled(GroupHorseBandits, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Bandits + "/" + HorseBanditsGroupName)]
        public int HorseBanditsPercent
        {
            get { return ClampedPercent(GroupHorseBandits); }
            set { SetPercent(GroupHorseBandits, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_caravan}Caravan Troops", IsToggle = true, Order = 0, RequireRestart = false, HintText = CaravanHint)]
        [SettingPropertyGroup(CaravanGroup, GroupOrder = 6)]
        public bool CaravanTroops
        {
            get { return Enabled(GroupCaravan); }
            set { SetEnabled(GroupCaravan, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(CaravanGroup)]
        public int CaravanTroopsPercent
        {
            get { return ClampedPercent(GroupCaravan); }
            set { SetPercent(GroupCaravan, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_poachers}Army of Poachers", IsToggle = true, Order = 0, RequireRestart = false, HintText = PoachersHint)]
        [SettingPropertyGroup(Bandits + "/{=ftr_mcm_poachers}Army of Poachers", GroupOrder = 7)]
        public bool ArmyOfPoachers
        {
            get { return Enabled(GroupPoachers); }
            set { SetEnabled(GroupPoachers, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Bandits + "/{=ftr_mcm_poachers}Army of Poachers")]
        public int ArmyOfPoachersPercent
        {
            get { return ClampedPercent(GroupPoachers); }
            set { SetPercent(GroupPoachers, value); }
        }

        // Flat under Troops with GroupOrder 7 so Conspiracy sits after culture/caravan groups
        // (nested Conspiracy/Anti-Empire had no mid-level order and sorted to the top).
        [SettingPropertyBool("{=ftr_mcm_conspiracy_anti}Anti-Empire", IsToggle = true, Order = 0, RequireRestart = false, HintText = ConspiracyHint)]
        [SettingPropertyGroup(Conspiracy, GroupOrder = 7)]
        public bool ConspiracyAntiEmpire
        {
            get { return Enabled(GroupConspiracyAnti); }
            set { SetEnabled(GroupConspiracyAnti, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 1, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Conspiracy)]
        public int ConspiracyAntiEmpirePercent
        {
            get { return ClampedPercent(GroupConspiracyAnti); }
            set { SetPercent(GroupConspiracyAnti, value); }
        }

        [SettingPropertyBool("{=ftr_mcm_conspiracy_imperial}Imperial", IsToggle = true, Order = 2, RequireRestart = false, HintText = ConspiracyHint)]
        [SettingPropertyGroup(Conspiracy)]
        public bool ConspiracyImperial
        {
            get { return Enabled(GroupConspiracyImperial); }
            set { SetEnabled(GroupConspiracyImperial, value); }
        }

        [SettingPropertyInteger(PercentName, 0, 100, "0", Order = 3, RequireRestart = false, HintText = PercentHint)]
        [SettingPropertyGroup(Conspiracy)]
        public int ConspiracyImperialPercent
        {
            get { return ClampedPercent(GroupConspiracyImperial); }
            set { SetPercent(GroupConspiracyImperial, value); }
        }
    }
}
