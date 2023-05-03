using Avalon.Common.Players;
using Avalon.Network;
using Avalon.Network.Handlers;
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
        Projectile.timeLeft = 30;
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
            Main.spriteBatch.Draw(TEX, links[i], new Rectangle(0, 0, TEX.Width, linklength), c, rotation + 1.57f, new Vector2(TEX.Width / 2f, linklength), 1.1f,
                SpriteEffects.None, 1f);
        }
    }
    public override void PostDraw(Color lightColor)
    {
        Player p = Main.player[Projectile.owner];
        if (Projectile.ai[0] > 0)
        {
            if (Vector2.Distance(Projectile.position, p.position) < Vector2.Distance(p.GetModPlayer<AvalonPlayer>().MousePosition, p.position))
            {
                if (Main.netMode != NetmodeID.SinglePlayer)
                {
                    ModContent.GetInstance<SyncMouse>().Send(new BasicPlayerNetworkArgs(p));
                }
                DrawChain(p.Center + new Vector2(50, 0).RotatedBy(p.AngleTo(p.GetModPlayer<AvalonPlayer>().MousePosition)), p.GetModPlayer<AvalonPlayer>().MousePosition);
            }
        }
        
        if (false) //Main.netMode != NetmodeID.SinglePlayer)
        {
            if (Projectile.ai[0] > 9f)
            {
                if (Vector2.Distance(Projectile.position, p.position) < Vector2.Distance(p.GetModPlayer<AvalonPlayer>().MousePosition, p.position))
                {
                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        ModContent.GetInstance<SyncMouse>().Send(new BasicPlayerNetworkArgs(p));
                    }
                    for (var num617 = 0; num617 < 1; num617++)
                    {
                        var value12 = Projectile.position;
                        value12 -= Projectile.velocity * num617 * 0.25f;
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
                        else if (p.team == (int)Terraria.Enums.Team.Red)
                        {
                            c = Color.Red;
                        }
                        Dust d = Dust.NewDustDirect(value12, 1, 1, ModContent.DustType<Dusts.PointingDust>(), 0f, 0f, 0, c, 1f);
                        d.velocity *= 0.2f;
                        d.noGravity = true;
                    }
                }
            }
        }
    }
    public override void AI()
    {
        Projectile.ai[0]++;
        Player p = Main.player[Projectile.owner];
        if (!p.channel) Projectile.Kill();
        //if (Projectile.ai[0] > 9f)
        //{
        //    if (Vector2.Distance(Projectile.position, p.position) < Vector2.Distance(p.GetModPlayer<AvalonPlayer>().MousePosition, p.position))
        //    {
        //        if (Main.netMode != NetmodeID.SinglePlayer)
        //        {
        //            ModContent.GetInstance<SyncMouse>().Send(new BasicPlayerNetworkArgs(p));
        //        }
        //        for (var num617 = 0; num617 < 1; num617++)
        //        {
        //            var value12 = Projectile.position;
        //            value12 -= Projectile.velocity * num617 * 0.25f;
        //            Projectile.alpha = 255;
        //            var num618 = ModContent.DustType<Dusts.PointingDust>();
        //            Color c = Color.White;
        //            if (p.team == (int)Terraria.Enums.Team.Pink)
        //            {
        //                c = new Color(171, 59, 218);
        //            }
        //            else if (p.team == (int)Terraria.Enums.Team.Green)
        //            {
        //                c = Color.Green; // new Color(59, 218, 85);
        //            }
        //            else if (p.team == (int)Terraria.Enums.Team.Blue)
        //            {
        //                c = Color.Blue; // new Color(59, 149, 218);
        //            }
        //            else if (p.team == (int)Terraria.Enums.Team.Yellow)
        //            {
        //                c = Color.Yellow; // new Color(218, 183, 59);
        //            }
        //            else if (p.team == (int)Terraria.Enums.Team.Red || Main.netMode == NetmodeID.SinglePlayer)
        //            {
        //                c = Color.Red; // new Color(218, 59, 59);
        //            }
        //            var num619 = Dust.NewDust(value12, 1, 1, num618, 0f, 0f, 0, c, 1f);
        //            Main.dust[num619].position = value12;
        //            Main.dust[num619].color = c;
        //            Main.dust[num619].velocity *= 0.2f;
        //            Main.dust[num619].noGravity = true;
        //        }
        //    }
        //}
    }

}
