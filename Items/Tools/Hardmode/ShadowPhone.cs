using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Items.Accessories.Info;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;
public abstract class ShadowPhoneBase : ModItem
{
	public virtual int PhoneToTurnInto => -1;
	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[Type] = 31;
		ItemID.Sets.ShimmerCountsAsItem[Type] = ModContent.ItemType<ShadowPhoneDummy>();
		ItemID.Sets.DuplicationMenuToolsFilter[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToConsumable(false, useTurn: true, width: 24, height: 28);
		Item.maxStack = 1;
		Item.rare = ItemRarityID.Red;
		Item.value = Item.sellPrice(0, 10);
	}
	public override bool AltFunctionUse(Player player)
	{
		if (player.itemTime == 0 && player.itemAnimation == 0)
		{
			player.releaseUseTile = false;
			Main.mouseRightRelease = false;
			SoundEngine.PlaySound(SoundID.Unlock, player.position);
			Item.ChangeItemType(PhoneToTurnInto);
			Recipe.FindRecipes();
		}
		return true;
	}
	public sealed override bool? UseItem(Player player)
	{
		TeleportAction(player);
		SoundEngine.PlaySound(SoundID.Item6, player.position);
		return true;
	}
	public virtual Action<Player> TeleportAction => player => { };
	public override void UpdateInfoAccessory(Player player)
	{
		player.GetModPlayer<EyeoftheGodsPlayer>().DamageDisplay = true;
		player.GetModPlayer<EyeoftheGodsPlayer>().DefenseDisplay = true;
		player.GetModPlayer<CalcSpecPlayer>().CalcSpecDisplay = true;
		player.accThirdEye = true;
		player.accFishFinder = true;
		player.accWeatherRadio = true;
		player.accCalendar = true;
		player.accCritterGuide = true;
		player.accDreamCatcher = true;
		player.accJarOfSouls = true;
		player.accStopwatch = true;
		player.accOreFinder = true;
		player.accWatch = 3;
		player.accDepthMeter = 1;
		player.accCompass = 1;
	}
	#region Teleport methods
	public static void TeleportToSurface(Player p)
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
	public static void DungeonPort(Player player)
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
	public static void JungleTropicsPort(Player player)
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
	#endregion Teleport methods
}

// The actual right click logic is in AltFunctionUse for convenience of not having to replicate a lengthy flag in the vanilla ItemCheck_ManageRightClickFeatures method since AltFunctionUse is called inside.
// This detour is just to reset some values which cause undesirable behaviour (by default, returning true in AltFunctionUse makes the item call UseItem when right clicked, this prevents that so the item doesn't get held out).
public class ShadowPhoneHook : ModHook
{
	protected override void Apply()
	{
		On_Player.ItemCheck_ManageRightClickFeatures += On_Player_ItemCheck_ManageRightClickFeatures;
	}

	private void On_Player_ItemCheck_ManageRightClickFeatures(On_Player.orig_ItemCheck_ManageRightClickFeatures orig, Player self)
	{
		orig.Invoke(self);
		if (ItemLoader.GetItem(self.inventory[self.selectedItem].type) is ShadowPhoneBase)
		{
			self.altFunctionUse = 0;
			self.controlUseItem = false;
		}
	}
}

public class ShadowPhoneGlobalItem : GlobalItem
{
	// Turns the dummy shadow phone item (the one with a blank screen) into the shadow phone (home) item when created, same method as the shellphone uses.
	// Done in GlobalItem as doing it in the ModItem fails to build for whatever reason.
	public override void OnCreated(Item item, ItemCreationContext context)
	{
		if (item.type == ModContent.ItemType<ShadowPhoneDummy>())
		{
			item.SetDefaults(ModContent.ItemType<ShadowPhoneHome>());
		}
	}

