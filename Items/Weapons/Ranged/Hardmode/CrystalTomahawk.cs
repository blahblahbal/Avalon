using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Hardmode
{
	public class CrystalTomahawk : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 2000;
		}
		public override void SetDefaults()
		{
			Item.DefaultToThrownWeapon(ModContent.ProjectileType<Projectiles.Ranged.CrystalTomahawk>(), 44, 5f, 17f, 10, width: 26, height: 26);
			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.sellPrice(copper: 14);
		}
		public override void AddRecipes()
		{
			CreateRecipe(555)
				.AddIngredient(ItemID.CrystalShard, 5)
				.AddIngredient(ItemID.SoulofLight)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
