using Avalon.Common.Extensions;
using Avalon.Projectiles.Magic.Tomes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Tomes;

public class ChaosTome : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSpellBook(ModContent.ProjectileType<ChaosBolt>(), 21, 4f, 8, 8f, 25, 25);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(silver: 54);
		Item.UseSound = SoundID.Item20;
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(10, 0);
	}
}

