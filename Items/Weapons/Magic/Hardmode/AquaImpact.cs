using Avalon.Items.Material.Shards;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode;

public class AquaImpact : ModItem
{
	public override void SetDefaults()
	{
		Item.DamageType = DamageClass.Magic;
		Item.damage = 61;
		Item.autoReuse = true;
		Item.shootSpeed = 7f;
		Item.mana = 10;
		Item.rare = ItemRarityID.Yellow;
		Item.noMelee = true;
		Item.width = 16;
		Item.useTime = 25;
		Item.knockBack = 5.5f;
		Item.shoot = ModContent.ProjectileType<Projectiles.Magic.AquaBlast>();
		Item.UseSound = SoundID.Item21;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.value = Item.sellPrice(0, 25, 0, 0);
		Item.useAnimation = 25;
		Item.height = 16;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.WaterBolt)
			.AddIngredient(ModContent.ItemType<TorrentShard>(), 6)
			.AddIngredient(ItemID.SoulofMight, 15)
			.AddIngredient(ItemID.Bubble, 25)
			.AddTile(TileID.Bookcases)
			.Register();
	}
}
