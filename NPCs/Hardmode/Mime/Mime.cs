using Terraria.GameContent.Bestiary;
using System;
using Avalon.Items.Accessories.Hardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace Avalon.NPCs.Hardmode.Mime;

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
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.Mime"))
        });
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
		if (NPC.position.Distance(Main.player[NPC.target].position) < 180f && NPC.velocity.Y == 0f && IsOnGround(NPC) && NPC.position.Y + NPC.height > Main.player[NPC.target].position.Y + Main.player[NPC.target].height)
		{
			Vector2 jump = new Vector2((MathF.Sqrt(Math.Abs(NPC.DirectionTo(Main.player[NPC.target].position).X) + 1f) - 1f) * NPC.direction * 2f, NPC.DirectionTo(Main.player[NPC.target].position).Y * 7.75f);
			jump *= MathHelper.Clamp(NPC.position.Distance(Main.player[NPC.target].position) / 60f, 0.6f, 1.1f);
			if (NPC.velocity.X > 0 && jump.X > 0 || NPC.velocity.X < 0 && jump.X < 0)
			{
				NPC.velocity += jump;
			}
		}
	}
	// Copied from the IsOnGround method in ClassExtensions.cs
	public static bool IsOnGround(NPC npc)
	{
		var tileX_1 = Main.tile[(int)(npc.position.X / 16f), (int)(npc.position.Y / 16f) + 3];
		var tileX_2 = Main.tile[(int)(npc.position.X / 16f) + 1, (int)(npc.position.Y / 16f) + 3];

		return tileX_1.HasTile && (Main.tileSolid[tileX_1.TileType] || Main.tileSolidTop[tileX_1.TileType]) && npc.velocity.Y == 0f ||
				tileX_2.HasTile && (Main.tileSolid[tileX_2.TileType] || Main.tileSolidTop[tileX_2.TileType]) && npc.velocity.Y == 0f;
	}
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
