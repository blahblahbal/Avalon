using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class PhantoplasmDust : ModDust
{
	public override Color? GetAlpha(Dust dust, Color lightColor)
	{
		return new Color(1f,1f,1f,0f);
	}
}
