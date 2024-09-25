using Terraria.ID;

namespace Avalon.Data.Sets;

internal class Recipe
{
	public static bool[] RottenChunkOnlyItem = ItemID.Sets.Factory.CreateBoolSet(
		ItemID.Leather, ItemID.WormFood, ItemID.TimelessTravelerHood,
		ItemID.TimelessTravelerRobe, ItemID.TimelessTravelerBottom);
}
