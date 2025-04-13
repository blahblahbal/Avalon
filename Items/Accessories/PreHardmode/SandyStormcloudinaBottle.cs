using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

class SandyStormcloudinaBottle : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Orange;
        Item.width = 20;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 3, 0, 0);
        Item.height = 26;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetJumpState<CloudInABottleJump>().Enable();
        player.GetJumpState<BlizzardInABottleJump>().Enable();
        player.GetJumpState<SandstormInABottleJump>().Enable();
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.CloudinaBottle)
            .AddIngredient(ItemID.BlizzardinaBottle)
            .AddIngredient(ItemID.SandstorminaBottle)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
