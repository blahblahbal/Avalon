using System;
using System.Collections.Generic;
using System.Linq;
using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Items.Accessories.Hardmode;
using Avalon.Items.Material.Ores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon;

public static class ClassExtensions
{
    public static List<List<Point>> AddValidNeighbors(List<List<Point>> p, Point start)
    {
        p.Add(new List<Point>()
        {
            start + new Point(0, -1), start + new Point(0, 1), start + new Point(-1, 0), start + new Point(1, 0)
        });

        return p;
    }

    public static bool DownedAllButOneMechBoss()
    {
        return NPC.downedMechBoss1 && NPC.downedMechBoss2 && !NPC.downedMechBoss3 ||
            NPC.downedMechBoss1 && !NPC.downedMechBoss2 && NPC.downedMechBoss3 ||
            !NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3;
    }
    public static Vector2 LengthClamp(this Vector2 vector,float max, float min = 0)
    {
        if (vector.Length() > max) return Vector2.Normalize(vector) * max;
        else if (vector.Length() < min) return Vector2.Normalize(vector) * min;
        else return vector;
    }
    //public static bool AnyOreRiftsInRange(int x, int y, Item )
    //{
    //    for (int i = 0; i < 400; i++)
    //    {
    //        if (Main.item[i].type == ModContent.ItemType<Items.OreRift>() && Vector2.Distance(Main.item[i].position, 
    //    }
    //}


