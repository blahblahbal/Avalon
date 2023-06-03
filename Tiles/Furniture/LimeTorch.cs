using Avalon.Common.Templates;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture
{
    public class LimeTorch : ModTorch
    {
        public override Vector3 LightColor => new Vector3(1.427451f,2,0);
        public override int TorchItem => ModContent.ItemType<Items.Placeable.Furniture.LimeTorch>();
        public override int DustType => DustID.GreenTorch;
    }
}