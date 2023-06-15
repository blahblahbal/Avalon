using Avalon.Common;
using Avalon.Items.Material.TomeMats;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes;

class TaleoftheRedLotus : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.value = 5000;
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Ranged) += 0.05f;
        player.statLifeMax2 += 20;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<DewOrb>(), 6)
            .AddIngredient(ModContent.ItemType<CarbonSteel>(), 5)
            .AddIngredient(ModContent.ItemType<Sandstone>(), 10)
            .AddIngredient(ItemID.FallenStar, 15)
            .AddIngredient(ModContent.ItemType<MysticalTomePage>(), 4)
            .AddTile(ModContent.TileType<Tiles.TomeForge>())
            .Register();
    }
}
