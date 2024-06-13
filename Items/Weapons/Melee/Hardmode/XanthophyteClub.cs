using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

class XanthophyteClub : ModItem
{
    // TODO: ADD PROJECTILE
    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 40;
        Item.UseSound = SoundID.Item1;
        Item.damage = 97;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1.2f;
        Item.rare = ItemRarityID.Yellow;
        Item.useTime = 24;
        Item.knockBack = 6.1f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 5, 52, 0);
        Item.useAnimation = 24;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.Bars.XanthophyteBar>(), 12)
            .AddIngredient(ModContent.ItemType<Material.Shards.VenomShard>())
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
