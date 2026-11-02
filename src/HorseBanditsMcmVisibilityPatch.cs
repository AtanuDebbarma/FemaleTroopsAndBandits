using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using TaleWorlds.Library;

namespace FemaleTroopsAndBandits
{
    // Attribute MCM cannot hide a group by itself. When Horse Bandits is not in the
    // enabled module list, drop that group's properties so the toggle and slider are gone.
    [HarmonyPatch]
    internal static class HorseBanditsMcmVisibilityPatch
    {
        static bool Prepare()
        {
            return AccessTools.TypeByName("MCM.Implementation.AttributeSettingsPropertyDiscoverer") != null;
        }

        static MethodBase TargetMethod()
        {
            Type type = AccessTools.TypeByName("MCM.Implementation.AttributeSettingsPropertyDiscoverer");
            if (type == null)
            {
                return null;
            }

            MethodInfo method = AccessTools.Method(type, "GetProperties", new Type[] { typeof(BaseSettings) });
            if (method == null)
            {
                method = AccessTools.Method(type, "GetProperties");
            }

            return method;
        }

        static void Postfix(BaseSettings settings, ref IEnumerable<ISettingsPropertyDefinition> __result)
        {
            if (settings == null || settings.Id != "FemaleTroopsAndBandits")
            {
                return;
            }

            if (FemaleTroopBranches.HorseBanditsModuleSelected())
            {
                return;
            }

            if (__result == null)
            {
                return;
            }

            List<ISettingsPropertyDefinition> kept = new List<ISettingsPropertyDefinition>();
            foreach (ISettingsPropertyDefinition definition in __result)
            {
                if (definition == null)
                {
                    continue;
                }

                if (IsHorseBanditsGroup(definition))
                {
                    continue;
                }

                kept.Add(definition);
            }

            __result = kept;
        }

        private static bool IsHorseBanditsGroup(ISettingsPropertyDefinition definition)
        {
            string groupName = null;
            try
            {
                groupName = definition.GroupName;
            }
            catch
            {
            }

            if (!string.IsNullOrEmpty(groupName)
                && groupName.IndexOf(FemaleTroopsSettings.HorseBanditsGroupMarker) >= 0)
            {
                return true;
            }

            if (!string.IsNullOrEmpty(groupName)
                && groupName.IndexOf("Horse Bandits (Mod Support)") >= 0)
            {
                return true;
            }

            try
            {
                PropertyInfo raw = definition.GetType().GetProperty("GroupNameRaw", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (raw != null)
                {
                    object value = raw.GetValue(definition, null);
                    string rawName = value as string;
                    if (!string.IsNullOrEmpty(rawName)
                        && rawName.IndexOf(FemaleTroopsSettings.HorseBanditsGroupMarker) >= 0)
                    {
                        return true;
                    }
                }
            }
            catch
            {
            }

            return false;
        }

        static Exception Finalizer(Exception __exception)
        {
            if (__exception != null)
            {
                Debug.Print("[FemaleTroopsAndBandits] Horse Bandits MCM visibility filter skipped: " + __exception.Message);
            }

            return null;
        }
    }
}
