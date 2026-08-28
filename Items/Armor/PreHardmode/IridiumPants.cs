using Avalon.Common.Extensions;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Legs)]
public class IridiumPants : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(8);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1, 20);
	}
	public override void UpdateEquip(Player player)
	{
		player.moveSpeed += 0.3f;
	}
	public override bool ModifyEquipTextureDraw(ref PlayerDrawSet drawInfo, ref DrawData drawData, EquipTexture equipTexture, string methodName)
	{
		return ModContent.GetInstance<IridiumHat>().ModifyEquipTextureDraw(ref drawInfo, ref drawData, equipTexture, methodName);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.IridiumBar>(), 17)
			.AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 5)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
