using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ExxoAvalonOrigins.Common
{
    public static class ClassExtensions
    {
        public static void GetPointOnSwungItemPath(float spriteWidth, float spriteHeight, float normalizedPointOnPath, float itemScale, out Vector2 location, out Vector2 outwardDirection, Player player)
        {
            float num = (float)Math.Sqrt(spriteWidth * spriteWidth + spriteHeight * spriteHeight);
            float num2 = (float)(player.direction == 1).ToInt() * ((float)Math.PI / 2f);
            if (player.gravDir == -1f)
            {
                num2 += (float)Math.PI / 2f * (float)player.direction;
            }
            outwardDirection = player.itemRotation.ToRotationVector2().RotatedBy(3.926991f + num2);
            location = player.RotatedRelativePoint(player.itemLocation + outwardDirection * num * normalizedPointOnPath * itemScale);
        }
        public static int FindClosestNPC(this Entity entity, float maxDistance, Func<NPC, bool> invalidNPCPredicate)
        {
            int closest = -1;
            float lastDistance = maxDistance;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (invalidNPCPredicate.Invoke(npc))
                {
                    continue;
                }

                if (Vector2.Distance(entity.Center, npc.Center) < lastDistance)
                {
                    lastDistance = Vector2.Distance(entity.Center, npc.Center);
                    closest = i;
                }
            }

            return closest;
        }
    }
}
