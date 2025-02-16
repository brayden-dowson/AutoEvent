﻿// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         AutoEvent
//    Project:          AutoEvent
//    FileName:         SoundInfo.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/06/2023 5:04 PM
//    Created Date:     09/06/2023 5:04 PM
// -----------------------------------------

using System.ComponentModel;
using YamlDotNet.Serialization;

namespace AutoEvent.Interfaces;

public class SoundInfo
{
    public SoundInfo() { }
    public SoundInfo(string name, byte volume = 10, bool loop = true)
    {
        SoundName = name;
        Volume = volume;
        Loop = loop;
    }
    [Description("The name of the sound.")]
    public string SoundName { get; set; }
    
    [Description("The volume that the sound should play at.")]
    public byte Volume { get; set; } = 10;
    
    [Description("Should the sound loop or not.")]
    public bool Loop { get; set; } = true;
    
    [YamlIgnore]
    public bool StartAutomatically { get; set; } = true;
}