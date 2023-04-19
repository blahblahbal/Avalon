using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Superhardmode;

class CrystalEdge : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Cyan;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 4, 0, 0);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().CrystalEdge = true;
    }
    //public override void AddRecipes()
    //{
    //    Recipe.Create(Type)
    //        .AddIngredient(ItemID.CrystalShard, 50)
    //        .AddIngredient(ItemID.SoulofMight, 10)
    //        .AddIngredient(ModContent.ItemType<Material.SoulofBlight>(), 5)
    //        .AddTile(TileID.TinkerersWorkbench).Register();
    //}
}
