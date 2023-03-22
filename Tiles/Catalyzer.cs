using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class Catalyzer : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(146, 155, 187), LanguageManager.Instance.GetText("Catalyzer"));
        Main.tileFrameImportant[Type] = true;
        AnimationFrameHeight = 38;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.addTile(Type);
        Main.tileLighted[Type] = true;
        DustType = DustID.Stone;
    }

    public override void AnimateTile(ref int frame, ref int frameCounter)
    {
        frameCounter++;
        if (frameCounter > 4)
        {
            frameCounter = 0;
            frame++;
            if (frame >= 12) frame = 0;
        }
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 48, 32, ModContent.ItemType<Items.Placeable.Crafting.Catalyzer>());
    }
}
