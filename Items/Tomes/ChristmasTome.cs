using Avalon.Common;
using Avalon.Items.Material.TomeMats;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes;

class ChristmasTome : ModItem
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
        Item.value = 15000;
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetCritChance(DamageClass.Magic) += 3;
        player.GetCritChance(DamageClass.Melee) += 3;
        player.GetCritChance(DamageClass.Ranged) += 3;
        player.GetCritChance(DamageClass.Throwing) += 3;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<MysticalClaw>(), 3)
            .AddIngredient(ModContent.ItemType<MysteriousPage>(), 2)
            .AddIngredient(ModContent.ItemType<Sandstone>(), 5)
            .AddIngredient(ModContent.ItemType<DewOrb>())
            .AddIngredient(ModContent.ItemType<MysticalTomePage>(), 2)
            .AddTile(ModContent.TileType<Tiles.TomeForge>())
            .Register();
    }
}
