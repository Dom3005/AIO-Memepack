using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(TimeOfDay))]
    internal class TimeOfDayPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        public static void Update(ref TimeOfDay __instance)
        {
            if(ConfigManager._disableEarlyLeaveVote.Value)
                __instance.votedShipToLeaveEarlyThisRound = true;
        }
    }
}
