using Avalon.Items.Material.Bars;
using Avalon.Items.Material.Shards;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class FeroziumPickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 34;
        Item.height = 34;
        Item.damage = 17;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.crit += 6;
        Item.pick = 195;
        Item.rare = ItemRarityID.Lime;
        Item.useTime = 15;
        Item.knockBack = 3f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 250000;
        Item.useAnimation = 15;
        Item.UseSound = SoundID.Item1;
    }
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.AdamantiteBar, 15)
			.AddIngredient(ItemID.FrostCore)
			.AddIngredient(ModContent.ItemType<FrigidShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ItemID.TitaniumBar, 15)
			.AddIngredient(ItemID.FrostCore)
			.AddIngredient(ModContent.ItemType<FrigidShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<TroxiniumBar>(), 15)
			.AddIngredient(ItemID.FrostCore)
			.AddIngredient(ModContent.ItemType<FrigidShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
