using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Items.Other;

class PlatinumHeart : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.IgnoresEncumberingStone[Type] = true;
        ItemID.Sets.IsAPickup[Type] = true;
    }
    public override void SetDefaults()
    {
        Item.Size = new Vector2(12);
    }

    public override bool CanPickup(Player player)
    {
        return true;
    }
}
