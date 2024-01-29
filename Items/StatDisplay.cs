using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items;

class StatDisplay : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return false;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Purple;
        Item.useAnimation = Item.useTime = 20;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.width = dims.Width;
        Item.maxStack = 1;
        Item.value = 0;
        Item.height = dims.Height;
    }
    public override bool? UseItem(Player player)
    {
        player.GetModPlayer<AvalonPlayer>().DisplayStats = !player.GetModPlayer<AvalonPlayer>().DisplayStats;
        return true;
    }
}
