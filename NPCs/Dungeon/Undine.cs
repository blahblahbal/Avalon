using Avalon.Items.Banners;
using Avalon.Projectiles.Hostile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Dungeon;

public class Undine : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 6;
		Data.Sets.NPCSets.Watery[NPC.type] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Bleeding] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire3] = true;
	}

    public override void SetDefaults()
    {
		NPC.damage = 25;
        NPC.lifeMax = 225;
        NPC.width = 24;
        NPC.aiStyle = NPCAIStyleID.HoveringFighter;
		NPC.value = 145f;
        NPC.height = 40;
        NPC.knockBackResist = 0.35f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
		NPC.noGravity = true;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<UndineBanner>();
    }
	public override Color? GetAlpha(Color drawColor)
	{
		return drawColor with { A = 200 } * NPC.Opacity;
	}
	public override void AI()
	{
		NPC.spriteDirection = NPC.direction;
		NPC.rotation = NPC.velocity.X * 0.1f;
		Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Water);
		d.noGravity = true;
		//if (NPC.collideX)
		//	NPC.velocity.X = Math.Sign(NPC.velocity.X) * -2;
		//if (NPC.collideY)
		//	NPC.velocity.Y = Math.Sign(NPC.velocity.Y) * -1;

		NPC.ai[3]++;
		if (NPC.ai[3] > 240)
		{
			NPC.localAI[3] = 1;
			//NPC.SimpleFlyMovement(new Vector2(0, NPC.directionY), 0.03f);
			if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[3] % 6 == 0)
			{
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Top + Main.rand.NextVector2Square(-2,2) + new Vector2(4 * NPC.spriteDirection,-8), Vector2.UnitY.RotatedByRandom(0.6f) * Main.rand.NextFloat(-1, -5), ModContent.ProjectileType<UndineTear>(), 15, 2, -1);
			}
			NPC.velocity.X *= 0.8f;
			if (NPC.ai[3] > 440)
			{
				NPC.ai[3] = Main.rand.Next(-60, 60);
				NPC.netUpdate = true;
			}
		}
		else
		{
			//NPC.TargetClosest(false);
			//NPC.SimpleFlyMovement(new Vector2(NPC.direction * 3, NPC.directionY), 0.04f);
			NPC.localAI[3] = 0;
		}
		//bool shouldAcceptTop = false;
		//if (NPC.HasValidTarget)
		//{
		//	NPC.direction = -Math.Sign(NPC.Center.X - Main.player[NPC.target].Center.X);
		//	if (NPC.Center.Y > Main.player[NPC.target].Top.Y)
		//		shouldAcceptTop = true;
		//}

		//if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height + 16 * 2, shouldAcceptTop))
		//	NPC.directionY = -1;
		//else
		//	NPC.directionY = 1;
	}
	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter++;
		NPC.frame.Y = (int)((NPC.frameCounter / 6) % 3) * frameHeight;
		if (NPC.localAI[3] == 1)
		{
			NPC.frame.Y += frameHeight * 3;
		}
	}
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.Undine")),
        });
    public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneDungeon
        ? 0.3f : 0f;

    public override void HitEffect(NPC.HitInfo hit)
    {
	}
	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.Common(ItemID.GoldenKey, 65));
	}
}
