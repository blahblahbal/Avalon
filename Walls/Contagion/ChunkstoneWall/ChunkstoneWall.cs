using Avalon.Dusts;
using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls.Contagion.ChunkstoneWall;
[LegacyName("ChunkstoneWall")]
public class ChunkstoneWallItem : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<ChunkstoneWallSafe>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(ModContent.ItemType<ChunkstoneBlock>()).AddCondition(Condition.InGraveyard).AddTile(TileID.WorkBenches).Register();
		Recipe.Create(ModContent.ItemType<ChunkstoneBlock>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).DisableDecraft().Register();
	}
}
public class ChunkstoneWall : ModWall
{
	public override void SetStaticDefaults()
	{
		WallID.Sets.Conversion.Stone[Type] = true;
		WallID.Sets.CannotBeReplacedByWallSpread[Type] = true;
		AddMapEntry(new Color(38, 49, 33));
		DustType = ModContent.DustType<ContagionDust>();
	}
}
public class ChunkstoneWallSafe : ModWall
{
	public override string Texture => ModContent.GetInstance<ChunkstoneWall>().Texture;
	public override void SetStaticDefaults()
	{
		Main.wallHouse[Type] = true;
		AddMapEntry(new Color(38, 49, 33));
		DustType = ModContent.DustType<ContagionDust>();
	}
}
