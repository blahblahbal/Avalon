using Avalon.Common.EnemyGauntletSystem;
using Avalon.Common.EnemyGauntletSystem.Gauntlets;
using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.Functional;

public class EnemyGauntletChest : ChestTemplate
{
	public override int DropItem => ItemID.Skull;
	public override void AddMapEntries()
	{
		for(int i = 0; i < 4; i++)
		{
			AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry" + i), MapChestName);
		}
	}
	public override bool IsLockedChest(int i, int j)
	{
		return (TileObjectData.GetTileStyle(Main.tile[i, j]) + 1) % 2 == 0;
	}
	public override bool RightClick(int i, int j)
	{
		int style = TileObjectData.GetTileStyle(Main.tile[i, j]);
		if ((style + 1) % 2 == 0)
		{
			Point topLeft = TileObjectData.TopLeft(i, j).ToPoint();
			for(int x = 0; x < EnemyGauntletSystem.Gauntlets.Length; x++)
			{
				if (EnemyGauntletSystem.Gauntlets[x] != null && EnemyGauntletSystem.Gauntlets[x].PointOfInterest == topLeft)
				{
					return base.RightClick(i, j);
				}
			}

			EnemyGauntlet EG = new IceShrineGauntlet();
			EG.Begin(new Rectangle(topLeft.X - 40, topLeft.Y - 25, 41, 41), topLeft);

		}
		return base.RightClick(i,j);
	}
}
