using Avalon.Items.Consumables;
using Avalon.NPCs.Bosses.Hardmode;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class LibraryAltar : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = false;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
        TileObjectData.addTile(Type);
        AddMapEntry(Color.Gray);
        DustType = DustID.Stone;
    }
	public override void MouseOver(int i, int j)
	{
		Main.LocalPlayer.cursorItemIconEnabled = true;
		Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<EctoplasmicBeacon>();
	}
	public override bool RightClick(int i, int j)
	{
		//if(Main.LocalPlayer.HeldItem.type == ModContent.ItemType<EctoplasmicBeacon>())
		//{
		//	Vector2 vector = new Point(i, j).ToWorldCoordinates();

		//	Main.NewText(Main.tile[i, j].TileFrameY);

		//	if (Main.tile[i,j].TileFrameY == 36)
		//	{
		//		vector.Y -= 16f;
		//	}
		//	vector.X -= Math.Sign(Main.tile[i, j].TileFrameX - 32) * 16;
		//	vector.Y -= 16 * 28;


		//	SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/NPC/PhantasmJumpscareScary2024") { Volume = 0.8f,}, vector);
		//	//SoundEngine.PlaySound(SoundID.ScaryScream, vector);
		//	for (int x = 0; x < 35; x++)
		//	{
		//		Dust d = Dust.NewDustPerfect(vector,DustID.DungeonSpirit);
		//		d.noGravity = true;
		//		d.velocity = Main.rand.NextVector2Circular(30, 30);
		//		d.scale = 2;
		//	}


		//	NPC.SpawnBoss((int)vector.X, (int)vector.Y, ModContent.NPCType<Phantasm>(),Main.myPlayer);
		//	return true;
		//}

		return base.RightClick(i, j);
	}
}
