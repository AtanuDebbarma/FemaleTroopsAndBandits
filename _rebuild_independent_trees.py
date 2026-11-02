# One-shot rebuild: independent female trees, no vanilla ID overwrites.
# Requires BANNERLORD_DIR (folder that contains Bannerlord.exe / Modules / bin), or pass it as argv[1].
import os
import re
import sys
from pathlib import Path

xml_path = Path(__file__).resolve().parent / "ModuleData" / "spfnpccharacters.xml"
bannerlord_dir = Path(sys.argv[1] if len(sys.argv) > 1 else os.environ.get("BANNERLORD_DIR", "")).expanduser()
if not bannerlord_dir or not bannerlord_dir.is_dir():
    raise SystemExit(
        "Set BANNERLORD_DIR to your Bannerlord install folder, or pass that folder as the first argument."
    )
van_path = bannerlord_dir / "Modules" / "SandBoxCore" / "ModuleData" / "spnpccharacters.xml"
if not van_path.is_file():
    raise SystemExit("Vanilla characters not found: %s" % van_path)

xml = xml_path.read_text(encoding="utf-8")
van = set(re.findall(r'<NPCCharacter\b[^>]*\bid\s*=\s*"([^"]+)"', van_path.read_text(encoding="utf-8", errors="ignore")))

blocks = []
for m in re.finditer(r"(<NPCCharacter\b[^>]*>)(.*?)(</NPCCharacter>)", xml, re.S):
    open_tag, body, close = m.group(1), m.group(2), m.group(3)
    attrs = dict(re.findall(r'(\w+)\s*=\s*"([^"]*)"', open_tag))
    ups = re.findall(r'<upgrade_target\s+id\s*=\s*"NPCCharacter\.([^"]+)"\s*/>', body)
    blocks.append({"attrs": attrs, "body": body, "open": open_tag, "close": close, "ups": ups})

by_id = {b["attrs"]["id"]: b for b in blocks}

renames = {
    "galloglass_tier_2": "female_galloglass_tier_2",
    "galloglass_tier_3": "female_galloglass_tier_3",
    "eleftheroi_tier_1": "female_eleftheroi_tier_1",
}


def replace_ups(body, new_ups):
    body2 = re.sub(r"\s*<upgrade_targets>.*?</upgrade_targets>", "", body, flags=re.S)
    if not new_ups:
        return body2
    block = "\n\t\t<upgrade_targets>\n"
    for u in new_ups:
        block += '\t\t\t<upgrade_target\n\t\t\t\tid="NPCCharacter.%s" />\n' % u
    block += "\t\t</upgrade_targets>\n"
    if "<Equipments>" in body2:
        return body2.replace("<Equipments>", block + "\t\t<Equipments>", 1)
    return body2 + block


def only_female_ups(ups):
    out = []
    for u in ups:
        u2 = renames.get(u, u)
        if u2 in van and u2 not in renames:
            continue
        if u2 in by_id:
            if by_id[u2]["attrs"].get("is_female") == "true" or u2 in renames.values() or u2 in renames:
                out.append(renames.get(u2, u2) if u2 in renames else u2)
                continue
        if (
            "female" in u2
            or u2.endswith("woman")
            or u2.endswith("ess")
            or u2.endswith("daughter")
            or u2.startswith("female_")
        ):
            out.append(u2)
    # dedupe
    seen = set()
    result = []
    for u in out:
        u = renames.get(u, u)
        if u not in seen:
            seen.add(u)
            result.append(u)
    return result


def force_attr(open_tag, key, value):
    if re.search(r"\b%s\s*=" % key, open_tag):
        return re.sub(r'(%s\s*=\s*")[^"]*(")' % key, r"\1%s\2" % value, open_tag)
    return open_tag.replace(">", "\n\t\t%s=\"%s\">" % (key, value), 1)


def set_id_name(open_tag, new_id, name=None):
    open_tag = re.sub(r'(id\s*=\s*")[^"]*(")', r"\1%s\2" % new_id, open_tag, count=1)
    if name is not None:
        open_tag = re.sub(r'(name\s*=\s*")[^"]*(")', r"\1%s\2" % name, open_tag, count=1)
    return open_tag


out_chars = []

# Keep unique non-vanilla characters; rename broken vanilla-id females.
for b in blocks:
    cid = b["attrs"]["id"]
    if cid in van and cid not in renames:
        continue

    new_id = renames.get(cid, cid)
    ups = []
    for u in b["ups"]:
        u2 = renames.get(u, u)
        if u2 in van and u2 not in renames:
            continue
        ups.append(u2)
    ups = list(dict.fromkeys(ups))

    open_tag = set_id_name(b["open"], new_id)
    open_tag = force_attr(open_tag, "is_female", "true")
    body = replace_ups(b["body"], ups)
    out_chars.append(open_tag + body + b["close"])

