using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

class NaquadahAnvil : ModItem
{
    public override void SetStaticDefaults()
    {
        //Tooltip.SetDefault("Used to craft items from mythril, orichalcum, naquadah, adamantite, titanium, and troxinium bars");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.maxStack = 9999;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.NaquadahAnvil>();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 25000;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.NaquadahBar>(), 10)
            .AddTile(TileID.Anvils)
			.SortAfterFirstRecipesOf(ItemID.MythrilAnvil)
			.Register();
    }
}
