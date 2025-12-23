using Avalon.Dusts;
using Avalon.NPCs.Bosses.Hardmode.Phantasm.Projectiles;
using Avalon.UI;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;


namespace Avalon.NPCs.Bosses.Hardmode.Phantasm
{
	public partial class Phantasm : ModNPC
	{
		const int swordDamage = 30;
		const int handDamage = 25;
		const int projDamage = 20;
		private void Phase0_Dash()
		{
			int dashInterval = 90;
			if (Main.expertMode) dashInterval = 60;
			if (NPC.ai[1] < dashInterval - 30)
			{
				NPC.velocity += NPC.Center.DirectionTo(target.Center + new Vector2(0, -100).RotatedBy(NPC.ai[0] * 0.02f * NPC.direction)) * 0.2f;
				NPC.velocity = NPC.velocity.LengthClamp(10);
				NPC.rotation = Utils.AngleLerp(NPC.velocity.X * -0.04f, NPC.rotation, 0.94f);
			}
			else
			{
				Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit);
				d.noGravity = true;
				d.scale = Main.rand.NextFloat(2);
				d.velocity = NPC.velocity;
				if (NPC.ai[1] > dashInterval && NPC.ai[1] % 7 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity.RotatedBy(MathHelper.PiOver2) * 0.3f, ModContent.ProjectileType<LostSoul>(), projDamage, 1,-1,target.whoAmI);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity.RotatedBy(-MathHelper.PiOver2) * 0.3f, ModContent.ProjectileType<LostSoul>(), projDamage, 1, -1, target.whoAmI);
				}
			}
			NPC.ai[0]++;
			NPC.ai[1]++;
			if (NPC.ai[1] == dashInterval)
			{
				NPC.rotation = NPC.Center.DirectionTo(target.Center).ToRotation() - MathHelper.PiOver2;
				SoundEngine.PlaySound(SoundID.NPCDeath39, NPC.position);
				NPC.velocity = NPC.Center.DirectionTo(target.Center) * 20f;
			}
			else if (NPC.ai[1] > dashInterval + 40)
			{
				NPC.ai[1] = Main.rand.Next(-200, 0);
				NPC.netUpdate = true;
			}
			else if (NPC.ai[1] > dashInterval - 30 && NPC.ai[1] < dashInterval)
			{
				NPC.rotation = Utils.AngleLerp(NPC.Center.DirectionTo(target.Center).ToRotation() - MathHelper.PiOver2, NPC.rotation, 0.7f);
				NPC.velocity *= 0.96f;
			}
			if (NPC.ai[0] > 600 && NPC.ai[1] < dashInterval)
			{
				Transition();
				NPC.TargetClosest();
				phase = (byte)Main.rand.Next(1,3);
				NPC.ai[0] = phase == 1 ? Main.rand.Next(-120, -60) : 0;
				NPC.ai[1] = 0;
			}
		}
		private void Phase1_Swords()
		{
			NPC.velocity += NPC.Center.DirectionTo(target.Center) * 0.2f;
			NPC.velocity = NPC.velocity.LengthClamp(10);
			NPC.rotation = Utils.AngleLerp(NPC.velocity.X * -0.04f, NPC.rotation, 0.94f);

			int pause = 20;
			if (Main.expertMode) pause = 15;
			if (NPC.ai[0] % pause == 0 && NPC.ai[0] <= pause * 5)
			{
				if(Main.netMode != NetmodeID.MultiplayerClient)
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<SoulDagger>(), swordDamage, 1, -1, NPC.whoAmI, NPC.ai[0]);
				}
				SoundEngine.PlaySound(SoundID.Item8 with { Pitch = -0.5f + (NPC.ai[0] / 75), MaxInstances = 12 }, NPC.position);
			}
			NPC.ai[0]++;
			if (NPC.ai[0] > 300)
			{
				Transition();
				NPC.TargetClosest();
				phase = (byte)Main.rand.Next(3);
				NPC.ai[0] = 0;
				NPC.ai[1] = 0;
				NPC.netUpdate = true;
			}
		}
		private void Phase2_Hands()
		{
			NPC.ai[0]++;
			float speed = 0.75f;
			if (Main.expertMode) speed = 1.1f;
			NPC.velocity += NPC.Center.DirectionTo(target.Center + new Vector2(0, 400).RotatedBy(NPC.ai[0] * 0.04f * NPC.direction)) * speed;
			NPC.velocity = NPC.velocity.LengthClamp(12);
			NPC.rotation = Utils.AngleLerp(NPC.velocity.X * -0.04f, NPC.rotation, 0.94f);

			if (NPC.ai[0] % 15 == 0 && NPC.ai[0] > 60)
			{
				if(Main.netMode != NetmodeID.MultiplayerClient)
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-Math.Sign(NPC.Center.X - target.Center.X) * 2, Main.rand.NextFloat(-0.2f, 0.2f)), ModContent.ProjectileType<SoulGrabber>(), handDamage, 1, -1, NPC.whoAmI, NPC.ai[0]);
				}
			}
			if (NPC.ai[0] >= 60 * 10)
			{
				Transition();
				phase = (byte)Main.rand.Next(2);
				NPC.ai[1] = 0;
				NPC.ai[0] = 0;
				NPC.netUpdate = true;
			}
		}
		private void Phase3_Transition()
		{
			NPC.velocity *= 0.97f;
			NPC.ai[0]++;
			NPC.rotation = Utils.AngleLerp(NPC.rotation, 0, 0.03f);

			NPC.position += Main.rand.NextVector2Circular(NPC.ai[0] / 10, NPC.ai[0] / 10);
			NPC.netUpdate = true;
			if (NPC.ai[0] > 60)
			{
				PunchCameraModifier modifier = new PunchCameraModifier(target.Center, Main.rand.NextVector2Circular(1, 1), 8f, 7f, 45, 3000f);
				Main.instance.CameraModifiers.Add(modifier);
				for (int i = 0; i < 40; i++)
				{
					int num890 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 0, default, 1f);
					Main.dust[num890].velocity *= 5f;
					Main.dust[num890].scale = 1.5f;
					Main.dust[num890].noGravity = true;
					Main.dust[num890].fadeIn = 2f;
				}
				for (int i = 0; i < 20; i++)
				{
					int num893 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
					Main.dust[num893].velocity *= 2f;
					Main.dust[num893].scale = 1.5f;
					Main.dust[num893].noGravity = true;
					Main.dust[num893].fadeIn = 3f;
				}
				for (int i = 0; i < 40; i++)
				{
					int num892 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.SpectreStaff, 0f, 0f, 0, default(Color), 1f);
					Main.dust[num892].velocity *= 5f;
					Main.dust[num892].scale = 1.5f;
					Main.dust[num892].noGravity = true;
					Main.dust[num892].fadeIn = 2f;
				}
				for (int i = 0; i < 40; i++)
				{
					int num891 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
					Main.dust[num891].velocity *= 10f;
					Main.dust[num891].scale = 1.5f;
					Main.dust[num891].noGravity = true;
					Main.dust[num891].fadeIn = 1.5f;
				}

				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					//NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<PhantoplasmaBall>(),NPC.whoAmI,NPC.whoAmI);
					for (int i = 0; i < 12; i++)
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Main.rand.NextVector2Circular(12, 12), ModContent.ProjectileType<LostSoul>(), projDamage, 1, -1, target.whoAmI);
					}
				}
				Transition();
				SoundEngine.PlaySound(SoundID.Roar, NPC.position);
				NPC.ai[0] = 0;
				NPC.ai[1] = -200;
				phase = 4;
			}
		}
		private void Phase4_Dash2()
		{
			int dashInterval = 75;
			if (Main.expertMode) dashInterval = 40;
			if (NPC.ai[1] < dashInterval - 30)
			{
				NPC.velocity += NPC.Center.DirectionTo(target.Center + new Vector2(0, -100).RotatedBy(NPC.ai[0] * 0.02f * NPC.direction)) * 0.2f;
				NPC.velocity = NPC.velocity.LengthClamp(10);
				NPC.rotation = Utils.AngleLerp(NPC.velocity.X * -0.04f, NPC.rotation, 0.94f);
			}
			else
			{
				Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit);
				d.noGravity = true;
				d.scale = Main.rand.NextFloat(2);
				d.velocity = NPC.velocity;
				if (NPC.ai[1] > dashInterval && NPC.ai[1] % 7 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity.RotatedBy(MathHelper.PiOver2) * 0.3f, ModContent.ProjectileType<LostSoul>(), projDamage, 1, -1, target.whoAmI);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity.RotatedBy(-MathHelper.PiOver2) * 0.3f, ModContent.ProjectileType<LostSoul>(), projDamage, 1, -1, target.whoAmI);
				}
			}
			NPC.ai[0]++;
			NPC.ai[1]++;
			if (NPC.ai[1] == dashInterval)
			{
				NPC.rotation = NPC.Center.DirectionTo(target.Center).ToRotation() - MathHelper.PiOver2;
				SoundEngine.PlaySound(SoundID.NPCDeath39, NPC.position);
				NPC.velocity = NPC.Center.DirectionTo(target.Center) * 25f;
			}
			else if (NPC.ai[1] > dashInterval + 40)
			{
				NPC.ai[1] = Main.rand.Next(-100, -20);
				NPC.netUpdate = true;
			}
			else if (NPC.ai[1] > dashInterval - 30 && NPC.ai[1] < dashInterval)
			{
				NPC.rotation = Utils.AngleLerp(NPC.Center.DirectionTo(target.Center).ToRotation() - MathHelper.PiOver2, NPC.rotation, 0.7f);
				NPC.velocity *= 0.96f;
			}
			if (NPC.ai[0] > 600 && NPC.ai[1] < dashInterval)
			{
				NPC.TargetClosest();
				Transition();
				phase = (byte)Main.rand.Next(4, 8);
				NPC.ai[0] = 0;
				NPC.ai[1] = 0;
			}

			// below is the double dash thing

			//int dashInterval = 30;
			//if (NPC.ai[1] < dashInterval - 30)
			//{
			//	NPC.velocity += NPC.Center.DirectionTo(target.Center + new Vector2(0, -100).RotatedBy(NPC.ai[0] * 0.03f * NPC.direction)) * 0.2f;
			//	NPC.velocity = NPC.velocity.LengthClamp(10);
			//	NPC.rotation = Utils.AngleLerp(NPC.velocity.X * -0.04f, NPC.rotation, 0.94f);
			//}
			//else
			//{
			//	Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit);
			//	d.noGravity = true;
			//	d.scale = Main.rand.NextFloat(2);
			//	d.velocity = NPC.velocity;
			//	if (NPC.ai[1] > dashInterval && NPC.ai[1] % 10 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
			//	{
			//		Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity.RotatedBy(MathHelper.PiOver2) * 0.3f, ModContent.ProjectileType<LostSoul>(), projDamage, 1, -1, target.whoAmI);
			//		Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity.RotatedBy(-MathHelper.PiOver2) * 0.3f, ModContent.ProjectileType<LostSoul>(), projDamage, 1, -1, target.whoAmI);
			//	}
			//}
			//NPC.ai[0]++;
			//NPC.ai[1]++;
			//if (NPC.ai[1] == dashInterval || NPC.ai[1] == dashInterval * 2)
			//{
			//	NPC.rotation = NPC.Center.DirectionTo(target.Center).ToRotation() - MathHelper.PiOver2;
			//	SoundEngine.PlaySound(SoundID.NPCDeath39, NPC.position);
			//	NPC.velocity = NPC.Center.DirectionTo(target.Center) * 20f;
			//}
			//else if (NPC.ai[1] > (dashInterval * 3))
			//{
			//	NPC.ai[1] = Main.rand.Next(-300, -200);
			//	NPC.netUpdate = true;
			//}
			//else if (NPC.ai[1] > dashInterval - 30 && NPC.ai[1] < dashInterval)
			//{
			//	NPC.rotation = Utils.AngleLerp(NPC.Center.DirectionTo(target.Center).ToRotation() - MathHelper.PiOver2, NPC.rotation, 0.7f);
			//	NPC.velocity *= 0.96f;
			//}
			//if (NPC.ai[0] > 500 && NPC.ai[1] < dashInterval)
			//{
			//	NPC.TargetClosest();
			//	playPhantasmSound();
			//	phase = (byte)Main.rand.Next(4, 8);
			//	NPC.ai[0] = 0;
			//	NPC.ai[1] = 0;
			//}
		}
		private void Phase5_Hands2()
		{
			NPC.ai[0]++;
			NPC.velocity += NPC.Center.DirectionTo(target.Center + new Vector2(0, 500).RotatedBy(NPC.ai[0] * 0.04f * NPC.direction)) * 1.2f;
			NPC.velocity = NPC.velocity.LengthClamp(14);
			NPC.rotation = Utils.AngleLerp(NPC.velocity.X * -0.04f, NPC.rotation, 0.94f);

			if (NPC.ai[0] % 11 == 0 && NPC.ai[0] > 60)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-Math.Sign(NPC.Center.X - target.Center.X) * Main.rand.NextFloat(3,5), Main.rand.NextFloat(-0.3f, 0.3f)), ModContent.ProjectileType<SoulGrabber>(), handDamage, 1, -1, NPC.whoAmI, NPC.ai[0]);
				}
			}
			if (NPC.ai[0] >= 60 * 10)
			{
				phase = (byte)Main.rand.Next(4, 8);
				NPC.ai[1] = 0;
				NPC.ai[0] = 0;
				NPC.netUpdate = true;
				Transition();
			}
		}
		private void Phase6_Swords2()
		{
			NPC.velocity += NPC.Center.DirectionTo(target.Center) * 0.2f;
			NPC.velocity = NPC.velocity.LengthClamp(10);
			NPC.rotation = Utils.AngleLerp(NPC.velocity.X * -0.04f, NPC.rotation, 0.94f);

			int pause = 20;
			if (Main.expertMode) pause = 10;
			if (NPC.ai[0] % pause == 0 && NPC.ai[0] < pause * 9)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<PhantomDagger>(), swordDamage, 1, -1, NPC.whoAmI, NPC.ai[0]);
				}
				SoundEngine.PlaySound(SoundID.Item8 with { Pitch = -0.5f + (NPC.ai[0] / 90), MaxInstances = 12 }, NPC.position);
			}
			NPC.ai[0]++;
			if (NPC.ai[0] > 200 && NPC.ai[1] < 3)
			{
				NPC.ai[0] = 0;
				NPC.ai[1]++;
			}
			if (NPC.ai[0] > 400)
			{
				Transition();
				NPC.TargetClosest();
				phase = (byte)Main.rand.Next(4, 8);
				NPC.ai[0] = 0;
				NPC.ai[1] = 0;
				NPC.netUpdate = true;
			}
		}
		private void Phase7_SawHandsScary()
		{
			NPC.velocity += NPC.Center.DirectionTo(new Vector2(target.Center.X,MathHelper.Lerp(target.Center.Y,eyePos.Y,0.6f))) * 0.2f;
			NPC.velocity = NPC.velocity.LengthClamp(10);
			NPC.rotation = Utils.AngleLerp(NPC.velocity.X * -0.04f, NPC.rotation, 0.94f);

			NPC.ai[0]++;
			if (NPC.ai[0] > 100)
			{
				NPC.velocity *= 0.93f;
			}

			if (NPC.ai[0] >= 120 && NPC.ai[0] % 10 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
			{
				if (NPC.ai[1] >= 6)
				{
					NPC.ai[0] = 0;
					NPC.ai[1] = 0;
					NPC.TargetClosest();
					Transition();
					phase = (byte)Main.rand.Next(4, 8);
					NPC.netUpdate = true;
					return;
				}

				if (NPC.ai[1] % 2 == 0 && NPC.ai[0] == 120)
				{
					int iterations = 12;
					for (int i = 0; i < iterations; i++)
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Bottom, new Vector2(0, 8).RotatedBy(i * MathHelper.TwoPi / iterations), ModContent.ProjectileType<Soulblade>(), swordDamage, 1);
				}
				else if (NPC.ai[1] % 2 == 1 && NPC.ai[0] % 10 == 0)
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.Center.DirectionTo(target.Center).RotatedByRandom(0.5f) * 10, ModContent.ProjectileType<SoulGrabber>(), handDamage, 1);
				}
			}

			if (NPC.ai[0] == 160)
			{
				NPC.ai[0] = 0;
				NPC.ai[1]++;
			}
		}
		private void Phase8_Transition2()
		{
			NPC.velocity *= 0.97f;
			NPC.ai[0]++;
			NPC.rotation = Utils.AngleLerp(NPC.rotation, 0, 0.03f);

			NPC.position += Main.rand.NextVector2Circular(NPC.ai[0] / 10, NPC.ai[0] / 10);
			NPC.netUpdate = true;
			if (NPC.ai[0] > 60)
			{
				PunchCameraModifier modifier = new PunchCameraModifier(target.Center, Main.rand.NextVector2Circular(1, 1), 8f, 7f, 45, 3000f);
				Main.instance.CameraModifiers.Add(modifier);
				for (int i = 0; i < 40; i++)
				{
					int num890 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<PhantoplasmDust>(), 0f, 0f, 0, default(Color), 1f);
					Main.dust[num890].velocity *= 5f;
					Main.dust[num890].scale = 1.5f;
					Main.dust[num890].noGravity = true;
					Main.dust[num890].fadeIn = 2f;
				}
				for (int i = 0; i < 20; i++)
				{
					int num893 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<PhantoplasmDust>(), 0f, 0f, 0, default(Color), 1f);
					Main.dust[num893].velocity *= 2f;
					Main.dust[num893].scale = 1.5f;
					Main.dust[num893].noGravity = true;
					Main.dust[num893].fadeIn = 3f;
				}
				for (int i = 0; i < 40; i++)
				{
					int num892 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.SpectreStaff, 0f, 0f, 0, default(Color), 1f);
					Main.dust[num892].velocity *= 5f;
					Main.dust[num892].scale = 1.5f;
					Main.dust[num892].noGravity = true;
					Main.dust[num892].fadeIn = 2f;
				}
				for (int i = 0; i < 40; i++)
				{
					int num891 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<PhantoplasmDust>(), 0f, 0f, 0, default(Color), 1f);
					Main.dust[num891].velocity *= 10f;
					Main.dust[num891].scale = 1.5f;
					Main.dust[num891].noGravity = true;
					Main.dust[num891].fadeIn = 1.5f;
				}

				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					//NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<PhantoplasmaBall>(), NPC.whoAmI, NPC.whoAmI,1);
					for (int i = 0; i < 12; i++)
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Main.rand.NextVector2Circular(12, 12), ModContent.ProjectileType<Phantom>(), projDamage, 1, -1, target.whoAmI);
					}
					for(int i = 0; i < 4; i++)
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(), eyePos, Vector2.One.RotatedBy(i * MathHelper.PiOver2) * 5, ModContent.ProjectileType<PhantasmOrbSpawner>(), 0, 1, -1, NPC.whoAmI);
					}
					//Vector2 direction = Main.rand.NextVector2CircularEdge(8, 8);
					//Projectile.NewProjectile(NPC.GetSource_FromThis(), eyePos, direction, ModContent.ProjectileType<PhantasmOrbSpawner>(), 0, 1, -1, NPC.whoAmI);
					//Projectile.NewProjectile(NPC.GetSource_FromThis(), eyePos, direction.RotatedBy(Main.rand.NextFloat(MathHelper.PiOver2,MathHelper.Pi) * (Main.rand.NextBool()? 1 : -1)), ModContent.ProjectileType<PhantasmOrbSpawner>(), 0, 1, -1, NPC.whoAmI);
				}
				SoundEngine.PlaySound(SoundID.Roar, NPC.position);
				NPC.ai[0] = 0;
				NPC.ai[1] = -200;
				Transition();
				phase = 12;
			}
		}
		private void Phase9_Dash3()
		{
			int dashInterval = 90;
			if (Main.expertMode) dashInterval = 60;
			if (NPC.ai[1] < dashInterval - 50)
			{
				NPC.velocity += NPC.Center.DirectionTo(target.Center + new Vector2(0, -100).RotatedBy(NPC.ai[0] * 0.04f * NPC.direction)) * 0.3f;
				NPC.velocity = NPC.velocity.LengthClamp(12);
				NPC.rotation = Utils.AngleLerp(NPC.velocity.X * -0.04f, NPC.rotation, 0.94f);
			}
			else
			{
				Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, ModContent.DustType<PhantoplasmDust>());
				d.noGravity = true;
				d.scale = Main.rand.NextFloat(2);
				d.velocity = NPC.velocity;
				if (NPC.ai[1] > dashInterval && NPC.ai[1] % 6 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity.RotatedBy(MathHelper.PiOver2) * 0.3f, ModContent.ProjectileType<Phantom>(), projDamage, 1, -1, target.whoAmI);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity.RotatedBy(-MathHelper.PiOver2) * 0.3f, ModContent.ProjectileType<Phantom>(), projDamage, 1, -1, target.whoAmI);
				}
			}
			NPC.ai[0]++;
			NPC.ai[1]++;
			if (NPC.ai[1] == dashInterval)
			{
				NPC.rotation = NPC.Center.DirectionTo(target.Center).ToRotation() - MathHelper.PiOver2;
				SoundEngine.PlaySound(SoundID.NPCDeath39, NPC.position);
				NPC.velocity = NPC.Center.DirectionTo(target.Center) * 25f;
			}
			else if (NPC.ai[1] > (dashInterval * 2))
			{
				NPC.ai[1] = Main.rand.Next(-300, -200);
				NPC.netUpdate = true;
			}
			else if (NPC.ai[1] > dashInterval - 30 && NPC.ai[1] < dashInterval)
			{
				NPC.rotation = Utils.AngleLerp(NPC.Center.DirectionTo(target.Center).ToRotation() - MathHelper.PiOver2, NPC.rotation, 0.7f);
				NPC.velocity *= 0.96f;
			}
			if (NPC.ai[0] > 500 && NPC.ai[1] < dashInterval)
			{
				Transition();
				NPC.TargetClosest();
				phase = (byte)Main.rand.Next(9, 13);
				NPC.ai[0] = 0;
				NPC.ai[1] = 0;
			}
		}
		private void Phase10_ZoinksItsTheGayBlades()
		{
			NPC.velocity += NPC.Center.DirectionTo(target.Center) * 0.2f;
			NPC.velocity = NPC.velocity.LengthClamp(10);
			NPC.rotation = Utils.AngleLerp(NPC.velocity.X * -0.04f, NPC.rotation, 0.94f);

			if (NPC.ai[0] % 10 == 0 && NPC.ai[0] < 90)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<PhantomDagger>(), swordDamage, 1, -1, NPC.whoAmI, NPC.ai[0]);
				}
				SoundEngine.PlaySound(SoundID.Item8 with { Pitch = -0.5f + (NPC.ai[0] / 90), MaxInstances = 12}, NPC.position);
			}
			NPC.ai[0]++;
			if (NPC.ai[0] > 200 && NPC.ai[1] < 3)
			{
				NPC.ai[0] = 0;
				NPC.ai[1]++;
			}
			if (NPC.ai[0] > 300)
			{
				Transition();
				NPC.TargetClosest();
				phase = (byte)Main.rand.Next(9, 13);
				NPC.ai[0] = 0;
				NPC.ai[1] = 0;
				NPC.netUpdate = true;
			}
		}
		private void Phase12_PhantasmalDeathrayLikeMoonlordOMQCantBelieveTheySTOLEFROMAVALONOnceAgainTrulyPatheticRelogicWhyWouldYouDoThatLikeSeriouslyGuysWhatTheHellWhyWouldYouDoThatStealingIsWrongDontDoItGuysWTFGenuinelyCannotBelieveThisWouldHappenManWhatTheHellITRUSTEDYOURELOGICBUTyouGoAndDOTHISWHATTHEHELLISeriouslyCannotBelieveThatTheyWouldDoThatLikeReallyWhatTheHellGuysLikeComeOnWTFDudesAndDudettesAndNoneOfTheAbovesTheRenameBoxIsSoLongThatItGoesOffTheScreenWhenYouTryToRenameThisMethodWhichIsKindaFunnyIThinkInMyOpinionIDKThoughItsJustMySenseofHumorNotYoursYouAreAllowedToDisagreeAllYouWantIWontReallyCareAllThatMuchButIMIghtMentallySendNeedlesToYourMindOrSomethingLikeThatIfYouTellMeYouDisagree()
		{
			NPC.rotation = Utils.AngleLerp(NPC.velocity.X * -0.04f, NPC.rotation, 0.94f);
			NPC.ai[0]++;
			int beamStart = 150;
			if (NPC.ai[0] is > 20 and < 60 && NPC.Center.Distance(eyePos) > 16)
			{
				NPC.ai[0] = 20;
			}

			if (NPC.ai[0] > 60)
			{
				NPC.velocity *= 0.9f;
				if (NPC.ai[0] < beamStart)
				{
					Vector2 vector = Main.rand.NextVector2Circular(1, 1);
					Dust d = Dust.NewDustPerfect(NPC.Center + vector * 130,ModContent.DustType<PhantoplasmDust>(),-vector * 6, 128);
					d.noGravity = true;
					d.scale = 3;
				}
			}
			else
			{
				NPC.velocity = Vector2.Lerp(NPC.velocity + NPC.Center.DirectionTo(eyePos) * 0.2f, NPC.Center.DirectionTo(eyePos) * NPC.velocity.Length(), 0.02f);
				NPC.velocity = NPC.velocity.LengthClamp(12);
			}

			if (NPC.ai[0] == beamStart)
			{
				if(Main.netMode != NetmodeID.MultiplayerClient)
				{
					if (Main.expertMode)
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<PhantomDeathray>(), 50, 1, -1, NPC.whoAmI, ai2: 3);
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<PhantomDeathray>(), 50, 1, -1, NPC.whoAmI, ai2: 1);
					}
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<PhantomDeathray>(), 50, 1, -1, NPC.whoAmI, ai2: 2);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<PhantomDeathray>(), 50, 1, -1, NPC.whoAmI);
				}

				beamSound = SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/NPC/ScaryLaser") {Volume = 0.8f },NPC.position);
			}

			//if (NPC.ai[0] > beamStart && NPC.ai[0] % 30 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
			//{
			//	Vector2 rotation = Main.rand.NextVector2CircularEdge(1, 1);
			//	Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + (rotation * 16 * 50), -rotation, ModContent.ProjectileType<PhantomGrabber>(), projDamage, 1, -1, target.whoAmI);
			//}

			if (NPC.ai[0] > beamStart + ContentSamples.ProjectilesByType[ModContent.ProjectileType<PhantomDeathray>()].timeLeft)
			{
				Transition();
				NPC.TargetClosest();
				phase = (byte)Main.rand.Next(9, 12);
				NPC.ai[0] = 0;
				NPC.ai[1] = 0;
				NPC.netUpdate = true;
			}
		}
		private SlotId beamSound = SlotId.Invalid;
		private void Phase11_SawHandsScary2()
		{
			NPC.velocity += NPC.Center.DirectionTo(new Vector2(target.Center.X, MathHelper.Lerp(target.Center.Y, eyePos.Y, 0.6f))) * 0.2f;
			NPC.velocity = NPC.velocity.LengthClamp(10);
			NPC.rotation = Utils.AngleLerp(NPC.velocity.X * -0.04f, NPC.rotation, 0.94f);

			NPC.ai[0]++;
			if (NPC.ai[0] > 100)
			{
				NPC.velocity *= 0.93f;
			}

			if (NPC.ai[0] >= 120 && Main.netMode != NetmodeID.MultiplayerClient)
			{
				if (NPC.ai[1] >= 6)
				{
					NPC.ai[0] = 0;
					NPC.ai[1] = 0;
					NPC.TargetClosest();
					Transition();
					phase = (byte)Main.rand.Next(9, 13);
					NPC.netUpdate = true;
					return;
				}

				if (NPC.ai[1] % 2 == 0 && NPC.ai[0] == 120)
				{
					int iterations = 16;
					for(int i = 0; i < iterations; i++)
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Bottom, new Vector2(0,8).RotatedBy(i * MathHelper.TwoPi / iterations), ModContent.ProjectileType<PhantomBlade>(), swordDamage, 1);
				}
				else if (NPC.ai[1] % 2 == 1 && NPC.ai[0] % 10 == 0)
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.Center.DirectionTo(target.Center).RotatedByRandom(0.5f) * 10, ModContent.ProjectileType<PhantomGrabber>(), handDamage, 1);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.Center.DirectionTo(target.Center).RotatedByRandom(0.5f) * 8, ModContent.ProjectileType<PhantomGrabber>(), handDamage, 1);
				}
			}

			if (NPC.ai[0] == 160)
			{
				NPC.ai[0] = 0;
				NPC.ai[1]++;
			}
		}
	}
}
