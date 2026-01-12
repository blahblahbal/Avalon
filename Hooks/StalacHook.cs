using Avalon.Common;
using Avalon.Data.Sets;
using Avalon.Tiles.Blastplains;
using Avalon.Tiles.Savanna;
using MonoMod.Cil;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
	internal class StalacHook : ModHook
	{
		protected override void Apply()
		{
			On_WorldGen.GetDesiredStalagtiteStyle += On_WorldGen_GetDesiredStalactiteStyle;

			IL_WorldGen.PaintTheSand += IL_AddStalacCheck;
			IL_WorldGen.PlaceTile += IL_AddStalacCheck;
			IL_WorldGen.PlaceTight += IL_AddStalacCheck;
			IL_WorldGen.BlockBelowMakesSandFall += IL_AddStalacCheck;

			IL_WorldGen.TileFrame += IL_AddStalacCheck;

			IL_WorldGen.UpdateWorld_OvergroundTile += IL_AddStalacCheck;

			IL_WorldGen.UpdateWorld_UndergroundTile += IL_AddStalacCheck;

			IL_WorldGen.ReplaceTile_EliminateNaturalExtras += IL_AddStalacCheck;
		}

		private static void IL_AddStalacCheck(ILContext il)
		{
			Utilities.AddAlternativeIdChecks(il, TileID.Stalactite, id => TileSets.Stalac.Contains(id));
		}

		private static void On_WorldGen_GetDesiredStalactiteStyle(On_WorldGen.orig_GetDesiredStalagtiteStyle orig, int x, int j, out bool fail, out int desiredStyle, out int height, out int y)
		{
			orig(x, j, out fail, out desiredStyle, out height, out y);
			switch (fail)
			{
				case true when desiredStyle == ModContent.TileType<Nest>():
					fail = false;
					desiredStyle = 7;
					for (var i = y; i < y + height; i++)
					{
						Main.tile[x, i].TileType = (ushort)ModContent.TileType<NestStalac>();
					}
					break;

				case false when Main.tile[x, j].TileType == ModContent.TileType<NestStalac>():
					for (var i = y; i < y + height; i++)
					{
						Main.tile[x, i].TileType = TileID.Stalactite;
					}
					break;

				case true when desiredStyle == ModContent.TileType<BlastedStone>():
					fail = false;
					desiredStyle = 7;
					for (var i = y; i < y + height; i++)
					{
						Main.tile[x, i].TileType = (ushort)ModContent.TileType<BlastedStalac>();
					}
					break;

				case false when Main.tile[x, j].TileType == ModContent.TileType<BlastedStalac>():
					for (var i = y; i < y + height; i++)
					{
						Main.tile[x, i].TileType = TileID.Stalactite;
					}
					break;
			}
		}
	}
}
