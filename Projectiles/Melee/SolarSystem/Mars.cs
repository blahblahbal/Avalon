using Avalon.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.SolarSystem;

public class Mars : ModProjectile
{
    private int hostPosition = -1;
    private LinkedListNode<int> positionNode;

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.penetrate = -1;
        Projectile.width = dims.Width;
        Projectile.height = dims.Height;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.tileCollide = false;
        Projectile.ownerHitCheck = true;
        Projectile.extraUpdates = 3;
        Projectile.timeLeft = 600;
        DrawOffsetX = -(int)((dims.Width / 2) - (Projectile.Size.X / 2));
        DrawOriginOffsetY = -(int)((dims.Width / 2) - (Projectile.Size.Y / 2));
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        AvalonPlayer modPlayer = Main.player[Projectile.owner].GetModPlayer<AvalonPlayer>();
        positionNode ??= modPlayer.HandleMars();
        writer.Write(positionNode.Value);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        base.ReceiveExtraAI(reader);
        hostPosition = reader.ReadInt32();
    }
    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        AvalonPlayer modPlayer = player.GetModPlayer<AvalonPlayer>();

        if (!Main.player[Projectile.owner].channel)
        {
            Projectile.ai[2] = 1;
        }
        else Projectile.timeLeft = 600;

        if (Projectile.ai[2] == 0)
        {
            // Get position in circle
            if (hostPosition == -1)
            {
                positionNode ??= modPlayer.HandleMars();
            }
            else
            {
                positionNode ??= modPlayer.ObtainExistingMars(hostPosition);
            }
            const int radius = 98;
            const float speed = 1.54f;
            Vector2 target = Main.projectile[(int)Projectile.ai[1]].Center +
                             (Vector2.One.RotatedBy(
                                 (MathHelper.TwoPi / modPlayer.Mars.Count * positionNode.Value) +
                                 modPlayer.MarsRotation) * radius);
            Vector2 error = target - Projectile.Center;

            Projectile.velocity = error * speed;
        }
        else if (Projectile.ai[2] == 1)
        {
            Projectile.velocity = Vector2.Normalize(Main.projectile[(int)Projectile.ai[1]].Center - player.Center) * 8f;
            //Projectile.ai[2] = 2;
        }
    }
}
