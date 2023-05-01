using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;

namespace Avalon.UI;

internal class MinionSlotCounter : UIState
{
    private Asset<Texture2D> slotStart;
    private Asset<Texture2D> slotBottom;
    private Asset<Texture2D> slotTop;
    private Asset<Texture2D> slotBottomEnd;
    private Asset<Texture2D> slotTopEnd;
    private Texture2D filling;

    private Texture2D fillingClassic;


    private Asset<Texture2D> barOne;
    private Asset<Texture2D> barLeft;
    private Asset<Texture2D> barLeftEnd;
    private Texture2D fillingBar;
    private float textYOffset;
    private Vector2 labelDimensions;
    private string labelText = "Minion Slots";

    public MinionSlotCounter()
    {
        filling = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/MinionSlot/Filling", AssetRequestMode.ImmediateLoad).Value;
        slotStart = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/MinionSlot/SlotStart", AssetRequestMode.ImmediateLoad);
        slotBottom = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/MinionSlot/SlotBottom", AssetRequestMode.ImmediateLoad);
        slotTop = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/MinionSlot/SlotTop", AssetRequestMode.ImmediateLoad);
        slotBottomEnd = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/MinionSlot/SlotBottomEnd", AssetRequestMode.ImmediateLoad);
        slotTopEnd = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/MinionSlot/SlotTopEnd", AssetRequestMode.ImmediateLoad);
        fillingClassic = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/MinionSlot/FillingClassic", AssetRequestMode.ImmediateLoad).Value;

        barOne = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/MinionSlot/SlotBarOne", AssetRequestMode.ImmediateLoad);
        barLeft = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/MinionSlot/SlotBarLeft", AssetRequestMode.ImmediateLoad);
        barLeftEnd = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/MinionSlot/SlotBarLeftEnd", AssetRequestMode.ImmediateLoad);
        fillingBar = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/MinionSlot/FillingBar", AssetRequestMode.ImmediateLoad).Value;
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
        if (Main.ResourceSetsManager.ActiveSetKeyName == "Default")
        {
            var player = Main.LocalPlayer;
            int fillMod = -1;

            int ypos = 38;

            if (Main.playerInventory)
            {
                ypos = 94;
            }
            DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, FontAssets.MouseText.Value, labelText, new Vector2((Main.screenWidth - 320 - labelDimensions.X + 15), ypos - 25), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, default(Vector2), 0.7f, SpriteEffects.None, 0f);

            Vector2 fillingPos = new Vector2(Main.screenWidth - 320, ypos);
            for (int fillingLoop = 0; fillingLoop < player.numMinions; fillingLoop++)
            {

                if (fillingLoop > 0) fillingPos += new Vector2(-(fillingClassic.Width / 2), (fillingClassic.Height / 2) * fillMod);
                fillMod *= -1;
                var origin = new Vector2(fillingClassic.Width / 2);
                spriteBatch.Draw(fillingClassic, fillingPos, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);
            }
        }
        if (Main.ResourceSetsManager.ActiveSetKeyName == "HorizontalBars" ||
            Main.ResourceSetsManager.ActiveSetKeyName == "HorizontalBarsWithText" ||
            Main.ResourceSetsManager.ActiveSetKeyName == "HorizontalBarsWithFullText")
        {
            var player = Main.LocalPlayer;
            int ypos = (Main.ResourceSetsManager.ActiveSetKeyName == "HorizontalBarsWithText" || Main.ResourceSetsManager.ActiveSetKeyName == "HorizontalBarsWithFullText") ? 43 : 38;

            if (Main.playerInventory)
            {
                ypos = 94;
            }

            Vector2 backTexPos = new Vector2(Main.screenWidth - 320, ypos);
            Vector2 fillingPos = new Vector2(Main.screenWidth - 320, ypos);
            for (int backTexLoop = 1; backTexLoop <= player.maxMinions; backTexLoop++)
            {
                Texture2D backTex;
                if (backTexLoop > 1) backTexPos += new Vector2(-(fillingBar.Width - 2), 0);
                if (backTexLoop == 11) backTexPos += new Vector2((fillingBar.Width - 2) * 10, fillingBar.Height - 2);

                if (backTexLoop % 10 == 1)
                {
                    backTex = barOne.Value;
                }
                else if (backTexLoop % 10 == 0 || backTexLoop == player.maxMinions)
                {
                    backTex = barLeftEnd.Value;
                }
                else backTex = barLeft.Value;

                var origin = new Vector2(backTex.Width / 2);
                spriteBatch.Draw(backTex, backTexPos, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);
            }
            for (int fillingTexLoop = 0; fillingTexLoop < player.numMinions; fillingTexLoop++)
            {
                if (fillingTexLoop > 0) fillingPos += new Vector2(-(fillingBar.Width - 2), 0);
                if (fillingTexLoop == 10) fillingPos += new Vector2((fillingBar.Width - 2) * 10, fillingBar.Height - 2);
                var origin = new Vector2(fillingBar.Width / 2);
                spriteBatch.Draw(fillingBar, fillingPos, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);
            }
        }
        if (Main.ResourceSetsManager.ActiveSetKeyName == "New" ||
            Main.ResourceSetsManager.ActiveSetKeyName == "NewWithText")
        {
            CalculatedStyle dimensions = GetDimensions();
            var player = Main.LocalPlayer;

            int modifier = -1;
            int fillMod = -1;

            int xpos = Main.screenWidth - 320;
            int ypos = Main.ResourceSetsManager.ActiveSetKeyName == "NewWithText" ? 43 : 38;

            if (Main.playerInventory)
            {
                ypos = 94;
            }
            //Vector2 pos = new Vector2(dimensions.Width, dimensions.Y);
            Vector2 backTexPos = new Vector2(Main.screenWidth - 320, ypos);
            Vector2 fillingPos = new Vector2(Main.screenWidth - 320, ypos);
            for (int backTexLoop = 1; backTexLoop <= player.maxMinions; backTexLoop++)
            {
                Texture2D backTex;

                if (backTexLoop > 1) backTexPos += new Vector2(-(filling.Width / 2), (filling.Height / 2) * modifier);
                modifier *= -1;

                if (backTexLoop == 1)
                {
                    backTex = slotStart.Value;
                }
                else if (backTexLoop == player.maxMinions - 1)
                {
                    if (backTexLoop % 2 == 0)
                    {
                        backTex = slotBottomEnd.Value;
                    }
                    else
                    {
                        backTex = slotTopEnd.Value;
                    }
                }
                else if (backTexLoop % 2 == 0)
                {
                    backTex = slotBottom.Value;
                }
                else if (backTexLoop % 2 == 1)
                {
                    backTex = slotTop.Value;
                }

                else // never reaches this, but is required to allow the use of backTex variable lol
                {
                    backTex = slotStart.Value;
                }
                var origin = new Vector2(backTex.Width / 2);
                spriteBatch.Draw(backTex, backTexPos, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);
            }

            for (int fillingLoop = 0; fillingLoop < player.numMinions; fillingLoop++)
            {

                if (fillingLoop > 0) fillingPos += new Vector2(-(filling.Width / 2), (filling.Height / 2) * fillMod);
                fillMod *= -1;
                var origin = new Vector2(filling.Width / 2);
                spriteBatch.Draw(filling, fillingPos, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);
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
