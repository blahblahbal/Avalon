using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Reflection;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Avalon.Hooks;
internal class AvalonReflection
{
    private static FieldInfo Main_MH = null;
    private static FieldInfo Asd_uImage = null;

    internal static string PATH = "";

    internal static int Main_mH
    {
        get => (int)Main_MH.GetValue(null);
        set => Main_MH.SetValue(null, value);
    }

    internal static Asset<Texture2D> ASD_uImage
    {
        get => (Asset<Texture2D>)Asd_uImage.GetValue(null);
        set => Asd_uImage.SetValue(new ArmorShaderData(new Ref<Effect>(ModContent.Request<Effect>("Avalon/Effects/" + PATH, AssetRequestMode.ImmediateLoad).Value), PATH), value);
    }
    internal static void Init()
    {
        Main_MH = typeof(Main).GetField("mH", BindingFlags.NonPublic | BindingFlags.Static);
        Asd_uImage = typeof(ArmorShaderData).GetField("_uImage", BindingFlags.Instance | BindingFlags.NonPublic);
    }
    internal static void Unload()
    {
        Main_MH = null;
        Asd_uImage = null;
    }
}
