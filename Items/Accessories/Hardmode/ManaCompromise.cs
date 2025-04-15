using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class ManaCompromise : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 6, 70);
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.buffImmune[BuffID.ManaSickness] = true;
		player.manaFlower = true;
		player.GetDamage(DamageClass.Magic) -= 0.12f;
		player.manaCost -= 0.08f;
	}
}
