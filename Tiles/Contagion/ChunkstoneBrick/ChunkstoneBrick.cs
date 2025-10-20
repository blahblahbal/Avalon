using Avalon.Tiles.Contagion.Chunkstone;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion.ChunkstoneBrick;

public class ChunkstoneBrick : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<ChunkstoneBrickTile>());
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<ChunkstoneBlock>(), 2)
			.AddTile(TileID.Furnaces)
			.Register();
	}
}

[LegacyName("ChunkstoneBrick")]
public class ChunkstoneBrickTile : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(109, 149, 91));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Tile.ChunkstoneBrick>();
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.ChunkstoneBrickDust>();
    }
}