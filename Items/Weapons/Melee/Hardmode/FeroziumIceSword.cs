using Avalon.Items.Material.Bars;
using Avalon.Items.Material.Shards;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

class FeroziumIceSword : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 46;
        Item.height = 50;
        Item.damage = 50;
        Item.autoReuse = true;
        Item.useTurn = false;
        Item.scale = 1.5f;
        Item.shootSpeed = 15f;
        Item.crit += 2;
        Item.rare = ItemRarityID.Pink;
        Item.useTime = 20;
        Item.knockBack = 6f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.IcicleFerozium>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 350000;
        Item.useAnimation = 20;
        Item.UseSound = SoundID.Item1;
    }
    public override void AddRecipes()
    {
		Terraria.Recipe.Create(Type)
			.AddIngredient(ItemID.AdamantiteBar, 18)
			.AddIngredient(ItemID.FrostCore)
			.AddIngredient(ModContent.ItemType<FrigidShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();

		Terraria.Recipe.Create(Type)
			.AddIngredient(ItemID.TitaniumBar, 18)
			.AddIngredient(ItemID.FrostCore)
			.AddIngredient(ModContent.ItemType<FrigidShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();

		Terraria.Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<TroxiniumBar>(), 18)
			.AddIngredient(ItemID.FrostCore)
			.AddIngredient(ModContent.ItemType<FrigidShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
