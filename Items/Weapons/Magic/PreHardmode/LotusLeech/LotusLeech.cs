using Avalon.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode.LotusLeech;

public class LotusLeech : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 26;
		Item.autoReuse = true;
		Item.damage = 0;
		Item.shoot = ModContent.ProjectileType<LotusLeechHeal>();
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.rare = ItemRarityID.Blue;
		Item.UseSound = SoundID.Item8;
		Item.useStyle = ItemUseStyleID.HoldUp;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{

		player.itemRotation += MathHelper.Pi / 6f * player.direction * player.gravDir;
		player.itemLocation -= new Vector2(player.direction * 10, player.gravDir * 10);
	}
	public override void UseAnimation(Player player)
	{
		bool Success = false;
		int Count = 0;
		foreach (var npc in Main.ActiveNPCs)
		{
			if (!npc.friendly && npc.lifeMax > 5 && npc.netID != NPCID.TargetDummy && npc.Distance(player.Center) < 400)
			{
				Count++;
				Success = true;
			}
			if (Count > 2)
			{
				break;
			}
		}

		if (!Success)
		{
			for (float j = 0; j < MathHelper.TwoPi; j += MathHelper.TwoPi / 32f)
			{
				var dust = Dust.NewDustPerfect(player.Center - new Vector2(player.direction * -14, player.gravDir * 24), DustID.ViciousPowder, new Vector2(0, 3).RotatedBy(j));
				dust.noGravity = true;
			}
		}
		else
		{
			for (float j = 0; j < MathHelper.TwoPi; j += MathHelper.TwoPi / 32f)
			{
				var dust = Dust.NewDustPerfect(player.Center - new Vector2(player.direction * -14, player.gravDir * 24), DustID.PurificationPowder, new Vector2(0, 3).RotatedBy(j));
				dust.noGravity = true;
			}
		}
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		bool Success = false;
		int Count = 0;
		foreach (var npc in Main.ActiveNPCs)
		{
			if (!npc.friendly && npc.lifeMax > 5 && npc.netID != NPCID.TargetDummy && npc.Distance(player.Center) < 400)
			{
				Count++;
				Success = true;
				Projectile P = Projectile.NewProjectileDirect(source, Main.rand.NextVector2FromRectangle(npc.Hitbox) + npc.velocity, npc.velocity, type, damage, knockback, player.whoAmI);
			}
			if (Count > 2)
			{
				break;
			}
		}

		if (!Success)
		{
			player.CheckMana(25, true);
			player.manaRegenDelay = 100;
			player.AddBuff(BuffID.ManaSickness, 60 * 5);
			player.AddBuff(ModContent.BuffType<LotusCurse>(), 60 * 7);
		}

		return false;
	}
}
public class LotusLeechHeal : ModProjectile
{
	public override string Texture => ModContent.GetInstance<LotusLeech>().Texture;
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(6);
		Projectile.alpha = 255;
		Projectile.friendly = true;
		Projectile.aiStyle = -1;
		Projectile.timeLeft = 60 * 2;
		Projectile.tileCollide = false;
	}

	public override void AI()
	{
		if (Projectile.ai[1] == 0f)
		{
			Projectile.oldPosition = Projectile.position;
			Projectile.ai[1] = 1f;
			SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
			for (float j = 0; j < MathHelper.TwoPi; j += MathHelper.TwoPi / 32f)
			{
				var dust = Dust.NewDustPerfect(Projectile.Center, DustID.PurificationPowder, new Vector2(0, 3).RotatedBy(j));
				dust.noGravity = true;
			}
		}

		if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
		{
			Projectile.tileCollide = true;
		}

		for (float i = 0; i < 1; i += 0.2f)
		{
			var dust = Dust.NewDustPerfect(Vector2.Lerp(Projectile.Center, Projectile.oldPosition + (Projectile.Size / 2), i), DustID.PurificationPowder, Vector2.Zero);
			dust.noGravity = true;
			dust.scale = 1;
			dust.frame.Y = 0;
		}

		Player player = Main.player[Projectile.owner];
		Projectile.velocity += Projectile.Center.DirectionTo(player.Center);
		Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.Center.DirectionTo(player.Center) * Projectile.velocity.Length(), 0.04f);

		if (Projectile.velocity.Length() > 10)
		{
			Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 10;
		}

		if (Projectile.Hitbox.Intersects(player.Hitbox))
		{
			player.statMana += 7;
			CombatText.NewText(player.Hitbox, CombatText.HealMana, 7);
			Projectile.Kill();

			SoundEngine.PlaySound(SoundID.MaxMana, Projectile.position);
		}

		if (!player.active || player.dead)
		{
			Projectile.Kill();
		}
	}
}
public class LotusCurse : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[Type] = true;
	}
	public override void Update(Player player, ref int buffIndex)
	{
		player.maxRunSpeed /= 2f;
		player.runAcceleration /= 2;
		player.accRunSpeed /= 2;
		player.endurance -= 0.3f;
		player.GetDamage(DamageClass.Magic) /= 2;

		Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.ViciousPowder);
		d.noGravity = true;
		d.velocity = Vector2.Zero;
	}
}