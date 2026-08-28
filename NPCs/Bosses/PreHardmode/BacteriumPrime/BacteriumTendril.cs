using Avalon.Core;
using Avalon.Items.Material;
using Avalon.Items.Material.Ores;
using Avalon.Projectiles.Hostile.BacteriumPrime;
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
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.PreHardmode.BacteriumPrime;

public class BacteriumTendril : ModNPC
{
	private static SoundStyle _chompSound = new SoundStyle("Avalon/Sounds/NPC/Chomp_", 4) { PitchVariance = 0.4f, MaxInstances = 10 };
	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
			Hide = true // Hides this NPC from the bestiary
		};
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
	}
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[Type] = 4;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(new CommonDrop(ModContent.ItemType<Booger>(), 2, 2, 5, 3));
		npcLoot.Add(new CommonDrop(ModContent.ItemType<BacciliteOre>(), 2, 5, 12, 3));
		npcLoot.Add(new CommonDrop(ItemID.Heart, 2));
	}
	public override void SetDefaults()
	{
		NPC.damage = 27;
		NPC.noTileCollide = true;
		NPC.lifeMax = 500;
		NPC.noGravity = true;
		NPC.width = NPC.height = 30;
		NPC.aiStyle = -1;
		NPC.npcSlots = 0;
		NPC.HitSound = SoundID.NPCHit25;
		NPC.DeathSound = SoundID.NPCDeath12;
		NPC.knockBackResist = 0.5f;
		NPC.timeLeft = 200000;
		NPC.netAlways = true;
		NPC.dontCountMe = true; // don't count with radar
	}
	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		NPC.lifeMax = (int)(NPC.lifeMax * balance * bossAdjustment * 0.5f);
	}
	private NPC Owner { get => Main.npc[(int)NPC.ai[3]]; }
	private Player Target { get => Main.player[NPC.target]; }
	private Vector2 ConnectionPoint { get => Owner.Center + new Vector2(60, 0).RotatedBy(NPC.ai[0]); }
	public override void AI()
	{
		NPC.localAI[2] += 0.04f;
		var seed = Utils.RandomNextSeed((ulong)NPC.whoAmI);
		int randDirection = Utils.RandomInt(ref seed, 2) == 0 ? -1 : 1;
		NPC.rotation = (Utils.RandomFloat(ref seed) * MathHelper.TwoPi) + NPC.localAI[2] * randDirection;
		_chompSound.Pitch = -0.4f;
		_chompSound.pitchVariance = 0.3f;
		_chompSound.Volume = 0.4f;
		if (NPC.ai[2] != 0)
		{
			NPC.ai[2]++;
			//if (NPC.ai[2] < 61)
			//{
			//	Vector2 vect = Main.rand.NextVector2CircularEdge(1,1);
			//	Dust d = Dust.NewDustPerfect(NPC.Center + vect * 30, ModContent.DustType<SimpleColorableGlowyDust>(), vect * -3);
			//	d.scale = 1.25f;
			//	d.noGravity = true;
			//	d.color = new Color(0.5f, 0.55f, 0.2f, 0.3f);
			//	d.velocity += NPC.velocity;
			//}
			if (NPC.ai[2] > 61)
			{
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.Center.DirectionTo(Target.Center) * 6, ModContent.ProjectileType<TendrilShot>(), 10, 1, ai0: NPC.target);
				NPC.ai[2] = -60;
				NPC.velocity -= NPC.Center.DirectionTo(Target.Center) * 7;
			}
		}
		NPC.alpha = Owner.alpha;
		NPC.ai[0] += 0.002f;
		NPC.ai[1]++;
		if (!Owner.active || Owner.type != ModContent.NPCType<BacteriumPrime>())
		{
			NPC.active = false;
		}
		float ownerDist = NPC.Center.Distance(Owner.Center);
		if (ownerDist < 70)
		{
			NPC.position -= NPC.Center.DirectionTo(Owner.Center) * (70 - ownerDist);
		}

		NPC.position += Owner.velocity;
		NPC.TargetClosest();
		if (!NPC.HasValidTarget || !Target.Hitbox.IntersectsConeFastInaccurate(Owner.Center, 400, NPC.ai[0],1f))
		{
			NPC.frame.Y = (int)((Math.Abs(NPC.ai[2]) / 60f) * 4) * 32;
			Vector2 targetPos = ConnectionPoint + new Vector2(15, 0).RotatedBy(NPC.ai[0]) + new Vector2(5).RotatedBy(NPC.ai[1] * 0.05f);
			NPC.SimpleFlyMovement(NPC.Center.DirectionTo(targetPos) * 3f, 0.08f);
			NPC.localAI[0] = 1;
			NPC.ai[1] = 0;
			return;
		}
		else
		{
			var targetDist = NPC.Center.Distance(Target.Center);
			NPC.localAI[2] += Utils.Remap(targetDist,32,400,0.08f,0f);
			NPC.frameCounter++;
			if (NPC.frameCounter > MathHelper.Clamp(targetDist / 16, 2, 7))
			{
				NPC.frame.Y += 32;
				NPC.frameCounter = Main.rand.Next(-4,1);
				if (NPC.frame.Y == 128)
				{
					SoundEngine.PlaySound(_chompSound, NPC.position);
					NPC.frame.Y = 0;
				}
			}
		}
		NPC.localAI[0] = 0;

		float timerForSin = MathF.Pow((NPC.ai[1] % 180) / 180,3) * MathHelper.TwoPi;
		float sin = MathF.Sin(timerForSin);
		float cos = MathF.Cos(timerForSin);

		Vector2 whipTargetPos = Vector2.Lerp(Target.Center, ConnectionPoint, 0.5f);
		float connectionDist = Target.Center.Distance(ConnectionPoint);
		whipTargetPos += new Vector2(connectionDist / 2 * -cos, -sin * connectionDist * 0.5f * randDirection).RotatedBy(ConnectionPoint.DirectionTo(Target.Center).ToRotation());

		NPC.SimpleFlyMovement(NPC.Center.DirectionTo(whipTargetPos) * Utils.Remap(connectionDist, 0, 128, 3f, 8f), Utils.Remap(NPC.Center.Distance(whipTargetPos),0,64,0.1f,1f));

		Dust.QuickDust(whipTargetPos, Color.Red);
	}
	public override void OnKill()
	{
		Owner.netUpdate = true;
		if (Owner.ai[0] == 0)
			Owner.ai[0] = 1;
	}
	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		var chain = AssetReferences.NPCs.Bosses.PreHardmode.BacteriumPrime.BacteriumTendril_Chain.Asset;
		Vector2 direction = NPC.Center.DirectionTo(ConnectionPoint);
		float rotation = direction.ToRotation() + MathHelper.PiOver2;
		int divisions = 3;
		int iterations = (int)Math.Ceiling(NPC.Center.Distance(ConnectionPoint) / chain.Height() * divisions);
		//for (int i = 0; i <= iterations; i++)
		//{
		//	spriteBatch.Draw(Chain.Value, ConnectionPoint - (direction * i * Chain.Height() / divisions) - screenPos + new Vector2(0, (MathF.Sin(i / (float)iterations * MathHelper.Pi) * (iterations / 1.5f)) + (iterations / 3f)), new Rectangle(0,(i % divisions) * Chain.Height() / divisions,Chain.Width(),Chain.Height() / 2), drawColor * NPC.Opacity, rotation, new Vector2(Chain.Width() / 2, Chain.Height()), NPC.scale, SpriteEffects.None, 0);
		//}
		Color glowColor = NPC.GetNPCColorTintedByBuffs(new Color(1f, 0.9f, 0f, 0f)) * NPC.Opacity * (Math.Abs(NPC.ai[2]) / 60f);
		if (NPC.ai[2] != 0)
		{
			for (int i = 0; i < 4; i++)
				spriteBatch.Draw(TextureAssets.Npc[Type].Value, NPC.Center - screenPos + new Vector2(4,0).RotatedBy(i * MathHelper.PiOver2), NPC.frame, glowColor * NPC.Opacity, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, SpriteEffects.None, 0);
		}
		for (int i = 1; i <= iterations; i++)
		{
			if (NPC.ai[2] != 0)
			{
				spriteBatch.Draw(chain.Value, new Vector2(2, 0).RotatedBy(rotation) + ConnectionPoint - (direction * i * chain.Height() / divisions) - screenPos, new Rectangle(0, (i % divisions) * chain.Height() / divisions, chain.Width(), chain.Height() / 2), glowColor, rotation, new Vector2(chain.Width() / 2, chain.Height()), NPC.scale, SpriteEffects.None, 0);
				spriteBatch.Draw(chain.Value, new Vector2(-2,0).RotatedBy(rotation) + ConnectionPoint - (direction * i * chain.Height() / divisions) - screenPos, new Rectangle(0, (i % divisions) * chain.Height() / divisions, chain.Width(), chain.Height() / 2), glowColor, rotation, new Vector2(chain.Width() / 2, chain.Height()), NPC.scale, SpriteEffects.None, 0);
			}
			spriteBatch.Draw(chain.Value, ConnectionPoint - (direction * i * chain.Height() / divisions) - screenPos, new Rectangle(0, (i % divisions) * chain.Height() / divisions, chain.Width(), chain.Height() / 2), drawColor * NPC.Opacity, rotation, new Vector2(chain.Width() / 2, chain.Height()), NPC.scale, SpriteEffects.None, 0);
		}
		spriteBatch.Draw(TextureAssets.Npc[Type].Value,NPC.Center - screenPos,NPC.frame,drawColor * NPC.Opacity,NPC.rotation,NPC.frame.Size() / 2, NPC.scale, SpriteEffects.None, 0);
		if (NPC.ai[2] != 0)
		{
			spriteBatch.Draw(TextureAssets.Npc[Type].Value, NPC.Center - screenPos, NPC.frame, glowColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, SpriteEffects.None, 0);
		}
		return false;
	}
}
