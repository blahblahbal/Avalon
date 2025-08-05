using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Avalon.DropConditions;
using Avalon.Hooks;
using Avalon.Items.Accessories.Expert;
using Avalon.Items.Accessories.Hardmode;
using Avalon.Items.Accessories.PreHardmode;
using Avalon.Items.Ammo;
using Avalon.Items.Armor.PreHardmode;
using Avalon.Items.Consumables;
using Avalon.Items.Food;
using Avalon.Items.Material;
using Avalon.Items.Material.Bars;
using Avalon.Items.Material.Herbs;
using Avalon.Items.Material.Ores;
using Avalon.Items.Other;
using Avalon.Items.Pets;
using Avalon.Items.Placeable.Furniture;
using Avalon.Items.Placeable.MusicBoxes;
using Avalon.Items.Placeable.Seed;
using Avalon.Items.Placeable.Tile;
using Avalon.Items.Placeable.Tile.Ancient;
using Avalon.Items.Potions.Buff;
using Avalon.Items.Potions.Other;
using Avalon.Items.Tools.Hardmode;
using Avalon.Items.Tools.PreHardmode;
using Avalon.Items.Tools.Superhardmode;
using Avalon.Items.Weapons.Magic.PreHardmode;
using Avalon.Items.Weapons.Melee.PreHardmode;
using Avalon.ModSupport.MLL.Dusts;
using Avalon.ModSupport.MLL.Items;
using Avalon.ModSupport.MLL.Liquids;
using Avalon.Prefixes;
using Avalon.Reflection;
using Avalon.Tiles;
using Microsoft.Xna.Framework;
using ModLiquidLib.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Avalon.Common;

public class AvalonGlobalItem : GlobalItem
{
	public static int ShroomiteAmmoCounter = 0;

	private static readonly List<int> nonSolidExceptions = new()
	{
		TileID.Cobweb,
		TileID.LivingCursedFire,
		TileID.LivingDemonFire,
		TileID.LivingFire,
		TileID.LivingFrostFire,
		TileID.LivingIchor,
		TileID.LivingUltrabrightFire,
		TileID.ChimneySmoke,
		TileID.Bubble,
		TileID.Rope,
		TileID.SilkRope,
		TileID.VineRope,
		TileID.WebRope,
		ModContent.TileType<LivingLightning>(),
		ModContent.TileType<LivingPathogen>(),
        //ModContent.TileType<VineRope>(),
    };
	public override void UpdateInventory(Item item, Player player)
	{
		if (item.type == ItemID.GreedyRing || item.type == ItemID.DiscountCard)
		{
			player.discountEquipped = true;
		}
	}

