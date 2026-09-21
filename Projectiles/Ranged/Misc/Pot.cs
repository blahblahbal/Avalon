using Avalon;
using Avalon.Items.Weapons.Ranged.Thrown;
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Avalon.Projectiles.Ranged.Misc;

public class Pot : ModProjectile
{
	public override string Texture => $"Terraria/Images/Tiles_{TileID.Pots}";
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
	}
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(20);
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.frame = Main.rand.Next(13);
	}
	public override void AI()
	{
		Projectile.ai[1]++;
		Projectile.rotation += Projectile.direction * 0.25f;
		if (Projectile.ai[1] > 15)
			Projectile.velocity.Y += 0.3f;
	}
	private Item? _spawnItem = null;
	public override void OnSpawn(IEntitySource source)
	{
		if (source is EntitySource_ItemUse_WithAmmo i)
			_spawnItem = i.Item;
	}
	public override void OnKill(int timeLeft)
	{

		if (Main.myPlayer == Projectile.owner && _spawnItem != null)
		{
			StatModifier damageModifier = Main.LocalPlayer.GetTotalDamage(Projectile.DamageType);
			damageModifier = damageModifier.CombineWith(Main.LocalPlayer.specialistDamage);
			damageModifier = damageModifier.CombineWith(new StatModifier((float)_spawnItem.damage / _spawnItem.OriginalDamage, 1));
			CombinedHooks.ModifyWeaponDamage(Main.LocalPlayer, _spawnItem, ref damageModifier);

			var effectRandom = new WeightedRandom<int>();
			effectRandom.Add(-1, 30); // Nothing
			effectRandom.Add(0, 12); // Grenade
			effectRandom.Add(1, 28); // Coins
			effectRandom.Add(2, 30); // Torch
			switch (effectRandom.Get())
			{
				case 0: // Grenade
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, -Projectile.oldVelocity.RotatedByRandom(2) * Main.rand.NextFloat(0.25f, 0.5f), ProjectileID.Grenade, (int)damageModifier.ApplyTo(30), 4, Projectile.owner);
					break;
				case 1: // Coins
					int coins = Main.rand.Next(3, 7);

					var coinRandom = new WeightedRandom<int>();
					coinRandom.Add(0, 60);
					coinRandom.Add(1, 30);
					coinRandom.Add(2, 10);

					for (int i = 0; i < coins; i++)
					{
						int coinType = coinRandom.Get();
						int damage = 6;
						if (coinType == 1)
							damage = 12;
						else if (coinType == 2)
							damage = 25;
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, -Projectile.oldVelocity.RotatedByRandom(2) * Main.rand.NextFloat(0.2f,0.75f), ModContent.ProjectileType<PotCoin>(), (int)damageModifier.ApplyTo(damage), 2, Projectile.owner, coinType);
					}
					break;
				case 2: // torch
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, -Projectile.oldVelocity.RotatedByRandom(2) * Main.rand.NextFloat(0.25f, 0.5f) + new Vector2(0,-2), ModContent.ProjectileType<PotTorch>(), (int)damageModifier.ApplyTo(2), 0, Projectile.owner);
					break;
			}
		}


		SoundEngine.PlaySound(SoundID.Shatter with { pitchVariance = 0.3f}, Projectile.position);
		for(int i = 0; i < 16; i++)
		{
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Pot);
			d.velocity *= 2;
		}

		int verticalVariants = 4;
		int horizontalVariants = 3;
		int firstPotGore = 51;
		switch ((Projectile.frame / horizontalVariants) % verticalVariants)
		{
			case 1:
				firstPotGore = 166;
				break;
			case 2:
				firstPotGore = 169;
				break;
			case 3:
				firstPotGore = 172;
				break;
		}
		for(int i = 0; i < 3; i++)
		{
			Gore g = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), Projectile.position, Main.rand.NextVector2Circular(3, 3), firstPotGore + i);
			g.timeLeft = 60;
		}
	}
	public override bool PreDraw(ref Color lightColor)
	{
		var tex = TextureAssets.Projectile[Type].Value;
		DrawData d = new DrawData(tex, Vector2.Zero, null, lightColor, 0, Vector2.Zero, Projectile.scale, SpriteEffects.None);
		int horizontalVariants = 3;
		int verticalVariants = 4;
		int frameOffsetX = (Projectile.frame % horizontalVariants) * 36;
		int frameOffsetY = ((Projectile.frame / horizontalVariants) % verticalVariants) * 36;

		for(int k = Projectile.oldPos.Length - 1; k > 0; k--)
		{
			d.position = Projectile.oldPos[k] + (Projectile.Size / 2) - Main.screenPosition;
			d.rotation = Projectile.oldRot[k];
			d.color = lightColor with { A = 200 } * (1f - (k / (float)Projectile.oldPos.Length)) * 0.75f;
			for (int i = 0; i < 4; i++)
			{
				Rectangle frame = new Rectangle((i % 2) * 18 + frameOffsetX, (i / 2) * 18 + frameOffsetY, 16, 16);
				Main.EntitySpriteDraw(d with {sourceRect = frame, origin = new Vector2(16 - (i % 2) * 16, 16 - (i / 2) * 16 + 8) });
			}
		}
		d.position = Projectile.Center - Main.screenPosition;
		d.rotation = Projectile.rotation;
		d.color = lightColor;
		for (int i = 0; i < 4; i++)
		{
			Rectangle frame = new Rectangle((i % 2) * 18 + frameOffsetX, (i / 2) * 18 + frameOffsetY, 16, 16);
			Main.EntitySpriteDraw(d with { sourceRect = frame, origin = new Vector2(16 - (i % 2) * 16, 16 - (i /2) * 16 + 8)});
		}
		return false;
	}
}
