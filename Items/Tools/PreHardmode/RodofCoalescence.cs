using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using System;
using Avalon.Systems;

namespace Avalon.Items.Tools.PreHardmode;

public class RodofCoalescence : ModItem
{
    public override void SetDefaults()
    {
        Item.autoReuse = false;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 60;
        Item.useTime = 60;
        Item.width = 20;
        Item.height = 20;
        Item.UseSound = SoundID.Item8;
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 2);
    }
    public override bool CanUseItem(Player player)
    {
        return !player.HasBuff(ModContent.BuffType<Buffs.Debuffs.Coalesced>());
    }
    public override bool? UseItem(Player player)
    {
        Vector2 position = player.getRect().TopLeft();
        for (int num20 = 0; num20 < 5; num20++)
        {
            Dust obj8 = Dust.NewDustDirect(position, player.getRect().Width, player.getRect().Height + 24, DustID.Water);
            obj8.velocity.Y *= 0f;
            obj8.velocity.Y -= 3.5f;
            obj8.velocity.X *= 1.5f;
            obj8.scale = 0.8f;
            obj8.alpha = 130;
            obj8.noGravity = true;
            obj8.fadeIn = 1.2f;
        }
        if (player.ItemAnimationEndingOrEnded && !player.HasBuff(ModContent.BuffType<Buffs.Debuffs.Coalesced>()))
        {
			if (player.whoAmI == Main.myPlayer)
			{
				if (TeleportPlayer(player))
				{
					player.AddBuff(ModContent.BuffType<Buffs.Debuffs.Coalesced>(), 60 * 5);
					return true;
				}
			}
        }
        return false;
    }
    public bool TeleportPlayer(Player player)
    {
        Vector2 pointPosition = player.GetModPlayer<AvalonPlayer>().MousePosition;
        if (player.gravDir == 1f)
        {
            pointPosition.Y -= player.height;
        }
        pointPosition.X -= player.width / 2;
        LimitPointToArea(player, 20, ref pointPosition);
        if (pointPosition.X < 50f || pointPosition.X > Main.maxTilesX * 16 - 50 || pointPosition.Y < 50f || pointPosition.Y > Main.maxTilesY * 16 - 50)
        {
            return false;
        }
        Point tileCoords = pointPosition.ToTileCoordinates();
        if ((Main.tile[tileCoords.X, tileCoords.Y].WallType == WallID.LihzahrdBrickUnsafe && !NPC.downedPlantBoss && (Main.remixWorld || tileCoords.Y > Main.worldSurface) ||
            Data.Sets.Wall.Hellcastle[Main.tile[tileCoords.X, tileCoords.Y].WallType] && !ModContent.GetInstance<DownedBossSystem>().DownedPhantasm) ||
            Collision.SolidCollision(pointPosition, player.width, player.height))
        {
            return false;
        }
        player.Teleport(pointPosition, 6);
        NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, player.whoAmI, pointPosition.X, pointPosition.Y, 6);
        for (int num20 = 0; num20 < 75; num20++)
        {
            Dust obj8 = Dust.NewDustDirect(pointPosition, player.getRect().Width, player.getRect().Height + 24, DustID.Water);
            obj8.velocity.Y *= 0f;
            obj8.velocity.Y -= 3.5f;
            obj8.velocity.X *= 1.5f;
            obj8.scale = 1.2f;
            obj8.alpha = 130;
            obj8.noGravity = true;
            obj8.fadeIn = 0f;
        }
        return true;

    }
    public void LimitPointToArea(Player player, int tiles, ref Vector2 pointPoisition)
    {
        Vector2 center = player.Center;
        Vector2 val = pointPoisition - center;
        float num = Math.Abs(val.X);
        float num2 = Math.Abs(val.Y);
        float num3 = 1f;
        if (num > tiles * 16)
        {
            float num4 = (tiles * 16) / num;
            
            if (num3 > num4)
            {
                num3 = num4;
                
            }
        }
        if (num2 > tiles * 16)
        {
            float num5 = (tiles * 16) / num2;
            if (num3 > num5)
            {
                num3 = num5;
            }
        }
        Vector2 vector2 = val * num3;
        pointPoisition = center + vector2;
    }
}