    /// <summary>
    /// Harvests an area using a veinminer algorithm.
    /// </summary>
    /// <param name="p">The tile coordinates as a Point.</param>
    /// <param name="type">The tile type to veinmine.</param>
    public static void VeinMine(Point p, int type, int maxTiles = 500)
    {
        int tiles = 0;

        Tile tile = Framing.GetTileSafely(p);
        if (!tile.HasTile || tile.TileType != type)
        {
            return;
        }

        List<List<Point>> points = new List<List<Point>>();
        points = AddValidNeighbors(points, p);

        int index = 0;
        while (points.Count > 0 && tiles < maxTiles && index < points.Count)
        {
            List<Point> tilePos = points[index];
            foreach (Point a in tilePos)
            {
                Tile t = Framing.GetTileSafely(a.X, a.Y);
                if (t.HasTile && t.TileType == type)
                {
                    WorldGen.KillTile(a.X, a.Y);
                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 20, a.X, a.Y);
                    }
                    tiles++;
                    AddValidNeighbors(points, a);
                }
            }
            index++;
        }
    }

    public static void RiftReplace(Point p, int type, int replace, int maxTiles = 500)
    {
        int tiles = 0;

        Tile tile = Framing.GetTileSafely(p);
        if (!tile.HasTile || tile.TileType != type)
        {
            return;
        }

        List<List<Point>> points = new List<List<Point>>();
        points = AddValidNeighbors(points, p);

        int index = 0;
        while (points.Count > 0 && tiles < maxTiles && index < points.Count)
        {
            List<Point> tilePos = points[index];
            foreach (Point a in tilePos)
            {
                Tile t = Framing.GetTileSafely(a.X, a.Y);
                if (t.HasTile && t.TileType == type)
                {
                    Tile q = Framing.GetTileSafely(a.X, a.Y);
                    if (replace == ushort.MaxValue)
                    {
                        q.HasTile = false;
                    }
                    else
                    {
                        q.TileType = (ushort)replace;
                        WorldGen.SquareTileFrame(a.X, a.Y);
                        if (Main.netMode != NetmodeID.SinglePlayer)
                        {
                            NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 21, a.X, a.Y, replace);
                        }
                    }
                    tiles++;
                    AddValidNeighbors(points, a);
                }
            }
            index++;
        }
    }

    /// <summary>
    ///     Helper method for checking if the current item is an armor piece - used for armor prefixes.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>Whether or not the item is an armor piece.</returns>
    public static bool IsArmor(this Item item) =>
        (item.headSlot != -1 || item.bodySlot != -1 || item.legSlot != -1) && !item.vanity;

    /// <summary>
    /// Helper method to check if the current item is a tool - used for tool prefixes.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>Whether or not the item is a tool.</returns>
    public static bool IsTool(this Item item)
    {
        return item.pick > 0 || item.axe > 0 || item.hammer > 0;
    }

    public static void Active(this Tile t, bool a) => t.HasTile = a;

    public static Vector2 ClampToCircle(Vector2 center, float radius, Vector2 pos)
    {
        // Calculate the offset vector from the center of the circle to our position
        Vector2 offset = pos - center;
        // Calculate the linear distance of this offset vector
        float distance = offset.Length();
        if (radius < distance)
        {
            // If the distance is more than our radius we need to clamp
            // Calculate the direction to our position
            Vector2 direction = offset / distance;
            // Calculate our new position using the direction to our old position and our radius
            pos = center + direction * radius;
            return pos;
        }
        return pos;
    }
    public static void ConsumeStamina(this Player p, int amt)
    {
        if (p.GetModPlayer<AvalonStaminaPlayer>().StaminaDrain)
        {
            amt *= (int)(p.GetModPlayer<AvalonStaminaPlayer>().StaminaDrainStacks * p.GetModPlayer<AvalonStaminaPlayer>().StaminaDrainMult);
        }

        if (p.GetModPlayer<AvalonStaminaPlayer>().StatStam >= amt)
        {
            p.GetModPlayer<AvalonStaminaPlayer>().StatStam -= amt;
        }
        else if (p.GetModPlayer<AvalonStaminaPlayer>().StamFlower)
        {
            p.GetModPlayer<AvalonStaminaPlayer>().QuickStamina();
            if (p.GetModPlayer<AvalonStaminaPlayer>().StatStam >= amt)
            {
                p.GetModPlayer<AvalonStaminaPlayer>().StatStam -= amt;
            }
        }
    }
    public static void AttemptToConvertNPCToContagion(this NPC n)
    {
        if (n.type == NPCID.Bunny)
        {
            n.Transform(ModContent.NPCType<NPCs.Critters.ContaminatedBunny>());
        }
        if (n.type == NPCID.Goldfish)
        {
            n.Transform(ModContent.NPCType<NPCs.Critters.ContaminatedGoldfish>());
        }
        if (n.type == NPCID.Penguin)
        {
            n.Transform(ModContent.NPCType<NPCs.Critters.ContaminatedPenguin>());
        }
    }

    public static Item HasItemInArmorFindIt(this Player p, int type)
    {
        for (int i = 0; i < p.armor.Length; i++)
        {
            if (p.armor[i].type == type) return p.armor[i];
        }
        return null;
    }
    public static int HasItemInArmorReturnIndex(this Player p, int type)
    {
        for (int i = 0; i < p.armor.Length; i++)
        {
            if (p.armor[i].type == type) return i;
        }
        return -1;
    }

    public static void SendPacket(this Player p, ModPacket packet, bool server)
    {
        if (!server)
        {
            packet.Send();
        }
        else
        {
            packet.Send(-1, p.whoAmI);
        }
    }

    /// <summary>
    ///     Checks if the current player has an item in their armor/accessory slots.
    /// </summary>
    /// <param name="p">The player.</param>
    /// <param name="type">The item ID to check.</param>
    /// <returns>Whether or not the item is found.</returns>
    public static bool HasItemInArmor(this Player p, int type) => p.armor.Any(t => type == t.type);
    public static bool InPillarZone(this Player p)
    {
        if (!p.ZoneTowerStardust && !p.ZoneTowerVortex && !p.ZoneTowerSolar)
        {
            return p.ZoneTowerNebula;
        }

        return true;
    }

    /// <summary>
    ///     Used to draw float coordinates to rounded coordinates to avoid blurry rendering of textures.
    /// </summary>
    /// <param name="vector">The vector to convert.</param>
    /// <returns>The rounded vector.</returns>
    public static Vector2 ToNearestPixel(this Vector2 vector) => new((int)(vector.X + 0.5f), (int)(vector.Y + 0.5f));
    
    /// <summary>
    ///     Helper method for Vampire Teeth and Blah's Knives life steal.
    /// </summary>
    /// <param name="p">The player.</param>
    /// <param name="dmg">The damage to use in the life steal calculation.</param>
    /// <param name="position">The position to spawn the life steal projectile at.</param>
    public static void VampireHeal(this Player p, int dmg, Vector2 position)
    {
        float num = dmg * 0.075f;
        if ((int)num == 0)
        {
            return;
        }

        if (p.lifeSteal <= 0f)
        {
            return;
        }

        p.lifeSteal -= num;
        int num2 = p.whoAmI;
        Projectile.NewProjectile(
            p.GetSource_Accessory(new Item(ModContent.ItemType<VampireTeeth>())),
            position.X, position.Y, 0f, 0f, ProjectileID.VampireHeal, 0, 0f, p.whoAmI, num2, num);
    }
    public static Asset<T> VanillaLoad<T>(this Asset<T> asset) where T : class
    {
        try
        {
            if (asset.State == AssetState.NotLoaded)
            {
                Main.Assets.Request<Texture2D>(asset.Name, AssetRequestMode.ImmediateLoad);
            }
        }
        catch (AssetLoadException e)
        {
        }

        return asset;
    }
    public static int GetRhodiumVariantItemOre(this AvalonWorld.RhodiumVariant? rhodiumVariant)
    {
        return rhodiumVariant switch
        {
            AvalonWorld.RhodiumVariant.Osmium => ModContent.ItemType<OsmiumOre>(),
            AvalonWorld.RhodiumVariant.Rhodium => ModContent.ItemType<RhodiumOre>(),
            AvalonWorld.RhodiumVariant.Iridium => ModContent.ItemType<IridiumOre>(),
            _ => -1,
        };
    }
    /// <summary>
    /// Extension to hide something from displaying in the Bestiary.
    /// </summary>
    /// <param name="itemDropRule"></param>
    /// <returns></returns>
    public static LeadingConditionRule HideFromBestiary(this IItemDropRule itemDropRule)
    {
        var conditionRule = new LeadingConditionRule(new Conditions.NeverTrue());
        conditionRule.OnFailedConditions(itemDropRule, true);
        return conditionRule;
    }
    /// <summary>
    ///     A helper method to check if the given Player is touching the ground.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns>True if the player is touching the ground, false otherwise.</returns>
    public static bool IsOnGround(this Player player) =>
        (Main.tile[(int)(player.position.X / 16f), (int)(player.position.Y / 16f) + 3].HasTile &&
         Main.tileSolid[
             Main.tile[(int)(player.position.X / 16f), (int)(player.position.Y / 16f) + 3].TileType]) ||
        (Main.tile[(int)(player.position.X / 16f) + 1, (int)(player.position.Y / 16f) + 3].HasTile &&
         Main.tileSolid[
             Main.tile[(int)(player.position.X / 16f) + 1, (int)(player.position.Y / 16f) + 3].TileType] &&
         player.velocity.Y == 0f);

    public static bool PlayerDoublePressedSetBonusActivateKey(this Player player)
    {
        return (player.doubleTapCardinalTimer[Main.ReversedUpDownArmorSetBonuses ? 1 : 0] < 15 && ((player.releaseUp && Main.ReversedUpDownArmorSetBonuses && player.controlUp) || (player.releaseDown && !Main.ReversedUpDownArmorSetBonuses && player.controlDown)));
    }

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

    public static void DrawGas(string Texture, Color color, Projectile projectile, float spread, int iterations)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        int frameHeight = texture.Height / Main.projFrames[projectile.type];
        Rectangle frame = new Rectangle(0, frameHeight * projectile.frame, texture.Width, frameHeight);
        Vector2 drawPos = projectile.Center - Main.screenPosition;
        Main.EntitySpriteDraw(texture, drawPos, frame, color * projectile.Opacity, projectile.rotation, new Vector2(texture.Width, frameHeight) / 2, projectile.scale, SpriteEffects.None, 0);

        for (int i = 0; i < iterations; i++)
        {
            Main.EntitySpriteDraw(texture, drawPos + new Vector2(0, projectile.width / spread * ((float)projectile.alpha) / 128).RotatedBy(i * (MathHelper.TwoPi) / iterations), frame, color * projectile.Opacity * 0.4f, projectile.rotation + ((float)projectile.alpha / 128) * (i / 128), new Vector2(texture.Width, frameHeight) / 2, projectile.scale, SpriteEffects.FlipVertically, 0);
        }
    }
    public static void Load<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TagCompound tag)
        where TKey : notnull
    {
        if (tag.ContainsKey("keys") && tag.ContainsKey("values"))
        {
            TKey[] keys = tag.Get<TKey[]>("keys");
            TValue[] values = tag.Get<TValue[]>("values");

            for (int i = 0; i < keys.Length; i++)
            {
                dictionary[keys[i]] = values[i];
            }
        }
    }

    public static Color CycleThroughColors(Color[] Colors, int Rate, float Offset = 0)
    {
        float fade = ((Main.GameUpdateCount + Offset) % Rate) / (float)Rate;
        int index = (int)(((Main.GameUpdateCount + Offset) / (float)Rate) % Colors.Length);
        int nextIndex = (index + 1) % Colors.Length;

        return Color.Lerp(Colors[index], Colors[nextIndex], fade);
    }
    public static TagCompound Save<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
       where TKey : notnull
    {
        TKey[] keys = dictionary.Keys.ToArray();
        TValue[] values = dictionary.Values.ToArray();
        var tag = new TagCompound();
        tag.Set("keys", keys);
        tag.Set("values", values);
        return tag;
    }

    public static Rectangle GetDims(this ModTexturedType texturedType) =>
        Main.netMode == NetmodeID.Server ? Rectangle.Empty : texturedType.GetTexture().Frame();

    public static Rectangle GetDims(this ModItem modItem) =>
        Main.netMode == NetmodeID.Server ? Rectangle.Empty : modItem.GetTexture().Frame();

    public static Rectangle GetDims(this ModProjectile modProjectile) =>
        Main.netMode == NetmodeID.Server ? Rectangle.Empty : modProjectile.GetTexture().Frame();

    public static Asset<Texture2D> GetTexture(this ModTexturedType texturedType) =>
        ModContent.Request<Texture2D>(texturedType.Texture);

    public static Asset<Texture2D> GetTexture(this ModItem modItem) =>
        ModContent.Request<Texture2D>(modItem.Texture);

    public static Asset<Texture2D> GetTexture(this ModProjectile modProjectile) =>
        ModContent.Request<Texture2D>(modProjectile.Texture);
}
