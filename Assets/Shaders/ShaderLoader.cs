using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Assets.Shaders;

public class ShaderLoader : ModSystem
{
	public override void Load()
	{
		if (Main.netMode == NetmodeID.Server)
		{
			return;
		}
		AddMiscShaders("AdditiveColor");
		AddMiscShaders("WOSLaser");
	}
	private void AddMiscShaders(string name)
	{
		GameShaders.Misc.Add($"Avalon:{name}", new MiscShaderData(Mod.Assets.Request<Effect>($"Effects/{name}"), name));
	}
}
