using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

class JunglePickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 40;
        Item.UseSound = SoundID.Item1;
        Item.damage = 7;
        Item.autoReuse = true;
        Item.pick = 56;
        Item.useTime = 20;
        Item.knockBack = 3f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.rare = ItemRarityID.Orange;
        Item.value = 18000;
        Item.useAnimation = 20;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Stinger, 10)
            .AddIngredient(ItemID.JungleSpores, 12)
            .AddIngredient(ModContent.ItemType<Material.Shards.ToxinShard>())
            .AddTile(TileID.Anvils)
            .Register();
    }
}