	public override void OnSpawn(Item item, IEntitySource source)
	{
		if (source is EntitySource_ItemOpen newSource)
		{
			if (!ItemID.Sets.BossBag[newSource.ItemType])
			{
				if (newSource.Player.GetModPlayer<AvalonPlayer>().DupeLoot && Main.rand.NextBool(30))
				{
					Item.NewItem(new EntitySource_OverfullInventory(newSource.Player), item.position, item.type, item.stack);
				}
				if (newSource.Player.GetModPlayer<AvalonPlayer>().AdvDupeLoot && Main.rand.NextBool(20))
				{
					Item.NewItem(new EntitySource_OverfullInventory(newSource.Player), item.position, item.type, item.stack);
				}
			}
		}
		if (source is EntitySource_Loot loot)
		{
			if (loot.Entity is NPC npc)
			{
				if (Main.player[npc.lastInteraction].GetModPlayer<AvalonPlayer>().DupeLoot && Main.rand.NextBool(30) && !npc.boss)
				{
					Item.NewItem(new EntitySource_DropAsItem(npc), item.position, item.type, item.stack);
				}
				if (Main.player[npc.lastInteraction].GetModPlayer<AvalonPlayer>().AdvDupeLoot && Main.rand.NextBool(20) && !npc.boss)
				{
					Item.NewItem(new EntitySource_DropAsItem(npc), item.position, item.type, item.stack);
				}
			}
		}
	}
	public override void SetStaticDefaults()
	{
		#region Shimmer
		// ores
		ItemID.Sets.ShimmerTransformToItem[ItemID.ChlorophyteOre] = ModContent.ItemType<TroxiniumOre>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<TroxiniumOre>()] = ItemID.TitaniumOre;
		ItemID.Sets.ShimmerTransformToItem[ItemID.AdamantiteOre] = ModContent.ItemType<NaquadahOre>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<NaquadahOre>()] = ItemID.OrichalcumOre;
		ItemID.Sets.ShimmerTransformToItem[ItemID.MythrilOre] = ModContent.ItemType<DurataniumOre>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<DurataniumOre>()] = ItemID.PalladiumOre;
		ItemID.Sets.ShimmerTransformToItem[ItemID.CobaltOre] = ModContent.ItemType<IridiumOre>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<IridiumOre>()] = ModContent.ItemType<OsmiumOre>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<OsmiumOre>()] = ModContent.ItemType<RhodiumOre>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<RhodiumOre>()] = ModContent.ItemType<BismuthOre>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<BismuthOre>()] = ItemID.PlatinumOre;
		ItemID.Sets.ShimmerTransformToItem[ItemID.GoldOre] = ModContent.ItemType<ZincOre>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<ZincOre>()] = ItemID.TungstenOre;
		ItemID.Sets.ShimmerTransformToItem[ItemID.SilverOre] = ModContent.ItemType<NickelOre>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<NickelOre>()] = ItemID.LeadOre;
		ItemID.Sets.ShimmerTransformToItem[ItemID.IronOre] = ModContent.ItemType<BronzeOre>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<BronzeOre>()] = ItemID.TinOre;
		// end ores

		// ancient bricks
		ItemID.Sets.ShimmerTransformToItem[ItemID.IronBrick] = ModContent.ItemType<AncientIronBrick>();
		ItemID.Sets.ShimmerTransformToItem[ItemID.AdamantiteBeam] = ModContent.ItemType<AncientAdamantiteBrick>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Items.Placeable.Tile.OrangeBrick>()] = ModContent.ItemType<AncientOrangeBrick>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Items.Placeable.Tile.PurpleBrick>()] = ModContent.ItemType<AncientPurpleBrick>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Items.Placeable.Tile.YellowBrick>()] = ModContent.ItemType<AncientYellowBrick>();
		// end ancient bricks

		ItemID.Sets.ShimmerTransformToItem[ItemID.CrimstoneBlock] = ModContent.ItemType<ChunkstoneBlock>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<ChunkstoneBlock>()] = ItemID.EbonstoneBlock;

		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Items.Placeable.Tile.ApocalyptusWood>()] = ItemID.Wood;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Coughwood>()] = ItemID.Wood;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Items.Placeable.Tile.BleachedEbony>()] = ItemID.Wood;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Items.Placeable.Tile.ResistantWood>()] = ItemID.Wood;

		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<UnderworldKey>()] = ModContent.ItemType<UnderworldChest>();

		// weapons
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<DesertLongsword>()] = ItemID.AntlionClaw;
		ItemID.Sets.ShimmerTransformToItem[ItemID.AntlionClaw] = ModContent.ItemType<DesertLongsword>();

		//ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<AncientDartRifle>()] = ItemID.DartRifle;
		//ItemID.Sets.ShimmerTransformToItem[ItemID.DartRifle] = ModContent.ItemType<AncientDartRifle>();

		//ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<AncientDartPistol>()] = ItemID.DartPistol;
		//ItemID.Sets.ShimmerTransformToItem[ItemID.DartPistol] = ModContent.ItemType<AncientDartPistol>();

		//ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<AncientDartShotgun>()] = ModContent.ItemType<DartShotgun>();
		//ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<DartShotgun>()] = ModContent.ItemType<AncientDartShotgun>();
		// end weapons

		// misc items
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<StaminaCrystal>()] = ModContent.ItemType<EnergyCrystal>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<UnstableCatalyzer>()] = ModContent.ItemType<Items.Placeable.Crafting.Catalyzer>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<ContagionKey>()] = ModContent.ItemType<ContagionChest>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<PlagueCrate>()] = ModContent.ItemType<ContagionCrate>();

		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Zircon>()] = ItemID.Diamond;
		ItemID.Sets.ShimmerTransformToItem[ItemID.Diamond] = ItemID.Ruby;
		ItemID.Sets.ShimmerTransformToItem[ItemID.Ruby] = ModContent.ItemType<Peridot>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Peridot>()] = ItemID.Emerald;
		ItemID.Sets.ShimmerTransformToItem[ItemID.Emerald] = ItemID.Sapphire;
		ItemID.Sets.ShimmerTransformToItem[ItemID.Diamond] = ModContent.ItemType<Tourmaline>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Tourmaline>()] = ItemID.Topaz;
		ItemID.Sets.ShimmerTransformToItem[ItemID.Topaz] = ItemID.Amethyst;

		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Items.Placeable.Wall.ImperviousBrickWallItem>()] = ModContent.ItemType<Items.Placeable.Wall.ImperviousBrickWallUnsafe>();
		// end misc items

		// equipment
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<ArgusLantern>()] = ItemID.MagicLantern;
		ItemID.Sets.ShimmerTransformToItem[ItemID.MagicLantern] = ModContent.ItemType<ArgusLantern>();

		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<HiddenBlade>()] = ModContent.ItemType<AmmoMagazine>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<AmmoMagazine>()] = ModContent.ItemType<HiddenBlade>();

		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<GreekExtinguisher>()] = ModContent.ItemType<SixHundredWattLightbulb>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<SixHundredWattLightbulb>()] = ModContent.ItemType<GreekExtinguisher>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Windshield>()] = ModContent.ItemType<SurgicalMask>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<SurgicalMask>()] = ModContent.ItemType<Windshield>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<GoldenShield>()] = ModContent.ItemType<RubberBoot>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<RubberBoot>()] = ModContent.ItemType<GoldenShield>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<OxygenTank>()] = ModContent.ItemType<Vortex>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Vortex>()] = ModContent.ItemType<OxygenTank>();

		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<RhodiumHeadgear>()] = ModContent.ItemType<AncientTitaniumHeadgear>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<AncientTitaniumHeadgear>()] = ModContent.ItemType<RhodiumHeadgear>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<RhodiumPlateMail>()] = ModContent.ItemType<AncientTitaniumPlateMail>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<AncientTitaniumPlateMail>()] = ModContent.ItemType<RhodiumPlateMail>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<RhodiumGreaves>()] = ModContent.ItemType<AncientTitaniumGreaves>();
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<AncientTitaniumGreaves>()] = ModContent.ItemType<RhodiumGreaves>();
		// end equipment

		// ambrosia
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Blackberry>()] = ItemID.Ambrosia;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Durian>()] = ItemID.Ambrosia;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Mangosteen>()] = ItemID.Ambrosia;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Medlar>()] = ItemID.Ambrosia;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Raspberry>()] = ItemID.Ambrosia;
		// end ambrosia

		// music boxes
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<MusicBoxArmageddonSlime>()] = ItemID.MusicBox;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<MusicBoxBacteriumPrime>()] = ItemID.MusicBox;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<MusicBoxContagion>()] = ItemID.MusicBox;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<MusicBoxDarkMatter>()] = ItemID.MusicBox;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<MusicBoxDesertBeak>()] = ItemID.MusicBox;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<MusicBoxDesertBeakOtherworldly>()] = ItemID.MusicBox;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<MusicBoxHellCastle>()] = ItemID.MusicBox;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<MusicBoxPhantasm>()] = ItemID.MusicBox;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<MusicBoxSkyFortress>()] = ItemID.MusicBox;
		//ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<MusicBoxTropics>()] = ItemID.MusicBox;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<MusicBoxTuhrtlOutpost>()] = ItemID.MusicBox;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<MusicBoxUndergroundContagion>()] = ItemID.MusicBox;
		// end music boxes

		// torches
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<BrownTorch>()] = ItemID.ShimmerTorch;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<CyanTorch>()] = ItemID.ShimmerTorch;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<LimeTorch>()] = ItemID.ShimmerTorch;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<ContagionTorch>()] = ItemID.ShimmerTorch;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<PathogenTorch>()] = ItemID.ShimmerTorch;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<SlimeTorch>()] = ItemID.ShimmerTorch;
		// end torches
		#endregion Shimmer

		Item.staff[ItemID.Vilethorn] = true;
		Item.staff[ItemID.HiveWand] = true;
		ContentSamples.ItemsByType[ItemID.HiveWand].GetPrefixCategories().AddRange([PrefixCategory.Magic]);
	}
	public override void PostUpdate(Item item)
	{
		if (CollisionHooks.AcidCollision(item.position, item.width, item.height))
		{
			SoundEngine.PlaySound(SoundID.LiquidsWaterLava, item.position);
			SoundEngine.PlaySound(SoundID.SplashWeak, item.position);

			for (int n = 0; n < 5; n++)
			{
				int num8 = Dust.NewDust(new Vector2(item.position.X - 6f, item.position.Y + item.height / 2 - 8f), item.width + 12, 24, ModContent.DustType<AcidLiquidSplash>());
				Main.dust[num8].velocity.Y -= 1.5f;
				Main.dust[num8].velocity.X *= 2.5f;
				Main.dust[num8].scale = 1.3f;
				Main.dust[num8].alpha = 100;
				Main.dust[num8].noGravity = true;
			}
			if (item.rare == ItemRarityID.White && !ItemID.Sets.IsLavaImmuneRegardlessOfRarity[item.type])
			{
				item.TurnToAir();
			}
		}
		//if (item.lavaWet && item.position.Y / 16 > Main.maxTilesY - 190)
		//{
		//	if (item.type == ModContent.ItemType<HellboundRemote>() && Main.hardMode) // &&
		//		//ModContent.GetInstance<DownedBossSystem>().DownedPhantasm)
		//	{
		//		if (Main.netMode != NetmodeID.MultiplayerClient)
		//		{
		//			item.active = false;
		//			item.type = ItemID.None;
		//			item.stack = 0;
		//			if (Main.hardMode && ModContent.GetInstance<DownedBossSystem>().DownedPhantasm)
		//			{
		//				AvalonGlobalNPC.SpawnWOS(item.position);
		//				SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/WoS"), item.position);
		//			}

		//			NetMessage.SendData(MessageID.SyncItem, -1, -1, NetworkText.Empty, item.whoAmI);
		//		}
		//	}
		//}
	}
	public override bool CanRightClick(Item item)
	{
		if (Main.mouseRightRelease && Main.mouseRight)
		{
			bool isItemInInventory = false;
			for (int i = 0; i < Main.LocalPlayer.inventory.Length; i++)
			{
				if (item == Main.LocalPlayer.inventory[i])
				{
					isItemInInventory = true;
					break;
				}
			}
			if (Main.LocalPlayer.chest != -1)
			{
				for (int j = 0; j < Main.chest[Main.LocalPlayer.chest].item.Length; j++)
				{
					if (item == Main.chest[Main.LocalPlayer.chest].item[j])
					{
						isItemInInventory = true;
						break;
					}
				}
			}

			if (item.GetGlobalItem<AvalonGlobalItemInstance>().StaminaScroll && isItemInInventory)
			{
				// if the top slot has an item and the bottom slot doesn't, put current item in bottom slot
				if (ModContent.GetInstance<StaminaSlot>().FunctionalItem.type != ItemID.None && ModContent.GetInstance<StaminaSlot2>().FunctionalItem.type == ItemID.None &&
					ModContent.GetInstance<StaminaSlot>().FunctionalItem.type != item.type)
				{
					ModContent.GetInstance<StaminaSlot2>().FunctionalItem.SetDefaults(item.type);
					item.TurnToAir();
					SoundEngine.PlaySound(SoundID.Grab);
					return false;
				}
				// if both slots have an item, swap current item with top slot
				else if (ModContent.GetInstance<StaminaSlot>().FunctionalItem.type != ItemID.None && ModContent.GetInstance<StaminaSlot2>().FunctionalItem.type != ItemID.None)
				{
					Item scrollInSlot = new Item();
					scrollInSlot.SetDefaults(ModContent.GetInstance<StaminaSlot>().FunctionalItem.type);

					ModContent.GetInstance<StaminaSlot>().FunctionalItem.SetDefaults(item.type);
					item.SetDefaults(scrollInSlot.type);

					SoundEngine.PlaySound(SoundID.Grab);
					return false;
				}
			}
			if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome && ModContent.GetInstance<TomeSlot>().FunctionalItem.type != ItemID.None && isItemInInventory)
			{
				Item tomeInSlot = new Item();
				tomeInSlot.SetDefaults(ModContent.GetInstance<TomeSlot>().FunctionalItem.type);

				ModContent.GetInstance<TomeSlot>().FunctionalItem.SetDefaults(item.type);
				item.SetDefaults(tomeInSlot.type);

				SoundEngine.PlaySound(SoundID.Grab);
				return false;
			}
			// genie slot swapping
			if (item.GetGlobalItem<AvalonGlobalItemInstance>().Genie && ModContent.GetInstance<GenieSlot>().FunctionalItem.type != ItemID.None && isItemInInventory)
			{
				Item genieInSlot = new Item();
				genieInSlot.SetDefaults(ModContent.GetInstance<GenieSlot>().FunctionalItem.type);

				ModContent.GetInstance<GenieSlot>().FunctionalItem.SetDefaults(item.type);
				item.SetDefaults(genieInSlot.type);

				SoundEngine.PlaySound(SoundID.Grab);
				return false;
			}
			if (item.type == ModContent.ItemType<Breakdawn>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				int pfix = item.prefix;
				item.ChangeItemType(ModContent.ItemType<Breakdawn3x3>());
				item.Prefix(pfix);
				return false;
			}
			if (item.type == ModContent.ItemType<Breakdawn3x3>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				int pfix = item.prefix;
				item.ChangeItemType(ModContent.ItemType<Breakdawn>());
				item.Prefix(pfix);
				return false;
			}
			if (item.type == ModContent.ItemType<PickaxeofDusk>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				int pfix = item.prefix;
				item.ChangeItemType(ModContent.ItemType<PickaxeofDusk3x3>());
				item.Prefix(pfix);
				return false;
			}
			if (item.type == ModContent.ItemType<PickaxeofDusk3x3>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				int pfix = item.prefix;
				item.ChangeItemType(ModContent.ItemType<PickaxeofDusk>());
				item.Prefix(pfix);
				return false;
			}
			if (item.type == ModContent.ItemType<AccelerationPickaxe>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				int pfix = item.prefix;
				item.ChangeItemType(ModContent.ItemType<AccelerationPickaxeSpeed>());
				item.Prefix(pfix);
				return false;
			}
			if (item.type == ModContent.ItemType<AccelerationPickaxeSpeed>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				int pfix = item.prefix;
				item.ChangeItemType(ModContent.ItemType<AccelerationPickaxe>());
				item.Prefix(pfix);
				return false;
			}
			if (item.type == ModContent.ItemType<Items.Tools.Superhardmode.AccelerationDrill>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				int pfix = item.prefix;
				item.ChangeItemType(ModContent.ItemType<AccelerationDrillSpeed>());
				item.Prefix(pfix);
				return false;
			}
			if (item.type == ModContent.ItemType<AccelerationDrillSpeed>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				int pfix = item.prefix;
				item.ChangeItemType(ModContent.ItemType<Items.Tools.Superhardmode.AccelerationDrill>());
				item.Prefix(pfix);
				return false;
			}
			if (item.type == ModContent.ItemType<ManipulatorTime>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				item.ChangeItemType(ModContent.ItemType<ManipulatorMoon>());
				return false;
			}
			if (item.type == ModContent.ItemType<ManipulatorMoon>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				item.ChangeItemType(ModContent.ItemType<ManipulatorRain>());
				return false;
			}
			if (item.type == ModContent.ItemType<ManipulatorRain>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				item.ChangeItemType(ModContent.ItemType<ManipulatorSand>());
				return false;
			}
			if (item.type == ModContent.ItemType<ManipulatorSand>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				item.ChangeItemType(ModContent.ItemType<ManipulatorTime>());
				return false;
			}
			if (item.type == ModContent.ItemType<ShadowPhone>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				item.ChangeItemType(ModContent.ItemType<ShadowPhoneSurface>());
				return false;
			}
			if (item.type == ModContent.ItemType<ShadowPhoneSurface>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				item.ChangeItemType(ModContent.ItemType<ShadowPhoneHome>());
				return false;
			}
			if (item.type == ModContent.ItemType<ShadowPhoneHome>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				item.ChangeItemType(ModContent.ItemType<ShadowPhoneDungeon>());
				return false;
			}
			if (item.type == ModContent.ItemType<ShadowPhoneDungeon>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				item.ChangeItemType(ModContent.ItemType<ShadowPhoneJungleTropics>());
				return false;
			}
			if (item.type == ModContent.ItemType<ShadowPhoneJungleTropics>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				item.ChangeItemType(ModContent.ItemType<ShadowPhoneOcean>());
				return false;
			}
			if (item.type == ModContent.ItemType<ShadowPhoneOcean>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				item.ChangeItemType(ModContent.ItemType<ShadowPhoneHell>());
				return false;
			}
			if (item.type == ModContent.ItemType<ShadowPhoneHell>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				item.ChangeItemType(ModContent.ItemType<ShadowPhoneRandom>());
				return false;
			}
			if (item.type == ModContent.ItemType<ShadowPhoneRandom>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				item.ChangeItemType(ModContent.ItemType<ShadowPhone>());
				return false;
			}
		}
		return base.CanRightClick(item);
	}
	public override bool IsAnglerQuestAvailable(int type)
	{
		if (ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Contagion)
		{
			if (type == ItemID.BloodyManowar || type == ItemID.Cursedfish || type == ItemID.EaterofPlankton ||
				type == ItemID.Ichorfish || type == ItemID.InfectedScabbardfish)
				return false;
		}
		return base.IsAnglerQuestAvailable(type);
	}
	public override void ExtractinatorUse(int extractType, int extractinatorBlockType, ref int resultType, ref int resultStack)
	{
		if (extractType == 0 || extractType == ItemID.DesertFossil)
		{
			if (resultType is ItemID.CopperOre or ItemID.TinOre)
			{
				if (Main.rand.NextBool(3))
				{
					resultType = ModContent.ItemType<BronzeOre>();
				}
			}
			if (resultType is ItemID.IronOre or ItemID.LeadOre)
			{
				if (Main.rand.NextBool(3))
				{
					resultType = ModContent.ItemType<NickelOre>();
				}
			}
			if (resultType is ItemID.SilverOre or ItemID.TungstenOre)
			{
				if (Main.rand.NextBool(3))
				{
					resultType = ModContent.ItemType<ZincOre>();
				}
			}
			if (resultType is ItemID.GoldOre or ItemID.PlatinumOre)
			{
				if (Main.rand.NextBool(3))
				{
					resultType = ModContent.ItemType<BismuthOre>();
				}
			}
			if (resultType is ItemID.Amber or ItemID.Topaz or ItemID.Amethyst or ItemID.Sapphire or ItemID.Emerald or ItemID.Ruby or ItemID.Diamond)
			{
				if (Main.rand.NextBool(7))
				{
					resultType = ModContent.ItemType<RhodiumOre>();
				}
				else if (Main.rand.NextBool(7))
				{
					resultType = ModContent.ItemType<OsmiumOre>();
				}
				else if (Main.rand.NextBool(7))
				{
					resultType = ModContent.ItemType<IridiumOre>();
				}
				else if (Main.rand.NextBool(7))
				{
					resultType = ModContent.ItemType<Heartstone>();
				}
				else if (Main.rand.NextBool(7))
				{
					resultType = ModContent.ItemType<Starstone>();
				}
				else if (Main.rand.NextBool(7))
				{
					resultType = ModContent.ItemType<Boltstone>();
				}
			}
			if (resultType is ItemID.Topaz or ItemID.Amethyst)
			{
				if (Main.rand.NextBool(3))
				{
					resultType = ModContent.ItemType<Tourmaline>();
				}
			}
			if (resultType is ItemID.Sapphire or ItemID.Emerald)
			{
				if (Main.rand.NextBool(3))
				{
					resultType = ModContent.ItemType<Peridot>();
				}
			}
			if (resultType is ItemID.Ruby or ItemID.Diamond)
			{
				if (Main.rand.NextBool(3))
				{
					resultType = ModContent.ItemType<Zircon>();
				}
			}
		}
	}
	public override void ModifyWeaponCrit(Item item, Player player, ref float crit)
	{
		Item curItem = item;

		Item storedItem = new Item();
		storedItem.SetDefaults(curItem.type);

		float cMagic = 0f;
		float cMelee = 0f;
		float cRanged = 0f;
		if (storedItem.DamageType == DamageClass.Magic)
		{
			cMagic = item.crit + player.GetTotalCritChance(DamageClass.Magic);
		}
		if (storedItem.DamageType == DamageClass.Melee)
		{
			cMelee = item.crit + player.GetTotalCritChance(DamageClass.Melee);
		}
		if (storedItem.DamageType == DamageClass.Ranged)
		{
			cRanged = item.crit + player.GetTotalCritChance(DamageClass.Ranged);
		}

		// magic
		if (player.GetModPlayer<AvalonPlayer>().MaxMagicCrit < 100)
		{
			if (item.CountsAsClass(DamageClass.Magic))
			{
				if (crit > player.GetModPlayer<AvalonPlayer>().MaxMagicCrit)
					crit = player.GetModPlayer<AvalonPlayer>().MaxMagicCrit;
			}
		}
		else
		{
			if (item.DamageType == DamageClass.Magic)
			{
				crit = (int)cMagic;
			}
		}
		// melee
		if (player.GetModPlayer<AvalonPlayer>().MaxMeleeCrit < 100)
		{
			if (item.CountsAsClass(DamageClass.Melee))
			{
				if (crit > player.GetModPlayer<AvalonPlayer>().MaxMeleeCrit)
					crit = player.GetModPlayer<AvalonPlayer>().MaxMeleeCrit;
			}
		}
		else
		{
			if (item.DamageType == DamageClass.Melee)
			{
				crit = (int)cMelee;
			}
		}
		// ranged
		if (player.GetModPlayer<AvalonPlayer>().MaxRangedCrit < 100)
		{
			if (item.CountsAsClass(DamageClass.Ranged))
			{
				if (crit > player.GetModPlayer<AvalonPlayer>().MaxRangedCrit)
					crit = player.GetModPlayer<AvalonPlayer>().MaxRangedCrit;
			}
		}
		else
		{
			if (item.DamageType == DamageClass.Ranged)
			{
				crit = (int)cRanged;
			}
		}
	}
	public override void HoldItem(Item item, Player player)
	{
		// Tile interactions
		#region Tile interactions
		if (Main.myPlayer == player.whoAmI) // Make sure we're only doing all of this for the player holding the item, syncing manually
		{
			Point tilePos = Main.MouseWorld.ToTileCoordinates();
			Tile tile = Main.tile[tilePos];
			Tile tileSafe = Framing.GetTileSafely(tilePos);

			#region sponges 3x3
			if (Main.mouseRight && !Main.mouseLeft && player.cursorItemIconID == 0 && !player.mouseInterface &&
				player.IsInTileInteractionRange(tilePos.X, tilePos.Y, TileReachCheckSettings.Simple) &&
				(item.type == ItemID.SuperAbsorbantSponge || item.type == ItemID.LavaAbsorbantSponge ||
				item.type == ItemID.HoneyAbsorbantSponge || item.type == ItemID.UltraAbsorbantSponge ||
				item.type == ModContent.ItemType<BloodAbsorbantSponge>()))
			{
				for (int i = tilePos.X - 1; i <= tilePos.X + 1; i++)
				{
					for (int j = tilePos.Y - 1; j <= tilePos.Y + 1; j++)
					{
						Tile tile2 = Main.tile[i, j];
						if ((item.type == ItemID.SuperAbsorbantSponge && (tile2.LiquidType == LiquidID.Water || tile2.LiquidType == LiquidID.Shimmer)) ||
							item.type == ItemID.LavaAbsorbantSponge && tile2.LiquidType == LiquidID.Lava ||
							item.type == ItemID.HoneyAbsorbantSponge && tile2.LiquidType == LiquidID.Honey ||
							item.type == ModContent.ItemType<BloodAbsorbantSponge>() && tile2.LiquidType == LiquidLoader.LiquidType<Blood>() ||
							item.type == ItemID.UltraAbsorbantSponge)
						{
							SoundEngine.PlaySound(SoundID.SplashWeak, player.position);

							tile2.LiquidAmount = 0;
							WorldGen.SquareTileFrame(i, j, true);
							if (Main.netMode == NetmodeID.MultiplayerClient)
							{
								NetMessage.sendWater(i, j);
							}
						}
					}
				}
			}
			#endregion

			#region right click bottomless buckets to do 3x3
			if (Main.mouseRight && !Main.mouseLeft && player.cursorItemIconID == 0 && !player.mouseInterface &&
				player.IsInTileInteractionRange(tilePos.X, tilePos.Y, TileReachCheckSettings.Simple) &&
				(item.type == ItemID.BottomlessBucket || item.type == ItemID.BottomlessLavaBucket ||
				item.type == ItemID.BottomlessHoneyBucket || item.type == ItemID.BottomlessShimmerBucket ||
				item.type == ModContent.ItemType<BottomlessBloodBucket>() ||
				item.type == ModContent.ItemType<BottomlessAcidBucket>()))
			{
				int liquidID = LiquidID.Water;
				if (item.type == ItemID.BottomlessLavaBucket) liquidID = LiquidID.Lava;
				if (item.type == ItemID.BottomlessHoneyBucket) liquidID = LiquidID.Honey;
				if (item.type == ItemID.BottomlessShimmerBucket) liquidID = LiquidID.Shimmer;
				if (item.type == ModContent.ItemType<BottomlessBloodBucket>()) liquidID = LiquidLoader.LiquidType<Blood>();
				if (item.type == ModContent.ItemType<BottomlessAcidBucket>()) liquidID = LiquidLoader.LiquidType<Acid>();

				for (int i = tilePos.X - 1; i <= tilePos.X + 1; i++)
				{
					for (int j = tilePos.Y - 1; j <= tilePos.Y + 1; j++)
					{
						Tile tile2 = Main.tile[i, j];
						if (tile2.LiquidType != liquidID && tile2.LiquidAmount > 0)
						{
							return;
						}

						SoundEngine.PlaySound(SoundID.SplashWeak, player.position);

						tile2.LiquidType = liquidID;
						tile2.LiquidAmount = 255;
						WorldGen.SquareTileFrame(i, j, true);
						if (Main.netMode == NetmodeID.MultiplayerClient)
						{
							NetMessage.sendWater(i, j);
						}
					}
				}
			}
			#endregion

			#region shimmer bucket
			if (item.type == ItemID.EmptyBucket)
			{
				int liquidAmount = tile.LiquidAmount;

				if (player.IsInTileInteractionRange(tilePos.X, tilePos.Y, TileReachCheckSettings.Simple) && !tile.HasTile &&
					liquidAmount > 200 && tile.LiquidType == LiquidID.Shimmer)
				{
					player.cursorItemIconEnabled = true;
					player.cursorItemIconID = item.type;
					if (player.itemTime == 0 && player.itemAnimation > 0 && player.controlUseItem)
					{
						if (liquidAmount > 100 && tile.LiquidType == LiquidID.Shimmer)
						{
							SoundEngine.PlaySound(SoundID.SplashWeak, player.position);
							item.stack--;
							player.PutItemInInventoryFromItemUsage(ModContent.ItemType<ShimmerBucket>(), player.selectedItem);
							player.itemTime = player.inventory[player.selectedItem].useTime;

							tile.LiquidAmount = 0;
							tile.LiquidType = LiquidID.Water;
							WorldGen.SquareTileFrame(tilePos.X, tilePos.Y, resetFrame: false);
							if (Main.netMode == NetmodeID.MultiplayerClient)
							{
								NetMessage.sendWater(tilePos.X, tilePos.Y);
							}
							else
							{
								Liquid.AddWater(tilePos.X, tilePos.Y);
							}

							if (liquidAmount >= 255)
							{
								return;
							}

							for (int i = tilePos.X - 1; i <= tilePos.Y + 1; i++)
							{
								for (int j = tilePos.X - 1; j <= tilePos.Y + 1; j++)
								{
									Tile tile2 = Main.tile[i, j];
									if ((i != tilePos.X || j != tilePos.Y) && tile2.LiquidAmount > 0 && tile2.LiquidType == LiquidID.Shimmer)
									{
										int liquidAmount2 = tile2.LiquidAmount;
										if (liquidAmount2 + liquidAmount > 255)
											liquidAmount2 = 255 - liquidAmount;

										liquidAmount += liquidAmount2;
										tile2.LiquidAmount -= (byte)liquidAmount2;
										tile2.LiquidType = LiquidID.Shimmer;
										if (tile2.LiquidAmount == 0)
										{
											tile2.LiquidType = LiquidID.Water;
										}

										WorldGen.SquareTileFrame(i, j, resetFrame: false);
										if (Main.netMode == NetmodeID.MultiplayerClient)
										{
											NetMessage.sendWater(i, j);
										}
										else
										{
											Liquid.AddWater(i, j);
										}
									}
								}
							}
						}
					}
				}
			}
			#endregion

			#region prefix removal
			if (item.prefix > 0)
			{
				Tile tilePosSafe = Framing.GetTileSafely(tilePos);
				if (player.IsInTileInteractionRange(tilePos.X, tilePos.Y, TileReachCheckSettings.Simple) &&
					tilePosSafe.TileType == ModContent.TileType<Tiles.Furniture.Grindstone>())
				{
					player.noThrow = 2;
					player.cursorItemIconEnabled = true;
					player.cursorItemIconID = item.type;
					if (Main.mouseRight && Main.mouseRightRelease)
					{
						if (ContentSamples.ItemsByType[item.type].value < item.value)
						{
							int money = (int)(item.value / 4 * Main.rand.NextFloat(0.5f, 1.5f)) / 5;
							ClassExtensions.DropCoinsProperly(money, (int)player.position.X, (int)player.position.Y);
						}
						SoundEngine.PlaySound(SoundID.Item37);
						item.ResetPrefix();
						if (Main.mouseItem.type == item.type)
						{
							Main.mouseItem.ResetPrefix();
						}
					}
				}
			}
			#endregion

			#region herb seed block swap
			/*if (Data.Sets.Item.HerbSeeds[item.type])
			{
				Point mpTile = player.GetModPlayer<AvalonPlayer>().MousePosition.ToTileCoordinates();

				if ((Main.tile[mpTile.X, mpTile.Y].TileType == TileID.BloomingHerbs ||
					 (Main.tile[mpTile.X, mpTile.Y].TileType == ModContent.TileType<Tiles.Herbs.Barfbush>() &&
					  Main.tile[mpTile.X, mpTile.Y].TileFrameX == 36) ||
					 (Main.tile[mpTile.X, mpTile.Y].TileType == ModContent.TileType<Tiles.Herbs.Bloodberry>() &&
					  Main.tile[mpTile.X, mpTile.Y].TileFrameX == 36) ||
					 (Main.tile[mpTile.X, mpTile.Y].TileType == ModContent.TileType<Tiles.Herbs.Sweetstem>() &&
					  Main.tile[mpTile.X, mpTile.Y].TileFrameX == 36) ||
					 (Main.tile[mpTile.X, mpTile.Y].TileType == ModContent.TileType<Tiles.Herbs.Holybird>() &&
					  Main.tile[mpTile.X, mpTile.Y].TileFrameX == 36)) &&
					(Main.tile[mpTile.X, mpTile.Y + 1].TileType == TileID.ClayPot ||
					 Main.tile[mpTile.X, mpTile.Y + 1].TileType == TileID.PlanterBox ||
					 Main.tile[mpTile.X, mpTile.Y + 1].TileType == ModContent.TileType<PlanterBoxes>()) && Main.mouseLeft)
				{
					WorldGen.KillTile(mpTile.X, mpTile.Y);
					if (!Main.tile[mpTile.X, mpTile.Y].HasTile && Main.netMode != NetmodeID.SinglePlayer)
					{
						NetMessage.SendData(Terraria.ID.MessageID.TileManipulation, -1, -1, null, 0, mpTile.X, mpTile.Y);
					}

					WorldGen.PlaceTile(mpTile.X, mpTile.Y, item.createTile, style: item.placeStyle);
					if (Main.tile[mpTile.X, mpTile.Y].HasTile && Main.netMode != NetmodeID.SinglePlayer)
					{
						NetMessage.SendData(Terraria.ID.MessageID.TileManipulation, -1, -1, null, 1, mpTile.X, mpTile.Y,
							item.createTile, item.placeStyle);
					}

					item.stack--;
				}
			}*/
			#endregion herb seed block swap
		}
		#endregion Tile interactions
	}
	public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
	{
		if (ItemID.Sets.BossBag[item.type])
		{
			itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<StaminaCrystal>(), 4));
		}
		if (item.type == ItemID.FairyQueenBossBag)
		{
			CommonDrop? d = null;
			foreach (IItemDropRule item2 in itemLoot.Get(false))
			{
				if (item2 is not CommonDrop drop)
				{
					continue;
				}
				d = drop;
				if (d != null)
				{
					if (d.itemId == ItemID.EmpressFlightBooster)
					{
						d.itemId = ModContent.ItemType<CrystalSkull>();
					}
				}
			}

			itemLoot.Add(ItemDropRule.Common(ItemID.EmpressFlightBooster, 3));
		}
		// wooden crate
		if (item.type == ItemID.WoodenCrate)
		{
			itemLoot.Add(new CommonDropNotScalingWithLuck(ModContent.ItemType<StaminaPotion>(), 5, 2, 4));
			AlwaysAtleastOneSuccessDropRule? oneFromRulesRule = null;
			foreach (IItemDropRule item2 in itemLoot.Get(false))
			{
				if (item2 is not AlwaysAtleastOneSuccessDropRule rule1)
				{
					continue;
				}
				oneFromRulesRule = rule1;

				if (oneFromRulesRule != null)
				{
					int[] IDs = new int[4];
					for (int i = 0; i < oneFromRulesRule.rules.Length; i++)
					{
						if (oneFromRulesRule.rules[i] is SequentialRulesNotScalingWithLuckRule drop)
						{
							for (int z = 0; z < drop.rules.Length; z++)
							{
								if (drop.rules[z] is not OneFromRulesRule thing) continue;
								CommonDropNotScalingWithLuck c = (CommonDropNotScalingWithLuck)thing.options[0];
								IDs[z] = c.itemId;
							}
						}
					}
					if (IDs.Contains(ItemID.CopperOre))
					{
						IItemDropRule[] ores = new IItemDropRule[6]
						{
							ItemDropRule.NotScalingWithLuck(ItemID.CopperOre, 1, 4, 15),
							ItemDropRule.NotScalingWithLuck(ItemID.TinOre, 1, 4, 15),
							ItemDropRule.NotScalingWithLuck(ItemID.IronOre, 1, 4, 15),
							ItemDropRule.NotScalingWithLuck(ItemID.LeadOre, 1, 4, 15),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BronzeOre>(), 1, 4, 15),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NickelOre>(), 1, 4, 15)
						};

						IItemDropRule[] bars = new IItemDropRule[6]
						{
							ItemDropRule.NotScalingWithLuck(ItemID.CopperBar, 1, 2, 5),
							ItemDropRule.NotScalingWithLuck(ItemID.TinBar, 1, 2, 5),
							ItemDropRule.NotScalingWithLuck(ItemID.IronBar, 1, 2, 5),
							ItemDropRule.NotScalingWithLuck(ItemID.LeadBar, 1, 2, 5),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BronzeBar>(), 1, 2, 5),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NickelBar>(), 1, 2, 5)
						};

						oneFromRulesRule.rules[3] = ItemDropRule.SequentialRulesNotScalingWithLuck(1, new OneFromRulesRule(7, ores), new OneFromRulesRule(8, bars));
					}
					// add Avalon ores into the list
					/*if (IDs.Contains(ItemID.CopperOre))
                    {
                        IItemDropRule[] ores = new IItemDropRule[6]
                        {
                            ItemDropRule.NotScalingWithLuck(ItemID.CopperOre, 1, 4, 15),
                            ItemDropRule.NotScalingWithLuck(ItemID.TinOre, 1, 4, 15),
                            ItemDropRule.NotScalingWithLuck(ItemID.IronOre, 1, 4, 15),
                            ItemDropRule.NotScalingWithLuck(ItemID.LeadOre, 1, 4, 15),
                            ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BronzeOre>(), 1, 4, 15),
                            ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NickelOre>(), 1, 4, 15)
                        };
                        oneFromRulesRule.rules[3] = new OneFromRulesRule(7, ores);
                        //oneFromRulesRule.rules[3].OnSuccess(new OneFromRulesRule(7, ores));
                    }
                    // add Avalon bars into the list
                    if (IDs.Contains(ItemID.CopperBar))
                    {
                        IItemDropRule[] bars = new IItemDropRule[6]
                        {
                            ItemDropRule.NotScalingWithLuck(ItemID.CopperBar, 1, 2, 5),
                            ItemDropRule.NotScalingWithLuck(ItemID.TinBar, 1, 2, 5),
                            ItemDropRule.NotScalingWithLuck(ItemID.IronBar, 1, 2, 5),
                            ItemDropRule.NotScalingWithLuck(ItemID.LeadBar, 1, 2, 5),
                            ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BronzeBar>(), 1, 2, 5),
                            ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NickelBar>(), 1, 2, 5)
                        };
                        oneFromRulesRule.rules[3] = new OneFromRulesRule(7, bars);
                        //oneFromRulesRule.rules[3].OnSuccess(new OneFromRulesRule(8, bars));
                    }*/
				}
			}
		}
		// pearlwood crate
		if (item.type == ItemID.WoodenCrateHard)
		{
			itemLoot.Add(new CommonDropNotScalingWithLuck(ModContent.ItemType<StaminaPotion>(), 5, 2, 4));
			AlwaysAtleastOneSuccessDropRule? alwaysAtLeast1Success = null;
			foreach (IItemDropRule item2 in itemLoot.Get(false))
			{
				if (item2 is not AlwaysAtleastOneSuccessDropRule rule1)
				{
					continue;
				}
				alwaysAtLeast1Success = rule1;

				if (alwaysAtLeast1Success != null)
				{
					int[] IDs = new int[4];
					for (int i = 0; i < alwaysAtLeast1Success.rules.Length; i++)
					{
						if (alwaysAtLeast1Success.rules[i] is SequentialRulesNotScalingWithLuckRule drop)
						{
							for (int z = 0; z < drop.rules.Length; z++)
							{
								if (drop.rules[z] is SequentialRulesNotScalingWithLuckRule drop2)
								{
									for (int q = 0; q < drop2.rules.Length; q++)
									{
										if (drop2.rules[q] is not OneFromRulesRule thing) continue;

										CommonDropNotScalingWithLuck c = (CommonDropNotScalingWithLuck)thing.options[0];
										IDs[q] = c.itemId;
									}
								}
							}
						}
					}
					//if (IDs.Contains(ItemID.CopperOre))
					{
						IItemDropRule[] ores = new IItemDropRule[9]
						{
							ItemDropRule.NotScalingWithLuck(ItemID.CopperOre, 1, 4, 15),
							ItemDropRule.NotScalingWithLuck(ItemID.TinOre, 1, 4, 15),
							ItemDropRule.NotScalingWithLuck(ItemID.IronOre, 1, 4, 15),
							ItemDropRule.NotScalingWithLuck(ItemID.LeadOre, 1, 4, 15),
							ItemDropRule.NotScalingWithLuck(ItemID.SilverOre, 1, 4, 15),
							ItemDropRule.NotScalingWithLuck(ItemID.TungstenOre, 1, 4, 15),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BronzeOre>(), 1, 4, 15),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NickelOre>(), 1, 4, 15),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ZincOre>(), 1, 4, 15)
						};
						IItemDropRule[] hardmodeOres = new IItemDropRule[3]
						{
							ItemDropRule.NotScalingWithLuck(ItemID.CobaltOre, 1, 4, 15),
							ItemDropRule.NotScalingWithLuck(ItemID.PalladiumOre, 1, 4, 15),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<DurataniumOre>(), 1, 4, 15)
						};
						IItemDropRule[] bars = new IItemDropRule[9]
						{
							ItemDropRule.NotScalingWithLuck(ItemID.CopperBar, 1, 2, 5),
							ItemDropRule.NotScalingWithLuck(ItemID.TinBar, 1, 2, 5),
							ItemDropRule.NotScalingWithLuck(ItemID.IronBar, 1, 2, 5),
							ItemDropRule.NotScalingWithLuck(ItemID.LeadBar, 1, 2, 5),
							ItemDropRule.NotScalingWithLuck(ItemID.SilverBar, 1, 2, 5),
							ItemDropRule.NotScalingWithLuck(ItemID.TungstenBar, 1, 2, 5),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BronzeBar>(), 1, 2, 5),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NickelBar>(), 1, 2, 5),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ZincBar>(), 1, 2, 5)
						};
						IItemDropRule[] hardmodeBars = new IItemDropRule[3]
						{
							ItemDropRule.NotScalingWithLuck(ItemID.CobaltBar, 1, 2, 3),
							ItemDropRule.NotScalingWithLuck(ItemID.PalladiumBar, 1, 2, 3),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<DurataniumBar>(), 1, 2, 3)
						};

						alwaysAtLeast1Success.rules[3] = ItemDropRule.SequentialRulesNotScalingWithLuck(1, ItemDropRule.SequentialRulesNotScalingWithLuck(7, new OneFromRulesRule(2, hardmodeOres), new OneFromRulesRule(1, ores)), ItemDropRule.SequentialRulesNotScalingWithLuck(8, new OneFromRulesRule(2, hardmodeBars), new OneFromRulesRule(1, bars)));
					}
				}
			}
		}

		//  iron crate
		if (item.type == ItemID.IronCrate)
		{
			itemLoot.Add(new CommonDropNotScalingWithLuck(ModContent.ItemType<StaminaPotion>(), 5, 2, 4));
			AlwaysAtleastOneSuccessDropRule? alwaysAtLeast1Success = null;
			foreach (IItemDropRule item2 in itemLoot.Get(false))
			{
				if (item2 is not AlwaysAtleastOneSuccessDropRule rule1)
				{
					continue;
				}
				alwaysAtLeast1Success = rule1;

				if (alwaysAtLeast1Success != null)
				{
					int[] IDs = new int[6];
					for (int i = 0; i < alwaysAtLeast1Success.rules.Length; i++)
					{
						if (alwaysAtLeast1Success.rules[i] is SequentialRulesNotScalingWithLuckRule drop)
						{
							for (int z = 0; z < drop.rules.Length; z++)
							{
								if (drop.rules[z] is not OneFromRulesRule thing) continue;
								CommonDropNotScalingWithLuck c = (CommonDropNotScalingWithLuck)thing.options[0];
								IDs[z] = c.itemId;
							}
						}
					}
					//if (IDs.Contains(ItemID.CopperOre))
					{
						IItemDropRule[] ores = new IItemDropRule[9]
						{
							ItemDropRule.NotScalingWithLuck(ItemID.CopperOre, 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ItemID.TinOre, 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ItemID.IronOre, 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ItemID.LeadOre, 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ItemID.SilverOre, 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ItemID.TungstenOre, 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BronzeOre>(), 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NickelOre>(), 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ZincOre>(), 1, 12, 21)
						};
						IItemDropRule[] bars = new IItemDropRule[9]
						{
							ItemDropRule.NotScalingWithLuck(ItemID.CopperBar, 1, 4, 7),
							ItemDropRule.NotScalingWithLuck(ItemID.TinBar, 1, 4, 7),
							ItemDropRule.NotScalingWithLuck(ItemID.IronBar, 1, 4, 7),
							ItemDropRule.NotScalingWithLuck(ItemID.LeadBar, 1, 4, 7),
							ItemDropRule.NotScalingWithLuck(ItemID.SilverBar, 1, 4, 7),
							ItemDropRule.NotScalingWithLuck(ItemID.TungstenBar, 1, 4, 7),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BronzeBar>(), 1, 4, 7),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NickelBar>(), 1, 4, 7),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ZincBar>(), 1, 4, 7)
						};

						alwaysAtLeast1Success.rules[2] = ItemDropRule.SequentialRulesNotScalingWithLuck(1, new OneFromRulesRule(6, ores), new OneFromRulesRule(4, bars));
					}
				}
			}
		}
		// mythril crate
		if (item.type == ItemID.IronCrateHard)
		{
			itemLoot.Add(new CommonDropNotScalingWithLuck(ModContent.ItemType<StaminaPotion>(), 5, 2, 4));
			AlwaysAtleastOneSuccessDropRule? alwaysAtLeast1Success = null;
			foreach (IItemDropRule item2 in itemLoot.Get(false))
			{
				if (item2 is not AlwaysAtleastOneSuccessDropRule rule1)
				{
					continue;
				}
				alwaysAtLeast1Success = rule1;

				if (alwaysAtLeast1Success != null)
				{
					int[] IDs = new int[4];
					for (int i = 0; i < alwaysAtLeast1Success.rules.Length; i++)
					{
						if (alwaysAtLeast1Success.rules[i] is SequentialRulesNotScalingWithLuckRule drop)
						{
							for (int z = 0; z < drop.rules.Length; z++)
							{
								if (drop.rules[z] is SequentialRulesNotScalingWithLuckRule drop2)
								{
									for (int q = 0; q < drop2.rules.Length; q++)
									{
										if (drop2.rules[q] is not OneFromRulesRule thing) continue;

										CommonDropNotScalingWithLuck c = (CommonDropNotScalingWithLuck)thing.options[0];
										IDs[q] = c.itemId;
									}
								}
							}
						}
					}
					//if (IDs.Contains(12))
					{
						IItemDropRule[] ores = new IItemDropRule[9]
						{
							ItemDropRule.NotScalingWithLuck(ItemID.CopperOre, 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ItemID.TinOre, 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ItemID.IronOre, 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ItemID.LeadOre, 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ItemID.SilverOre, 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ItemID.TungstenOre, 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BronzeOre>(), 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NickelOre>(), 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ZincOre>(), 1, 12, 21)
						};
						IItemDropRule[] bars = new IItemDropRule[9]
						{
							ItemDropRule.NotScalingWithLuck(ItemID.CopperBar, 1, 4, 7),
							ItemDropRule.NotScalingWithLuck(ItemID.TinBar, 1, 4, 7),
							ItemDropRule.NotScalingWithLuck(ItemID.IronBar, 1, 4, 7),
							ItemDropRule.NotScalingWithLuck(ItemID.LeadBar, 1, 4, 7),
							ItemDropRule.NotScalingWithLuck(ItemID.SilverBar, 1, 4, 7),
							ItemDropRule.NotScalingWithLuck(ItemID.TungstenBar, 1, 4, 7),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BronzeBar>(), 1, 4, 7),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NickelBar>(), 1, 4, 7),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ZincBar>(), 1, 4, 7)
						};
						IItemDropRule[] hardmodeOres = new IItemDropRule[6]
						{
							ItemDropRule.NotScalingWithLuck(ItemID.CobaltOre, 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ItemID.PalladiumOre, 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ItemID.MythrilOre, 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ItemID.OrichalcumOre, 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<DurataniumOre>(), 1, 12, 21),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NaquadahOre>(), 1, 12, 21)
						};
						IItemDropRule[] hardmodeBars = new IItemDropRule[6]
						{
							ItemDropRule.NotScalingWithLuck(ItemID.CobaltBar, 1, 3, 7),
							ItemDropRule.NotScalingWithLuck(ItemID.PalladiumBar, 1, 3, 7),
							ItemDropRule.NotScalingWithLuck(ItemID.MythrilBar, 1, 3, 7),
							ItemDropRule.NotScalingWithLuck(ItemID.OrichalcumBar, 1, 3, 7),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<DurataniumBar>(), 1, 3, 7),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NaquadahBar>(), 1, 3, 7)
						};
						alwaysAtLeast1Success.rules[2] = ItemDropRule.SequentialRulesNotScalingWithLuck(1, ItemDropRule.SequentialRulesNotScalingWithLuck(6, new OneFromRulesRule(2, hardmodeOres), new OneFromRulesRule(1, ores)), ItemDropRule.SequentialRulesNotScalingWithLuck(4, new OneFromRulesRule(3, 2, hardmodeBars), new OneFromRulesRule(1, bars)));
					}
				}
			}
		}

		// golden crate
		if (item.type == ItemID.GoldenCrate)
		{
			itemLoot.Add(new CommonDropNotScalingWithLuck(ModContent.ItemType<StaminaPotion>(), 5, 2, 4));
			AlwaysAtleastOneSuccessDropRule? alwaysAtLeast1Success = null;
			foreach (IItemDropRule item2 in itemLoot.Get(false))
			{
				if (item2 is not AlwaysAtleastOneSuccessDropRule rule1)
				{
					continue;
				}
				alwaysAtLeast1Success = rule1;

				if (alwaysAtLeast1Success != null)
				{
					int[] IDs = new int[4];
					for (int i = 0; i < alwaysAtLeast1Success.rules.Length; i++)
					{
						if (alwaysAtLeast1Success.rules[i] is SequentialRulesNotScalingWithLuckRule drop)
						{
							for (int z = 0; z < drop.rules.Length; z++)
							{
								if (drop.rules[z] is not OneFromRulesRule thing) continue;
								CommonDropNotScalingWithLuck c = (CommonDropNotScalingWithLuck)thing.options[0];
								IDs[z] = c.itemId;
							}
						}
					}
					//if (IDs.Contains(ItemID.SilverOre))
					{
						IItemDropRule[] ores = new IItemDropRule[6]
						{
							ItemDropRule.NotScalingWithLuck(14, 1, 25, 34),
							ItemDropRule.NotScalingWithLuck(701, 1, 25, 34),
							ItemDropRule.NotScalingWithLuck(13, 1, 25, 34),
							ItemDropRule.NotScalingWithLuck(702, 1, 25, 34),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ZincOre>(), 1, 25, 34),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BismuthOre>(), 1, 25, 34)
						};
						IItemDropRule[] bars = new IItemDropRule[6]
						{
							ItemDropRule.NotScalingWithLuck(21, 1, 8, 11),
							ItemDropRule.NotScalingWithLuck(705, 1, 8, 11),
							ItemDropRule.NotScalingWithLuck(19, 1, 8, 11),
							ItemDropRule.NotScalingWithLuck(706, 1, 8, 11),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ZincBar>(), 1, 8, 11),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BismuthBar>(), 1, 8, 11)
						};
						alwaysAtLeast1Success.rules[2] = ItemDropRule.SequentialRulesNotScalingWithLuck(1, new OneFromRulesRule(5, ores), new OneFromRulesRule(3, 2, bars));
					}
				}
			}
		}
		// titanium crate
		if (item.type == ItemID.GoldenCrateHard)
		{
			itemLoot.Add(new CommonDropNotScalingWithLuck(ModContent.ItemType<StaminaPotion>(), 5, 2, 4));
			AlwaysAtleastOneSuccessDropRule? alwaysAtLeast1Success = null;
			foreach (IItemDropRule item2 in itemLoot.Get(false))
			{
				if (item2 is not AlwaysAtleastOneSuccessDropRule rule1)
				{
					continue;
				}
				alwaysAtLeast1Success = rule1;

				if (alwaysAtLeast1Success != null)
				{
					IItemDropRule[] newRule = new IItemDropRule[4];
					int[] IDs = new int[4];
					for (int i = 0; i < alwaysAtLeast1Success.rules.Length; i++)
					{
						if (alwaysAtLeast1Success.rules[i] is SequentialRulesNotScalingWithLuckRule drop)
						{
							for (int z = 0; z < drop.rules.Length; z++)
							{
								if (drop.rules[z] is SequentialRulesNotScalingWithLuckRule drop2)
								{
									for (int q = 0; q < drop2.rules.Length; q++)
									{
										if (drop2.rules[q] is not OneFromRulesRule thing) continue;

										CommonDropNotScalingWithLuck c = (CommonDropNotScalingWithLuck)thing.options[0];
										IDs[q] = c.itemId;
									}
								}
							}
						}
					}
					//if (IDs.Contains(ItemID.SilverOre))
					{
						IItemDropRule[] ores = new IItemDropRule[6]
						{
							ItemDropRule.NotScalingWithLuck(14, 1, 25, 34),
							ItemDropRule.NotScalingWithLuck(701, 1, 25, 34),
							ItemDropRule.NotScalingWithLuck(13, 1, 25, 34),
							ItemDropRule.NotScalingWithLuck(702, 1, 25, 34),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ZincOre>(), 1, 25, 34),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BismuthOre>(), 1, 25, 34)
						};
						IItemDropRule[] bars = new IItemDropRule[6]
						{
							ItemDropRule.NotScalingWithLuck(21, 1, 8, 11),
							ItemDropRule.NotScalingWithLuck(705, 1, 8, 11),
							ItemDropRule.NotScalingWithLuck(19, 1, 8, 11),
							ItemDropRule.NotScalingWithLuck(706, 1, 8, 11),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ZincBar>(), 1, 8, 11),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BismuthBar>(), 1, 8, 11)
						};

						IItemDropRule[] hardmodeOres = new IItemDropRule[6]
						{
							ItemDropRule.NotScalingWithLuck(365, 1, 25, 34),
							ItemDropRule.NotScalingWithLuck(1105, 1, 25, 34),
							ItemDropRule.NotScalingWithLuck(366, 1, 25, 34),
							ItemDropRule.NotScalingWithLuck(1106, 1, 25, 34),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NaquadahOre>(), 1, 25, 34),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<TroxiniumOre>(), 1, 25, 34)
						};
						IItemDropRule[] hardmodeBars = new IItemDropRule[6]
						{
							ItemDropRule.NotScalingWithLuck(382, 1, 8, 11),
							ItemDropRule.NotScalingWithLuck(1191, 1, 8, 11),
							ItemDropRule.NotScalingWithLuck(391, 1, 8, 11),
							ItemDropRule.NotScalingWithLuck(1198, 1, 8, 11),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NaquadahBar>(), 1, 8, 11),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<TroxiniumBar>(), 1, 8, 11)
						};
						alwaysAtLeast1Success.rules[2] = ItemDropRule.SequentialRulesNotScalingWithLuck(1, ItemDropRule.SequentialRulesNotScalingWithLuck(5, new OneFromRulesRule(2, hardmodeOres), new OneFromRulesRule(1, ores)), ItemDropRule.SequentialRulesNotScalingWithLuckWithNumerator(3, 2, new OneFromRulesRule(3, 2, hardmodeBars), new OneFromRulesRule(1, bars)));
					}
				}
			}
		}

		// biome crates
		if (item.type is ItemID.OceanCrate or ItemID.JungleFishingCrate or ItemID.FloatingIslandFishingCrate or ItemID.CorruptFishingCrate or
			ItemID.CrimsonFishingCrate or ItemID.HallowedFishingCrate or ItemID.DungeonFishingCrate or ItemID.FrozenCrate or ItemID.OasisCrate or
			ItemID.LavaCrate)
		{
			itemLoot.Add(new CommonDropNotScalingWithLuck(ModContent.ItemType<StaminaPotion>(), 6, 2, 4));
			int oreIndex = -1;
			int barIndex = -1;
			switch (item.type)
			{
				case ItemID.HallowedFishingCrate:
					oreIndex = 1;
					barIndex = 2;
					break;
				case ItemID.OceanCrate:
				case ItemID.DungeonFishingCrate:
					oreIndex = 3;
					barIndex = 4;
					break;
				case ItemID.FloatingIslandFishingCrate:
				case ItemID.OasisCrate:
					oreIndex = 5;
					barIndex = 6;
					break;
				case ItemID.CorruptFishingCrate:
				case ItemID.CrimsonFishingCrate:
				case ItemID.JungleFishingCrate:
				case ItemID.FrozenCrate:
					oreIndex = 2;
					barIndex = 3;
					break;
				case ItemID.LavaCrate:
					oreIndex = 7;
					barIndex = 8;
					break;
			}

			AlwaysAtleastOneSuccessDropRule? alwaysAtLeast1Success = null;
			foreach (IItemDropRule item2 in itemLoot.Get(false))
			{
				if (item2 is not AlwaysAtleastOneSuccessDropRule rule1)
				{
					continue;
				}
				alwaysAtLeast1Success = rule1;

				if (alwaysAtLeast1Success != null)
				{
					int[] IDs = new int[20];
					for (int i = 0; i < alwaysAtLeast1Success.rules.Length; i++)
					{
						if (alwaysAtLeast1Success.rules[i] is OneFromRulesRule drop)
						{
							CommonDropNotScalingWithLuck c = (CommonDropNotScalingWithLuck)drop.options[0];
							IDs[i] = c.itemId;
						}
					}
					//if (IDs.Contains(ItemID.CopperOre))
					{
						IItemDropRule[] ores = new IItemDropRule[12]
						{
							ItemDropRule.NotScalingWithLuck(12, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(699, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(11, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(700, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(14, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(701, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(13, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(702, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BronzeOre>(), 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NickelOre>(), 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ZincOre>(), 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BismuthOre>(), 1, 20, 35)
						};
						IItemDropRule[] bars = new IItemDropRule[9]
						{
							ItemDropRule.NotScalingWithLuck(22, 1, 6, 16),
							ItemDropRule.NotScalingWithLuck(704, 1, 6, 16),
							ItemDropRule.NotScalingWithLuck(21, 1, 6, 16),
							ItemDropRule.NotScalingWithLuck(705, 1, 6, 16),
							ItemDropRule.NotScalingWithLuck(19, 1, 6, 16),
							ItemDropRule.NotScalingWithLuck(706, 1, 6, 16),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NickelBar>(), 1, 6, 16),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ZincBar>(), 1, 6, 16),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BismuthBar>(), 1, 6, 16)
						};
						alwaysAtLeast1Success.rules[oreIndex] = new OneFromRulesRule(7, ores);
						alwaysAtLeast1Success.rules[barIndex] = new OneFromRulesRule(4, bars);
					}
				}
			}
		}

		// biome crates hardmode
		if (item.type is ItemID.OceanCrateHard or ItemID.JungleFishingCrateHard or ItemID.FloatingIslandFishingCrateHard or ItemID.CorruptFishingCrateHard or
			ItemID.CrimsonFishingCrateHard or ItemID.HallowedFishingCrateHard or ItemID.DungeonFishingCrateHard or ItemID.FrozenCrateHard or ItemID.OasisCrateHard or
			ItemID.LavaCrateHard)
		{
			itemLoot.Add(new CommonDropNotScalingWithLuck(ModContent.ItemType<StaminaPotion>(), 6, 2, 4));
			int oreIndex = -1;
			int barIndex = -1;
			switch (item.type)
			{
				case ItemID.HallowedFishingCrateHard:
					oreIndex = 1;
					barIndex = 2;
					break;
				case ItemID.OceanCrateHard:
				case ItemID.DungeonFishingCrateHard:
					oreIndex = 3;
					barIndex = 4;
					break;
				case ItemID.FloatingIslandFishingCrateHard:
				case ItemID.OasisCrateHard:
					oreIndex = 5;
					barIndex = 6;
					break;
				case ItemID.CorruptFishingCrateHard:
				case ItemID.CrimsonFishingCrateHard:
				case ItemID.JungleFishingCrateHard:
				case ItemID.FrozenCrateHard:
					oreIndex = 2;
					barIndex = 3;
					break;
				case ItemID.LavaCrateHard:
					oreIndex = 7;
					barIndex = 8;
					break;
			}

			AlwaysAtleastOneSuccessDropRule? alwaysAtLeast1Success = null;
			foreach (IItemDropRule item2 in itemLoot.Get(false))
			{
				if (item2 is not AlwaysAtleastOneSuccessDropRule rule1)
				{
					continue;
				}
				alwaysAtLeast1Success = rule1;

				if (alwaysAtLeast1Success != null)
				{
					int[] IDs = new int[20];
					for (int i = 0; i < alwaysAtLeast1Success.rules.Length; i++)
					{
						if (alwaysAtLeast1Success.rules[i] is SequentialRulesNotScalingWithLuckRule drop)
						{
							for (int z = 0; z < drop.rules.Length; z++)
							{
								if (drop.rules[z] is OneFromRulesRule drop2)
								{
									CommonDropNotScalingWithLuck c = (CommonDropNotScalingWithLuck)drop2.options[0];
									IDs[i] = c.itemId;
								}
							}
						}
					}
					//if (IDs.Contains(ItemID.CopperOre))
					{
						IItemDropRule[] ores = new IItemDropRule[12]
						{
							ItemDropRule.NotScalingWithLuck(12, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(699, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(11, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(700, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(14, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(701, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(13, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(702, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BronzeOre>(), 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NickelOre>(), 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ZincOre>(), 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BismuthOre>(), 1, 20, 35)
						};
						IItemDropRule[] hardmodeOres = new IItemDropRule[9]
						{
							ItemDropRule.NotScalingWithLuck(364, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(1104, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(365, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(1105, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(366, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(1106, 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<DurataniumOre>(), 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NaquadahOre>(), 1, 20, 35),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<TroxiniumOre>(), 1, 20, 35),
						};
						IItemDropRule[] bars = new IItemDropRule[9]
						{
							ItemDropRule.NotScalingWithLuck(22, 1, 6, 16),
							ItemDropRule.NotScalingWithLuck(704, 1, 6, 16),
							ItemDropRule.NotScalingWithLuck(21, 1, 6, 16),
							ItemDropRule.NotScalingWithLuck(705, 1, 6, 16),
							ItemDropRule.NotScalingWithLuck(19, 1, 6, 16),
							ItemDropRule.NotScalingWithLuck(706, 1, 6, 16),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NickelBar>(), 1, 6, 16),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ZincBar>(), 1, 6, 16),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BismuthBar>(), 1, 6, 16)
						};
						IItemDropRule[] hardmodeBars = new IItemDropRule[9]
						{
							ItemDropRule.NotScalingWithLuck(381, 1, 5, 16),
							ItemDropRule.NotScalingWithLuck(1184, 1, 5, 16),
							ItemDropRule.NotScalingWithLuck(382, 1, 5, 16),
							ItemDropRule.NotScalingWithLuck(1191, 1, 5, 16),
							ItemDropRule.NotScalingWithLuck(391, 1, 5, 16),
							ItemDropRule.NotScalingWithLuck(1198, 1, 5, 16),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<DurataniumBar>(), 1, 6, 16),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NaquadahBar>(), 1, 6, 16),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<TroxiniumBar>(), 1, 6, 16)
						};
						alwaysAtLeast1Success.rules[oreIndex] = ItemDropRule.SequentialRulesNotScalingWithLuck(7, new OneFromRulesRule(2, hardmodeOres), new OneFromRulesRule(1, ores));
						alwaysAtLeast1Success.rules[barIndex] = ItemDropRule.SequentialRulesNotScalingWithLuck(4, new OneFromRulesRule(3, 2, hardmodeBars), new OneFromRulesRule(1, bars));
					}
				}
			}
		}

		// glacier staff/frozen lyre from frozen/boreal crates
		if (item.type is ItemID.FrozenCrate or ItemID.FrozenCrateHard)
		{
			AlwaysAtleastOneSuccessDropRule? oneFromRulesRule = null;
			foreach (IItemDropRule item2 in itemLoot.Get(false))
			{
				if (item2 is not AlwaysAtleastOneSuccessDropRule rule1)
				{
					continue;
				}
				oneFromRulesRule = rule1;

				if (oneFromRulesRule != null)
				{
					int[] IDs = new int[4];
					for (int i = 0; i < oneFromRulesRule.rules.Length; i++)
					{
						if (oneFromRulesRule.rules[i] is SequentialRulesNotScalingWithLuckRule drop)
						{
							for (int z = 0; z < drop.rules.Length; z++)
							{
								if (drop.rules[z] is not OneFromRulesRule thing) continue;
								CommonDropNotScalingWithLuck c = (CommonDropNotScalingWithLuck)thing.options[0];
								IDs[z] = c.itemId;
							}
						}
					}
					//if (IDs.Contains(ItemID.CopperOre))
					{
						IItemDropRule ruleSnowballCannonIceBow = ItemDropRule.ByCondition(new Conditions.NotRemixSeed(), 1319);
						ruleSnowballCannonIceBow.OnFailedConditions(ItemDropRule.NotScalingWithLuck(725), hideLootReport: true);
						IItemDropRule[] bc_iceList = new IItemDropRule[8]
						{
							ItemDropRule.NotScalingWithLuck(670),
							ItemDropRule.NotScalingWithLuck(724),
							ItemDropRule.NotScalingWithLuck(950),
							ruleSnowballCannonIceBow,
							ItemDropRule.NotScalingWithLuck(987),
							ItemDropRule.NotScalingWithLuck(1579),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<GlacierStaff>()),
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<FrozenLyre>())
						};
						oneFromRulesRule.rules[0] = new OneFromRulesRule(1, bc_iceList);
					}
				}
			}
		}

		// flower of the jungle
		if (item.type is ItemID.JungleFishingCrate or ItemID.JungleFishingCrateHard)
		{
			AlwaysAtleastOneSuccessDropRule? oneFromRulesRule = null;
			foreach (IItemDropRule item2 in itemLoot.Get(false))
			{
				if (item2 is not AlwaysAtleastOneSuccessDropRule rule1)
				{
					continue;
				}
				oneFromRulesRule = rule1;

				if (oneFromRulesRule != null)
				{
					int[] IDs = new int[4];
					for (int i = 0; i < oneFromRulesRule.rules.Length; i++)
					{
						if (oneFromRulesRule.rules[i] is SequentialRulesNotScalingWithLuckRule drop)
						{
							for (int z = 0; z < drop.rules.Length; z++)
							{
								if (drop.rules[z] is not OneFromRulesRule thing) continue;
								CommonDropNotScalingWithLuck c = (CommonDropNotScalingWithLuck)thing.options[0];
								IDs[z] = c.itemId;
							}
						}
					}
					//if (IDs.Contains(ItemID.CopperOre))
					{
						IItemDropRule[] bc_jungle = new IItemDropRule[2]
						{
							ItemDropRule.NotScalingWithLuck(ItemID.FlowerBoots, 20),
							ItemDropRule.OneFromOptionsNotScalingWithLuck(1, ItemID.AnkletoftheWind, ItemID.Boomstick, ItemID.FeralClaws, ItemID.StaffofRegrowth, ItemID.FiberglassFishingPole, ModContent.ItemType<FlowerofTheJungle>())
						};
						oneFromRulesRule.rules[0] = ItemDropRule.SequentialRulesNotScalingWithLuck(1, bc_jungle);
					}
				}
			}
		}


		if (item.type == ItemID.HerbBag)
		{
			foreach (IItemDropRule item2 in itemLoot.Get(false))
			{
				if (item2 is HerbBagDropsItemDropRule herbRule && Array.IndexOf(herbRule.dropIds, 313) > -1)
				{
					HashSet<int> set = new HashSet<int>(herbRule.dropIds)
					{
						ModContent.ItemType<Barfbush>(),
						ModContent.ItemType<BarfbushSeeds>(),
						ModContent.ItemType<Bloodberry>(),
						ModContent.ItemType<BloodberrySeeds>(),
						ModContent.ItemType<Sweetstem>(),
						ModContent.ItemType<SweetstemSeeds>()
					};
					herbRule.dropIds = set.ToArray();
					break;
				}
			}
		}
		if (item.type == ItemID.LockBox)
		{
			OneFromRulesRule? oneFromRulesRule = null;
			foreach (IItemDropRule item2 in itemLoot.Get(false))
			{
				if (item2 is not OneFromRulesRule rule1)
				{
					continue;
				}
				oneFromRulesRule = rule1;

				if (oneFromRulesRule != null)
				{
					IItemDropRule ruleAquaScepterBubbleGun = ItemDropRule.ByCondition(new Conditions.NotRemixSeed(), ItemID.AquaScepter);
					ruleAquaScepterBubbleGun.OnFailedConditions(ItemDropRule.NotScalingWithLuck(ItemID.BubbleGun), hideLootReport: true);

					IItemDropRule[] goldenLockBoxList = new IItemDropRule[]
					{
						ItemDropRule.NotScalingWithLuck(ItemID.Valor),
						ItemDropRule.NotScalingWithLuck(ItemID.Muramasa),
						ItemDropRule.NotScalingWithLuck(ItemID.CobaltShield),
						ruleAquaScepterBubbleGun,
						ItemDropRule.NotScalingWithLuck(ItemID.BlueMoon),
						ItemDropRule.NotScalingWithLuck(ItemID.MagicMissile),
						ItemDropRule.NotScalingWithLuck(ItemID.Handgun),
						ItemDropRule.NotScalingWithLuck(ModContent.ItemType<SapphirePickaxe>()),
						ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Blueshift>())
					};
					oneFromRulesRule.options = goldenLockBoxList;
					break;
				}
			}
		}

		if (item.type == ItemID.KingSlimeBossBag)
		{
			itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BandofSlime>(), 3));
		}
		if (item.type == ItemID.PlanteraBossBag)
		{
			itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<LifeDew>(), 1, 10, 17));
			itemLoot.Add(ItemDropRule.Common(ItemID.ChlorophyteOre, 1, 75, 130));
		}
		if (item.type == ItemID.WallOfFleshBossBag)
		{
			itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<FleshyTendril>(), 1, 25, 36));
		}
		if (item.type == ItemID.EyeOfCthulhuBossBag)
		{
			var contagionCondition = new IsContagion();
			var corruptionCondition = new Conditions.IsCorruption();
			var crimson = new CrimsonNotContagion();
			var corruptionNotContagion = new Combine(true, null, new Invert(contagionCondition), corruptionCondition);

			// remove corruption loot
			itemLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.DemoniteOre);
			itemLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.CorruptSeeds);
			itemLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.UnholyArrow);

			// remove crimson loot
			itemLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.CrimtaneOre);
			itemLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.CrimsonSeeds);

			// add corruption loot back
			LeadingConditionRule corruptionRule = new LeadingConditionRule(corruptionNotContagion);
			corruptionRule.OnSuccess(ItemDropRule.Common(ItemID.DemoniteOre, 1, 30, 90));
			corruptionRule.OnSuccess(ItemDropRule.Common(ItemID.CorruptSeeds, 1, 1, 3));
			corruptionRule.OnSuccess(ItemDropRule.Common(ItemID.UnholyArrow, 1, 20, 50));
			corruptionRule.OnSuccess(ItemDropRule.Common(ItemID.BloodMoonStarter, 12));
			itemLoot.Add(corruptionRule);

			// add crimson loot back
			LeadingConditionRule crimsonRule = new LeadingConditionRule(crimson);
			crimsonRule.OnSuccess(ItemDropRule.Common(ItemID.CrimtaneOre, 1, 30, 90));
			crimsonRule.OnSuccess(ItemDropRule.Common(ItemID.CrimsonSeeds, 1, 1, 3));
			crimsonRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BloodyArrow>(), 1, 20, 50));
			crimsonRule.OnSuccess(ItemDropRule.Common(ItemID.BloodMoonStarter, 12));
			itemLoot.Add(crimsonRule);

			// add contagion loot
			LeadingConditionRule contagionRule = new LeadingConditionRule(contagionCondition);
			contagionRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BacciliteOre>(), 1, 30, 90));
			contagionRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ContagionSeeds>(), 1, 1, 3));
			contagionRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<IckyArrow>(), 1, 20, 50));
			contagionRule.OnSuccess(ItemDropRule.Common(ItemID.BloodMoonStarter, 12));
			itemLoot.Add(contagionRule);
		}
	}
	public override void SetDefaults(Item item)
	{
		if (item.type != ModContent.ItemType<InactiveCoating>() && item.paintCoating == Data.Sets.AvalonCoatingsID.ActuatorCoating)
		{
			throw new Exception("Item is attempting to use the same coating ID as an already exisiting coating Item. This is a Mod Conflict and would suggest disabling each mod one at a time.");
		}
		if (item.IsArmor())
		{
			ItemID.Sets.CanGetPrefixes[item.type] = true;
		}
		if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome)
		{
			item.accessory = true;
		}
		switch (item.type)
		{
			case ItemID.BloodMoonStarter:
				item.DefaultToSpawner(useAnim: 15, useTime: 30);
				item.shoot = ModContent.ProjectileType<Projectiles.Tools.BloodyAmulet>();
				item.rare = ItemRarityID.Green;
				item.UseSound = SoundID.Item4;
				break;
			case ItemID.Hive:
				item.createTile = TileID.Hive;
				item.useTime = 10;
				item.useAnimation = 15;
				item.useStyle = ItemUseStyleID.Swing;
				item.consumable = true;
				item.useTurn = true;
				item.autoReuse = true;
				break;
			case ItemID.HiveWand:
				item.tileWand = -1;
				item.useTime = 20;
				item.useAnimation = 20;
				item.useTurn = false;
				item.useStyle = ItemUseStyleID.Shoot;
				item.damage = 24;
				item.DamageType = DamageClass.Magic;
				item.rare = ItemRarityID.Green;
				item.knockBack = 1.8f;
				item.UseSound = SoundID.Item43;
				item.shootSpeed = 7.5f;
				item.shoot = ModContent.ProjectileType<Projectiles.Magic.BeeBolt>();
				item.mana = 10;
				item.createTile = -1;
				break;
			case ItemID.Amethyst:
				item.value = Item.sellPrice(0, 0, 3, 75);
				break;
			case ItemID.Topaz:
				item.value = Item.sellPrice(0, 0, 7, 50);
				break;
			case ItemID.Sapphire:
				item.value = Item.sellPrice(0, 0, 15);
				break;
			case ItemID.Emerald:
				item.value = Item.sellPrice(0, 0, 18, 75);
				break;
			case ItemID.Ruby:
				item.value = Item.sellPrice(0, 0, 26, 25);
				break;
			case ItemID.Diamond:
				item.value = Item.sellPrice(0, 0, 30);
				break;
			case ItemID.Grenade:
				item.ammo = ItemID.Grenade;
				break;
			case ItemID.CactusHelmet:
			case ItemID.CactusBreastplate:
			case ItemID.CactusLeggings:
				item.defense = 2;
				break;
			case ItemID.GravitationPotion:
				item.buffTime = 3600 * 6;
				break;
			case ItemID.MagicPowerPotion:
				item.buffTime = 3600 * 5;
				break;
			case ItemID.CellPhone:
			case ItemID.Shellphone:
			case ItemID.ShellphoneHell:
			case ItemID.ShellphoneOcean:
			case ItemID.ShellphoneSpawn:
				item.useTime = item.useAnimation = 30;
				break;
			case ItemID.RottenChunk:
				item.useStyle = ItemUseStyleID.Swing;
				item.useAnimation = 15;
				item.useTime = 10;
				item.consumable = true;
				item.useTurn = true;
				item.autoReuse = true;
				item.createTile = ModContent.TileType<RottenChunk>();
				break;
			case ItemID.Vertebrae:
				item.useStyle = ItemUseStyleID.Swing;
				item.useAnimation = 15;
				item.useTime = 10;
				item.consumable = true;
				item.useTurn = true;
				item.autoReuse = true;
				item.createTile = ModContent.TileType<Vertebrae>();
				break;
			case ItemID.Ectoplasm:
				item.useStyle = ItemUseStyleID.Swing;
				item.useAnimation = 15;
				item.useTime = 10;
				item.consumable = true;
				item.useTurn = true;
				item.autoReuse = true;
				item.createTile = ModContent.TileType<Ectoplasm>();
				break;
			case ItemID.ShadowScale:
				item.chlorophyteExtractinatorConsumable = false;
				item.useStyle = ItemUseStyleID.Swing;
				item.useAnimation = 15;
				item.useTime = 10;
				item.consumable = true;
				item.useTurn = true;
				item.autoReuse = true;
				item.createTile = ModContent.TileType<ShadowScale>();
				break;
			case ItemID.TissueSample:
				item.chlorophyteExtractinatorConsumable = false;
				item.useStyle = ItemUseStyleID.Swing;
				item.useAnimation = 15;
				item.useTime = 10;
				item.consumable = true;
				item.useTurn = true;
				item.autoReuse = true;
				item.createTile = ModContent.TileType<TissueSample>();
				break;
			#region miscellaneous changes
			case ItemID.ShroomiteDiggingClaw:
				item.pick = 205;
				break;
			case ItemID.Picksaw:
				item.tileBoost++;
				break;
			case ItemID.HeatRay:
				item.mana = 5;
				break;
			case ItemID.TheAxe:
				item.hammer = 95;
				break;
			case ItemID.LaserDrill:
				item.pick = 220;
				break;
			case ItemID.Hellstone:
				item.value = Item.sellPrice(0, 0, 13, 30);
				break;
			case ItemID.Vilethorn:
				item.useStyle = ItemUseStyleID.Shoot;
				break;
			#endregion miscellaneous changes
			case ItemID.EmpressFlightBooster:
				item.rare = ItemRarityID.Yellow;
				item.expert = false;
				break;
			#region ML item rebalance
			case ItemID.Meowmere:
				item.damage = 145;
				break;
			case ItemID.SDMG:
				item.damage = 49;
				break;
			case ItemID.StarWrath:
				item.damage = 85;
				break;
			case ItemID.LastPrism:
				item.damage = 68;
				break;
			case ItemID.Terrarian:
				item.damage = 144;
				break;
			case ItemID.LunarFlareBook:
				item.damage = 75;
				break;
			case ItemID.RainbowCrystalStaff:
				item.damage = 115;
				break;
			case ItemID.MoonlordTurretStaff:
				item.damage = 85;
				break;
				#endregion ML item rebalance
		}
		item.GetGlobalItem<AvalonGlobalItemInstance>().TileBoostSaved = item.tileBoost;

		if (ItemID.Sets.Torches[item.type])
		{
			item.ammo = ItemID.Torch;
			item.notAmmo = true;
		}
	}
	public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		#region split projectile bonus
		if (player.GetModPlayer<AvalonPlayer>().SplitProj && Main.rand.NextBool(7) && item.DamageType == DamageClass.Ranged && item.type != ModContent.ItemType<TorchLauncher>() && item.useAmmo > 0 && !Data.Sets.ItemSets.Longbows[item.type])
		{
			for (int num122 = 0; num122 < 2; num122++)
			{
				float num123 = velocity.X;
				float num124 = velocity.Y;
				num123 += Main.rand.Next(-30, 31) * 0.05f;
				num124 += Main.rand.Next(-30, 31) * 0.05f;
				if (item.type == ModContent.ItemType<Items.Weapons.Ranged.PreHardmode.EggCannon>())
				{
					Vector2 vel = new Vector2(num123, num124);
					Projectile.NewProjectile(source, position, vel.RotatedByRandom(0.04f), ModContent.ProjectileType<Projectiles.Ranged.ExplosiveEgg>(), damage, knockback, player.whoAmI, ai2: Main.rand.NextBool(3) ? 1 : 0);
				}
				else
				{
					Projectile.NewProjectile(player.GetSource_FromThis(), position.X, position.Y, num123, num124, type, damage, knockback, player.whoAmI);
				}
			}
			return false;
		}
		#endregion
		return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
	}
	public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
	{
		if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome && slot < 19 && !modded)
		{
			return false;
		}
		if (item.GetGlobalItem<AvalonGlobalItemInstance>().StaminaScroll && slot < 19 && !modded)
		{
			return false;
		}
		if (item.GetGlobalItem<AvalonGlobalItemInstance>().Genie && slot < 19 && !modded)
		{
			return false;
		}

		return base.CanEquipAccessory(item, player, slot, modded);
	}
	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
	{
		TooltipLine? tooltipLine = tooltips.Find(x => x.Name == "ItemName" && x.Mod == "Terraria");
		TooltipLine? tooltipMat = tooltips.Find(x => x.Name == "Material" && x.Mod == "Terraria");
		TooltipLine? tooltipEquip = tooltips.Find(x => x.Name == "Equipable" && x.Mod == "Terraria");
		TooltipLine? healLife = tooltips.Find(x => x.Name == "HealLife" && x.Mod == "Terraria");
		TooltipLine? buffTime = tooltips.Find(x => x.Name == "BuffTime" && x.Mod == "Terraria");
		if (healLife != null && item.healLife > 0)
		{
			if (Main.LocalPlayer.GetModPlayer<AvalonPlayer>().ThePill)
			{
				healLife.Text = Language.GetTextValue("CommonItemTooltip.RestoresLife", (int)(item.healLife * ThePill.LifeBonusAmount));
			}
		}
		if (tooltipEquip != null)
		{
			if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome)
			{
				tooltips.RemoveAt(tooltips.FindIndex(x => x.Name == "Equipable" && x.Mod == "Terraria"));
			}
		}
		if (item.type == ModContent.ItemType<VomitWater>() || item.type is ItemID.BloodWater or ItemID.UnholyWater or ItemID.HolyWater)
		{
			int index = tooltips.FindLastIndex(tt => tt.Mod.Equals("Terraria") && tt.Name.Equals("CritChance"));
			if (index != -1)
			{
				tooltips.RemoveAt(index);
			}
		}
		if (item.type is ItemID.SiltBlock or ItemID.SlushBlock or ItemID.DesertFossil)
		{
			int index = tooltips.FindLastIndex(tt => tt.Mod.Equals("Terraria") && tt.Name.Equals("Tooltip0"));
			if (index != -1)
			{
				tooltips.Insert(index + 1, new TooltipLine(Mod, "Tooltip0", NetworkText.FromKey("Mods.Avalon.TooltipEdits.Silt").ToString()));
			}
		}
		if (item.type == ItemID.MythrilAnvil || item.type == ItemID.OrichalcumAnvil)
		{
			int index = tooltips.FindLastIndex(tt => tt.Mod.Equals("Terraria") && tt.Name.Equals("Tooltip0"));
			if (index != -1)
			{
				tooltips[index].Text = Language.GetTextValue("Mods.Avalon.Items.NaquadahAnvil.Tooltip");
			}
		}
		if (item.type == ItemID.AdamantiteForge || item.type == ItemID.TitaniumForge)
		{
			int index = tooltips.FindLastIndex(tt => tt.Mod.Equals("Terraria") && tt.Name.Equals("Tooltip0"));
			if (index != -1)
			{
				tooltips[index].Text = Language.GetTextValue("Mods.Avalon.Items.TroxiniumForge.Tooltip");
			}
		}
		if (item.type == ModContent.ItemType<PickaxeofDusk3x3>() || item.type == ModContent.ItemType<AccelerationPickaxeSpeed>() ||
			item.type == ModContent.ItemType<AccelerationDrillSpeed>())
		{
			int index = tooltips.FindLastIndex(tt => tt.Mod.Equals("Terraria") && tt.Name.Equals("Knockback"));
			if (index != -1)
			{
				tooltips.Insert(index + 1, new TooltipLine(Mod, "PickPower", (item.type == ModContent.ItemType<PickaxeofDusk3x3>() ? "100" : "400") + NetworkText.FromKey("LegacyTooltip.26").ToString()));
			}
		}
		if (item.type == ModContent.ItemType<Breakdawn3x3>())
		{
			int index = tooltips.FindLastIndex(tt => tt.Mod.Equals("Terraria") && tt.Name.Equals("Knockback"));
			if (index != -1)
			{
				tooltips.Insert(index + 1, new TooltipLine(Mod, "AxePower", "160" + NetworkText.FromKey("LegacyTooltip.27").ToString()));
				tooltips.Insert(index + 1, new TooltipLine(Mod, "HammerPower", "70" + NetworkText.FromKey("LegacyTooltip.28").ToString()));
			}
		}
		if (item.type is ItemID.BottomlessBucket or ItemID.BottomlessLavaBucket or ItemID.BottomlessHoneyBucket or ItemID.BottomlessShimmerBucket)
		{
			int index = tooltips.FindLastIndex(tt => tt.Mod.Equals("Terraria") && tt.Name.Equals("Tooltip1"));
			if (index != -1)
			{
				tooltips.Insert(index + 1, new TooltipLine(Mod, "Tooltip1", NetworkText.FromKey("Mods.Avalon.TooltipEdits.BottomlessBuckets").ToString()));
			}
		}
		if (item.type is ItemID.SuperAbsorbantSponge or ItemID.LavaAbsorbantSponge or ItemID.HoneyAbsorbantSponge or ItemID.UltraAbsorbantSponge)
		{
			int index = tooltips.FindLastIndex(tt => tt.Mod.Equals("Terraria") && tt.Name.Equals("Tooltip0"));
			if (index != -1)
			{
				tooltips.Insert(index + 1, new TooltipLine(Mod, "Tooltip0", NetworkText.FromKey("Mods.Avalon.TooltipEdits.Sponges").ToString()));
			}
		}

		if (item.type is ItemID.CloudinaBottle or ItemID.BlizzardinaBottle or ItemID.SandstorminaBottle or ItemID.TsunamiInABottle or ItemID.FartinaJar)
		{
			int index = tooltips.FindLastIndex(tt => tt.Mod.Equals("Terraria") && tt.Name.Equals("Tooltip0"));
			if (index != -1)
			{
				tooltips.Insert(index + 1, new TooltipLine(Mod, "Tooltip0", NetworkText.FromKey("Mods.Avalon.TooltipEdits.ExtraJumps").ToString()));
			}
		}
		if (item.type is ItemID.CloudinaBalloon or ItemID.BlizzardinaBalloon or ItemID.SandstorminaBalloon or ItemID.SharkronBalloon or ItemID.FartInABalloon or
			ItemID.BalloonHorseshoeFart or ItemID.BalloonHorseshoeSharkron or ItemID.BlueHorseshoeBalloon or ItemID.YellowHorseshoeBalloon or ItemID.WhiteHorseshoeBalloon)
		{
			int index = tooltips.FindLastIndex(tt => tt.Mod.Equals("Terraria") && tt.Name.Equals("Tooltip1"));
			if (index != -1)
			{
				tooltips.Insert(index + 1, new TooltipLine(Mod, "Tooltip1", NetworkText.FromKey("Mods.Avalon.TooltipEdits.ExtraJumps").ToString()));
			}
		}
		if (item.type is ItemID.HiveWand)
		{
			int index = tooltips.FindLastIndex(tt => tt.Mod.Equals("Terraria") && tt.Name.Equals("Tooltip0"));
			if (index != -1)
			{
				tooltips[index].Text = NetworkText.FromKey("Mods.Avalon.TooltipEdits.HiveWand").ToString();
				//tooltips.Insert(index + 1, new TooltipLine(Mod, "Tooltip0", NetworkText.FromKey("Mods.Avalon.TooltipEdits.HiveWand").ToString()));
			}
		}
		//if (item.type is ItemID.Extractinator)
		//{
		//    int index = tooltips.FindLastIndex(tt => tt.Mod.Equals("Terraria") && tt.Name.Equals("Tooltip0"));
		//    if (index != -1)
		//    {
		//        tooltips.Insert(index + 1, new TooltipLine(Mod, "Tooltip0", NetworkText.FromKey("Mods.Avalon.TooltipEdits.Extractinator").ToString()));
		//    }
		//}
		//if (item.type is ItemID.ChlorophyteExtractinator)
		//{
		//    int index = tooltips.FindLastIndex(tt => tt.Mod.Equals("Terraria") && tt.Name.Equals("Tooltip2"));
		//    if (index != -1)
		//    {
		//        tooltips.Insert(index + 1, new TooltipLine(Mod, "Tooltip2", NetworkText.FromKey("Mods.Avalon.TooltipEdits.Extractinator").ToString()));
		//    }
		//}
		//if (item.type is ItemID.HandOfCreation)
		//{
		//    int index = tooltips.FindLastIndex(tt => tt.Mod.Equals("Terraria") && tt.Name.Equals("Tooltip5"));
		//    if (index != -1)
		//    {
		//        tooltips.Insert(index + 1, new TooltipLine(Mod, "Tooltip5", NetworkText.FromKey("Mods.Avalon.TooltipEdits.HandofCreation").ToString()));
		//    }
		//}
		if (Data.Sets.ItemSets.Herbs[item.type])
		{
			int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name)) && tt.Name.Equals("Material"));
			if (index != -1)
			{
				tooltips.Insert(index + 1, new TooltipLine(Mod, "Material", NetworkText.FromKey("Mods.Avalon.TooltipEdits.HerbologyLine").ToString()));
			}
		}
		if (Data.Sets.ItemSets.Potions[item.type])
		{
			int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name)) && tt.Name.Equals("Tooltip0"));
			if (index != -1)
			{
				tooltips.Insert(index + (item.type == ModContent.ItemType<BloodCastPotion>() ? 2 : 1), new TooltipLine(Mod, "Tooltip0", NetworkText.FromKey("Mods.Avalon.TooltipEdits.HerbologyLine").ToString()));
			}
		}
		if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome)
		{
			int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
				&& tt.Name.Equals("ItemName"));
			if (index != -1)
			{
				tooltips.Insert(index + 1, new TooltipLine(Mod, "TomeTooltip", NetworkText.FromKey("Mods.Avalon.CommonItemTooltip.Tome").ToString()));
				tooltips.Insert(index + 2, new TooltipLine(Mod, "TomeGradeTooltip", NetworkText.FromKey("Mods.Avalon.CommonItemTooltip.TomeGrade") + " " + item.GetGlobalItem<AvalonGlobalItemInstance>().TomeGrade));
			}
		}
		if (item.IsTool())
		{
			if (item.prefix == ModContent.PrefixType<Efficient>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& tt.Name.Equals("PrefixSpeed"));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixTool", Language.GetTextValue("Mods.Avalon.PrefixTooltips.Efficiency"))
					{
						IsModifier = true
					});
				}
				index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& tt.Name.Equals("PrefixSpeed"));
				if (index != -1)
				{
					tooltips.RemoveAt(index);
				}
			}
			if (item.prefix == ModContent.PrefixType<Expanded>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& tt.Name.StartsWith("PrefixSpeed"));
				if (index != -1)
				{
					tooltips.Insert(index, new TooltipLine(Mod, "PrefixTool", Language.GetTextValue("Mods.Avalon.PrefixTooltips.Expanded"))
					{
						IsModifier = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Extended>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& tt.Name.StartsWith("PrefixSpeed"));
				if (index != -1)
				{
					tooltips.Insert(index, new TooltipLine(Mod, "PrefixTool",
						"+" + (item.GetGlobalItem<AvalonGlobalItemInstance>().TileBoostSaved + 1).ToString() +
						Language.GetTextValue("Mods.Avalon.PrefixTooltips.Range"))
					{
						IsModifier = true
					});
				}
				index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& tt.Name.StartsWith("TileBoost"));
				if (index != -1)
				{
					tooltips.RemoveAt(index);
				}
			}

			if (item.prefix == ModContent.PrefixType<Broadened>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& tt.Name.StartsWith("PrefixSpeed"));
				if (index != -1)
				{
					tooltips.Insert(index, new TooltipLine(Mod, "PrefixTool",
						"+" + (item.GetGlobalItem<AvalonGlobalItemInstance>().TileBoostSaved + 2).ToString() +
						Language.GetTextValue("Mods.Avalon.PrefixTooltips.Range"))
					{
						IsModifier = true
					});
				}
				index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& tt.Name.StartsWith("TileBoost"));
				if (index != -1)
				{
					tooltips.RemoveAt(index);
				}
			}
			if (item.prefix == ModContent.PrefixType<Shrunken>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& tt.Name.StartsWith("PrefixSpeed"));
				if (index != -1)
				{
					tooltips.Insert(index, new TooltipLine(Mod, "PrefixTool",
						((item.GetGlobalItem<AvalonGlobalItemInstance>().TileBoostSaved - 1) < 0 ? "" : "+") +
						(item.GetGlobalItem<AvalonGlobalItemInstance>().TileBoostSaved - 1).ToString() +
						Language.GetTextValue("Mods.Avalon.PrefixTooltips.Range"))
					{
						IsModifier = true,
						IsModifierBad = true
					}); ;
				}
				index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& tt.Name.StartsWith("TileBoost"));
				if (index != -1)
				{
					tooltips.RemoveAt(index);
				}
			}
		}
		if (item.IsArmor() && !item.social)
		{
			if (item.prefix == ModContent.PrefixType<Busted>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccDefense", "-1 " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.Defense"))
					{
						IsModifier = true,
						IsModifierBad = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Loaded>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccDefense", "+1 " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.Defense"))
					{
						IsModifier = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Protective>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccDefense", "+2 " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.Defense"))
					{
						IsModifier = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Disgusting>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccDefense", "-2 " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.Defense"))
					{
						IsModifier = true,
						IsModifierBad = true
					});
					tooltips.Insert(index + 2, new TooltipLine(Mod, "PrefixAccStinky", Language.GetTextValue("Mods.Avalon.PrefixTooltips.Stinky"))
					{
						IsModifier = true,
						IsModifierBad = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Barbaric>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccDefense", "+3% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.Damage"))
					{
						IsModifier = true
					});
					tooltips.Insert(index + 2, new TooltipLine(Mod, "PrefixAccKnockback", "+6% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.Knockback"))
					{
						IsModifier = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Bloated>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccMeleeDamage", "+5% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.MeleeDamage"))
					{
						IsModifier = true
					});
					tooltips.Insert(index + 2, new TooltipLine(Mod, "PrefixAccKnockback", "-2% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.MeleeSpeed"))
					{
						IsModifier = true,
						IsModifierBad = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Boosted>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccMoveSpeed", "+4% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.MovementSpeed"))
					{
						IsModifier = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Fluidic>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccMoveSpeed", "+5% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.MovementSpeed"))
					{
						IsModifier = true
					});
					tooltips.Insert(index + 2, new TooltipLine(Mod, "PrefixAccIgnoreWater", "+25% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.Fluidic"))
					{
						IsModifier = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Glorious>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccDamage", "+3% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.Damage"))
					{
						IsModifier = true
					});
					tooltips.Insert(index + 2, new TooltipLine(Mod, "PrefixAccDefense", "+1 " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.Defense"))
					{
						IsModifier = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Handy>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccBlockRange", "+1 " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.PlacementRange"))
					{
						IsModifier = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Insane>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccTileSpeed", "+30% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.TileSpeed"))
					{
						IsModifier = true
					});
					tooltips.Insert(index + 2, new TooltipLine(Mod, "PrefixAccWallSpeed", "+30% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.WallSpeed"))
					{
						IsModifier = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Mythic>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccMana", "+20 " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.Mana"))
					{
						IsModifier = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Sharpened>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccArmorPen", "+2 " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.ArmorPenetration"))
					{
						IsModifier = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Silly>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccCritChance", "+2% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.CritChance"))
					{
						IsModifier = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Slimy>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccEndurance", "-3% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.DamageTaken"))
					{
						IsModifier = true
					});
				}
			}
		}
		if (item.accessory && !item.social)
		{
			if (item.prefix == ModContent.PrefixType<Enchanted>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable") || tt.Name.Equals("Expert")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccMana", "+20 " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.Mana"))
					{
						IsModifier = true
					});
					tooltips.Insert(index + 2, new TooltipLine(Mod, "PrefixAccMoveSpeed", "+3% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.MovementSpeed"))
					{
						IsModifier = true
					});
					tooltips.Insert(index + 3, new TooltipLine(Mod, "PrefixAccDefense", "+1 " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.Defense"))
					{
						IsModifier = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Robust>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable") || tt.Name.Equals("Expert")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccDefense", "+3 " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.Defense"))
					{
						IsModifier = true
					});
					tooltips.Insert(index + 2, new TooltipLine(Mod, "PrefixAccDamage", "+3% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.Damage"))
					{
						IsModifier = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Lurid>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable") || tt.Name.Equals("Expert")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccCritBonus", "+2% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.CritChance"))
					{
						IsModifier = true
					});
					tooltips.Insert(index + 2, new TooltipLine(Mod, "PrefixAccDefense", "+2 " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.Defense"))
					{
						IsModifier = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Hoarding>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccAmmo", Language.GetTextValue("Mods.Avalon.PrefixTooltips.Ammo"))
					{
						IsModifier = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Greedy>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccMoney", Language.GetTextValue("Mods.Avalon.PrefixTooltips.Money"))
					{
						IsModifier = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Motivated>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccStamRegen", Language.GetTextValue("Mods.Avalon.PrefixTooltips.StamRegen"))
					{
						IsModifier = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Bogus>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccCritBonus", "+4% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.CritDamage"))
					{
						IsModifier = true
					});
					tooltips.Insert(index + 2, new TooltipLine(Mod, "PrefixAccCritBonus", "+2% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.CritChance"))
					{
						IsModifier = true
					});
					tooltips.Insert(index + 3, new TooltipLine(Mod, "PrefixAccMaxCritReduction", "-1% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.CritCap"))
					{
						IsModifier = true,
						IsModifierBad = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Languid>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable") || tt.Name.Equals("Expert")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccMoveSpeed", "-2% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.MovementSpeed"))
					{
						IsModifier = true,
						IsModifierBad = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Magical>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable") || tt.Name.Equals("Expert")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccMana", "+40 " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.Mana"))
					{
						IsModifier = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Sturdy>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable") || tt.Name.Equals("Expert")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccEndurance", "-1% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.DamageTaken"))
					{
						IsModifier = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Timid>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable") || tt.Name.Equals("Expert")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccMeleeSpeed", "-2% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.MeleeSpeed"))
					{
						IsModifier = true,
						IsModifierBad = true
					});
				}
			}
			if (item.prefix == ModContent.PrefixType<Vigorous>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
						&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable") || tt.Name.Equals("Expert")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccDamage", "+2% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.Damage"))
					{
						IsModifier = true
					});
					tooltips.Insert(index + 2, new TooltipLine(Mod, "PrefixAccMeleeSpeed", "+2% " + Language.GetTextValue("Mods.Avalon.PrefixTooltips.MeleeSpeed"))
					{
						IsModifier = true
					});
				}
			}
		}

		//if (tooltipMat != null)
		//{
		//    if (item.GetGlobalItem<AvalonGlobalItemInstance>().TomeMaterial)
		//    {
		//        tooltipMat.Text = Language.GetTextValue("Mods.Avalon.CommonItemTooltip.TomeMaterial");
		//    }
		//}
		if (tooltipLine != null && (ModContent.GetInstance<AvalonConfig>().VanillaRenames || ModContent.GetInstance<AvalonConfig>().VanillaTextureReplacement))
		{
			//if (item.type == ItemID.BloodMoonStarter)
			//{
			//    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.BloodyTear");
			//}
		}
		if (tooltipLine != null && ModContent.GetInstance<AvalonConfig>().VanillaRenames)
		{
			//if (item.type == ItemID.CoinGun)
			//{
			//    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.CoinGun");
			//}
			//if (item.type == ItemID.FieryGreatsword)
			//{
			//    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.FieryGreatsword");
			//}
			//if (item.type == ItemID.PurpleMucos)
			//{
			//    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.PurpleMucus");
			//}
			//if (item.type == ItemID.HighTestFishingLine)
			//{
			//    tooltipLine.Text = tooltipLine.Text.Replace("Test", "Tensile");
			//}
			//if (item.type == ItemID.BlueSolution)
			//{
			//    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Hallow");
			//}
			//if (item.type == ItemID.DarkBlueSolution)
			//{
			//    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Mushrooms");
			//}
			//if (item.type == ItemID.GreenSolution)
			//{
			//    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Purity");
			//}
			//if (item.type == ItemID.PurpleSolution)
			//{
			//    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Corruption");
			//}
			//if (item.type == ItemID.RedSolution)
			//{
			//    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Crimson");
			//}
			//if (item.type == ItemID.SandSolution)
			//{
			//    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Desert");
			//}
			//if (item.type == ItemID.SnowSolution)
			//{
			//    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Snow");
			//}
			//if (item.type == ItemID.DirtSolution)
			//{
			//    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Forest");
			//}
			//if (item.type == ItemID.FrostsparkBoots)
			//{
			//    tooltipLine.Text = tooltipLine.Text.Replace("Frostspark", "Sparkfrost");
			//}
		}

		if (!item.social && PrefixLoader.GetPrefix(item.prefix) is ExxoPrefix exxoPrefix)
		{
			if (exxoPrefix.Category is PrefixCategory.Accessory or PrefixCategory.Custom)
			{
				tooltips.AddRange(exxoPrefix.TooltipLines);
			}
		}

		#region vanillla tooltip edits
		switch (item.type)
		{
			//case ItemID.Vine:
			//    tooltips.Add(new TooltipLine(Mod, "Rope", "Can be climbed on"));
			//    break;
			case ItemID.IceBlade:
				foreach (TooltipLine tooltip in tooltips)
				{
					if (tooltip.Name == "Tooltip0")
					{
						tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.IceBlade");
					}
				}
				break;
			case ItemID.AntlionClaw:
				foreach (TooltipLine tooltip in tooltips)
				{
					if (tooltip.Name == "Tooltip0")
					{
						tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.MandibleBlade");
					}
				}
				break;
			case ItemID.Frostbrand:
				foreach (TooltipLine tooltip in tooltips)
				{
					if (tooltip.Name == "Tooltip0")
					{
						tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.Frostbrand");
					}
				}
				break;
			case ItemID.DeathbringerPickaxe:
			case ItemID.NightmarePickaxe:
				foreach (TooltipLine tooltip in tooltips)
				{
					if (tooltip.Name == "Tooltip0")
					{
						tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.EvilPickaxe");
					}
				}
				break;
			case ItemID.HighTestFishingLine:
				foreach (TooltipLine tooltip in tooltips)
				{
					if (tooltip.Name == "Tooltip0")
					{
						tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.FishingLine");
					}
				}
				break;
			case ItemID.Seed:
				foreach (TooltipLine tooltip in tooltips)
				{
					if (tooltip.Name == "Tooltip0")
					{
						tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.Blowpipes");
					}
				}
				break;
			case ItemID.TempleKey:
				foreach (TooltipLine tooltip in tooltips)
				{
					if (tooltip.Name == "Tooltip0")
					{
						tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.TempleKey");
					}
				}
				break;
			case ItemID.PoisonDart:
				foreach (TooltipLine tooltip in tooltips)
				{
					if (tooltip.Name == "Tooltip1")
					{
						tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.PoisonDart");
					}
				}
				break;
			case ItemID.CoinGun:
				foreach (TooltipLine tooltip in tooltips)
				{
					if (tooltip.Name == "Tooltip0")
					{
						tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.CoinGun.PartOne");
					}
					if (tooltip.Name == "Tooltip1")
					{
						tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.CoinGun.PartTwo");
					}
				}
				break;
			case ItemID.PickaxeAxe:
				foreach (TooltipLine tooltip in tooltips)
				{
					if (tooltip.Name == "Tooltip0")
					{
						tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.PickaxeAxe.PartOne");
					}
					if (tooltip.Name == "Tooltip1")
					{
						tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.PickaxeAxe.PartTwo");
					}
				}
				break;
			case ItemID.Drax:
				foreach (TooltipLine tooltip in tooltips)
				{
					if (tooltip.Name == "Tooltip0")
					{
						tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.Drax.PartOne");
					}
					if (tooltip.Name == "Tooltip1")
					{
						tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.Drax.PartTwo");
					}
				}
				break;
		}
		#endregion
	}
	public override void UpdateVanity(Item item, Player player)
	{
		if (item.type == ItemID.HighTestFishingLine)
		{
			player.accFishingLine = true;
		}
	}
	public override bool AllowPrefix(Item item, int pre)
	{
		if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome || item.GetGlobalItem<AvalonGlobalItemInstance>().StaminaScroll || item.GetGlobalItem<AvalonGlobalItemInstance>().Genie)
		{
			return false;
		}
		return base.AllowPrefix(item, pre);
	}
	public override bool? PrefixChance(Item item, int pre, UnifiedRandom rand)
	{
		if (item.IsArmor() && pre == -3)
		{
			return true;
		}
		if ((item.GetGlobalItem<AvalonGlobalItemInstance>().Tome ||
			item.GetGlobalItem<AvalonGlobalItemInstance>().StaminaScroll ||
			item.GetGlobalItem<AvalonGlobalItemInstance>().Genie) && (pre == -1 || pre == -3))
		{
			return false;
		}

		return base.PrefixChance(item, pre, rand);
	}
	public override void UseAnimation(Item item, Player player)
	{
		#region aoe pick mining prefix
		if (item.prefix == ModContent.PrefixType<Expanded>())
		{
			if (player.whoAmI == Main.myPlayer)
			{
				if (item.pick > 0)
				{
					Point p = Main.MouseWorld.ToTileCoordinates();
					if (player.IsInTileInteractionRange(Player.tileTargetX, Player.tileTargetY, TileReachCheckSettings.Simple))
					{
						for (int x = p.X; x <= p.X + 1; x++)
						{
							for (int y = p.Y; y <= p.Y + 1; y++)
							{
								//if (x == p.X && y == p.Y)
								//player.ClearMiningCacheAt(p.X, p.Y, Main.tile[x, y].TileType);
								if (x == p.X && y == p.Y)
								{
									//player.ClearMiningCacheAt(x, y, Main.tile[x, y].TileType);
									continue;
								}
								//else 
								player.PickTile(x, y, item.pick);
							}
						}
					}
				}
				if (item.axe > 0)
				{
					int dmgAmt = (int)(item.axe * 1.2f);
					Point tilePosPoint = Main.MouseWorld.ToTileCoordinates();
					if (player.IsInTileInteractionRange(tilePosPoint.X, tilePosPoint.Y, TileReachCheckSettings.Simple))
					{
						for (int x = tilePosPoint.X; x <= tilePosPoint.X + 1; x++)
						{
							for (int y = tilePosPoint.Y; y <= tilePosPoint.Y + 1; y++)
							{
								int tileType = Main.tile[x, y].TileType;
								if (Main.tile[x, y].HasTile && Main.tileAxe[tileType])
								{
									if (!WorldGen.CanKillTile(x, y))
									{
										dmgAmt = 0;
									}
									if (player.hitTile.AddDamage(tileType, dmgAmt) >= 100)
									{
										player.ClearMiningCacheAt(x, y, 1);
										WorldGen.KillTile(x, y);
										if (Main.netMode == NetmodeID.MultiplayerClient)
										{
											NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, x, y);
										}
										player.hitTile.Clear(tileType);
									}
									if (dmgAmt != 0)
									{
										player.hitTile.Prune();
									}
								}
							}
						}
					}
				}
				if (item.hammer > 0)
				{
					int dmgAmt = (int)(item.hammer * 1.5f);
					Point tilePosPoint = Main.MouseWorld.ToTileCoordinates();
					if (player.IsInTileInteractionRange(tilePosPoint.X, tilePosPoint.Y, TileReachCheckSettings.Simple))
					{
						for (int x = tilePosPoint.X; x <= tilePosPoint.X + 1; x++)
						{
							for (int y = tilePosPoint.Y; y <= tilePosPoint.Y + 1; y++)
							{
								if (Main.tile[x, y].WallType > 0 && (!Main.tile[x, y].HasTile || x != tilePosPoint.X || y != tilePosPoint.Y || (!Main.tileHammer[Main.tile[x, y].TileType] && !player.poundRelease)) && player.toolTime == 0 && player.itemAnimation > 0 && player.controlUseItem && item.hammer > 0 && Player.CanPlayerSmashWall(x, y))
								{
									player.PickWall(x, y, dmgAmt);
									player.itemTime = item.useTime / 2;
								}
							}
						}
					}
				}
			}
		}

		#endregion
	}
	public override bool? UseItem(Item item, Player player)
	{
		#region aoe pick mining prefix
		//if (item.prefix == ModContent.PrefixType<Area>())
		//{
		//    if (player.whoAmI == Main.myPlayer && player.ItemAnimationJustStarted)
		//    {
		//        if (item.pick > 0)
		//        {
		//            int savedPickPower = item.pick;
		//            item.pick = 0;
		//            Point p = player.GetModPlayer<AvalonPlayer>().MousePosition.ToTileCoordinates();
		//            if (player.IsInTileInteractionRange(Player.tileTargetX, Player.tileTargetY, TileReachCheckSettings.Simple))
		//            {
		//                for (int x = p.X; x <= p.X + 1; x++)
		//                {
		//                    for (int y = p.Y; y <= p.Y + 1; y++)
		//                    {
		//                        //if (x == p.X && y == p.Y)
		//                            //player.ClearMiningCacheAt(p.X, p.Y, Main.tile[x, y].TileType);
		//                        //if (x == p.X && y == p.Y)
		//                        //{
		//                        //    //player.ClearMiningCacheAt(x, y, Main.tile[x, y].TileType);
		//                        //    continue;
		//                        //}
		//                        //else 
		//                            player.PickTile(x, y, savedPickPower);
		//                    }
		//                }
		//            }
		//            item.pick = savedPickPower;
		//        }
		//        if (item.axe > 0)
		//        {
		//            int dmgAmt = (int)(item.axe * 1.2f);
		//            Point tilePosPoint = player.GetModPlayer<AvalonPlayer>().MousePosition.ToTileCoordinates();
		//            if (player.IsInTileInteractionRange(tilePosPoint.X, tilePosPoint.Y, TileReachCheckSettings.Simple))
		//            {
		//                for (int x = tilePosPoint.X; x <= tilePosPoint.X + 1; x++)
		//                {
		//                    for (int y = tilePosPoint.Y; y <= tilePosPoint.Y + 1; y++)
		//                    {
		//                        int tileType = Main.tile[x, y].TileType;
		//                        if (Main.tile[x, y].HasTile && Main.tileAxe[tileType])
		//                        {
		//                            if (!WorldGen.CanKillTile(x, y))
		//                            {
		//                                dmgAmt = 0;
		//                            }
		//                            if (player.hitTile.AddDamage(tileType, dmgAmt) >= 100)
		//                            {
		//                                player.ClearMiningCacheAt(x, y, 1);
		//                                WorldGen.KillTile(x, y);
		//                                if (Main.netMode == NetmodeID.MultiplayerClient)
		//                                {
		//                                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, x, y);
		//                                }
		//                                player.hitTile.Clear(tileType);
		//                            }
		//                            if (dmgAmt != 0)
		//                            {
		//                                player.hitTile.Prune();
		//                            }
		//                        }
		//                    }
		//                }
		//            }
		//        }
		//        if (item.hammer > 0)
		//        {
		//            int dmgAmt = (int)(item.hammer * 1.5f);
		//            Point tilePosPoint = player.GetModPlayer<AvalonPlayer>().MousePosition.ToTileCoordinates();
		//            if (player.IsInTileInteractionRange(tilePosPoint.X, tilePosPoint.Y, TileReachCheckSettings.Simple))
		//            {
		//                for (int x = tilePosPoint.X; x <= tilePosPoint.X + 1; x++)
		//                {
		//                    for (int y = tilePosPoint.Y; y <= tilePosPoint.Y + 1; y++)
		//                    {
		//                        if (Main.tile[x, y].WallType > 0 && (!Main.tile[x, y].HasTile || x != tilePosPoint.X || y != tilePosPoint.Y || (!Main.tileHammer[Main.tile[x, y].TileType] && !player.poundRelease)) && player.toolTime == 0 && player.itemAnimation > 0 && player.controlUseItem && item.hammer > 0 && Player.CanPlayerSmashWall(x, y))
		//                        {
		//                            player.PickWall(x, y, dmgAmt);
		//                            player.itemTime = item.useTime / 2;
		//                        }
		//                    }
		//                }
		//            }
		//        }
		//    }
		//}

		#endregion

		#region oil bottle
		if (player.GetModPlayer<AvalonPlayer>().OilBottle && player.whoAmI == Main.myPlayer && player.GetModPlayer<AvalonPlayer>().OilBottleTimer == 0 &&
			player.itemAnimation > 0 && item.damage > 0)
		{
			player.GetModPlayer<AvalonPlayer>().OilBottleTimer = 120;
			Vector2 center = player.Center;
			Vector2 vector = player.DirectionTo(player.ApplyRangeCompensation(0.2f, center, Main.MouseWorld)) * 10f;
			Projectile.NewProjectile(player.GetSource_FromThis(), center.X, center.Y, vector.X, vector.Y, ModContent.ProjectileType<Projectiles.OilBottle>(), 13, 3f, player.whoAmI);
		}
		#endregion

		#region cloud/obsidian glove
		if (player.GetModPlayer<AvalonPlayer>().CloudGlove && player.whoAmI == Main.myPlayer && Main.mouseLeft && item.type != ModContent.ItemType<DungeonWand>() &&
			item.tileWand == -1)
		{
			if (ModContent.GetInstance<CloudGloveBuilderToggle>().CurrentState == 0)
			{
				bool inrange = player.IsInTileInteractionRange(Player.tileTargetX, Player.tileTargetY, TileReachCheckSettings.Simple);
				if (item.createTile > -1 &&
					(Main.tileSolid[item.createTile] || nonSolidExceptions.Contains(item.createTile)) &&
					(Main.tile[Player.tileTargetX, Player.tileTargetY].LiquidType != LiquidID.Lava ||
					 player.GetModPlayer<AvalonPlayer>().ObsidianGlove) &&
					!Main.tile[Player.tileTargetX, Player.tileTargetY].HasTile && inrange)
				{
					bool subtractFromStack = WorldGen.PlaceTile(Player.tileTargetX, Player.tileTargetY, item.createTile);
					if (Main.tile[Player.tileTargetX, Player.tileTargetY].HasTile &&
						Main.netMode != NetmodeID.SinglePlayer && subtractFromStack)
					{
						NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, Player.tileTargetX,
							Player.tileTargetY, item.createTile);
					}

					if (subtractFromStack)
					{
						item.stack--;
					}
				}

				if (item.createWall > 0 && Main.tile[Player.tileTargetX, Player.tileTargetY].WallType == 0 && inrange)
				{
					WorldGen.PlaceWall(Player.tileTargetX, Player.tileTargetY, item.createWall);
					if (Main.tile[Player.tileTargetX, Player.tileTargetY].WallType != 0 &&
						Main.netMode != NetmodeID.SinglePlayer)
					{
						NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 3, Player.tileTargetX,
							Player.tileTargetY, item.createWall);
					}

					//Main.PlaySound(0, Player.tileTargetX * 16, Player.tileTargetY * 16, 1);
					item.stack--;
				}
			}
		}
		#endregion
		return base.UseItem(item, player);
	}
	public static int DungeonWallItemToBackwallID(int type)
	{
		if (type == ItemID.BlueBrickWall) return WallID.BlueDungeonUnsafe;
		else if (type == ItemID.BlueSlabWall) return WallID.BlueDungeonSlabUnsafe;
		else if (type == ItemID.BlueTiledWall) return WallID.BlueDungeonTileUnsafe;
		else if (type == ItemID.GreenBrickWall) return WallID.GreenDungeonUnsafe;
		else if (type == ItemID.GreenSlabWall) return WallID.GreenDungeonSlabUnsafe;
		else if (type == ItemID.GreenTiledWall) return WallID.GreenDungeonTileUnsafe;
		else if (type == ItemID.PinkBrickWall) return WallID.PinkDungeonUnsafe;
		else if (type == ItemID.PinkSlabWall) return WallID.PinkDungeonSlabUnsafe;
		else if (type == ItemID.PinkTiledWall) return WallID.PinkDungeonTileUnsafe;
		else if (type == ModContent.ItemType<Items.Placeable.Wall.OrangeBrickWall>()) return ModContent.WallType<Walls.OrangeBrickUnsafe>();
		else if (type == ModContent.ItemType<Items.Placeable.Wall.OrangeSlabWall>()) return ModContent.WallType<Walls.OrangeSlabUnsafe>();
		else if (type == ModContent.ItemType<Items.Placeable.Wall.OrangeTiledWall>()) return ModContent.WallType<Walls.OrangeTiledUnsafe>();
		else if (type == ModContent.ItemType<Items.Placeable.Wall.PurpleBrickWall>()) return ModContent.WallType<Walls.PurpleBrickUnsafe>();
		else if (type == ModContent.ItemType<Items.Placeable.Wall.PurpleSlabWall>()) return ModContent.WallType<Walls.PurpleSlabWallUnsafe>();
		else if (type == ModContent.ItemType<Items.Placeable.Wall.PurpleTiledWall>()) return ModContent.WallType<Walls.PurpleTiledWallUnsafe>();
		else if (type == ModContent.ItemType<Items.Placeable.Wall.YellowBrickWall>()) return ModContent.WallType<Walls.YellowBrickUnsafe>();
		else if (type == ModContent.ItemType<Items.Placeable.Wall.YellowSlabWall>()) return ModContent.WallType<Walls.YellowSlabWallUnsafe>();
		else if (type == ModContent.ItemType<Items.Placeable.Wall.YellowTiledWall>()) return ModContent.WallType<Walls.YellowTiledWallUnsafe>();
		return 0;
	}
	public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		if (item.useAmmo == AmmoID.Arrow || item.useAmmo == AmmoID.Bullet || item.useAmmo == AmmoID.Rocket)
		{
			if (type == ModContent.ProjectileType<Projectiles.Ranged.ShroomiteArrow>())
			{
				ShroomiteAmmoCounter++;
				if (ShroomiteAmmoCounter == 1)
				{
					type = ModContent.ProjectileType<Projectiles.Ranged.ShroomiteBullet>();
				}
				else if (ShroomiteAmmoCounter == 2)
				{
					type = ModContent.ProjectileType<Projectiles.Ranged.ShroomiteRocket>();
				}
				else if (ShroomiteAmmoCounter == 3)
				{
					type = ModContent.ProjectileType<Projectiles.Ranged.ShroomiteArrow>();
					ShroomiteAmmoCounter = 0;
				}
			}
		}
		base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
	}
	public override bool CanUseItem(Item item, Player player)
	{
		// potion/elixir usage lockout
		if (Data.Sets.ItemSets.ElixirToPotionBuffID.ContainsKey(item.type) && player.HasBuff(Data.Sets.ItemSets.ElixirToPotionBuffID[item.type]) ||
			Data.Sets.ItemSets.PotionToElixirBuffID.ContainsKey(item.type) && player.HasBuff(Data.Sets.ItemSets.PotionToElixirBuffID[item.type]))
		{
			return false;
		}
		return base.CanUseItem(item, player);
	}
	public override int ChoosePrefix(Item item, UnifiedRandom rand)
	{
		return item.IsArmor() ? Main.rand.Next(ExxoPrefix.ExxoCategoryPrefixes[ExxoPrefixCategory.Armor]).Type : base.ChoosePrefix(item, rand);
	}
}
