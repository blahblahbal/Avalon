using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Avalon.Items.Consumables;
using System.Collections.Generic;

namespace Avalon.Tiles.Furniture;

public class PlacedStaminaCrystal : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(Color.Green, this.GetLocalization("MapEntry"));
        AnimationFrameHeight = 36;
        Main.tileSpelunker[Type] = true;
        Main.tileOreFinderPriority[Type] = 550;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        TileObjectData.newTile.DrawYOffset = 0;
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.addTile(Type);
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 300;
        Main.tileFrameImportant[Type] = true;
        DustType = DustID.Grass;
    }
	public override IEnumerable<Item> GetItemDrops(int i, int j)
	{
		yield return new Item(ModContent.ItemType<StaminaCrystal>());
	}
	public override void AnimateTile(ref int frame, ref int frameCounter)
    {
        frameCounter++;
        if (frameCounter > 6)
        {
            frameCounter = 0;
            frame++;
            if (frame >= 11) frame = 0;
        }
    }
    public override bool KillSound(int i, int j, bool fail)
    {
        if (!fail)
        {
            SoundEngine.PlaySound(SoundID.Shatter, new Vector2(i, j).ToWorldCoordinates());
            return false;
        }
        return base.KillSound(i, j, fail);
    }
}
