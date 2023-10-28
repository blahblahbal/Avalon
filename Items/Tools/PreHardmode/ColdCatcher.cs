using Avalon.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

internal class ColdCatcher : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 28;
        Item.shootSpeed = 16.5f;
        Item.rare = ItemRarityID.Blue;
        Item.useTime = 8;
        Item.fishingPole = 25;
        Item.shoot = ModContent.ProjectileType<ColdCatcherBobber>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 2, 80);
        Item.useAnimation = 8;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.Bars.BacciliteBar>(), 10)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
