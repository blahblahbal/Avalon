using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls.Contagion.ContagionLumpWall;

[LegacyName("ContagionLumpWall")]
public class ContagionLumpWallItem : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<ContagionLumpWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(ModContent.ItemType<ChunkstoneBlock>()).AddCondition(Condition.InGraveyard).AddTile(TileID.WorkBenches).Register();
		Recipe.Create(ModContent.ItemType<ChunkstoneBlock>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).DisableDecraft().Register();
	}
}
public class ContagionLumpWall : ModWall
{
    public override void SetStaticDefaults()
    {
        WallID.Sets.Conversion.NewWall1[Type] = true;
        Main.wallHouse[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<ContagionLumpWall>();
        AddMapEntry(new Color(61, 71, 51));
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}
public class ContagionLumpWallUnsafe : ModWall
{
	public override string Texture => ModContent.GetInstance<ContagionLumpWall>().Texture;
	public override void SetStaticDefaults()
	{
		WallID.Sets.Conversion.NewWall1[Type] = true;
		Main.wallBlend[Type] = ModContent.WallType<ContagionLumpWall>();
		AddMapEntry(new Color(61, 71, 51));
		DustType = ModContent.DustType<Dusts.ContagionDust>();
	}
}
