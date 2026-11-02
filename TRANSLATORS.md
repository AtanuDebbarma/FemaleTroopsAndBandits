# Translating Female Troops and Bandits

Female Troops and Bandits ships in English only. Every piece of text the mod adds can be translated, and translations are welcome as separate mods that require Female Troops and Bandits. You do not need to change or copy any of its files.

## What can be translated

- **Mod Configuration Menu:** every group, setting name and hint is in `ModuleData/Languages/EN/ftr_strings_en.xml`. Use that file as your MCM template.
- **Troop and item names:** each has a `{=ftr_*}` id with an English fallback in the ModuleData character and item XML (`spfnpccharacters.xml`, `ftr_extra_troops.xml`, `ftr_bandit_troops.xml`, `ftr_quest_caravan_troops.xml`, `ftr_sword_sister_t6.xml`, `ftr_items.xml`, and related files). Put those same ids into your language file with translated `text` values.

Vanilla troop names the mod does not redefine stay as the base game's own text.

## Making a translation module

A translation module contains only text files. Example for Russian:

```
FemaleTroopsAndBandits_RU/
├─ SubModule.xml
└─ ModuleData/
   └─ Languages/
      └─ RU/
         ├─ language_data.xml
         └─ ftr_strings_ru.xml
```

`SubModule.xml`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<Module>
  <Name value="Female Troops and Bandits - Russian"/>
  <Id value="FemaleTroopsAndBandits_RU"/>
  <Version value="v1.0.0"/>
  <DefaultModule value="false"/>
  <ModuleCategory value="Singleplayer"/>
  <DependedModules>
    <DependedModule Id="FemaleTroopsAndBandits" DependentVersion="v1.0.0"/>
  </DependedModules>
  <DependedModuleMetadatas>
    <DependedModuleMetadata id="FemaleTroopsAndBandits" order="LoadBeforeThis" version="v1.0.0"/>
  </DependedModuleMetadatas>
</Module>
```

Give your module its own `Id`. Set `DependentVersion` and `version` to the Female Troops and Bandits version your file was made from.

`language_data.xml`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<LanguageData id="Русский">
  <LanguageFile xml_path="RU/ftr_strings_ru.xml" />
</LanguageData>
```

`ftr_strings_ru.xml` is a copy of `ftr_strings_en.xml` (plus any troop/item ids you add) with the language tag changed to the same name and every `text` value translated:

```xml
<tags>
  <tag language="Русский" />
</tags>
```

### Language names

The language name must match the game's exactly, including accents and capital letters:

| Language | Name to use |
|---|---|
| Brazilian Portuguese | `Português (BR)` |
| Chinese (Simplified) | `简体中文` |
| Chinese (Traditional) | `繁體中文` |
| French | `Français` |
| German | `Deutsch` |
| Italian | `Italiano` |
| Japanese | `日本語` |
| Korean | `한국어` |
| Latin American Spanish | `Español (LA)` |
| Polish | `Polski` |
| Russian | `Русский` |
| Turkish | `Türkçe` |

For a language the game does not list here, copy the name from the `language_data.xml` of a mod that adds that language to the game.

## Rules for the text

- **Keep every `id` exactly as it is.** Only change `text`.
- **Save the file as UTF-8.**
- **Escape XML characters** inside `text`: `&` as `&amp;`, `<` as `&lt;`, `>` as `&gt;` and `"` as `&quot;`.
- **Missing lines are fine.** Any ID left out of your file shows in English, so a partial translation still works.

## Testing

1. Enable your module after Female Troops and Bandits in the launcher and choose your language in the game's options.
2. Check the **Female Troops and Bandits** page in MCM under **Options > Mod Options**, and the encyclopedia names of the mod's own troops and items.
3. A line that still shows in English means its ID is missing or misspelled in your file. If the game log reports an XML error, check the escaping rules above.

The translation only changes text. It adds nothing to saves, so it can be added or removed at any time.

## Keeping up with updates

IDs are kept from one version to the next, so a translation keeps working after Female Troops and Bandits updates. New text gets new IDs, which show in English until your translation adds them. The [changelog](CHANGELOG.md) lists added, changed and removed IDs for each version.

## Permission and credit

You may publish a translation of Female Troops and Bandits as a separate mod, on Nexus Mods or elsewhere, under terms of your choice, as long as it contains only translated text and no code or other files from Female Troops and Bandits. Please list Female Troops and Bandits as a requirement and credit it in your mod's description:

    Translation of Female Troops and Bandits by AtanuDebbarma
    https://github.com/AtanuDebbarma/FemaleTroopsAndBandits
