using Avalon.Items.Placeable.Wall;
using Avalon.Walls.Ancient.AncientOrangeBrick;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ancient.AncientOrangeBrick;
public class AncientOrangeBrick : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<AncientOrangeBrickTile>());
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<AncientOrangeBrickWallItem>(), 4)
			.AddTile(TileID.WorkBenches)
			.DisableDecraft()
			.Register();
	}
}
[LegacyName("AncientOrangeBrick")]
public class AncientOrangeBrickTile : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(135, 76, 56));// 166, 87, 45));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileDungeon[Type] = true;
		TileID.Sets.DungeonBiome[Type] = 1;
		HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.OrangeDungeonDust>();
        TileID.Sets.GeneralPlacementTiles[Type] = false;
    }
}
