using Avalon.Common.Extensions;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Other;

public class CompressedExtractinationBlock : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.EverythingElse;
	}
	public override void SetDefaults()
	{
		Item.DefaultToUseable(false, 15, 15);
		Item.rare = ItemRarityID.Blue;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.SiltBlock, 100)
			.AddTile(TileID.WorkBenches).Register();

		Recipe.Create(Type)
			.AddIngredient(ItemID.SlushBlock, 100)
			.AddTile(TileID.WorkBenches).Register();
	}
	public override void HoldItem(Player player)
	{
		if (player.ItemAnimationJustStarted)
		{
			bool inrange = (player.position.X / 16f - Player.tileRangeX - player.inventory[player.selectedItem].tileBoost - player.blockRange <= Player.tileTargetX &&
				(player.position.X + player.width) / 16f + Player.tileRangeX + player.inventory[player.selectedItem].tileBoost - 1f + player.blockRange >= Player.tileTargetX &&
				player.position.Y / 16f - Player.tileRangeY - player.inventory[player.selectedItem].tileBoost - player.blockRange <= Player.tileTargetY &&
				(player.position.Y + player.height) / 16f + Player.tileRangeY + player.inventory[player.selectedItem].tileBoost - 2f + player.blockRange >= Player.tileTargetY);
			if ((Main.tile[Player.tileTargetX, Player.tileTargetY].TileType == TileID.Extractinator ||
				Main.tile[Player.tileTargetX, Player.tileTargetY].TileType == TileID.ChlorophyteExtractinator) && inrange)
			{
				MethodInfo? dynMethod = typeof(Player).GetMethod("ExtractinatorUse", BindingFlags.NonPublic | BindingFlags.Instance);
				SoundEngine.PlaySound(SoundID.Grab);
				float mult = 1f;
				if (Main.tile[Player.tileTargetX, Player.tileTargetY].TileType == TileID.ChlorophyteExtractinator)
				{
					mult *= 0.33f;
				}
				player.ApplyItemTime(Item, mult);
				for (int i = 0; i < 100; i++)
				{
					if (dynMethod != null)
						dynMethod.Invoke(player, new object[] { 0, Main.tile[Player.tileTargetX, Player.tileTargetY].TileType });
				}
				Item.stack--;
				if (Item.stack <= 0)
				{
					Item.SetDefaults();
					Item.stack = 0;
				}
			}
		}
	}
}
