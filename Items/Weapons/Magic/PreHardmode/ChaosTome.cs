using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode;

public class ChaosTome : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSpellBook(ModContent.ProjectileType<Projectiles.Magic.ChaosBolt>(), 21, 4f, 8, 8f, 25, 25);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(silver: 54);
		Item.UseSound = SoundID.Item20;
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(10, 0);
	}
}
