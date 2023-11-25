using System;
using Avalon.Common;
using Avalon.Items.Banners;
using Avalon.Items.Material;
using Avalon.Projectiles.Hostile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;

namespace Avalon.NPCs.PreHardmode;

public class Shadlopod : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 4;
    }

    public override void SetDefaults()
    {
        NPC.damage = 35;
        NPC.lifeMax = 110;
        NPC.defense = 5;
        NPC.aiStyle = -1;
        NPC.value = 150f;
        NPC.height = 32;
        NPC.width = 20;
        NPC.knockBackResist = 0f;
        NPC.HitSound = SoundID.NPCHit20;
        NPC.DeathSound = SoundID.NPCDeath23;
        NPC.buffImmune[BuffID.Confused] = true;
        NPC.gfxOffY = 10;
        NPC.noGravity = true;
        NPC.hide = true;
    }
    float collisionPoint = 0f;
    bool Grounded = false;
    public override void AI()
    {
        bool FoundTile = false;
        if (NPC.ai[0] == 0 && !Grounded)
        {
            NPC.ai[0] = 1;
            for (int i = 32; i < 700; i += 4)
            {
                Main.LocalPlayer.position = NPC.position;
                Main.NewText(i, Color.Wheat);
                if (Collision.SolidCollision(NPC.Center + new Vector2(0, -i), NPC.width, NPC.height))
                {
                    NPC.position.Y = new Vector2(0, NPC.position.Y + -i + 16).ToTileCoordinates().Y * 16;
                    FoundTile = true;
                    NPC.hide = false;
                    break;
                }
            }
            if (!FoundTile)
            {
                NPC.active = false;
            }
            NPC.netUpdate = true;
        }

        if (NPC.ai[0] == 1 && !Grounded)
        {
            NPC.TargetClosest();
            NPC.behindTiles = true;

            bool TargetValidForShootingAt = NPC.HasValidTarget ? Main.player[NPC.target].Center.Y > NPC.Center.Y || Collision.CanHitLine(NPC.Center, 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) : false;

            NPC.rotation = MathHelper.SmoothStep(NPC.Center.DirectionTo(TargetValidForShootingAt ? Main.player[NPC.target].Center : NPC.Bottom).ToRotation() - MathHelper.PiOver2, NPC.rotation, 0.9f);

            NPC.ai[1]++;
            if(Main.expertMode)
                NPC.ai[1]+= 0.5f;
            if ((int)NPC.ai[1] % 120 == 0 && TargetValidForShootingAt)
            {
                //Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, NPC.Bottom.DirectionTo(Main.player[NPC.target].Center) * 12,ProjectileID.CursedFlameHostile,24,0);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2(0, 16).RotatedBy(NPC.rotation), new Vector2(0, 2).RotatedBy(NPC.rotation), ModContent.ProjectileType<DarkeningInk>(), 8, 0, ai0: NPC.target);
                SoundEngine.PlaySound(SoundID.Item64, NPC.position);
            }

            if (NPC.ai[0] != 0 && !Collision.SolidCollision(new Vector2(NPC.position.X, new Vector2(0, NPC.position.Y - 16).ToTileCoordinates().Y * 16), NPC.width, NPC.height))
            {
                NPC.noGravity = false;
                NPC.aiStyle = 3;
                Grounded = true;
                NPC.netUpdate = true;
            }
        }
        else
        {
            NPC.rotation = NPC.velocity.X * -0.1f;
            if(NPC.gfxOffY != 2)
            {
                NPC.gfxOffY--;
                NPC.height+= 3;
            }
        }
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.BloodshotEye")),
        });

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ItemID.RottenChunk, 66,1,2));
    }
    public override void FindFrame(int frameHeight)
    {
        NPC.frameCounter += !Grounded ? 1 : Math.Abs(NPC.velocity.X * 1.3f);
        if(NPC.frameCounter > 10)
        {
            NPC.frame.Y += !Grounded ? frameHeight : frameHeight * NPC.direction;
            NPC.frameCounter = 0;
        }
        if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
            NPC.frame.Y = 0;
        else if (NPC.frame.Y < 0)
            NPC.frame.Y = frameHeight * (Main.npcFrameCount[NPC.type] - 1);
    }

    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0)
        {
            for (int i = 0; i < 35; i++)
            {
                Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(1, 3), 0, default, Main.rand.NextFloat(1f, 2f));
                d.velocity += NPC.velocity * 1f;

                Dust d2 = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Wraith, Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(1, 3), 0, default, 1f);
                d2.velocity += NPC.velocity * 1f;
            }
            if (Main.netMode != NetmodeID.Server)
            {
                for(int i = 0; i < 4; i++)
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("Shadlopod" + $"{i}").Type);
            }
        }
        else
        {
            for (int i = 0; i < (int)hit.Damage / 3; i++)
            {
                Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Blood, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 1), 0, default, Main.rand.NextFloat(1f, 1.2f));
                d.velocity += NPC.velocity * 1f;
            }
        }
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneCorrupt && !spawnInfo.Player.InPillarZone()
        ? 0.2f * AvalonGlobalNPC.ModSpawnRate
        : 0f;
}
