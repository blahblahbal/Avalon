using Avalon.Items.Material.Bars;
using Avalon.Items.Material.Shards;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

class FeroziumWaraxe : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 48;
        Item.damage = 30;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1.5f;
        Item.axe = 24;
        Item.crit += 2;
        Item.rare = ItemRarityID.Lime;
        Item.useTime = 20;
        Item.knockBack = 8f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 350000;
        Item.useAnimation = 20;
        Item.UseSound = SoundID.Item1;
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ItemID.AdamantiteBar, 12)
			.AddIngredient(ItemID.FrostCore)
            .AddIngredient(ModContent.ItemType<FrigidShard>())
            .AddTile(TileID.MythrilAnvil)
			.Register();

		Terraria.Recipe.Create(Type)
			.AddIngredient(ItemID.TitaniumBar, 12)
			.AddIngredient(ItemID.FrostCore)
			.AddIngredient(ModContent.ItemType<FrigidShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();

		Terraria.Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<TroxiniumBar>(), 12)
			.AddIngredient(ItemID.FrostCore)
			.AddIngredient(ModContent.ItemType<FrigidShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
