using Avalon.Common.Players;
using Avalon.Common.Templates;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture
{
    public class ContagionTorch : ModTorch
    {
        public override Vector3 LightColor => new Vector3(0.8f,1.4f,0);
        public override int TorchItem => ModContent.ItemType<Items.Placeable.Furniture.ContagionTorch>();
        public override int DustType => DustID.JungleTorch;

        public override float GetTorchLuck(Player player)
        {
            if (player.GetModPlayer<AvalonBiomePlayer>().ZoneContagion)
            {
                return 1f;
            }
            return base.GetTorchLuck(player);
        }
    }
}
