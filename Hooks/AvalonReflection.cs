using System.Reflection;
using Terraria;

namespace ExxoAvalonOrigins.Hooks;
internal class AvalonReflection
{
    private static FieldInfo Main_MH = null;

    internal static int Main_mH
    {
        get => (int)Main_MH.GetValue(null);
        set => Main_MH.SetValue(null, value);
    }
    internal static void Init()
    {
        Main_MH = typeof(Main).GetField("mH", BindingFlags.NonPublic | BindingFlags.Static);
    }
    internal static void Unload()
    {
        Main_MH = null;
    }
}
