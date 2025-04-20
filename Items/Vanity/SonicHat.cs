using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity
{
	[AutoloadEquip(EquipType.Head)]
	public class SonicHat : ModItem
	{
		public override void SetDefaults()
		{
			//https://cdn.discordapp.com/attachments/1083705573528326204/1161441714163163246/Mosquitio.mp4
			Rectangle dims = this.GetDims();
			Item.rare = ItemRarityID.Blue;
			Item.width = dims.Width;
			Item.vanity = true;
			Item.value = Item.sellPrice(0, 0, 50);
			Item.height = dims.Height;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.MushroomGrassSeeds, 5)
				.AddIngredient(ItemID.Silk, 20)
				.AddTile(TileID.Loom)
				.Register();
		}
	}
}
