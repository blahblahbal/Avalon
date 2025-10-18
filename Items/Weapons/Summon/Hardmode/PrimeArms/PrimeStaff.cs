using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Avalon.Projectiles.Summon;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Summon.Hardmode.PrimeArms;
public class PrimeStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
		ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
		ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f; 
	}
	public override void SetDefaults()
	{
		Item.DefaultToMinionWeaponUpgradeable(50, 6.5f, 30, 14);
		Item.buffType = ModContent.BuffType<PrimeArms>();
		Item.shoot = ModContent.ProjectileType<PrimeArmsCounter>();
		Item.shootSpeed = 0f;
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 10);
		Item.UseSound = SoundID.Item44;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Bone, 50)
			.AddIngredient(ItemID.HallowedBar, 12)
			.AddIngredient(ItemID.SoulofFright, 20)
			.AddIngredient(ModContent.ItemType<Material.Shards.DemonicShard>(), 3)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
	public override bool CanUseItem(Player player)
	{
		return true;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		player.AddBuff(Item.buffType, 2);
		var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
		projectile.originalDamage = Item.damage;
		return false;
	}
}
public class PrimeArms : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[Type] = true;
		Main.buffNoSave[Type] = false;
	}
	public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
	{
		tip += "\n" + Language.GetTextValue("Mods.Avalon.TooltipEdits.UpgradeStage") + Main.LocalPlayer.ownedProjectileCounts[ModContent.ProjectileType<PrimeArmsCounter>()];
		base.ModifyBuffText(ref buffName, ref tip, ref rare);
	}
	public override void Update(Player player, ref int buffIndex)
	{
		if (player.ownedProjectileCounts[ModContent.ProjectileType<PrimeArmsCounter>()] > 0)
		{
			player.GetModPlayer<AvalonPlayer>().PrimeMinion = true;
		}
		if (!player.GetModPlayer<AvalonPlayer>().PrimeMinion)
		{
			player.DelBuff(buffIndex);
			buffIndex--;
		}
		else
		{
			player.buffTime[buffIndex] = 18000;
		}
		if (player.whoAmI == Main.myPlayer)
		{
			UpdatePrimeMinionStatus(player);
		}
	}
	private void UpdatePrimeMinionStatus(Player player)
	{
		if (player.ownedProjectileCounts[ModContent.ProjectileType<PrimeArmsCounter>()] < 1)
		{
			foreach (var projectile in Main.ActiveProjectiles)
			{
				if (projectile.owner == player.whoAmI)
				{
					if (projectile.type == ModContent.ProjectileType<PriminiCannon>() || projectile.type == ModContent.ProjectileType<PriminiLaser>() || projectile.type == ModContent.ProjectileType<PriminiSaw>() || projectile.type == ModContent.ProjectileType<PriminiVice>())
					{
						projectile.Kill();
					}
				}
			}
		}
		else if (player.ownedProjectileCounts[ModContent.ProjectileType<PriminiCannon>()] < 1)
		{
			IEntitySource source = player.GetSource_Misc("PrimeTierSwap");

			Vector2 cannonPos = player.Center + new Vector2(40f, -40f);
			Vector2 laserPos = player.Center + new Vector2(-40f);
			Vector2 sawPos = player.Center + new Vector2(-40f, 40f);
			Vector2 vicePos = player.Center + new Vector2(40f);

			Projectile p1 = Projectile.NewProjectileDirect(source, cannonPos, Vector2.Zero, ModContent.ProjectileType<PriminiCannon>(), 0, 0f, player.whoAmI);
			Projectile p2 = Projectile.NewProjectileDirect(source, laserPos, Vector2.Zero, ModContent.ProjectileType<PriminiLaser>(), 0, 0f, player.whoAmI);
			Projectile p3 = Projectile.NewProjectileDirect(source, sawPos, Vector2.Zero, ModContent.ProjectileType<PriminiSaw>(), 0, 0f, player.whoAmI);
			Projectile p4 = Projectile.NewProjectileDirect(source, vicePos, Vector2.Zero, ModContent.ProjectileType<PriminiVice>(), 0, 0f, player.whoAmI);
			p1.rotation = p1.Center.AngleTo(player.Center);
			p2.rotation = p2.Center.AngleTo(player.Center);
			p3.rotation = p3.Center.AngleTo(player.Center);
			p4.rotation = p4.Center.AngleTo(player.Center);
		}
	}
}
public class PrimeArmsCounter : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		Main.projPet[Projectile.type] = true;
	}
	public override void SetDefaults()
	{
		Projectile.netImportant = true;
		Projectile.width = 10;
		Projectile.height = 10;
		Projectile.penetrate = -1;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		Projectile.friendly = true;
		Projectile.minion = true;
		Projectile.minionSlots = 1f;
		Projectile.timeLeft = 60;
		Projectile.aiStyle = -1;
		Projectile.hide = true;
	}
	private static List<int> _blacklistedTargets = new List<int>();
	public override void AI()
	{
		Player owner = Main.player[Projectile.owner];
		AvalonGlobalProjectile.UpgradeableMinionCounterAI(Projectile, owner, ModContent.BuffType<PrimeArms>(), ref owner.GetModPlayer<AvalonPlayer>().PrimeMinion);
	}
	public static void ModifyPrimeMinionStats(Projectile projectile, Player owner)
	{
		int damageMod = 3;
		float origScale = 1f;
		float scaleMod = 0.1f;

		int ownedCounts = owner.ownedProjectileCounts[ModContent.ProjectileType<PrimeArmsCounter>()];

		projectile.originalDamage = owner.GetModPlayer<AvalonPlayer>().highestPrimeCounterOriginalDamage;
		projectile.originalDamage += Math.Max(0, ownedCounts - 1) * damageMod;
		projectile.originalDamage = (int)owner.GetTotalDamage(DamageClass.Summon).ApplyTo(projectile.originalDamage);

		projectile.knockBack = owner.GetModPlayer<AvalonPlayer>().primeMinionKnockback;

		projectile.scale = origScale;
		projectile.scale += scaleMod * ownedCounts;

		if (ownedCounts > 7)
		{
			projectile.frame = 2;
			projectile.scale = origScale;
			if (ownedCounts < 10)
			{
				projectile.scale += scaleMod * (ownedCounts - 7);
			}
			else
			{
				projectile.scale += scaleMod * 2;
			}
		}
		else if (ownedCounts > 4)
		{
			projectile.frame = 1;
			projectile.scale = origScale;
			projectile.scale += scaleMod * (ownedCounts - 4);
		}
	}
}
public class UpgradeableMinionHook : ModHook
{
	protected override void Apply()
	{
		On_Player.UpdateProjectileCaches += On_Player_UpdateProjectileCaches;
		On_Player.ResetProjectileCaches += On_Player_ResetProjectileCaches;
	}

	private void On_Player_UpdateProjectileCaches(On_Player.orig_UpdateProjectileCaches orig, Player self, int i)
	{
		orig(self, i);
		foreach (var proj in Main.ActiveProjectiles)
		{
			if (proj.owner != i)
			{
				continue;
			}
			if (proj.type == ModContent.ProjectileType<PrimeArmsCounter>())
			{
				int originalDamage = proj.originalDamage;
				if (self.GetModPlayer<AvalonPlayer>().highestPrimeCounterOriginalDamage < originalDamage)
				{
					self.GetModPlayer<AvalonPlayer>().highestPrimeCounterOriginalDamage = originalDamage;
				}
				self.GetModPlayer<AvalonPlayer>().primeMinionKnockback = proj.knockBack;
				break;
			}
		}
	}
	private void On_Player_ResetProjectileCaches(On_Player.orig_ResetProjectileCaches orig, Player self)
	{
		self.GetModPlayer<AvalonPlayer>().highestPrimeCounterOriginalDamage = 0;
		self.GetModPlayer<AvalonPlayer>().primeMinionKnockback = 0f;
		orig(self);
	}
}

