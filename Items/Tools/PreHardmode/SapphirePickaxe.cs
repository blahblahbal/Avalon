using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

class SapphirePickaxe : ModItem
{
    int useTimer = 0;
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.damage = 9;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.crit += 5;
        Item.pick = 80;
        Item.rare = ItemRarityID.Green;
        Item.useTime = 20;
        Item.knockBack = 2f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 18000;
        Item.useAnimation = 20;
        Item.UseSound = SoundID.Item1;
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
                Item.useTime = 20;
            }
        }
    }
}
