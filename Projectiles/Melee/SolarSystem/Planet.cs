using Avalon.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.SolarSystem;

public abstract class Planet : ModProjectile
{
    private int hostPosition = -1;
    private LinkedListNode<int> positionNode;
    public virtual int Radius { get; set; } = 1;
    public virtual string PlanetName { get; set; } = "Mercury";

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 100;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        AvalonPlayer modPlayer = Main.player[Projectile.owner].GetModPlayer<AvalonPlayer>();
        positionNode ??= modPlayer.HandlePlanets((int)Projectile.ai[0]);
        writer.Write(positionNode.Value);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        base.ReceiveExtraAI(reader);
        hostPosition = reader.ReadInt32();
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("Avalon/Projectiles/Melee/SolarSystem/Trail");
        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;

        for (int i = 1; i < Projectile.oldPos.Length; i++)
        {
            Vector2 drawPos = Projectile.oldPos[i] + new Vector2(Projectile.width / 2, Projectile.height / 2) - Main.screenPosition;
            Main.EntitySpriteDraw(texture, drawPos, frame, Color.White, Projectile.oldRot[i], frameOrigin, 1f, SpriteEffects.None, 0);
        }
        Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + frameOrigin, frame, Color.White, Projectile.rotation, frameOrigin, 1f, SpriteEffects.None, 0);

        Texture2D planetTexture = (Texture2D)ModContent.Request<Texture2D>("Avalon/Projectiles/Melee/SolarSystem/" + PlanetName);
        Rectangle planetFrame = planetTexture.Frame();
        Vector2 planetFrameOrigin = planetFrame.Size() / 2f;
        Main.EntitySpriteDraw(planetTexture, Projectile.position - Main.screenPosition + planetFrameOrigin, planetFrame, Color.White, Projectile.rotation, planetFrameOrigin, 1f, SpriteEffects.None, 0);

        return false;
    }
    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        AvalonPlayer modPlayer = player.GetModPlayer<AvalonPlayer>();

        if (!Main.player[Projectile.owner].channel)
        {
            Projectile.ai[2] = 1;
        }
        else Projectile.timeLeft = 600;

        if (Projectile.ai[2] == 0)
        {
            // Get position in circle
            if (hostPosition == -1)
            {
                positionNode ??= modPlayer.HandlePlanets((int)Projectile.ai[0]);
            }
            else
            {
                positionNode ??= modPlayer.ObtainExistingPlanet(hostPosition, (int)Projectile.ai[0]);
            }
            const float speed = 1.54f;
            Vector2 target = Main.projectile[(int)Projectile.ai[1]].Center +
                             (Vector2.One.RotatedBy(
                                 (MathHelper.TwoPi / modPlayer.Planets[(int)Projectile.ai[0]].Count * positionNode.Value) +
                                 modPlayer.PlanetRotation[(int)Projectile.ai[0]]) * Radius);
            Vector2 error = target - Projectile.Center;

            Projectile.velocity = error * speed;
        }
        else if (Projectile.ai[2] == 1)
        {
            Projectile.velocity = Vector2.Normalize(Main.projectile[(int)Projectile.ai[1]].Center - player.Center) * 8f;
            //Projectile.ai[2] = 2;
        }
    }
}
