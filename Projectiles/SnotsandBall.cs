using Avalon.Items.Placeable.Tile;
using Avalon.Tiles.Contagion;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles;

public abstract class SnotsandBall : ModProjectile
{
    public override string Texture => "Avalon/Projectiles/SnotsandBall";
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.FallingBlockDoesNotFallThroughPlatforms[Projectile.type] = true;
        ProjectileID.Sets.ForcePlateDetection[Type] = true;
    }
}

public class SnotsandBallFallingProjectile : SnotsandBall
{
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        ProjectileID.Sets.FallingBlockTileItem[Type] = new(ModContent.TileType<Snotsand>(), ModContent.ItemType<SnotsandBlock>());
    }
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.CrimsandBallFalling);
    }
}

public class SnotsandSandgunProjectile : SnotsandBall
{
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        ProjectileID.Sets.FallingBlockTileItem[Type] = new(ModContent.TileType<Snotsand>());
    }
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.CrimsandBallGun);
        AIType = ProjectileID.CrimsandBallGun;
    }
}
