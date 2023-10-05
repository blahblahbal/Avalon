using Avalon.Buffs;
using Avalon.Common.Players;
using Avalon.Items.Potions.Buff;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode
{
    public class ThePill : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.PutridScent);
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<AvalonPlayer>().ThePill = true;
            player.potionDelayTime = (int)(player.potionDelayTime * 1.15);
        }
    }
    public class ThePillGlobalItem : GlobalItem
    {
        public override void GetHealLife(Item item, Player player, bool quickHeal, ref int healValue)
        {
            if (player.GetModPlayer<AvalonPlayer>().ThePill) healValue = (int)(healValue * 1.5f);
        }
        public override bool? UseItem(Item item, Player player)
        {
            if (player.GetModPlayer<AvalonPlayer>().ThePill && item.buffType == ModContent.BuffType<Rejuvenation>())
            {
                player.buffTime[player.FindBuffIndex(ModContent.BuffType<Rejuvenation>())] = (int)(player.buffTime[player.FindBuffIndex(ModContent.BuffType<Rejuvenation>())] * 1.5f);
            }
            return base.UseItem(item, player);
        }
    }
}
