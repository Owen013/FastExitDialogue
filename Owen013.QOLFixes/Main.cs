﻿using HarmonyLib;
using OWML.ModHelper;
using OWML.Common;
using System.Reflection;

namespace QOLFixes;

public class Main : ModBehaviour
{
    // Mod vars
    public static Main Instance;
    public delegate void ConfigureEvent();
    public event ConfigureEvent OnConfigure;

    public override void Configure(IModConfig config)
    {
        base.Configure(config);
        Config.ReticleMode = config.GetSettingsValue<string>("ReticleMode");
        Config.IsAutoScoutEquipDisabled = config.GetSettingsValue<bool>("DisableAutoScoutEquip");
        Config.IsFreezeTimeAtEyeDisabled = config.GetSettingsValue<bool>("DisableFreezeTime");
        Config.CanManuallyEquipTranslator = config.GetSettingsValue<bool>("EquipTranslator");
        Config.IsEyesAlwaysGlowEnabled = config.GetSettingsValue<bool>("EyesAlwaysGlow");
        Config.IsCancelDialogueEnabled = config.GetSettingsValue<bool>("ExitDialogue");

        OnConfigure?.Invoke();
    }

    private void Awake()
    {
        Instance = this;
        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
    }

    private void Start()
    {
        Log("Quality of Life Changes is ready to go!", MessageType.Success);
    }

    public void Log(string text, MessageType type = MessageType.Message)
    {
        ModHelper.Console.WriteLine(text, type);
    }
}