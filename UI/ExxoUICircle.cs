//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using ReLogic.Content;
//using Terraria;

//namespace ExxoAvalonOrigins.UI;

//public class ExxoUICircle : ExxoUIElement
//{
//    private static readonly Asset<Effect> CircleEffect =
//        ExxoAvalonOrigins.Mod.Assets.Request<Effect>("Effects/Circle", AssetRequestMode.ImmediateLoad);

//    public override bool IsDynamicallySized => false;
//    public Color Color { get; set; } = Color.White;

//    public override bool ContainsPoint(Vector2 point) =>
//        Vector2.Distance(GetInnerDimensions().Center(), point) <= GetInnerDimensions().Width / 2;

//    protected override void DrawSelf(SpriteBatch spriteBatch)
//    {
//        spriteBatch.End();
//        CircleEffect.Value.CurrentTechnique = CircleEffect.Value.Techniques["Default"];

//        using var whiteRectangle = new Texture2D(Main.spriteBatch.GraphicsDevice, 1, 1);
//        whiteRectangle.SetData(new[] { Color.White });

//        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp,
//            DepthStencilState.None, RasterizerState.CullNone, CircleEffect.Value, Main.UIScaleMatrix);

//        CircleEffect.Value.Parameters["Color"].SetValue(Color.ToVector4());

//        spriteBatch.Draw(
//            whiteRectangle,
//            GetInnerDimensions().ToRectangle(),
//            Color.White);
//        spriteBatch.End();
//        BeginDefaultSpriteBatch(spriteBatch);
//    }
//}
