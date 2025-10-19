using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Items.Material;
using Avalon.Items.Weapons.Magic.PreHardmode.GlassEye;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode.BloodstainedEye;

public class BloodstainedEye : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSpellBook(ModContent.ProjectileType<BloodyTear>(), 32, 2f, 3, 14f, 30, 30, 1.1f, width: 16, height: 16);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 60);
		Item.UseSound = SoundID.NPCHit1;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		int numberProjectiles = Main.rand.NextBool(3) ? 2 : 1;
		for (int i = 0; i < numberProjectiles; i++)
		{
			Vector2 perturbedSpeed = AvalonUtils.GetShootSpread(velocity, position, Type, MathHelper.ToRadians(8), Main.rand.NextFloat(-4.2f, 0f), random: true);
			Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
		}
		return false;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<GlassEye.GlassEye>()).AddIngredient(ModContent.ItemType<BloodshotLens>()).AddIngredient(ModContent.ItemType<BottledLava>()).AddTile(TileID.Anvils).Register();
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(5, 0);
	}
}
public class BloodyTear : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.penetrate = 1;
		Projectile.width = 12;
		Projectile.height = 12;
		Projectile.aiStyle = 1;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.timeLeft = 70;
		Projectile.scale = 1.4f;
		Projectile.alpha = -100;
	}
	public override void AI()
	{
		Lighting.AddLight(Projectile.position, 30 / 255f, 20 / 255f, 0);
		Projectile.spriteDirection = Projectile.direction;
		Projectile.scale *= 0.99f;
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] == 4f)
		{
			for (int num257 = 0; num257 < 10; num257++)
			{
				int newDust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), 8, 8, DustID.Blood, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, 100, default(Color), 1f);
				Main.dust[newDust].position = (Main.dust[newDust].position + Projectile.Center) / 2f;
				Main.dust[newDust].noGravity = true;
				Main.dust[newDust].velocity *= 0.5f;
			}
		}
		if (Main.rand.NextBool(6))
		{
			Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), 8, 8, DustID.Blood, Projectile.oldVelocity.X * 0.1f, Projectile.oldVelocity.Y * 0.1f, 100, default(Color), 1f);
		}
	}
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.NPCDeath9, Projectile.position);
		for (int num237 = 0; num237 < 10; num237++)
		{
			int num239 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), 8, 8, DustID.Blood, Projectile.oldVelocity.X * 0.2f, Projectile.oldVelocity.Y * 0.2f, 100, default(Color), 1.5f);
			Dust dust30 = Main.dust[num239];
			dust30.noGravity = false;
			dust30.scale = 1f;
		}
	}
}
