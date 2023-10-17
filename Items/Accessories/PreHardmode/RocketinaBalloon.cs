using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

public class RocketinaBalloon : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(gold: 3);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetJumpState<RocketBottleJump>().Enable();
        player.jumpBoost = true;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<RocketinaBottle>())
            .AddIngredient(ItemID.ShinyRedBalloon)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
