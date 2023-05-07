using Avalon.Common.Players;
using Avalon.Dusts;
using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class ImbuePathogen : ModBuff
{
    public override void SetStaticDefaults()
    {
        BuffID.Sets.IsAFlaskBuff[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<AvalonPlayer>().PathogenImbue = true;
        //if(player.meleeEnchant > 0)
        //{
        //    for (int i = 0; i < player.buffType.Length; i++)
        //    {
        //        if (player.buffType[i] == Type)
        //        {
        //            player.DelBuff(player.buffType[i]);
        //        }
        //    }
        //}
    }
    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        rare = ModContent.RarityType<FlaskBuffNameRarity>();
        base.ModifyBuffText(ref buffName, ref tip, ref rare);
    }
}
