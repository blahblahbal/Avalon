using Avalon.ModSupport.MLL.Liquids;
using ModLiquidLib.ModLoader;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Items
{
	//This is an example of a modded sponge
	//Here we make this item only absorb Example Liquid using very similar logic from that of ExampleLiquidBucket
	public class BloodAbsorbantSponge : ModItem
	{
		//The SetStaticDefaults of a sponge
		public override void SetStaticDefaults()
		{
			ItemID.Sets.AlsoABuildingItem[Type] = true; //Unused, but useful to have here for both other mods and future game updates
			ItemID.Sets.DuplicationMenuToolsFilter[Type] = true;
			//ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<BloodBucket>();

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		//The SetDefaults of a sponge
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

		//Here in HoldItem we do our sponge logic
		//We use HoldItem as it is the hook/method run right before sponge logic
		//This is so no extra item logic is run inbetween this hook/method and when the normal sponge logic would occur
		public override void HoldItem(Player player)
		{
			if (!player.JustDroppedAnItem)//make sure the player hasn't dropped an item
			{
				if (player.whoAmI != Main.myPlayer)//only run this logic on clients, we sync this in multiplayer later
				{
					return;
				}
				//we check that the player's cursor is in range to suck up liquid
				if (player.noBuilding ||
					!(player.position.X / 16f - Player.tileRangeX - Item.tileBoost <= Player.tileTargetX) ||
					!((player.position.X + player.width) / 16f + Player.tileRangeX + Item.tileBoost - 1f >= Player.tileTargetX) ||
					!(player.position.Y / 16f - Player.tileRangeY - Item.tileBoost <= Player.tileTargetY) ||
					!((player.position.Y + player.height) / 16f + Player.tileRangeY + Item.tileBoost - 2f >= Player.tileTargetY))
				{
					return;
				}
				//We then set the player's cursor to be that of this item
				if (!Main.GamepadDisableCursorItemIcon)
				{
					player.cursorItemIconEnabled = true;
					Main.ItemIconCacheUpdate(Item.type);
				}
				//Make sure that the player isn't using the item
				if (!player.ItemTimeIsZero || player.itemAnimation <= 0 || !player.controlUseItem)
				{
					return;
				}
				Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
				if (tile.LiquidType == LiquidLoader.LiquidType<Blood>()) //if the cursor's liquid selected is example liquid, then run the following logic
				{
					int origType = tile.LiquidType;
					int origAmount = 0;
					//count how much liquid us nearby in a 9x9 area
					for (int i = Player.tileTargetX - 1; i <= Player.tileTargetX + 1; i++)
					{
						for (int j = Player.tileTargetY - 1; j <= Player.tileTargetY + 1; j++)
						{
							Tile tile2 = Main.tile[i, j];
							if (tile2.LiquidType == origType)
							{
								int origAmountOld = origAmount;
								origAmount = origAmountOld + tile2.LiquidAmount;
							}
						}
					}
					//if the amount of liquid is less than 100, do not suck up any liquid
					if (tile.LiquidAmount <= 0 || origAmount <= 100)
					{
						return;
					}
					//The following is the code to then suck up liquid
					//We do this by...
					int liquidType = tile.LiquidType;
					SoundEngine.PlaySound(SoundID.SplashWeak, player.position);//...playing the suck up sound
					player.ApplyItemTime(Item); //...add usetime to the item
					int liquidAmount = tile.LiquidAmount;
					tile.LiquidAmount = 0; //...set the liquid amount to 0
					tile.LiquidType = 0; //...reset the liquid type
					WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, resetFrame: false); //...update the world nearby
																										 //...if the game is in multiplayerm we then send a packet...
					if (Main.netMode == NetmodeID.MultiplayerClient)
					{
						NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
					}
					else
					{
						//...otherwise we update our game's liquid
						Liquid.AddWater(Player.tileTargetX, Player.tileTargetY);
					}

					//If liquid amount is a full tile we then remove a small amount of liquid from nearby tiles
					if (liquidAmount >= 255)
					{
						return;
					}
					//In yet another 9x9 square around the cursor
					//We check for the liquid amount, and then remove some of that liquid's count accordingly.
					for (int k = Player.tileTargetX - 1; k <= Player.tileTargetX + 1; k++)
					{
						for (int l = Player.tileTargetY - 1; l <= Player.tileTargetY + 1; l++)
						{
							if (k == Player.tileTargetX && l == Player.tileTargetY)
							{
								continue;
							}
							Tile tile3 = Main.tile[k, l];
							if (tile3.LiquidAmount <= 0)
							{
								continue;
							}
							if (tile3.LiquidType == origType)
							{
								int currentAmount = tile3.LiquidAmount;
								if (currentAmount + liquidAmount > 255)
								{
									currentAmount = 255 - liquidAmount;
								}
								liquidAmount += currentAmount;
								tile3.LiquidAmount -= (byte)currentAmount;
								tile3.LiquidType = liquidType;
								if (tile3.LiquidAmount == 0) //If the tile has been set to have no liquid, we reset the liquid's type
								{
									tile3.LiquidType = 0;
								}
								//We make sure we update nearby tiles 
								WorldGen.SquareTileFrame(k, l, resetFrame: false);
								//and update multiplayer/singleplayer accordingly
								if (Main.netMode == NetmodeID.MultiplayerClient)
								{
									NetMessage.sendWater(k, l);
								}
								else
								{
									Liquid.AddWater(k, l);
								}
							}
						}
					}
				}
			}
		}
	}
}
