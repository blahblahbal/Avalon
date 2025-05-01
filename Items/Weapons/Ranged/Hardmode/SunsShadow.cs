using Avalon.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Hardmode;

public class SunsShadow : ModItem
{
	public override void SetDefaults()
	{
		// todo: custom use sound
		Item.DefaultToBlowpipe(27, 3.5f, 4.5f, 40, 40);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 7);
	}
	//public override void UseItemFrame(Player player)
	//{
	//    player.bodyFrame.Y = player.bodyFrame.Height * 2;
	//}
	//public override Vector2? HoldoutOffset()
	//{
	//    return new Vector2(0, -7);
	//}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(4, -2);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Projectile P = Projectile.NewProjectileDirect(source, position - new Vector2(0, 4) + (Vector2.Normalize(velocity) * 42), velocity, ModContent.ProjectileType<SunsShadowProj>(), damage, knockback);
		P.ai[0] = type;
		return false;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.BeetleHusk, 8)
			.AddIngredient(ItemID.TurtleShell, 1)
			.AddIngredient(ItemID.ChlorophyteBar, 3)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
