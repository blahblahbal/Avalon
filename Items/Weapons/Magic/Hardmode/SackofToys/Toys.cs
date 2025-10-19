
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode.SackofToys;
public class Die : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Projectile.type] = 2;
	}
	public override void SetDefaults()
	{
		Projectile.width = 18;
		Projectile.height = 18;
		Projectile.aiStyle = 1;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.penetrate = -1;
		Projectile.alpha = 0;
		Projectile.friendly = true;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		Projectile.ai[2]++;
		if (Projectile.ai[2] >= 4f)
		{
			Projectile.position += Projectile.velocity;
			Projectile.Kill();
		}
		else
		{
			if (Projectile.velocity.Y != oldVelocity.Y)
			{
				Projectile.velocity.Y = -oldVelocity.Y;
			}
			if (Projectile.velocity.X != oldVelocity.X)
			{
				Projectile.velocity.X = -oldVelocity.X;
			}
		}
		return false;
	}

	public override void OnSpawn(IEntitySource source)
	{
		switch (Main.rand.Next(2))
		{
			case 0:
				Projectile.frame = 0;
				break;
			case 1:
				Projectile.frame = 1;
				break;

		}
	}

	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation();
	}
}
public class Doll : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = 28;
		Projectile.height = 32;
		Projectile.aiStyle = 1;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.penetrate = -1;
		Projectile.alpha = 0;
		Projectile.friendly = true;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		if (Projectile.velocity.X != oldVelocity.X)
		{
			Projectile.velocity.X = -oldVelocity.X * 0.7f;
		}
		if (Projectile.velocity.Y != oldVelocity.Y)
		{
			Projectile.velocity.Y = -oldVelocity.Y * 0.7f;
		}
		return false;
	}

	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation();
	}
}
public class Lego : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Projectile.type] = 3;
	}
	public override void SetDefaults()
	{
		Projectile.width = 18;
		Projectile.height = 18;
		Projectile.aiStyle = 1;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.penetrate = -1;
		Projectile.alpha = 0;
		Projectile.friendly = true;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		if (Projectile.velocity.X != oldVelocity.X)
		{
			Projectile.velocity.X = -oldVelocity.X;
		}
		if (Projectile.velocity.Y != oldVelocity.Y)
		{
			Projectile.velocity.Y = 0f;
		}
		return false;
	}

	public override void OnSpawn(IEntitySource source)
	{
		switch (Main.rand.Next(3))
		{
			case 0:
				Projectile.frame = 0;
				break;
			case 1:
				Projectile.frame = 1;
				break;
			case 2:
				Projectile.frame = 2;
				break;

		}
	}

	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation();
	}
}
public class Marble : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Projectile.type] = 3;
	}
	public override void SetDefaults()
	{
		Projectile.width = 18;
		Projectile.height = 18;
		Projectile.aiStyle = 1;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.penetrate = -1;
		Projectile.alpha = 0;
		Projectile.friendly = true;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		if (Projectile.velocity.X != oldVelocity.X)
		{
			Projectile.velocity.X = -oldVelocity.X;
		}
		if (Projectile.velocity.Y != oldVelocity.Y)
		{
			Projectile.velocity.Y = 0f;
		}
		return false;
	}

	public override void OnSpawn(IEntitySource source)
	{
		switch (Main.rand.Next(3))
		{
			case 0:
				Projectile.frame = 0;
				break;
			case 1:
				Projectile.frame = 1;
				break;
			case 2:
				Projectile.frame = 2;
				break;

		}
	}

	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation();
	}
}
public class RockingHorse : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = 42;
		Projectile.height = 36;
		Projectile.aiStyle = 1;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.penetrate = -1;
		Projectile.alpha = 0;
		Projectile.friendly = true;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		if (Projectile.velocity.X != oldVelocity.X)
		{
			Projectile.velocity.X = -oldVelocity.X * 0.7f;
		}
		if (Projectile.velocity.Y != oldVelocity.Y)
		{
			Projectile.velocity.Y = -oldVelocity.Y * 0.7f;
		}
		return false;
	}

	public override void AI()
	{
		Projectile.ai[0]++;
		if (Projectile.ai[0] > 5)
		{
			Projectile.velocity.Y += 0.3f;
			Projectile.ai[0] = 0;
		}
	}
}
public class Table : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = 42;
		Projectile.height = 36;
		Projectile.aiStyle = 1;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.penetrate = -1;
		Projectile.alpha = 0;
		Projectile.friendly = true;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		if (Projectile.velocity.X != oldVelocity.X)
		{
			Projectile.velocity.X = -oldVelocity.X * 0.7f;
		}
		if (Projectile.velocity.Y != oldVelocity.Y)
		{
			Projectile.velocity.Y = -oldVelocity.Y * 0.7f;
		}
		return false;
	}

	public override void AI()
	{
		Projectile.ai[0]++;
		if (Projectile.ai[0] > 5)
		{
			Projectile.velocity.Y += 0.3f;
			Projectile.ai[0] = 0;
		}
	}
}
public class Vase : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = 18;
		Projectile.height = 18;
		Projectile.aiStyle = 1;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.penetrate = -1;
		Projectile.alpha = 0;
		Projectile.friendly = true;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		Projectile.ai[2]++;
		if (Projectile.ai[2] >= 4f)
		{
			Projectile.position += Projectile.velocity;
			Projectile.Kill();
		}
		else
		{
			if (Projectile.velocity.Y != oldVelocity.Y)
			{
				Projectile.velocity.Y = -oldVelocity.Y;
			}
			if (Projectile.velocity.X != oldVelocity.X)
			{
				Projectile.velocity.X = -oldVelocity.X;
			}
		}
		return false;
	}

	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation() / Projectile.ai[2];
	}
}
