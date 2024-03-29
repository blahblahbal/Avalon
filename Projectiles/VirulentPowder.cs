using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles;

public class VirulentPowder : ModProjectile
{
    public override void SetDefaults()
    {
		Projectile.width = 48;
        Projectile.height = 48;
        Projectile.aiStyle = 6;
        Projectile.friendly = true;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.alpha = 255;
        Projectile.ignoreWater = true;
    }
    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
        width = Projectile.width + 32;
        height = Projectile.height + 32;
        return true;
    }

    public override bool? CanCutTiles()
    {
        return false;
    }

    public override void AI()
    {
        if (Projectile.ai[1] <= 1f)
        {
            Projectile.ai[1] = 2f;
            int dustType = ModContent.DustType<Dusts.ContagionPowder>();
            int num90 = 30;
            for (int num91 = 0; num91 < num90; num91++)
            {
                float randVel = Main.rand.NextFloat(1f, 1.75f);
                Dust dust7 = Main.dust[Dust.NewDust(Entity.position, Entity.width, Entity.height, dustType, Entity.velocity.X * randVel, Entity.velocity.Y * randVel, 50)];
                dust7.noGravity = true;
            }
        }
        bool flag34 = Main.myPlayer == Projectile.owner;

        int num988 = (int)(Projectile.position.X / 16f) - 1;
        int num999 = (int)((Projectile.position.X + (float)Projectile.width) / 16f) + 2;
        int num1010 = (int)(Projectile.position.Y / 16f) - 1;
        int num1021 = (int)((Projectile.position.Y + (float)Projectile.height) / 16f) + 2;
        if (num988 < 0)
        {
            num988 = 0;
        }
        if (num999 > Main.maxTilesX)
        {
            num999 = Main.maxTilesX;
        }
        if (num1010 < 0)
        {
            num1010 = 0;
        }
        if (num1021 > Main.maxTilesY)
        {
            num1021 = Main.maxTilesY;
        }
        Vector2 vector57 = default(Vector2);
        for (int num1032 = num988; num1032 < num999; num1032++)
        {
            for (int num1043 = num1010; num1043 < num1021; num1043++)
            {
                vector57.X = num1032 * 16;
                vector57.Y = num1043 * 16;
                if (!(Projectile.position.X + (Projectile.width + 32) > vector57.X) || !(Projectile.position.X - (Projectile.width + 32) < vector57.X + 16f) || !(Projectile.position.Y + (Projectile.height + 32) > vector57.Y) || !(Projectile.position.Y - (Projectile.height + 32) < vector57.Y + 16f))
                {
                    continue;
                }
                WorldGeneration.Helpers.ConversionHelper.ConvertToContagion(num1032, num1043, 2);
                Tile tile = Main.tile[num1032, num1043];
                if (tile.TileType >= 0 && tile.TileType < TileID.Count && TileID.Sets.CommonSapling[tile.TileType])
                {
                    if (Main.remixWorld && num1043 >= (int)Main.worldSurface - 1 && num1043 < Main.maxTilesY - 20)
                    {
                        WorldGen.AttemptToGrowTreeFromSapling(num1032, num1043, underground: false);
                    }
                    WorldGen.AttemptToGrowTreeFromSapling(num1032, num1043, num1043 > (int)Main.worldSurface - 1);
                }
            }
        }
        //Copied from Projectile.cs, for converting NPCs
        if (Main.netMode != NetmodeID.MultiplayerClient)
        {
            for (int num47 = 0; num47 < 200; num47++)
            {
                if (Main.npc[num47].active)
                {
                    Rectangle value2 = new Rectangle((int)Main.npc[num47].position.X, (int)Main.npc[num47].position.Y, Main.npc[num47].width, Main.npc[num47].height);
                    if (new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height).Intersects(value2))
                    {
                        Main.npc[num47].AttemptToConvertNPCToContagion();
                    }
                }
            }
        }
    }
}
