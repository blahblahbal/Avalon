using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.PlayerDrawLayers;

public class ItemGlowmask : GlobalItem
{
    public int glowOffsetX = 10; // defaults to 10 for vanilla holdout offset
    public int glowOffsetY;
    public Texture2D glowTexture;
    public int glowAlpha = 255;

    public override bool InstancePerEntity => true;
}

public class PlayerUseItemGlowmask : PlayerDrawLayer
{
    public override Position GetDefaultPosition() => new AfterParent(Terraria.DataStructures.PlayerDrawLayers.HeldItem);
    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        if (drawInfo.shadow != 0)
            return;

        Player drawPlayer = drawInfo.drawPlayer;
        Item heldItem = drawPlayer.HeldItem;

        if (!heldItem.IsAir)
        {
            Texture2D texture = heldItem.GetGlobalItem<ItemGlowmask>().glowTexture;
            Color color = new Color(255, 255, 255, heldItem.GetGlobalItem<ItemGlowmask>().glowAlpha);
            if (heldItem.GetGlobalItem<ItemGlowmask>().glowTexture != null && drawPlayer.itemAnimation > 0)
            {
                Vector2 basePosition = drawPlayer.itemLocation - Main.screenPosition;
                basePosition = new Vector2((int)basePosition.X, (int)basePosition.Y) + (drawPlayer.RotatedRelativePoint(drawPlayer.MountedCenter) - drawPlayer.Center);
                if (heldItem.useStyle == ItemUseStyleID.Shoot)
                {
                    if (Item.staff[heldItem.type])
                    {
                        if (heldItem.type == ModContent.ItemType<Items.Material.PointingLaser>())
                        {
                            if (drawPlayer.team == (int)Terraria.Enums.Team.Red || Main.netMode == NetmodeID.SinglePlayer)
                            {
                                color.R = 218;
                                color.G = 59;
                                color.B = 59;
                            }
                            if (drawPlayer.team == (int)Terraria.Enums.Team.Yellow)
                            {
                                color.R = 218;
                                color.G = 183;
                                color.B = 59;
                            }
                            if (drawPlayer.team == (int)Terraria.Enums.Team.Green)
                            {
                                color.R = 59;
                                color.G = 218;
                                color.B = 85;
                            }
                            if (drawPlayer.team == (int)Terraria.Enums.Team.Blue)
                            {
                                color.R = 59;
                                color.G = 149;
                                color.B = 218;
                            }
                            if (drawPlayer.team == (int)Terraria.Enums.Team.Pink)
                            {
                                color.R = 171;
                                color.G = 59;
                                color.B = 218;
                            }
                        }
                        float rotationMod = MathHelper.PiOver4 * -drawPlayer.direction * drawPlayer.gravDir;
                        DrawData staffDraw = new DrawData(
                            texture,                                                        // texture
                            basePosition,                                                   // position
                            default,                                                        // texture coords
                            color,                                                          // color
                            drawPlayer.itemRotation - rotationMod,                          // rotation
                            new Vector2(drawPlayer.direction == -1 ? texture.Width : 0,     // origin X
                            drawPlayer.gravDir == 1 ? texture.Height : 0),                  // origin Y
                            drawPlayer.GetAdjustedItemScale(heldItem),                      // scale
                            drawInfo.itemEffect                                             // sprite effects
                            );
                        drawInfo.DrawDataCache.Add(staffDraw);

                    }
                    else
                    {
                        Vector2 offsetFix = new Vector2(texture.Width / 2, texture.Height / 2 + (heldItem.GetGlobalItem<ItemGlowmask>().glowOffsetY * drawPlayer.gravDir));
                        int glowOffsetXDefaultTo = heldItem.GetGlobalItem<ItemGlowmask>().glowOffsetX + heldItem.GetGlobalItem<ItemGlowmask>().glowOffsetX * -2;
                        Vector2 positionFix = new Vector2(drawPlayer.direction == -1 ? texture.Width - glowOffsetXDefaultTo : glowOffsetXDefaultTo, texture.Height / 2);

                        DrawData horizontalStaffDraw = new DrawData(
                            texture,                                        // texture
                            basePosition + offsetFix,                       // position
                            default,                                        // texture coords
                            color,                                          // color
                            drawPlayer.itemRotation,                        // rotation
                            positionFix,                                    // origin
                            drawPlayer.GetAdjustedItemScale(heldItem),      // scale
                            drawInfo.itemEffect                             // sprite effects
                            );
                        drawInfo.DrawDataCache.Add(horizontalStaffDraw);
                    }
                }
                else
                {
                    DrawData swingDraw = new DrawData(
                        texture,                                                        // texture
                        basePosition,                                                   // position
                        default,                                                        // texture coords
                        color,                                                          // color
                        drawPlayer.itemRotation,                                        // rotation
                        new Vector2(drawPlayer.direction == -1 ? texture.Width : 0,     // origin X
                        drawPlayer.gravDir == 1 ? texture.Height : 0),                  // origin Y
                        drawPlayer.GetAdjustedItemScale(heldItem),                      // scale
                        drawInfo.itemEffect                                             // sprite effects
                        );
                    drawInfo.DrawDataCache.Add(swingDraw);
                }
            }
        }
    }
}
