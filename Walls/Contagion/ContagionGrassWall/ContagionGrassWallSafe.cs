using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls.Contagion.ContagionGrassWall;

[LegacyName("ContagionGrassWall")]
public class ContagionGrassWallItem : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<ContagionGrassWallSafe>());
	}
}
public class ContagionGrassWallSafe : ModWall
{
	public override void SetStaticDefaults()
	{
		AddMapEntry(new Color(106, 116, 59));
		HitSound = SoundID.Grass;
		Main.wallHouse[Type] = true;
		DustType = ModContent.DustType<Dusts.ContagionDust>();
	}
}
[LegacyName("ContagionGrassWall")]
public class ContagionGrassWallUnsafe : ModWall
{
	public override string Texture => ModContent.GetInstance<ContagionGrassWallSafe>().Texture;
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(106, 116, 59));
        HitSound = SoundID.Grass;
        WallID.Sets.Conversion.Grass[Type] = true;
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}
