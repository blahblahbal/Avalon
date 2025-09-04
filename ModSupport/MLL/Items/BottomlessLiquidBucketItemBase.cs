using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Items;
public abstract class BottomlessLiquidBucketItemBase : ModItem
{
	public abstract int LiquidType();
	public virtual bool OverwriteLiquids => false;
	public override void SetStaticDefaults()
	{
		ItemID.Sets.AlsoABuildingItem[Type] = true;
		ItemID.Sets.DuplicationMenuToolsFilter[Type] = true;

		CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
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
	public override bool AltFunctionUse(Player player)
	{
		return true;
	}
	public override void HoldItem(Player player)
	{
		if (!player.JustDroppedAnItem)
		{
			if (player.whoAmI != Main.myPlayer)
			{
				return;
			}
			if (player.noBuilding || !player.IsTargetTileInItemRange(Item))
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
			//Pour(player, player.altFunctionUse == 2);
			MLLSystems.PourBucket(Item, player, LiquidType(), player.altFunctionUse == 2, OverwriteLiquids);
		}
	}
	//public void Pour(Player player, bool largePour)
	//{
	//	bool placed = false;
	//	int radiusMod = largePour ? 1 : 0;
	//	for (int i = Player.tileTargetX - radiusMod; i <= Player.tileTargetX + radiusMod; i++)
	//	{
	//		bool filledLiquidAbove = false; // when right clicking, tries to ensure that lower liquid tiles get entirely filled in, so the liquid doesn't settle and require more filling
	//		for (int j = Player.tileTargetY - radiusMod; j <= Player.tileTargetY + radiusMod; j++)
	//		{
	//			Tile tile = Main.tile[i, j];
	//			if (tile.LiquidAmount >= 200 && !filledLiquidAbove)
	//			{
	//				if (OverwriteLiquids)
	//				{
	//					if (tile.LiquidType == LiquidType())
	//					{
	//						continue;
	//					}
	//				}
	//				else
	//				{
	//					continue;
	//				}
	//			}
	//			if (tile.HasUnactuatedTile)
	//			{
	//				if (Main.tileSolid[tile.TileType])
	//				{
	//					if (!Main.tileSolidTop[tile.TileType])
	//					{
	//						if (tile.TileType != TileID.Grate)
	//						{
	//							filledLiquidAbove = false;
	//							continue;
	//						}
	//					}
	//				}
	//			}

	//			if (tile.LiquidAmount != 0)
	//			{
	//				if (tile.LiquidType != LiquidType())
	//				{
	//					if (OverwriteLiquids)
	//					{
	//						LiquidHooks.PlayLiquidChangeSound(i, j, LiquidType(), tile.LiquidType);
	//					}
	//					else
	//					{
	//						continue;
	//					}
	//				}
	//			}
	//			tile.LiquidType = LiquidType();
	//			tile.LiquidAmount = byte.MaxValue;
	//			WorldGen.SquareTileFrame(i, j);
	//			player.ApplyItemTime(Item);
	//			if (Main.netMode == NetmodeID.MultiplayerClient)
	//			{
	//				NetMessage.sendWater(i, j);
	//			}
	//			placed = true;
	//			filledLiquidAbove = true;
	//		}
	//	}
	//	if (placed)
	//	{
	//		SoundEngine.PlaySound(SoundID.SplashWeak, player.position);
	//	}
	//}
}
