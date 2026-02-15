using Avalon;
using Avalon.Common.Extensions;
using Avalon.Projectiles.Magic.Other;
using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Other;

public class MagicCleaver : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeaponSwing(ModContent.ProjectileType<MagicCleaverProj>(), 85, 5f, 16, 20f, 18, true, noUseGraphic: true, width: 20, height: 36);
		Item.rare = ModContent.RarityType<BlueRarity>();
		Item.value = Item.sellPrice(silver: 72);
		Item.UseSound = SoundID.Item1;
	}
}