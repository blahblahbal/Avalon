using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture
{
    public class ContagionCampfire : ModCampfire
    {
        public override Vector3 LightColor => new Vector3(0.8f, 1.4f, 0);
        public override int TorchItem => ModContent.ItemType<Items.Placeable.Furniture.ContagionCampfire>();
        public override int dustType => DustID.JungleTorch;
    }
}
