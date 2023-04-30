using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;

namespace Avalon.UI;

internal class MinionSlotCounter : UIState
{
    private Asset<Texture2D> slotStart;
    private Asset<Texture2D> slotStartUncapped;
    private Asset<Texture2D> slotBottom;
    private Asset<Texture2D> slotTop;
    private Asset<Texture2D> slotBottomEnd;
    private Asset<Texture2D> slotTopEnd;
    private Texture2D filling;
    private Texture2D fillingClassic;
    private float textYOffset;
    private Vector2 labelDimensions;
    private string labelText = "Minion Slots";

    public MinionSlotCounter()
    {
        filling = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/MinionSlot/Filling", AssetRequestMode.ImmediateLoad).Value;
        slotStart = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/MinionSlot/SlotStart", AssetRequestMode.ImmediateLoad);
        slotStartUncapped = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/MinionSlot/SlotStartUncapped", AssetRequestMode.ImmediateLoad);
        slotBottom = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/MinionSlot/SlotBottom", AssetRequestMode.ImmediateLoad);
        slotTop = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/MinionSlot/SlotTop", AssetRequestMode.ImmediateLoad);
        slotBottomEnd = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/MinionSlot/SlotBottomEnd", AssetRequestMode.ImmediateLoad);
        slotTopEnd = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/MinionSlot/SlotTopEnd", AssetRequestMode.ImmediateLoad);
        fillingClassic = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/MinionSlot/FillingClassic", AssetRequestMode.ImmediateLoad).Value;
        textYOffset = 200;

        labelDimensions = FontAssets.MouseText.Value.MeasureString(labelText);

        Left.Set(Main.screenWidth - 500, 0);
        Top.Set(textYOffset + labelDimensions.Y, 0);
        Width.Set(ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/Stamina").Value.Width, 0);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        if (Main.player[Main.myPlayer].ghost)
        {
            return;
        }
        if (Main.ResourceSetsManager.ActiveSetKeyName == "Default" || Main.ResourceSetsManager.ActiveSetKeyName == "HorizontalBars")
        {
            var player = Main.LocalPlayer;
            int fillMod = -1;

            int ypos = 38;

            if (Main.playerInventory)
            {
                ypos = 94;
            }
            DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, FontAssets.MouseText.Value, labelText, new Vector2((Main.screenWidth - 320 - labelDimensions.X + 15), ypos - 15), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, default(Vector2), 0.7f, SpriteEffects.None, 0f);

            //Vector2 pos = new Vector2(dimensions.Width, dimensions.Y);
            Vector2 pos = new Vector2(Main.screenWidth - 320, ypos);
            for (int q = 1; q < player.numMinions; q++)
            {

                if (q > 1) pos += new Vector2(-(fillingClassic.Width / 2), (fillingClassic.Height / 2) * fillMod);
                fillMod *= -1;
                var origin = new Vector2(fillingClassic.Width / 2);
                spriteBatch.Draw(fillingClassic, pos, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);
            }
        }

        if (Main.ResourceSetsManager.ActiveSetKeyName == "New")
        {
            CalculatedStyle dimensions = GetDimensions();
            //DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, FontAssets.MouseText.Value, labelText, new Vector2((Main.screenWidth - 500 - labelDimensions.X + 15), textYOffset - 10), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, default(Vector2), 0.7f, SpriteEffects.None, 0f);

            var player = Main.LocalPlayer;

            int modifier = -1;
            int fillMod = -1;

            int xpos = Main.screenWidth - 320;
            int ypos = 38;

            if (Main.playerInventory)
            {
                ypos = 94;
            }

            //Vector2 pos = new Vector2(dimensions.Width, dimensions.Y);
            Vector2 pos = new Vector2(Main.screenWidth - 320, ypos);
            Vector2 pos2 = new Vector2(Main.screenWidth - 320, ypos);
            for (int q = 1; q <= player.maxMinions; q++)
            {
                Texture2D backTex;

                if (q > 1) pos += new Vector2(-(filling.Width / 2), (filling.Height / 2) * modifier);
                modifier *= -1;

                if (q == 1)
                {
                    backTex = slotStart.Value;
                }
                else if (q == player.maxMinions - 1)
                {
                    if (q % 2 == 0)
                    {
                        backTex = slotBottomEnd.Value;
                    }
                    else
                    {
                        backTex = slotTopEnd.Value;
                    }
                }
                else if (q % 2 == 0)
                {
                    backTex = slotBottom.Value;
                }
                else if (q % 2 == 1)
                {
                    backTex = slotTop.Value;
                }

                else // never reaches this, but is required to allow the use of backTex variable lol
                {
                    backTex = slotStart.Value;
                }
                var origin = new Vector2(backTex.Width / 2);
                spriteBatch.Draw(backTex, pos, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);
            }

            for (int q = 1; q < player.numMinions; q++)
            {

                if (q > 1) pos2 += new Vector2(-(filling.Width / 2), (filling.Height / 2) * fillMod);
                fillMod *= -1;
                var origin = new Vector2(filling.Width / 2);
                spriteBatch.Draw(filling, pos2, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);
            }

            if (IsMouseHovering)
            {
                string mouseText = string.Format("{0}/{1}", player.slotsMinions, player.maxMinions);
                Main.instance.MouseText(mouseText);
            }
        }
        base.DrawSelf(spriteBatch);
    }
}
