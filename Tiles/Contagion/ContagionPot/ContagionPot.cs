using Avalon;
using Avalon.Common;
using Avalon.Items.Placeable.Furniture;
using Avalon.ModSupport;
using Avalon.WorldGeneration.Enums;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Contagion.ContagionPot;

public class ContagionPot : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileFrameImportant[Type] = true;
		Main.tileLavaDeath[Type] = true;
		Main.tileWaterDeath[Type] = false;
		Main.tileOreFinderPriority[Type] = 100;
		Main.tileSpelunker[Type] = true;
		Main.tileCut[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.addTile(Type);
		LocalizedText name = CreateMapEntryName();
		AddMapEntry(new Color(96, 116, 75), name);
		DustType = ModContent.DustType<Dusts.ContagionDust>();
		HitSound = SoundID.NPCDeath1;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		return false;
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		_ = j - Main.tile[i, j].TileFrameY / 18;
		_ = i - Main.tile[i, j].TileFrameX / 18;
		SoundEngine.PlaySound(SoundID.NPCDeath1, new Vector2(i * 16, j * 16));
		SoundEngine.PlaySound(SoundID.Dig, new Vector2(i * 16, j * 16));
		Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.Pot, 0f, 0f, 0, default, 1f);
		Gore.NewGore(WorldGen.GetItemSource_FromTileBreak(i, j), new Vector2(i * 16, j * 16), default, ModContent.GoreType<Gores.ContagionPotGore1>());
		Gore.NewGore(WorldGen.GetItemSource_FromTileBreak(i, j), new Vector2(i * 16, j * 16), default, ModContent.GoreType<Gores.ContagionPotGore2>());
		Gore.NewGore(WorldGen.GetItemSource_FromTileBreak(i, j), new Vector2(i * 16, j * 16), default, ModContent.GoreType<Gores.ContagionPotGore3>());
		if (!WorldGen.gen && Main.netMode != NetmodeID.MultiplayerClient)
		{
			if (WorldGen.genRand.NextBool(15))
			{
				if (j < Main.worldSurface)
				{
					int num6 = WorldGen.genRand.Next(11);
					if (num6 == 0)
					{
						if (Main.hardMode && WorldGen.genRand.NextBool(2))
						{
							Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.SummoningPotion, 1, false, 0, false);
						}
						else Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.IronskinPotion, 1, false, 0, false);
					}
					if (num6 == 1)
					{
						if (Main.hardMode && WorldGen.genRand.NextBool(2))
						{
							Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<Items.Potions.Buff.AuraPotion>(), 1, false, 0, false);
						}
						else Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.ShinePotion, 1, false, 0, false);
					}
					if (num6 == 2)
					{
						if (Main.hardMode && WorldGen.genRand.NextBool(2))
						{
							Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.AmmoReservationPotion, 1, false, 0, false);
						}
						else Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.NightOwlPotion, 1, false, 0, false);
					}
					if (num6 == 3)
					{
						if (Main.hardMode && WorldGen.genRand.NextBool(2))
						{
							Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<Items.Potions.Buff.ShockwavePotion>(), 1, false, 0, false);
						}
						else Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.SwiftnessPotion, 1, false, 0, false);
					}
					if (num6 == 4)
					{
						if (Main.hardMode && WorldGen.genRand.NextBool(2))
						{
							Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<Items.Potions.Buff.TimeShiftPotion>(), 1, false, 0, false);
						}
						else Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.MiningPotion, 1, false, 0, false);
					}
					if (num6 == 5)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.CalmingPotion, 1, false, 0, false);
					}
					if (num6 == 6)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.BuilderPotion, 1, false, 0, false);
					}
					if (num6 == 7)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.RecallPotion, WorldGen.genRand.Next(1, 3), false, 0, false);
					}
				}
				else if (j < Main.rockLayer)
				{
					int num7 = WorldGen.genRand.Next(10);
					if (num7 == 0)
					{
						if (WorldGen.genRand.NextBool(2))
						{
							Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<Items.Potions.Buff.ShockwavePotion>(), 1, false, 0, false);
						}
						else Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.RegenerationPotion, 1, false, 0, false);
					}
					if (num7 == 1)
					{
						if (WorldGen.genRand.NextBool(2))
						{
							Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<Items.Potions.Buff.AuraPotion>(), 1, false, 0, false);
						}
						else Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.ShinePotion, 1, false, 0, false);
					}
					if (num7 == 2)
					{
						if (Main.hardMode && WorldGen.genRand.NextBool(2))
						{
							Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<Items.Potions.Buff.BloodCastPotion>(), 1, false, 0, false);
						}
						else Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.NightOwlPotion, 1, false, 0, false);
					}
					if (num7 == 3)
					{
						if (Main.hardMode && WorldGen.genRand.NextBool(2))
						{
							Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<Items.Potions.Buff.CloverPotion>(), 1, false, 0, false);
						}
						else Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.SwiftnessPotion, 1, false, 0, false);
					}
					if (num7 == 4)
					{
						if (Main.hardMode && WorldGen.genRand.NextBool(2))
						{
							Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.AmmoReservationPotion, 1, false, 0, false);
						}
						else Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.ArcheryPotion, 1, false, 0, false);
					}
					if (num7 == 5)
					{
						if (WorldGen.genRand.NextBool(2))
						{
							Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.WrathPotion, 1, false, 0, false);
						}
						else Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.GillsPotion, 1, false, 0, false);
					}
					if (num7 == 6)
					{
						if (WorldGen.genRand.NextBool(2))
						{
							Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<Items.Potions.Buff.GPSPotion>(), 1, false, 0, false);
						}
						else Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.HunterPotion, 1, false, 0, false);
					}
					if (num7 == 7)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.MiningPotion, 1, false, 0, false);
					}
					if (num7 == 8)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.TrapsightPotion, 1, false, 0, false);
					}
					if (num7 == 9)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.RecallPotion, WorldGen.genRand.Next(1, 3), false, 0, false);
					}
				}
			}
			else
			{
				int num10 = Main.rand.Next(9);
				if (num10 == 0 && Main.player[Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16)].statLife < Main.player[Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16)].statLifeMax2)
				{
					Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.Heart, 1, false, 0, false);
				}
				else if (num10 == 1 && Main.player[Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16)].statMana < Main.player[Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16)].statManaMax2)
				{
					Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.Star, 1, false, 0, false);
				}
				else if (num10 == 2)
				{
					int torchStack = WorldGen.genRand.Next(4, 12);
					int glowstickStack = WorldGen.genRand.Next(2, 5);
					if (Main.expertMode)
					{
						torchStack = WorldGen.genRand.Next(5, 18);
						glowstickStack = WorldGen.genRand.Next(3, 11);
					}
					if (Main.LocalPlayer.ZoneCorrupt && Main.tile[i, j].LiquidAmount <= 0)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.CorruptTorch, torchStack); //Corrupt Torches
					}
					else if (Main.LocalPlayer.ZoneCrimson && Main.tile[i, j].LiquidAmount <= 0)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.CrimsonTorch, torchStack); //Crimson Torches
					}
					else if (Main.LocalPlayer.ZoneHallow && Main.tile[i, j].LiquidAmount <= 0)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.HallowedTorch, torchStack); //Hallow Torches
					}
					else if ((Main.LocalPlayer.InModBiome<Biomes.Contagion>() || Main.LocalPlayer.InModBiome<Biomes.UndergroundContagion>()) && Main.tile[i, j].LiquidAmount <= 0)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<ContagionTorch>(), torchStack); //Contagion Torches
					}
					else if ((Main.LocalPlayer.InModBiome<Biomes.Savanna>() || Main.LocalPlayer.InModBiome<Biomes.UndergroundTropics>()) && Main.tile[i, j].LiquidAmount <= 0)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<SavannaTorch>(), torchStack); //Tropics Torches
					}
					else if (Main.tile[i, j].LiquidAmount > 0 && !Main.LocalPlayer.ZoneSnow && !Main.LocalPlayer.ZoneRockLayerHeight)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.Glowstick, glowstickStack); //Glowstick
					}
					else if (Main.tile[i, j].LiquidAmount > 0 && Main.LocalPlayer.ZoneSnow && Main.LocalPlayer.ZoneRockLayerHeight)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.StickyGlowstick, glowstickStack); //Sticky Glowstick
					}
					else
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.Torch, glowstickStack); //Torches
					}
				}
				else if (num10 == 3)
				{
					int stack2 = Main.rand.Next(8) + 3;
					int type2 = 40;
					if (j < Main.rockLayer && WorldGen.genRand.NextBool(2))
					{
						if (Main.hardMode)
						{
							type2 = ItemID.Grenade;
						}
						else
						{
							type2 = ItemID.Shuriken;
						}
					}
					if (Main.hardMode)
					{
						if (Main.rand.NextBool(2))
						{
							if (WorldGen.SavedOreTiers.Silver == ItemID.TungstenOre)
							{
								type2 = ItemID.TungstenBullet;
							}
							else if (WorldGen.SavedOreTiers.Silver == ModContent.ItemType<Items.Material.Ores.ZincOre>())
							{
								type2 = ModContent.ItemType<Items.Ammo.ZincBullet>();
							}
							else type2 = ItemID.SilverBullet;
						}
						else
						{
							if (!AltLibrarySupport.EvilBiomeArrow(ref type2))
							{
								if (ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldEvil.Corruption) type2 = ItemID.UnholyArrow;
								else if (ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldEvil.Crimson) type2 = ModContent.ItemType<Items.Ammo.BloodyArrow>();
								else if (ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldEvil.Contagion) type2 = ModContent.ItemType<Items.Ammo.IckyArrow>(); // contagion arrow
							}
						}
					}
					Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, type2, stack2, false, 0, false);
				}
				else if (num10 == 4)
				{
					int type3 = ItemID.LesserHealingPotion;
					//if (ModContent.GetInstance<AvalonWorld>().SuperHardmode && j > Main.rockLayer && Main.rand.NextBool(2))
					//{
					//    type3 = ItemID.GreaterHealingPotion;
					//}
					//else 
					if (j > Main.maxTilesY - 200 || Main.hardMode/* && !ModContent.GetInstance<AvalonWorld>().SuperHardmode*/)
					{
						type3 = ItemID.HealingPotion;
					}
					if (Main.expertMode)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, type3, WorldGen.genRand.Next(1, 3), false, 0, false);
					}
					else
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, type3, 1, false, 0, false);
					}
				}
				else if (num10 == 5 && j > Main.rockLayer)
				{
					int stack3 = Main.rand.Next(4) + 1;
					if (Main.expertMode)
					{
						stack3 += WorldGen.genRand.Next(4);
					}
					Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.Bomb, stack3, false, 0, false);
				}
				else if (num10 == 6 && j < Main.maxTilesY - 200 && !Main.hardMode)
				{
					int stack4 = Main.rand.Next(20, 41);
					Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.Rope, stack4, false, 0, false);
				}
				else
				{
					float num11 = 200 + WorldGen.genRand.Next(-100, 101);
					if (j < Main.worldSurface)
					{
						num11 *= 0.5f;
					}
					else if (j < Main.rockLayer)
					{
						num11 *= 0.75f;
					}
					else if (j > Main.maxTilesY - 250)
					{
						num11 *= 1.25f;
					}
					num11 *= 1f + Main.rand.Next(-20, 21) * 0.01f;
					if (Main.rand.NextBool(5))
					{
						num11 *= 1f + Main.rand.Next(5, 11) * 0.01f;
					}
					if (Main.rand.NextBool(10))
					{
						num11 *= 1f + Main.rand.Next(10, 21) * 0.01f;
					}
					if (Main.rand.NextBool(15))
					{
						num11 *= 1f + Main.rand.Next(20, 41) * 0.01f;
					}
					if (Main.rand.NextBool(20))
					{
						num11 *= 1f + Main.rand.Next(40, 81) * 0.01f;
					}
					if (Main.rand.NextBool(25))
					{
						num11 *= 1f + Main.rand.Next(50, 101) * 0.01f;
					}
					ClassExtensions.DropCoinsProperly(num11, i * 16, j * 16);
				}
			}
		}
	}
}
