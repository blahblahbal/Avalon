using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Pets;

public class ArgusLantern : ModItem
{
	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
		{
			player.AddBuff(Item.buffType, 3600);
		}
	}
	public override void SetDefaults()
	{
		Item.DefaultToVanitypet(ModContent.ProjectileType<Projectiles.Pets.ArgusLantern>(), ModContent.BuffType<Buffs.Pets.ArgusLantern>());
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 2, 50);
	}
}
