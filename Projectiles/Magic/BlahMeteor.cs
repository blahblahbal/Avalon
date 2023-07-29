using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Magic;

public class BlahMeteor : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 40;
        Projectile.height = 40;
        Projectile.aiStyle = -1;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.penetrate = 9;
        Projectile.friendly = true;
        Projectile.scale = 0.5f;
        Projectile.damage = 100;
    }
    public override bool PreAI()
    {
        Lighting.AddLight(Projectile.position, 249 / 255, 201 / 255, 77 / 255);
        return true;
    }
    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        Projectile.position.X = Projectile.position.X + Projectile.width / 2;
        Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
        Projectile.width = 22;
        Projectile.height = 22;
        Projectile.position.X = Projectile.position.X - Projectile.width / 2;
        Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
        for (int num341 = 0; num341 < 30; num341++)
        {
            int num342 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
            Main.dust[num342].velocity *= 1.4f;
        }
        for (int num343 = 0; num343 < 20; num343++)
        {
            int num344 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 3.5f);
            Main.dust[num344].noGravity = true;
            Main.dust[num344].velocity *= 7f;
            num344 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.5f);
            Main.dust[num344].velocity *= 3f;
        }
        for (int num345 = 0; num345 < 2; num345++)
        {
            float scaleFactor8 = 0.4f;
            if (num345 == 1)
            {
                scaleFactor8 = 0.8f;
            }
            int num346 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default, Main.rand.Next(61, 64), 1f);
            Main.gore[num346].velocity *= scaleFactor8;
            Main.gore[num346].velocity.X++;
            Main.gore[num346].velocity.Y++;
            num346 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default, Main.rand.Next(61, 64), 1f);
            Main.gore[num346].velocity *= scaleFactor8;
            Main.gore[num346].velocity.X--;
            Main.gore[num346].velocity.Y++;
            num346 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default, Main.rand.Next(61, 64), 1f);
            Main.gore[num346].velocity *= scaleFactor8;
            Main.gore[num346].velocity.X++;
            Main.gore[num346].velocity.Y--;
            num346 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default, Main.rand.Next(61, 64), 1f);
            Main.gore[num346].velocity *= scaleFactor8;
            Main.gore[num346].velocity.X--;
            Main.gore[num346].velocity.Y--;
        }
    }
    public override void AI()
    {
        if (Projectile.ai[1] != -1f && Projectile.position.Y > Projectile.ai[1])
        {
            Projectile.tileCollide = true;
        }
        if (Projectile.position.HasNaNs())
        {
            Projectile.Kill();
            return;
        }
        bool num220 = WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.position.X / 16, (int)Projectile.position.Y / 16));
        Dust dust2 = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)(Projectile.width * Projectile.scale), (int)(Projectile.height * Projectile.scale), DustID.Torch)];

        dust2.position = new Vector2(Projectile.position.X + (Projectile.width - 0.5f * Projectile.scale), Projectile.position.Y + (Projectile.height - 0.5f * Projectile.scale));
        dust2.velocity = Vector2.Zero;
        dust2.scale = 1.5f;
        dust2.noGravity = true;
        if (num220)
        {
            dust2.noLight = true;
        }
        //left side
        bool num221 = WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.position.X / 16, (int)Projectile.position.Y / 16));
        dust2 = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)(Projectile.width * Projectile.scale), (int)(Projectile.height * Projectile.scale), DustID.Torch)];

        dust2.position = new Vector2(Projectile.position.X + (Projectile.width * Projectile.scale), Projectile.position.Y + (Projectile.height - 0.5f * Projectile.scale));
        dust2.velocity = Vector2.Zero;
        dust2.scale = 1.5f;
        dust2.noGravity = true;
        if (num221)
        {
            dust2.noLight = true;
        }
        //right side
        bool num222 = WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.position.X / 16, (int)Projectile.position.Y / 16));
        dust2 = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), (int)(Projectile.width * Projectile.scale), (int)(Projectile.height * Projectile.scale), DustID.Torch)];

        dust2.position = new Vector2(Projectile.position.X + (Projectile.width + 0.5f * Projectile.scale), Projectile.position.Y + (Projectile.height - 0.5f * Projectile.scale));
        dust2.velocity = Vector2.Zero;
        dust2.scale = 1.5f;
        dust2.noGravity = true;
        if (num221)
        {
            dust2.noLight = true;
        }
        Projectile.ai[0]++;
        if (Projectile.ai[0] == 20)
        {
            float speedX = Projectile.velocity.X + Main.rand.Next(20, 51) * 0.1f;
            float speedY = Projectile.velocity.Y + Main.rand.Next(20, 51) * 0.1f;
            int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(speedX, speedY), ModContent.ProjectileType<BlahStar>(), Projectile.damage, Projectile.knockBack);
            Main.projectile[p].friendly = true;
            Main.projectile[p].hostile = false;
            Main.projectile[p].owner = Projectile.owner;
        }
        if (Projectile.ai[0] == 40)
        {
            float speedX = Projectile.velocity.X + Main.rand.Next(-51, -20) * 0.1f;
            float speedY = Projectile.velocity.Y + Main.rand.Next(20, 51) * 0.1f;
            int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(speedX, speedY), ModContent.ProjectileType<BlahStar>(), Projectile.damage, Projectile.knockBack);
            Main.projectile[p].friendly = true;
            Main.projectile[p].hostile = false;
            Main.projectile[p].owner = Projectile.owner;
            Projectile.ai[0] = 0;
        }
    }
}
