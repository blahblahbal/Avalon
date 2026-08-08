using Avalon.Projectiles.Summon.Whips;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Summon.Whips;

public class AnchorWhipworm : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToWhip(ModContent.ProjectileType<AnchorWhipwormProjectile>(), 32, 2f, 8f, 42);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(gold: 4);
	}
	public override bool MeleePrefix()
	{
		return true;
	}
}
