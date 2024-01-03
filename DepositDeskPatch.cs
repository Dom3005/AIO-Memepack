using HarmonyLib;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(DepositItemsDesk))]
    internal class DepositDeskPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void Start(ref DepositItemsDesk __instance)
        {
            shuffleSounds(__instance);
            __instance.speakerAudio.maxDistance *= 3f;
            __instance.speakerAudio.volume *= 1.5f;
        }

        [HarmonyPatch(nameof(DepositItemsDesk.OpenShutDoor))]
        [HarmonyPrefix]
        public static void toggleDoor(ref DepositItemsDesk __instance)
        {
            shuffleSounds(__instance);
        }

        private static void shuffleSounds(DepositItemsDesk __instance)
        {
            __instance.rumbleSFX = AudioManager.getRandomClip("traut-euch.mp3", "rehehehe.mp3");
            __instance.doorOpenSFX = AudioManager.getRandomClip("trymacs-hier.mp3", "rehehehe.mp3");
            __instance.rewardGood = AudioManager.getRandomClip("yippee.mp3");
            __instance.microphoneAudios = AudioManager.getAllClips("hier-ist-alles-super.mp3", "chicken-nuggets.mp3", "monte-schonen-abend.mp3", "kaltes-wasser.mp3", "skull-guitar.mp3");
        }
    }
}
