using Avalon.Common.Extensions;
using Avalon.Projectiles.Magic.Tomes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Tomes;

public class DevilsScythe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSpellBook(ModContent.ProjectileType<DevilScythe>(), 150, 4.75f, 16, 0.2f, 30, 30);
		Item.rare = ItemRarityID.Purple;
		Item.value = Item.sellPrice(silver: 80);
		Item.UseSound = SoundID.Item8;
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(6, 2);
	}
}