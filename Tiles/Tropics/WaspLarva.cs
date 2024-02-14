using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Tropics;

public class WaspLarva : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileCut[Type] = true;
        Main.tileSolid[Type] = false;
        Main.tileNoAttach[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileWaterDeath[Type] = true;
        Main.tileFrameImportant[Type] = true;
        AnimationFrameHeight = 54;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
        TileObjectData.addTile(Type);
        DustType = DustID.Stone;
        HitSound = SoundID.NPCDeath1;
        AddMapEntry(new Color(172, 154, 131));
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        NPC.SpawnOnPlayer(Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16), ModContent.NPCType<NPCs.Bosses.PreHardmode.KingSting>());
    }

    public override void AnimateTile(ref int frame, ref int frameCounter)
    {
        frame = Main.tileFrame[TileID.Larva];
    }
    //public override void PlaceInWorld(int i, int j, Item item)
    //{
    //    Main.tile[i, j].frameX = (short)(Main.rand.Next(0, 8) * 18);
    //}
}
