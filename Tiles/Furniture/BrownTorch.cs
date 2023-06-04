using Avalon.Common.Templates;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture
{
    public class BrownTorch : ModTorch
    {
        public override Vector3 LightColor => new Vector3(1.1f, 0.8f, 0.4f);
        public override int TorchItem => ModContent.ItemType<Items.Placeable.Furniture.BrownTorch>();
        public override int DustType => DustID.WhiteTorch;
    }
}
