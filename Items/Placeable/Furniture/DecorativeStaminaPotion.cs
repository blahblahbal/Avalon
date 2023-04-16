using Avalon.Tiles.Furniture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture
{
    public class DecorativeStaminaPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 30;
        }
        public override void SetDefaults() 
        {
            Item.CloneDefaults(ItemID.PlaceableHealingPotion);
            Item.createTile = ModContent.TileType<PlacedStaminaPotion>();
            Item.placeStyle = 0;
        }
    }
}
