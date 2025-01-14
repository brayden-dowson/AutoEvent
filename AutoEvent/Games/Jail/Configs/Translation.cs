﻿using Exiled.API.Interfaces;

namespace AutoEvent.Games.Infection
{
#if EXILED
    public class JailTranslate  : ITranslation 
#else
    public class JailTranslate 
#endif
    {
        public string JailCommandName { get; set; } = "jail";
        public string JailName { get; set; } = "Simon's Prison";
        public string JailDescription { get; set; } = "Jail mode from CS 1.6, in which you need to hold events [VERY HARD].";
        public string JailBeforeStart { get; set; } = "<color=yellow><color=red><b><i>{name}</i></b></color>\n<i>Trigger or release a lockdown by shooting the big button</i>\nBefore the start: <color=red>{time}</color> seconds</color>";
        public string JailBeforeStartPrisoners { get; set; } = "<color=yellow><color=red><b><i>{name}</i></b></color>\n<i>Escape the prison at any costs. Be careful. Items ar scattered around the map to help you survive.</i>\nBefore the start: <color=red>{time}</color> seconds</color>";
        public string JailCycle { get; set; } = "<size=20><color=red>{name}</color>\n<color=yellow>Prisoners: {dclasscount}</color> || <color=#14AAF5>Jailers: {mtfcount}</color>\n<color=red>{time}</color></size>";
        public string JailPrisonersWin { get; set; } = "<color=red><b><i>Prisoners Win</i></b></color>\n<color=red>{time}</color>";
        public string JailJailersWin { get; set; } = "<color=#14AAF5><b><i>Jailers Win</i></b></color>\n<color=red>{time}</color>";
        public string JailLockdownOnCooldown { get; set; } = "You cannot trigger a lockdown, while it is on cooldown. Cooldown Remaining: {cooldown}";
        public string JailLivesRemaining { get; set; } = "You have {lives} lives remaining.";
        public string JailNoLivesRemaining { get; set; } = "You have no more lives remaining.";
    }
}