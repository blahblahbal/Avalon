using Avalon.Items.Weapons.Melee.Hardmode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee;

public class SoulEdgeDash : ModProjectile
{
	private const int initialTimeLeft = 20;
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(64);
		Projectile.DamageType = DamageClass.Melee;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.tileCollide = false;
		Projectile.timeLeft = initialTimeLeft;
		Projectile.penetrate = -1;
	}
	public override void AI()
	{
		Player player = Main.player[Projectile.owner];
		if (Projectile.timeLeft == initialTimeLeft)
		{
			player.immune = true;
			player.AddImmuneTime(ImmunityCooldownID.General, 60);
			player.GetModPlayer<SoulEdgePlayer>().SoulEdgeDamage = 0;
		}
		player.velocity = Projectile.velocity * new Vector2(4,3);
		player.heldProj = Projectile.whoAmI;
		player.SetDummyItemTime(2);

		Projectile.velocity *= 0.99f;

		if(Projectile.timeLeft == 1)
		{
			Projectile.velocity *= 0.3f;
			player.velocity *= 0.3f;
		}

		if(Main.myPlayer == Projectile.owner && Projectile.timeLeft % 5 == 0)
		{
			for(int i = 0; i < 2; i++)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, (Projectile.velocity * 0.3f).RotatedBy(MathHelper.PiOver2 + (MathHelper.Pi * i)),ModContent.ProjectileType<SoulEaterFriendly>(),Projectile.damage / 9, Projectile.knockBack / 9, Projectile.owner);
			}
		}

		Projectile.Center = player.Center + new Vector2(0,player.gfxOffY) + Vector2.Normalize(Projectile.velocity) * 10;
		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, new Vector2(0, TextureAssets.Projectile[Type].Height()),Projectile.scale,SpriteEffects.None,0);
		for(int i = 0; i < 2; i++)
		{
			Main.EntitySpriteDraw(TextureAssets.Extra[ExtrasID.SharpTears].Value, Projectile.Center - Main.screenPosition + Vector2.Normalize(Projectile.velocity) * 110, null, new Color(1f,0.8f,0.8f,0f) * 0.6f, MathHelper.PiOver2 * i + Projectile.rotation + MathHelper.PiOver4, TextureAssets.Extra[ExtrasID.SharpTears].Size() / 2, new Vector2(Projectile.scale, Projectile.scale + 1 - i), SpriteEffects.None, 0);
		}
		
		return false;
	}
}
