using Avalon.Common.Templates;
using Terraria.ModLoader;

namespace Avalon.Tiles.Herbs;

public class BarfbushPlanterBox : PlanterBoxTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Tile.BarfbushPlanterBox>();
}
