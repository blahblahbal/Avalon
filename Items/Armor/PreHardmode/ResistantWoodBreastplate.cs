using Avalon.Items.Placeable.Tile;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode
{
	[AutoloadEquip(EquipType.Body)]
	public class ResistantWoodBreastplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
		}
		public override void SetDefaults()
		{
			Item.DefaultToArmor(3);
		}
		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<ResistantWood>(), 30).AddTile(TileID.WorkBenches).Register();
		}
	}
}
