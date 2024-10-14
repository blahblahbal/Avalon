using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics;
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

	public static Color ActuatedRetroWallColor(Color oldColor, Tile tile)
	{
		if (!tile.Get<AvalonTileData>().IsWallActupainted)
		{
			return oldColor;
		}
		double num = 0.4;
		return new Color((int)(byte)(num * (double)(int)oldColor.R), (int)(byte)(num * (double)(int)oldColor.G), (int)(byte)(num * (double)(int)oldColor.B), (int)oldColor.A);
	}

	public static VertexColors ActuatedWallColor(VertexColors oldVertexColor, Tile tile)
	{
		if (!tile.Get<AvalonTileData>().IsWallActupainted)
		{
			return oldVertexColor;
		}
		double num = 0.4;
		Color TopLeft = new Color((int)(byte)(num * (double)(int)oldVertexColor.TopLeftColor.R), (int)(byte)(num * (double)(int)oldVertexColor.TopLeftColor.G), (int)(byte)(num * (double)(int)oldVertexColor.TopLeftColor.B), (int)oldVertexColor.TopLeftColor.A);
		Color TopRight = new Color((int)(byte)(num * (double)(int)oldVertexColor.TopRightColor.R), (int)(byte)(num * (double)(int)oldVertexColor.TopRightColor.G), (int)(byte)(num * (double)(int)oldVertexColor.TopRightColor.B), (int)oldVertexColor.TopRightColor.A);
		Color BottomLeft = new Color((int)(byte)(num * (double)(int)oldVertexColor.BottomLeftColor.R), (int)(byte)(num * (double)(int)oldVertexColor.BottomLeftColor.G), (int)(byte)(num * (double)(int)oldVertexColor.BottomLeftColor.B), (int)oldVertexColor.BottomLeftColor.A);
		Color BottomRight = new Color((int)(byte)(num * (double)(int)oldVertexColor.BottomRightColor.R), (int)(byte)(num * (double)(int)oldVertexColor.BottomRightColor.G), (int)(byte)(num * (double)(int)oldVertexColor.BottomRightColor.B), (int)oldVertexColor.BottomRightColor.A);
		return new VertexColors(TopLeft, TopRight, BottomLeft, BottomRight);
	}
}
