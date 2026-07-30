using Avalon;
using Avalon.Items.Banners;
using Avalon.Common.Players;
using Avalon.Projectiles.Hostile;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Corruption;

public class Shadlopod : ModNPC
{
	public override void SetStaticDefaults()
	{
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
		Main.npcFrameCount[NPC.type] = 4;
	}

	public override Color? GetAlpha(Color drawColor)
	{
		return base.GetAlpha(drawColor);
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
		BannerItem = ModContent.ItemType<ShadlopodBanner>();
		Banner = NPC.type;
	}
	float collisionPoint = 0f;
	bool Grounded = false;
	public override bool PreAI()
	{
		if (Grounded && NPC.velocity.Y < 0f)
		{
			NPC.velocity.Y *= 0.88f;
		}
		return true;
	}
	public static Vector2 StandardQuadraticFormula(float a, float b, float c, Vector2 x)
	{
		return new Vector2(StandardQuadraticFormula(a, b, c, x.X), StandardQuadraticFormula(a, b, c, x.Y));
	}
	public static float StandardQuadraticFormula(float a, float b, float c, float x)
	{
		return a * MathF.Pow(x, 2f) + b * x + c;
	}
	public static Vector2 QuadraticFormula(float a, float b, float c)
	{
		float x = MathF.Sqrt(QuadraticDiscriminant(a, b, c));
		return new Vector2((-b - x) / (2f * a), (-b + x) / (2f * a));
	}
	public static float QuadraticDiscriminant(float a, float b, float c)
	{
		return MathF.Pow(b, 2) - 4f * a * c;
	}
	public const float gravityInMetres = 9.80665f;
	public const float gravityInFeet = 32.1740486f;
	public const float gravityInPixelsPerTick = 4.28987314f;
	public const float oneMetreToFoot = 3.2808399f;

