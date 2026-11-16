# Changelog

## 1.0.4

- **Female bandit dialogue VO muted.** When a female bandit (any bandit line / bandit or hideout party leader) speaks in a map conversation cutscene, voice audio is unlinked so the scene continues silently. Vanilla male bandit and all non-bandit voices are unchanged.
- **Optional troop packs verify before settling.** War Sails / Camel Cavalry / Horse Bandits XML load is no longer marked done on file-open alone. Sentinel female troops must resolve with Culture + IsReady; session/daily hooks retry until verified. Save repair waits for that settle so empty `ftr_*` shells are not cleared too early.
- **Civilian equipment schema.** Six minor-faction female troops used `EquipmentRoster equipmentType="Civilian"`; corrected to `EquipmentSet` (jawwal / karakhuzaits T2–T3).

## 1.0.3

- Targets Bannerlord **1.5.7** / War Sails **1.3.7**.
- **Optional troop packs** (War Sails Nord / marines / Corsairs / naval caravans, Camel Cavalry, Horse Bandits) live in separate ModuleData XML files and load only when that DLC/mod is active — not declared as always-on SubModule XmlNodes.
- **War Sails / optional packs actually load.** `OptionalTroopXmlLoader.TryLoadForCampaign` runs on game start (and again on session launch). Fixes empty `ftr_nord_*` / marine / corsair / naval-caravan shells when continuing a save with NavalDLC — the reported 1.5.7 + War Sails recruit crash.
- **Old-save rematerialize.** After optional XML loads, rewrite empty `ftr_*` volunteer slots and party/prison stacks to the live registered troop, or clear the shell if still missing. Resolve paths treat empty shells as not usable.

## 1.0.1

- Fixed MCM error when opening Mod Options: Conspiracy Anti-Empire and Imperial percent sliders no longer share one group path (duplicate `Female troops percent` key). Each conspiracy group is nested again under Troops → Conspiracy.

## 1.0.0

First release.

- Branch and encyclopedia wiring wait until character Culture is ready (session launch / daily tick / MCM). Avoids Crash Doctor false "troop files did not fully load" notices from touching UpgradeTargets during OnGameStart.

- Direct continuation and upgrade of Female Troops Reborn V1.7.0. Independent female trees (`ftr_*`). Vanilla troops are not overwritten.
- Full MCM under **Troops** then **Bandits**. Per-group toggle and 0–100% slider. Default 25%.
- Kingdom recruits, militia, minor factions, mercenaries (Sword Sisters stay vanilla), gangsters, caravan troops (including Smugglers quest parties and War Sails naval/convoy caravan troops), War Sails marines, bandits, Corsairs, Army of Poachers, and Story Mode conspiracy parties.
- War Sails naval caravan combat line: `ftr_naval_armed_trader_*`, `ftr_naval_caravan_guard_*`, `ftr_naval_veteran_caravan_guard_*` (7 cultures). Same **Caravan Troops** MCM group as land caravans.
- War Sails Nord female recruit and militia trees. Female Sea Raiders follow the Nord upgrade exit when War Sails is loaded.
- Optional [Camel Cavalry](https://www.nexusmods.com/mountandblade2bannerlord/mods/14240): female camel upgrades follow Aserai / Khuzait recruit switches.
- Optional [Horse Bandits](https://www.nexusmods.com/mountandblade2bannerlord/mods/14448): MCM **HorseBandits (Mod Support)** under Bandits (default 25%). Omitted from MCM when that mod is not enabled.
- MCM: Conspiracy (Anti-Empire / Imperial) sits at the bottom of Troops; long tooltips shortened.
- Optional [Raise Your Banner](https://www.nexusmods.com/mountandblade2bannerlord/mods/3253): banner-bearer battle-equipment fill for auto-resolve.
- Sword Sister Tier 6: Upgraded Veteran Sword Sister and Upgraded Gallant Sword Sister. Vanilla T5 unchanged.
- Encyclopedia hides female lines when their switch is off or a required optional mod is missing.
- Safe to add to an existing campaign. Unsafe to remove after its troops or items are in the save.
- Translations: MCM in `ModuleData/Languages/EN/ftr_strings_en.xml`; troop/item `{=ftr_*}` ids in ModuleData XML. See [TRANSLATORS.md](TRANSLATORS.md).
- MCM translation IDs in this version: `ftr_mcm_title`, `ftr_mcm_root_troops`, `ftr_mcm_root_bandits`, `ftr_mcm_root_troops_hint`, `ftr_mcm_root_bandits_hint`, `ftr_mcm_group_kingdom`, `ftr_mcm_group_militia`, `ftr_mcm_group_factions`, `ftr_mcm_group_marines`, `ftr_mcm_group_bandits`, `ftr_mcm_group_conspiracy`, `ftr_mcm_caravan`, `ftr_mcm_poachers`, `ftr_mcm_horse_bandits`, `ftr_mcm_empire`, `ftr_mcm_aserai`, `ftr_mcm_battania`, `ftr_mcm_khuzait`, `ftr_mcm_sturgia`, `ftr_mcm_vlandia`, `ftr_mcm_nord`, `ftr_mcm_ghilman`, `ftr_mcm_legion`, `ftr_mcm_skolderbroda`, `ftr_mcm_boar`, `ftr_mcm_beni_zilal`, `ftr_mcm_wolfskins`, `ftr_mcm_hidden_hand`, `ftr_mcm_lake_rats`, `ftr_mcm_brotherhood`, `ftr_mcm_embers`, `ftr_mcm_jawwal`, `ftr_mcm_karakhergit`, `ftr_mcm_forest_people`, `ftr_mcm_eleftheroi`, `ftr_mcm_mercenaries`, `ftr_mcm_gangsters`, `ftr_mcm_looters`, `ftr_mcm_sea_raiders`, `ftr_mcm_mountain`, `ftr_mcm_forest_bandits`, `ftr_mcm_desert_bandits`, `ftr_mcm_steppe_bandits`, `ftr_mcm_corsairs`, `ftr_mcm_conspiracy_anti`, `ftr_mcm_conspiracy_imperial`, `ftr_mcm_recruit_hint`, `ftr_mcm_recruit_branch_hint`, `ftr_mcm_militia_hint`, `ftr_mcm_faction_hint`, `ftr_mcm_merc_hint`, `ftr_mcm_gang_hint`, `ftr_mcm_marine_hint`, `ftr_mcm_bandit_hint`, `ftr_mcm_looters_hint`, `ftr_mcm_sea_raiders_hint`, `ftr_mcm_corsair_hint`, `ftr_mcm_poachers_hint`, `ftr_mcm_caravan_hint`, `ftr_mcm_horse_bandits_hint`, `ftr_mcm_conspiracy_hint`, `ftr_mcm_percent_name`, and `ftr_mcm_percent_hint`.
