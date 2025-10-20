using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ancient.AncientYellowBrick;

public class AncientYellowBrick : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<AncientYellowBrickTile>());
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<AncientYellowBrickWallItem>(), 4)
			.AddTile(TileID.WorkBenches)
			.DisableDecraft()
			.Register();
	}
}
[LegacyName("AncientYellowBrick")]
public class AncientYellowBrickTile : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(91, 84, 52));//(82, 52, 156)); 94, 71, 117));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileDungeon[Type] = true;
		TileID.Sets.DungeonBiome[Type] = 1;
		HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.YellowDungeonDust>();
    }
}