	public static float gravPx = 0.0714978857f * 0.5f;
	public static float MetresToFeet(float m)
	{
		return m * oneMetreToFoot;
	}
	public float FinalAngle(Vector2 pos, float u)
	{
		Vector2 d = pos - NPC.Center;
		if (d.X == 0)
		{
			return MathHelper.PiOver2;
		}
		float a = (0.5f * gravPx * MathF.Pow(d.X, 2f)) / MathF.Pow(u, 2f);
		float b = d.X;
		float c = -d.Y + a;
		float discSqrt = MathF.Sqrt(QuadraticDiscriminant(a, b, c));

		float negdxminusdisc = -d.X - discSqrt;
		float negdxplusdisc = -d.X + discSqrt;

		Vector2 div2a = new(negdxminusdisc / (2f * a), negdxplusdisc / (2f * a));

		float angle = d.X > 0 ? MathF.Atan(div2a.Y) : MathF.PI + MathF.Atan(div2a.X);

		return angle;
	}
	public bool CanHit(Vector2 pos, float speed, out float time)
	{
		bool retVal = false;
		//bool tempVal = false;
		//for (int k = 0; k <= 5; k++)
		//{
		//	Vector2 u = new(2f, 0f);
		//	u = u.RotatedBy(MathHelper.ToRadians(k * 17f));
		//	Main.NewText(u.ToRotation());
		//	float ux = u.X;
		//	float uy = u.Y;
		//	Vector2 projections = QuadraticFormula(0.5f * gravPx, uy, NPC.position.Y - pos.Y);
		//	float time = MathF.Max(projections.X, projections.Y);
		//	//Main.NewText(projections, Main.DiscoColor);
		//	//Main.NewText(NPC.Center.X - pos.X, Main.DiscoColor);
		//	if (MathF.Abs(NPC.Center.X - pos.X) <= ux * time)
		//	{
		//		retVal = true;
		//		tempVal = true;
		//	}
		//	else
		//	{
		//		tempVal = false;
		//	}
		//	for (int i = 0; i < 15; i++)
		//	{
		//		float j = i * 32 + 16;
		//		Vector2 projections2 = QuadraticFormula(0.5f * gravPx, uy, -j);
		//		float time2 = MathF.Max(projections2.X, projections2.Y);
		//		Dust.QuickDust(NPC.Center + new Vector2(ux * time2, j), tempVal ? Color.Green : Color.Red);
		//		Dust.QuickDust(NPC.Center + new Vector2(-ux * time2, j), tempVal ? Color.Green : Color.Red);
		//	}
		//}
		Vector2 projections = QuadraticFormula(0.5f * gravPx, 0, NPC.Center.Y - pos.Y);
		//Main.NewText(projections, Color.Blue);
		time = MathF.Max(projections.X, projections.Y);
		if (MathF.Abs(NPC.Center.X - pos.X) <= speed * time)
		{
			retVal = true;
		}
		int iterations = 15;
		for (int i = 0; i < iterations; i++)
		{
			float j = Utils.Remap(i * 32 + 16, 0, (iterations - 1) * 32 + 16, 0, pos.Y - NPC.Center.Y);
			Vector2 projections2 = QuadraticFormula(0.5f * gravPx, 0f, -j);
			float time2 = MathF.Max(projections2.X, projections2.Y);
			Dust.QuickDust(NPC.Center + new Vector2(speed * time2, j), retVal ? Color.Green : Color.Red);
			Dust.QuickDust(NPC.Center + new Vector2(-speed * time2, j), retVal ? Color.Green : Color.Red);
		}
		return retVal;
	}
	public override void AI()
	{
		bool FoundTile = false;
		if (NPC.ai[0] == 0 && !Grounded)
		{
			NPC.ai[0] = 1;
			for (int i = 32; i < 700; i += 4)
			{
				//Main.LocalPlayer.position = NPC.position;
				//Main.NewText(i, Color.Wheat);
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
			// todo: I could probably get a bit of extra range if I bring back the offset from the centre, and make the npcs rotation not based on shooting direction
			NPC.TargetClosest();
			NPC.behindTiles = true;
			Player player = Main.player[NPC.target];
			float speed = 2f;
			Vector2 pos = player.Center;
			bool canHit = CanHit(pos, speed, out float time);
			float angle = MathHelper.PiOver2;
			float rotSpeed = 0.9f;
			if (canHit)
			{
				Main.NewText($"old: {player.GetModPlayer<AvalonPlayer>().playerOldVelocity[0]}", Main.DiscoColor);
				Main.NewText($"cur: {player.velocity}", Main.DiscoColor);
				Main.NewText(Vector2.Dot(Vector2.Normalize(player.GetModPlayer<AvalonPlayer>().playerOldVelocity[0]), Vector2.Normalize(player.velocity)));
				var chase = Utils.GetChaseResults(player.oldPosition + player.Size / 2, player.GetModPlayer<AvalonPlayer>().playerOldVelocity[0].Length(), player.Center, player.velocity);
				var fac = Utils.FactorAcceleration(Vector2.Zero, time, new Vector2(speed, gravPx), 5);
				// https://discord.com/channels/103110554649894912/534215632795729922/1394309371097514096
				Main.NewText(fac);
				Main.NewText($"acc: {player.runAcceleration}");
				Main.NewText((player.velocity.Length() >= player.GetModPlayer<AvalonPlayer>().playerOldVelocity[0].Length()));
				// blah blah blah, use the newtext stuff above to combine the old and new velocities
				// need to also fix the player's Y velocity being able to exceed the max shooting angle
				Vector2 pos2 = player.Center + player.velocity * (time / 4f); // fudging some motion prediction here
				pos2 = player.Center;
				if (MathF.Abs(NPC.Center.X - pos2.X) <= speed * time)
				{
					pos = pos2;
				}
				else
				{
					pos.X = NPC.Center.X + speed * time * MathF.Sign(player.Center.X - NPC.Center.X);
				}
				angle = FinalAngle(pos, speed);
				//Main.NewText(angle, Color.Red);
				//Vector2 projections = QuadraticFormula(0.5f * gravPx, MathF.Sin(angle) * speed, NPC.Center.Y - pos.Y);
				//Main.NewText(projections, Color.Green);
				//float time = MathF.Max(projections.X, projections.Y);
				//Dust.QuickDust(NPC.Center + new Vector2(MathF.Cos(angle) * speed * time, pos.Y - NPC.Center.Y), Color.Green);
				int iterations = 15;
				for (int i = 0; i < iterations; i++)
				{
					float j = Utils.Remap(i * 32 + 16, 0, (iterations - 1) * 32 + 16, 0, pos.Y - NPC.Center.Y);
					Vector2 projections2 = QuadraticFormula(0.5f * gravPx, MathF.Sin(angle) * speed, -j);
					float time2 = MathF.Max(projections2.X, projections2.Y);
					Dust.QuickDust(NPC.Center + new Vector2(MathF.Cos(angle) * speed * time2, j), Color.Green);
				}
				rotSpeed = 0.9f;
			}
			//Main.NewText(CanHit(Main.player[NPC.target].Center), Main.DiscoColor);
			//Main.NewText(FinalAngle(Main.player[NPC.target].Center, 2f));

			//bool TargetValidForShootingAt = NPC.HasValidTarget ? Main.player[NPC.target].Center.Y > NPC.Center.Y && Collision.CanHitLine(NPC.Center + new Vector2(0, 16).RotatedBy(NPC.rotation) + new Vector2(0, 32), 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) : false;
			bool TargetValidForShootingAt = canHit;

			//NPC.rotation = MathHelper.SmoothStep(NPC.Center.DirectionTo(TargetValidForShootingAt ? Main.player[NPC.target].Center : NPC.Bottom).ToRotation() - MathHelper.PiOver2, NPC.rotation, 0.9f);
			NPC.rotation = Utils.AngleLerp(MathHelper.SmoothStep(angle - MathHelper.PiOver2, NPC.rotation, rotSpeed), 0, 0.01f);

			NPC.ai[1]++;
			if (Main.expertMode)
				NPC.ai[1] += 0.5f;
			if ((int)NPC.ai[1] % 120 == 0 && TargetValidForShootingAt)
			{
				//Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, NPC.Bottom.DirectionTo(Main.player[NPC.target].Center) * 12,ProjectileID.CursedFlameHostile,24,0);
				Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(speed, 0).RotatedBy(angle), ModContent.ProjectileType<ShadlopodInk>(), 8, 0, ai0: NPC.target);
				SoundEngine.PlaySound(SoundID.Item64, NPC.position);
			}

			if (NPC.ai[0] != 0 && !Collision.SolidCollision(new Vector2(NPC.position.X, new Vector2(0, NPC.position.Y - 16).ToTileCoordinates().Y * 16), NPC.width, NPC.height))
			{
				NPC.noGravity = false;
				NPC.aiStyle = NPCAIStyleID.Fighter;
				Grounded = true;
				NPC.netUpdate = true;
			}
		}
		else
		{
			NPC.TargetClosest();
			NPC.rotation = NPC.velocity.X * -0.1f;
		}
	}
	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption,
			new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.Shadlopod")),
		});

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(new CommonDrop(ItemID.RottenChunk, 3, 1, 2, 2));
	}
	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter += !Grounded ? 1 : Math.Abs(NPC.velocity.X * 1.3f);
		if (NPC.frameCounter > 10)
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
				for (int i = 0; i < 4; i++)
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
		? 0.2f : 0f;
}
