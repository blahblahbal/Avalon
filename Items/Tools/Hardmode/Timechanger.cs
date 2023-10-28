using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

class Timechanger : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.useTime = 30;
        Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.shoot = ModContent.ProjectileType<Projectiles.Tools.Timechanger>();
        Item.value = Item.sellPrice(0, 2, 70);
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddRecipeGroup("Avalon:GoldBar", 30)
            .AddIngredient(ItemID.SoulofLight, 15)
            .AddIngredient(ItemID.SoulofNight, 15)
            .AddRecipeGroup("Avalon:Tier3Watch")
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
    public override void UpdateInventory(Player player)
    {
        player.accWatch = 3;
    }
}
