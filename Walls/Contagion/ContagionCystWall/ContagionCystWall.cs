using Avalon.Tiles.Contagion.Chunkstone;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls.Contagion.ContagionCystWall;

[LegacyName("ContagionCystWall")]
public class ContagionCystWallItem : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<ContagionCystWall>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(ModContent.ItemType<ChunkstoneBlock>()).AddCondition(Condition.InGraveyard).AddTile(TileID.WorkBenches).Register();
		Recipe.Create(ModContent.ItemType<ChunkstoneBlock>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).DisableDecraft().Register();
	}
}
public class ContagionCystWall : ModWall
{
    public override void SetStaticDefaults()
    {
        WallID.Sets.Conversion.NewWall3[Type] = true;
        Main.wallHouse[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<ContagionCystWall>();
        AddMapEntry(new Color(56, 66, 59));
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}
public class ContagionCystWallUnsafe : ModWall
{
	public override string Texture => ModContent.GetInstance<ContagionCystWall>().Texture;
	public override void SetStaticDefaults()
	{
		WallID.Sets.Conversion.NewWall3[Type] = true;
		Main.wallBlend[Type] = ModContent.WallType<ContagionCystWall>();
		AddMapEntry(new Color(56, 66, 59));
		DustType = ModContent.DustType<Dusts.ContagionDust>();
	}
}
