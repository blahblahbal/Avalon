using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon.Items.Weapons.Ranged.PreHardmode.OsmiumTierLongbows;
public class RhodiumLongbowHeld : LongbowTemplate
{
	public override void SetDefaults()
	{
		base.SetDefaults();
		DrawOffsetX = -16;
		DrawOriginOffsetY = -25;
	}
	public override void Shoot(IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, float Power)
	{
		Projectile P = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Projectile.owner);
		if (Power == 1)
		{
			SoundEngine.PlaySound(SoundID.Item110);
			P.GetGlobalProjectile<OsmiumTierLongbowGlobalProj>().SpawnDust = true;
			P.GetGlobalProjectile<LongbowGlobalProj>().LongbowArrow = true;
			if (P.penetrate > 0)
				P.penetrate += 2;
			P.ai[0] = -100;
			P.usesLocalNPCImmunity = true;
			P.localNPCHitCooldown = 30;
			P.extraUpdates++;
			P.netUpdate = true;
		}
	}
}
public class OsmiumTierLongbowGlobalProj : GlobalProjectile
{
	public override bool InstancePerEntity => true;
	public bool SpawnDust;
	private bool SpawnedDust;
	public override void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter)
	{
		bitWriter.WriteBit(SpawnDust);
	}
	public override void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader)
	{
		SpawnDust = bitReader.ReadBit();
	}
	public override void PostAI(Projectile projectile)
	{
		if (SpawnDust && !SpawnedDust)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust d = Dust.NewDustDirect(projectile.Center, 0, 0, DustID.Snow);
				d.noGravity = true;
				d.velocity = projectile.velocity.RotatedByRandom(0.1f) * i * 0.1f;
				d.alpha = 25;
				d.color = new Color(200, 200, 200);
			}
			projectile.netUpdate = true;
			SpawnedDust = true;
		}
	}
}
public class OsmiumLongbowHeld : RhodiumLongbowHeld { }
public class IridiumLongbowHeld : RhodiumLongbowHeld { }
