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
		Main.meleeBuff[Type] = true;
		Main.persistentBuff[Type] = true;
	}
	public override void Update(Player player, ref int buffIndex)
	{
		player.GetModPlayer<AvalonEnchantPlayer>().PathogenImbue = true;
		player.MeleeEnchantActive = true;
	}
	public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
	{
		rare = ModContent.RarityType<FlaskBuffNameRarity>();
		base.ModifyBuffText(ref buffName, ref tip, ref rare);
	}
}
