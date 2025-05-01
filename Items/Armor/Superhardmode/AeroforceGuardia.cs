using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Avalon.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Superhardmode;

[AutoloadEquip(EquipType.Head)]
public class AeroforceGuardia : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(8);
		Item.rare = ModContent.RarityType<CrabbyRarity>();
		Item.value = Item.sellPrice(0, 3);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.HallowedBar, 15)
			.AddIngredient(ItemID.Feather, 16)
			.AddIngredient(ItemID.SoulofFlight, 10)
			.AddIngredient(ModContent.ItemType<Material.Bars.CaesiumBar>(), 5)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return body.type == ModContent.ItemType<AeroforceProtector>() && legs.type == ModContent.ItemType<AeroforceLeggings>();
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Aeroforce");
		player.GetModPlayer<AvalonPlayer>().SkyBlessing = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Summon) += 0.08f;
		player.maxMinions++;
	}
}
