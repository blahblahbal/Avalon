using Avalon;
using Avalon.Common.Extensions;
using Avalon.Projectiles.Melee.Boomerangs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Boomerangs;

public class VirulentScythe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToBoomerang(ModContent.ProjectileType<VirulentScytheProj>(), 50, 2.5f, 18, 15f, true, width: 34, height: 36);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 20);
	}
}