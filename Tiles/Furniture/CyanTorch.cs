using Avalon.Common.Templates;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture
{
    public class CyanTorch : ModTorch
    {
        public override Vector3 LightColor => new Vector3(0, 1f, 1f);
        public override int TorchItem => ModContent.ItemType<Items.Placeable.Furniture.CyanTorch>();
        public override int dustType => ModContent.DustType<CyanTorchDust>();
    }
}
