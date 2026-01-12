using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Tiles.Blastplains;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModLiquidLib.ModLoader;
using ModLiquidLib.Utils.Structs;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics;
using Terraria.Graphics.Light;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.GameContent.Liquid.LiquidRenderer;

namespace Avalon.ModSupport.MLL.Liquids;

internal class GasHook : ModHook
{
	protected override void Apply()
	{
		On_Main.DoDraw += On_Main_DoDraw;
		On_Player.Update += On_Player_Update;
	}

	private void On_Player_Update(On_Player.orig_Update orig, Player self, int i)
	{
		orig.Invoke(self, i);
		if (self.GetModPlayer<AvalonPlayer>().InCaesiumGas)
		{
			self.gravDir = -1;
		}
	}

	private void On_Main_DoDraw(On_Main.orig_DoDraw orig, Main self, GameTime gameTime)
	{
		float gd = Main.gameMenu ? 0 : Main.LocalPlayer.gravDir;
		if (!Main.gameMenu && Main.LocalPlayer.GetModPlayer<AvalonPlayer>().InCaesiumGas)
		{
			Main.LocalPlayer.gravDir = 1;
		}
		orig.Invoke(self, gameTime);
		if (!Main.gameMenu && Main.LocalPlayer.GetModPlayer<AvalonPlayer>().InCaesiumGas)
		{
			Main.LocalPlayer.gravDir = -1;
		}
	}
}
public class GasSystem : ModSystem
{
	public override void ModifyTransformMatrix(ref SpriteViewMatrix Transform)
	{
		if (!Main.gameMenu && Main.LocalPlayer.GetModPlayer<AvalonPlayer>().InCaesiumGas)
		{
			//Main.GameViewMatrix.Effects = SpriteEffects.FlipVertically;
			//Main.BackgroundViewMatrix.Effects = SpriteEffects.FlipVertically;
		}
	}
}

//This liquid shows how a liquid's movement can be edited to do something seperate from the default.
//Here we showcase a liquid with upside down movement. Flowing upwards rather than downwards.
public class CaesiumGas : ModLiquid
{
	public override void SetStaticDefaults()
	{
		VISCOSITY_MASK[Type] = 200;
		WATERFALL_LENGTH[Type] = 2;
		DEFAULT_OPACITY[Type] = 1f;
		SlopeOpacity = 1f;
		WaterRippleMultiplier = 1f;
		SplashDustType = DustID.Cloud;
		SplashSound = SoundID.SplashWeak;
		AddMapEntry(new Color(71, 194, 163));
	}

	public override int LiquidMerge(int i, int j, int otherLiquid)
	{
		return ModContent.TileType<CaesiumCrystal>();
	}

	public override LightMaskMode LiquidLightMaskMode(int i, int j)
	{
		return LightMaskMode.None;
	}

	public override int ChooseWaterfallStyle(int i, int j)
	{
		return ModContent.GetInstance<CaesiumGasFall>().Slot;
	}

	public override void RetroDrawEffects(int i, int j, SpriteBatch spriteBatch, ref RetroLiquidDrawInfo drawData, float liquidAmountModified, int liquidGFXQuality)
	{
		drawData.liquidAlphaMultiplier = 1f;
	}
	public override void OnPlayerCollision(Player player)
	{
		player.GetModPlayer<AvalonPlayer>().InCaesiumGas = true;
		player.gravDir = -1;
		//player.gravity = -0.4f;
		//player.maxFallSpeed = -5f;
	}
	public override bool PlayerLiquidMovement(Player player, bool fallThrough, bool ignorePlats)
	{
		player.gravDir = -1;
		//player.velocity.Y = -3.5f;
		//if (player.controlJump)
		//{
		//	player.velocity.Y *= -1;
		//}
		return base.PlayerLiquidMovement(player, fallThrough, ignorePlats);
	}

