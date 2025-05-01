using Avalon.Common.Extensions;
using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode;

public class PurpleHaze : ModItem
{
	public SoundStyle gas = new("Terraria/Sounds/Item_34")
	{
		Volume = 0.5f,
		Pitch = -0.5f,
		PitchVariance = 1.5f,
		MaxInstances = 10,
	};
	public override void SetDefaults()
	{
		Item.DefaultToSpellBook(ModContent.ProjectileType<Projectiles.Magic.PurpleHaze>(), 26, 1.5f, 5, 8f, 7, 21);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 4);
		Item.UseSound = gas;
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(10, 0);
	}

	public override void AddRecipes()
	{
		Recipe.Create(Type).AddIngredient(ItemID.SpellTome).AddIngredient(ModContent.ItemType<Pathogen>(), 20).AddIngredient(ItemID.SoulofNight, 15).AddTile(TileID.Bookcases).Register();
	}
	//public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	//{
	//    type = Main.rand.Next(new int[] { ModContent.ProjectileType<Projectiles.Melee.VirulentCloudWeak>(), ModContent.ProjectileType<Projectiles.Melee.VirulentCloudSmallWeak>() });
	//    SoundEngine.PlaySound(gas, player.Center);
	//    for (int num194 = 0; num194 < 2; num194++)
	//    {
	//        float num195 = velocity.X;
	//        float num196 = velocity.Y;
	//        num195 += Main.rand.Next(-40, 41) * 0.05f;
	//        num196 += Main.rand.Next(-40, 41) * 0.05f;
	//        Projectile.NewProjectile(source, position.X, position.Y, num195, num196, type, damage, knockback, player.whoAmI, 0f, 0f);
	//    }
	//    return false;
	//}
}