root_specs = [
    ("ftr_aserai_recruit", "aserai_recruit", "{=ftr_aserai_recruit}Aserai Tribeswoman Recruit"),
    ("ftr_aserai_youth", "aserai_youth", "{=ftr_aserai_youth}Aserai Youthwoman"),
    ("ftr_battanian_volunteer", "battanian_volunteer", "{=ftr_battanian_volunteer}Battanian Volunteeress"),
    ("ftr_battanian_highborn_youth", "battanian_highborn_youth", "{=ftr_battanian_highborn}Battanian Highborn Girl"),
    ("ftr_imperial_recruit", "imperial_recruit", "{=ftr_imperial_recruit}Imperial Recruitess"),
    ("ftr_imperial_vigla_recruit", "imperial_vigla_recruit", "{=ftr_imperial_vigla}Imperial Vigla Recruitess"),
    ("ftr_khuzait_nomad", "khuzait_nomad", "{=ftr_khuzait_nomad}Khuzait Nomadess"),
    ("ftr_sturgian_recruit", "sturgian_recruit", "{=ftr_sturgian_recruit}Sturgian Recruitess"),
    ("ftr_vlandian_recruit", "vlandian_recruit", "{=ftr_vlandian_recruit}Vlandian Recruitess"),
    ("ftr_vlandian_squire", "vlandian_squire", "{=ftr_vlandian_squire}Vlandian Squiress"),
]

for new_id, src_id, name in root_specs:
    src = by_id[src_id]
    ups = only_female_ups(src["ups"])
    open_tag = set_id_name(src["open"], new_id, name)
    open_tag = force_attr(open_tag, "is_female", "true")
    open_tag = force_attr(open_tag, "is_basic_troop", "true")
    open_tag = force_attr(open_tag, "occupation", "Soldier")
    body = replace_ups(src["body"], ups)
    out_chars.append(open_tag + body + src["close"])

minor_ids = [
    "ghilman_tier_1",
    "legion_of_the_betrayed_tier_1",
    "skolderbrotva_tier_1",
    "company_of_the_boar_tier_1",
    "beni_zilal_tier_1",
    "wolfskins_tier_1",
    "hidden_hand_tier_1",
    "lakepike_tier_1",
    "brotherhood_of_woods_tier_1",
    "embers_of_flame_tier_1",
    "jawwal_tier_1",
    "karakhuzaits_tier_1",
    "forest_people_tier_1",
    "gangster_1",
    "galloglass_tier_1",
]

for src_id in minor_ids:
    if src_id not in by_id:
        continue
    src = by_id[src_id]
    if src_id == "galloglass_tier_1":
        ups = ["female_galloglass_tier_2"]
    else:
        ups = only_female_ups(src["ups"])
        if not ups:
            ups = [renames.get(u, u) for u in src["ups"] if "female" in u or u.endswith("woman") or u.endswith("ess")]
            ups = list(dict.fromkeys(ups))
    if not ups:
        continue
    new_id = "ftr_" + src_id
    open_tag = set_id_name(src["open"], new_id)
    open_tag = force_attr(open_tag, "is_female", "true")
    open_tag = force_attr(open_tag, "is_basic_troop", "true")
    open_tag = force_attr(open_tag, "occupation", "Soldier")
    body = replace_ups(src["body"], ups)
    out_chars.append(open_tag + body + src["close"])

bak = xml_path.with_suffix(".xml.bak_2022")
if not bak.exists():
    bak.write_text(xml, encoding="utf-8")
    print("backed up:", bak)

header = (
    '<?xml version="1.0" encoding="utf-8"?>\n'
    "<NPCCharacters>\n\n"
    "<!-- Female Troops Reborn for Bannerlord 1.4.8: independent female trees only. No vanilla troop IDs. -->\n\n"
)
footer = "\n</NPCCharacters>\n"
text = header + "\n\n".join(out_chars) + footer
xml_path.write_text(text, encoding="utf-8")

nids = re.findall(r'<NPCCharacter\b[^>]*\bid\s*=\s*"([^"]+)"', text)
overlap = sorted(set(nids) & van)
defined = set(nids) | van
miss = []
for m in re.finditer(r"<NPCCharacter\b([^>]*)>(.*?)</NPCCharacter>", text, re.S):
    attrs = dict(re.findall(r'(\w+)\s*=\s*"([^"]*)"', m.group(1)))
    for u in re.findall(r'<upgrade_target\s+id\s*=\s*"NPCCharacter\.([^"]+)"', m.group(2)):
        if u not in defined:
            miss.append((attrs["id"], u))

print("wrote", xml_path)
print("chars", len(nids), "unique", len(set(nids)))
print("vanilla overlap", overlap, "count", len(overlap))
print("missing upgrades", miss, "count", len(miss))
for r, _, _ in root_specs:
    print("root", r, "OK" if r in nids else "MISSING")
for x in (
    "khuzait_noble_daughter",
    "sturgian_warrior_daughter",
    "female_galloglass_tier_2",
    "female_galloglass_tier_3",
    "female_eleftheroi_tier_1",
    "ftr_galloglass_tier_1",
):
    print(x, "OK" if x in nids else "MISSING")
