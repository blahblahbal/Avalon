using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Systems;

// for now just used for darkening postdraw of tiles when actuated
public class TileGlowDrawing : ModSystem
{
	public static Color ActuatedColor(Color oldColor, Tile tile)
	{
		if (!tile.IsActuated)
		{
			return oldColor;
		}
		double num = 0.4;
		return new Color((int)(byte)(num * (double)(int)oldColor.R), (int)(byte)(num * (double)(int)oldColor.G), (int)(byte)(num * (double)(int)oldColor.B), (int)oldColor.A);
	}
}
