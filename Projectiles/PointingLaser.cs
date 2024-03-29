using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles;

public class PointingLaser : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 4 / 1;
        Projectile.height = dims.Height * 4 / 1 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.tileCollide = false;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.MaxUpdates = 1;
        Projectile.extraUpdates = 1;
        Projectile.alpha = 255;
        Projectile.timeLeft = 9;
        Projectile.damage = 0;
        Projectile.penetrate = -1;
    }
    public override bool? CanCutTiles()
    {
        return false;
    }

    public void DrawChain(Vector2 start, Vector2 end)
    {
        start -= Main.screenPosition;
        end -= Main.screenPosition;
        Texture2D TEX = ModContent.Request<Texture2D>(Texture).Value;
        int linklength = TEX.Height;
        Vector2 chain = end - start;

        float length = (float)chain.Length();
        int numlinks = (int)Math.Ceiling(length / linklength);
        Vector2[] links = new Vector2[numlinks];
        float rotation = (float)Math.Atan2(chain.Y, chain.X);
        Projectile P = Projectile;
        Player Pr = Main.player[P.owner];
        Player MyPr = Main.player[Main.myPlayer];
        for (int i = 0; i < numlinks; i++)
        {
            links[i] = start + chain / numlinks * i;

            #region color determination
            Player p = Main.player[Projectile.owner];
            Color c = Color.White;
            if (p.team == (int)Terraria.Enums.Team.Pink)
            {
                c = new Color(171, 59, 218);
            }
            else if (p.team == (int)Terraria.Enums.Team.Green)
            {
                c = Color.Green; // new Color(59, 218, 85);
            }
            else if (p.team == (int)Terraria.Enums.Team.Blue)
            {
                c = Color.Blue; // new Color(59, 149, 218);
            }
            else if (p.team == (int)Terraria.Enums.Team.Yellow)
            {
                c = Color.Yellow; // new Color(218, 183, 59);
            }
            else if (p.team == (int)Terraria.Enums.Team.Red || Main.netMode == NetmodeID.SinglePlayer)
            {
                c = Color.Red;
            }
            #endregion
            c.A = 255;
            Main.spriteBatch.Draw(TEX, links[i], new Rectangle(0, 0, TEX.Width, linklength), c, rotation + 1.57f, new Vector2(TEX.Width / 2f, linklength), 2f,
                SpriteEffects.None, 1f);
        }
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Player p = Main.player[Projectile.owner];
        if (Projectile.ai[0] > 0)
        {
            if (Vector2.Distance(Projectile.position, p.position) < Vector2.Distance(p.GetModPlayer<AvalonPlayer>().MousePosition, p.position))
            {
                DrawChain(p.RotatedRelativePoint(p.MountedCenter) + new Vector2(50, 0).RotatedBy(p.AngleTo(p.GetModPlayer<AvalonPlayer>().MousePosition)), p.GetModPlayer<AvalonPlayer>().MousePosition);
            }
        }
        return false;
    }

    public override void AI()
    {
        Projectile.ai[0]++;
        Player p = Main.player[Projectile.owner];
        if (!p.channel) Projectile.Kill();
    }
}
