using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.GameContent.Drawing.TileDrawing;

namespace Avalon.Tiles.Furniture.Functional;

public class MonsterBanner : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileFrameImportant[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileLavaDeath[Type] = true;

		TileID.Sets.MultiTileSway[Type] = true;

		DustType = -1;

		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.CoordinateHeights = [16, 16, 16];
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom | AnchorType.PlanterBox, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.StyleWrapLimit = 111;
		TileObjectData.newTile.DrawYOffset = -2;

		// This alternate placement supports placing on un-hammered platform tiles.
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.Platform, TileObjectData.newTile.Width, 0);
		TileObjectData.newAlternate.DrawYOffset = -10;
		TileObjectData.addAlternate(0);

		TileObjectData.addTile(Type);

		AddMapEntry(new Color(13, 88, 130));
	}
	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY = 0;
	}
	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		if (TileObjectData.IsTopLeft(Main.tile[i, j]))
		{
			Main.instance.TilesRenderer.AddSpecialPoint(i, j, TileCounterType.MultiTileVine);
		}
		return false;
	}
	public override void NearbyEffects(int i, int j, bool closer)
	{
		if (closer)
		{
			return;
		}

		// Calculate the tile place style, then map that place style to an ItemID and BannerID.
		int tileStyle = TileObjectData.GetTileStyle(Main.tile[i, j]);
		int itemType = TileLoader.GetItemDropFromTypeAndStyle(Type, tileStyle);
		int bannerID = NPCLoader.BannerItemToNPC(itemType);

		if (bannerID == -1)
		{
			return;
		}

		// Once the BannerID and Item type have been calculated, we apply the banner buff
		if (ItemID.Sets.BannerStrength.IndexInRange(itemType) && ItemID.Sets.BannerStrength[itemType].Enabled)
		{
			Main.SceneMetrics.NPCBannerBuff[bannerID] = true;
			Main.SceneMetrics.hasBanner = true;
		}
	}
}
