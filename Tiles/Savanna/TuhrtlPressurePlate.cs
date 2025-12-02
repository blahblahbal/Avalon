using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Savanna;

public class TuhrtlPressurePlate : ModTile
{
	public override void SetStaticDefaults()
	{
		TileID.Sets.PressurePlate[Type] = -2;
		TileID.Sets.IsATrigger[Type] = true;

		Main.tileFrameImportant[Type] = true;
		Main.tileObsidianKill[Type] = true;

		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
		TileObjectData.newTile.CoordinateHeights = [18];
		TileObjectData.newTile.CoordinatePadding = 0;
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.addTile(Type);

		AddMapEntry(new Color(99, 89, 85));
		DustType = DustID.Silt;
	}
	public override bool IsTileDangerous(int i, int j, Player player)
	{
		return true;
	}
	public override void HitSwitch(int i, int j)
	{
		SoundEngine.PlaySound(SoundID.Mech, new Vector2(i * 16, j * 16));
		Wiring.TripWire(i, j, 1, 1);
	}
	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		if (!fail)
		{
			PressurePlateHelper.DestroyPlate(new Point(i, j)); // Handles sending a signal if mined while standing on it.
		}
	}
	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY += 2;
	}
	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		//if (PressurePlateHelper.PressurePlatesPressed.ContainsKey(new Point(i, j)))
		//{
		//	frameXOffset += 18;
		//}
	}
}
