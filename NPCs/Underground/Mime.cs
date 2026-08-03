using Terraria.GameContent.Bestiary;
using System;
using Avalon.Items.Accessories.Hardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace Avalon.NPCs.Underground;

public class Mime : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 3;
    }

    public override void SetDefaults()
    {
        NPC.damage = 75;
        NPC.noTileCollide = false;
        NPC.lifeMax = 630;
        NPC.defense = 46;
        NPC.noGravity = false;
        NPC.width = 18;
        NPC.aiStyle = 3;
        NPC.value = 1500f;
        NPC.height = 40;
        NPC.knockBackResist = 0.15f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<Items.Banners.MimeBanner>();
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(
		[
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.Mime"))
        ]);
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ConfusionTalisman>(), 8));
		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ManaCompromise>(), 100));
	}
	public override void PostAI()
	{
		if (MathF.Abs(NPC.velocity.X) < 2f)
		{
			if (NPC.velocity.X > 0 && NPC.direction == 1)
			{
				NPC.velocity.X += NPC.velocity.X * 0.3f;
			}
			if (NPC.velocity.X < 0 && NPC.direction == -1)
			{
				NPC.velocity.X += NPC.velocity.X * 0.3f;
			}
		}
		if (NPC.WithinRange(Main.player[NPC.target].Center, 180f) && IsOnGround() && NPC.BottomLeft.Y > Main.player[NPC.target].BottomLeft.Y)
		{
			Vector2 jump = new((MathF.Sqrt(Math.Abs(NPC.DirectionTo(Main.player[NPC.target].Center).X) + 1f) - 1f) * NPC.direction * 2f, NPC.DirectionTo(Main.player[NPC.target].Center).Y * 7.75f);
			jump *= MathHelper.Clamp(NPC.Distance(Main.player[NPC.target].Center) / 60f, 0.6f, 1.1f);
			if (NPC.velocity.X > 0 && jump.X > 0 || NPC.velocity.X < 0 && jump.X < 0)
			{
				NPC.velocity += jump;
			}
		}
	}
	public bool IsOnGround() => NPC.velocity.Y == 0f && NPC.collideY;
	public override void FindFrame(int frameHeight)
	{
		if (NPC.velocity.Y == 0f)
		{
			if (NPC.direction == 1)
			{
				NPC.spriteDirection = 1;
			}
			if (NPC.direction == -1)
			{
				NPC.spriteDirection = -1;
			}
		}
		if (NPC.velocity.Y != 0f || NPC.direction == -1 && NPC.velocity.X > 0f || NPC.direction == 1 && NPC.velocity.X < 0f)
		{
			NPC.frameCounter = 0.0;
			NPC.frame.Y = frameHeight * 2;
		}
		else if (NPC.velocity.X == 0f)
		{
			NPC.frameCounter = 0.0;
			NPC.frame.Y = 0;
		}
		else
		{
			NPC.frameCounter += Math.Abs(NPC.velocity.X);
			if (NPC.frameCounter < 12.0)
			{
				NPC.frame.Y = 0;
			}
			else if (NPC.frameCounter < 24.0)
			{
				NPC.frame.Y = frameHeight;
			}
			else if (NPC.frameCounter < 36.0)
			{
				NPC.frame.Y = frameHeight * 2;
			}
			else if (NPC.frameCounter < 48.0)
			{
				NPC.frame.Y = frameHeight;
			}
			else
			{
				NPC.frameCounter = 0.0;
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
		{
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MimeHead").Type, 0.9f);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MimeArm").Type, 0.9f);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MimeArm").Type, 0.9f);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MimeLeg").Type, 0.9f);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MimeLeg").Type, 0.9f);
		}
	}
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        return spawnInfo.Player.ZoneRockLayerHeight && spawnInfo.Player.ZoneMarble && Main.hardMode ? 0.14f : 0f;
    }
}
