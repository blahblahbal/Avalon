using Avalon.Tiles.Contagion.Chunkstone;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls.Contagion.ContagionMouldWall;
[LegacyName("ContagionMouldWall")]
public class ContagionMouldWallItem : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<ContagionMouldWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(ModContent.ItemType<ChunkstoneBlock>()).AddCondition(Condition.InGraveyard).AddTile(TileID.WorkBenches).Register();
		Recipe.Create(ModContent.ItemType<ChunkstoneBlock>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).DisableDecraft().Register();
	}
}
public class ContagionMouldWall : ModWall
{
    public override void SetStaticDefaults()
    {
        WallID.Sets.Conversion.NewWall2[Type] = true;
        Main.wallHouse[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<ContagionMouldWall>();
        AddMapEntry(new Color(66, 77, 50));
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}
public class ContagionMouldWallUnsafe : ModWall
{
	public override string Texture => ModContent.GetInstance<ContagionMouldWall>().Texture;
	public override void SetStaticDefaults()
	{
		WallID.Sets.Conversion.NewWall2[Type] = true;
		Main.wallBlend[Type] = ModContent.WallType<ContagionMouldWall>();
		AddMapEntry(new Color(66, 77, 50));
		DustType = ModContent.DustType<Dusts.ContagionDust>();
	}
}