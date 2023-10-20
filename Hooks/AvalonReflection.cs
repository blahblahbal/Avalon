using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Hooks;
internal class AvalonReflection
{
    private static FieldInfo Main_MH = null;
    private static FieldInfo NPC_SpawnRate = null;

    internal static int Main_mH
    {
        get => (int)Main_MH.GetValue(null);
        set => Main_MH.SetValue(null, value);
    }
    internal static int NPC_spawnRate
    {
        get => (int)NPC_SpawnRate.GetValue(null);
        set => NPC_SpawnRate.SetValue(null, value);
    }
    internal static void Init()
    {
        Main_MH = typeof(Main).GetField("mH", BindingFlags.NonPublic | BindingFlags.Static);
        NPC_SpawnRate = typeof(NPC).GetField("spawnRate", BindingFlags.NonPublic | BindingFlags.Static);
    }
    internal static void Unload()
    {
        Main_MH = null;
        NPC_SpawnRate = null;
    }
}
