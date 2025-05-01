using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Legs)]
public class NaquadahShinguards : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(12);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 2, 30);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.NaquadahBar>(), 15)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
	public override void UpdateEquip(Player player)
	{
		player.moveSpeed += 0.06f;
	}
}
