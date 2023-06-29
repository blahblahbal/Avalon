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
        Projectile.timeLeft = 21;
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
                Vector2 mousePos = Main.MouseScreen + Main.screenPosition;
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    p.GetModPlayer<AvalonPlayer>().MousePosition = mousePos;
                    CursorPosition.SendPacket(mousePos, p.whoAmI);
                    //ModContent.GetInstance<SyncMouse>().Send(new BasicPlayerNetworkArgs(p));
                }
                else if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    p.GetModPlayer<AvalonPlayer>().MousePosition = mousePos;
                }

                //if (Main.netMode != NetmodeID.SinglePlayer)
                //{
                //    ModContent.GetInstance<SyncMouse>().Send(new BasicPlayerNetworkArgs(p));

                //}
                DrawChain(p.Center + new Vector2(50, 0).RotatedBy(p.AngleTo(p.GetModPlayer<AvalonPlayer>().MousePosition)), p.GetModPlayer<AvalonPlayer>().MousePosition);
            }
        }

        //Player p = Main.player[Projectile.owner];
        //Texture2D TEX = ModContent.Request<Texture2D>(Texture).Value;
        //Vector2 Start = p.Center + new Vector2(0, 20).RotatedBy(p.AngleTo(p.GetModPlayer<AvalonPlayer>().MousePosition)) - Main.screenPosition;
        //Vector2 End = p.GetModPlayer<AvalonPlayer>().MousePosition - Main.screenPosition;
        //Main.spriteBatch.End();
        //Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer);
        //if (Vector2.Distance(Projectile.position, p.position) < Vector2.Distance(p.GetModPlayer<AvalonPlayer>().MousePosition, p.position))
        //{
        //    Main.EntitySpriteDraw(TEX, Start, new Rectangle(0, TEX.Height, TEX.Height, TEX.Width), new Color(255, 255, 255, 0), Start.DirectionTo(End).ToRotation(), new Vector2(0, TEX.Height / 2f), new Vector2((float)Math.Cbrt(Start.Distance(End)) * (float)Math.Cbrt(Start.Distance(End)), 5), SpriteEffects.None, 0);
        //}
        return false;
    }

    public override void AI()
    {
        Projectile.ai[0]++;
        Player p = Main.player[Projectile.owner];
        if (!p.channel) Projectile.Kill();
    }
}
