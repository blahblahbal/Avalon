using Avalon.Particles;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Forest;

public class AngryFairy : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[NPC.type] = 4;
		Data.Sets.NPCSets.Flyer[NPC.type] = true;
	}
	public override void SetDefaults()
	{
		NPC.damage = 5;
		NPC.lifeMax = 25;
		NPC.defense = 0;
		NPC.aiStyle = NPCAIStyleID.HoveringFighter;
		NPC.value = 10;
		NPC.height = 16;
		NPC.width = 16;
		NPC.HitSound = SoundID.NPCHit5 with { pitch = 0.3f };
		NPC.DeathSound = SoundID.NPCDeath7 with { pitch = 0.3f }; 
		NPC.noGravity = true;
		Banner = NPC.type;
		BannerItem = ModContent.ItemType<Items.Banners.AngryFairyBanner>();
	}
	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
			new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.AngryFairy"))
		});
	}
	public override void AI()
	{
		NPC.spriteDirection = -NPC.direction;
		NPC.rotation = NPC.velocity.X * 0.1f;
		NPC.velocity.X *= 0.93f;

		if (Main.rand.NextBool(20))
		{
			Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.LifeDrain);
			d.noGravity = true;
			d.velocity *= 0.3f;
			d.velocity.X += NPC.velocity.X;

			d.noGravity = !Main.rand.NextBool(3);
			if (!d.noGravity)
				d.velocity *= 0.2f;
		}

		if (Main.rand.NextBool(40))
			SoundEngine.PlaySound(SoundID.Pixie with { pitch = -0.3f, volume = 0.75f}, NPC.position);
	}
	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		return spawnInfo.Player.ZoneForest ? ModContent.GetInstance<BiomeTileCounts>().Trees > 170 ? 0.025f : 0.1f : 0f;
	}
	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter++;
		if(NPC.frameCounter > 3)
		{
			NPC.frameCounter = 0;
			NPC.frame.Y += frameHeight;
			if (NPC.frame.Y > frameHeight * 3)
				NPC.frame.Y = 0;
		}
	}
	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life <= 0)
		{
			for (int i = 0; i < 2; i++)
			{
				var p = VanillaParticles.RequestPrettySparkleParticle();
				p.ColorTint = Color.Red;
				p.TimeToLive = Main.rand.Next(15, 30);
				p.Scale = new Vector2(3, 0.75f);
				p.Rotation = Main.rand.NextFloatDirection();
				p.LocalPosition = NPC.Center;
				Main.ParticleSystem_World_OverPlayers.Add(p);
			}

			for (int i = 0; i < 30; i++)
			{
				Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.LifeDrain);
				d.noGravity = !Main.rand.NextBool(3);
				d.velocity += Main.rand.NextVector2Circular(5, 5);
				if (!d.noGravity)
					d.velocity *= 0.2f;
			}
		}
		else
		{
			for (int i = 0; i < 7; i++)
			{
				Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.LifeDrain);
				d.noGravity = true;
				d.velocity += NPC.velocity.RotatedByRandom(1) * Main.rand.NextFloat();
			}
		}
	}
}
