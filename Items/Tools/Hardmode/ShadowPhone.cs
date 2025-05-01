using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Items.Accessories.Info;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class ShadowPhone : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToConsumable(false, useTurn: true, width: 24, height: 28);
		Item.maxStack = 1;
		Item.rare = ItemRarityID.Red;
		Item.value = Item.sellPrice(0, 10);
	}
	public override void HoldItem(Player player)
	{
		if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
		{
			SoundEngine.PlaySound(SoundID.Unlock, player.position);
			Item.ChangeItemType(ModContent.ItemType<ShadowPhoneSurface>());
		}
	}
	public override bool? UseItem(Player player)
	{
		player.Shellphone_Spawn();
		SoundEngine.PlaySound(SoundID.Item6, player.position);
		return true;
	}
	public override void UpdateInfoAccessory(Player player)
	{
		player.GetModPlayer<EyeoftheGodsPlayer>().DamageDisplay = true;
		player.GetModPlayer<EyeoftheGodsPlayer>().DefenseDisplay = true;
		player.GetModPlayer<CalcSpecPlayer>().CalcSpecDisplay = true;
		player.accThirdEye = player.accFishFinder = player.accWeatherRadio = player.accCalendar = player.accCritterGuide = player.accDreamCatcher =
			player.accJarOfSouls = player.accStopwatch = player.accOreFinder = true;
		player.accWatch = 3;
		player.accDepthMeter = 1;
		player.accCompass = 1;
	}
}

public class ShadowPhoneSurface : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToConsumable(false, useTurn: true, width: 24, height: 28);
		Item.maxStack = 1;
		Item.rare = ItemRarityID.Red;
		Item.value = Item.sellPrice(0, 10);
	}
	public override void HoldItem(Player player)
	{
		if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
		{
			SoundEngine.PlaySound(SoundID.Unlock, player.position);
			Item.ChangeItemType(ModContent.ItemType<ShadowPhoneHome>());
		}
	}
	public override bool? UseItem(Player player)
	{
		TeleportToSurface(player);
		SoundEngine.PlaySound(SoundID.Item6, player.position);
		return true;
	}

	public void TeleportToSurface(Player p)
	{
		//p.noFallDmg = true;
		float xpos = p.position.X;
		float ypos = (float)(Main.worldSurface / 2f) * 16f;
		if (!Main.tile[(int)(xpos / 16f), (int)(ypos / 16f) + 3].HasTile)
		{
			while (!Main.tile[(int)(xpos / 16f), (int)(ypos / 16f) + 4].HasTile)
			{
				ypos += 16f;
			}
		}
		else
		{
			while (Main.tile[(int)(xpos / 16f), (int)(ypos / 16f) + 4].HasTile)
			{
				ypos -= 16f;
			}
		}
		Vector2 newPos = new(xpos, ypos);
		p.Teleport(newPos, 7);
		p.velocity = Vector2.Zero;
		if (Main.netMode == NetmodeID.Server)
		{
			RemoteClient.CheckSection(p.whoAmI, p.position);
			NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, p.whoAmI, newPos.X, newPos.Y, 7);
		}
	}

	public override void UpdateInfoAccessory(Player player)
	{
		player.GetModPlayer<EyeoftheGodsPlayer>().DamageDisplay = true;
		player.GetModPlayer<EyeoftheGodsPlayer>().DefenseDisplay = true;
		player.GetModPlayer<CalcSpecPlayer>().CalcSpecDisplay = true;
		player.accThirdEye = player.accFishFinder = player.accWeatherRadio = player.accCalendar = player.accCritterGuide = player.accDreamCatcher =
			player.accJarOfSouls = player.accStopwatch = player.accOreFinder = true;
		player.accWatch = 3;
		player.accDepthMeter = 1;
		player.accCompass = 1;
	}
}

