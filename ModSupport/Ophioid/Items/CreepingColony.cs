using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using OphioidMod.Items;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.Ophioid.Items
{
	[ExtendsFromMod("OphioidMod")]
	class ModifyLivingCarrionShimmer : GlobalItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<LivingCarrion>()] = ModContent.ItemType<CreepingColony>();
		}
	}

	[ExtendsFromMod("OphioidMod")]
	class CreepingColony : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ModLoader.HasMod("OphioidMod");
		}
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Creeping Colony");
			// Tooltip.SetDefault("'A putrid stench comes from the thing you just made, it might attract something...' \nSummons Ophiopede in a Corruption world");
			Item.ResearchUnlockCount = 3;
			ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;
			ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<DeadFungusbug>();
		}
		public override void SetDefaults()
		{
			Item.width = 12;
			Item.height = 12;
			Item.maxStack = Item.CommonMaxStack;
			Item.rare = ItemRarityID.Orange;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
		}

		public override bool CanUseItem(Player player)
		{
			return !OphioidMod.OphioidWorld.OphioidBoss;
		}

		public override bool? UseItem(Player player)
		{
			if (player.whoAmI == Main.myPlayer)
			{
				// If the player using the item is the client
				// (explicitly excluded serverside here)
				SoundEngine.PlaySound(SoundID.Roar, player.position);

				int type = ModContent.NPCType<OphioidMod.NPCs.OphiopedeHead>();

				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					// If the player is not in multiplayer, spawn directly
					NPC.SpawnOnPlayer(player.whoAmI, type);
				}
				else
				{
					// If the player is in multiplayer, request a spawn
					// This will only work if NPCID.Sets.MPAllowedEnemies[type] is true, which we set in OphiopedeHead
					NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
				}
			}

			return true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.SoulofNight, 1)
				.AddIngredient(ItemID.SoulofLight, 1)
				.AddIngredient(ItemID.SoulofFright, 1)
				.AddIngredient(ItemID.SoulofSight, 1)
				.AddIngredient(ItemID.SoulofMight, 1)
				.AddIngredient(ModContent.ItemType<Booger>(), 5)
				.AddIngredient(ModContent.ItemType<Pathogen>(), 3)
				.AddIngredient(ModContent.ItemType<YuckyBit>(), 3)
				.AddTile(TileID.MythrilAnvil)
				.SortAfterFirstRecipesOf(ModContent.ItemType<LivingCarrion>())
				.Register();

			Recipe.Create(ModContent.ItemType<InfestedCompost>())
				.AddIngredient(ItemID.BeetleHusk, 4)
				.AddIngredient(ModContent.ItemType<DeadFungusbug>())
				.AddIngredient(ModContent.ItemType<CreepingColony>())
				.AddTile(TileID.MythrilAnvil)
				.Register();

			Recipe.Create(ModContent.ItemType<InfestedCompost>())
				.AddIngredient(ItemID.BeetleHusk, 4)
				.AddIngredient(ModContent.ItemType<LivingCarrion>())
				.AddIngredient(ModContent.ItemType<CreepingColony>())
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
