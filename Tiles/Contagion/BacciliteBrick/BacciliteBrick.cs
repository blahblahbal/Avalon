using Avalon.Items.Material.Ores;
using Avalon.Tiles.Contagion.Chunkstone;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion.BacciliteBrick;

public class BacciliteBrick : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.CloneDefaults(ItemID.DemoniteBrick);
		Item.createTile = ModContent.TileType<BacciliteBrickTile>();
	}
	public override void AddRecipes()
	{
		CreateRecipe(5)
			.AddIngredient(ModContent.ItemType<BacciliteOre>())
			.AddIngredient(ModContent.ItemType<ChunkstoneBlock>(), 5)
			.AddTile(TileID.Furnaces)
			.Register();
	}
}
[LegacyName("BacciliteBrick")]
public class BacciliteBrickTile : ModTile
{
	public override void SetStaticDefaults()
	{
		AddMapEntry(new Color(96, 124, 60));
		Main.tileSolid[Type] = true;
		Main.tileMergeDirt[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileBrick[Type] = true;
		Main.tileMerge[Type][TileID.WoodBlock] = true;
		Main.tileMerge[TileID.WoodBlock][Type] = true;
		Main.tileLighted[Type] = true;
		HitSound = SoundID.Tink;
		DustType = ModContent.DustType<Dusts.ChunkstoneBrickDust>();
	}
	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.2f;
		g = 0.3f;
		b = 0f;
	}
}
