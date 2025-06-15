using Terraria;

namespace Avalon.Common
{
	public struct AvalonTileData : ITileData
	{
		public byte DataByte;

		public bool IsTileActupainted
		{
			readonly get => TileDataPacking.GetBit(DataByte, 1);
			set => DataByte = (byte)TileDataPacking.SetBit(value, DataByte, 1);
		}

		public bool IsWallActupainted
		{
			readonly get => TileDataPacking.GetBit(DataByte, 2);
			set => DataByte = (byte)TileDataPacking.SetBit(value, DataByte, 2);
		}
	}
}
