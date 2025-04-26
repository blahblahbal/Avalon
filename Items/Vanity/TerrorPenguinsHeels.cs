using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity;

[AutoloadEquip(EquipType.Legs)]
public class TerrorPenguinsHeels : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToVanity();
		Item.rare = ItemRarityID.Purple;
		Item.value = Item.sellPrice(0, 5);
	}
	public override void Load()
	{
		if (Main.netMode == NetmodeID.Server)
			return;

		EquipLoader.AddEquipTexture(Mod, $"{Texture}_Female_{EquipType.Legs}", EquipType.Legs, name: Name + "_Female");
	}
	public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
	{
		if (!male)
		{
			equipSlot = EquipLoader.GetEquipSlot(Mod, Name + "_Female", EquipType.Legs);
		}
	}
}
