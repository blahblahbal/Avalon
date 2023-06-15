using Avalon.Common;
using Avalon.Items.Material.TomeMats;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes;

class MediationsFlame : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.Size = new(30);
        Item.value = 5000;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Magic) += 0.05f;
        player.manaCost -= 0.1f;
        player.statManaMax2 += 60;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<RubybeadHerb>(), 5)
            .AddIngredient(ModContent.ItemType<FineLumber>(), 20)
            .AddIngredient(ItemID.FallenStar, 25)
            .AddIngredient(ItemID.MeteoriteBar, 10)
            .AddIngredient(ModContent.ItemType<MysticalTomePage>(), 2)
            .AddTile(ModContent.TileType<Tiles.TomeForge>())
            .Register();
    }
}
