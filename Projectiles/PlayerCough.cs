using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles
{
	public class PlayerCough : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.NoLiquidDistortion[Type] = true;
		}
		public override void SetDefaults()
		{
			Projectile.width = 24;
			Projectile.height = 24;
			Projectile.damage = 0;
			Projectile.friendly = true;
			Projectile.timeLeft = 900;
			Projectile.tileCollide = false;
			Projectile.scale = 0.75f;
			Projectile.alpha = 128;
		}
		public override bool PreDraw(ref Color lightColor)
		{
			ClassExtensions.DrawGas(TextureAssets.Projectile[Type].Value, lightColor * 0.8f, Projectile, 4, 6);
			return false;
		}
		public override void AI()
		{
			Projectile.ai[1]++;
			if (Projectile.ai[2] > 1)
			{
				Projectile.alpha += 1;
				if (Projectile.ai[1] % 10 == 0)
				{
					Projectile.damage--;
				}
			}
			else
				Projectile.alpha -= 3;

			if (Projectile.alpha <= 100)
				Projectile.ai[2]++;

			if (Projectile.alpha == 255) Projectile.Kill();

			Projectile.velocity = Projectile.velocity.RotatedByRandom(0.1f) * 0.985f;
			Projectile.rotation += MathHelper.Clamp(Projectile.velocity.Length() * 0.03f, -0.6f, 0.6f);
			Projectile.scale += 0.01f;
			Projectile.Resize((int)(32 * Projectile.scale), (int)(32 * Projectile.scale));

			bool[] decreaseDebuffTime = new bool[Main.player[Projectile.owner].buffType.Length];
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (!Main.npc[i].townNPC)
				{
					if (Main.npc[i].getRect().Intersects(Projectile.getRect()))
					{
						for (int b = 0; b < Main.player[Projectile.owner].buffType.Length; b++)
						{
							if (Main.debuff[Main.player[Projectile.owner].buffType[b]] && !BuffID.Sets.NurseCannotRemoveDebuff[Main.player[Projectile.owner].buffType[b]] && Main.player[Projectile.owner].buffType[b] != BuffID.Tipsy)
							{
								Main.npc[i].AddBuff(Main.player[Projectile.owner].buffType[b], Main.player[Projectile.owner].buffTime[b] > 3600 ? 3600 : Main.player[Projectile.owner].buffTime[b]);
								decreaseDebuffTime[b] = true;
							}
						}
					}
				}
			}
			if (Projectile.ai[2] == 0)
			{
				for (int j = 0; j < decreaseDebuffTime.Length; j++)
				{
					if (decreaseDebuffTime[j])
					{
						Main.player[Projectile.owner].buffTime[j] = (int)(Main.player[Projectile.owner].buffTime[j] * 0.8f);
						decreaseDebuffTime[j] = false;
						continue;
					}
				}
				Projectile.ai[2]++;
			}
		}
		public override bool? CanCutTiles()
		{
			return false;
		}
	}
}
