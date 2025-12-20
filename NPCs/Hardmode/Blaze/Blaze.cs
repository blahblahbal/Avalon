using Avalon.Items.Banners;
using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Hardmode.Blaze;

public class Blaze : ModNPC
{
	private static Asset<Texture2D> glowTexture;
	public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 4;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire3] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.CursedInferno] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.ShadowFlame] = true;
        Data.Sets.NPCSets.Fiery[NPC.type] = true;
		glowTexture = ModContent.Request<Texture2D>(Texture + "Glow");
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.Blaze")),
        });

    public override void SetDefaults()
    {
        NPC.damage = 69;
        NPC.scale = 1.2f;
        NPC.noTileCollide = false;
        NPC.lifeMax = 460;
        NPC.lavaImmune = true;
        NPC.defense = 35;
        NPC.noGravity = true;
        NPC.aiStyle = -1;
        NPC.width = 24;
        NPC.value = Item.buyPrice(0, 0, 45);
        NPC.timeLeft = 750;
        NPC.knockBackResist = 0.3f;
        NPC.height = 32;
        //npc.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath6;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<BlazeBanner>();
        DrawOffsetY = 10;
    }

    public override void PostDraw(SpriteBatch spriteBatch, Vector2 vector, Color drawColor)
    {
        SpriteEffects effects = SpriteEffects.None;
        if (NPC.spriteDirection == 1)
        {
            effects = SpriteEffects.FlipHorizontally;
        }

        float num66 = Main.NPCAddHeight(NPC);
        var vector13 = new Vector2(TextureAssets.Npc[NPC.type].Width() / 2,
            TextureAssets.Npc[NPC.type].Height() / Main.npcFrameCount[NPC.type] / 2);
        float glow = ((float)Math.Sin(Main.timeForVisualEffects * 0.1f) * 0.5f + 0.5f);
        for (int i = 0; i < 4; i++)
        {
            Main.spriteBatch.Draw(glowTexture.Value,
                new Vector2(NPC.position.X - vector.X + (NPC.width / 2) - (TextureAssets.Npc[NPC.type].Width() * NPC.scale / 2f) + (vector13.X * NPC.scale), NPC.position.Y - Main.screenPosition.Y + NPC.height - (TextureAssets.Npc[NPC.type].Height() * NPC.scale / Main.npcFrameCount[NPC.type]) + 4f +(vector13.Y * NPC.scale) + num66) + new Vector2(0,2 * glow).RotatedBy(i * MathHelper.PiOver2)
                , NPC.frame, new Color(200, 200, 200, 0) * glow * 0.3f, NPC.rotation, vector13,
                NPC.scale, effects, 0f);
        }
    }

    public override void ModifyNPCLoot(NPCLoot loot)
    {
        loot.Add(ItemDropRule.Common(ItemID.Hellstone, 80, 1, 6));
        loot.Add(ItemDropRule.Common(ModContent.ItemType<BrimstoneBlock>(), 80, 8, 24));
    }

    public override void AI()
    {
        NPC.netUpdate = true;
        NPC.ai[1]++;
        NPC.TargetClosest();
        Player? player7 = Main.player[NPC.target];
        float num1221 = 0.022f;
        float num1222 = player7.position.X + (player7.width / 2);
        float num1223 = player7.position.Y + (player7.height / 2);
        var vector164 = new Vector2(NPC.position.X + (NPC.width * 0.5f), NPC.position.Y + (NPC.height * 0.5f));
        num1222 = (int)(num1222 / 8f) * 8;
        num1223 = (int)(num1223 / 8f) * 8;
        vector164.X = (int)(vector164.X / 8f) * 8;
        vector164.Y = (int)(vector164.Y / 8f) * 8;
        num1222 -= vector164.X;
        num1223 -= vector164.Y;
        if (player7.position.X + 300f < NPC.position.X || player7.position.X - 300f > NPC.position.X ||
            player7.position.Y + 300f < NPC.position.Y || player7.position.Y - 300f > NPC.position.Y)
        {
            if (player7.position.X + 300f < NPC.position.X)
            {
                if (NPC.velocity.X > -6f)
                {
                    NPC.velocity.X = NPC.velocity.X - 0.2f;
                }
            }
            else if (player7.position.X - 300f > NPC.position.X && NPC.velocity.X < 6f)
            {
                NPC.velocity.X = NPC.velocity.X + 0.2f;
            }

            if (player7.position.Y + 300f < NPC.position.Y)
            {
                if (NPC.velocity.Y > -6f)
                {
                    NPC.velocity.Y = NPC.velocity.Y - 0.2f;
                }
            }
            else if (player7.position.Y - 300f > NPC.position.Y && NPC.velocity.Y < 6f)
            {
                NPC.velocity.Y = NPC.velocity.Y + 0.2f;
            }
        }
        else
        {
            NPC.velocity.X = NPC.velocity.X * 0.95f;
            NPC.velocity.Y = NPC.velocity.Y * 0.95f;
            NPC.ai[2] += 1f;
            if (NPC.ai[2] == 60f)
            {
                NPC.ai[0] = Main.rand.Next(-7, 7);
                NPC.velocity.X = NPC.velocity.X + NPC.ai[0];
                NPC.velocity.Y = NPC.velocity.Y + NPC.ai[0];
                NPC.ai[2] = 0f;
            }
        }

        Vector2 vector165 = NPC.velocity;
        NPC.velocity = Collision.TileCollision(NPC.position, NPC.velocity, NPC.width, NPC.height);
        if (NPC.velocity.X != vector165.X)
        {
            NPC.velocity.X = -vector165.X;
        }

        if (NPC.velocity.Y != vector165.Y)
        {
            NPC.velocity.Y = -vector165.Y;
        }

        NPC.rotation = (float)Math.Atan2(num1223, num1222) - 1.57f;
        float num1224 = 0.7f;
        if (NPC.collideX)
        {
            NPC.netUpdate = true;
            NPC.velocity.X = NPC.oldVelocity.X * -num1224;
            if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 2f)
            {
                NPC.velocity.X = 2f;
            }

            if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -2f)
            {
                NPC.velocity.X = -2f;
            }
        }

        if (NPC.collideY)
        {
            NPC.netUpdate = true;
            NPC.velocity.Y = NPC.oldVelocity.Y * -num1224;
            if (NPC.velocity.Y > 0f && NPC.velocity.Y < 1.5)
            {
                NPC.velocity.Y = 2f;
            }

            if (NPC.velocity.Y < 0f && NPC.velocity.Y > -1.5)
            {
                NPC.velocity.Y = -2f;
            }
        }

        if (Main.rand.NextBool(20))
        {
            int num1225 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + (NPC.height * 0.25f)), NPC.width,
                (int)(NPC.height * 0.5f), DustID.Torch, NPC.velocity.X, 2f, 75, NPC.color, NPC.scale);
            Main.dust[num1225].velocity.X *= 0.5f;
            Main.dust[num1225].velocity.Y *= 0.1f;
            Main.dust[num1225].customData = 0;
        }

        if (Main.rand.NextBool(40))
        {
            int num1226 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + (NPC.height * 0.25f)), NPC.width,
                (int)(NPC.height * 0.5f), DustID.Pixie, NPC.velocity.X, 2f);
            Main.dust[num1226].velocity.X *= 0.5f;
            Main.dust[num1226].velocity.Y *= 0.1f;
        }

        if (NPC.wet && !NPC.lavaWet) {
            NPC.StrikeNPC(new NPC.HitInfo { Damage = 50, Knockback = 0f, HitDirection = 0});
        }

        if (NPC.localAI[0] is > 130 and < 180)
        {
            float rotation = Main.rand.NextFloat(0,MathHelper.TwoPi);
            Dust d = Dust.NewDustPerfect(NPC.Center + new Vector2(30,0).RotatedBy(rotation), DustID.Torch, new Vector2(-3,0).RotatedBy(rotation));
            d.noGravity = true;
            d.velocity += NPC.velocity;
            d.position += NPC.velocity;
            d.scale *= 2;

        }

        if (Main.netMode != NetmodeID.MultiplayerClient && !Main.player[NPC.target].dead)
        {
            if (NPC.justHit)
            {
                NPC.localAI[0] = 0f;
            }

            NPC.localAI[0] += 1f;
            if (NPC.localAI[0] is 180 or 200 or 220)
            {
                if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position,
                        Main.player[NPC.target].width, Main.player[NPC.target].height))
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)(NPC.position.X + (NPC.width / 2) + NPC.velocity.X),
                        (int)(NPC.position.Y + (NPC.height / 2) + NPC.velocity.Y), ModContent.NPCType<BlazeOrb>());
                }
                NPC.localAI[1] = 0f;
                if (NPC.localAI[0] == 220)
                {
                    NPC.localAI[0] = 0f;
                }
            }
        }

        if (!Main.dayTime || !Main.player[NPC.target].dead)
        {
            return;
        }

        NPC.velocity.Y = NPC.velocity.Y - (num1221 * 2f);
        if (NPC.timeLeft > 10)
        {
            NPC.timeLeft = 10;
        }

        if (((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f) || (NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f) ||
             (NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f) || (NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f)) &&
            !NPC.justHit)
        {
            NPC.netUpdate = true;
        }
    }

    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life > 0)
        {

            SoundEngine.PlaySound(SoundID.NPCHit37 with { Pitch = -0.5f }, NPC.Center);
            var rectangle = new Rectangle((int)NPC.position.X, (int)(NPC.position.Y + ((NPC.height - NPC.width) / 2)),
                NPC.width, NPC.width);
            int num8 = 50;
            for (int i = 1; i <= num8; i++)
            {
                if (Main.rand.NextBool(3))
                {
                    int num10 = Dust.NewDust(NPC.position, rectangle.Width, rectangle.Height, DustID.Torch, 0f, 0f, 100,
                    default, 2f);
                    Main.dust[num10].noGravity = true;
                    Main.dust[num10].velocity = Main.rand.NextVector2Circular(6f, 6f);
                }
            }

            for (int j = 1; j <= num8; j++)
            {
                if (Main.rand.NextBool(2))
                {
                    int num11 = Dust.NewDust(NPC.position, rectangle.Width, rectangle.Height, DustID.Wraith, 0f, 0f, 100, default, 1.4f);
                    Main.dust[num11].noGravity = true;
                    Main.dust[num11].velocity = Main.rand.NextVector2Circular(8f, 8f);
                }
            }
        }
    }

    public override void OnKill()
    {
        int num12 = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f,
            ProjectileID.Grenade, 0, 0f, NPC.target);
        Main.projectile[num12].Kill();
    }

    public override void FindFrame(int frameHeight)
    {
        if (NPC.velocity.X > 0f)
        {
            NPC.spriteDirection = 1;
        }

        if (NPC.velocity.X < 0f)
        {
            NPC.spriteDirection = -1;
        }

        NPC.rotation = NPC.velocity.X * 0.1f;
        if (NPC.type == NPCID.Bee || NPC.type == NPCID.BeeSmall)
        {
            NPC.frameCounter += 1.0;
            NPC.rotation = NPC.velocity.X * 0.2f;
        }

        NPC.frameCounter += 1.0;
        if (NPC.frameCounter >= 6.0)
        {
            NPC.frame.Y = NPC.frame.Y + frameHeight;
            NPC.frameCounter = 0.0;
        }

        if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
        {
            NPC.frame.Y = 0;
        }
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneUnderworldHeight && Main.hardMode
        ? 0.1f : 0f;
}
