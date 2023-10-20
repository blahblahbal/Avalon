using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Trophy.Relics
{
    public class DesertBeakRelic : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.EyeofCthulhuMasterTrophy);
            Item.placeStyle = 1;
            Item.createTile = ModContent.TileType<Tiles.Furniture.Relics>();
        }
    }
}
