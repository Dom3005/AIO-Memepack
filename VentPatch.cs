using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(EnemyVent))]
    internal class VentPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        public static void Update(ref EnemyVent __instance)
        {
            if(!__instance.occupied && __instance.ventAudio.isPlaying)
            {
                AudioSource source = AudioManager.createAudioSource(__instance.transform.position, __instance.transform, 10);
                source.clip = AudioManager.getAudioClip("trymacs-hier.mp3");
                source.loop = false;
                source.Play();
            }
        }
    }
}
