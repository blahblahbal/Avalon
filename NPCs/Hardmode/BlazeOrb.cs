using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Hardmode;

public class BlazeOrb : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 1;

        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
        {
            Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
    }

    public override void SetDefaults()
    {
        NPC.damage = 65;
        NPC.scale = 0.9f;
        NPC.noTileCollide = true;
        NPC.lifeMax = 1;
        NPC.defense = 0;
        NPC.noGravity = true;
        NPC.alpha = 80;
        NPC.width = 16;
        NPC.aiStyle = -1;
        NPC.height = 16;
        NPC.HitSound = SoundID.NPCHit3;
        NPC.DeathSound = SoundID.NPCDeath3;
        NPC.knockBackResist = 0f;
    }

    public override void AI()
    {
        if (NPC.target == 255)
        {
            NPC.TargetClosest(true);
            var num279 = 6f;
            var vector25 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
            var num280 = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - vector25.X;
            var num281 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2 - vector25.Y;
            var num282 = (float)Math.Sqrt(num280 * num280 + num281 * num281);
            num282 = num279 / num282;
            NPC.velocity.X = num280 * num282;
            NPC.velocity.Y = num281 * num282;
        }
        NPC.ai[0] += 1f;
        if (NPC.ai[0] > 3f)
        {
            NPC.ai[0] = 3f;
        }
        if (NPC.ai[0] == 2f)
        {
            NPC.position += NPC.velocity;
            SoundEngine.PlaySound(SoundID.Item20, NPC.position);
            for (var num285 = 0; num285 < 20; num285++)
            {
                var num286 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + 2f), NPC.width, NPC.height, DustID.Torch, 0f, 0f, 100, default(Color), 1.8f);
                Main.dust[num286].velocity *= 1.3f;
                Main.dust[num286].velocity += NPC.velocity;
                Main.dust[num286].noGravity = true;
            }
        }
        if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height) && Main.netMode != NetmodeID.MultiplayerClient)
        {
            var num287 = (int)(NPC.position.X + NPC.width / 2) / 16;
            var num288 = (int)(NPC.position.Y + NPC.height / 2) / 16;
            var num289 = 8;
            for (var num290 = num287 - num289; num290 <= num287 + num289; num290++)
            {
                for (var num291 = num288 - num289; num291 < num288 + num289; num291++)
                {
                    if (Math.Abs(num290 - num287) + Math.Abs(num291 - num288) < num289 * 0.5)
                    {
                        var tile = Main.tile[num290, num291];
                        if (tile.TileType == TileID.Ash)
                        {
                            if (Main.rand.NextBool(3))
                            {
                                Main.tile[num290, num291].TileType = TileID.Hellstone;
                            }
                            else
                            {
                                Main.tile[num290, num291].TileType = (ushort)ModContent.TileType<Tiles.BrimstoneBlock>();
                            }
                            WorldGen.SquareTileFrame(num290, num291, true);
                            if (Main.netMode == NetmodeID.Server)
                            {
                                NetMessage.SendTileSquare(-1, num290, num291, 1);
                            }
                        }
                        //else if (tile.TileType == TileID.Hellstone)
                        //{
                        //    if (Main.rand.Next(5) == 0 && ModContent.GetInstance<AvalonWorld>().SuperHardmode && Main.hardMode)
                        //    {
                        //        Main.tile[num290, num291].TileType = (ushort)ModContent.TileType<Tiles.Ores.CaesiumOre>();
                        //    }
                        //    WorldGen.SquareTileFrame(num290, num291);
                        //    if (Main.netMode == NetmodeID.Server)
                        //    {
                        //        NetMessage.SendTileSquare(-1, num290, num291, 1);
                        //    }
                        //}
                        else if (tile.TileType == TileID.Obsidian)
                        {
                            if (Main.rand.NextBool(10))
                            {
                                Main.tile[num290, num291].TileType = TileID.Hellstone;
                            }
                            WorldGen.SquareTileFrame(num290, num291);
                            if (Main.netMode == NetmodeID.Server)
                            {
                                NetMessage.SendTileSquare(-1, num290, num291, 1);
                            }
                        }
                    }
                }
            }
        }
        if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
        {
            var arg_12ED7_0 = Main.netMode;
            NPC.StrikeNPC(new NPC.HitInfo {Damage = 999, Knockback = 0f, HitDirection = 0});
        }
        if (NPC.timeLeft > 100)
        {
            NPC.timeLeft = 100;
        }
        for (var num292 = 0; num292 < 2; num292++)
        {
            var num302 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + 2f), NPC.width, NPC.height, DustID.Torch, NPC.velocity.X * 0.1f, NPC.velocity.Y * 0.1f, 80, default(Color), 1.3f);
            Main.dust[num302].velocity *= 0.3f;
            Main.dust[num302].noGravity = true;
        }
        NPC.rotation += 0.4f * NPC.direction;
        return;
    }
}