public class ShadowPhoneDungeon : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToConsumable(false, useTurn: true, width: 24, height: 28);
		Item.maxStack = 1;
		Item.rare = ItemRarityID.Red;
		Item.value = Item.sellPrice(0, 10);
	}
	public override void HoldItem(Player player)
	{
		if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
		{
			SoundEngine.PlaySound(SoundID.Unlock, player.position);
			Item.ChangeItemType(ModContent.ItemType<ShadowPhoneJungleTropics>());
		}
	}
	public override bool? UseItem(Player player)
	{
		DungeonPort(player);
		SoundEngine.PlaySound(SoundID.Item6, player.position);
		return true;
	}

	public void DungeonPort(Player player)
	{
		bool canSpawn = false;
		int num = Main.dungeonX;
		int num2 = 100;
		int num3 = num2 / 2;
		int teleportStartY = Main.dungeonY - 3;
		int teleportRangeY = 0;
		Player.RandomTeleportationAttemptSettings settings = new Player.RandomTeleportationAttemptSettings
		{
			mostlySolidFloor = true,
			avoidAnyLiquid = true,
			avoidLava = true,
			avoidHurtTiles = true,
			avoidWalls = true,
			attemptsBeforeGivingUp = 1000,
			maximumFallDistanceFromOrignalPoint = 30
		};
		Vector2 vector = player.CheckForGoodTeleportationSpot(ref canSpawn, num - num3, num2, teleportStartY, teleportRangeY, settings);
		if (!canSpawn)
		{
			vector = player.CheckForGoodTeleportationSpot(ref canSpawn, num - num2, num3, teleportStartY, teleportRangeY, settings);
		}
		if (!canSpawn)
		{
			vector = player.CheckForGoodTeleportationSpot(ref canSpawn, num + num3, num3, teleportStartY, teleportRangeY, settings);
		}
		if (canSpawn)
		{
			Vector2 newPos = vector;
			player.Teleport(newPos, 7);
			player.velocity = Vector2.Zero;
			if (Main.netMode == NetmodeID.Server)
			{
				RemoteClient.CheckSection(player.whoAmI, player.position);
				NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, player.whoAmI, newPos.X, newPos.Y, 7);
			}
		}
		else
		{
			Vector2 newPos2 = player.position;
			player.Teleport(newPos2, 7);
			player.velocity = Vector2.Zero;
			if (Main.netMode == NetmodeID.Server)
			{
				RemoteClient.CheckSection(player.whoAmI, player.position);
				NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, player.whoAmI, newPos2.X, newPos2.Y, 7, 1);
			}
		}
	}

	public override void UpdateInfoAccessory(Player player)
	{
		player.GetModPlayer<EyeoftheGodsPlayer>().DamageDisplay = true;
		player.GetModPlayer<EyeoftheGodsPlayer>().DefenseDisplay = true;
		player.GetModPlayer<CalcSpecPlayer>().CalcSpecDisplay = true;
		player.accThirdEye = player.accFishFinder = player.accWeatherRadio = player.accCalendar = player.accCritterGuide = player.accDreamCatcher =
			player.accJarOfSouls = player.accStopwatch = player.accOreFinder = true;
		player.accWatch = 3;
		player.accDepthMeter = 1;
		player.accCompass = 1;
	}
}

public class ShadowPhoneOcean : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToConsumable(false, useTurn: true, width: 24, height: 28);
		Item.maxStack = 1;
		Item.rare = ItemRarityID.Red;
		Item.value = Item.sellPrice(0, 10);
	}
	public override void HoldItem(Player player)
	{
		if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
		{
			SoundEngine.PlaySound(SoundID.Unlock, player.position);
			Item.ChangeItemType(ModContent.ItemType<ShadowPhoneHell>());
		}
	}
	public override bool? UseItem(Player player)
	{
		player.MagicConch();
		SoundEngine.PlaySound(SoundID.Item6, player.position);
		return true;
	}
	public override void UpdateInfoAccessory(Player player)
	{
		player.GetModPlayer<EyeoftheGodsPlayer>().DamageDisplay = true;
		player.GetModPlayer<EyeoftheGodsPlayer>().DefenseDisplay = true;
		player.GetModPlayer<CalcSpecPlayer>().CalcSpecDisplay = true;
		player.accThirdEye = player.accFishFinder = player.accWeatherRadio = player.accCalendar = player.accCritterGuide = player.accDreamCatcher =
			player.accJarOfSouls = player.accStopwatch = player.accOreFinder = true;
		player.accWatch = 3;
		player.accDepthMeter = 1;
		player.accCompass = 1;
	}
}

