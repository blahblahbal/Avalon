using Avalon.Common.EnemyGauntletSystem;
using Avalon.Tiles.Blastplains;
using Avalon.Tiles.CrystalMines;
using Avalon.Tiles.Ores;
using Avalon.WorldGeneration.Passes;
using Avalon.WorldGeneration.Structures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items;

public class EnemyGauntletStarter : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return true;
	}
	public override void SetDefaults()
	{
		Item.rare = ItemRarityID.Purple;
		Item.width = 15;
		Item.maxStack = 1;
		Item.useAnimation = Item.useTime = 14;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.value = 0;
		Item.height = 15;
		Item.autoReuse = true;
	}
	public override bool? UseItem(Player player)
	{
		if (player.ItemAnimationJustStarted)
		{
			TestGauntlet t = new TestGauntlet();
			Point center = player.Bottom.ToTileCoordinates();
			t.Begin(new Rectangle(center.X - 20, center.Y - 20, 40, 25), player.Center.ToTileCoordinates());
		}
		return base.UseItem(player);
	}
}