	//This method copies over code from Liquid.Update to reverse its Y movement from down to up.
	//UpdateLiquid hook also returns false as we don't want the vanilla movement effecting this liquid
	public override bool UpdateLiquid(int i, int j, Liquid liquid)
	{
		Tile left = Main.tile[i - 1, j];
		Tile right = Main.tile[i + 1, j];
		Tile up = Main.tile[i, j - 1];
		Tile down = Main.tile[i, j + 1];
		Tile tile = Main.tile[i, j];
		byte liquidAmount = tile.LiquidAmount;
		float transferAmount = 0f;

		bool? evaporationHook = LiquidLoader.EvaporatesInHell(i, j, Type);
		if (j > Main.UnderworldLayer && evaporationHook != null && !(bool)evaporationHook && tile.LiquidAmount > 0)
		{
			byte evaporationAmount = 2;
			if (tile.LiquidAmount < evaporationAmount)
			{
				evaporationAmount = tile.LiquidAmount;
			}
			tile.LiquidAmount -= evaporationAmount;
		}
		if (tile.LiquidAmount == 0)
		{
			liquid.kill = 999;
			return false;
		}

		Liquid.LiquidCheck(liquid.x, liquid.y, Type);
		if (!Liquid.quickFall)
		{
			int fallDelay = 0;
			fallDelay = LiquidLoader.GetLiquid(Type).FallDelay;
			if (LiquidLoader.LiquidEditingFallDelay(Type) != null)
			{
				fallDelay = (int)LiquidLoader.LiquidEditingFallDelay(Type);
			}
			if (liquid.delay < fallDelay)
			{
				liquid.delay++;
				return false;
			}
			liquid.delay = 0;
		}

		if ((!up.HasTile || !Main.tileSolid[up.TileType] || Main.tileSolidTop[up.TileType]) && (up.LiquidAmount <= 0 || up.LiquidType == Type) && up.LiquidAmount < byte.MaxValue)
		{
			bool hasFullAmount = false;
			transferAmount = 255 - up.LiquidAmount;
			if (transferAmount > tile.LiquidAmount)
			{
				transferAmount = tile.LiquidAmount;
			}
			if (transferAmount == 1f && tile.LiquidAmount == byte.MaxValue)
			{
				hasFullAmount = true;
			}
			if (!hasFullAmount)
			{
				tile.LiquidAmount -= (byte)transferAmount;
			}
			up.LiquidAmount += (byte)transferAmount;
			up.LiquidType = Type;
			Liquid.AddWater(i, j - 1);
			up.SkipLiquid = true;
			tile.SkipLiquid = true;
			if (Liquid.quickSettle && tile.LiquidAmount > 250)
			{
				tile.LiquidAmount = byte.MaxValue;
			}
			else if (!hasFullAmount)
			{
				Liquid.AddWater(i, j);
				Liquid.AddWater(i, j);
			}
		}
		if (tile.LiquidAmount > 0)
		{
			bool moveLeft = true;
			bool moveRight = true;
			bool moveFarLeft = true;
			bool moveFarRight = true;
			if (left.HasUnactuatedTile && Main.tileSolid[left.TileType] && !Main.tileSolidTop[left.TileType])
			{
				moveLeft = false;
			}
			else if (left.LiquidAmount > 0 && left.LiquidType != Type)
			{
				moveLeft = false;
			}
			else if (Main.tile[i - 2, j].HasUnactuatedTile && Main.tileSolid[Main.tile[i - 2, j].TileType] && !Main.tileSolidTop[Main.tile[i - 2, j].TileType])
			{
				moveFarLeft = false;
			}
			else if (Main.tile[i - 2, j].LiquidAmount == 0)
			{
				moveFarLeft = false;
			}
			else if (Main.tile[i - 2, j].LiquidAmount > 0 && Main.tile[i - 2, j].LiquidType != Type)
			{
				moveFarLeft = false;
			}
			if (right.HasUnactuatedTile && Main.tileSolid[right.TileType] && !Main.tileSolidTop[right.TileType])
			{
				moveRight = false;
			}
			else if (right.LiquidAmount > 0 && right.LiquidType != Type)
			{
				moveRight = false;
			}
			else if (Main.tile[i + 2, j].HasUnactuatedTile && Main.tileSolid[Main.tile[i + 2, j].TileType] && !Main.tileSolidTop[Main.tile[i + 2, j].TileType])
			{
				moveFarRight = false;
			}
			else if (Main.tile[i + 2, j].LiquidAmount == 0)
			{
				moveFarRight = false;
			}
			else if (Main.tile[i + 2, j].LiquidAmount > 0 && Main.tile[i + 2, j].LiquidType != Type)
			{
				moveFarRight = false;
			}
			int amountOffset = 0;
			if (tile.LiquidAmount < 3)
			{
				amountOffset = -1;
			}
			if (tile.LiquidAmount > 250)
			{
				moveFarLeft = false;
				moveFarRight = false;
			}
			if (moveLeft && moveRight)
			{
				if (moveFarLeft && moveFarRight)
				{
					bool moveFurtherLeft = true;
					bool moveFurtherRight = true;
					if (Main.tile[i - 3, j].HasUnactuatedTile && Main.tileSolid[Main.tile[i - 3, j].TileType] && !Main.tileSolidTop[Main.tile[i - 3, j].TileType])
					{
						moveFurtherLeft = false;
					}
					else if (Main.tile[i - 3, j].LiquidAmount == 0)
					{
						moveFurtherLeft = false;
					}
					else if (Main.tile[i - 3, j].LiquidType != Type)
					{
						moveFurtherLeft = false;
					}
					if (Main.tile[i + 3, j].HasUnactuatedTile && Main.tileSolid[Main.tile[i + 3, j].TileType] && !Main.tileSolidTop[Main.tile[i + 3, j].TileType])
					{
						moveFurtherRight = false;
					}
					else if (Main.tile[i + 3, j].LiquidAmount == 0)
					{
						moveFurtherRight = false;
					}
					else if (Main.tile[i + 3, j].LiquidType != Type)
					{
						moveFurtherRight = false;
					}
					if (moveFurtherLeft && moveFurtherRight)
					{
						transferAmount = left.LiquidAmount + right.LiquidAmount + Main.tile[i - 2, j].LiquidAmount + Main.tile[i + 2, j].LiquidAmount + Main.tile[i - 3, j].LiquidAmount + Main.tile[i + 3, j].LiquidAmount + tile.LiquidAmount + amountOffset;
						transferAmount = (float)Math.Round(transferAmount / 7f);
						int liquidAmountCount = 0;
						left.LiquidType = Type;
						if (left.LiquidAmount != (byte)transferAmount)
						{
							left.LiquidAmount = (byte)transferAmount;
							Liquid.AddWater(i - 1, j);
						}
						else
						{
							liquidAmountCount++;
						}
						right.LiquidType = Type;
						if (right.LiquidAmount != (byte)transferAmount)
						{
							right.LiquidAmount = (byte)transferAmount;
							Liquid.AddWater(i + 1, j);
						}
						else
						{
							liquidAmountCount++;
						}
						Tile farLeftTile = Main.tile[i - 2, j];
						farLeftTile.LiquidType = Type;
						if (Main.tile[i - 2, j].LiquidAmount != (byte)transferAmount)
						{
							Main.tile[i - 2, j].LiquidAmount = (byte)transferAmount;
							Liquid.AddWater(i - 2, j);
						}
						else
						{
							liquidAmountCount++;
						}
						Tile farRightTile = Main.tile[i + 2, j];
						farRightTile.LiquidType = Type;
						if (Main.tile[i + 2, j].LiquidAmount != (byte)transferAmount)
						{
							Main.tile[i + 2, j].LiquidAmount = (byte)transferAmount;
							Liquid.AddWater(i + 2, j);
						}
						else
						{
							liquidAmountCount++;
						}
						Tile furtherLeftTile = Main.tile[i - 3, j];
						furtherLeftTile.LiquidType = Type;
						if (Main.tile[i - 3, j].LiquidAmount != (byte)transferAmount)
						{
							Main.tile[i - 3, j].LiquidAmount = (byte)transferAmount;
							Liquid.AddWater(i - 3, j);
						}
						else
						{
							liquidAmountCount++;
						}
						Tile furtherRightTile = Main.tile[i + 3, j];
						furtherRightTile.LiquidType = Type;
						if (Main.tile[i + 3, j].LiquidAmount != (byte)transferAmount)
						{
							Main.tile[i + 3, j].LiquidAmount = (byte)transferAmount;
							Liquid.AddWater(i + 3, j);
						}
						else
						{
							liquidAmountCount++;
						}
						if (left.LiquidAmount != (byte)transferAmount || tile.LiquidAmount != (byte)transferAmount)
						{
							Liquid.AddWater(i - 1, j);
						}
						if (right.LiquidAmount != (byte)transferAmount || tile.LiquidAmount != (byte)transferAmount)
						{
							Liquid.AddWater(i + 1, j);
						}
						if (Main.tile[i - 2, j].LiquidAmount != (byte)transferAmount || tile.LiquidAmount != (byte)transferAmount)
						{
							Liquid.AddWater(i - 2, j);
						}
						if (Main.tile[i + 2, j].LiquidAmount != (byte)transferAmount || tile.LiquidAmount != (byte)transferAmount)
						{
							Liquid.AddWater(i + 2, j);
						}
						if (Main.tile[i - 3, j].LiquidAmount != (byte)transferAmount || tile.LiquidAmount != (byte)transferAmount)
						{
							Liquid.AddWater(i - 3, j);
						}
						if (Main.tile[i + 3, j].LiquidAmount != (byte)transferAmount || tile.LiquidAmount != (byte)transferAmount)
						{
							Liquid.AddWater(i + 3, j);
						}
						if (liquidAmountCount != 6 || up.LiquidAmount <= 0)
						{
							tile.LiquidAmount = (byte)transferAmount;
						}
					}
					else
					{
						int liquidAmountCount = 0;
						transferAmount = left.LiquidAmount + right.LiquidAmount + Main.tile[i - 2, j].LiquidAmount + Main.tile[i + 2, j].LiquidAmount + tile.LiquidAmount + amountOffset;
						transferAmount = (float)Math.Round(transferAmount / 5f);
						left.LiquidType = Type;
						if (left.LiquidAmount != (byte)transferAmount)
						{
							left.LiquidAmount = (byte)transferAmount;
							Liquid.AddWater(i - 1, j);
						}
						else
						{
							liquidAmountCount++;
						}
						right.LiquidType = Type;
						if (right.LiquidAmount != (byte)transferAmount)
						{
							right.LiquidAmount = (byte)transferAmount;
							Liquid.AddWater(i + 1, j);
						}
						else
						{
							liquidAmountCount++;
						}
						Tile farLeftTile = Main.tile[i - 2, j];
						farLeftTile.LiquidType = Type;
						if (Main.tile[i - 2, j].LiquidAmount != (byte)transferAmount)
						{
							Main.tile[i - 2, j].LiquidAmount = (byte)transferAmount;
							Liquid.AddWater(i - 2, j);
						}
						else
						{
							liquidAmountCount++;
						}
						Tile farRightTile = Main.tile[i + 2, j];
						farRightTile.LiquidType = Type;
						if (Main.tile[i + 2, j].LiquidAmount != (byte)transferAmount)
						{
							Main.tile[i + 2, j].LiquidAmount = (byte)transferAmount;
							Liquid.AddWater(i + 2, j);
						}
						else
						{
							liquidAmountCount++;
						}
						if (left.LiquidAmount != (byte)transferAmount || tile.LiquidAmount != (byte)transferAmount)
						{
							Liquid.AddWater(i - 1, j);
						}
						if (right.LiquidAmount != (byte)transferAmount || tile.LiquidAmount != (byte)transferAmount)
						{
							Liquid.AddWater(i + 1, j);
						}
						if (Main.tile[i - 2, j].LiquidAmount != (byte)transferAmount || tile.LiquidAmount != (byte)transferAmount)
						{
							Liquid.AddWater(i - 2, j);
						}
						if (Main.tile[i + 2, j].LiquidAmount != (byte)transferAmount || tile.LiquidAmount != (byte)transferAmount)
						{
							Liquid.AddWater(i + 2, j);
						}
						if (liquidAmountCount != 4 || down.LiquidAmount <= 0)
						{
							tile.LiquidAmount = (byte)transferAmount;
						}
					}
				}
				else if (moveFarLeft)
				{
					transferAmount = left.LiquidAmount + right.LiquidAmount + Main.tile[i - 2, j].LiquidAmount + tile.LiquidAmount + amountOffset;
					transferAmount = (float)Math.Round(transferAmount / 4f);
					left.LiquidType = Type;
					if (left.LiquidAmount != (byte)transferAmount || tile.LiquidAmount != (byte)transferAmount)
					{
						left.LiquidAmount = (byte)transferAmount;
						Liquid.AddWater(i - 1, j);
					}
					right.LiquidType = Type;
					if (right.LiquidAmount != (byte)transferAmount || tile.LiquidAmount != (byte)transferAmount)
					{
						right.LiquidAmount = (byte)transferAmount;
						Liquid.AddWater(i + 1, j);
					}
					Tile farLeftTile = Main.tile[i - 2, j];
					farLeftTile.LiquidType = Type;
					if (Main.tile[i - 2, j].LiquidAmount != (byte)transferAmount || tile.LiquidAmount != (byte)transferAmount)
					{
						Main.tile[i - 2, j].LiquidAmount = (byte)transferAmount;
						Liquid.AddWater(i - 2, j);
					}
					tile.LiquidAmount = (byte)transferAmount;
				}
				else if (moveFarRight)
				{
					transferAmount = left.LiquidAmount + right.LiquidAmount + Main.tile[i + 2, j].LiquidAmount + tile.LiquidAmount + amountOffset;
					transferAmount = (float)Math.Round(transferAmount / 4f);
					left.LiquidType = Type;
					if (left.LiquidAmount != (byte)transferAmount || tile.LiquidAmount != (byte)transferAmount)
					{
						left.LiquidAmount = (byte)transferAmount;
						Liquid.AddWater(i - 1, j);
					}
					right.LiquidType = Type;
					if (right.LiquidAmount != (byte)transferAmount || tile.LiquidAmount != (byte)transferAmount)
					{
						right.LiquidAmount = (byte)transferAmount;
						Liquid.AddWater(i + 1, j);
					}
					Tile farRightTile = Main.tile[i + 2, j];
					farRightTile.LiquidType = Type;
					if (Main.tile[i + 2, j].LiquidAmount != (byte)transferAmount || tile.LiquidAmount != (byte)transferAmount)
					{
						Main.tile[i + 2, j].LiquidAmount = (byte)transferAmount;
						Liquid.AddWater(i + 2, j);
					}
					tile.LiquidAmount = (byte)transferAmount;
				}
				else
				{
					transferAmount = left.LiquidAmount + right.LiquidAmount + tile.LiquidAmount + amountOffset;
					transferAmount = (float)Math.Round(transferAmount / 3f);
					if (transferAmount == 254f && WorldGen.genRand.NextBool(30))
					{
						transferAmount = 255f;
					}
					left.LiquidType = Type;
					if (left.LiquidAmount != (byte)transferAmount)
					{
						left.LiquidAmount = (byte)transferAmount;
						Liquid.AddWater(i - 1, j);
					}
					right.LiquidType = Type;
					if (right.LiquidAmount != (byte)transferAmount)
					{
						right.LiquidAmount = (byte)transferAmount;
						Liquid.AddWater(i + 1, j);
					}
					tile.LiquidAmount = (byte)transferAmount;
				}
			}
			else if (moveLeft)
			{
				transferAmount = left.LiquidAmount + tile.LiquidAmount + amountOffset;
				transferAmount = (float)Math.Round(transferAmount / 2f);
				if (left.LiquidAmount != (byte)transferAmount)
				{
					left.LiquidAmount = (byte)transferAmount;
				}
				left.LiquidType = Type;
				if (tile.LiquidAmount != (byte)transferAmount || left.LiquidAmount != (byte)transferAmount)
				{
					Liquid.AddWater(i - 1, j);
				}
				tile.LiquidAmount = (byte)transferAmount;
			}
			else if (moveRight)
			{
				transferAmount = right.LiquidAmount + tile.LiquidAmount + amountOffset;
				transferAmount = (float)Math.Round(transferAmount / 2f);
				if (right.LiquidAmount != (byte)transferAmount)
				{
					right.LiquidAmount = (byte)transferAmount;
				}
				right.LiquidType = Type;
				if (tile.LiquidAmount != (byte)transferAmount || right.LiquidAmount != (byte)transferAmount)
				{
					Liquid.AddWater(i + 1, j);
				}
				tile.LiquidAmount = (byte)transferAmount;
			}
		}
		if (tile.LiquidAmount != liquidAmount)
		{
			if (tile.LiquidAmount == 254 && liquidAmount == byte.MaxValue)
			{
				if (Liquid.quickSettle)
				{
					tile.LiquidAmount = byte.MaxValue;
					liquid.kill++;
				}
				else
				{
					liquid.kill++;
				}
			}
			else
			{
				Liquid.AddWater(i, j + 1);
				liquid.kill = 0;
			}
		}
		else
		{
			liquid.kill++;
		}
		return false;
	}

