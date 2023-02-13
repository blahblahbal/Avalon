using System;
using System.Collections.Generic;
using ExxoAvalonOrigins.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.PlayerDrawLayers;

public class LargeGemLayer : PlayerDrawLayer
{
    public override Position GetDefaultPosition()
    {
        return new AfterParent(Terraria.DataStructures.PlayerDrawLayers.CaptureTheGem);
    }
    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        if (drawInfo.shadow != 0f)
        {
            return;
        }
        Player drawPlayer = drawInfo.drawPlayer;
        bool[] ownedLargeGems = drawPlayer.GetModPlayer<AvalonPlayer>().OwnedLargeGems;
        if (ownedLargeGems.Length > 0)
        {
            bool flag2 = false;
            var value = default(DrawData);
            float numGems = 0f;
            for (int num23 = 0; num23 < 10; num23++)
            {
                if (ownedLargeGems[num23])
                {
                    numGems += 1f;
                }
            }
            float num25 = 1f - numGems * 0.06f;
            float num26 = (numGems - 1f) * 4f;
            switch ((int)numGems)
            {
                case 2:
                    num26 += 10f;
                    break;
                case 3:
                    num26 += 8f;
                    break;
                case 4:
                    num26 += 6f;
                    break;
                case 5:
                    num26 += 6f;
                    break;
                case 6:
                    num26 += 2f;
                    break;
                case 7:
                    num26 += 0f;
                    break;
                case 8:
                    num26 += 0f;
                    break;
                case 9:
                    num26 += 0f;
                    break;
                case 10:
                    num26 += 0f;
                    break;
            }
            float rotSpeed = drawPlayer.miscCounter / 300f * ((float)Math.PI * 2f);
            if (numGems > 0f)
            {
                float spacing = (float)Math.PI * 2f / numGems;
                float num29 = 0f;
                var ringSize = new Vector2(1.5f, 0.85f);
                var list = new List<DrawData>();
                for (int num30 = 0; num30 < 10; num30++)
                {
                    if (!ownedLargeGems[num30])
                    {
                        num29 += 1f;
                        continue;
                    }
                    Vector2 value14 = (rotSpeed + spacing * (num30 - num29)).ToRotationVector2();
                    float num31 = num25;
                    if (flag2)
                    {
                        num31 = MathHelper.Lerp(num25 * 0.7f, 1f, value14.Y / 2f + 0.5f);
                    }

                    Texture2D texture2D4 = null;
                    if (num30 < 7)
                    {
                        texture2D4 = TextureAssets.Gem[num30].Value;
                    }
                    else
                    {
                        switch (num30)
                        {
                            case 7:
                                texture2D4 = ModContent.GetModItem(ModContent.ItemType<Items.Other.LargeZircon>()).GetTexture().Value;
                                num31 *= 1.5f;
                                break;

                            case 8:
                                texture2D4 = ModContent.GetModItem(ModContent.ItemType<Items.Other.LargeTourmaline>()).GetTexture().Value;
                                num31 *= 1.5f;
                                break;

                            case 9:
                                texture2D4 = ModContent.GetModItem(ModContent.ItemType<Items.Other.LargePeridot>()).GetTexture().Value;
                                num31 *= 1.5f;
                                break;
                        }
                    }

                    value = new DrawData(texture2D4, new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X + drawPlayer.width / 2), (int)(drawInfo.Position.Y - Main.screenPosition.Y + drawPlayer.height - 80f)) + value14 * ringSize * num26, null, new Color(250, 250, 250, Main.mouseTextColor / 2), 0f, texture2D4.Size() / 2f, (Main.mouseTextColor / 1000f + 0.8f) * num31, SpriteEffects.None, 0);
                    list.Add(value);
                }
                if (flag2)
                {
                    list.Sort(DelegateMethods.CompareDrawSorterByYScale);
                }
                drawInfo.DrawDataCache.AddRange(list);
            }
        }
    }
}
