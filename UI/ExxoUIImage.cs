using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;

namespace Avalon.UI;

public class ExxoUIImage : ExxoUIElement
{
    private bool awaitingInternalUpdate;
    private Vector2 inset;
    private float scale = 1f;

    public ExxoUIImage(Asset<Texture2D>? texture = null)
    {
        OverrideSamplerState = SamplerState.PointClamp;
        SetImage(texture);
    }

    public override bool IsDynamicallySized => false;
    public float LocalRotation { get; set; }
    public float LocalScale { get; set; } = 1f;

    public Vector2 Inset
    {
        get => inset;
        set
        {
            inset = value;
            awaitingInternalUpdate = true;
            UpdateDimensions();
        }
    }

    public float Scale
    {
        get => scale;
        set
        {
            scale = value;
            awaitingInternalUpdate = true;
            UpdateDimensions();
        }
    }

    protected Color Color { get; set; } = Color.White;

    protected Asset<Texture2D>? Texture { get; private set; }

    public void SetImage(Asset<Texture2D>? texture)
    {
        texture?.VanillaLoad();
        Texture = texture;
        awaitingInternalUpdate = true;
        UpdateDimensions();
    }

    /// <inheritdoc />
    protected override void UpdateSelf(GameTime gameTime)
    {
        if (awaitingInternalUpdate)
        {
            UpdateDimensions();
        }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        if (Texture == null)
        {
            return;
        }

        spriteBatch.Draw(Texture.Value,
            (GetDimensions().Position() + (Texture.Size() * Scale / 2) - (Inset * Scale)).ToNearestPixel(), null,
            Color, LocalRotation, Texture.Size() / 2, Scale * LocalScale, SpriteEffects.None, 0f);
    }

    private void UpdateDimensions()
    {
        if (!Texture?.IsLoaded ?? true)
        {
            return;
        }

        MinWidth.Set((Texture.Width() - (Inset.X * 2)) * Scale, 0f);
        MinHeight.Set((Texture.Height() - (Inset.Y * 2)) * Scale, 0f);
        awaitingInternalUpdate = false;
    }
}
