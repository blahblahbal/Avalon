using Avalon.Common;
using Avalon.Items.Material;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon.ModSupport.Ophioid.Items
{
	[ExtendsFromMod("OphioidMod")]
	class OphioidSystems : ModSystem
	{
		// the string is set in the LoadWorldData method in AvalonWorld.cs
		public override void OnWorldUnload()
		{
			ExxoAvalonOrigins.OphioidMod.Call("SetCustomWorldEvilForDeathMessage", string.Empty);
		}
	}
	[ExtendsFromMod("OphioidMod")]
	class ModifyLivingCarrionShimmer : GlobalItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			if (ExxoAvalonOrigins.OphioidMod.TryFind("LivingCarrion", out ModItem LivingCarrion))
			{
				ItemID.Sets.ShimmerTransformToItem[LivingCarrion.Type] = ModContent.ItemType<CreepingColony>();
			}
		}
	}

	[ExtendsFromMod("OphioidMod")]
	class CreepingColony : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Creeping Colony");
			// Tooltip.SetDefault("'A putrid stench comes from the thing you just made, it might attract something...' \nSummons Ophiopede in a Corruption world");
			Item.ResearchUnlockCount = 3;
			ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;
			if (ExxoAvalonOrigins.OphioidMod.TryFind("DeadFungusbug", out ModItem DeadFungusBug))
			{
				ItemID.Sets.ShimmerTransformToItem[Type] = DeadFungusBug.Type;
			}
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
			return !(bool)ExxoAvalonOrigins.OphioidMod.Call("OphioidBossIsActive");
		}

		public override bool? UseItem(Player player)
		{
			if (ExxoAvalonOrigins.OphioidMod.TryFind("OphiopedeHead", out ModNPC OphiopedeHead))
			{
				if (player.whoAmI == Main.myPlayer)
				{
					// If the player using the item is the client
					// (explicitly excluded serverside here)
					SoundEngine.PlaySound(SoundID.Roar, player.position);

					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						// If the player is not in multiplayer, spawn directly
						NPC.SpawnOnPlayer(player.whoAmI, OphiopedeHead.Type);
					}
					else
					{
						// If the player is in multiplayer, request a spawn
						// This will only work if NPCID.Sets.MPAllowedEnemies[type] is true, which we set in OphiopedeHead
						NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: OphiopedeHead.Type);
					}
				}
				return true;
			}
			return false;

		}

		public override void AddRecipes()
		{
			if (ExxoAvalonOrigins.OphioidMod.TryFind("DeadFungusbug", out ModItem DeadFungusBug) && ExxoAvalonOrigins.OphioidMod.TryFind("LivingCarrion", out ModItem LivingCarrion) && ExxoAvalonOrigins.OphioidMod.TryFind("InfestedCompost", out ModItem InfestedCompost))
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
					.SortAfterFirstRecipesOf(LivingCarrion.Type)
					.Register();

				Recipe.Create(InfestedCompost.Type)
					.AddIngredient(ItemID.BeetleHusk, 4)
					.AddIngredient(DeadFungusBug.Type)
					.AddIngredient(ModContent.ItemType<CreepingColony>())
					.AddTile(TileID.MythrilAnvil)
					.Register();

				Recipe.Create(InfestedCompost.Type)
					.AddIngredient(ItemID.BeetleHusk, 4)
					.AddIngredient(LivingCarrion.Type)
					.AddIngredient(ModContent.ItemType<CreepingColony>())
					.AddTile(TileID.MythrilAnvil)
					.Register();
			}
		}
	}
}
