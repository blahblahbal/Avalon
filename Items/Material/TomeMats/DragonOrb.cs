using ExxoAvalonOrigins.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Material.TomeMats;

class DragonOrb : ModItem
{
    public override void SetStaticDefaults()
    {
        Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 8));
        ItemID.Sets.ItemNoGravity[Item.type] = true;
        Item.ResearchUnlockCount = 2;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 2, 0);
        Item.maxStack = 9999;
        Item.height = 26;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeMaterial = true;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<ElementDust>(), 3)
            .AddIngredient(ModContent.ItemType<ElementDiamond>(), 2)
            .AddIngredient(ModContent.ItemType<RubybeadHerb>(), 6)
            .AddIngredient(ModContent.ItemType<DewOrb>(), 3)
            .AddIngredient(ModContent.ItemType<StrongVenom>(), 5)
            .AddIngredient(ModContent.ItemType<MysticalTotem>())
            .AddIngredient(ModContent.ItemType<DewofHerbs>(), 3)
            .AddIngredient(ModContent.ItemType<MysticalClaw>(), 4)
            .AddTile(ModContent.TileType<Tiles.TomeForge>())
            .Register();
    }
}
