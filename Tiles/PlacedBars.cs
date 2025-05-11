using Avalon.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class PlacedBars : ModTile
{
	private Asset<Texture2D>? glow;
	public override void SetStaticDefaults()
	{
		glow = ModContent.Request<Texture2D>(Texture + "_Glow");

		Main.tileFrameImportant[Type] = true;
		Main.tileSolidTop[Type] = true;
		Main.tileSolid[Type] = true;
		Main.tileShine[Type] = 1100;

		DustType = -1;

		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.addTile(Type);
	}

	// basically just makes sure the bar below it isn't hammered, and causes it to break if so
	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		WorldGen.Check1x1(i, j, Type);
		return false;
	}

	// selects the map entry depending on the frameX
	public override ushort GetMapOption(int i, int j)
	{
		return (ushort)(Main.tile[i, j].TileFrameX / 18);
	}

	// Troxinium Bar Glow
	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		TileGlowDrawing.DrawGlowmask(i, j, new Color(255, 255, 255, 0) * (Lighting.Brightness(i, j) * 4f), glow);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		switch (Main.tile[i, j].TileFrameX / 18)
		{
			case 0:
				type = ModContent.DustType<Dusts.CaesiumDust>();
				break;
			case 1:
				type = DustID.Adamantite; // Oblivion
				break;
			case 2:
				type = DustID.MagicMirror; // Hydrolyth
				break;
			case 3:
				type = ModContent.DustType<Dusts.RhodiumDust>();
				break;
			case 4:
				type = ModContent.DustType<Dusts.OsmiumDust>();
				break;
			case 5:
				type = DustID.UltraBrightTorch; // Ferozium
				break;
			case 6:
				type = DustID.Dirt; // Unvolandite
				break;
			case 7:
				type = DustID.CorruptGibs; //Vorazylcum
				break;
			case 8:
				type = DustID.Stone; // Tritanorium
				break;
			case 9:
				type = ModContent.DustType<Dusts.DurataniumDust>();
				break;
			case 10:
				type = ModContent.DustType<Dusts.NaquadahDust>();
				break;
			case 11:
				type = ModContent.DustType<Dusts.TroxiniumDust>();
				break;
			case 12:
				type = ModContent.DustType<Dusts.ContagionWeapons>(); // Baccilite
				break;
			case 13:
				type = DustID.InfernoFork; // Pyroscoric
				break;
			case 14:
				type = DustID.RainCloud; // Beetle
				break;
			case 15:
				type = DustID.BubbleBurst_Purple; // Kryzinvium/Superhardmode
				break;
			case 16:
				Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.MagicMirror);
				Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.Enchanted_Gold);
				Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.Enchanted_Pink);
				return false;
			case 17:
				type = DustID.Ice_Pink; // Berserker
				break;
			case 18:
				type = ModContent.DustType<Dusts.BronzeDust>();
				break;
			case 19:
				type = ModContent.DustType<Dusts.NickelDust>();
				break;
			case 20:
				type = ModContent.DustType<Dusts.ZincDust>();
				break;
			case 21:
				type = ModContent.DustType<Dusts.BismuthDust>();
				break;
			case 22:
				type = ModContent.DustType<Dusts.IridiumDust>();
				break;
			case 23:
				type = DustID.KryptonMoss; // Xanthophyte
				break;
			case 24:
				type = DustID.Corruption; // Corrupted
				break;
		}
		return base.CreateDust(i, j, ref type);
	}
}
