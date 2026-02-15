using Avalon;
using Avalon.Common.Extensions;
using Avalon.PlayerDrawLayers;
using Avalon.Projectiles.Magic.Wands;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Wands;

public class CrystalUnity : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Item.type] = true;
		ItemGlowmask.AddGlow(this, 255);
	}
	public override void SetDefaults()
	{
		Item.DefaultToStaff(ModContent.ProjectileType<CrystalUnityShard>(), 46, 1.5f, 14, 13f, 11, 11, true);
		Item.scale = 0.9f;
		Item.reuseDelay = 14;
		Item.rare = ModContent.RarityType<Rarities.FractureRarity>();
		Item.value = Item.sellPrice(0, 10, 10);
		Item.UseSound = SoundID.Item43;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddRecipeGroup("Avalon:GemStaves", 2)
			.AddIngredient(ItemID.CrystalStorm)
			.AddIngredient(ModContent.ItemType<Material.TomeMats.ElementDiamond>(), 2)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		int x = Main.rand.Next(9);
		if (Main.rand.NextBool(15))
		{
			x = 9;
		}

		for (int spread = 0; spread < 3; spread++)
		{
			int dmg = Item.damage;
			if (x == 9) dmg = (int)(dmg * 2.5f);
			Projectile.NewProjectile(source, position, velocity * Main.rand.NextFloat(0.8f, 1.2f), type, (int)player.GetDamage(DamageClass.Magic).ApplyTo(dmg), knockback, player.whoAmI, Main.rand.NextFloat() - 0.5f, 0f, x);
		}
		return false;
	}
}