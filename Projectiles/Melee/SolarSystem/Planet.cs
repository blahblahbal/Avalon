using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.SolarSystem;

public abstract class Planet : ModProjectile
{
	private static Asset<Texture2D> texture;
	private int hostPosition = -1;
    private LinkedListNode<int> positionNode;
    public virtual int Radius { get; set; } = 1;
    public virtual string PlanetName { get; set; } = "Mercury";

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 100;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
        texture = ModContent.Request<Texture2D>("Avalon/Projectiles/Melee/SolarSystem/Trail");
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
        float scale = 1f;
        Color planetColor = Color.White;
        switch (PlanetName)
        {
            case "Mercury":
                scale = 0.37f;
                //planetColor = new Color(206, 143, 90);
                break;
            case "Venus":
                scale = 0.75f;
                planetColor = new Color(222, 186, 135);
                break;
            case "Earth":
                scale = 0.75f;
                planetColor = new Color(86, 212, 251);
                break;
            case "Mars":
                scale = 0.6f;
                planetColor = new Color(214, 74, 61);
                break;
            case "Jupiter":
                scale = 2.5f;
                planetColor = new Color(206, 143, 90);
                break;
            case "Saturn":
                scale = 1.8f;
                planetColor = new Color(228, 197, 138);
                break;
            case "Uranus":
                scale = 1.3f;
                planetColor = new Color(57, 116, 186);
                break;
            case "Neptune":
                scale = 1.4f;
                planetColor = new Color(64, 106, 255);
                break;
            case "Pluto":
                scale = 0.37f;
                planetColor = new Color(234, 219, 198);
                break;
        }
        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;

        int start = 1;
        if (PlanetName == "Mercury" || PlanetName == "Pluto") start = 3;
        if (PlanetName == "Neptune" || PlanetName == "Uranus") start = 4;
        float oof = 1f;
        for (int i = start; i < Projectile.oldPos.Length; i++)
        {
            if (PlanetName == "Jupiter")
            {
               
            }
            scale *= 0.98f;
            oof -= 0.005f;
            Color c = planetColor;
            c *= oof;

            //Main.NewText(c.A);
            Vector2 drawPos = Projectile.oldPos[i] + new Vector2(Projectile.width / 2, Projectile.height / 2) - Main.screenPosition;
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
            Main.EntitySpriteDraw(texture.Value, drawPos, frame, c, Projectile.oldRot[i] + 1.57f, frameOrigin, scale, SpriteEffects.None, 0);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
        }

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
        else Projectile.timeLeft = 300;

        if (Projectile.ai[2] == 2)
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
            float speed = Main.rand.NextFloat(6.5f, 10f);
            Projectile.velocity = Vector2.Normalize(Main.projectile[(int)Projectile.ai[1]].Center - player.Center) * speed;
        }
        else if (Projectile.ai[2] == 0)
        {
            if (hostPosition == -1)
            {
                positionNode ??= modPlayer.HandlePlanets((int)Projectile.ai[0]);
            }
            else
            {
                positionNode ??= modPlayer.ObtainExistingPlanet(hostPosition, (int)Projectile.ai[0]);
            }

            Vector2 target = Main.projectile[(int)Projectile.ai[1]].Center +
                             (Vector2.One.RotatedBy(
                                 (MathHelper.TwoPi / modPlayer.Planets[(int)Projectile.ai[0]].Count * positionNode.Value) +
                                 modPlayer.PlanetRotation[(int)Projectile.ai[0]]) * Radius);
            Vector2 heading = target - Projectile.Center;
            float multiplier = 1;
            switch ((int)Projectile.ai[0])
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    multiplier = 1f;
                    break;
                case 6:
                    multiplier = 1.8f;
                    break;
                case 7:
                    multiplier = 2.6f;
                    break;
                case 8:
                    multiplier = 3f;
                    break;
            }

            heading.Normalize();
            heading *= new Vector2(3).Length();
            Projectile.velocity = heading * multiplier;

            if (Vector2.Distance(target, Projectile.Center) < 1)
            {
                Projectile.ai[2] = 2;
            }
        }
    }
}
