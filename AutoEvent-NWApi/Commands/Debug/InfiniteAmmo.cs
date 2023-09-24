﻿// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         AutoEvent
//    Project:          AutoEvent
//    FileName:         InfiniteAmmo.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/23/2023 11:44 AM
//    Created Date:     09/23/2023 11:44 AM
// -----------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AutoEvent.API;
using CommandSystem;
using PluginAPI.Core;
using Utils.NonAllocLINQ;
#if EXILED
using Exiled.Permissions.Extensions;
#endif

namespace AutoEvent.Commands.Debug;

public class InfiniteAmmo : ICommand, IUsageProvider
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {

#if EXILED
        if (!sender.CheckPermission("ev.debug"))
        {
            response = "You do not have permission to use this command";
            return false;
        }
#else
        var config = AutoEvent.Singleton.Config;
        var player = Player.Get(sender);
        if (sender is ServerConsoleSender || sender is CommandSender cmdSender && cmdSender.FullPermissions)
        {
            goto skipPermissionCheck;
        }

        if (!config.PermissionList.Contains(ServerStatic.PermissionsHandler._members[player.UserId]))
        {
            response = "<color=red>You do not have permission to use this command!</color>";
            return false;
        }
#endif

        skipPermissionCheck:

        if (arguments.Count >= 1)
        {
            Player ply = Player.GetPlayers().FirstOrDefault(pl => pl.Nickname.ToLower().Contains(arguments.At(0).ToLower()));
            if (ply is null)
            {
                response = $"Player \"{arguments.At(0)}\" not found. Please specify a valid player.";
                return false;
            }
            AmmoMode ammo = AmmoMode.InfiniteAmmo;
            if (arguments.Count >= 2)
            {
                switch (arguments.At(1).ToLower())
                {
                    case "infinite":
                        ammo = AmmoMode.InfiniteAmmo;
                        break;
                    case "endless":
                        ammo = AmmoMode.EndlessClip;
                        break;
                    case "none":
                        ammo = AmmoMode.None;
                        break;
                }
            }
            ply.GiveInfiniteAmmo(ammo);
            response = $"Infinite ammo {(ammo == AmmoMode.None ? "removed" : "given" )}.";
            return true;
        }

        response = "Please specify a player.";
        return false;
    }

    public string Command { get; } = "InfAmmo";
    public string[] Aliases { get; } = Array.Empty<string>();
    public string Description { get; } = "Gives a user infinite ammo.";
    public string[] Usage { get; } = new[] { "%player%", "[Infinite / Endless / None]" };
}