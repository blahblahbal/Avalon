using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Avalon.Items.Accessories;

internal class IntelligenceBoots : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.rare = ItemRarityID.Gray;
        Item.accessory = true;
        Item.value = Item.sellPrice(0);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (player.GetModPlayer<IntBootsPlayer>().BootsCount > 0)
        {
            player.GetModPlayer<IntBootsPlayer>().Boots = true;
            player.GetModPlayer<IntBootsPlayer>().BootsCount--;
        }
        else if (Main.rand.NextBool(1))
        {
            player.GetModPlayer<IntBootsPlayer>().BootsCount = Main.rand.Next(100, 150);
        }
    }
}
class IntBootsPlayer : ModPlayer
{
    public bool Boots;
    public int BootsCount = 0;

    public override void ResetEffects()
    {
        Boots = false;
    }
    public override void PostUpdateEquips()
    {
        if (Boots)
        {
            bool left = Player.controlLeft;
            bool up = Player.controlUp;
            bool down = Player.controlDown;
            bool right = Player.controlRight;
            bool inv = Player.controlInv;
            bool mount = Player.controlMount;
            bool hook = Player.controlHook;
            bool qHeal = Player.controlQuickHeal;
            bool qMana = Player.controlQuickMana;
            bool map = Player.controlMap;
            bool jump = Player.controlJump;

            List<bool> controls = new List<bool>()
            {
                left, up, down, right, inv, mount, hook, qHeal, qMana, map, jump
            };

            bool[] array = controls.ToArray();
            Player.controlLeft = Main.rand.NextFromList(array);
            Player.controlUp = Main.rand.NextFromList(array);
            Player.controlDown = Main.rand.NextFromList(array);
            Player.controlRight = Main.rand.NextFromList(array);
            Player.controlInv = Main.rand.NextFromList(array);
            Player.controlMount = Main.rand.NextFromList(array);
            Player.controlHook = Main.rand.NextFromList(array);
            Player.controlQuickHeal = Main.rand.NextFromList(array);
            Player.controlQuickMana = Main.rand.NextFromList(array);
            Player.controlMap = Main.rand.NextFromList(array);
            Player.controlJump = Main.rand.NextFromList(array);
        }
    }
}
