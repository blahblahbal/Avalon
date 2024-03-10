using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class GloveofCreation : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 10);
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().WorksInVanity = true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().CloudGlove = true;
        player.GetModPlayer<AvalonPlayer>().ObsidianGlove = true;
        player.equippedAnyWallSpeedAcc = true;
        player.equippedAnyTileSpeedAcc = true;
        player.autoPaint = true;
        player.equippedAnyTileRangeAcc = true;
        player.treasureMagnet = true;
        player.chiselSpeed = true;
        player.portableStoolInfo.SetStats(26, 26, 26);
    }
    public override void UpdateVanity(Player player)
    {
        UpdateAccessory(player, false);
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<ObsidianGlove>())
            .AddIngredient(ItemID.HandOfCreation)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
