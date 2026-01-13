using Avalon.Biomes;
using Avalon.Buffs.AdvancedBuffs;
using Avalon.Buffs.Debuffs;
using Avalon.Common.Players;
using Avalon.Items.Accessories.Hardmode;
using Avalon.Items.Accessories.Info;
using Avalon.Items.Consumables;
using Avalon.Items.Material;
using Avalon.Items.Other;
using Avalon.Items.Placeable.HerbsAndSeeds;
using Avalon.Items.Placeable.Tile;
using Avalon.ModSupport;
using Avalon.NPCs.Bosses.Hardmode.WallOfSteel;
using Avalon.NPCs.Contagion;
using Avalon.NPCs.Hellcastle;
using Avalon.NPCs.Savanna;
using Avalon.NPCs.TownNPCs;
using Avalon.NPCs.Underground;
using Avalon.Systems;
using Avalon.Tiles.Savanna;
using Avalon.Walls.Contagion.ContagionGrassWall;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Common;

public class AvalonGlobalNPC : GlobalNPC
{
	public static int BleedTime = 60 * 7;
	public static int PhantasmBoss = -1;

	/// <summary>
	///     Finds a type of NPC.
	/// </summary>
	/// <param name="type">The type of NPC to find.</param>
	/// <returns>The index of the found NPC in the Main.npc[] array.</returns>
	public static int FindATypeOfNPC(int type)
	{
		foreach (var npc in Main.ActiveNPCs)
		{
			if (type == npc.type && npc.active)
			{
				return npc.whoAmI;
			}
		}

		return 0;
	}
	public override bool PreAI(NPC npc)
	{
		if (Main.netMode != NetmodeID.MultiplayerClient)
		{
			//if (Main.rand.NextBool(1) && !Main.npcChatRelease)
			{
				for (int i = 0; i < Main.maxPlayers; i++)
				{
					if (Main.player[i].active && npc.GetGlobalNPC<AvalonGlobalNPCInstance>().SkellyBanana)//Main.player[i].talkNPC == npc.whoAmI && npc.type == NPCID.SkeletonMerchant)
					{
						Item.NewItem(npc.GetSource_FromThis(), npc.position, ItemID.Banana);
						Utils.PoofOfSmoke(npc.position);
						if (Main.netMode == NetmodeID.SinglePlayer)
						{
							Main.NewText(Language.GetTextValue("Mods.Avalon.Banana"), 230, 230, 0);
						}
						else
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.Avalon.Banana"), new Color(230, 230, 0));
						}
						npc.active = false;
						npc.GetGlobalNPC<AvalonGlobalNPCInstance>().SkellyBanana = false;
					}
				}
			}
		}
		return true;
	}

	/// <summary>
	/// Spawns the Wall of Steel at the given position.
	/// </summary>
	/// <param name="pos">The position to spawn the boss at.</param>
	/// <param name="essence">Whether or not the method will broadcast the "has awoken!" message.</param>
	public static void SpawnWOS(Vector2 pos, bool essence = false)
	{
		if (pos.Y / 16f < Main.maxTilesY - 205)
		{
			return;
		}
		if (AvalonWorld.WallOfSteel >= 0 || Main.wofNPCIndex >= 0)
		{
			return;
		}
		if (Main.netMode == NetmodeID.MultiplayerClient)
		{
			return;
		}
		int num = 1;
		if (pos.X / 16f > Main.maxTilesX / 2)
		{
			num = -1;
		}
		bool flag = false;
		int num2 = (int)pos.X;
		while (!flag)
		{
			flag = true;
			foreach (var player in Main.ActivePlayers)
			{
				if (player.active && player.position.X > num2 - 1200 && player.position.X < num2 + 1200)
				{
					num2 -= num * 16;
					flag = false;
				}
			}
			if (num2 / 16 < 20 || num2 / 16 > Main.maxTilesX - 20)
			{
				flag = true;
			}
		}
		int num3 = (int)pos.Y;
		int num4 = num2 / 16;
		int num5 = num3 / 16;
		int num6 = 0;
		try
		{
			while (WorldGen.SolidTile(num4, num5 - num6) || Main.tile[num4, num5 - num6].LiquidAmount >= 100)
			{
				if (!WorldGen.SolidTile(num4, num5 + num6) && Main.tile[num4, num5 + num6].LiquidAmount < 100)
				{
					num5 += num6;
					goto IL_162;
				}
				num6++;
			}
			num5 -= num6;
		}
		catch
		{
		}
	IL_162:
		num3 = num5 * 16;
		int num7 = NPC.NewNPC(NPC.GetBossSpawnSource(Player.FindClosest(pos, 32, 32)), num2, num3, ModContent.NPCType<WallofSteel>(), 0);
		if (Main.netMode == NetmodeID.Server && num7 < 200)
		{
			NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, num7);
		}
		//if (Main.npc[num7].displayName == "")
		//{
		//    Main.npc[num7].DisplayName = "Wall of Steel";
		//}
		if (!essence)
		{
			if (Main.netMode == NetmodeID.SinglePlayer)
			{
				Main.NewText(Language.GetTextValue("Mods.Avalon.WOSAwaken"), 175, 75, 255);
				return;
			}
			if (Main.netMode == NetmodeID.Server)
			{
				ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.Avalon.WOSAwaken")), new Color(175, 75, 255));
			}
		}
	}
	public override void OnKill(NPC npc)
	{
		//if (npc.type == NPCID.DungeonSpirit && Main.rand.NextBool(15) &&
		//	Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneDungeon)
		//{
		//	int proj = Projectile.NewProjectile(npc.GetSource_FromThis(), npc.position, npc.velocity,
		//		ModContent.ProjectileType<Projectiles.SpiritPoppy>(), 0, 0, Main.myPlayer);
		//	Main.projectile[proj].velocity.Y = -2.5f;
		//	Main.projectile[proj].velocity.X = Main.rand.Next(-45, 46) * 0.1f;
		//}
		if (npc.type == NPCID.Vulture && AvalonWorld.SpawnDesertBeak)
		{
			AvalonWorld.VultureKillCount++;
		}
		if (npc.type == NPCID.SkeletronHead && !NPC.downedBoss3)
		{
			AvalonWorld.GenerateSulphur();
		}
		if (npc.type is NPCID.TheDestroyer or NPCID.Retinazer or NPCID.Spazmatism or NPCID.SkeletronPrime)
		{
			if (ClassExtensions.DownedAllButOneMechBoss())
			{
				if ((npc.type == NPCID.Spazmatism && NPC.AnyNPCs(NPCID.Retinazer)) || (npc.type == NPCID.Retinazer && NPC.AnyNPCs(NPCID.Spazmatism)))
				{
					return;
				}
				AvalonWorld.GenerateHallowedOre();
			}
		}
		if (npc.type == NPCID.MoonLordCore && !NPC.downedMoonlord)
		{
			if (Main.netMode == NetmodeID.SinglePlayer)
			{
				Main.NewText(Language.GetTextValue("Mods.Avalon.BossDefeatedBlurbs.MoonLord"), 50, 255, 130);
			}
			else if (Main.netMode == NetmodeID.Server)
			{
				ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.Avalon.BossDefeatedBlurbs.MoonLord"), new Color(50, 255, 130));
			}
		}

		// add back when hardmode/hidden temple releases
		//if (npc.type == NPCID.Golem && !NPC.downedGolemBoss)
		//{
		//    if (Main.netMode == NetmodeID.SinglePlayer)
		//    {
		//        Main.NewText(Language.GetTextValue("Mods.Avalon.BossDefeatedBlurbs.Golem"), 50, 255, 130);
		//    }
		//    else if (Main.netMode == NetmodeID.Server)
		//    {
		//        ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.Avalon.BossDefeatedBlurbs.Golem"), new Color(50, 255, 130));
		//    }
		//}
	}

	/// <summary>
	/// A method that scrambles the stats of a target enemy.
	/// </summary>
	/// <param name="n">The NPC to scramble.</param>
	public static void ScrambleStats(NPC n)
	{
		float amount = (float)(Main.rand.NextFloat() + 0.5f + Main.rand.NextFloat() * 0.5f);
		//n.life = (int)(n.lifeMax * amount);
		n.defDefense *= (int)(n.defense * amount);
		n.defDamage *= (int)(n.damage * amount);
	}
	public override void ModifyShop(NPCShop shop)
	{
		Condition corruption = new Condition("Corruption", () => ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Corruption);
		Condition crimson = new Condition("Crimson", () => ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Crimson);
		Condition contagion = new Condition("Contagion", () => ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Contagion);
		AltLibrarySupport.ReplaceShopConditions(ref corruption, ref crimson, ref contagion);
		Condition notContagion = new Condition("Not Contagion", () => !contagion.IsMet());
		Condition downedBP = new Condition("BacteriumPrime", () => ModContent.GetInstance<DownedBossSystem>().DownedBacteriumPrime);

		if (shop.NpcType == NPCID.Merchant)
		{
			shop.Add(new Item(ItemID.MusicBox)
			{
				shopCustomPrice = 350000,
			});
		}
		if (shop.NpcType == NPCID.Painter)
		{
			shop.Add(ModContent.ItemType<InactiveCoating>(), Condition.InGraveyard, Condition.DownedSkeletron);
		}
		if (shop.NpcType == NPCID.PartyGirl)
		{
			shop.Add(new Item(ModContent.ItemType<Items.Accessories.Vanity.AncientHeadphones>())
			{
				shopCustomPrice = Item.buyPrice(gold: 12),
			});
		}
		if (shop.NpcType == NPCID.Pirate)
		{
			shop.Add(new Item(ModContent.ItemType<FalseTreasureMap>())
			{
				shopCustomPrice = Item.buyPrice(0, 4),
			}, Condition.DownedPirates);
		}
		if (shop.NpcType == NPCID.Cyborg)
		{
			shop.Add(new Item(ModContent.ItemType<MartianPeaceTreaty>())
			{
				shopCustomPrice = Item.buyPrice(0, 4),
			}, Condition.DownedMartians);
		}
		if (shop.NpcType == NPCID.GoblinTinkerer)
		{
			shop.Add(new Item(ModContent.ItemType<RocketinaBottle>())
			{
				shopCustomPrice = Item.buyPrice(0, 6),
			}, Condition.Hardmode);

			shop.Add(new Item(ModContent.ItemType<GoblinRetreatOrder>())
			{
				shopCustomPrice = Item.buyPrice(0, 4),
			}, Condition.DownedGoblinArmy);

			shop.Add(new Item(ModContent.ItemType<CalculatorSpectacles>())
			{
				shopCustomPrice = Item.buyPrice(0, 5),
			}, Condition.DownedGoblinArmy);
		}
		if (shop.NpcType == NPCID.Dryad)
		{
			shop.InsertAfter(ItemID.CrimsonPlanterBox, new Item(ModContent.ItemType<BarfbushPlanterBox>())
			{
				shopCustomPrice = Item.buyPrice(silver: 1)
			}, downedBP);
			shop.InsertAfter(ItemID.CrimsonPlanterBox, new Item(ModContent.ItemType<SweetstemPlanterBox>())
			{
				shopCustomPrice = Item.buyPrice(silver: 1)
			}, Condition.DownedQueenBee);
			shop.InsertAfter(ItemID.FireBlossomPlanterBox, new Item(ModContent.ItemType<HolybirdPlanterBox>())
			{
				shopCustomPrice = Item.buyPrice(silver: 1)
			}, Condition.Hardmode);

			shop.InsertAfter(ItemID.CrimsonGrassEcho, new Item(ModContent.ItemType<ContagionGrassWallItem>())
			{
				shopCustomPrice = Item.buyPrice(silver: 2, copper: 50)
			}, Condition.BloodMoon, contagion);

			shop.InsertAfter(ItemID.CrimsonSeeds, new Item(ModContent.ItemType<ContagionSeeds>())
			{
				shopCustomPrice = Item.buyPrice(silver: 5)
			}, Condition.BloodMoon, contagion);

			shop.InsertAfter(ItemID.CrimsonSeeds, new Item(ModContent.ItemType<ContagionSeeds>())
			{
				shopCustomPrice = Item.buyPrice(silver: 5)
			}, corruption, Condition.InGraveyard, Condition.Hardmode);

			shop.InsertAfter(ItemID.CorruptSeeds, new Item(ModContent.ItemType<ContagionSeeds>())
			{
				shopCustomPrice = Item.buyPrice(silver: 5)
			}, crimson, Condition.InGraveyard, Condition.Hardmode);

			shop.InsertAfter(ItemID.ViciousPowder, new Item(ModContent.ItemType<VirulentPowder>())
			{
				shopCustomPrice = Item.buyPrice(silver: 1)
			}, Condition.BloodMoon, contagion);

			// the seeds here aren't always positioned correctly, but it works and I do NOT want to keep relaunching to figure out something as minor as positioning
			if (shop.TryGetEntry(ItemID.CorruptSeeds, out NPCShop.Entry entry))
			{
				entry.AddCondition(notContagion);
			}
			if (shop.TryGetEntry(ItemID.CrimsonSeeds, out NPCShop.Entry entry2))
			{
				entry2.AddCondition(notContagion);
			}
			shop.InsertAfter(ModContent.ItemType<ContagionGrassWallItem>(), new Item(ItemID.CorruptSeeds)
			{
				shopCustomPrice = Item.buyPrice(silver: 5)
			}, contagion, Condition.InGraveyard, Condition.Hardmode);


			if (shop.TryGetEntry(ItemID.CorruptGrassEcho, out NPCShop.Entry entry3))
			{
				entry3.AddCondition(notContagion);
			}
			if (shop.TryGetEntry(ItemID.CrimsonGrassEcho, out NPCShop.Entry entry4))
			{
				entry4.AddCondition(notContagion);
			}
			if (shop.TryGetEntry(ItemID.VilePowder, out NPCShop.Entry entry5))
			{
				entry5.AddCondition(notContagion);
			}
			if (shop.TryGetEntry(ItemID.ViciousPowder, out NPCShop.Entry entry6))
			{
				entry6.AddCondition(notContagion);
			}
		}
	}
	/// <summary>
	///  A method to choose a random Town NPC death messages.
	/// </summary>
	/// <param name="type">The Town NPC's type.</param>
	/// <returns>The string containing the death message.</returns>
	public static string TownDeathMsg(int type)
	{
		string result = string.Empty;
		if (type == NPCID.Merchant)
		{
			int r = Main.rand.Next(7);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.Merchant.DeathText.{r}");
		}
		else if (type == NPCID.Nurse)
		{
			int r = Main.rand.Next(5);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.Nurse.DeathText.{r}");
		}
		else if (type == NPCID.OldMan)
		{
			int r = Main.rand.Next(2);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.OldMan.DeathText.{r}");
		}
		else if (type == NPCID.ArmsDealer)
		{
			int r = Main.rand.Next(7);
			if (r == 5)
			{
				if (!Main.dayTime) result += Language.GetTextValue($"Mods.Avalon.NPCs.ArmsDealer.DeathText.{r}");
				else result += Language.GetTextValue($"Mods.Avalon.NPCs.ArmsDealer.DeathText.{r - 1}");
			}
			else result += Language.GetTextValue($"Mods.Avalon.NPCs.ArmsDealer.DeathText.{r}");
		}
		else if (type == NPCID.Dryad)
		{
			int r = Main.rand.Next(7);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.Dryad.DeathText.{r}");
		}
		else if (type == NPCID.Guide)
		{
			int r = Main.rand.Next(8);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.Guide.DeathText.{r}");
		}
		else if (type == NPCID.Demolitionist)
		{
			int r = Main.rand.Next(7);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.Demolitionist.DeathText.{r}");
		}
		else if (type == NPCID.Clothier)
		{
			int r = Main.rand.Next(6);
		}
		else if (type == NPCID.GoblinTinkerer)
		{
			int r = Main.rand.Next(6);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.GoblinTinkerer.DeathText.{r}");
		}
		else if (type == NPCID.Wizard)
		{
			int r = Main.rand.Next(7);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.Wizard.DeathText.{r}");
		}
		else if (type == NPCID.SantaClaus)
		{
			int r = Main.rand.Next(2);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.SantaClaus.DeathText.{r}");
		}
		else if (type == NPCID.Mechanic)
		{
			int r = Main.rand.Next(6);
			if (r == 5)
			{
				result += Language.GetTextValue($"Mods.Avalon.NPCs.Mechanic.DeathText.{r}", Main.worldName);
			}
			else result += Language.GetTextValue($"Mods.Avalon.NPCs.Mechanic.DeathText.{r}");
		}
		else if (type == NPCID.Truffle)
		{
			int r = Main.rand.Next(7);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.Truffle.DeathText.{r}");
		}
		else if (type == NPCID.Steampunker)
		{
			int r = Main.rand.Next(5);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.Steampunker.DeathText.{r}");
		}
		else if (type == NPCID.DyeTrader)
		{
			int r = Main.rand.Next(6);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.DyeTrader.DeathText.{r}");
		}
		else if (type == NPCID.PartyGirl)
		{
			int r = Main.rand.Next(6);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.PartyGirl.DeathText.{r}");
		}
		else if (type == NPCID.Cyborg)
		{
			int r = Main.rand.Next(9);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.Cyborg.DeathText.{r}");
		}
		else if (type == NPCID.Painter)
		{
			int r = Main.rand.Next(6);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.Painter.DeathText.{r}");
		}
		else if (type == NPCID.WitchDoctor)
		{
			int r = Main.rand.Next(6);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.WitchDoctor.DeathText.{r}");
		}
		else if (type == NPCID.Pirate)
		{
			int r = Main.rand.Next(5);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.Pirate.DeathText.{r}");
		}
		else if (type == NPCID.Stylist)
		{
			int r = Main.rand.Next(6);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.Stylist.DeathText.{r}");
		}
		else if (type == NPCID.TravellingMerchant)
		{
			int r = Main.rand.Next(7);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.TravellingMerchant.DeathText.{r}");
		}
		else if (type == NPCID.Angler)
		{
			int r = Main.rand.Next(9);
			if (r == 8)
			{
				if (NPC.AnyNPCs(NPCID.Pirate)) result += Language.GetTextValue($"Mods.Avalon.NPCs.Angler.DeathText.{r}", Main.npc[FindATypeOfNPC(NPCID.Pirate)].GivenName);
				else result += Language.GetTextValue($"Mods.Avalon.NPCs.Angler.DeathText.{r - 2}");
			}
			else result += Language.GetTextValue($"Mods.Avalon.NPCs.Angler.DeathText.{r}");
		}
		else if (type == NPCID.TaxCollector)
		{
			int r = Main.rand.Next(6);
			if (r == 3)
			{
				result += Language.GetTextValue($"Mods.Avalon.NPCs.TaxCollector.DeathText.{r}", Main.worldName);
			}
			else result += Language.GetTextValue($"Mods.Avalon.NPCs.Angler.DeathText.{r}");
		}
		else if (type == NPCID.DD2Bartender)
		{
			int r = Main.rand.Next(5);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.DD2Bartender.DeathText.{r}");
		}
		else if (type == NPCID.Princess)
		{
			int r = Main.rand.Next(5);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.Princess.DeathText.{r}");
		}
		else if (type == NPCID.Golfer)
		{
			int r = Main.rand.Next(6);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.Golfer.DeathText.{r}");
		}
		else if (type == NPCID.BestiaryGirl)
		{
			int r = Main.rand.Next(3);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.BestiaryGirl.DeathText.{r}");
		}
		//else if (type == ModContent.NPCType<Iceman>())
		//{
		//	int r = Main.rand.Next(7);
		//	if (r == 4)
		//	{
		//		if (NPC.AnyNPCs(NPCID.ArmsDealer)) result += Language.GetTextValue($"Mods.Avalon.NPCs.Iceman.DeathText.{r}", Main.npc[FindATypeOfNPC(NPCID.ArmsDealer)].GivenName);
		//		else result += Language.GetTextValue($"Mods.Avalon.NPCs.Iceman.DeathText.{r + 1}");
		//	}
		//	else result += Language.GetTextValue($"Mods.Avalon.NPCs.Iceman.DeathText.{r}");
		//}
		else if (type == ModContent.NPCType<Librarian>())
		{
			int r = Main.rand.Next(7);
			result += Language.GetTextValue($"Mods.Avalon.NPCs.Librarian.DeathText.{r}");
		}
		else result += Language.GetTextValue("Mods.Avalon.NPCs.DeathTextGeneric");

		return result;
	}
	public override void OnSpawn(NPC npc, IEntitySource source)
	{
		if (source is EntitySource_Parent parent && parent.Entity is NPC npc2 && npc2.HasBuff(BuffID.Cursed))
		{
			npc.active = false;
		}
	}

	public override void DrawEffects(NPC npc, ref Color drawColor)
	{
		if (npc.HasBuff<Lacerated>())
		{
			for (int i = 0; i < npc.GetGlobalNPC<AvalonGlobalNPCInstance>().LacerateStacks; i++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood);
			}
		}
		if (npc.HasBuff<AstralCurse>())
		{
			Dust.NewDust(npc.position, npc.width, npc.height, DustID.DungeonSpirit);
		}
	}
	public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
	{
		//if (spawnInfo.Player.InModBiome<Tropics>() && !spawnInfo.Player.InPillarZone())
		//{
		//    pool.Clear();
		//    //pool.Add(ModContent.NPCType<Rafflesia>(), 0.6f);
		//    pool.Add(ModContent.NPCType<AmberSlime>(), 0.6f);
		//    if (!Main.dayTime)
		//    {

		//    }
		//    /*pool.Add(ModContent.NPCType<TropicalSlime>(), 0.9f);
		//    if (Main.hardMode)
		//    {
		//        pool.Add(ModContent.NPCType<PoisonDartFrog>(), 0.9f);
		//    }*/
		//}
		if (spawnInfo.Player.InModBiome<UndergroundTropics>())
		{
			pool.Clear();
			pool.Add(ModContent.NPCType<Rafflesia>(), 0.6f);
			pool.Add(ModContent.NPCType<InfestedAmberSlime>(), 0.6f);
			pool.Add(ModContent.NPCType<AmberSlime>(), 0.6f);
			pool.Add(ModContent.NPCType<TropicalSlimeGrassy>(), 0.6f);
			pool.Add(ModContent.NPCType<TropicalSlimeShroomy>(), 0.6f);
			pool.Add(ModContent.NPCType<Mosquito>(), 0.6f);
			pool.Add(ModContent.NPCType<MosquitoDroopy>(), 0.6f);
			pool.Add(ModContent.NPCType<MosquitoSmall>(), 0.6f);
			pool.Add(ModContent.NPCType<MosquitoPainted>(), 0.6f);

			if (Main.hardMode)
			{
				pool.Add(ModContent.NPCType<PoisonDartFrog>(), 0.4f);
				pool.Add(ModContent.NPCType<RedArowana>(), 0.5f);
				pool.Add(ModContent.NPCType<RedArowana2>(), 0.5f);
				pool.Add(ModContent.NPCType<VenusFlytrap>(), 0.3f);
			}
		}
		if (spawnInfo.Player.InModBiome<ContagionCaveDesert>())
		{
			pool.Clear();
			//pool.Add(NPCID.DesertScorpionWalk, 0.35f);
			pool.Add(NPCID.Antlion, 0.55f);
			pool.Add(NPCID.WalkingAntlion, 0.35f);
			pool.Add(NPCID.GiantWalkingAntlion, 0.05f);
			//pool.Add(NPCID.GiantFlyingAntlion, 0.05f);
			//pool.Add(NPCID.FlyingAntlion, 0.35f);
			if (Main.hardMode)
			{
				pool.Add(NPCID.DesertBeast, 0.3f);
				pool.Add(NPCID.DesertLamiaDark, 0.45f);
				pool.Add(NPCID.DesertDjinn, 0.45f);
				pool.Add(NPCID.DuneSplicerHead, 0.2f);
				pool.Add(ModContent.NPCType<ContaminatedGhoul>(), 0.33f);
				pool.Add(ModContent.NPCType<MineralSlime>(), 0.2f);
			}
		}
		if (spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneHellcastle)
		{
			pool.Clear();
			pool.Add(NPCID.Demon, 0.2f);
			pool.Add(NPCID.RedDevil, 0.2f);
			pool.Add(ModContent.NPCType<EctoHand>(), 0.3f);
			pool.Add(ModContent.NPCType<HellboundLizard>(), 1f);
			pool.Add(ModContent.NPCType<Gargoyle>(), 1f);
			//if (ModContent.GetInstance<AvalonWorld>().SuperHardmode && Main.hardMode)
			//{
			//    pool.Add(ModContent.NPCType<ArmoredHellTortoise>(), 1f);
			//}
		}
		if (spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneContagion && !spawnInfo.Player.InPillarZone())// && !spawnInfo.Player.HasBuff(ModContent.BuffType<SilenceCandleBuff>()))
		{
			pool.Clear();
			pool.Add(ModContent.NPCType<Bactus>(), 1f);
			pool.Add(ModContent.NPCType<PyrasiteHead>(), 0.1f);
			if (Main.hardMode)
			{
				pool.Add(ModContent.NPCType<Cougher>(), 0.8f);
				pool.Add(ModContent.NPCType<Ickslime>(), 0.7f);
				if (spawnInfo.Player.ZoneRockLayerHeight)
				{
					pool.Add(ModContent.NPCType<Viris>(), 1f);
					//pool.Add(ModContent.NPCType<GrossyFloat>(), 0.6f);
				}

				if (spawnInfo.Player.ZoneDesert)
				{
					pool.Add(ModContent.NPCType<ViralMummy>(), 0.3f);
					pool.Add(ModContent.NPCType<SicklyVulture>(), 1f);
					//pool.Add(ModContent.NPCType<EvilVulture>(), 0.4f);
				}
			}
		}
	}
	public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
	{
		if (player.InModBiome<Contagion>())
		{
			spawnRate = (int)(spawnRate * 0.65f);
			maxSpawns = (int)(maxSpawns * 1.3f);
		}
		if (player.InModBiome<Biomes.Savanna>() || player.InModBiome<UndergroundTropics>())
		{
			spawnRate = (int)(spawnRate * 0.4f);
			maxSpawns = (int)(maxSpawns * 1.5f);
		}
		if (player.GetModPlayer<AvalonPlayer>().AdvancedBattle)
		{
			spawnRate = (int)(spawnRate * AdvBattle.RateMultiplier);
			maxSpawns = (int)(maxSpawns * AdvBattle.SpawnMultiplier);
		}

		if (player.GetModPlayer<AvalonPlayer>().AdvancedCalming)
		{
			spawnRate = (int)(spawnRate * AdvCalming.RateMultiplier);
			maxSpawns = (int)(maxSpawns * AdvCalming.SpawnMultiplier);
		}
	}
	public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
	{
		if (npc.type is NPCID.BloodJelly or NPCID.Unicorn or NPCID.DarkMummy or NPCID.LightMummy && Main.rand.NextBool(9))
		{
			target.AddBuff(ModContent.BuffType<BrokenWeaponry>(), 60 * 7);
		}
		if (npc.type == NPCID.Mummy || npc.type == NPCID.FungoFish || npc.type == NPCID.Clinger)
		{
			if (Main.rand.NextBool(9))
			{
				target.AddBuff(ModContent.BuffType<Unloaded>(), 60 * 7);
			}
		}
		if (npc.type is NPCID.AngryTrapper && Main.rand.NextBool(15))
		{
			target.AddBuff(ModContent.BuffType<CurseofDelirium>(), 60 * 60);
		}
		if (npc.type is NPCID.IlluminantBat or NPCID.IlluminantSlime && Main.rand.NextBool(6))
		{
			target.AddBuff(ModContent.BuffType<CurseofDelirium>(), 60 * 15);
		}
		if ((npc.type is NPCID.EnchantedSword or NPCID.CursedHammer or NPCID.CrimsonAxe || npc.type == ModContent.NPCType<InfectedPickaxe>()) && Main.rand.NextBool(5))
		{
			target.AddBuff(ModContent.BuffType<CurseofDelirium>(), 60 * 60);
		}
	}
	public static bool SpikeCollision2(Vector2 Position, int Width, int Height)
	{
		int LowX = (int)((Position.X - 2f) / 16f); // - Radius;
		int HighX = (int)((Position.X + (float)Width) / 16f); // + Radius;
		int LowY = (int)((Position.Y - 2f) / 16f); // - Radius;
		int HighY = (int)((Position.Y + (float)Height) / 16f); // + Radius;
		if (LowX < 0)
		{
			LowX = 0;
		}
		if (HighX > Main.maxTilesX)
		{
			HighX = Main.maxTilesX;
		}
		if (LowY < 0)
		{
			LowY = 0;
		}
		if (HighY > Main.maxTilesY)
		{
			HighY = Main.maxTilesY;
		}
		for (int i = LowX; i <= HighX; i++)
		{
			for (int j = LowY; j <= HighY; j++)
			{
				if (Main.tile[i, j] != null && Main.tile[i, j].HasTile && (Main.tile[i, j].TileType == ModContent.TileType<Tiles.DemonSpikescale>() || Main.tile[i, j].TileType == ModContent.TileType<Tiles.BloodiedSpike>() || Main.tile[i, j].TileType == ModContent.TileType<Tiles.NastySpike>()))
				{
					return true;
				}
			}
		}
		return false;
	}
	public override void PostAI(NPC npc)
	{
		npc.GetGlobalNPC<AvalonGlobalNPCInstance>().SpikeTimer++;
		if (npc.GetGlobalNPC<AvalonGlobalNPCInstance>().SpikeTimer >= 60)
		{
			if (!npc.townNPC && npc.lifeMax > 5 && !npc.dontTakeDamage && !npc.noTileCollide && SpikeCollision2(npc.position, npc.width, npc.height))
			{
				NPC.HitInfo hit = new NPC.HitInfo();
				hit.Damage = 30 + (int)(npc.defense / 2);
				hit.HitDirection = 0;
				hit.Knockback = 0;
				npc.StrikeNPC(hit);
				npc.GetGlobalNPC<AvalonGlobalNPCInstance>().SpikeTimer = 0;
			}
		}

		#region platform leaf
		Point tile = npc.position.ToTileCoordinates() + new Point(0, npc.height / 16 + 1);
		Point tile2 = npc.position.ToTileCoordinates() + new Point(npc.width / 16, npc.height / 16 + 1);
		if (WorldGen.InWorld(tile.X, tile.Y) && WorldGen.InWorld(tile2.X, tile2.Y))
		{
			int xpos;
			int ypos;
			for (xpos = Main.tile[tile.X, tile.Y].TileFrameX / 18; xpos > 2; xpos -= 3) { }
			for (ypos = Main.tile[tile.X, tile.Y].TileFrameY / 18; ypos > 3; ypos -= 4) { }
			xpos = tile.X - xpos;
			ypos = tile.Y - ypos;
			if (npc.velocity.Y > 4.85f && !npc.noGravity && !npc.noTileCollide)
			{
				if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Savanna.PlatformLeaf>() &&
					Main.tile[tile2.X, tile2.Y].TileType == ModContent.TileType<Tiles.Savanna.PlatformLeaf>() && Main.tile[tile.X, tile.Y].TileFrameY < 74)
				{
					for (int i = xpos; i < xpos + 3; i++)
					{
						for (int j = ypos; j < ypos + 4; j++)
						{
							Main.tile[i, j].TileType = (ushort)ModContent.TileType<PlatformLeafCollapsed>();
						}
					}
					SoundStyle s = new SoundStyle("Terraria/Sounds/Grass") { Pitch = -0.8f };
					SoundEngine.PlaySound(s, new Vector2((tile.X + 1) * 16, tile.Y * 16));
					WorldGen.TreeGrowFX(xpos + 1, ypos, 2, ModContent.GoreType<SavannaTreeLeaf>(), true);
				}
			}
		}
		#endregion
	}
	public override bool CheckDead(NPC npc)
	{
		if (npc.townNPC && npc.life <= 0)
		{
			if (Main.netMode == NetmodeID.SinglePlayer)
			{
				Main.NewText(npc.FullName + TownDeathMsg(npc.type), new Color(178, 0, 90));
				npc.life = 0;
				npc.active = false;
				npc.NPCLoot();
				SoundEngine.PlaySound(SoundID.NPCDeath1, npc.position);
			}
			else
			{
				ChatHelper.BroadcastChatMessage(
					NetworkText.FromLiteral(npc.FullName + TownDeathMsg(npc.type)),
					new Color(178, 0, 90));
				NetMessage.SendData(MessageID.DamageNPC, -1, -1, null, npc.whoAmI, -1);
				int t = 0;
				int s = 1;
				switch (npc.type)
				{
					case NPCID.Guide:
						if (npc.GivenName == "Andrew")
						{
							t = ItemID.GreenCap;
						}
						break;
					case NPCID.DyeTrader:
						if (Main.rand.NextBool(8))
						{
							t = ItemID.DyeTradersScimitar;
						}
						break;
					case NPCID.Painter:
						if (Main.rand.NextBool(10))
						{
							t = ItemID.PainterPaintballGun;
						}
						break;
					case NPCID.DD2Bartender:
						if (Main.rand.NextBool(8))
						{
							t = ItemID.AleThrowingGlove;
						}
						break;
					case NPCID.Stylist:
						if (Main.rand.NextBool(8))
						{
							t = ItemID.StylistKilLaKillScissorsIWish;
						}
						break;
					case NPCID.Clothier:
						t = ItemID.RedHat;
						break;
					case NPCID.PartyGirl:
						if (Main.rand.NextBool(4))
						{
							t = ItemID.PartyGirlGrenade;
							s = Main.rand.Next(30, 61);
						}
						break;
					case NPCID.TaxCollector:
						if (Main.rand.NextBool(8))
						{
							t = 3351;
						}
						break;
					case NPCID.TravellingMerchant:
						t = ItemID.PeddlersHat;
						break;
					case NPCID.Princess:
						t = ItemID.PrincessWeapon;
						break;
				}
				if (t > 0)
				{
					int a = Item.NewItem(npc.GetSource_Loot(), npc.position, 16, 16, t, s);
					NetMessage.SendData(MessageID.SyncItem, -1, -1, NetworkText.Empty, a);
				}
				// Main.npc[npc.whoAmI].NPCLoot();
				SoundEngine.PlaySound(SoundID.NPCDeath1, npc.position);
			}
			return false;
		}

		return base.CheckDead(npc);
	}
	public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		if (npc.netID is 529 or 533)
		{
			bestiaryEntry.Info.Add(new ModBiomeBestiaryInfoElement(Mod, ModContent.GetInstance<ContagionCaveDesert>().DisplayName.Value, ModContent.GetInstance<ContagionCaveDesert>().BestiaryIcon, "Assets/Bestiary/ContagionBG", null));
		}
	}
	#region commented out incase i missed something
	//public override void ModifyGlobalLoot(GlobalLoot globalLoot)
	//{
	//    var desertPostBeakCondition = new DesertPostBeakDrop();

	//    globalLoot.Add(ItemDropRule.ByCondition(desertPostBeakCondition, ModContent.ItemType<AncientTitaniumHeadgear>(), 150));
	//    globalLoot.Add(ItemDropRule.ByCondition(desertPostBeakCondition, ModContent.ItemType<AncientTitaniumPlateMail>(), 150));
	//    globalLoot.Add(ItemDropRule.ByCondition(desertPostBeakCondition, ModContent.ItemType<AncientTitaniumGreaves>(), 150));
	//}
	//public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
	//{
	//    var hardModeCondition = new HardmodeOnly();
	//    var notFromStatueCondition = new Conditions.NotFromStatue();
	//    var notExpertCondition = new Conditions.NotExpert();

	//    var preHardModeCondition = new Invert(hardModeCondition);
	//    var superHardModeCondition = new Superhardmode();
	//    var hardmodePreSuperHardmodeCondition =
	//        new Combine(true, null, hardModeCondition, new Invert(new Superhardmode()));

	//    int p = Player.FindClosest(npc.position, npc.width, npc.height);

	//    switch (npc.type)
	//    {

	//        case NPCID.BoneSerpentHead:
	//            npcLoot.Add(ItemDropRule.Common(ItemID.Sunfury, 20));
	//            break;
	//        case NPCID.GoblinThief:
	//            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GoblinDagger>(), 100));
	//            break;
	//    }

	//    if (npc.type is NPCID.BloodZombie or NPCID.Drippler)
	//    {
	//        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SanguineKatana>(), 30));
	//        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BloodBarrage>(), 30));
	//        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Vanity.SanguineKabuto>(), 30));
	//    }

	//    #region shards
	//    if (Data.Sets.NPC.Toxic[npc.type])
	//    {
	//        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ToxinShard>(), 8));
	//    }

	//    if (Data.Sets.NPC.Undead[npc.type])
	//    {
	//        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<UndeadShard>(), 11));
	//    }

	//    if (Data.Sets.NPC.Fiery[npc.type])
	//    {
	//        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FireShard>(), 8));
	//    }

	//    if (Data.Sets.NPC.Watery[npc.type])
	//    {
	//        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WaterShard>(), 8));
	//    }

	//    if (Data.Sets.NPC.Earthen[npc.type])
	//    {
	//        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EarthShard>(), 12));
	//    }

	//    if (Data.Sets.NPC.Flyer[npc.type])
	//    {
	//        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BreezeShard>(), 13));
	//    }

	//    if (Data.Sets.NPC.Frozen[npc.type])
	//    {
	//        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FrostShard>(), 10));
	//    }

	//    if (Data.Sets.NPC.Wicked[npc.type])
	//    {
	//        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CorruptShard>(), 9));
	//    }

	//    if (Data.Sets.NPC.Arcane[npc.type])
	//    {
	//        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ArcaneShard>(), 7));
	//    }


	//    #endregion shards


	//}
	#endregion commented out incase i missed something
}
