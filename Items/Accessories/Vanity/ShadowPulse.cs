using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Vanity;

class ShadowPulse : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ModContent.RarityType<Rarities.QuibopsRarity>();
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 0, 45);
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().WorksInVanity = true;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Accessories;
    }
    public override void UpdateVanity(Player player)
    {
        player.GetModPlayer<AvalonPlayer>().PulseCharm = true;
        player.GetModPlayer<AvalonPlayer>().ShadowCharm = true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().PulseCharm = true;
        player.GetModPlayer<AvalonPlayer>().ShadowCharm = true;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<ShadowCharm>())
            .AddIngredient(ModContent.ItemType<PulseVeil>())
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
