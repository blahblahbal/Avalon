using Avalon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode.CaesiumScimitar;

public class CaesiumExplosion : ModProjectile
{
	private static Asset<Texture2D>? texture2;
	public override void SetStaticDefaults()
	{
		texture2 = ModContent.Request<Texture2D>("Avalon/Assets/Textures/WhiteExplosion");
	}
	public override void SetDefaults()
	{
		Projectile.width = 70;
		Projectile.height = 70;
		Projectile.friendly = true;
		Projectile.aiStyle = 0;
		Projectile.penetrate = -1;
		Projectile.extraUpdates = 1;
		Projectile.knockBack = 0;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = false;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = -1;
	}
	public override void AI()
	{
		if (Projectile.ai[0] == 0)
		{
			for (int i = 0; i < 10; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, Main.rand.NextVector2Circular(6, 6));
				d.customData = 0;
				d.fadeIn = Main.rand.NextFloat(1.2f);
			}
			Projectile.rotation = Main.rand.NextFloat(0, MathHelper.TwoPi);
			Projectile.scale = Main.rand.NextFloat(0.7f, 1.1f);
		}
		Projectile.scale += 0.01f;
		Projectile.Resize((int)(70 * Projectile.scale), (int)(70 * Projectile.scale));
		Projectile.velocity *= 0.95f;
		Projectile.ai[0]++;

		Projectile.frameCounter++;
		if (Projectile.frameCounter % 5 == 0)
		{
			Projectile.frame++;
		}
		if (Projectile.frame > 7)
		{
			Projectile.Kill();
		}
		Lighting.AddLight(Projectile.position, new Vector3(0, MathHelper.Lerp(1f, 0f, Projectile.ai[0] / (3 * 7)), 0));
	}
	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.HitDirectionOverride = Projectile.Center.X <= Main.player[Projectile.owner].Center.X ? -1 : 1;
	}
	public override void OnSpawn(IEntitySource source)
	{
		Projectile.rotation = Main.rand.NextFloat(MathHelper.Pi, -MathHelper.Pi);
	}
	public override bool PreDraw(ref Color lightColor)
	{
		int frameHeight = TextureAssets.Projectile[Type].Value.Height / 7;
		Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, TextureAssets.Projectile[Type].Value.Width, frameHeight);
		Vector2 frameOrigin = new Vector2(TextureAssets.Projectile[Type].Value.Width) / 2;
		Vector2 DrawPos = Projectile.Center - Main.screenPosition;

		float muliply = (12 - Projectile.ai[0]) / 28;
		// Pretend i spelled that correctly :)
		for (int i = 0; i < 8; i++)
		{
			Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, DrawPos + new Vector2(0, Projectile.ai[0] * 0.05f).RotatedBy(i * MathHelper.PiOver4), frame, Color.Gray * 0.3f, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);
		}
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, DrawPos, frame, new Color(255, 255, 255, 200), Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);

		for (int i = 0; i < 8; i++)
		{
			Main.EntitySpriteDraw(texture2.Value, DrawPos + new Vector2(0, Projectile.ai[0] * 0.05f).RotatedBy(i * MathHelper.PiOver4), frame, new Color(255, 255, 255, 0) * muliply, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);
		}

		return false;
	}
}
