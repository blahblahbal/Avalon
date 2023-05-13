using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

class GoldminePickaxe : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 28;
        Item.UseSound = SoundID.Item1;
        Item.damage = 10;
        Item.autoReuse = true;
        Item.useTurn = true;
        //Item.scale = 1.15f;
        Item.pick = 69;
        Item.rare = ItemRarityID.Blue;
        Item.useTime = 13;
        Item.knockBack = 3f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 0, 36, 0);
        Item.useAnimation = 21;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.Bars.BacciliteBar>(), 12)
            .AddIngredient(ModContent.ItemType<Material.Booger>(), 6)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
