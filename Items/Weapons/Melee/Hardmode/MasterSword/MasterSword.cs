using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode.MasterSword;

public class MasterSword : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<MasterSwordBeam>(), 90, 5f, 9.5f, 54, 18, width: 54, height: 54);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 9, 63);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (player.statLife >= player.statLifeMax2 * 0.80f)
		{
			SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/MasterSword"), player.position);
			Projectile.NewProjectile(source, position, velocity, type, (int)(damage * 1.3f), knockback, player.whoAmI);
			return false;
		}
		return false;
	}
}
public class MasterSwordBeam : ModProjectile
{
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Projectile.width = 20;
		Projectile.height = 20;
		Projectile.aiStyle = -1;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.penetrate = 1;
		Projectile.alpha = 0;
		Projectile.friendly = true;
		DrawOffsetX = -(int)((dims.Width / 2) - (Projectile.Size.X / 2));
		DrawOriginOffsetY = -(int)((dims.Width / 2) - (Projectile.Size.Y / 2));
	}
	public override Color? GetAlpha(Color lightColor)
	{
		if (this.Projectile.localAI[1] >= 15f)
		{
			return new Color(255, 255, 255, this.Projectile.alpha);
		}
		if (this.Projectile.localAI[1] < 5f)
		{
			return Color.Transparent;
		}
		int num7 = (int)((this.Projectile.localAI[1] - 5f) / 10f * 255f);
		return new Color(num7, num7, num7, num7);
	}
	public override void AI()
	{
		if (Projectile.localAI[1] < 15f)
		{
			Projectile.localAI[1] += 1f;
		}
		else
		{
			if (Projectile.localAI[0] == 0f)
			{
				Projectile.scale -= 0.02f;
				Projectile.alpha += 30;
				if (Projectile.alpha >= 250)
				{
					Projectile.alpha = 255;
					Projectile.localAI[0] = 1f;
				}
			}
			else if (Projectile.localAI[0] == 1f)
			{
				Projectile.scale += 0.02f;
				Projectile.alpha -= 30;
				if (Projectile.alpha <= 0)
				{
					Projectile.alpha = 0;
					Projectile.localAI[0] = 0f;
				}
			}
		}

		Projectile.rotation += Projectile.direction * 0.4f;
		if (Projectile.direction == 1)
		{
			Projectile.spriteDirection = 1;
		}
		else
		{
			Projectile.spriteDirection = -1;
		}
		Lighting.AddLight(Projectile.Center, (63 / 255f) / 3f, (214 / 255f) / 3f, 1 / 3f);
	}
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		for (int dustAmount = 4; dustAmount < 13; dustAmount++)
		{
			float velX = Projectile.oldVelocity.X / (float)dustAmount;
			float velY = Projectile.oldVelocity.Y / (float)dustAmount;
			int dustType = DustID.Vortex;
			int dust = Dust.NewDust(new Vector2(Projectile.position.X - velX, Projectile.position.Y - velY), 8, 8, dustType, Projectile.oldVelocity.X * 0.2f, Projectile.oldVelocity.Y * 0.2f, 100, default(Color), 1.8f);
			Main.dust[dust].velocity *= 1.5f;
			Main.dust[dust].noGravity = true;
		}
	}
}