using Avalon.Common.Players;
using Avalon.Common.Templates;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Tropics
{
    public class TropicsTorch : ModTorch
    {
        public override Vector3 LightColor => new Vector3(0.69f, 1f, 0.42f);
        public override int TorchItem => ModContent.ItemType<Items.Placeable.Furniture.TropicsTorch>();
        public override int dustType => DustID.JungleTorch;

        public override float GetTorchLuck(Player player)
        {
            if (player.GetModPlayer<AvalonBiomePlayer>().ZoneTropics)
            {
                return 1f;
            }
            return base.GetTorchLuck(player);
        }
    }
}
