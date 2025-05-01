using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode;

public class GlassEye : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSpellBook(ModContent.ProjectileType<Projectiles.Magic.Tear>(), 14, 2f, 3, 12f, 35, 35, 1.1f, width: 16, height: 16);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 12, 0);
		Item.UseSound = SoundID.NPCHit1;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ItemID.Lens, 1).AddIngredient(ItemID.FallenStar, 2).AddIngredient(ItemID.BottledWater, 1).AddTile(TileID.WorkBenches).Register();
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(5, 0);
	}
}
