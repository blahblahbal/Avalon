using Avalon.Common.Templates;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture
{
    public class LimeTorch : ModTorch
    {
        public override Vector3 LightColor => new Vector3(0.714f, 1f, 0);
        public override int TorchItem => ModContent.ItemType<Items.Placeable.Furniture.LimeTorch>();
        public override int dustType => ModContent.DustType<LimeTorchDust>();
    }
}
