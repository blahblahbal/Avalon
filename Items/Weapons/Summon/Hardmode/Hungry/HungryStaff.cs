using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Summon.Hardmode.Hungry;

public class HungryStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
		ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

		ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMinionWeapon(ModContent.ProjectileType<HungrySummon>(), ModContent.BuffType<Hungry>(), 21, 1.5f, 30);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1);
		Item.UseSound = SoundID.Item44;
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		position = Main.MouseWorld;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 14)
			.AddTile(TileID.Anvils)
			.Register();
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		player.AddBuff(Item.buffType, 2);
		var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
		projectile.originalDamage = Item.damage;
		if (player.GetModPlayer<AvalonPlayer>().FleshArmor)
		{
			projectile.minionSlots = 0.5f;
		}
		return false;
	}
}
public class Hungry : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[Type] = true;
		Main.buffNoSave[Type] = false;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		if (player.ownedProjectileCounts[ModContent.ProjectileType<HungrySummon>()] > 0)
		{
			player.GetModPlayer<AvalonPlayer>().HungryMinion = true;
		}
		if (!player.GetModPlayer<AvalonPlayer>().HungryMinion)
		{
			player.DelBuff(buffIndex);
			buffIndex--;
		}
		else
		{
			player.buffTime[buffIndex] = 18000;
		}
	}
}
public class HungrySummon : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Type] = 6;
		Main.projPet[Projectile.type] = true;
		ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
	}
	public override bool? CanCutTiles()
	{
		return false;
	}
	public override bool MinionContactDamage()
	{
		return true;
	}
	public override void SetDefaults()
	{
		Projectile.aiStyle = -1;
		Rectangle dims = this.GetDims();
		Projectile.DamageType = DamageClass.Summon;
		Projectile.netImportant = true;
		Projectile.width = 24;
		Projectile.height = 24;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 2;
		Projectile.minion = true;
		Projectile.minionSlots = 1f;
		Projectile.tileCollide = true;
		Projectile.ignoreWater = true;
		Projectile.friendly = true;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 30;
		DrawOffsetX = -(int)(dims.Width / 2 - Projectile.Size.X / 2);
		DrawOriginOffsetY = -(int)(dims.Height / Main.projFrames[Projectile.type] / 2 - Projectile.Size.Y / 2);
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (Projectile.oldVelocity.X != Projectile.velocity.X)
		{
			Projectile.velocity.X = -Projectile.oldVelocity.X * 0.6f;
		}
		if (Projectile.oldVelocity.Y != Projectile.velocity.Y)
		{
			Projectile.velocity.Y = -Projectile.oldVelocity.Y * 0.6f;
		}
		return false;
	}
	public ref float RotationCounter => ref Projectile.ai[0];
	public int TargetIndex { get => (int)Projectile.ai[1]; set => Projectile.ai[1] = value; }
	public bool Latched { get => Convert.ToBoolean(Projectile.ai[2]); set => Projectile.ai[2] = Convert.ToSingle(value); }
	public override void AI()
	{
		Player player = Main.player[Projectile.owner];
		AvalonPlayer.MinionRemoveCheck(player, ModContent.BuffType<Hungry>(), Projectile);

		if (Vector2.Distance(Projectile.position, player.position) > 2000)
		{
			Projectile.position = player.position;
		}

		if (Projectile.timeLeft != 5 && Main.rand.NextBool(3))
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(10, 0).RotatedBy(Projectile.rotation) + Main.rand.NextVector2Circular(3, 3), DustID.Blood);
			d.velocity = new Vector2(MathHelper.Clamp(Projectile.velocity.Length(), 3, 10), 0).RotatedBy(Projectile.rotation).RotatedByRandom(0.1f);
			d.noGravity = !Main.rand.NextBool(5);
			d.fadeIn = Main.rand.NextFloat(0, 0.5f);
		}

		if (++Projectile.frameCounter >= 5)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= Main.projFrames[Projectile.type])
			{
				Projectile.frame = 0;
			}
		}

		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi;
		if (TargetIndex != -1)
		{
			if (!Main.npc[TargetIndex].active)
			{
				Projectile.velocity = Main.rand.NextVector2Circular(7, 7);
				Latched = false;
				TargetIndex = -1;
			}
		}

		Vector2 TargetPos;
		RotationCounter++;

		bool TargetingEnemy;
		int TargetNPC = Projectile.FindClosestNPC(700, npc => !npc.CanBeChasedBy(Projectile.ModProjectile, false) || npc.Distance(player.Center) > 1000);
		if (Collision.SolidCollision(Projectile.Center - new Vector2(8), 8, 8) && Collision.SolidCollision(Projectile.position - new Vector2(32), Projectile.width + 32, Projectile.height + 32) || Projectile.position.Distance(Projectile.oldPosition) < 1.7f)
		{
			Projectile.tileCollide = false;
		}
		else
		{
			Projectile.tileCollide = true;
		}

		if (TargetNPC != -1 && Projectile.timeLeft <= 2)
		{
			TargetingEnemy = true;
			TargetPos = Main.npc[TargetNPC].Center + new Vector2(0, -10 + (float)(Projectile.minionPos * Math.Sin(Projectile.ai[0]))).RotatedBy(Projectile.ai[0] * 0.1f * (Projectile.minionPos * 0.7f + 1));
		}
		else
		{
			TargetingEnemy = false;
			//TargetPos = player.Center + new Vector2(0, -30 + (Projectile.minionPos * -10)); // stacked above
			TargetPos = player.Center + new Vector2(0, -30 + (float)(Projectile.minionPos * Math.Sin(Projectile.ai[0]))).RotatedBy(Projectile.ai[0] * 0.1f * (Projectile.minionPos * 0.1f + 1));
			if (Projectile.position.Distance(player.position) > 500)
			{
				Projectile.tileCollide = false;
			}
			Latched = false;
		}
		if (player.HasMinionAttackTargetNPC)
		{
			TargetNPC = player.MinionAttackTargetNPC;
			TargetingEnemy = true;
			TargetPos = Main.npc[TargetNPC].Center;
			if (TargetIndex != TargetNPC)
			{
				Latched = false;
			}
		}

		if (Projectile.timeLeft == 3)
		{
			SoundEngine.PlaySound(SoundID.NPCDeath12);

			for (int i = 0; i < 30; i++)
			{
				Dust d2 = Dust.NewDustPerfect(Projectile.Center + new Vector2(-14, 0).RotatedBy(Projectile.rotation), DustID.Blood);
				d2.velocity = Vector2.Normalize(Projectile.velocity).RotatedByRandom(1) * Main.rand.NextFloat(4f, 6f);
				d2.noGravity = Main.rand.NextBool(5);
				d2.fadeIn = 1;
			}
		}

		if (!Latched)
		{
			TargetIndex = TargetNPC;
		}
		if (!Latched)
		{
			if (Projectile.Center.Distance(TargetPos) > 100 && !TargetingEnemy)
			{
				Projectile.velocity = Vector2.SmoothStep(Projectile.velocity + Projectile.Center.DirectionTo(TargetPos) * 0.1f, Projectile.Center.DirectionTo(TargetPos) * Projectile.velocity.Length(), 0.3f);
			}
			else if (TargetingEnemy)
			{
				Projectile.velocity = Vector2.SmoothStep(Projectile.velocity + Projectile.Center.DirectionTo(TargetPos) * 0.2f, Projectile.Center.DirectionTo(TargetPos) * Projectile.velocity.Length(), 0.3f);
			}

			if (Projectile.velocity.Length() >= 7f)
			{
				Projectile.velocity = Vector2.Lerp(Vector2.Normalize(Projectile.velocity) * 7f, Projectile.velocity, 0.9f);
			}
			if (Projectile.velocity.Length() <= 2)
			{
				Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 2f;
			}
			AvalonGlobalProjectile.AvoidOwnedMinions(Projectile);
		}
		else if (TargetIndex != -1)//Biting
		{
			if (Main.npc[TargetIndex].active && Main.npc[TargetIndex].Hitbox.Intersects(Projectile.Hitbox))
			{
				Dust d2 = Dust.NewDustPerfect(Projectile.Center + new Vector2(-14, 0).RotatedBy(Projectile.rotation), DustID.Blood);
				d2.velocity = Projectile.velocity.RotatedByRandom(1) * Main.rand.NextFloat(-6f, -1f);
				d2.noGravity = Main.rand.NextBool();
				d2.fadeIn = 1;

				Projectile.frameCounter += 2;
				Projectile.Center = Main.npc[TargetNPC].Hitbox.ClosestPointInRect(Projectile.Center + Main.npc[TargetNPC].velocity);
			}
			else
			{
				TargetIndex = 0;
				Latched = false;
			}
		}
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (target.whoAmI == TargetIndex && !Latched)
		{
			Latched = true;
		}
		if (Main.rand.NextBool(30))
		{
			Latched = false;
			Projectile.timeLeft = 60;
		}
		Projectile.velocity = Projectile.Center.DirectionTo(target.Center);
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (Projectile.timeLeft <= 2 && target.whoAmI == TargetIndex)
			return base.CanHitNPC(target);
		else
			return false;
	}
	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = true;
		return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
	}
	public override bool ShouldUpdatePosition()
	{
		return !Latched;
	}
	public void DrawChain(Vector2 start, Vector2 end, Color color)
	{
		start -= Main.screenPosition;
		end -= Main.screenPosition;
		Texture2D TEX = TextureAssets.Chain12.Value;
		int linklength = TEX.Height;
		Vector2 chain = end - start;

		float length = (float)chain.Length();
		int numlinks = (int)Math.Ceiling(length / linklength);
		Vector2[] links = new Vector2[numlinks];
		float rotation = (float)Math.Atan2(chain.Y, chain.X);
		Projectile P = Projectile;
		Player Pr = Main.player[P.owner];
		Player MyPr = Main.player[Main.myPlayer];
		for (int i = 0; i < numlinks; i++)
		{
			links[i] = start + chain / numlinks * i;

			Main.spriteBatch.Draw(TEX, links[i], new Rectangle(0, 0, TEX.Width, linklength), color, rotation + 1.57f, new Vector2(TEX.Width / 2f, linklength), 1f,
				SpriteEffects.None, 1f);
		}
	}
	public override bool PreDraw(ref Color lightColor)
	{
		//DrawChain(Main.player[Projectile.owner].Center, Projectile.Center, lightColor);
		return base.PreDraw(ref lightColor);
	}
}
