using HarmonyLib;
using UnityEngine;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(FlashlightItem))]
    internal class FlashlightPatch
    {
        private static Light lightbulb;
        private static float initialIntensity;

        private static float intensityMultiplier;

        [HarmonyPatch(nameof(FlashlightItem.Start))]
        [HarmonyPostfix]
        public static void Start(ref FlashlightItem __instance)
        {
            if (ConfigManager._enableFlashlightColors.Value)
            {
                float r = (float)Random.Range(50, 100) / 100;
                float g = (float)Random.Range(50, 100) / 100;
                float b = (float)Random.Range(50, 100) / 100;
                lightbulb.color = new Color(r, g, b);
            }
            __instance.itemProperties.batteryUsage *= 3f;
        }

        [HarmonyPatch(nameof(FlashlightItem.Update))]
        [HarmonyPostfix]
        public static void Update(ref FlashlightItem __instance)
        {
            intensityMultiplier = ConfigManager._flashlightStrength.Value;

            lightbulb = __instance.flashlightBulb;
            initialIntensity = lightbulb.intensity * intensityMultiplier;
            lightbulb.spotAngle *= 1.1f;
        }
    }
}
