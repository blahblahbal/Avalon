using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class DurataniumOmegaShield : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 4;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.value = 100000;
        Item.accessory = true;
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().CobShield = true;
        player.GetModPlayer<AvalonPlayer>().PallShield = true;
        player.GetModPlayer<AvalonPlayer>().DuraShield = true;
        player.GetModPlayer<AvalonPlayer>().DuraOmegaShield = true;
        player.noKnockback = true;
        player.GetModPlayer<AvalonPlayer>().TrapImmune = true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<CobaltCrossShield>())
            .AddIngredient(ModContent.ItemType<PalladiumCrossShield>())
            .AddIngredient(ModContent.ItemType<DurataniumCrossShield>())
            .AddIngredient(ItemID.SoulofFright, 5)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
