using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Waters;

public class PhantomOvergrowthWaterStyle : ModWaterStyle
{
	private Asset<Texture2D> rainTexture;
	public override void Load()
	{
		rainTexture = ModContent.Request<Texture2D>("Avalon/Waters/PhantomRain");
	}
	public override int ChooseWaterfallStyle()
    {
        return Mod.Find<ModWaterfallStyle>("PhantomOvergrowthWaterfallStyle").Slot;
    }

    public override int GetSplashDust()
    {
        return ModContent.DustType<PhantomWaterSplash>();
    }

    public override int GetDropletGore()
    {
        return Mod.Find<ModGore>("PhantomLavaDroplet").Type;
    }

    public override void LightColorMultiplier(ref float r, ref float g, ref float b)
    {
        r = 1f;
        g = 1f;
        b = 1f;
    }

    public override Color BiomeHairColor()
    {
        return Color.White;
    }

    public override byte GetRainVariant()
    {
        return (byte)Main.rand.Next(3);
    }

    public override Asset<Texture2D> GetRainTexture()=> rainTexture;
}


