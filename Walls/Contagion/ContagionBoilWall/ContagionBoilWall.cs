using Avalon.Tiles.Contagion.Chunkstone;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls.Contagion.ContagionBoilWall;

[LegacyName("ContagionBoilWall")]
public class ContagionBoilWallItem : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<ContagionBoilWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(ModContent.ItemType<ChunkstoneBlock>()).AddCondition(Condition.InGraveyard).AddTile(TileID.WorkBenches).Register();
		Recipe.Create(ModContent.ItemType<ChunkstoneBlock>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).DisableDecraft().Register();
	}
}
public class ContagionBoilWall : ModWall
{
    public override void SetStaticDefaults()
    {
        WallID.Sets.Conversion.NewWall4[Type] = true;
        Main.wallHouse[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<ContagionBoilWall>();
        AddMapEntry(new Color(63, 66, 56));
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}
public class ContagionBoilWallUnsafe : ModWall
{
	public override string Texture => ModContent.GetInstance<ContagionBoilWall>().Texture;
	public override void SetStaticDefaults()
	{
		WallID.Sets.Conversion.NewWall4[Type] = true;
		Main.wallBlend[Type] = ModContent.WallType<ContagionBoilWall>();
		AddMapEntry(new Color(63, 66, 56));
		DustType = ModContent.DustType<Dusts.ContagionDust>();
	}
}