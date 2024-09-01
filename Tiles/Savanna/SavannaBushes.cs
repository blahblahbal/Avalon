using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Savanna;

public class SavannaBushes : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(150, 125, 8), this.GetLocalization("MapEntry"));
        Main.tileFrameImportant[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
        TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.newTile.LavaDeath = true;
        TileObjectData.addTile(Type);
        DustType = ModContent.DustType<SavannaGrassBladeDust>();
        HitSound = SoundID.Grass;
    }
}
