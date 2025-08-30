using Avalon.Common;
using Avalon.Common.Players;
using Avalon.ModSupport.MLL.Buffs;
using Avalon.ModSupport.MLL.Dusts;
using Avalon.Tiles;
using Avalon.Tiles.Contagion;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModLiquidLib.Hooks;
using ModLiquidLib.ModLoader;
using ModLiquidLib.Utils.Structs;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics;
using Terraria.Graphics.Light;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static ModLiquidLib.ModLiquidLib;

namespace Avalon.ModSupport.MLL.Liquids;

internal class Acid : ModLiquid
{
	public override void SetStaticDefaults()
	{
		VisualViscosity = 160;
		LiquidFallLength = 6;
		DefaultOpacity = 0.85f;
		SlopeOpacity = 1f;
		WaterRippleMultiplier = 0.6f;
		SplashDustType = ModContent.DustType<AcidLiquidSplash>();
		SplashSound = SoundID.SplashWeak;
		FallDelay = 2; //The delay when liquids are falling. Liquids will wait this extra amount of frames before falling again.
		ChecksForDrowning = false; //If the player can drown in this liquid
		AllowEmitBreathBubbles = false; //Bubbles will come out of the player's mouth normally when drowning, here we can stop that by setting it to false.
		FishingPoolSizeMultiplier = 2f; //The multiplier used for calculating the size of a fishing pool of this liquid. Here, each liquid tile counts as 2 for every tile in a fished pool.
		UsesLavaCollisionForWet = true;
		ExtinguishesOnFireDebuffs = false;
		AddMapEntry(new Color(0, 255, 0));
	}
	public override bool PreSlopeDraw(int i, int j, bool behindBlocks, ref Vector2 drawPosition, ref Rectangle liquidSize, ref VertexColors colors)
	{
		colors.TopLeftColor *= DefaultOpacity;
		colors.TopRightColor *= DefaultOpacity;
		colors.BottomLeftColor *= DefaultOpacity;
		colors.BottomRightColor *= DefaultOpacity;
		return base.PreSlopeDraw(i, j, behindBlocks, ref drawPosition, ref liquidSize, ref colors);
	}
	public override bool BlocksTilePlacement(Player player, int i, int j)
	{
		return true;
	}
	public override void EmitEffects(int i, int j, LiquidCache liquidCache)
	{
		if (Main.instance.IsActive && !Main.gamePaused && Main.tile[i, j].LiquidType == Type && Main.tile[i, j].LiquidAmount != 0)
		{
			if (Main.tile[i, j].LiquidAmount > 200 && Main.rand.NextBool(350))
			{
				Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, ModContent.DustType<AcidLiquidSplash>());
			}
			if (Main.rand.NextBool(350))
			{
				Dust dust = Dust.NewDustDirect(new Vector2(i * 16, j * 16), 16, 8, ModContent.DustType<AcidLiquidSplash>(), 0f, 0f, 50, default, 1.5f);
				dust.velocity *= 0.8f;
				dust.velocity.X *= 2f;
				dust.velocity.Y -= Main.rand.Next(1, 7) * 0.1f;
				if (Main.rand.NextBool(10))
				{
					dust.velocity.Y *= Main.rand.Next(2, 5);
				}
				dust.noGravity = true;
			}
		}
	}
	public override void ModifyNearbyTiles(int i, int j, int liquidX, int liquidY)
	{
		Tile tile = Main.tile[i, j];
		// grasses into dirt
		if (tile.TileType == TileID.Grass || tile.TileType == TileID.CorruptGrass || tile.TileType == TileID.HallowedGrass || tile.TileType == TileID.CrimsonGrass || tile.TileType == TileID.GolfGrass || tile.TileType == TileID.GolfGrassHallowed || tile.TileType == ModContent.TileType<Ickgrass>())
		{
			tile.TileType = TileID.Dirt;
			WorldGen.SquareTileFrame(i, j);
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendTileSquare(-1, i, j, 3);
			}
		}
		// jungle grasses into mud
		else if (tile.TileType == TileID.JungleGrass || tile.TileType == TileID.MushroomGrass || tile.TileType == TileID.CorruptJungleGrass || tile.TileType == TileID.CrimsonJungleGrass || tile.TileType == ModContent.TileType<ContagionJungleGrass>())
		{
			tile.TileType = TileID.Mud;
			WorldGen.SquareTileFrame(i, j);
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendTileSquare(-1, i, j, 3);
			}
		}
		// ash grasses into ash
		else if (tile.TileType == TileID.AshGrass || tile.TileType == ModContent.TileType<Ectograss>())
		{
			tile.TileType = TileID.Ash;
			WorldGen.SquareTileFrame(i, j);
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendTileSquare(-1, i, j, 3);
			}
		}
	}
	public override bool UpdateLiquid(int i, int j, Liquid liquid)
	{
		if (Main.tile[i, j].LiquidAmount >= 64 && AvalonWorld.AcidDestroyTilesTimer % 16 == 0)
		{
			for (int k = i - 1; k <= i + 1; k++)
			{
				int l = j + (k == i ? 1 : 0);
				if (!WorldGen.InWorld(k, l)) return base.UpdateLiquid(i, j, liquid);

				if (Main.tile[k, l].HasTile && TileID.Sets.CanBeDugByShovel[Main.tile[k, l].TileType])
				{
					ClassExtensions.KillTileFast(k, l, true);
					SoundEngine.PlaySound(SoundID.LiquidsWaterLava, new Vector2(k, l) * 16);
					SoundEngine.PlaySound(SoundID.SplashWeak, new Vector2(k, l) * 16);
					for (int n = 0; n < 4; n++)
					{
						Dust dust = Dust.NewDustDirect(new Vector2(k * 16, l * 16), 12, 24, DustID.Smoke);
						dust.color = Main.rand.NextFromList(Color.LightGray, Color.Gray);
						dust.velocity.Y -= 1.5f;
						dust.velocity.X *= 1.5f;
						dust.scale = 1.6f;
						dust.noGravity = true;
					}
					Main.tile[i, j].LiquidAmount -= 64;
					//WorldGen.SquareTileFrame(k, l);

					if (Main.netMode != NetmodeID.SinglePlayer)
					{
						NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 4, k, l);
					}
				}
			}
		}
		if (Main.tile[i, j].LiquidAmount <= 0)
		{
			Tile liq = Main.tile[i, j];
			liq.LiquidAmount = 0;
			liq.LiquidType = LiquidID.Water;
		}
		return base.UpdateLiquid(i, j, liquid);
	}
	public override bool PreLiquidMerge(int liquidX, int liquidY, int tileX, int tileY, int otherLiquid)
	{
		//tile variables, these help us edit the liquid at certain tile positions
		Tile leftTile = Main.tile[tileX - 1, tileY];
		Tile rightTile = Main.tile[tileX + 1, tileY];
		Tile upTile = Main.tile[tileX, tileY - 1];
		Tile tile = Main.tile[tileX, tileY];
		Tile liquidTile = Main.tile[liquidX, liquidY];

		//Checks the type of merging
		//
		//For more context:
		//Liquid to Liquid merging is split up into 2 types, 
		// * Top/Side merging
		// * Down merging
		//
		//Here we get which type the merging is based on the liquidY relitive to the tileY.
		//This is because liquidY and tileY are different in the down merging, but the same in the up/side merging
		byte reductionDivisor = 4;
		if (liquidY == tileY)
		{
			//This is up/side merging for the liquid

			//Here we remove the liquid when merging
			//Majority of this code determines whether a liquid merge is spawned or not by checking the surrounding liquid amounts
			int liquidCount = 0;
			if (leftTile.LiquidType != Type)
			{
				//liquidCount += leftTile.LiquidAmount;
				liquidTile.LiquidAmount -= (byte)(Math.Min(leftTile.LiquidAmount, liquidTile.LiquidAmount) / reductionDivisor);
				leftTile.LiquidAmount = 0;
			}
			if (rightTile.LiquidType != Type)
			{
				//liquidCount += rightTile.LiquidAmount;
				liquidTile.LiquidAmount -= (byte)(Math.Min(rightTile.LiquidAmount, liquidTile.LiquidAmount) / reductionDivisor);
				rightTile.LiquidAmount = 0;
			}
			if (upTile.LiquidType != Type)
			{
				//liquidCount += upTile.LiquidAmount;
				liquidTile.LiquidAmount -= (byte)(Math.Min(upTile.LiquidAmount, liquidTile.LiquidAmount) / reductionDivisor);
				upTile.LiquidAmount = 0;
			}

			//check is the nearby amount is more than 24, and the other liquid is not this liquid
			if (liquidCount < 24 || otherLiquid == Type)
			{
				return false;
			}

			// don't do anything to delete liquid
			// doNothing();

			//play the liquid merge sound
			if (!WorldGen.gen)
			{
				LiquidHooks.PlayLiquidChangeSound(tileX, tileY, Type, otherLiquid);
			}
			if (Main.netMode == NetmodeID.Server)
			{
				ModPacket packet = ModContent.GetInstance<ModLiquidLib.ModLiquidLib>().GetPacket();
				packet.Write((byte)MessageType.SyncCollisionSounds);
				packet.Write(tileX);
				packet.Write(tileY);
				packet.Write(Type);
				packet.Write(otherLiquid);
				packet.Send();
			}
			//frame the tile/update the tile/s nearby
			WorldGen.SquareTileFrame(liquidX, liquidY);
			//sync changes in multiplayer
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendTileSquare(-1, tileX - 1, tileY - 1, 3);
			}
		}
		else
		{
			//This is down merging for the liquid

			//remove the liquid amount 
			liquidTile.LiquidAmount -= (byte)(Math.Min(tile.LiquidAmount, liquidTile.LiquidAmount) / reductionDivisor);
			tile.LiquidAmount = 0;
			tile.LiquidType = 0;
			//play liquid merge sound
			if (!WorldGen.gen)
			{
				LiquidHooks.PlayLiquidChangeSound(tileX, tileY, Type, otherLiquid);
			}
			if (Main.netMode == NetmodeID.Server)
			{
				ModPacket packet = ModContent.GetInstance<ModLiquidLib.ModLiquidLib>().GetPacket();
				packet.Write((byte)MessageType.SyncCollisionSounds);
				packet.Write(tileX);
				packet.Write(tileY);
				packet.Write(Type);
				packet.Write(otherLiquid);
				packet.Send();
			}
			//frame the tile/s around the tile
			WorldGen.SquareTileFrame(liquidX, liquidY);
			//sync tile changs around
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendTileSquare(-1, tileX - 1, tileY, 3);
			}
		}
		return false;
	}

	public override void LiquidMergeSound(int i, int j, int otherLiquid, ref SoundStyle? collisionSound)
	{
		collisionSound = SoundID.LiquidsWaterLava;
	}

	public override int ChooseWaterfallStyle(int i, int j)
	{
		return ModContent.GetInstance<AcidFall>().Slot;
	}

	public override LightMaskMode LiquidLightMaskMode(int i, int j)
	{
		return LightMaskMode.None;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0f;
		g = 1f;
		b = 0f;
	}

	public override bool EvaporatesInHell(int i, int j)
	{
		return false;
	}

	public override void RetroDrawEffects(int i, int j, SpriteBatch spriteBatch, ref RetroLiquidDrawInfo drawData, float liquidAmountModified, int liquidGFXQuality)
	{
		drawData.liquidAlphaMultiplier *= 1.7f;
		if (drawData.liquidAlphaMultiplier > 1f)
		{
			drawData.liquidAlphaMultiplier = 1f;
		}
	}
	public override void OnPlayerCollision(Player player)
	{
		int DMG = 40 + player.statDefense / 2;
		if (player.GetModPlayer<AvalonPlayer>().AcidDmgReduction)
		{
			DMG = 20 + player.statDefense / 2;
		}
		player.Hurt(PlayerDeathReason.ByCustomReason(NetworkText.FromKey($"Mods.Avalon.DeathText.Acid_{Main.rand.Next(5)}", $"{player.name}")), DMG, 0);
		float time = 7;
		if (Main.expertMode) time = 14;
		if (Main.masterMode) time = 17.5f;
		player.AddBuff(ModContent.BuffType<Dissolving>(), (int)(60 * time));
	}
	public override void OnNPCCollision(NPC npc)
	{
		if (!Data.Sets.NPCSets.NoAcidDamage[npc.type] && !npc.dontTakeDamage && Main.netMode != NetmodeID.MultiplayerClient && npc.immune[255] == 0)
		{
			npc.immune[255] = 30;
			npc.SimpleStrikeNPC(65 + npc.defense / 2, 0, noPlayerInteraction: true);
			npc.AddBuff(ModContent.BuffType<Dissolving>(), 60 * 7);
		}
	}
	public override void ItemLiquidCollision(Item item, ref Vector2 wetVelocity, ref float gravity, ref float maxFallSpeed)
	{
		//The following has this liquid delete items of the Blue rarity similar to how lava deletes items of the white rarity
		//We put this here as liquid movement is called just before lava deletion (Item.CheckLavaDeath)
		if (!item.beingGrabbed)
		{
			if (item.playerIndexTheItemIsReservedFor == Main.myPlayer && item.rare == ItemRarityID.White && item.type >= ItemID.None && !ItemID.Sets.IsLavaImmuneRegardlessOfRarity[item.type])
			{
				item.active = false;
				item.type = ItemID.None;
				item.stack = 0;
				if (Main.netMode != NetmodeID.SinglePlayer)
				{
					NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item.whoAmI);
				}

				SoundEngine.PlaySound(SoundID.LiquidsWaterLava, item.position);
				SoundEngine.PlaySound(SoundID.SplashWeak, item.position);

				for (int n = 0; n < 5; n++)
				{
					Dust dust = Dust.NewDustDirect(new Vector2(item.position.X - 6f, item.position.Y + item.height / 2 - 8f), item.width + 12, 24, DustID.Smoke);
					dust.color = Main.rand.NextFromList(Color.LightGray, Color.Gray);
					dust.velocity.Y -= 1.5f;
					dust.velocity.X *= 1.5f;
					dust.scale = 1.6f;
					dust.noGravity = true;
				}

				for (int n = 0; n < 5; n++)
				{
					Dust dust = Dust.NewDustDirect(new Vector2(item.position.X - 6f, item.position.Y + item.height / 2 - 8f), item.width + 12, 24, ModContent.DustType<AcidLiquidSplash>());
					dust.velocity.Y -= 1.5f;
					dust.velocity.X *= 2.5f;
					dust.scale = 1.3f;
					dust.alpha = 100;
					dust.noGravity = true;
				}
			}
		}
	}

	//The following region contains all the logic for what this liquid does when being entered and exited by different entities.
	#region Splash Effects

	//Each hook/method is used to execute what would happen when something enters/exits a liquid.
	//There is a hook for the following
	// * Players
	// * NPCs
	// * Projectiles
	// * Items
	//Each hook has a "isEnter" param, which is true whenever entity is entering the liquid.
	//This is usually used to do different effects when entering a liquid rather than exiting one

	//The following hooks/methods have adapted code from vanilla's splash code for honey as the splash dusts themselves are based on the honey splash dust.
	public override bool OnPlayerSplash(Player player, bool isEnter)
	{
		for (int i = 0; i < 20; i++)
		{
			Dust dust = Dust.NewDustDirect(new Vector2(player.position.X - 6f, player.position.Y + player.height / 2 - 8f), player.width + 12, 24, SplashDustType);
			dust.velocity.Y -= 1f;
			dust.velocity.X *= 2.5f;
			dust.scale = 1.3f;
			dust.alpha = 100;
			dust.noGravity = true;
		}
		SoundEngine.PlaySound(SplashSound, player.position);
		return false;
	}

	public override bool OnNPCSplash(NPC npc, bool isEnter)
	{
		for (int i = 0; i < 10; i++)
		{
			Dust dust = Dust.NewDustDirect(new Vector2(npc.position.X - 6f, npc.position.Y + npc.height / 2 - 8f), npc.width + 12, 24, SplashDustType);
			dust.velocity.Y -= 1f;
			dust.velocity.X *= 2.5f;
			dust.scale = 1.3f;
			dust.alpha = 100;
			dust.noGravity = true;
		}
		//only play the sound if the npc isnt a slime, mouse, tortoise, or if it has no gravity
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
		for (int i = 0; i < 10; i++)
		{
			Dust dust = Dust.NewDustDirect(new Vector2(proj.position.X - 6f, proj.position.Y + proj.height / 2 - 8f), proj.width + 12, 24, SplashDustType);
			dust.velocity.Y -= 1f;
			dust.velocity.X *= 2.5f;
			dust.scale = 1.3f;
			dust.alpha = 100;
			dust.noGravity = true;
		}
		SoundEngine.PlaySound(SplashSound, proj.position);
		return false;
	}

	public override bool OnItemSplash(Item item, bool isEnter)
	{
		for (int i = 0; i < 5; i++)
		{
			Dust dust = Dust.NewDustDirect(new Vector2(item.position.X - 6f, item.position.Y + item.height / 2 - 8f), item.width + 12, 24, SplashDustType);
			dust.velocity.Y -= 1f;
			dust.velocity.X *= 2.5f;
			dust.scale = 1.3f;
			dust.alpha = 100;
			dust.noGravity = true;
		}
		SoundEngine.PlaySound(SplashSound, item.position);
		return false;
	}
	#endregion
}
