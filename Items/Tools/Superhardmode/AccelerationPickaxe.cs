using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Superhardmode;

public class AccelerationPickaxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPickaxe(400, 28, 2f, 12, 12, 6);
		Item.rare = ModContent.RarityType<Rarities.DarkGreenRarity>();
		Item.value = Item.sellPrice(0, 20, 32);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<AccelerationDrill>())
			.AddTile(TileID.TinkerersWorkbench)
			.Register();

		Recipe.Create(ModContent.ItemType<AccelerationDrill>())
			.AddIngredient(Type)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
	public override void HoldItem(Player player)
	{
		if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory && !player.controlUseItem)
		{
			SoundEngine.PlaySound(SoundID.Unlock, player.position);
			int pfix = Item.prefix;
			Item.ChangeItemType(ModContent.ItemType<AccelerationPickaxeSpeed>());
			Item.Prefix(pfix);
		}
		if (player.controlUseItem)
		{
			if (player.IsInTileInteractionRange(Player.tileTargetX, Player.tileTargetY, TileReachCheckSettings.Simple))
			{
				Tile t = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
				if (TileID.Sets.Ore[t.TileType])
				{
					ClassExtensions.VeinMine(new Point(Player.tileTargetX, Player.tileTargetY), t.TileType);
				}
			}
		}
	}
}

public class AccelerationPickaxeSpeed : ModItem
{
	public override string Texture => ModContent.GetInstance<AccelerationPickaxe>().Texture;
	public override void SetDefaults()
	{
		Item.DefaultToPickaxe(0, 28, 2f, 12, 12, 6);
		Item.rare = ModContent.RarityType<Rarities.DarkGreenRarity>();
		Item.value = Item.sellPrice(0, 20, 32);
	}
	//public override void AddRecipes()
	//{
	//    Recipe.Create(Type)
	//        .AddIngredient(ModContent.ItemType<AccelerationDrill>())
	//        .AddTile(TileID.TinkerersWorkbench)
	//        .Register();

	//    Recipe.Create(ModContent.ItemType<AccelerationDrill>())
	//        .AddIngredient(Type)
	//        .AddTile(TileID.TinkerersWorkbench)
	//        .Register();
	//}

	public override void HoldItem(Player player)
	{
		if (!Main.GamepadDisableCursorItemIcon && player.position.X / 16f - Player.tileRangeX - player.inventory[player.selectedItem].tileBoost <= Player.tileTargetX && (player.position.X + player.width) / 16f + Player.tileRangeX + player.inventory[player.selectedItem].tileBoost - 1f >= Player.tileTargetX && player.position.Y / 16f - Player.tileRangeY - player.inventory[player.selectedItem].tileBoost <= Player.tileTargetY && (player.position.Y + player.height) / 16f + Player.tileRangeY + player.inventory[player.selectedItem].tileBoost - 2f >= Player.tileTargetY)
		{
			player.cursorItemIconEnabled = true;
			Main.ItemIconCacheUpdate(Type);
		}
		if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory && !player.controlUseItem)
		{
			SoundEngine.PlaySound(SoundID.Unlock, player.position);
			int pfix = Item.prefix;
			Item.ChangeItemType(ModContent.ItemType<AccelerationPickaxe>());
			Item.Prefix(pfix);
		}
		if (player.controlUseItem && player.whoAmI == Main.myPlayer)
		{
			if (player.position.X / 16f - Player.tileRangeX - player.inventory[player.selectedItem].tileBoost <= Player.tileTargetX && (player.position.X + player.width) / 16f + Player.tileRangeX + player.inventory[player.selectedItem].tileBoost - 1f >= Player.tileTargetX && player.position.Y / 16f - Player.tileRangeY - player.inventory[player.selectedItem].tileBoost <= Player.tileTargetY && (player.position.Y + player.height) / 16f + Player.tileRangeY + player.inventory[player.selectedItem].tileBoost - 2f >= Player.tileTargetY)
			{
				Point p = player.GetModPlayer<AvalonPlayer>().MousePosition.ToTileCoordinates();
				for (int x = p.X - 1; x <= p.X + 1; x++)
				{
					for (int y = p.Y - 1; y <= p.Y + 1; y++)
					{
						player.PickTile(x, y, 400);
					}
				}
			}
		}
	}
}
