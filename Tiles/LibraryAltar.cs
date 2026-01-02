using Avalon.Items.Consumables;
using Avalon.NPCs.Bosses.Hardmode.Phantasm;
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
		Main.tileSolidTop[Type] = true;
		Main.tileTable[Type] = true;
		TileID.Sets.IgnoredByNpcStepUp[Type] = true;

		DustType = DustID.Stone;
		AdjTiles = [TileID.Bookcases];

		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
		TileObjectData.addTile(Type);

		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
		AddMapEntry(Color.Gray);
	}
	public override void MouseOver(int i, int j)
	{
		Main.LocalPlayer.cursorItemIconEnabled = true;
		Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<EctoplasmicBeacon>();
	}
	public override bool RightClick(int i, int j)
	{
		int index = Main.LocalPlayer.FindItem(ModContent.ItemType<EctoplasmicBeacon>());
		if (index > -1 && !NPC.AnyNPCs(ModContent.NPCType<Phantasm>()))
		{
			Main.LocalPlayer.inventory[index].stack--;
			if (Main.LocalPlayer.inventory[index].stack <= 0)
			{
				Main.LocalPlayer.inventory[index].SetDefaults();
			}
			Vector2 vector = new Point(i, j).ToWorldCoordinates();

			if (Main.tile[i, j].TileFrameY == 36)
			{
				vector.Y -= 16f;
			}
			vector.X -= Math.Sign(Main.tile[i, j].TileFrameX - 32) * 16;
			vector.Y -= 16 * 28;


			SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/NPC/PhantasmJumpscareScary2024") { Volume = 0.8f, }, vector);
			//SoundEngine.PlaySound(SoundID.ScaryScream, vector);
			for (int x = 0; x < 35; x++)
			{
				Dust d = Dust.NewDustPerfect(vector, DustID.DungeonSpirit);
				d.noGravity = true;
				d.velocity = Main.rand.NextVector2Circular(30, 30);
				d.scale = 2;
			}


			NPC.SpawnBoss((int)vector.X, (int)vector.Y, ModContent.NPCType<Phantasm>(), Main.myPlayer);
			return true;
		}

		return base.RightClick(i, j);
	}
}
