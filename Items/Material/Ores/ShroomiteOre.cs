using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Ores;

public class ShroomiteOre : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Ores.ShroomiteOre>();
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 0, 40, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
	public override void AddRecipes()
	{
		Recipe.Create(ItemID.ShroomiteBar)
			.AddIngredient(this, 5)
			.AddTile(TileID.AdamantiteForge)
			.Register();
	}
}
