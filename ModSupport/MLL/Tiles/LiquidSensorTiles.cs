using Avalon.ModSupport.MLL.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.ModSupport.MLL.Tiles;
public class LiquidSensorTiles : ModTile
{
	public override void SetStaticDefaults()
	{
		TileID.Sets.AllowsSaveCompressionBatching[Type] = true;
		TileID.Sets.DoesntGetReplacedWithTileReplacement[Type] = true;
		Main.tileFrameImportant[Type] = true;
		//Using TileObjectData we use HookPostPlaceMyPlayer to spawn a Tile Entity, using the ModTileEntity's Hook_AfterPlacement to spawn a TEModLogicSensors
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.None, 0, 0);
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<TEModLogicSensors>().Hook_AfterPlacement, -1, 0, processedCoordinates: true);
		TileObjectData.addTile(Type);
		DustType = -1;

		AddMapEntry(new Color(0, 255, 0), ModContent.GetInstance<AcidSensor>().DisplayName);
		AddMapEntry(new Color(200, 0, 0), ModContent.GetInstance<BloodSensor>().DisplayName);
	}
	public override ushort GetMapOption(int i, int j)
	{
		return (ushort)(Main.tile[i, j].TileFrameY / 18);
	}
}
