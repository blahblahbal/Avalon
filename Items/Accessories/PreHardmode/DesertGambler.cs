using Avalon.Buffs;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

//[AutoloadEquip(EquipType.Head)]
class DesertGambler : ModItem
{
    public override void SetStaticDefaults()
    {
        //ArmorIDs.Head.Sets.IsTallHat[Item.headSlot] = true;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 2);
        Item.height = dims.Height;
        Item.expert = true;
        Item.accessory = true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().DesertGamblerVisible = !hideVisual;
        if (player.statLife <= player.statLifeMax2 * 0.33f)
            player.AddBuff(ModContent.BuffType<Deadeye>(), 2);
    }
}
