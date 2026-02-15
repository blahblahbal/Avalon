using Avalon;
using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Items.Material;
using Avalon.Projectiles.Magic.Other;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Other;

public class BloodstainedEye : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSpellBook(ModContent.ProjectileType<BloodyTear>(), 32, 2f, 3, 14f, 30, 30, 1.1f, width: 16, height: 16);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 60);
		Item.UseSound = SoundID.NPCHit1;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		int numberProjectiles = Main.rand.NextBool(3) ? 2 : 1;
		for (int i = 0; i < numberProjectiles; i++)
		{
			Vector2 perturbedSpeed = AvalonUtils.GetShootSpread(velocity, position, Type, MathHelper.ToRadians(8), Main.rand.NextFloat(-4.2f, 0f), random: true);
			Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
		}
		return false;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<GlassEye>()).AddIngredient(ModContent.ItemType<BloodshotLens>()).AddIngredient(ModContent.ItemType<BottledLava>()).AddTile(TileID.Anvils).Register();
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(5, 0);
	}
}