public class ShadowPhoneHell : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToConsumable(false, useTurn: true, width: 24, height: 28);
		Item.maxStack = 1;
		Item.rare = ItemRarityID.Red;
		Item.value = Item.sellPrice(0, 10);
	}
	public override void HoldItem(Player player)
	{
		if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
		{
			SoundEngine.PlaySound(SoundID.Unlock, player.position);
			Item.ChangeItemType(ModContent.ItemType<ShadowPhoneRandom>());
		}
	}
	public override bool? UseItem(Player player)
	{
		player.DemonConch();
		SoundEngine.PlaySound(SoundID.Item6, player.position);
		return true;
	}
	public override void UpdateInfoAccessory(Player player)
	{
		player.GetModPlayer<EyeoftheGodsPlayer>().DamageDisplay = true;
		player.GetModPlayer<EyeoftheGodsPlayer>().DefenseDisplay = true;
		player.GetModPlayer<CalcSpecPlayer>().CalcSpecDisplay = true;
		player.accThirdEye = player.accFishFinder = player.accWeatherRadio = player.accCalendar = player.accCritterGuide = player.accDreamCatcher =
			player.accJarOfSouls = player.accStopwatch = player.accOreFinder = true;
		player.accWatch = 3;
		player.accDepthMeter = 1;
		player.accCompass = 1;
	}
}

public class ShadowPhoneJungleTropics : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToConsumable(false, useTurn: true, width: 24, height: 28);
		Item.maxStack = 1;
		Item.rare = ItemRarityID.Red;
		Item.value = Item.sellPrice(0, 10);
	}
	public override bool? UseItem(Player player)
	{
		JungleTropicsPort(player);
		SoundEngine.PlaySound(SoundID.Item6, player.position);
		return true;
	}
	public override void HoldItem(Player player)
	{
		if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
		{
			SoundEngine.PlaySound(SoundID.Unlock, player.position);
			Item.ChangeItemType(ModContent.ItemType<ShadowPhoneOcean>());
		}
	}
	public void JungleTropicsPort(Player player)
	{
		bool canSpawn = false;
		int num = AvalonWorld.JungleLocationX;
		if (AvalonWorld.JungleLocationX == 0)
		{
			num = Main.maxTilesX - Main.dungeonX;
		}
		int num2 = 100;
		int num3 = num2 / 2;
		int teleportStartY = 300;
		int teleportRangeY = 50;
		Player.RandomTeleportationAttemptSettings settings = new Player.RandomTeleportationAttemptSettings
		{
			mostlySolidFloor = true,
			avoidAnyLiquid = false,
			avoidLava = true,
			avoidHurtTiles = true,
			avoidWalls = true,
			attemptsBeforeGivingUp = 1000,
			maximumFallDistanceFromOrignalPoint = 30
		};
		Vector2 vector = player.CheckForGoodTeleportationSpot(ref canSpawn, num - num3, num2, teleportStartY, teleportRangeY, settings);
		if (!canSpawn)
		{
			vector = player.CheckForGoodTeleportationSpot(ref canSpawn, num - num2, num3, teleportStartY, teleportRangeY, settings);
		}
		if (!canSpawn)
		{
			vector = player.CheckForGoodTeleportationSpot(ref canSpawn, num + num3, num3, teleportStartY, teleportRangeY, settings);
		}
		if (canSpawn)
		{
			Vector2 newPos = vector;
			player.Teleport(newPos, 7);
			player.velocity = Vector2.Zero;
			if (Main.netMode == NetmodeID.Server)
			{
				RemoteClient.CheckSection(player.whoAmI, player.position);
				NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, player.whoAmI, newPos.X, newPos.Y, 7);
			}
		}
		else
		{
			Vector2 newPos2 = player.position;
			player.Teleport(newPos2, 7);
			player.velocity = Vector2.Zero;
			if (Main.netMode == NetmodeID.Server)
			{
				RemoteClient.CheckSection(player.whoAmI, player.position);
				NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, player.whoAmI, newPos2.X, newPos2.Y, 7, 1);
			}
		}
	}

	public override void UpdateInfoAccessory(Player player)
	{
		player.GetModPlayer<EyeoftheGodsPlayer>().DamageDisplay = true;
		player.GetModPlayer<EyeoftheGodsPlayer>().DefenseDisplay = true;
		player.GetModPlayer<CalcSpecPlayer>().CalcSpecDisplay = true;
		player.accThirdEye = player.accFishFinder = player.accWeatherRadio = player.accCalendar = player.accCritterGuide = player.accDreamCatcher =
			player.accJarOfSouls = player.accStopwatch = player.accOreFinder = true;
		player.accWatch = 3;
		player.accDepthMeter = 1;
		player.accCompass = 1;
	}
}

