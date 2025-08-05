using Avalon.ModSupport.MLL.Liquids;
using ModLiquidLib.ModLoader;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Items
{
	public class BottomlessAcidBucket : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.AlsoABuildingItem[Type] = true;
			ItemID.Sets.DuplicationMenuToolsFilter[Type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 12;
			Item.useTime = 5;
			Item.width = 20;
			Item.height = 20;
			Item.autoReuse = true;
			Item.rare = ItemRarityID.Lime;
			Item.value = Item.sellPrice(0, 10);
			Item.tileBoost += 2;
		}

		public override void HoldItem(Player player)
		{
			if (!player.JustDroppedAnItem)
			{
				if (player.whoAmI != Main.myPlayer)
				{
					return;
				}
				if (player.noBuilding ||
					!(player.position.X / 16f - Player.tileRangeX - Item.tileBoost <= Player.tileTargetX) ||
					!((player.position.X + player.width) / 16f + Player.tileRangeX + Item.tileBoost - 1f >= Player.tileTargetX) ||
					!(player.position.Y / 16f - Player.tileRangeY - Item.tileBoost <= Player.tileTargetY) ||
					!((player.position.Y + player.height) / 16f + Player.tileRangeY + Item.tileBoost - 2f >= Player.tileTargetY))
				{
					return;
				}

				if (!Main.GamepadDisableCursorItemIcon)
				{
					player.cursorItemIconEnabled = true;
					Main.ItemIconCacheUpdate(Item.type);
				}
				if (!player.ItemTimeIsZero || player.itemAnimation <= 0 || !player.controlUseItem)
				{
					return;
				}
				Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
				if (tile.LiquidAmount >= 200)
				{
					return;
				}
				if (tile.HasUnactuatedTile)
				{
					bool[] tileSolid = Main.tileSolid;
					if (tileSolid[tile.TileType])
					{
						bool[] tileSolidTop = Main.tileSolidTop;
						if (!tileSolidTop[tile.TileType])
						{
							if (tile.TileType != TileID.Grate)
							{
								return;
							}
						}
					}
				}

				if (tile.LiquidAmount != 0)
				{
					if (tile.LiquidType != LiquidLoader.LiquidType<Acid>())
					{
						return;
					}
				}
				SoundEngine.PlaySound(SoundID.SplashWeak, player.position);
				tile.LiquidType = LiquidLoader.LiquidType<Acid>();
				tile.LiquidAmount = byte.MaxValue;
				WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY);
				player.ApplyItemTime(Item);
				if (Main.netMode == NetmodeID.MultiplayerClient)
				{
					NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
				}
			}
		}
	}
}
