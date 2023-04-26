using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Localization;

namespace Avalon.UI;

public class GroupOptionButton<T> : Terraria.GameContent.UI.Elements.GroupOptionButton<T> {
    public GroupOptionButton(
        T option,
        LocalizedText title,
        LocalizedText description,
        Color textColor,
        string iconTexturePath,
        float textSize = 1f,
        float titleAlignmentX = 0.5f,
        float titleWidthReduction = 10f) :
        base(option, title, description, textColor, iconTexturePath, textSize, titleAlignmentX, titleWidthReduction) {
    }

    public GroupOptionButton(
        T option,
        LocalizedText title,
        LocalizedText description,
        Color textColor,
        Asset<Texture2D> iconTexture,
        float textSize = 1f,
        float titleAlignmentX = 0.5f,
        float titleWidthReduction = 10f) :
        this(option, title, description, textColor, (string)null!, textSize, titleAlignmentX, titleWidthReduction) {
        typeof(Terraria.GameContent.UI.Elements.GroupOptionButton<T>)
            .GetField("_iconTexture", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(this, iconTexture);
    }
}
