using HarmonyLib;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(WalkieTalkie))]
    internal class WalkietalkiePatch
    {
        [HarmonyPatch(nameof(WalkieTalkie.Start))]
        [HarmonyPostfix]
        public static void Start(ref FlashlightItem __instance)
        {
            __instance.itemProperties.batteryUsage *= 3f;
        }
    }
}
