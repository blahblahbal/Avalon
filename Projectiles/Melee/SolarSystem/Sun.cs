using Avalon.Common;
using Avalon.Network;
using Avalon.Network.Handlers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.SolarSystem;

public class Sun : ModProjectile
{
    Vector2 mousePosition = Vector2.Zero;

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.penetrate = -1;
        Projectile.width = dims.Width;
        Projectile.height = dims.Height;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.tileCollide = false;
        Projectile.ownerHitCheck = true;
        Projectile.extraUpdates = 1;
        Projectile.timeLeft = 600;
        DrawOffsetX = -(int)((dims.Width / 2) - (Projectile.Size.X / 2));
        DrawOriginOffsetY = -(int)((dims.Width / 2) - (Projectile.Size.Y / 2));
    }

    public override void AI()
    {
        if (Main.player[Projectile.owner].channel)
        {
            Projectile.timeLeft = 600;
            Projectile.ai[1] = 1;
        }
        else
        {
            Projectile.ai[1] = 2; // not channeling from the start, launch in direction of cursor
        }
        if (Projectile.ai[0] == 0) // spawn planets
        {
            // mercury
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position,
                Projectile.velocity, ModContent.ProjectileType<Mercury>(), (int)(Projectile.damage * 0.8f),
                Projectile.knockBack, ai1: Projectile.whoAmI);

            // venus
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position,
                Projectile.velocity, ModContent.ProjectileType<Venus>(), (int)(Projectile.damage * 0.65f),
                Projectile.knockBack, ai1: Projectile.whoAmI);

            // earth
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position,
                Projectile.velocity, ModContent.ProjectileType<Earth>(), (int)(Projectile.damage * 0.55f),
                Projectile.knockBack, ai1: Projectile.whoAmI);

            // mars
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position,
                Projectile.velocity, ModContent.ProjectileType<Mars>(), (int)(Projectile.damage * 0.43f),
                Projectile.knockBack, ai1: Projectile.whoAmI);

            // jupiter
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position,
                Projectile.velocity, ModContent.ProjectileType<Jupiter>(), (int)(Projectile.damage * 0.4f),
                Projectile.knockBack, ai1: Projectile.whoAmI);

            Projectile.ai[0] = 1;
        }
        if (Projectile.ai[1] == 0) // launch from player toward cursor
        {
            AvalonPlayer modPlayer = Main.player[Projectile.owner].GetModPlayer<AvalonPlayer>();
            Vector2 mousePos = Main.MouseScreen;
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                modPlayer.MousePosition = mousePos;
                CursorPosition.SendPacket(mousePos, Projectile.owner);
            }
            else if (Main.netMode == NetmodeID.SinglePlayer)
            {
                modPlayer.MousePosition = mousePos;
            }

            Vector2 heading = mousePos + Main.screenPosition - Main.player[Projectile.owner].Center;
            heading.Normalize();
            heading *= new Vector2(5f, 5f).Length(); // multiply by speed
            Projectile.velocity = heading;

            if (Vector2.Distance(Projectile.Center, mousePos + Main.screenPosition) < 20)
            {
                Projectile.ai[1] = 1; // lock on cursor
            }
        }
        else if (Projectile.ai[1] == 1) // if channel, lock on cursor; otherwise launch in direction of cursor
        {
            if (!Main.player[Projectile.owner].channel)
            {
                Projectile.ai[1] = 2;
                return;
            }
            else
            {
                Projectile.Center = Main.MouseScreen + Main.screenPosition;
                mousePosition = Main.MouseScreen + Main.screenPosition;
            }
        }
        else if (Projectile.ai[1] == 2) // after releasing mouse button, launch off in direction of cursor
        {
            AvalonPlayer modPlayer = Main.player[Projectile.owner].GetModPlayer<AvalonPlayer>();
            Vector2 mousePos = Main.MouseScreen;
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                modPlayer.MousePosition = mousePos;
                CursorPosition.SendPacket(mousePos, Projectile.owner);
            }
            else if (Main.netMode == NetmodeID.SinglePlayer)
            {
                modPlayer.MousePosition = mousePos;
            }
            if (mousePosition != Vector2.Zero)
            {

            }
            Vector2 heading = (mousePosition != Vector2.Zero ? mousePosition : mousePos + Main.screenPosition) - Main.player[Projectile.owner].Center;
            heading.Normalize();
            heading *= new Vector2(8f, 8f).Length(); // multiply by speed
            Projectile.velocity = heading;
            Projectile.ai[1] = 3;
        }
    }

    public override bool PreDraw(ref Color lightColor)
    {
        var texture = Mod.Assets.Request<Texture2D>("Projectiles/Melee/SolarSystem/SolarSystem_Chain");

        var position = Projectile.Center;
        var mountedCenter = Main.player[Projectile.owner].MountedCenter;
        var sourceRectangle = new Rectangle?();
        var origin = new Vector2(texture.Value.Width * 0.5f, texture.Value.Height * 0.5f);
        float num1 = texture.Value.Height;
        var vector2_4 = mountedCenter - position;
        var rotation = (float)Math.Atan2(vector2_4.Y, vector2_4.X) - 1.57f;
        var flag = true;
        if (float.IsNaN(position.X) && float.IsNaN(position.Y))
            flag = false;
        if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
            flag = false;
        while (flag)
        {
            if (vector2_4.Length() < num1 + 1.0)
            {
                flag = false;
            }
            else
            {
                var vector2_1 = vector2_4;
                vector2_1.Normalize();
                position += vector2_1 * num1;
                vector2_4 = mountedCenter - position;
                var color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                color2 = Projectile.GetAlpha(color2);
                Main.EntitySpriteDraw(texture.Value, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0);
            }
        }

        return true;
    }
}
