using Avalon.Common;
using Avalon.Items.Material.TomeMats;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes;

class BurningDesire : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.value = 15000;
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.statLifeMax2 += 40;
        player.statManaMax2 += 40;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Gravel>(), 7)
            .AddIngredient(ModContent.ItemType<RubybeadHerb>(), 3)
            .AddIngredient(ItemID.LifeCrystal)
            .AddIngredient(ModContent.ItemType<MysticalTomePage>(), 4)
            .AddTile(ModContent.TileType<Tiles.TomeForge>())
            .Register();
    }
}
