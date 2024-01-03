using HarmonyLib;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(HoarderBugAI))]
    internal class BugPatch
    {
        [HarmonyPatch(nameof(HoarderBugAI.Start))]
        [HarmonyPostfix]
        public static void Start(ref HoarderBugAI __instance)
        {
            __instance.chitterSFX = AudioManager.getAllClips("happy.mp3", "yippee.mp3", "ich-bins-farid.mp3");
            __instance.angryScreechSFX = AudioManager.getAllClips("bing-bong.mp3", "pimmelberger.mp3");
        }
    }
}
