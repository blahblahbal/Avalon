using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode.GemStaves;

public class TourmalineStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToStaff(ModContent.ProjectileType<TourmalineBolt>(), 18, 3.5f, 5, 6.5f, 38, 38, true);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 7);
		Item.UseSound = SoundID.Item43;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Bars.BronzeBar>(), 10)
			.AddIngredient(ModContent.ItemType<Material.Ores.Tourmaline>(), 8)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
public class TourmalineBolt : ModProjectile
{
	public override string Texture => ModContent.GetInstance<TourmalineStaff>().Texture;
	private static Color color = new Color(27, 247, 229) * 0.7f;
	private static int dustId = ModContent.DustType<Dusts.TourmalineDust>();

	public override void SetDefaults()
	{
		Projectile.CloneDefaults(ProjectileID.SapphireBolt);
		Projectile.aiStyle = -1;
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

		Lighting.AddLight(new Vector2((int)((Projectile.position.X + (float)(Projectile.width / 2)) / 16f), (int)((Projectile.position.Y + (float)(Projectile.height / 2)) / 16f)), color.ToVector3());
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
