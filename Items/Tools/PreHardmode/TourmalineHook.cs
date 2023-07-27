using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

class TourmalineHook : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.noUseGraphic = true;
        Item.useTurn = true;
        Item.shootSpeed = 16f;
        Item.rare = ItemRarityID.Blue;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 20;
        Item.shoot = ModContent.ProjectileType<Projectiles.Tools.TourmalineHook>();
        Item.value = Item.sellPrice(silver: 54);
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useAnimation = 20;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Ores.Tourmaline>(), 15)
            .AddTile(TileID.Anvils)
            .Register();
    }
}