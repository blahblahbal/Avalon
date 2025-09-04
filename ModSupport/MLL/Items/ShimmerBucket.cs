using ModLiquidLib.ID;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Items;

public class ShimmerBucket : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
		ItemID.Sets.AlsoABuildingItem[Type] = true;
		//ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.WaterBucket;
		ItemID.Sets.DuplicationMenuToolsFilter[Type] = true;
		LiquidID_TLmod.Sets.CreateLiquidBucketItem[LiquidID.Shimmer] = Type;

		CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 24;
		Item.maxStack = Item.CommonMaxStack;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.rare = ItemRarityID.Orange;
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
			Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
			if (tile.LiquidAmount >= 200)
			{
				return;
			}
			if (tile.HasUnactuatedTile)
			{
				if (Main.tileSolid[tile.TileType])
				{
					if (!Main.tileSolidTop[tile.TileType])
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
				if (tile.LiquidType != LiquidID.Shimmer)
				{
					return;
				}
			}
			SoundEngine.PlaySound(SoundID.SplashWeak, player.position);
			tile.LiquidType = LiquidID.Shimmer;
			tile.LiquidAmount = byte.MaxValue;
			WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY);
			Item.stack--;
			player.PutItemInInventoryFromItemUsage(ItemID.EmptyBucket, player.selectedItem);
			player.ApplyItemTime(Item);

			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
			}
		}
	}
}
