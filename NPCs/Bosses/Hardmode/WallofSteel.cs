using System;
using System.Threading.Tasks;
using Avalon.Items.Material;
using Avalon.Items.Placeable.Trophy;
using Avalon.Items.Weapons.Magic;
using Avalon.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent;
using Terraria.DataStructures;
using ReLogic.Content;
using Terraria.GameContent.Bestiary;
using Avalon.Common;
using Avalon.Items.Weapons.Magic.Superhardmode;
using Avalon.Items.Weapons.Ranged.Superhardmode;
using System.IO;

namespace Avalon.NPCs.Bosses.Hardmode;

[AutoloadBossHead]
public class WallofSteel : ModNPC
{
    private static Asset<Texture2D> wosTexture;
    private static Asset<Texture2D> mechaHungryChainTexture;
	private int TopEyeHP;
	private int BottomEyeHP;
	private int TopEyeMaxHP;
	private int BottomEyeMaxHP;
	public override string Texture => "Avalon/NPCs/Bosses/Hardmode/WallofSteelWall";
	public override void Load()
    {
        wosTexture = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>("Assets/Textures/WallofSteel");
        mechaHungryChainTexture = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>("Assets/Textures/MechaHungryChain");
    }

    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 1;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.CursedInferno] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Ichor] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Venom] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frozen] = true;
		var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
        { // Influences how the NPC looks in the Bestiary
            CustomTexturePath = "Avalon/Assets/Bestiary/WallofSteel",
            Position = new Vector2(7f, 0f)
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
    }

    public override void SetDefaults()
    {
        NPC.width = 200;
        NPC.height = 580;
		NPC.boss = NPC.noTileCollide = NPC.noGravity = NPC.behindTiles = true;
        NPC.npcSlots = 100f;
        NPC.damage = 150;
        NPC.lifeMax = 152000;
        NPC.timeLeft = 750000;
        NPC.defense = 55;
        NPC.aiStyle = -1;
        NPC.value = Item.buyPrice(0, 10);
        NPC.knockBackResist = 0;
        NPC.scale = 1f;
        NPC.HitSound = SoundID.NPCHit4;
        NPC.DeathSound = SoundID.NPCDeath14;
        //NPC.BossBar = ModContent.GetInstance<BossBars.WallofSteelBossBar>();
        Music = ExxoAvalonOrigins.MusicMod == null ? MusicID.Boss2 : MusicID.Boss2; // MusicLoader.GetMusicSlot(Avalon.MusicMod, "Sounds/Music/WallofSteel");
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.WallOfSteel"))
        });
    }

    public override void BossLoot(ref string name, ref int potionType)
    {
        potionType = ItemID.SuperHealingPotion;
    }
	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(TopEyeHP);
		writer.Write(TopEyeMaxHP);
		writer.Write(BottomEyeHP);
		writer.Write(BottomEyeMaxHP);
	}
	public override void ReceiveExtraAI(BinaryReader reader)
	{
		TopEyeHP = reader.ReadByte();
		TopEyeMaxHP = reader.ReadByte();
		BottomEyeHP = reader.ReadByte();
		BottomEyeMaxHP = reader.ReadByte();
	}
	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		SpriteEffects effects = SpriteEffects.None;
		if (NPC.spriteDirection == 1)
		{
			effects = SpriteEffects.FlipHorizontally;
		}

		float num66 = Main.NPCAddHeight(NPC);
		var vector13 = new Vector2(TextureAssets.Npc[NPC.type].Width() / 2,
			TextureAssets.Npc[NPC.type].Height() / Main.npcFrameCount[NPC.type] / 2);
		float glow = (float)Math.Sin(Main.timeForVisualEffects * 0.1f) * 0.5f + 0.5f;
		for (int i = 0; i < 4; i++)
		{
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture).Value,
				new Vector2(NPC.position.X - screenPos.X + (NPC.width / 2) - (TextureAssets.Npc[NPC.type].Width() * NPC.scale / 2f) + (vector13.X * NPC.scale), NPC.position.Y - Main.screenPosition.Y + NPC.height - (TextureAssets.Npc[NPC.type].Height() * NPC.scale / Main.npcFrameCount[NPC.type]) + 4f + (vector13.Y * NPC.scale) + num66) + new Vector2(0, 2 * glow).RotatedBy(i * MathHelper.PiOver2)
				, NPC.frame, default, NPC.rotation, vector13,
				NPC.scale, effects, 0f);
		}
	}
	public override Color? GetAlpha(Color lightColor)
    {
		return null; // Color.Lerp(Color.White, Lighting.GetColor((int)NPC.position.X, (int)NPC.position.Y), 0.5f);
    }

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
        NPC.lifeMax = (int)(NPC.lifeMax * 0.85f * bossAdjustment);
        NPC.damage = (int)(NPC.damage * 0.65f);
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 v, Color drawColor)
    {
        if (AvalonWorld.WallOfSteel >= 0)
        {
            for (int i = 0; i < 255; i++)
            {
                if (Main.player[i].gross && Main.player[i].active && Main.player[i].tongued && !Main.player[i].dead)
                {
                    float num = Main.npc[AvalonWorld.WallOfSteel].position.X + Main.npc[AvalonWorld.WallOfSteel].width / 2;
                    float num2 = Main.npc[AvalonWorld.WallOfSteel].position.Y + Main.npc[AvalonWorld.WallOfSteel].height / 2;
                    var vector = new Vector2(Main.player[i].position.X + Main.player[i].width * 0.5f, Main.player[i].position.Y + Main.player[i].height * 0.5f);
                    float num3 = num - vector.X;
                    float num4 = num2 - vector.Y;
                    float rotation = (float)Math.Atan2(num4, num3) - 1.57f;
                    bool flag = true;
                    while (flag)
                    {
                        float num5 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
                        if (num5 < 40f)
                        {
                            flag = false;
                        }
                        else
                        {
                            num5 = TextureAssets.Chain38.Value.Height / num5;
                            num3 *= num5;
                            num4 *= num5;
                            vector.X += num3;
                            vector.Y += num4;
                            num3 = num - vector.X;
                            num4 = num2 - vector.Y;
                            Color color = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16f));
                            spriteBatch.Draw(TextureAssets.Chain8.Value, new Vector2(vector.X - Main.screenPosition.X, vector.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, TextureAssets.Chain8.Value.Width, TextureAssets.Chain8.Value.Height)), color, rotation, new Vector2(TextureAssets.Chain8.Value.Width * 0.5f, TextureAssets.Chain8.Value.Height * 0.5f), 1f, SpriteEffects.None, 0f);
                        }
                    }
                }
            }
            for (int j = 0; j < 200; j++)
            {
                if (Main.npc[j].active && (Main.npc[j].type == ModContent.NPCType<MechanicalHungry>() || Main.npc[j].type == ModContent.NPCType<MechanicalHungry2>()))
                {
                    float num6 = Main.npc[AvalonWorld.WallOfSteel].position.X + Main.npc[AvalonWorld.WallOfSteel].width / 2;
                    float num7 = Main.npc[AvalonWorld.WallOfSteel].position.Y;
                    float num8 = AvalonWorld.WallOfSteelB - AvalonWorld.WallOfSteelT;
                    bool flag2 = false;
                    if (Main.npc[j].frameCounter > 7.0)
                    {
                        flag2 = true;
                    }
                    num7 = AvalonWorld.WallOfSteelT + num8 * Main.npc[j].ai[0];
                    var vector2 = new Vector2(Main.npc[j].position.X + Main.npc[j].width / 2, Main.npc[j].position.Y + Main.npc[j].height / 2);
                    float num9 = num6 - vector2.X;
                    float num10 = num7 - vector2.Y;
                    float rotation2 = (float)Math.Atan2(num10, num9) - 1.57f;
                    bool flag3 = true;
                    while (flag3)
                    {
                        SpriteEffects effects = SpriteEffects.None;
                        if (flag2)
                        {
                            effects = SpriteEffects.FlipHorizontally;
                            flag2 = false;
                        }
                        else
                        {
                            flag2 = true;
                        }
                        int height = 28;
                        float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
                        if (num11 < 40f)
                        {
                            height = (int)num11 - 40 + 28;
                            flag3 = false;
                        }
                        num11 = 28f / num11;
                        num9 *= num11;
                        num10 *= num11;
                        vector2.X += num9;
                        vector2.Y += num10;
                        num9 = num6 - vector2.X;
                        num10 = num7 - vector2.Y;
                        Color color2 = Lighting.GetColor((int)vector2.X / 16, (int)(vector2.Y / 16f));
                        Main.EntitySpriteDraw(mechaHungryChainTexture.Value, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, mechaHungryChainTexture.Value.Width, height)), color2, rotation2, new Vector2(mechaHungryChainTexture.Value.Width * 0.5f, mechaHungryChainTexture.Value.Height * 0.5f), 1f, effects, 0);
                    }
                }
            }
            int heightOfPartToTile = 120;
            float num13 = AvalonWorld.WallOfSteelT;
            float screenYPos = AvalonWorld.WallOfSteelB;
            screenYPos = Main.screenPosition.Y + Main.screenHeight;
            float num15 = (int)((num13 - Main.screenPosition.Y) / heightOfPartToTile) + 1;
            num15 *= heightOfPartToTile;
            if (num15 > 0f)
            {
                num13 -= num15;
            }
            float num16 = num13;
            float wosPosX = Main.npc[AvalonWorld.WallOfSteel].position.X + 20;
            float num18 = screenYPos - num13;
            bool flag4 = true;
            SpriteEffects effects2 = SpriteEffects.None;
            if (Main.npc[AvalonWorld.WallOfSteel].spriteDirection == -1)
            {
                wosPosX += 50; // going to the left
            }
            if (Main.npc[AvalonWorld.WallOfSteel].spriteDirection == 1)
            {
                effects2 = SpriteEffects.FlipHorizontally;
            }
            if (Main.npc[AvalonWorld.WallOfSteel].direction > 0)
            {
                wosPosX -= 80f; // going to the right
            }
            int num19 = 0;
            if (!Main.gamePaused)
            {
                AvalonWorld.WallOfSteelF++;
            }
            if (AvalonWorld.WallOfSteelF > 12)
            {
                num19 = 240;
                if (AvalonWorld.WallOfSteelF > 17)
                {
                    AvalonWorld.WallOfSteelF = 0;
                }
            }
            else if (AvalonWorld.WallOfSteelF > 6)
            {
                num19 = 120;
            }
            while (flag4)
            {
                num18 = screenYPos - num16;
                if (num18 > heightOfPartToTile)
                {
                    num18 = heightOfPartToTile;
                }
                bool flag5 = true;
                int num20 = 0;
                while (flag5)
                {
                    int x = (int)(wosPosX + wosTexture.Value.Width / 2) / 16;
                    int y = (int)(num16 + num20) / 16;
                    Main.spriteBatch.Draw(wosTexture.Value, new Vector2(wosPosX - Main.screenPosition.X, num16 + num20 - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, num19 + num20, wosTexture.Value.Width, 16)), Lighting.GetColor(x, y), 0f, default(Vector2), 1f, effects2, 0f);
                    num20 += 16;
                    if (num20 >= num18)
                    {
                        flag5 = false;
                    }
                }
                num16 += heightOfPartToTile;
                if (num16 >= screenYPos)
                {
                    flag4 = false;
                }
            }
        }
        return true;
    }

    public override void AI()
    {
		if (NPC.AnyNPCs(ModContent.NPCType<WallofSteelMouthEye>()) ||
			NPC.AnyNPCs(ModContent.NPCType<WallofSteelLaserEye>()))
		{
			NPC.dontTakeDamage = true;
		}
		else NPC.dontTakeDamage = false;
        bool expert = Main.expertMode;
        if (NPC.position.X < 160f || NPC.position.X > (Main.maxTilesX - 10) * 16)
        {
            NPC.active = false;
        }
        if (NPC.localAI[0] == 0f)
        {
            NPC.localAI[0] = 1f;
            AvalonWorld.WallOfSteelB = -1;
            AvalonWorld.WallOfSteelT = -1;
        }
        NPC.localAI[3] += 1f;
        if (NPC.localAI[3] >= 600 + Main.rand.Next(1000))
        {
            NPC.localAI[3] = -Main.rand.Next(200);
            //Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 10);
        }
        AvalonWorld.WallOfSteel = NPC.whoAmI;
        int tilePosX = (int)(NPC.position.X / 16f);
        int tilePosXwidth = (int)((NPC.position.X + NPC.width) / 16f);
        int tilePosYCenter = (int)((NPC.position.Y + NPC.height / 2) / 16f);
        int num447 = tilePosYCenter + 7;
        num447 += 4;
        if (AvalonWorld.WallOfSteelB == -1)
        {
            AvalonWorld.WallOfSteelB = num447 * 16;
        }
        else if (AvalonWorld.WallOfSteelB > num447 * 16)
        {
            AvalonWorld.WallOfSteelB--;
            if (AvalonWorld.WallOfSteelB < num447 * 16)
            {
                AvalonWorld.WallOfSteelB = num447 * 16;
            }
        }
        else if (AvalonWorld.WallOfSteelB < num447 * 16)
        {
            AvalonWorld.WallOfSteelB++;
            if (AvalonWorld.WallOfSteelB > num447 * 16)
            {
                AvalonWorld.WallOfSteelB = num447 * 16;
            }
        }
        num447 = tilePosYCenter - 7;
        num447 -= 4;
        if (AvalonWorld.WallOfSteelT == -1)
        {
            AvalonWorld.WallOfSteelT = num447 * 16;
        }
        else if (AvalonWorld.WallOfSteelT > num447 * 16)
        {
            AvalonWorld.WallOfSteelT--;
            if (AvalonWorld.WallOfSteelT < num447 * 16)
            {
                AvalonWorld.WallOfSteelT = num447 * 16;
            }
        }
        else if (AvalonWorld.WallOfSteelT < num447 * 16)
        {
            AvalonWorld.WallOfSteelT++;
            if (AvalonWorld.WallOfSteelT > num447 * 16)
            {
                AvalonWorld.WallOfSteelT = num447 * 16;
            }
        }
        float num450 = (AvalonWorld.WallOfSteelB + AvalonWorld.WallOfSteelT) / 2 - NPC.height / 2;
        if (NPC.Center.Y > Main.player[NPC.target].Center.Y)
        {
            if (NPC.velocity.Y > 0)
            {
                NPC.velocity.Y *= 0.98f;
            }
            NPC.velocity.Y -= 0.02f;
            if (NPC.velocity.Y < -2.2f)
            {
                NPC.velocity.Y = -2.2f;
            }
        }
        if (NPC.Center.Y < Main.player[NPC.target].Center.Y)
        {
            if (NPC.velocity.Y < 0)
            {
                NPC.velocity.Y *= 0.98f;
            }
            NPC.velocity.Y += 0.02f;
            if (NPC.velocity.Y > 2.2f)
            {
                NPC.velocity.Y = 2.2f;
            }
        }
        //npc.velocity.Y = 0f;
        //npc.position.Y = num450;
        if (NPC.position.Y / 16 < Main.maxTilesY - 200)
        {
            NPC.position.Y = (Main.maxTilesY - 200) * 16;
        }


		if (ClassExtensions.FindATypeOfNPC(ModContent.NPCType<WallofSteelMouthEye>()) == -1)
		{
			TopEyeHP = 0;
		}
		if (ClassExtensions.FindATypeOfNPC(ModContent.NPCType<WallofSteelLaserEye>()) == -1)
		{
			BottomEyeHP = 0;
		}

		int totalHP = TopEyeHP + BottomEyeHP + NPC.life;

		int totalMaxHP = TopEyeMaxHP + BottomEyeMaxHP + NPC.lifeMax;

		float num451 = 2.5f;
        if (totalHP < totalMaxHP * 0.75)
        {
            num451 += 0.25f;
        }
        if (totalHP < totalMaxHP * 0.5)
        {
            num451 += 0.4f;
        }
        if (totalHP < totalMaxHP * 0.25)
        {
            num451 += 0.5f;
        }
        if (totalHP < totalMaxHP * 0.1)
        {
            num451 += 0.6f;
        }
        if (totalHP < totalMaxHP * 0.66 && Main.expertMode)
        {
            num451 += 0.3f;
        }
        if (totalHP < totalMaxHP * 0.33 && Main.expertMode)
        {
            num451 += 0.3f;
        }
        if (totalHP < totalMaxHP * 0.05 && Main.expertMode)
        {
            num451 += 0.6f;
        }
        if (totalHP < totalMaxHP * 0.035 && Main.expertMode)
        {
            num451 += 0.6f;
        }
        if (totalHP < totalMaxHP * 0.025 && Main.expertMode)
        {
            num451 += 0.6f;
        }
        if (NPC.velocity.X == 0f)
        {
            NPC.TargetClosest(true);
            NPC.velocity.X = NPC.direction;
        }
        if (NPC.velocity.X < 0f)
        {
            NPC.velocity.X = -num451;
            NPC.direction = -1;
        }
        else
        {
            NPC.velocity.X = num451;
            NPC.direction = 1;
        }
        {
            NPC.ai[3]++;
            //if (NPC.ai[3] == 1)
            //{
            //    NPC.defense = 0;
            //    SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/LaserCharge"), NPC.Center);
            //}
            //if (NPC.ai[3] >= 60 && NPC.ai[3] <= 90)
            //{
            //    if (NPC.ai[3] == 60)
            //    {
            //        SoundEngine.PlaySound(SoundID.Item33, NPC.Center);
            //        NPC.ai[1] = (NPC.velocity.X < 0 ? NPC.position.X : NPC.position.X + NPC.width);
            //        NPC.ai[2] = NPC.Center.Y;
            //        NPC.localAI[1] = NPC.velocity.X;
            //        NPC.localAI[2] = NPC.velocity.Y;
            //    }
            //    int t = ModContent.ProjectileType<Projectiles.Hostile.WallofSteelLaser>(); // middle
            //    if (NPC.ai[3] == 60)
            //        t = ModContent.ProjectileType<Projectiles.Hostile.WallofSteelLaserStart>(); // start
            //    if (NPC.ai[3] == 90)
            //        t = ModContent.ProjectileType<Projectiles.Hostile.WallofSteelLaserEnd>(); // end
            //    if (NPC.ai[3] % 3 == 0)
            //    {
            //        int wide = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.ai[1], NPC.ai[2], NPC.localAI[1], NPC.localAI[2], t, 100, 4f);
            //        if (NPC.velocity.X > 0)
            //        {
            //            Main.projectile[wide].velocity = Vector2.Normalize(new Vector2(NPC.ai[1], NPC.ai[2]) - new Vector2(NPC.ai[1] - 100, NPC.ai[2])) * 20f;
            //        }
            //        else if (NPC.velocity.X < 0)
            //        {
            //            Main.projectile[wide].velocity = Vector2.Normalize(new Vector2(NPC.ai[1] - 100, NPC.ai[2]) - new Vector2(NPC.ai[1], NPC.ai[2])) * 20f;
            //        }

            //        Main.projectile[wide].tileCollide = false;
            //        if (Main.netMode != NetmodeID.SinglePlayer)
            //        {
            //            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, wide);
            //        }
            //    }
            //}
        }
        NPC.spriteDirection = NPC.direction;
        if (NPC.localAI[0] == 1f && Main.netMode != NetmodeID.MultiplayerClient)
        {
            NPC.localAI[0] = 2f;
            for (int num456 = 0; num456 < 11; num456++)
            {
                int hungry = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)num450, ModContent.NPCType<MechanicalHungry>(), NPC.whoAmI);
                Main.npc[hungry].ai[0] = num456 * 0.1f - 0.05f;
            }
            return;
		}
		//NPC.velocity.X = 0;
    }

	public override void HitEffect(NPC.HitInfo hit)
	{
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore1").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore2").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore3").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore3").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore4").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore5").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore6").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore6").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore7").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore8").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore9").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore10").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore11").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore12").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore13").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore14").Type, NPC.scale);
        }
    }
	public override void OnSpawn(IEntitySource source)
	{
		int topEye = NPC.NewNPC(source, (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<WallofSteelMouthEye>());
		int bottomEye = NPC.NewNPC(source, (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<WallofSteelLaserEye>());

		TopEyeMaxHP = Main.npc[topEye].lifeMax;
		TopEyeHP = Main.npc[topEye].life;
		BottomEyeMaxHP = Main.npc[bottomEye].lifeMax;
		BottomEyeHP = Main.npc[bottomEye].life;
	}
	public override void OnKill()
    {
        AvalonWorld.WallOfSteel = -1;
		/*if (!ModContent.GetInstance<AvalonWorld>().SuperHardmode && Main.hardMode)
        {
            if (!Main.expertMode)
                NPC.DropItemInstanced(NPC.position, new Vector2(NPC.width, NPC.height), ModContent.ItemType<Items.Consumables.MechanicalHeart>());
            Task.Run(() => ModContent.GetInstance<AvalonWorld>().InitiateSuperHardMode());
        }*/
		if (Main.netMode != NetmodeID.MultiplayerClient)
        {
            int num22 = (int)(NPC.position.X + (NPC.width / 2)) / 16;
            int num23 = (int)(NPC.position.Y + (NPC.height / 2)) / 16;
            int num24 = NPC.width / 4 / 16 + 1;
            for (int k = num22 - num24; k <= num22 + num24; k++)
            {
                for (int l = num23 - num24; l <= num23 + num24; l++)
                {
                    if ((k == num22 - num24 || k == num22 + num24 || l == num23 - num24 || l == num23 + num24) && !Main.tile[k, l].HasTile)
                    {
                        Main.tile[k, l].TileType = (ushort)ModContent.TileType<Tiles.ImperviousBrick>();
                        Main.tile[k, l].Active(true);
                    }
                    Main.tile[k, l].LiquidAmount = 0;
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendTileSquare(-1, k, l, 1);
                    }
                    else
                    {
                        WorldGen.SquareTileFrame(k, l, true);
                    }
                }
            }
        }
    }
	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
		notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, [ModContent.ItemType<FleshBoiler>(), ModContent.ItemType<MagicCleaver>(), ModContent.ItemType<Items.Accessories.Superhardmode.BubbleBoost>()]));
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<Items.BossBags.WallofSteelBossBag>()));
		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WallofSteelTrophy>(), 10));
		npcLoot.Add(notExpertRule);
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<SoulofBlight>(), 1, 40, 56));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<HellsteelPlate>(), 1, 20, 31));
	}
}