	//When loading or creating a world, liquids move differently that when updating
	//Here we edit the Settle Liquid movement to move upwards.
	//This method copies over code from liquid.SettleWaterAt and changing its y movement positioning from down to up
	//We return false here too, to stop normal liquid settling code from being executed
	public override bool SettleLiquidMovement(int i, int j)
	{
		Tile tile = Main.tile[i, j];
		int x = i;
		int y = j;
		int liquidAmount = tile.LiquidAmount;
		tile.LiquidAmount = 0;
		bool flag3 = true;
		while (true)
		{
			Tile up = Main.tile[x, y - 1];
			while (y > 5 && up.LiquidAmount == 0 && (!up.HasUnactuatedTile || !Main.tileSolid[up.TileType] || Main.tileSolidTop[up.TileType]))
			{
				y--;
				flag3 = false;
				up = Main.tile[x, y - 1];
			}
			int xNegReverser = -1;
			int xOffset = 0;
			int oldXNegReverser = -1;
			int oldXOffset = 0;
			bool forceFlowLeft = false;
			bool forceFlowRight = false;
			bool moveUpwards = false;
			while (true)
			{
				if (Main.tile[x + xOffset * xNegReverser, y].LiquidAmount == 0)
				{
					oldXNegReverser = xNegReverser;
					oldXOffset = xOffset;
				}
				if (xNegReverser == -1 && x + xOffset * xNegReverser < 5)
				{
					forceFlowRight = true;
				}
				else if (xNegReverser == 1 && x + xOffset * xNegReverser > Main.maxTilesX - 5)
				{
					forceFlowLeft = true;
				}
				up = Main.tile[x + xOffset * xNegReverser, y - 1];
				if (up.LiquidAmount != 0 && up.LiquidAmount != byte.MaxValue && up.LiquidType == Type)
				{
					int num8 = 255 - up.LiquidAmount;
					if (num8 > liquidAmount)
					{
						num8 = liquidAmount;
					}
					up.LiquidAmount += (byte)num8;
					liquidAmount -= num8;
					if (liquidAmount == 0)
					{
						break;
					}
				}
				if (y > 5 && up.LiquidAmount == 0 && (!up.HasUnactuatedTile || !Main.tileSolid[up.TileType] || Main.tileSolidTop[up.TileType]))
				{
					moveUpwards = true;
					break;
				}
				Tile xOffsetTile = Main.tile[x + (xOffset + 1) * xNegReverser, y];
				if (xOffsetTile.LiquidAmount != 0 && (!flag3 || xNegReverser != 1) || xOffsetTile.HasUnactuatedTile && Main.tileSolid[xOffsetTile.TileType] && !Main.tileSolidTop[xOffsetTile.TileType])
				{
					if (xNegReverser == 1)
					{
						forceFlowLeft = true;
					}
					else
					{
						forceFlowRight = true;
					}
				}
				if (forceFlowRight && forceFlowLeft)
				{
					break;
				}
				if (forceFlowLeft)
				{
					xNegReverser = -1;
					xOffset++;
				}
				else if (forceFlowRight)
				{
					if (xNegReverser == 1)
					{
						xOffset++;
					}
					xNegReverser = 1;
				}
				else
				{
					if (xNegReverser == 1)
					{
						xOffset++;
					}
					xNegReverser = -xNegReverser;
				}
			}
			x += oldXOffset * oldXNegReverser;
			if (liquidAmount == 0 || !moveUpwards)
			{
				break;
			}
			y--;
		}
		Tile finalPositionTile = Main.tile[x, y];
		finalPositionTile.LiquidAmount = (byte)liquidAmount;
		finalPositionTile.LiquidType = Type;
		return false;
	}

