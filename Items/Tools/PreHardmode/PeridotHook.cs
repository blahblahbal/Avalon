using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class PeridotHook : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.noUseGraphic = true;
        Item.useTurn = true;
        Item.shootSpeed = 12f;
        Item.rare = ItemRarityID.Blue;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 0;
        Item.knockBack = 7f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Tools.PeridotHook>();
        Item.value = Item.sellPrice(0, 0, 54, 0);
		Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.None;
        Item.useAnimation = 0;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Ores.Peridot>(), 15)
            .AddTile(TileID.Anvils)
			.SortAfterFirstRecipesOf(ItemID.EmeraldHook)
			.Register();
    }
}
