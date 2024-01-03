using GameNetcodeStuff;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PlayerControllerPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        private static void Start(PlayerControllerB __instance)
        {
            if (!ConfigManager._enableInfiniteSprint.Value) return;

            __instance.sprintMeterUI.gameObject.SetActive(false);
            __instance.sprintTime = 100f;
        }


        [HarmonyPatch("LateUpdate")]
        [HarmonyPostfix]
        private static void LateUpdate(PlayerControllerB __instance)
        {
            if (!ConfigManager._enableInfiniteSprint.Value) return;
            if (__instance.sprintMeter <= 0.5f) __instance.sprintMeter = 1f;
        }


        //
        // View angle 60 -> 80
        // for looking straight down
        //

        [HarmonyPatch(typeof(PlayerControllerB), "CalculateNormalLookingInput")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> CalculateNormalInput(IEnumerable<CodeInstruction> instructions)
        {
            foreach(CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldc_R4 && (float)instruction.operand == 60f)
                {
                    instruction.operand = 80f;
                    break;
                }
            }
            return instructions;
        }


        //
        // View angle 60 -> 80
        // for looking straight down
        //

        [HarmonyPatch(typeof(PlayerControllerB), "CalculateSmoothLookingInput")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> CalculateSmoothInput(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldc_R4 && (float)instruction.operand == 60f)
                {
                    instruction.operand = 80f;
                    break;
                }
            }
            return instructions;
        }



        //[HarmonyPatch(nameof(PlayerControllerB.KillPlayer))]
        //[HarmonyPrefix]
        //public static void KillPlayer(ref PlayerControllerB __instance)
        //{
        //    string[] deathSounds = { "auto-machen-so.mp3", "bigmac.mp3", "das-gibt-ne-6.mp3", "ekg.mp3", "fart.mp3", "harry-potter.mp3", "roblox-death.mp3",
        //        "skill-issue.mp3", "snore-mimimi.mp3", "vine-boom.mp3", "fortnite-knock.mp3"};

        //    AudioClip clip = AudioManager.getAudioClip(deathSounds[Random.Range(0, deathSounds.Length)]);
        //    AudioManager.createAudioSource(__instance.transform.position, null, 100).PlayOneShot(clip);
        //}

        
        [HarmonyPatch("KillPlayerClientRpc")]
        [HarmonyPrefix]
        public static void KillPlayerClient(ref PlayerControllerB __instance)
        {
            string[] deathSounds = { "auto-machen-so.mp3", "bigmac.mp3", "das-gibt-ne-6.mp3", "ekg.mp3", "fart.mp3", "harry-potter.mp3", "roblox-death.mp3",
                "skill-issue.mp3", "snore-mimimi.mp3", "vine-boom.mp3", "fortnite-knock.mp3"};

            AudioClip clip = AudioManager.getAudioClip(deathSounds[Random.Range(0, deathSounds.Length)]);
            AudioManager.createAudioSource(__instance.transform.position, null, 100).PlayOneShot(clip);
        }
    }
}
