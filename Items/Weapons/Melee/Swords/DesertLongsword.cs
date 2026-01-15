using Avalon;
using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords;

public class DesertLongsword : ModItem
{
	public override void SetDefaults()
	{
		Item.CloneDefaults(ItemID.AntlionClaw);
	}
}
