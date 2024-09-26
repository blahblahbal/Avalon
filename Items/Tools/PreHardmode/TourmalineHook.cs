using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

class TourmalineHook : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.noUseGraphic = true;
        Item.useTurn = true;
        Item.shootSpeed = 11f;
        Item.rare = ItemRarityID.Blue;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 0;
        Item.shoot = ModContent.ProjectileType<Projectiles.Tools.TourmalineHook>();
        Item.value = Item.sellPrice(silver: 54);
		Item.UseSound = SoundID.Item1;
		Item.useStyle = ItemUseStyleID.None;
        Item.useAnimation = 0;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Ores.Tourmaline>(), 15)
            .AddTile(TileID.Anvils)
			.SortAfterFirstRecipesOf(ItemID.TopazHook)
			.Register();
    }
}
