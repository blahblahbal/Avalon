using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.ResourceSets;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon.UI;

public struct PlayerStaminaStatsSnapshot
{
    public int Stamina;
    public int StaminaMax;
    public int StaminaMax2;
    public float StaminaPerSegment;
    public int StaminaCount;
    public int StaminaCountMT300;
    public int StaminaCountMT450;

    public PlayerStaminaStatsSnapshot(Player p)
    {
        Stamina = p.GetModPlayer<AvalonStaminaPlayer>().StatStam;
        StaminaMax = p.GetModPlayer<AvalonStaminaPlayer>().StatStamMax;
        StaminaMax2 = p.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2;
        float stamPerBolt = 30;
        int stamOver30 = StaminaMax2 / 30;
        int stamBarsMoreThan5 = (StaminaMax2 - 150) / 30;
        if (stamBarsMoreThan5 < 0)
        {
            stamBarsMoreThan5 = 0;
        }
        if (stamBarsMoreThan5 > 0)
        {
            stamOver30 = (StaminaMax2 - 150) / 30;
            stamPerBolt = StaminaMax2 / 5 - 30;
        }

        int stamBarsMoreThan10 = (StaminaMax2 - 300) / 30;
        if (stamBarsMoreThan10 < 0)
        {
            stamBarsMoreThan10 = 0;
        }
        if (stamBarsMoreThan10 > 0)
        {
            stamOver30 = (StaminaMax2 - 300) / 30;
            stamPerBolt = StaminaMax2 / 5 - 30;
        }

        int stamBarsMoreThan15 = (StaminaMax2 - 450) / 30;
        if (stamBarsMoreThan15 < 0)
        {
            stamBarsMoreThan15 = 0;
        }
        if (stamBarsMoreThan15 > 0)
        {
            stamOver30 = (StaminaMax2 - 450) / 30;
            stamPerBolt = StaminaMax2 / 5 - 30;
        }
        int num4 = StaminaMax2 - 150;
        if (StaminaMax2 < 150)
        {
            num4 = 0;
        }
        if (stamBarsMoreThan10 > 0)
        {
            num4 = StaminaMax2 - 300;
        }
        if (stamBarsMoreThan15 > 0)
        {
            num4 = StaminaMax2 - 450;
        }
        stamPerBolt += num4 / stamOver30;
        StaminaCount = stamBarsMoreThan5;
        StaminaCountMT300 = stamBarsMoreThan10;
        StaminaCountMT450 = stamBarsMoreThan15;
        StaminaPerSegment = stamPerBolt;
    }
}

class StaminaBar : UIState
{
    private const int staminaPerBar = 30;
    private const int maxStaminaBars = 5;
    private const int barSpacing = 26;
    private const string labelText = "Stamina";
    private float textYOffset;
    private Vector2 labelDimensions;
    private Texture2D staminaTexture1;
    private Texture2D staminaTexture2;
    private Texture2D staminaTexture3;
    private Texture2D staminaTexture4;


    // bar style fields
    private int stamSegmentsBarsCount;
    private int stamSegmentsBarsCount2;
    private int stamSegmentsBarsCount3;
    private int stamSegmentsBarsCount4;
    private float stamPercent;
    private bool stamHovered;
    private int maxSegmentCount;
    private Asset<Texture2D> stamFillGreen;
    private Asset<Texture2D> stamFillPurple;
    private Asset<Texture2D> stamFillOrange;
    private Asset<Texture2D> stamFillPink;

    private Asset<Texture2D> panelLeft;
    private Asset<Texture2D> panelMiddleStam;
    private Asset<Texture2D> panelRightStam;
    // end bar style fields

