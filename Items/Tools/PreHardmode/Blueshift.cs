using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class Blueshift : ModItem
{
    int useTimer = 0;
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
        Item.rare = ItemRarityID.Green;
        Item.value = 27000;
        Item.useAnimation = 20;
    }
    public override void HoldItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            if (player.controlUseItem)
            {
                useTimer++;
                if (useTimer % 60 == 0)
                {
                    useTimer = 0;
                    if (Item.useAnimation > 8)
                    {
                        Item.useAnimation--;
                    }
                    if (Item.useTime > 8)
                    {
                        Item.useTime--;
                    }
                }
            }
            else
            {
                Item.useAnimation = 20;
                Item.useTime = 24;
            }
        }
    }
}
