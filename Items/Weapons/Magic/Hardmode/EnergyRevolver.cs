using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode;

public class EnergyRevolver : ModItem
{
	SoundStyle LaserNoise = new("Terraria/Sounds/Item_91")
	{
		Volume = 0.5f,
		PitchVariance = 0.1f,
		MaxInstances = 7,
		Pitch = 1.6f
	};
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeapon(50, 20, ModContent.ProjectileType<Projectiles.Magic.EnergyLaser>(), 36, 2f, 6, 5f, 6, 6, true);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 4);
		Item.UseSound = LaserNoise;
	}
	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		if (ModContent.GetInstance<Common.AvalonClientConfig>().AdditionalScreenshakes)
		{
			UseStyles.gunStyle(player, 0, 2);
		}
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 newPos = position + new Vector2(0, -6 * player.direction).RotatedBy(velocity.ToRotation());
		Vector2 beamStartPos = newPos + Vector2.Normalize(velocity) * 50;
		Projectile.NewProjectile(source, newPos, velocity, type, damage, knockback, player.whoAmI, beamStartPos.X, beamStartPos.Y);
		ParticleSystem.AddParticle(new EnergyRevolverParticle(), beamStartPos, Vector2.Normalize(velocity) * 2, new Color(64, 255, 255, 0), 0, 0.8f, 14);
		ParticleSystem.AddParticle(new EnergyRevolverParticle(), beamStartPos, default, new Color(64, 64, 255, 0), 0, 1, 20);
		return false;
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-4f, 0);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.LaserRifle)
			.AddIngredient(ItemID.Lens, 10)
			.AddIngredient(ModContent.ItemType<Material.BloodshotLens>(), 5)
			.AddIngredient(ItemID.BlackLens)
			.AddIngredient(ItemID.SoulofFright, 16)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