	//Copies the normal mod splash code but makes the dust splash downwards rather than upwards
	#region upside down splash
	public override bool OnPlayerSplash(Player player, bool isEnter)
	{
		for (int j = 0; j < 20; j++)
		{
			int dust = Dust.NewDust(new Vector2(player.position.X - 6f, player.position.Y - player.height / 2 + 8f), player.width + 12, 24, SplashDustType);
			Main.dust[dust].velocity.Y -= -2f;
			Main.dust[dust].velocity.X *= 2.5f;
			Main.dust[dust].scale = 1.3f;
			Main.dust[dust].alpha = 100;
			Main.dust[dust].noGravity = true;
		}
		SoundEngine.PlaySound(SplashSound, player.position);
		return false;
	}

	public override bool OnNPCSplash(NPC npc, bool isEnter)
	{
		for (int j = 0; j < 10; j++)
		{
			int dust = Dust.NewDust(new Vector2(npc.position.X - 6f, npc.position.Y - npc.height / 2 + 8f), npc.width + 12, 24, SplashDustType);
			Main.dust[dust].velocity.Y -= -2f;
			Main.dust[dust].velocity.X *= 2.5f;
			Main.dust[dust].scale = 1.3f;
			Main.dust[dust].alpha = 100;
			Main.dust[dust].noGravity = true;
		}
		if (npc.aiStyle != NPCAIStyleID.Slime &&
				npc.type != NPCID.BlueSlime && npc.type != NPCID.MotherSlime && npc.type != NPCID.IceSlime && npc.type != NPCID.LavaSlime &&
				npc.type != NPCID.Mouse &&
				npc.aiStyle != NPCAIStyleID.GiantTortoise &&
				!npc.noGravity)
		{
			SoundEngine.PlaySound(SplashSound, npc.position);
		}
		return false;
	}

