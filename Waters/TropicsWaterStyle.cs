using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Waters;

public class TropicsWaterStyle : ModWaterStyle
{
    public override int ChooseWaterfallStyle()
    {
        return Mod.Find<ModWaterfallStyle>("TropicsWaterfallStyle").Slot;
    }

    public override int GetSplashDust()
    {
        return ModContent.DustType<Dusts.TropicsWaterSplash>();
    }

    public override int GetDropletGore()
    {
        return Mod.Find<ModGore>("TropicsDroplet").Type;
    }

    public override void LightColorMultiplier(ref float r, ref float g, ref float b)
    {
        r = 1f;
        g = 1f;
        b = 1f;
    }

    public override Color BiomeHairColor()
    {
        return new Color(134, 101, 200);
    }

    public override byte GetRainVariant()
    {
        return (byte)Main.rand.Next(3);
    }

    public override Asset<Texture2D> GetRainTexture()
    {
        return ModContent.Request<Texture2D>("Avalon/Waters/TropicsRain");
    }
}
