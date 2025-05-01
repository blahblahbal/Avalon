using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class TombMirror : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToConsumable(false, 32, 32, useTurn: true, width: 24, height: 28);
		Item.maxStack = 1;
		Item.rare = ItemRarityID.LightRed;
		Item.UseSound = SoundID.Item6;
	}
	public override bool CanUseItem(Player player)
	{
		return player.showLastDeath;
	}
	public override bool? UseItem(Player player)
	{
		Vector2 newPos = new Vector2(player.lastDeathPostion.X - 16f, player.lastDeathPostion.Y - 24f);
		player.Teleport(newPos);
		return true;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.RecallPotion, 3)
			.AddRecipeGroup("Tombstones", 10)
			.AddRecipeGroup("Herbs", 5)
			.AddTile(TileID.Furnaces)
			.Register();
	}
}
