using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic;

public class LotusLeechHeal : ModProjectile
{
    private Color color;
    private int dustId;

    public override void SetDefaults()
    {
        Projectile.Size = new Vector2(6);
        Projectile.alpha = 255;
        Projectile.friendly = true;
        Projectile.aiStyle = -1;
        Projectile.timeLeft = 60 * 2;
        Projectile.tileCollide = false;
        //Projectile.tileCollide = false;
    }

    public override void AI()
    {
        if (Projectile.ai[1] == 0f)
		{
			Projectile.oldPosition = Projectile.position;
            Projectile.ai[1] = 1f;
            SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
			for (float j = 0; j < MathHelper.TwoPi; j += MathHelper.TwoPi / 32f)
			{
				var dust = Dust.NewDustPerfect(Projectile.Center, DustID.PurificationPowder, new Vector2(0, 3).RotatedBy(j));
				dust.noGravity = true;
			}
        }

        if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
        {
            Projectile.tileCollide = true;
        }

        for (float i = 0; i < 1; i += 0.2f)
        {
            var dust = Dust.NewDustPerfect(Vector2.Lerp(Projectile.Center, Projectile.oldPosition + (Projectile.Size / 2), i), DustID.PurificationPowder, Vector2.Zero);
            dust.noGravity = true;
            dust.scale = 1;
            dust.frame.Y = 0;
        }

        Player player = Main.player[Projectile.owner];
        Projectile.velocity += Projectile.Center.DirectionTo(player.Center);
        Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.Center.DirectionTo(player.Center) * Projectile.velocity.Length(), 0.04f);

        if(Projectile.velocity.Length() > 10)
        {
            Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 10;
        }

        if (Projectile.Hitbox.Intersects(player.Hitbox))
        {
            player.statMana += 7;
            CombatText.NewText(player.Hitbox, CombatText.HealMana, 7);
            Projectile.Kill();

            SoundEngine.PlaySound(SoundID.MaxMana, Projectile.position);

            //for (float i = 0; i < MathHelper.TwoPi; i += MathHelper.TwoPi / 32f)
            //{
            //    var dust = Dust.NewDustPerfect(Projectile.Center, DustID.PurificationPowder, new Vector2(0, 3).RotatedBy(i));
            //    dust.noGravity = true;
            //}
        }

        if(!player.active || player.dead)
        {
            Projectile.Kill();
        }

        Lighting.AddLight(new Vector2((int)((Projectile.position.X + Projectile.width / 2) / 16f), (int)((Projectile.position.Y + Projectile.height / 2) / 16f)), color.ToVector3());
    }

    public override void OnKill(int timeLeft)
    {
    }
}
