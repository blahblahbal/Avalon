using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Vanity;

class AncientHeadphones : ModItem
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
        Item.accessory = true;
        Item.vanity = true;
        Item.value = Item.sellPrice(0, 2, 0, 0);
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().WorksInVanity = true;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Accessories;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.MusicBox)
            .AddRecipeGroup(RecipeGroupID.IronBar, 7)
            .AddIngredient(ItemID.FallenStar, 10)
            .AddTile(TileID.Anvils)
            .Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().AncientHeadphones = true;
        if (!hideVisual)
        {
            UpdateVanity(player);
        }
    }

    public override void UpdateVanity(Player player)
    {
        player.GetModPlayer<AvalonPlayer>().AncientHeadphones = true;
    }
}
