using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Assets.Shaders;

public class ShaderLoader : ModSystem
{
	private static Asset<Effect> AdditiveColor;
	private static Asset<Effect> WOSLaser;
	public override void Load()
	{
		if (Main.netMode == NetmodeID.Server)
		{
			return;
		}
		AdditiveColor = Mod.Assets.Request<Effect>("Effects/AdditiveColor");
		MiscShaderData AdditiveColorData = new MiscShaderData(AdditiveColor, "AdditiveColor");
		GameShaders.Misc.Add("Avalon:AdditiveColor", AdditiveColorData);

		WOSLaser = Mod.Assets.Request<Effect>("Effects/WOSLaser");
		MiscShaderData WOSLaserData = new MiscShaderData(WOSLaser, "WOSLaser");
		GameShaders.Misc.Add("Avalon:WOSLaser", WOSLaserData);
	}
}
