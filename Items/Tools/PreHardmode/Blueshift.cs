using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

class Blueshift : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 40;
        Item.UseSound = SoundID.Item1;
        Item.damage = 23;
        Item.autoReuse = true;
        Item.hammer = 65;
        Item.axe = 17;
        Item.useTime = 24;
        Item.knockBack = 3f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 27000;
        Item.useAnimation = 20;
    }
}
