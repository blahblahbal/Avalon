using Avalon.ModSupport.Thorium.Tiles;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.Thorium.Items.Placeable.Tile;

public class LargeMarineKelp : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return ExxoAvalonOrigins.ThoriumContentEnabled;
	}
	public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 15;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<LargeMarineKelpStage4>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
    }
}
