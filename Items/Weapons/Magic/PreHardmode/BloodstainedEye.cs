using Avalon.Common.Extensions;
using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode;

public class BloodstainedEye : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSpellBook(ModContent.ProjectileType<Projectiles.Magic.BloodyTear>(), 34, 2f, 3, 14f, 30, 30, 1.1f, width: 16, height: 16);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 60);
		Item.UseSound = SoundID.NPCHit1;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		int numberProjectiles = 1 + Main.rand.Next(2);
		for (int i = 0; i < numberProjectiles; i++)
		{
			Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(8));
			float scale = 1f - (Main.rand.NextFloat() * .3f);
			perturbedSpeed = perturbedSpeed * scale;
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
