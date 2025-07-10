using System;
using System.Reflection;
using Terraria;

namespace Avalon.Reflection;

public static class MainHelper
{
    public static readonly Func<int> GetMm = Utilities.CreateFieldReader<int>(typeof(Main).GetField("mH", BindingFlags.Static | BindingFlags.NonPublic)!);
	public static readonly Func<bool> GetSwapMusic = Utilities.CreateFieldReader<bool>(typeof(Main).GetField("swapMusic", BindingFlags.Static | BindingFlags.NonPublic)!);
}
