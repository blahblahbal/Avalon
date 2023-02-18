using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ExxoAvalonOrigins.Common;
using ExxoAvalonOrigins.Items.Material.TomeMats;

namespace ExxoAvalonOrigins.Items.Tomes;

class TheOasisRemembered : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 40);
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Summon) += 0.2f;
        player.GetKnockback(DamageClass.Summon) += 0.2f;
    }

    //public override void AddRecipes()
    //{
    //    CreateRecipe(1)
    //        .AddIngredient(ModContent.ItemType<DragonOrb>())
    //        .AddIngredient(ModContent.ItemType<HydrolythBar>(), 25)
    //        .AddIngredient(ModContent.ItemType<SoulofBlight>(), 10)
    //        .AddIngredient(ModContent.ItemType<MysticalTomePage>(), 5)
    //        .AddTile(ModContent.TileType<Tiles.TomeForge>())
    //        .Register();
    //}
}
