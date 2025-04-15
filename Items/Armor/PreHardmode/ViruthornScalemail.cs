using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Body)]
public class ViruthornScalemail : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(8);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 54);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BacciliteBar>(), 25).AddIngredient(ModContent.ItemType<Material.Booger>(), 15).AddTile(TileID.Anvils).Register();
	}
	public override void UpdateEquip(Player player)
	{
		player.GetModPlayer<AvalonPlayer>().AllCritDamage(0.07f);
	}
}
