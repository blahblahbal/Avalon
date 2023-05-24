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
    public class BacteriumPrimeRelic : ModItem
    {
        public override void SetDefaults() 
        {
            Item.CloneDefaults(ItemID.EyeofCthulhuMasterTrophy);
            Item.placeStyle = 0;
            Item.createTile = ModContent.TileType<Relics>();
        }
    }
}
