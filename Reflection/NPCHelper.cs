using System;
using System.Reflection;
using Terraria;

namespace Avalon.Reflection;

public static class NPCHelper
{
    public static readonly Func<int> GetSpawnRate = Utilities.CreateFieldReader<int>(typeof(NPC).GetField("spawnRate", BindingFlags.Static | BindingFlags.NonPublic)!);
}