	// Allow right clicking the item slot in the inventory to change it
	public override bool CanRightClick(Item item)
	{
		if (Main.mouseRightRelease && Main.mouseRight)
		{
			if (item.type == ModContent.ItemType<ShadowPhoneDummy>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				item.ChangeItemType(ModContent.ItemType<ShadowPhoneHome>());
				return false;
			}
			if (item.type == ModContent.ItemType<ShadowPhoneHome>())
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
				item.ChangeItemType(ModContent.ItemType<ShadowPhoneSpawn>());
				return false;
			}
			if (item.type == ModContent.ItemType<ShadowPhoneSpawn>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				item.ChangeItemType(ModContent.ItemType<ShadowPhoneSurface>());
				return false;
			}
			if (item.type == ModContent.ItemType<ShadowPhoneSurface>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				item.ChangeItemType(ModContent.ItemType<ShadowPhoneJungleTropics>());
				return false;
			}
			if (item.type == ModContent.ItemType<ShadowPhoneJungleTropics>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				item.ChangeItemType(ModContent.ItemType<ShadowPhoneDungeon>());
				return false;
			}
			if (item.type == ModContent.ItemType<ShadowPhoneDungeon>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				item.ChangeItemType(ModContent.ItemType<ShadowPhoneRandom>());
				return false;
			}
			if (item.type == ModContent.ItemType<ShadowPhoneRandom>())
			{
				SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
				item.ChangeItemType(ModContent.ItemType<ShadowPhoneHome>());
				return false;
			}
		}
		return base.CanRightClick(item);
	}
}

public class ShadowPhoneDummy : ShadowPhoneBase
{
	public override int PhoneToTurnInto => ModContent.ItemType<ShadowPhoneHome>();
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddRecipeGroup("Shellphone")
			.AddIngredient(ModContent.ItemType<EyeoftheGods>())
			.AddIngredient(ModContent.ItemType<CalculatorSpectacles>())
			.AddIngredient(ItemID.FallenStar, 40)
			.AddIngredient(ItemID.Diamond, 20)
			.AddIngredient(ItemID.ChlorophyteBar, 7)
			.AddIngredient(ItemID.Ectoplasm, 10)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}

public class ShadowPhoneHome : ShadowPhoneBase
{
	public override int PhoneToTurnInto => ModContent.ItemType<ShadowPhoneOcean>();
	public override Action<Player> TeleportAction => player => player.Spawn(PlayerSpawnContext.RecallFromItem);
}

public class ShadowPhoneOcean : ShadowPhoneBase
{
	public override int PhoneToTurnInto => ModContent.ItemType<ShadowPhoneHell>();
	public override Action<Player> TeleportAction => player => player.MagicConch();
}

public class ShadowPhoneHell : ShadowPhoneBase
{
	public override int PhoneToTurnInto => ModContent.ItemType<ShadowPhoneSpawn>();
	public override Action<Player> TeleportAction => player => player.DemonConch();
}

[LegacyName("ShadowPhone")]
public class ShadowPhoneSpawn : ShadowPhoneBase
{
	public override int PhoneToTurnInto => ModContent.ItemType<ShadowPhoneSurface>();
	public override Action<Player> TeleportAction => player => player.Shellphone_Spawn();
}

public class ShadowPhoneSurface : ShadowPhoneBase
{
	public override int PhoneToTurnInto => ModContent.ItemType<ShadowPhoneJungleTropics>();
	public override Action<Player> TeleportAction => TeleportToSurface;
}

public class ShadowPhoneJungleTropics : ShadowPhoneBase
{
	public override int PhoneToTurnInto => ModContent.ItemType<ShadowPhoneDungeon>();
	public override Action<Player> TeleportAction => JungleTropicsPort;
}

public class ShadowPhoneDungeon : ShadowPhoneBase
{
	public override int PhoneToTurnInto => ModContent.ItemType<ShadowPhoneRandom>();
	public override Action<Player> TeleportAction => DungeonPort;
}

public class ShadowPhoneRandom : ShadowPhoneBase
{
	public override int PhoneToTurnInto => ModContent.ItemType<ShadowPhoneHome>();
	public override Action<Player> TeleportAction => player => player.TeleportationPotion();
}
