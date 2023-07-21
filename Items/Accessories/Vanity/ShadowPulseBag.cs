using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Avalon.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Vanity;

class ShadowPulseBag : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = (ContentSamples.CreativeHelper.ItemGroup)Data.Sets.ItemGroupValues.VanityBags;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.vanity = true;
        Item.value = Item.sellPrice(0, 2, 0, 0);
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().WorksInVanity = true;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Omnibag>())
            .AddIngredient(ModContent.ItemType<ShadowPulse>())
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().PulseCharm = true;
        player.GetModPlayer<AvalonPlayer>().ShadowCharm = true;
        if (!hideVisual)
        {
            UpdateVanity(player);
        }
    }

    public override void UpdateVanity(Player player)
    {
        player.GetModPlayer<AvalonPlayer>().PulseCharm = true;
        player.GetModPlayer<AvalonPlayer>().ShadowCharm = true;
        ModContent.GetInstance<Omnibag>().UpdateVanity(player);
    }
}
