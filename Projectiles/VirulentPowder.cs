using System;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Drawing;

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

    public override void AI()
    {
        if (Projectile.ai[1] <= 1f)
        {
            Projectile.ai[1] = 2f;
            int dustType = ModContent.DustType<Dusts.ContagionPowder>();
            int num90 = 30;
            for (int num91 = 0; num91 < num90; num91++)
            {
                Dust dust7 = Main.dust[Dust.NewDust(Entity.position, Entity.width, Entity.height, dustType, Entity.velocity.X, Entity.velocity.Y, 50)];
                dust7.noGravity = true;
            }
        }
        bool flag4 = Main.myPlayer == Projectile.owner;
        //if (flag4)
        //{
        //    int num92 = (int)(Entity.position.X / 16f) - 1;
        //    int num93 = (int)((Entity.position.X + (float)Entity.width) / 16f) + 2;
        //    int num94 = (int)(Entity.position.Y / 16f) - 1;
        //    int num95 = (int)((Entity.position.Y + (float)Entity.height) / 16f) + 2;
        //    if (num92 < 0)
        //    {
        //        num92 = 0;
        //    }
        //    if (num93 > Main.maxTilesX)
        //    {
        //        num93 = Main.maxTilesX;
        //    }
        //    if (num94 < 0)
        //    {
        //        num94 = 0;
        //    }
        //    if (num95 > Main.maxTilesY)
        //    {
        //        num95 = Main.maxTilesY;
        //    }
        //    Vector2 vector15 = default(Vector2);
        //    for (int num96 = num92; num96 < num93; num96++)
        //    {
        //        for (int num97 = num94; num97 < num95; num97++)
        //        {
        //            vector15.X = num96 * 16;
        //            vector15.Y = num97 * 16;
        //            if (!(Entity.position.X + (float)Entity.width > vector15.X) || !(Entity.position.X < vector15.X + 16f) || !(Entity.position.Y + (float)Entity.height > vector15.Y) || !(Entity.position.Y < vector15.Y + 16f) || !Main.tile[num96, num97].HasTile);
        //            {
        //                continue;
        //            }
        //                WorldGen.Convert(num96, num97, 4, 1);
        //        }
        //    }
        //}

        //Copied from Projectile.cs, for converting NPCs

        //if ((type == 11 || type == 463) && Main.netMode != 1)
        //{
        //    bool crimson = type == 463;
        //    for (int num47 = 0; num47 < 200; num47++)
        //    {
        //        if (Main.npc[num47].active)
        //        {
        //            Rectangle value2 = new Rectangle((int)Main.npc[num47].position.X, (int)Main.npc[num47].position.Y, Main.npc[num47].width, Main.npc[num47].height);
        //            if (rectangle.Intersects(value2))
        //            {
        //                Main.npc[num47].AttemptToConvertNPCToEvil(crimson);
        //            }
        //        }
        //    }
        //}
    }
}
