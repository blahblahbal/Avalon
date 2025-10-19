using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode.GlassEye;

public class GlassEye : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSpellBook(ModContent.ProjectileType<Tear>(), 14, 2f, 3, 12f, 35, 35, 1.1f, width: 16, height: 16);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 12, 0);
		Item.UseSound = SoundID.NPCHit1;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ItemID.Lens, 1).AddIngredient(ItemID.FallenStar, 2).AddIngredient(ItemID.BottledWater, 1).AddTile(TileID.WorkBenches).Register();
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(5, 0);
	}
}
public class Tear : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.penetrate = 1;
		Projectile.width = 12;
		Projectile.height = 12;
		Projectile.aiStyle = 1;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.timeLeft = 50;
		Projectile.scale = 1.4f;
		Projectile.alpha = -100;
	}
	public override void AI()
	{
		Lighting.AddLight(Projectile.position, 0, 20 / 255f, 25 / 255f);
		Projectile.spriteDirection = Projectile.direction;
		Projectile.scale *= 0.985f;
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] == 4f)
		{
			for (int num257 = 0; num257 < 10; num257++)
			{
				int newDust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), 8, 8, DustID.Wet, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, 100, default(Color), 1f);
				Main.dust[newDust].position = (Main.dust[newDust].position + Projectile.Center) / 2f;
				Main.dust[newDust].noGravity = true;
				Main.dust[newDust].velocity *= 0.5f;
			}
		}
		if (Main.rand.NextBool(6))
		{
			Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), 8, 8, DustID.Wet, Projectile.oldVelocity.X * 0.1f, Projectile.oldVelocity.Y * 0.1f, 100, default(Color), 1f);
		}
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.Wet, 120);
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(BuffID.Wet, 120);
	}
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.NPCDeath9, Projectile.position);
		for (int num237 = 0; num237 < 10; num237++)
		{
			int num239 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), 8, 8, DustID.Wet, Projectile.oldVelocity.X * 0.2f, Projectile.oldVelocity.Y * 0.2f, 100, default(Color), 1f);
			Dust dust30 = Main.dust[num239];
			dust30.noGravity = false;
			dust30.scale = 1f;
		}
	}
}