public class ShadowPhoneRandom : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToConsumable(false, useTurn: true, width: 24, height: 28);
		Item.maxStack = 1;
		Item.rare = ItemRarityID.Red;
		Item.value = Item.sellPrice(0, 10);
	}
	public override void HoldItem(Player player)
	{
		if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
		{
			SoundEngine.PlaySound(SoundID.Unlock, player.position);
			Item.ChangeItemType(ModContent.ItemType<ShadowPhone>());
		}
	}
	public override bool? UseItem(Player player)
	{
		player.TeleportationPotion();
		SoundEngine.PlaySound(SoundID.Item6, player.position);
		return true;
	}

	public override void UpdateInfoAccessory(Player player)
	{
		player.GetModPlayer<EyeoftheGodsPlayer>().DamageDisplay = true;
		player.GetModPlayer<EyeoftheGodsPlayer>().DefenseDisplay = true;
		player.GetModPlayer<CalcSpecPlayer>().CalcSpecDisplay = true;
		player.accThirdEye = player.accFishFinder = player.accWeatherRadio = player.accCalendar = player.accCritterGuide = player.accDreamCatcher =
			player.accJarOfSouls = player.accStopwatch = player.accOreFinder = true;
		player.accWatch = 3;
		player.accDepthMeter = 1;
		player.accCompass = 1;
	}
}

public class ShadowPhoneHome : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToConsumable(false, useTurn: true, width: 24, height: 28);
		Item.maxStack = 1;
		Item.rare = ItemRarityID.Red;
		Item.value = Item.sellPrice(0, 10);
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = 0;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.ShellphoneDummy)
			.AddIngredient(ModContent.ItemType<EyeoftheGods>())
			.AddIngredient(ModContent.ItemType<CalculatorSpectacles>())
			.AddIngredient(ItemID.FallenStar, 40)
			.AddIngredient(ItemID.Diamond, 20)
			.AddIngredient(ItemID.ChlorophyteBar, 7)
			.AddIngredient(ItemID.Ectoplasm, 10)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ItemID.Shellphone)
			.AddIngredient(ModContent.ItemType<EyeoftheGods>())
			.AddIngredient(ModContent.ItemType<CalculatorSpectacles>())
			.AddIngredient(ItemID.FallenStar, 40)
			.AddIngredient(ItemID.Diamond, 20)
			.AddIngredient(ItemID.ChlorophyteBar, 7)
			.AddIngredient(ItemID.Ectoplasm, 10)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ItemID.ShellphoneSpawn)
			.AddIngredient(ModContent.ItemType<EyeoftheGods>())
			.AddIngredient(ModContent.ItemType<CalculatorSpectacles>())
			.AddIngredient(ItemID.FallenStar, 40)
			.AddIngredient(ItemID.Diamond, 20)
			.AddIngredient(ItemID.ChlorophyteBar, 7)
			.AddIngredient(ItemID.Ectoplasm, 10)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ItemID.ShellphoneOcean)
			.AddIngredient(ModContent.ItemType<EyeoftheGods>())
			.AddIngredient(ModContent.ItemType<CalculatorSpectacles>())
			.AddIngredient(ItemID.FallenStar, 40)
			.AddIngredient(ItemID.Diamond, 20)
			.AddIngredient(ItemID.ChlorophyteBar, 7)
			.AddIngredient(ItemID.Ectoplasm, 10)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ItemID.ShellphoneHell)
			.AddIngredient(ModContent.ItemType<EyeoftheGods>())
			.AddIngredient(ModContent.ItemType<CalculatorSpectacles>())
			.AddIngredient(ItemID.FallenStar, 40)
			.AddIngredient(ItemID.Diamond, 20)
			.AddIngredient(ItemID.ChlorophyteBar, 7)
			.AddIngredient(ItemID.Ectoplasm, 10)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
	public override bool? UseItem(Player player)
	{
		player.Spawn(PlayerSpawnContext.RecallFromItem);
		SoundEngine.PlaySound(SoundID.Item6, player.position);
		return true;
	}
	public override void HoldItem(Player player)
	{
		if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
		{
			SoundEngine.PlaySound(SoundID.Unlock, player.position);
			Item.ChangeItemType(ModContent.ItemType<ShadowPhoneDungeon>());
		}
	}
	public override void UpdateInfoAccessory(Player player)
	{
		player.GetModPlayer<EyeoftheGodsPlayer>().DamageDisplay = true;
		player.GetModPlayer<EyeoftheGodsPlayer>().DefenseDisplay = true;
		player.GetModPlayer<CalcSpecPlayer>().CalcSpecDisplay = true;
		player.accThirdEye = player.accFishFinder = player.accWeatherRadio = player.accCalendar = player.accCritterGuide = player.accDreamCatcher =
			player.accJarOfSouls = player.accStopwatch = player.accOreFinder = true;
		player.accWatch = 3;
		player.accDepthMeter = 1;
		player.accCompass = 1;
	}
}
