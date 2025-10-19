using Avalon;
using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode.ToxinStaff;

public class ToxinStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Item.type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToStaff(ModContent.ProjectileType<ToxicFang>(), 46, 0, 26, 14f, 20, 20, true);
		Item.reuseDelay = 14;
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 10, 10);
		Item.UseSound = SoundID.Item43;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.PoisonStaff)
			.AddIngredient(ModContent.ItemType<Material.Bars.XanthophyteBar>(), 14)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		position = player.Center + new Vector2(65, 0).RotatedBy(player.AngleTo(Main.MouseWorld));
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		int num157 = 4;
		if (Main.rand.NextBool(3))
		{
			num157++;
		}
		if (Main.rand.NextBool(4))
		{
			num157++;
		}
		if (Main.rand.NextBool(5))
		{
			num157++;
		}
		for (int spread = 0; spread < num157; spread++)
		{
			float xVel = velocity.X;
			float yVel = velocity.Y;
			float num161 = 0.04f * spread;
			xVel += Main.rand.Next(-35, 36) * num161;
			yVel += Main.rand.Next(-35, 36) * num161;
			int dmg = Item.damage;
			Projectile.NewProjectile(source, position.X, position.Y, xVel, yVel, type, (int)player.GetDamage(DamageClass.Magic).ApplyTo(dmg), knockback, player.whoAmI, 0f, 0f);
		}
		return false;
	}
}
public class ToxicFang : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = Projectile.height = 16;
		Projectile.aiStyle = -1;
		Projectile.tileCollide = true;
		Projectile.friendly = true;
		Projectile.timeLeft = 90;
		Projectile.alpha = 100;
		Projectile.penetrate = 4;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.ignoreWater = true;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 100);
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Toxic>(), 60 * 4);
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		return true;
	}
	public override void AI()
	{
		Lighting.AddLight(Projectile.position, 150 / 255f, 0, 100 / 255f);
		Projectile.rotation = Projectile.velocity.ToRotation() + 90 / 57.2957795f;
		for (int num26 = 0; num26 < 2; num26++)
		{
			float x2 = Projectile.position.X - Projectile.velocity.X / 10f * num26;
			float y2 = Projectile.position.Y - Projectile.velocity.Y / 10f * num26;
			int num27 = Dust.NewDust(new Vector2(x2, y2), Projectile.width, Projectile.height, ModContent.DustType<Dusts.ToxinDust>(), 0f, 0f, 0, default, 1f);
			Main.dust[num27].alpha = Projectile.alpha;
			Main.dust[num27].velocity *= 0f;
			Main.dust[num27].noGravity = true;
		}
		if (Projectile.ai[1] >= 20f)
		{
			Projectile.velocity.Y += 0.2f;
		}
		if (Projectile.velocity.Y > 16f)
		{
			Projectile.velocity.Y = 16f;
			return;
		}
	}
}
