# Female Troops and Bandits

A Mount & Blade II: Bannerlord mod that adds independent female troop trees for kingdom recruits, militia, minor factions, mercenaries, gangsters, caravan troops, bandits, War Sails marines, and Story Mode conspiracy forces. Female troops spawn from MCM percentages and do not overwrite vanilla troops.

**Nexus Mod URL:** Coming soon

## About this mod

This is a direct continuation and upgrade of **[Female Troops Reborn V1.7.0](https://www.nexusmods.com/mountandblade2bannerlord/mods/3715)** by AzureKnight and Xacor. It keeps the original idea of female troops while adding independent IDs, full MCM control, bandits, War Sails Nord support, and optional integration with published companion mods.

### Key differences from the original

- **Independent female troops:** Separate `ftr_*` IDs. Vanilla troops stay unchanged.
- **Full MCM:** Per-group toggle and 0–100% slider. Default 25%. No XML editing.
- **MCM layout:** **Troops** first, then **Bandits**.
- **Female bandits:** Looters, Sea Raiders, Mountain, Forest, Desert, Steppe, War Sails Corsairs, and Army of Poachers.
- **Caravan / Smugglers:** Female Armed Traders and Caravan Guards. Vanilla Smugglers quest parties use caravan templates, so they follow **Caravan Troops**.
- **Horse Bandits:** Optional MCM **Horse Bandits (Mod Support)** when **[Horse Bandits](https://www.nexusmods.com/mountandblade2bannerlord/mods/14448)** is enabled. Hidden if that mod is not in the load order.
- **Camel Cavalry:** Female camel upgrades when **[Camel Cavalry](https://www.nexusmods.com/mountandblade2bannerlord/mods/14240)** is installed (via Aserai / Khuzait recruit switches).
- **Raise Your Banner:** Quiet auto-resolve fix for empty banner-bearer battle equipment when **[Raise Your Banner](https://www.nexusmods.com/mountandblade2bannerlord/mods/3253)** is loaded.
- **War Sails:** Female Nord recruits, militia, marines, and Corsairs.
- **Sword Sister T6:** Elite mercenary upgrades without changing vanilla T5 Sword Sisters.

## What this mod adds

- **Kingdom recruits:** Empire, Aserai, Battania, Khuzait, Sturgia, Vlandia, Nord (War Sails)
- **Militia:** All seven cultures
- **Minor factions:** Ghilman, Legion of the Betrayed, Skolderbroda, Company of the Golden Boar, Beni Zilal, Wolfskins, Hidden Hand, Lake Rats, Brotherhood of the Woods, Embers of the Flame, Jawwal, Karakhergit, Forest People, Eleftheroi
- **Mercenaries:** Eastern and Western sellswords, mounted lines, crossbows (Sword Sisters stay vanilla; T6 upgrades are extra)
- **Gangsters:** All three tiers
- **Caravan troops:** Armed Trader, Caravan Guard, Veteran Caravan Guard (all cultures, Nord with War Sails); also used by Smugglers quest parties
- **Bandits:** Looters, Sea Raiders, Mountain, Forest, Desert, Steppe, Corsairs, Army of Poachers
- **Horse Bandits line:** when that mod is loaded
- **War Sails marines:** Empire, Sturgia, Aserai, Battania, Vlandia
- **Conspiracy:** Anti-Empire and Imperial (Story Mode)

At 0% new troops of that group stay male. At 100% every new troop of that group is female. Default 25%. Troops already in parties stay as they are.

## How it works

**Recruits:** Town, village, and castle volunteers can roll the female root for that culture. Castle Market Towns castle recruits work with no CMT code changes.

**Party fills:** During spawn and template fill (bandits, militia, alleys, many quests, conspiracy, War Sails pirates, and similar), mapped male template troops may swap to female. Quest hand-overs that move a soldier you already chose do not re-roll that soldier.

**Bandits:** Female bandits appear in parties, hideouts, chiefs, and bosses when that line’s switch is on. Deserters follow Looters. Northern pirates follow Sea Raiders. With War Sails, female Sea Raiders use the Nord upgrade exit.

**Upgrades:** Aserai and Khuzait female recruits can gain female camel upgrades when Camel Cavalry is installed. War Sails female land troops can gain an extra marine upgrade. Those links follow the matching MCM switches.

**Encyclopedia:** Female lines show while their group is on and any required optional mod is present. Reopen the page after changing a switch.

**Sword Sisters:** Vanilla T5 unchanged. Two T6 upgrades are always available.

## Mod options (MCM)

**Options > Mod Options > Female Troops and Bandits**

| Section | Groups |
|---|---|
| **Troops → Kingdom Recruits** | Empire, Aserai, Battania, Khuzait, Sturgia, Vlandia, Nord |
| **Troops → Militia** | Same seven cultures |
| **Troops → Minor Factions** | 14 factions |
| **Troops → Mercenaries** | One group |
| **Troops → Gangsters** | One group |
| **Troops → War Sails Marines** | Empire, Sturgia, Aserai, Battania, Vlandia |
| **Troops → Caravan Troops** | Armed Traders / Caravan Guards (incl. Smugglers parties) |
| **Troops → Conspiracy** | Anti-Empire, Imperial |
| **Bandits** | Looters, Sea Raiders, Mountain, Forest, Desert, Steppe, Corsairs, Army of Poachers, Horse Bandits (Mod Support) |

**Horse Bandits (Mod Support):** Shown only when Horse Bandits is enabled. Default on at 25%. Independent of the vanilla bandit-line sliders (different troop IDs). Female Highwaywoman follows Horse Bandits’ champion branch.

**Camel Cavalry:** No separate page. Aserai and Khuzait recruit switches also control female camel upgrades when Camel Cavalry is installed.

## Requirements and versions

| Dependency | Tested version | Expected to work with |
|---|---|---|
| Mount & Blade II: Bannerlord | 1.4.8 | 1.4.x |
| [Harmony][harmony] | 2.4.2 | 2.x |
| [ButterLib][butterlib] | 2.12.0 | 2.x |
| [UIExtenderEx][uiextenderex] | 3.0.3 | 3.x |
| [Mod Configuration Menu v5 (MCM)][mcm] | 5.13.1 | 5.x |
| War Sails (NavalDLC), optional | 1.2.8 | 1.2.x |
| Story Mode, optional | 1.4.8 | 1.4.x |
| [Camel Cavalry][camelcavalry], optional | 1.0.0 | 1.x |
| [Horse Bandits][horsebandits], optional | 1.0.0 | 1.x |
| [Raise Your Banner][raiseyourbanner], optional | — | — |
| [Castle Market Towns][cmt], optional | — | — |

This mod requires [MCM][mcm] (and the libraries MCM needs). Optional mods disable only their own features when absent.

## Compatibility

- **Vanilla and War Sails:** Independent female trees; no vanilla overwrite.
- **Story Mode:** Conspiracy female troops follow MCM.
- **[Camel Cavalry][camelcavalry]:** Female camel upgrades via Aserai / Khuzait recruit switches.
- **[Horse Bandits][horsebandits]:** MCM under Bandits when loaded; hidden otherwise.
- **[Raise Your Banner][raiseyourbanner]:** Banner-bearer battle-equipment fill when loaded.
- **[Castle Market Towns][cmt]:** Castle volunteers use the same female recruit rolls.
- **Other female troop mods:** May overlap; this mod uses `ftr_*` IDs to reduce conflicts.

## Installation

1. Install [Harmony][harmony], [ButterLib][butterlib], [UIExtenderEx][uiextenderex], and [MCM][mcm].
2. Optionally install War Sails, Story Mode, [Camel Cavalry][camelcavalry], [Horse Bandits][horsebandits], [Raise Your Banner][raiseyourbanner], or [Castle Market Towns][cmt].
3. Extract `FemaleTroopsAndBandits` into `Mount & Blade II Bannerlord\Modules`.
4. Enable it after [MCM][mcm] and after any optional mods listed above.

Safe to add to an existing campaign.

## Removing the mod from a campaign

Unsafe if the save already contains this mod’s troops or items. Keep it enabled for that campaign, or remove it before those objects enter the save.

## Translations

English ships in-mod. MCM strings: `ModuleData/Languages/EN/ftr_strings_en.xml`. Troop and item names use `{=ftr_*}` ids in ModuleData XML (English fallbacks inline). Publish translations as a separate module that requires this one. See [TRANSLATORS.md](TRANSLATORS.md).

## Building from source

Source and XML only. Game, [Harmony][harmony], and [MCM][mcm] DLLs are referenced from your install. C# 5 / .NET Framework 4.7.2+.

Point at the game with `FemaleTroopsAndBandits.user.props` (from the `.example`), `BANNERLORD_DIR`, or `/p:BannerlordDir=...`. Default Steam path is used if unset; a project inside `<game>\Modules\FemaleTroopsAndBandits` finds that game automatically.

```bat
build.cmd
```

DLL output: `bin\Win64_Shipping_Client\FemaleTroopsAndBandits.dll`.

```bat
build.cmd /p:DeployToGame=true
```

copies `SubModule.xml`, `ModuleData`, and the DLL into `Modules\FemaleTroopsAndBandits`. Close the game first.

## Repository layout

```
FemaleTroopsAndBandits.csproj
FemaleTroopsAndBandits.user.props.example
build.cmd
SubModule.xml
ModuleData/          Troops, items, body properties, languages
src/                 C# source
CHANGELOG.md
TRANSLATORS.md
NOTICE
LICENSE
```

## Credits

- **AzureKnight and Xacor:** [Female Troops Reborn V1.7.0](https://www.nexusmods.com/mountandblade2bannerlord/mods/3715)
- **AtanuDebbarma:** [Camel Cavalry](https://www.nexusmods.com/mountandblade2bannerlord/mods/14240), [Horse Bandits](https://www.nexusmods.com/mountandblade2bannerlord/mods/14448), and this continuation
- **TaleWorlds Entertainment:** Mount & Blade II: Bannerlord

## License

Copyright (C) 2026 AtanuDebbarma.

Free software under the [GNU General Public License v3.0](LICENSE), with the attribution term in [NOTICE](NOTICE):

- **Using or modifying this code:** credit **Female Troops and Bandits by AtanuDebbarma** with a link to [this repository][repo], and release under GPL v3.
- **Inspiration only:** a credit and link are kindly requested.

Mount & Blade II: Bannerlord is a trademark of TaleWorlds Entertainment. This project is not affiliated with or endorsed by TaleWorlds.

[mcm]: https://www.nexusmods.com/mountandblade2bannerlord/mods/612
[butterlib]: https://www.nexusmods.com/mountandblade2bannerlord/mods/2018
[harmony]: https://www.nexusmods.com/mountandblade2bannerlord/mods/2006
[uiextenderex]: https://www.nexusmods.com/mountandblade2bannerlord/mods/2102
[camelcavalry]: https://www.nexusmods.com/mountandblade2bannerlord/mods/14240
[horsebandits]: https://www.nexusmods.com/mountandblade2bannerlord/mods/14448
[raiseyourbanner]: https://www.nexusmods.com/mountandblade2bannerlord/mods/3253
[cmt]: https://www.nexusmods.com/mountandblade2bannerlord/mods/14182
[repo]: https://github.com/AtanuDebbarma/FemaleTroopsAndBandits
