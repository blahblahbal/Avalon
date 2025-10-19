using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode.GemStaves;

public class ZirconStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToStaff(ModContent.ProjectileType<ZirconBolt>(), 25, 4.75f, 9, 9.75f, 25, 25, true);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 0, 72);
		Item.UseSound = SoundID.Item43;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Bars.BismuthBar>(), 10)
			.AddIngredient(ModContent.ItemType<Material.Ores.Zircon>(), 8)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
public class ZirconBolt : ModProjectile
{
	public override string Texture => ModContent.GetInstance<ZirconStaff>().Texture;
	private static Color color = new Color(208, 142, 95) * 0.7f;
	private static int dustId = ModContent.DustType<Dusts.ZirconDust>();
	public override void SetDefaults()
	{
		Projectile.width = Projectile.height = 10;
		Projectile.alpha = 255;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.friendly = true;
		Projectile.aiStyle = -1;
		Projectile.penetrate = 2;
	}
	public override void AI()
	{
		for (var i = 0; i < 2; i++)
		{
			var dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustId, Projectile.velocity.X, Projectile.velocity.Y, 50, default, 1.2f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].velocity *= 0.3f;
		}
		if (Projectile.ai[1] == 0f)
		{
			Projectile.ai[1] = 1f;
			SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
		}

		Lighting.AddLight(new Vector2((int)((Projectile.position.X + Projectile.width / 2) / 16f), (int)((Projectile.position.Y + Projectile.height / 2) / 16f)), color.ToVector3());
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
		for (int num453 = 0; num453 < 15; num453++)
		{
			int num454 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustId, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 50, default, 1.2f);
			Main.dust[num454].noGravity = true;
			Dust dust152 = Main.dust[num454];
			Dust dust226 = dust152;
			dust226.scale *= 1.25f;
			dust152 = Main.dust[num454];
			dust226 = dust152;
			dust226.velocity *= 0.5f;
		}
	}
}
