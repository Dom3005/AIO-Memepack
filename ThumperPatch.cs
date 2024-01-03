using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(CrawlerAI))]
    internal class ThumperPatch
    {
        [HarmonyPatch(nameof(CrawlerAI.Start))]
        [HarmonyPostfix]
        public static void Start(ref CrawlerAI __instance)
        {
            __instance.hitWallSFX = AudioManager.getAllClips("pimmelberger.mp3");
            __instance.dieSFX = AudioManager.getAudioClip("snore-mimimi.mp3");
            __instance.longRoarSFX = AudioManager.getAllClips("was-zitterstn-so.mp3", "chinese-rap-song.mp3", "bing-bong.mp3");
            __instance.eatPlayerSFX = AudioManager.getAudioClip("number-one-victory-royale.mp3");
            __instance.shortRoar = AudioManager.getAudioClip("skull-guitar.mp3");
            __instance.bitePlayerSFX = AudioManager.getAudioClip("heheheha.mp3");
        }
    }
}
