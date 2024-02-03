using Avalon.Common.Templates;
using Terraria.ModLoader;

namespace Avalon.Tiles.Herbs;

public class TwilightPlumePlanterBox : PlanterBoxTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Tile.TwilightPlumePlanterBox>();
}
