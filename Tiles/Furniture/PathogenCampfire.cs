using Avalon.Common.Templates;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture
{
    public class PathogenCampfire : ModCampfire
    {
        public override Vector3 LightColor => new Vector3(0.5f, 0, 2f);
        public override int TorchItem => ModContent.ItemType<Items.Placeable.Furniture.PathogenCampfire>();
        public override int dustType => ModContent.DustType<PathogenDust>();

        public override bool WaterDeath => false;
        public override bool NoDustGravity => base.NoDustGravity;
    }
}
