using Avalon;
using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Avalon.Items.Material.Shards;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.ObsidianGlaive;

public class ObsidianGlaive : ModItem // Obisidian Glaive // what did the commentator mean by this // idk // damn
{
	int ShootTimes;
	public override void SetStaticDefaults()
	{
		ItemID.Sets.Spears[Item.type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToSpear(ModContent.ProjectileType<ObsidianGlaiveProj>(), 23, 4.5f, 25, 4f, true);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 10);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		ShootTimes++;
		if (ShootTimes % 2 == 0)
		{
			float RotationAmount = Main.rand.NextFloat(0.1f, 0.4f);
			Projectile.NewProjectile(source, position, velocity.RotatedBy(RotationAmount * -player.direction), type, damage, knockback, player.whoAmI, 0, 0, RotationAmount * 2);
		}
		else
		{
			float RotationAmount = Main.rand.NextFloat(0.1f, 0.4f);
			Projectile.NewProjectile(source, position, velocity.RotatedBy(RotationAmount * -player.direction), type, damage, knockback, player.whoAmI, 0, 0, RotationAmount * 2);
		}
		return false;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Obsidian, 30)
			.AddIngredient(ItemID.Fireblossom, 3)
			.AddIngredient(ModContent.ItemType<FireShard>())
			.AddTile(TileID.Anvils)
			.Register();
	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 1;
	}
}
public class ObsidianGlaiveProj : SpearTemplate2
{
	public override LocalizedText DisplayName => ModContent.GetInstance<ObsidianGlaive>().DisplayName;
	protected override float HoldoutRangeMax => 170;
	protected override float HoldoutRangeMin => 40;
	public override void SetDefaults()
	{
		base.SetDefaults();
	}
	public override bool PreDraw(ref Color lightColor)
	{
		return base.PreDraw(ref lightColor);
	}

	public override void PostAI()
	{
		Player player = Main.player[Projectile.owner];
		Projectile.position.Y += 5 * player.gravDir;
		Projectile.position.X += player.direction * 3;
		float duration = player.itemAnimationMax;

		Projectile.velocity = Projectile.velocity.RotatedBy(player.direction * (1 / duration) * Projectile.ai[2]);

		Dust d2 = Dust.NewDustDirect(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Torch, 0, 0);
		d2.customData = 0;
		d2.noGravity = true;
		d2.scale += 0.7f;
		d2.velocity += Projectile.velocity * 4;
		d2.velocity.Y *= 0.8f;
		if (Main.rand.NextBool(4))
		{
			Dust d = Dust.NewDustDirect(Projectile.position - Projectile.velocity * 3, Projectile.width, Projectile.height, DustID.Torch, 0, 0);
			d.customData = 0;
			d.fadeIn = 0.7f;
			d.velocity += Projectile.velocity * 4;
			d.velocity.Y *= 0.8f;
		}
		if (Main.rand.NextBool(5))
		{
			Dust d = Dust.NewDustDirect(Projectile.position - Projectile.velocity * 3, Projectile.width, Projectile.height, DustID.Obsidian, 0, 0, 128);
			d.customData = 0;
			d.noGravity = true;
			d.fadeIn = 1f;
			d.velocity += Projectile.velocity * 6;
			d.velocity.Y *= 0.5f;
		}
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (Main.rand.NextBool(3))
		{
			target.AddBuff(BuffID.OnFire3, 160);
		}
	}
}