    // fancy style fields
    private bool fancyStamHovered;
    private int fancyStamCount;
    private Asset<Texture2D> staminaTop;
    private Asset<Texture2D> staminaBottom;
    private Asset<Texture2D> staminaMiddle;
    private Asset<Texture2D> staminaSingle;
    private Asset<Texture2D> staminaFillGreenFancy;
    private Asset<Texture2D> staminaFillPurpleFancy;
    private Asset<Texture2D> staminaFillOrangeFancy;
    private Asset<Texture2D> staminaFillPinkFancy;
    private int lastStaminaFillingIndex;
    private float currentPlayerStamina;
    private float staminaPerBolt;
    private int playerStamCountOver150;
    private int playerStamCountOver300;
    private int playerStamCountOver450;
    // end fancy style fields

    public StaminaBar()
    {
        // FOR SOME REASON THIS DOES NOT LOAD THE TEXTURES
        staminaTexture1 = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/Stamina", AssetRequestMode.ImmediateLoad).Value;
        staminaTexture2 = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/Stamina2", AssetRequestMode.ImmediateLoad).Value;
        staminaTexture3 = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/Stamina3", AssetRequestMode.ImmediateLoad).Value;
        staminaTexture4 = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/Stamina4", AssetRequestMode.ImmediateLoad).Value;

        int manaStarSpacing = 28;
        textYOffset = manaStarSpacing * 11 + 30;

        labelDimensions = FontAssets.MouseText.Value.MeasureString(labelText);

        Top.Set(textYOffset + labelDimensions.Y, 0);
        Width.Set(ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/Stamina").Value.Width, 0);
    }

    #region bar style
    private void PrepareFieldsBars()
    {
        PlayerStaminaStatsSnapshot snap = new PlayerStaminaStatsSnapshot(Main.LocalPlayer);
        AvalonStaminaPlayer p = Main.LocalPlayer.GetModPlayer<AvalonStaminaPlayer>();
        staminaPerBolt = snap.StaminaPerSegment;
        stamSegmentsBarsCount = (int)(snap.StaminaMax2 / staminaPerBolt);
        if (stamSegmentsBarsCount > maxStaminaBars)
        {
            stamSegmentsBarsCount = maxStaminaBars;
        }
        maxSegmentCount = 5;
        stamSegmentsBarsCount2 = snap.StaminaCount;
        stamSegmentsBarsCount3 = snap.StaminaCountMT300;
        stamSegmentsBarsCount4 = snap.StaminaCountMT450;

        stamPercent = ((float)p.StatStam / p.StatStamMax2);
    }
    private void StaminaPanelDrawerBars(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
    {
        sourceRect = null;
        offset = Vector2.Zero;
        sprite = panelLeft;
        drawScale = 1f;
        if (elementIndex == lastElementIndex)
        {
            sprite = panelRightStam;
            offset = new Vector2(-19f, -6f);
        }
        else if (elementIndex != firstElementIndex)
        {
            sprite = panelMiddleStam;
        }
    }
    private void StaminaFillingDrawerBars(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
    {
        sprite = stamFillGreen;
        if (elementIndex >= stamSegmentsBarsCount - stamSegmentsBarsCount2)
        {
            sprite = stamFillPurple;
        }
        if (elementIndex >= stamSegmentsBarsCount - stamSegmentsBarsCount3)
        {
            sprite = stamFillOrange;
        }
        if (elementIndex >= stamSegmentsBarsCount - stamSegmentsBarsCount4)
        {
            sprite = stamFillPink;
        }
        FillBarByValues(elementIndex, sprite, stamSegmentsBarsCount, stamPercent, out offset, out drawScale, out sourceRect);
    }
    private static void FillBarByValues(int elementIndex, Asset<Texture2D> sprite, int segmentsCount, float fillPercent, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
    {
        sourceRect = null;
        offset = Vector2.Zero;
        float num = 1f;
        float num2 = 1f / segmentsCount;
        float t = 1f - fillPercent;
        float lerpValue = Utils.GetLerpValue(num2 * elementIndex, num2 * (elementIndex + 1), t, clamped: true);
        num = 1f - lerpValue;
        drawScale = 1f;
        Rectangle value = sprite.Frame();
        int num3 = (int)(value.Width * (1f - num));
        offset.X += num3;
        value.X += num3;
        value.Width -= num3;
        sourceRect = value;
    }
    #endregion bar style

    #region fancy style
    private void PrepareFieldsFancy()
    {
        PlayerStaminaStatsSnapshot snapshot = new PlayerStaminaStatsSnapshot(Main.LocalPlayer);
        playerStamCountOver150 = snapshot.StaminaCount;
        playerStamCountOver300 = snapshot.StaminaCountMT300;
        playerStamCountOver450 = snapshot.StaminaCountMT450;
        currentPlayerStamina = snapshot.Stamina;
        staminaPerBolt = snapshot.StaminaPerSegment;
        fancyStamCount = (int)(snapshot.StaminaMax2 / staminaPerBolt);
        if (fancyStamCount > maxStaminaBars)
        {
            fancyStamCount = maxStaminaBars;
        }
        lastStaminaFillingIndex = (int)(currentPlayerStamina / staminaPerBolt);
    }
    private void DrawStaminaBarFancy(SpriteBatch spriteBatch)
    {
        Vector2 vector = new Vector2(Main.screenWidth - 40, 28 * 9 + 10);
        _ = fancyStamCount;
        bool isHovered = false;
        ResourceDrawSettings resourceDrawSettings = default;
        resourceDrawSettings.ElementCount = fancyStamCount;
        resourceDrawSettings.ElementIndexOffset = 0;
        resourceDrawSettings.TopLeftAnchor = vector;
        resourceDrawSettings.GetTextureMethod = StaminaPanelDrawerFancy;
        resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
        resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitY;
        resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
        resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
        resourceDrawSettings.Draw(spriteBatch, ref isHovered);
        resourceDrawSettings = default;
        resourceDrawSettings.ElementCount = fancyStamCount;
        resourceDrawSettings.ElementIndexOffset = 0;
        resourceDrawSettings.TopLeftAnchor = vector + new Vector2(15f, 16f);
        resourceDrawSettings.GetTextureMethod = StaminaFillingDrawerFancy;
        resourceDrawSettings.OffsetPerDraw = Vector2.UnitY * 2f;
        resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitY;
        resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
        resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = new Vector2(0.5f, 0.5f);
        resourceDrawSettings.Draw(spriteBatch, ref isHovered);
        fancyStamHovered = isHovered;
        if (fancyStamHovered)
        {
            string mouseText = string.Format("{0}/{1}", Main.LocalPlayer.GetModPlayer<AvalonStaminaPlayer>().StatStam, Main.LocalPlayer.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2);
            Main.instance.MouseText(mouseText);
        }
    }
    private void StaminaFillingDrawerFancy(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
    {
        sourceRect = null;
        offset = new Vector2(-2, -2);
        if (elementIndex == firstElementIndex)
        {
            offset = new(-2f, 0f);
        }
        //sprite = staminaFillGreenFancy;
        //sprite = staminaBottom;
        sprite = staminaFillGreenFancy;
        if (elementIndex < playerStamCountOver450)
        {
            sprite = staminaFillPinkFancy;
        }
        else if (elementIndex < playerStamCountOver300)
        {
            sprite = staminaFillOrangeFancy;
        }
        else if (elementIndex < playerStamCountOver150)
        {
            sprite = staminaFillPurpleFancy;
        }

        float num = (drawScale = Utils.GetLerpValue(staminaPerBolt * elementIndex, staminaPerBolt * (elementIndex + 1), currentPlayerStamina, clamped: true));
        if (elementIndex == lastStaminaFillingIndex && num > 0f)
        {
            drawScale += Main.cursorScale - 1f;
        }
    }
    private void StaminaPanelDrawerFancy(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
    {
        sourceRect = null;
        offset = Vector2.Zero;
        sprite = staminaTop;
        drawScale = 1f;
        if (elementIndex == lastElementIndex && elementIndex == firstElementIndex)
        {
            sprite = staminaSingle;
        }
        else if (elementIndex == lastElementIndex)
        {
            sprite = staminaBottom;
            offset = new Vector2(0f, 0f);
        }
        else if (elementIndex != firstElementIndex)
        {
            sprite = staminaMiddle;
        }
    }
    #endregion fancy style
    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        if (Main.player[Main.myPlayer].ghost)
        {
            return;
        }
        if (Main.ResourceSetsManager.ActiveSetKeyName == "HorizontalBars" ||
            Main.ResourceSetsManager.ActiveSetKeyName == "HorizontalBarsWithText" ||
            Main.ResourceSetsManager.ActiveSetKeyName == "HorizontalBarsWithFullText")
        {
            panelLeft = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/Panel_Left");
            stamFillGreen = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/StaminaFill_Green");
            stamFillPurple = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/StaminaFill_Purple");
            stamFillOrange = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/StaminaFill_Orange");
            stamFillPink = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/StaminaFill_Pink");
            panelMiddleStam = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/StaminaPanel_Middle");
            panelRightStam = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/StaminaPanel_Right");

            PrepareFieldsBars();
            SpriteBatch sb = Main.spriteBatch;
            int xpos = 16;
            int ypos = 66;
            int finalXPos = Main.screenWidth - 135 - 22 + xpos;
            Vector2 vector = new Vector2(finalXPos, ypos);
            vector.X += (maxSegmentCount - stamSegmentsBarsCount) * panelMiddleStam.Value.Width;
            bool isHovered = false;

            ResourceDrawSettings resourceDrawSettings = default;
            resourceDrawSettings.ElementCount = stamSegmentsBarsCount + 2;
            resourceDrawSettings.ElementIndexOffset = 0;
            resourceDrawSettings.TopLeftAnchor = vector;
            resourceDrawSettings.GetTextureMethod = StaminaPanelDrawerBars;
            resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
            resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
            resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
            resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
            resourceDrawSettings.Draw(spriteBatch, ref isHovered);
            resourceDrawSettings = default;
            resourceDrawSettings.ElementCount = stamSegmentsBarsCount;
            resourceDrawSettings.ElementIndexOffset = 0;
            resourceDrawSettings.TopLeftAnchor = vector + new Vector2(6f, 6f);
            resourceDrawSettings.GetTextureMethod = StaminaFillingDrawerBars;
            resourceDrawSettings.OffsetPerDraw = new Vector2(stamFillGreen.Width(), 0f);
            resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.Zero;
            resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
            resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
            resourceDrawSettings.Draw(spriteBatch, ref isHovered);
            stamHovered = isHovered;

            if (stamHovered)
            {
                string mouseText = string.Format("{0}/{1}", Main.LocalPlayer.GetModPlayer<AvalonStaminaPlayer>().StatStam, Main.LocalPlayer.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2);
                Main.instance.MouseText(mouseText);
            }

            Left.Set(vector.X, 0);
            Height.Set(panelMiddleStam.Value.Height, 0);
            Width.Set(26 + stamFillGreen.Value.Width * stamSegmentsBarsCount + 6, 0);
        }
        else if (Main.ResourceSetsManager.ActiveSetKeyName == "New" ||
            Main.ResourceSetsManager.ActiveSetKeyName == "NewWithText")
        {
            staminaTop = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/StaminaFancy_Top");
            staminaMiddle = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/StaminaFancy_Middle");
            staminaBottom = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/StaminaFancy_Bottom");
            staminaSingle = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/StaminaFancy_Single");
            staminaFillGreenFancy = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/StaminaFancy_FillGreen");
            staminaFillPurpleFancy = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/StaminaFancy_FillPurple");
            staminaFillOrangeFancy = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/StaminaFancy_FillOrange");
            staminaFillPinkFancy = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/StaminaFancy_FillPink");
            PrepareFieldsFancy();
            DrawStaminaBarFancy(spriteBatch);

            Left.Set(Main.screenWidth - 25 - (ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/Stamina").Value.Width / 2f), 0);
            Height.Set(fancyStamCount * staminaTop.Value.Height, 0);
            Width.Set(ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/Stamina").Value.Width, 0);
        }
        else if (Main.ResourceSetsManager.ActiveSetKeyName == "Default")
        {
            CalculatedStyle dimensions = GetDimensions();
            // Draw labelText above stamina bar
            DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, FontAssets.MouseText.Value, labelText, new Vector2((Main.screenWidth - labelDimensions.X + 15), textYOffset), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, default(Vector2), 0.7f, SpriteEffects.None, 0f);

            var player = Main.LocalPlayer.GetModPlayer<AvalonStaminaPlayer>();

            int stamBars = player.StatStamMax2 / staminaPerBar;
            if (stamBars > maxStaminaBars)
            {
                stamBars = maxStaminaBars;
            }

            int staminaThreshold = staminaPerBar * maxStaminaBars;
            int amountBars = player.StatStamMax2 / staminaPerBar;
            int amountHighestTierBars = ((amountBars - 1) % maxStaminaBars) + 1;
            int highestStatLevel = ((player.StatStamMax2 - 1) / staminaThreshold) + 1;

            int staminaCounter = 0;
            bool activeFound = false;

            for (var i = 1; i < stamBars + 1; i++)
            {
                int intensity;
                float scale = 1f;
                bool activeBar = false;

                int statLevel = highestStatLevel;
                if (i > amountHighestTierBars)
                {
                    statLevel--;
                }

                if (!activeFound && staminaCounter + statLevel * staminaPerBar >= player.StatStam)
                {
                    float barProgress = (player.StatStam - staminaCounter) / (float)(statLevel * staminaPerBar);
                    intensity = (int)(30 + 225f * barProgress);
                    if (intensity < 30)
                    {
                        intensity = 30;
                    }
                    scale = barProgress / 4f + 0.75f;
                    if (scale < 0.75)
                    {
                        scale = 0.75f;
                    }

                    activeBar = true;
                    activeFound = true;
                }
                else if (!activeFound)
                {
                    intensity = 255;
                }
                else
                {
                    intensity = 30;
                    scale = 0.75f;
                }

                staminaCounter += statLevel * staminaPerBar;

                // Bobs the scale of the active bar with the cursor bobbing
                if (activeBar)
                {
                    scale += Main.cursorScale - 1f;
                }

                Texture2D texture;
                switch (statLevel)
                {
                    case 1:
                        texture = staminaTexture1;
                        break;
                    case 2:
                        texture = staminaTexture2;
                        break;
                    case 3:
                        texture = staminaTexture3;
                        break;
                    case 4:
                        texture = staminaTexture4;
                        break;

                    default:
                        texture = staminaTexture1;
                        break;
                }

                int alpha = (int)(intensity * 0.9f);
                int scaleOffsetX = (int)((texture.Width - texture.Width * scale) / 2f);
                var origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
                var position = new Vector2(dimensions.X + scaleOffsetX + (texture.Width / 2f), dimensions.Y + (barSpacing * (i - 1)) + (texture.Height / 2f));

                spriteBatch.Draw(texture, position, null, new Color(intensity, intensity, intensity, alpha), 0f, origin, scale, SpriteEffects.None, 0f);
            }

            if (IsMouseHovering)
            {
                string mouseText = string.Format("{0}/{1}", player.StatStam, player.StatStamMax2);
                Main.instance.MouseText(mouseText);
            }

            Left.Set(Main.screenWidth - 25 - (ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/Stamina").Value.Width / 2f), 0);
            Height.Set(barSpacing * stamBars, 0);
            Width.Set(ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/Stamina").Value.Width, 0);
        }
        base.DrawSelf(spriteBatch);
    }
}
