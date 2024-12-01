using Avalon.Common;
using Avalon.Compatability.Thorium.Projectiles.Hostile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Projectiles.Enemy;

namespace Avalon.Compatability.Thorium.Common
{
	public class ThoriumRespritesThatAreNotSimpleGraphicReplacementsProjectile : GlobalProjectile
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ModLoader.HasMod("ThoriumMod") && ModContent.GetInstance<AvalonConfig>().VanillaTextureReplacement;
		}
		private static Asset<Texture2D> cursedSickle;
		private static Asset<Texture2D> ichorSickle;
		private static Asset<Texture2D> pathogenSickle;
		int? cursedSickleProj => ExxoAvalonOrigins.Thorium != null ? ExxoAvalonOrigins.Thorium.Find<ModProjectile>("CursedSicklePro").Type : null;
		int? ichorSickleProj => ExxoAvalonOrigins.Thorium != null ? ExxoAvalonOrigins.Thorium.Find<ModProjectile>("IchorSicklePro").Type : null;
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			cursedSickle = ModContent.Request<Texture2D>("Avalon/Compatability/Thorium/Assets/Projectiles/CursedSickle");
			ichorSickle = ModContent.Request<Texture2D>("Avalon/Compatability/Thorium/Assets/Projectiles/IchorSickle");
			pathogenSickle = ModContent.Request<Texture2D>("Avalon/Compatability/Thorium/Assets/Projectiles/PathogenSickle");
			ProjectileID.Sets.TrailCacheLength[(int)cursedSickleProj] = 6;
			ProjectileID.Sets.TrailingMode[(int)cursedSickleProj] = 2;
			ProjectileID.Sets.TrailCacheLength[(int)ichorSickleProj] = 6;
			ProjectileID.Sets.TrailingMode[(int)ichorSickleProj] = 2;
		}
		public override bool PreDraw(Projectile projectile, ref Color lightColor)
		{
			if (projectile.type == cursedSickleProj || projectile.type == ichorSickleProj || projectile.type == ModContent.ProjectileType<PathogenSickle>())
			{
				float rotationMultiplier = 0.7f;
				var texture = cursedSickle;
				if (projectile.type == cursedSickleProj)
				{
					texture = cursedSickle;
				}
				else if (projectile.type == ichorSickleProj)
				{
					texture = ichorSickle;
				}
				else if (projectile.type == ModContent.ProjectileType<PathogenSickle>())
				{
					texture = pathogenSickle;
				}
				int frameHeight = texture.Value.Height;
				Rectangle frame = new Rectangle(0, frameHeight * projectile.frame, texture.Value.Width, frameHeight);
				int length = ProjectileID.Sets.TrailCacheLength[projectile.type];
				for (int i = 0; i < length; i++)
				{
					//float multiply = ((float)(length - i) / length) * projectile.Opacity * 0.2f;
					float multiply = (float)(length - i) / length * 0.5f;
					Main.EntitySpriteDraw(texture.Value, projectile.oldPos[i] - Main.screenPosition + (projectile.Size / 2f), frame, new Color(128, 128, 255, 128) * multiply, projectile.oldRot[i] * rotationMultiplier, new Vector2(texture.Value.Width, frameHeight) / 2, projectile.scale, SpriteEffects.None, 0);
				}

				Main.EntitySpriteDraw(texture.Value, projectile.position - Main.screenPosition + (projectile.Size / 2f), frame, new Color(255, 255, 255, 175) * 0.7f, projectile.rotation * rotationMultiplier, new Vector2(texture.Value.Width, frameHeight) / 2, projectile.scale, SpriteEffects.None, 0);
				return false;
			}
			return base.PreDraw(projectile, ref lightColor);
		}
	}
}
