using Avalon.Dusts;
using Avalon.Tiles.Savanna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class BacciliteOre : ModTile
{
	public override void SetStaticDefaults()
	{
		AddMapEntry(Color.Olive, this.GetLocalization("MapEntry"));
		Data.Sets.Tile.RiftOres[Type] = true;
		Main.tileSolid[Type] = true;
		Main.tileMergeDirt[Type] = true;
		Main.tileShine2[Type] = true;
		Main.tileShine[Type] = 1150;
		Main.tileSpelunker[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileOreFinderPriority[Type] = 320;
		Main.tileLighted[Type] = true;
		Main.tileMerge[Type][TileID.Mud] = true;
		Main.tileMerge[TileID.Mud][Type] = true;
		Main.tileMerge[Type][ModContent.TileType<Loam>()] = true;
		Main.tileMerge[ModContent.TileType<Loam>()][Type] = true;
		HitSound = SoundID.Tink;
		DustType = ModContent.DustType<ContagionWeapons>();
		MinPick = 55;
		TileID.Sets.Ore[Type] = true;
	}
	public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
	{
		if (Main.rand.NextBool(60))
		{
			Dust d = Dust.NewDustDirect(new Vector2(i, j) * 16, 16, 16, DustType, 0, 0, 128);
			d.noGravity = true;
			d.scale *= 0.1f;
			d.velocity *= 0.1f;
			d.fadeIn = Main.rand.NextFloat(0.5f,1.3f);
			d.velocity.Y += 0.4f;
		}
		else if (Main.rand.NextBool(15))
		{
			Vector2 vel = Vector2.Zero;
			Vector2 tilePos = Utils.ToWorldCoordinates(new Point(i, j));
			float distance = -1f;
			foreach (Player p in Main.ActivePlayers)
			{
				if (p.MountedCenter.Distance(tilePos) < 64f)
				{
					if (distance == -1f || p.MountedCenter.Distance(tilePos) < distance)
					{
						distance = p.MountedCenter.Distance(tilePos);
						vel = tilePos.DirectionTo(p.MountedCenter);
					}
				}
			}
			if (distance == -1f)
			{
				foreach (NPC n in Main.ActiveNPCs)
				{
					if (n.Center.Distance(tilePos) < 64f)
					{
						if (distance < 0f || n.Center.Distance(tilePos) < distance)
						{
							distance = n.Center.Distance(tilePos);
							vel = tilePos.DirectionTo(n.Center);
						}
					}
				}
			}
			if (distance != -1f)
			{
				Dust d = Dust.NewDustDirect(new Vector2(i, j) * 16, 16, 16, DustType, 0, 0, 128);
				d.noGravity = true;
				d.scale *= 0.6f;
				d.velocity = vel.RotatedByRandom(MathHelper.Pi / 6.5f) * 0.5f;
				d.fadeIn = Main.rand.NextFloat(0.7f, 1.2f);
				d.position += vel * 12f;
			}
		}
	}
	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.18f;
		g = 0.25f;
	}
}