	public override bool OnProjectileSplash(Projectile proj, bool isEnter)
	{
		for (int j = 0; j < 10; j++)
		{
			int dust = Dust.NewDust(new Vector2(proj.position.X - 6f, proj.position.Y - proj.height / 2 + 8f), proj.width + 12, 24, SplashDustType);
			Main.dust[dust].velocity.Y -= -2f;
			Main.dust[dust].velocity.X *= 2.5f;
			Main.dust[dust].scale = 1.3f;
			Main.dust[dust].alpha = 100;
			Main.dust[dust].noGravity = true;
		}
		SoundEngine.PlaySound(SplashSound, proj.position);
		return false;
	}

	public override bool OnItemSplash(Item item, bool isEnter)
	{
		for (int j = 0; j < 5; j++)
		{
			int dust = Dust.NewDust(new Vector2(item.position.X - 6f, item.position.Y - item.height / 2 + 8f), item.width + 12, 24, SplashDustType);
			Main.dust[dust].velocity.Y -= -2f;
			Main.dust[dust].velocity.X *= 2.5f;
			Main.dust[dust].scale = 1.3f;
			Main.dust[dust].alpha = 100;
			Main.dust[dust].noGravity = true;
		}
		SoundEngine.PlaySound(SplashSound, item.position);
		return false;
	}
	#endregion
}
