using Avalon.Common.Extensions;
using Avalon.Projectiles.Magic.Tomes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Tomes;

public class MagmafrostBolt : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSpellBook(ModContent.ProjectileType<MagmafrostBoltProj>(), 67, 5f, 10, 1.3f, 15, 15);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 10);
		Item.UseSound = SoundID.Item21;
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(6, 2);
	}
	//public override void AddRecipes()
	//{
	//    CreateRecipe(1).AddIngredient(ModContent.ItemType<FreezeBolt>()).AddIngredient(ModContent.ItemType<Material.DragonScale>(), 10).AddIngredient(ModContent.ItemType<Material.LifeDew>(), 50).AddIngredient(ItemID.LivingFireBlock, 40).AddTile(TileID.MythrilAnvil).Register();
	//}
}