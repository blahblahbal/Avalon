using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Superhardmode;
public class LoadedDie : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Cyan;
        Item.Size = new Vector2(16);
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 6);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().LoadedDie = true;
        player.GetModPlayer<AvalonPlayer>().CrystalEdge = true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(1)
            .AddIngredient(ModContent.ItemType<Hardmode.SixSidedDie>())
            .AddIngredient(ModContent.ItemType<CrystalEdge>())
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
