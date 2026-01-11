using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion;

public class IckyCactus : ModCactus
{
	private Asset<Texture2D> texture;
	private Asset<Texture2D> fruitTexture;
	public override void SetStaticDefaults()
	{
		GrowsOnTileId = new[] { ModContent.TileType<Snotsand>() };
		texture = ModContent.Request<Texture2D>("Avalon/Tiles/Contagion/IckyCactus");
		fruitTexture = ModContent.Request<Texture2D>("Avalon/Tiles/Contagion/IckyCactus_Fruit");
	}
    public override Asset<Texture2D> GetTexture() => texture;

    public override Asset<Texture2D> GetFruitTexture() => fruitTexture;
}
