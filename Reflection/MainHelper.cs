using System;
using System.Reflection;
using Terraria;

namespace Avalon.Reflection;

public static class MainHelper
{
    public static readonly Func<int> GetMm = Utilities.CreateFieldReader<int>(typeof(Main).GetField("mH", BindingFlags.Static | BindingFlags.NonPublic)!);
}
