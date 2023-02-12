using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace ExxoAvalonOrigins.PlayerDrawLayers;


/// <summary>
/// Adapted from ItemUseGlow from Spirit mod.
/// </summary>
public class ItemGlowmask : GlobalItem
{
    public int glowOffsetX;
    public int glowOffsetY;
    public Texture2D glowTexture;

    public override bool InstancePerEntity => true;
}

public class PlayerUseItemGlowmask : PlayerDrawLayer
{
    public override Position GetDefaultPosition() => new AfterParent(Terraria.DataStructures.PlayerDrawLayers.HeldItem);
    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        Item heldItem = drawInfo.heldItem;
        if (drawInfo.shadow != 0)
            return;

        Player drawPlayer = drawInfo.drawPlayer;
        Mod mod = ModLoader.GetMod("ExxoAvalonOrigins");
        if (!drawPlayer.HeldItem.IsAir)
        {
            Item item = drawPlayer.HeldItem;
            Texture2D texture = item.GetGlobalItem<ItemGlowmask>().glowTexture;
            Vector2 zero2 = Vector2.Zero;

            if (texture != null && drawPlayer.itemAnimation > 0)
            {
                Vector2 value2 = drawInfo.ItemLocation;
                if (item.useStyle == ItemUseStyleID.Shoot)
                {
                    bool flag14 = Item.staff[item.type];
                    if (flag14)
                    {
                        float num104 = drawPlayer.itemRotation + 0.785f * drawPlayer.direction;
                        int num105 = 0;
                        int num106 = 0;
                        Vector2 zero3 = new Vector2(0f, TextureAssets.Item[item.type].Value.Height);

                        if (drawPlayer.gravDir == -1f)
                        {
                            if (drawPlayer.direction == -1)
                            {
                                num104 += 1.57f;
                                zero3 = new Vector2(TextureAssets.Item[item.type].Value.Width, 0f);
                                num105 -= TextureAssets.Item[item.type].Value.Width;
                            }
                            else
                            {
                                num104 -= 1.57f;
                                zero3 = Vector2.Zero;
                            }
                        }
                        else if (drawPlayer.direction == -1)
                        {
                            zero3 = new Vector2(TextureAssets.Item[item.type].Value.Width, TextureAssets.Item[item.type].Value.Height);
                            num105 -= TextureAssets.Item[item.type].Value.Width;
                        }
                        Color c = Color.White;
                        if (item.type == ModContent.ItemType<Items.Material.PointingLaser>())
                        {
                            if (drawPlayer.team == (int)Terraria.Enums.Team.Red || Main.netMode == NetmodeID.SinglePlayer)
                            {
                                c = new Color(218, 59, 59);
                            }
                            if (drawPlayer.team == (int)Terraria.Enums.Team.Yellow)
                            {
                                c = new Color(218, 183, 59);
                            }
                            if (drawPlayer.team == (int)Terraria.Enums.Team.Green)
                            {
                                c = new Color(59, 218, 85);
                            }
                            if (drawPlayer.team == (int)Terraria.Enums.Team.Blue)
                            {
                                c = new Color(59, 149, 218);
                            }
                            if (drawPlayer.team == (int)Terraria.Enums.Team.Pink)
                            {
                                c = new Color(171, 59, 218);
                            }
                        }
                        DrawData value = new DrawData(texture,
                            new Vector2((int)(value2.X - Main.screenPosition.X + zero3.X + num105), (int)(value2.Y - Main.screenPosition.Y + num106)),
                            new Rectangle?(new Rectangle(0, 0, TextureAssets.Item[item.type].Value.Width, TextureAssets.Item[item.type].Value.Height)),
                            c, num104, zero3, drawInfo.drawPlayer.GetAdjustedItemScale(heldItem), drawInfo.itemEffect, 0);
                        drawInfo.DrawDataCache.Add(value);

                    }
                    else
                    {
                        Vector2 vector10 = new Vector2((TextureAssets.Item[item.type].Value.Width / 2), TextureAssets.Item[item.type].Value.Height / 2);

                        //Vector2 vector11 = this.DrawPlayerItemPos(drawPlayer.gravDir, item.type);
                        Vector2 vector11 = new Vector2(10, texture.Height / 2);
                        if (item.GetGlobalItem<ItemGlowmask>().glowOffsetX != 0)
                        {
                            vector11.X = item.GetGlobalItem<ItemGlowmask>().glowOffsetX;
                        }
                        vector11.Y += item.GetGlobalItem<ItemGlowmask>().glowOffsetY * drawPlayer.gravDir;
                        int num107 = (int)vector11.X;
                        vector10.Y = vector11.Y;
                        Vector2 origin5 = new Vector2((float)(-(float)num107), TextureAssets.Item[item.type].Value.Height / 2);
                        if (drawPlayer.direction == -1)
                        {
                            origin5 = new Vector2(TextureAssets.Item[item.type].Value.Width + num107, TextureAssets.Item[item.type].Value.Height / 2);
                        }

                        //value = new DrawData(TextureAssets.Item[item.type], new Vector2((float)((int)(value2.X - Main.screenPosition.X + vector10.X)), (float)((int)(value2.Y - Main.screenPosition.Y + vector10.Y))), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, TextureAssets.Item[item.type].Width, TextureAssets.Item[item.type].Height)), item.GetAlpha(color37), drawPlayer.itemRotation, origin5, item.scale, effect, 0);
                        //Main.playerDrawData.Add(value);


                        DrawData value = new DrawData(texture, new Vector2((int)(value2.X - Main.screenPosition.X + vector10.X), (int)(value2.Y - Main.screenPosition.Y + vector10.Y)), new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, 0, TextureAssets.Item[item.type].Value.Width, TextureAssets.Item[item.type].Value.Height)), Color.White, drawPlayer.itemRotation, origin5, drawInfo.drawPlayer.GetAdjustedItemScale(heldItem), drawInfo.itemEffect, 0);
                        drawInfo.DrawDataCache.Add(value);
                    }
                }
                else
                {
                    DrawData value = new DrawData(texture,
                        new Vector2((int)(value2.X - Main.screenPosition.X),
                            (int)(value2.Y - Main.screenPosition.Y)), new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)),
                        Color.White,
                        drawPlayer.itemRotation,
                        new Vector2(texture.Width * 0.5f - texture.Width * 0.5f * drawPlayer.direction, drawPlayer.gravDir == -1 ? 0f : texture.Height),
                        drawInfo.drawPlayer.GetAdjustedItemScale(heldItem),
                        drawInfo.itemEffect,
                        0);

                    drawInfo.DrawDataCache.Add(value);
                }
            }
        }
    }
}
