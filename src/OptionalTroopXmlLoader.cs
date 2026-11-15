using System;
using System.IO;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;
using TaleWorlds.ObjectSystem;

namespace FemaleTroopsAndBandits
{
    // Optional integration troop XML ships in this module's ModuleData but is not listed in
    // SubModule.xml (always-load crashes when the dependency is missing). Load at campaign
    // start only when that dependency is active.
    internal static class OptionalTroopXmlLoader
    {
        private static bool _loaded;

        internal static void TryLoadForCampaign()
        {
            if (_loaded)
            {
                return;
            }

            MBObjectManager manager = MBObjectManager.Instance;
            if (manager == null)
            {
                return;
            }

            string moduleRoot = GetModuleRoot();
            if (string.IsNullOrEmpty(moduleRoot))
            {
                return;
            }

            if (FemaleTroopBranches.NavalDlcModuleSelected())
            {
                LoadPack(manager, moduleRoot, "War Sails", new string[]
                {
                    "ftr_warsails_nord_troops.xml",
                    "ftr_warsails_extra_troops.xml",
                    "ftr_warsails_corsair_troops.xml",
                    "ftr_warsails_quest_caravan_troops.xml",
                    "ftr_naval_caravan_troops.xml",
                });
            }
            else
            {
                Debug.Print("[FemaleTroopsAndBandits] NavalDLC not active; War Sails female troops skipped.");
            }

            if (FemaleTroopBranches.CamelCavalryModuleSelected())
            {
                LoadPack(manager, moduleRoot, "Camel Cavalry", new string[]
                {
                    "ftr_camel_cavalry_troops.xml",
                });
            }
            else
            {
                Debug.Print("[FemaleTroopsAndBandits] CamelCavalry not active; female camel troops skipped.");
            }

            if (FemaleTroopBranches.HorseBanditsModuleSelected())
            {
                LoadPack(manager, moduleRoot, "Horse Bandits", new string[]
                {
                    "ftr_horse_bandits_troops.xml",
                });
            }
            else
            {
                Debug.Print("[FemaleTroopsAndBandits] HorseBandits not active; female Horse Bandits troops skipped.");
            }

            _loaded = true;
        }

        internal static void ResetForNewGameSession()
        {
            _loaded = false;
        }

        private static string GetModuleRoot()
        {
            try
            {
                return ModuleHelper.GetModuleFullPath("FemaleTroopsAndBandits");
            }
            catch (Exception exception)
            {
                Debug.Print("[FemaleTroopsAndBandits] optional troop XML path failed: " + exception.Message);
                return null;
            }
        }

        private static void LoadPack(MBObjectManager manager, string moduleRoot, string packName, string[] fileNames)
        {
            int loaded = 0;
            for (int i = 0; i < fileNames.Length; i++)
            {
                string path = moduleRoot + "ModuleData" + Path.DirectorySeparatorChar + fileNames[i];
                if (!File.Exists(path))
                {
                    Debug.Print("[FemaleTroopsAndBandits] missing " + packName + " troop XML: " + fileNames[i]);
                    continue;
                }

                try
                {
                    manager.LoadOneXmlFromFile(path, string.Empty, true);
                    loaded++;
                }
                catch (Exception exception)
                {
                    Debug.Print("[FemaleTroopsAndBandits] failed loading " + fileNames[i] + ": " + exception.Message);
                }
            }

            Debug.Print("[FemaleTroopsAndBandits] " + packName + " female troop XML loaded (" + loaded + " files).");
        }
    }
}
