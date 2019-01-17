
using Harmony;

namespace TempDropTweaks
{
    [HarmonyPatch(typeof(ExperienceMode), "Start")]
    class TempDrop
    {
        public static void Prefix(ExperienceMode __instance)
        {

            __instance.m_OutdoorTempDropCelsiusMax = TempDropOptions.temp_drop_celsius_max;

        }
    }
}
