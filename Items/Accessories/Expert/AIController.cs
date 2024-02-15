using Avalon.Buffs;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Expert;

public class AIController : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = -12;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 15);
        Item.buffType = ModContent.BuffType<StingerProbe>();
        Item.height = dims.Height;
        Item.expert = true;
        Item.damage = 134;
        Item.DamageType = DamageClass.Summon;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (!player.HasBuff(Item.buffType))
        {
            player.GetModPlayer<AvalonPlayer>().StingerProbeTimer = 0;
        }

        player.AddBuff(Item.buffType, 2);
    }
}
