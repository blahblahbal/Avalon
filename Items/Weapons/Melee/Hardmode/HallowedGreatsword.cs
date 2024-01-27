using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

class HallowedGreatsword : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 42;
        Item.UseSound = SoundID.Item1;
        Item.damage = 30;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.rare = ItemRarityID.Pink;
        Item.useTime = 18;
        Item.knockBack = 8f;
        Item.DamageType = DamageClass.Melee;
        Item.autoReuse = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 5, 0, 0);
        Item.useAnimation = 18;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.HallowedBar, 22)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
