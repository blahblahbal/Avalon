using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Pets;

public class PetriDish : ModItem
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
		Item.DefaultToVanitypet(ModContent.ProjectileType<Projectiles.Pets.BacteriumPet>(), ModContent.BuffType<Buffs.Pets.Bacterium>());
		Item.rare = ItemRarityID.Master;
		Item.value = Item.sellPrice(0, 1, 50);
	}
}
