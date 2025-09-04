using ModLiquidLib.Hooks;
using ModLiquidLib.ModLoader;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;

namespace Avalon.ModSupport.MLL;
public static class MLLSystems
{
	public static Recipe AddLiquid(this Recipe recipe, int liquidID)
	{
		recipe.Conditions.Add(new Condition(Language.GetOrRegister($"Mods.Avalon.Liquids.{LiquidLoader.GetLiquid(liquidID).Name}.RecipeEntry"), LiquidLoader.NearLiquid(liquidID).Predicate));

		return recipe;
	}
	public static Recipe AddLiquid<T>(this Recipe recipe) where T : ModLiquid
	{
		recipe.Conditions.Add(new Condition(Language.GetOrRegister($"Mods.Avalon.Liquids.{typeof(T).Name}.RecipeEntry"), LiquidLoader.NearLiquid(LiquidLoader.LiquidType<T>()).Predicate));

		return recipe;
	}
	public static void PourBucket(Item item, Player player, int liquidType, bool largePour, bool overwriteLiquids = false, bool bottomless = false)
	{
		bool placed = false;
		int radiusMod = largePour ? 1 : 0;
		for (int i = Player.tileTargetX - radiusMod; i <= Player.tileTargetX + radiusMod; i++)
		{
			bool filledLiquidAbove = false; // when right clicking, tries to ensure that lower liquid tiles get entirely filled in, so the liquid doesn't settle and require more filling
			for (int j = Player.tileTargetY - radiusMod; j <= Player.tileTargetY + radiusMod; j++)
			{
				Tile tile = Main.tile[i, j];
				if (tile.LiquidAmount >= 200 && !filledLiquidAbove)
				{
					if (overwriteLiquids)
					{
						if (tile.LiquidType == liquidType)
						{
							continue;
						}
					}
					else
					{
						continue;
					}
				}
				if (tile.HasUnactuatedTile)
				{
					if (Main.tileSolid[tile.TileType])
					{
						if (!Main.tileSolidTop[tile.TileType])
						{
							if (tile.TileType != TileID.Grate)
							{
								filledLiquidAbove = false;
								continue;
							}
						}
					}
				}

				if (tile.LiquidAmount != 0)
				{
					if (tile.LiquidType != liquidType)
					{
						if (overwriteLiquids)
						{
							LiquidHooks.PlayLiquidChangeSound(i, j, liquidType, tile.LiquidType);
						}
						else
						{
							continue;
						}
					}
				}
				tile.LiquidType = liquidType;
				tile.LiquidAmount = byte.MaxValue;
				WorldGen.SquareTileFrame(i, j);
				if (!bottomless)
				{
					item.stack--;
					player.PutItemInInventoryFromItemUsage(ItemID.EmptyBucket, player.selectedItem);
				}
				player.ApplyItemTime(item);
				if (Main.netMode == NetmodeID.MultiplayerClient)
				{
					NetMessage.sendWater(i, j);
				}
				placed = true;
				filledLiquidAbove = true;
			}
		}
		if (placed)
		{
			SoundEngine.PlaySound(SoundID.SplashWeak, player.position);
		}
	}
	public static void SpongeAbsorb(Item item, Player player, Tile tile, int origType, bool largeSuck, bool anyLiquid = false)
	{
		//The following is the code to then suck up liquid
		//We do this by...
		int liquidType = tile.LiquidType;
		SoundEngine.PlaySound(SoundID.SplashWeak, player.position);//...playing the suck up sound
		player.ApplyItemTime(item); //...add usetime to the item
		int liquidAmount = tile.LiquidAmount;
		tile.LiquidAmount = 0; //...set the liquid amount to 0
		tile.LiquidType = 0; //...reset the liquid type
		WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, resetFrame: false); //...update the world nearby
		if (Main.netMode == NetmodeID.MultiplayerClient)
		{
			//...if the game is in multiplayer we then send a packet...
			NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
		}
		else
		{
			//...otherwise we update our game's liquid
			Liquid.AddWater(Player.tileTargetX, Player.tileTargetY);
		}

		//If liquid amount is a full tile we then remove a small amount of liquid from nearby tiles
		if (liquidAmount >= 255 && !largeSuck)
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
				if (tile3.LiquidType == origType || anyLiquid)
				{
					if (largeSuck)
					{
						// if right clicking, suck up the entirety of the liquid in surrounding tiles
						tile3.LiquidAmount = 0;
						tile3.LiquidType = 0;
					}
					else
					{
						// left click behaviour
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