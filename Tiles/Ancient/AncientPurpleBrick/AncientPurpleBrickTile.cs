using Avalon.Items.Placeable.Wall;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ancient.AncientPurpleBrick;

public class AncientPurpleBrick : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<AncientPurpleBrickTile>());
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<AncientPurpleBrickWallItem>(), 4)
			.AddTile(TileID.WorkBenches)
			.DisableDecraft()
			.Register();
	}
}
[LegacyName("AncientPurpleBrick")]
public class AncientPurpleBrickTile : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(102, 78, 123));//(82, 52, 156)); 94, 71, 117));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileDungeon[Type] = true;
		TileID.Sets.DungeonBiome[Type] = 1;
		HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.PurpleDungeonDust>();
        TileID.Sets.GeneralPlacementTiles[Type] = false;
    }
}
