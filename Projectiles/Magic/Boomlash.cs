using System;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Avalon.Projectiles.Magic;

public class Boomlash : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 1;
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 29;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
    }
    public override void SetDefaults()
    {
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.friendly = true;
        Projectile.light = 0.8f;
        Projectile.DamageType = DamageClass.Magic;
        DrawOriginOffsetY = -6;
        Projectile.extraUpdates = 1;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        //Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
        Texture2D texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/Sparkly").Value;
        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;

        Texture2D texture2 = (Texture2D)ModContent.Request<Texture2D>(Texture);
        Rectangle frame2 = texture2.Frame();
        Vector2 frameOrigin2 = frame2.Size() / 2f;

        Color col = Color.Lerp(Color.Yellow, Color.Red, Main.masterColor) * 0.4f;
        Color col2 = Color.Lerp(Color.White, Color.Black, Main.masterColor);
        Vector2 stretchscale = new Vector2(Projectile.scale * 1.4f + (Main.masterColor / 2));


        for (int i = 1; i < (Projectile.oldPos.Length - 1); i++)
        {
            col.A = 0;
            Vector2 drawPos = Projectile.oldPos[i] - Main.screenPosition + new Vector2(Projectile.width / 2);
            //int col = (int)(128 - (i * 16) * Projectile.Opacity);
            //Main.EntitySpriteDraw(texture, drawPos, frame, new Color(col / i, col / i, col, 0), Projectile.oldRot[i], frameOrigin, Projectile.scale, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(texture, drawPos + Main.rand.NextVector2Circular(i / 2,i / 2), frame, new Color(col.R, col.G - (i * 8), col.B, 0) * (1 - (i * 0.04f)), Projectile.oldRot[i] + Main.rand.NextFloat(-i * 0.01f,i * 0.01f), frameOrigin, new Vector2(stretchscale.X - (i * 0.05f), stretchscale.Y * Main.rand.NextFloat(0.1f,0.05f) * Vector2.Distance(Projectile.oldPos[i], Projectile.oldPos[i+1]) - (i * 0.05f)), SpriteEffects.None, 0);
        }
        
        Main.EntitySpriteDraw(texture2, Projectile.Center - Main.screenPosition, frame2, new Color(255,0,0,0), Projectile.rotation, frameOrigin2, stretchscale * 0.8f, SpriteEffects.None, 0);
        col.A = 255;
        Main.EntitySpriteDraw(texture2, Projectile.Center - Main.screenPosition, frame2, col2 * Projectile.Opacity, Projectile.rotation, frameOrigin2, Projectile.scale, SpriteEffects.None, 0);

        return false;
    }

    public override void OnSpawn(IEntitySource source)
    {
        for(int k = 0; k < Projectile.oldPos.Length; k++)
        {
            Projectile.oldPos[k] = Projectile.position;
        }
    }
    public override void AI()
    {
        //if (Projectile.soundDelay == 0 && Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) > 2f)
        //{
        //    Projectile.soundDelay = 50;
        //    SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath, Projectile.position);
        //}
        if (Main.rand.NextBool(3) && Projectile.velocity != Vector2.Zero)
        {
            int dusty = Dust.NewDust(Projectile.position + new Vector2(0,-20).RotatedBy(Projectile.rotation), 0, 0, DustID.InfernoFork);
            Main.dust[dusty].noGravity = true;
            Main.dust[dusty].velocity = Projectile.velocity * -0.6f;
            Main.dust[dusty].scale = 1f;
        }
        if (Main.rand.NextBool(6))
        {
            int dusty = Dust.NewDust(Projectile.position + new Vector2(0, -20).RotatedBy(Projectile.rotation), 0, 0, DustID.Smoke);
            Main.dust[dusty].noGravity = true;
            Main.dust[dusty].scale = 1f;
            Main.dust[dusty].alpha = 128;
        }

        if (Main.myPlayer == Projectile.owner && Projectile.ai[0] == 0f)
        {

            Player player = Main.player[Projectile.owner];
            if (player.channel)
            {
                float maxDistance = 18f;
                Vector2 vectorToCursor = Main.MouseWorld - Projectile.Center;
                float distanceToCursor = vectorToCursor.Length();

                if (distanceToCursor > maxDistance)
                {
                    distanceToCursor = maxDistance / distanceToCursor;
                    vectorToCursor *= distanceToCursor;
                }

                int velocityXBy1000 = (int)(vectorToCursor.X * 1000f);
                int oldVelocityXBy1000 = (int)(Projectile.velocity.X * 1000f);
                int velocityYBy1000 = (int)(vectorToCursor.Y * 1000f);
                int oldVelocityYBy1000 = (int)(Projectile.velocity.Y * 1000f);

                if (velocityXBy1000 != oldVelocityXBy1000 || velocityYBy1000 != oldVelocityYBy1000)
                {
                    Projectile.netUpdate = true;
                }

                Projectile.velocity = vectorToCursor;

            }
            else if (Projectile.ai[0] == 0f)
            {

                Projectile.netUpdate = true;

                float maxDistance = 14f;
                Vector2 vectorToCursor = Main.MouseWorld - Projectile.Center;
                float distanceToCursor = vectorToCursor.Length();

                if (distanceToCursor == 0f)
                {
                    vectorToCursor = Projectile.Center - player.Center;
                    distanceToCursor = vectorToCursor.Length();
                }

                distanceToCursor = maxDistance / distanceToCursor;
                vectorToCursor *= distanceToCursor;

                Projectile.velocity = vectorToCursor;

                if (Projectile.velocity == Vector2.Zero)
                {
                    Projectile.Kill();
                }

                Projectile.ai[0] = 1f;
            }
        }

        if (Projectile.velocity != Vector2.Zero)
        {
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;
        }
    }
    public override void Kill(int timeLeft)
    {
        if (Projectile.penetrate == 1)
        {
            Projectile.maxPenetrate = -1;
            Projectile.penetrate = -1;

            int explosionArea = 60;
            Vector2 oldSize = Projectile.Size;
            Projectile.position = Projectile.Center;
            Projectile.Size += new Vector2(explosionArea);
            Projectile.Center = Projectile.position;

            Projectile.tileCollide = false;
            Projectile.velocity *= 0.01f;
            Projectile.Damage();
            Projectile.scale = 0.01f;

            Projectile.position = Projectile.Center;
            Projectile.Size = new Vector2(10);
            Projectile.Center = Projectile.position;
        }

        SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);

        for (int i = 0; i < 20; i++)
        {
            int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0, 0, 0, default, 2f);
            Main.dust[d].velocity = Main.rand.NextVector2Circular(6, 6);
            Main.dust[d].noGravity = true;
            Main.dust[d].fadeIn = 2.3f;
        }
        for (int i = 0; i < 20; i++)
        {
            int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0, 0, 0, default, 1.4f);
            Main.dust[d].velocity = Main.rand.NextVector2Circular(10, 6) + new Vector2(-3, 0).RotatedBy(Projectile.velocity.ToRotation());
            Main.dust[d].noGravity = !Main.rand.NextBool(10);
        }
        for (int i = 0; i < 7; i++)
        {
            int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 0, 0, 0, default, 1.4f);
            //Main.dust[d].color = Color.Red;
            Main.dust[d].velocity = Main.rand.NextVector2Circular(10, 6) + new Vector2(-5,0).RotatedBy(Projectile.velocity.ToRotation());
            Main.dust[d].noGravity = Main.rand.NextBool(3);
        }
        for (int i = 0; i < 9; i++)
        {
            int g = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2Circular(10, 6) + new Vector2(-1,0).RotatedBy(Projectile.velocity.ToRotation()), Main.rand.Next(61, 63), 0.8f);
            Main.gore[g].alpha = 128;
        }

        /*
        for (int i = 0; i < 10; i++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.position - Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<SoulofBlight>(), 0, 0, 100, Color.Black, 0.8f);
            dust.noGravity = true;
            dust.velocity *= 2f;
            dust = Dust.NewDustDirect(Projectile.position - Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<SoulofBlight>(), 0f, 0f, 100, Color.Black, 0.5f);
        }

        for (int i = 0; i < 50; i++)
        {
            int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
            Main.dust[dustIndex].velocity *= 1.4f;
        }
        for (int i = 0; i < 80; i++)
        {
            int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
            Main.dust[dustIndex].noGravity = true;
            Main.dust[dustIndex].velocity *= 5f;
            dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
            Main.dust[dustIndex].velocity *= 3f;
        }
        for (int g = 0; g < 2; g++)
        {
            int goreIndex = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[goreIndex].scale = 1.5f;
            Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
            Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
            goreIndex = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[goreIndex].scale = 1.5f;
            Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
            Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
            goreIndex = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[goreIndex].scale = 1.5f;
            Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
            Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
            goreIndex = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[goreIndex].scale = 1.5f;
            Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
            Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
        }
        */
        Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
        Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
        Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);

        //{
        //    int explosionRadius = 2; //You can edit the explosion radius for the boomlash
        //    {
        //        explosionRadius = 3;
        //    }
        //    int minTileX = (int)(Projectile.position.X / 16f - (float)explosionRadius);
        //    int maxTileX = (int)(Projectile.position.X / 16f + (float)explosionRadius);
        //    int minTileY = (int)(Projectile.position.Y / 16f - (float)explosionRadius);
        //    int maxTileY = (int)(Projectile.position.Y / 16f + (float)explosionRadius);
        //    if (minTileX < 0)
        //    {
        //        minTileX = 0;
        //    }
        //    if (maxTileX > Main.maxTilesX)
        //    {
        //        maxTileX = Main.maxTilesX;
        //    }
        //    if (minTileY < 0)
        //    {
        //        minTileY = 0;
        //    }
        //    if (maxTileY > Main.maxTilesY)
        //    {
        //        maxTileY = Main.maxTilesY;
        //    }
        //    bool canKillWalls = false;
        //    for (int x = minTileX; x <= maxTileX; x++)
        //    {
        //        for (int y = minTileY; y <= maxTileY; y++)
        //        {
        //            float diffX = Math.Abs((float)x - Projectile.position.X / 16f);
        //            float diffY = Math.Abs((float)y - Projectile.position.Y / 16f);
        //            double distance = Math.Sqrt((double)(diffX * diffX + diffY * diffY));
        //            if (distance < (double)explosionRadius && Main.tile[x, y] != null && Main.tile[x, y].WallType == 0)
        //            {
        //                canKillWalls = true;
        //                break;
        //            }
        //        }
        //    }
        //    AchievementsHelper.CurrentlyMining = true;
        //    for (int i = minTileX; i <= maxTileX; i++)
        //    {
        //        for (int j = minTileY; j <= maxTileY; j++)
        //        {
        //            float diffX = Math.Abs((float)i - Projectile.position.X / 16f);
        //            float diffY = Math.Abs((float)j - Projectile.position.Y / 16f);
        //            double distanceToTile = Math.Sqrt((double)(diffX * diffX + diffY * diffY));
        //            if (distanceToTile < (double)explosionRadius)
        //            {
        //                bool canKillTile = true;
        //                if (Main.tile[i, j] != null && Main.tile[i, j].HasTile)
        //                {
        //                    canKillTile = true;
        //                    if (Main.tileDungeon[(int)Main.tile[i, j].TileType] || Main.tile[i, j].TileType == 88 || Main.tile[i, j].TileType == 21 || Main.tile[i, j].TileType == 26 || Main.tile[i, j].TileType == 107 || Main.tile[i, j].TileType == 108 || Main.tile[i, j].TileType == 111 || Main.tile[i, j].TileType == 226 || Main.tile[i, j].TileType == 237 || Main.tile[i, j].TileType == 221 || Main.tile[i, j].TileType == 222 || Main.tile[i, j].TileType == 223 || Main.tile[i, j].TileType == 211 || Main.tile[i, j].TileType == 404)
        //                    {
        //                        canKillTile = false;
        //                    }
        //                    if (!Main.hardMode && Main.tile[i, j].TileType == 58)
        //                    {
        //                        canKillTile = false;
        //                    }
        //                    if (!TileLoader.CanExplode(i, j))
        //                    {
        //                        canKillTile = false;
        //                    }
        //                    if (canKillTile)
        //                    {
        //                        WorldGen.KillTile(i, j, false, false, false);
        //                        if (!Main.tile[i, j].HasTile && Main.netMode != NetmodeID.SinglePlayer)
        //                        {
        //                            NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, (float)i, (float)j, 0f, 0, 0, 0);
        //                        }
        //                    }
        //                }
        //                if (canKillTile)
        //                {
        //                    for (int x = i - 1; x <= i + 1; x++)
        //                    {
        //                        for (int y = j - 1; y <= j + 1; y++)
        //                        {
        //                            if (Main.tile[x, y] != null && Main.tile[x, y].WallType > 0 && canKillWalls && WallLoader.CanExplode(x, y, Main.tile[x, y].WallType))
        //                            {
        //                                WorldGen.KillWall(x, y, false);
        //                                if (Main.tile[x, y].WallType == 0 && Main.netMode != NetmodeID.SinglePlayer)
        //                                {
        //                                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 2, (float)x, (float)y, 0f, 0, 0, 0);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    AchievementsHelper.CurrentlyMining = false;
        //}
    }
}
