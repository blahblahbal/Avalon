using Avalon.Common.Templates;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture
{
    public class SlimeTorch : ModTorch
    {
        public override Vector3 LightColor => new Vector3(0.25f, 0.72f, 1f);
        public override int TorchItem => ModContent.ItemType<Items.Placeable.Furniture.SlimeTorch>();
        public override int dustType => DustID.t_Slime;
    }
}
