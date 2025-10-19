using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode.RottenApple;

public class RottenApple : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 99;
	}
	public override void SetDefaults()
	{
		Item.DefaultToThrownWeapon(ModContent.ProjectileType<RottenAppleProj>(), 20, 3f, 9f, 15);
	}
	public override void AddRecipes()
	{
		CreateRecipe(20)
			.AddIngredient(ItemID.Apple)
			.AddIngredient(ModContent.ItemType<Material.Shards.UndeadShard>())
			.AddTile(TileID.WorkBenches)
			.Register();

		CreateRecipe(20)
			.AddIngredient(ItemID.Apple)
			.AddIngredient(ModContent.ItemType<Material.RottenFlesh>(), 2)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
public class RottenAppleProj : ModProjectile
{
	public override string Texture => ModContent.GetInstance<RottenApple>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<RottenApple>().DisplayName;
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Projectile.width = 12;
		Projectile.height = 12;
		Projectile.aiStyle = 2;
		Projectile.friendly = true;
		Projectile.penetrate = 1;
		Projectile.DamageType = DamageClass.Ranged;
		AIType = ProjectileID.RottenEgg;
		DrawOffsetX = -(int)((dims.Width / 2) - (Projectile.Size.X / 2));
		DrawOriginOffsetY = -(int)((dims.Width / 2) - (Projectile.Size.Y / 2));
	}
	public override bool? CanHitNPC(NPC target)
	{
		return target.lifeMax >= 1 && !target.dontTakeDamage;
	}
	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (target.type == NPCID.Nurse || target.type == NPCID.DoctorBones || target.type == NPCID.WitchDoctor || target.type == NPCID.DrManFly || target.type == NPCID.ZombieDoctor)
		{
			modifiers.FinalDamage *= 3f;
		}
	}
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.Center);
		for (int i = 0; i < 10; i++)
		{
			int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.CorruptGibs, -Projectile.velocity.X / 3, -Projectile.velocity.Y / 3, Projectile.alpha);
			Main.dust[d].noGravity = !Main.rand.NextBool(3);
			if (Main.dust[d].noGravity)
				Main.dust[d].fadeIn = 1f;
		}
	}
}
