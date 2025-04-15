using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Head)]
public class NaquadahHeadguard : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(10);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 2, 40);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.NaquadahBar>(), 10)
			.AddTile(TileID.MythrilAnvil).Register();
	}
	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return body.type == ModContent.ItemType<NaquadahBreastplate>() && legs.type == ModContent.ItemType<NaquadahShinguards>();
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Naquadah");
		player.GetModPlayer<AvalonPlayer>().AuraThorns = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Ranged) += 0.07f;
		player.ammoCost80 = true;
	}
}
