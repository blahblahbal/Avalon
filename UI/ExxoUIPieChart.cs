//using System;
//using System.Collections.Generic;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using ReLogic.Content;
//using Terraria;
//using Terraria.UI;

//namespace ExxoAvalonOrigins.UI;

//public class ExxoUIPieChart : ExxoUIElement
//{
//    private const int MaxData = 16;

//    private static readonly Asset<Effect> PieChartEffect =
//        ExxoAvalonOrigins.Mod.Assets.Request<Effect>("Effects/PieChart", AssetRequestMode.ImmediateLoad);

//    private readonly Tuple<float, PieData>[] calculatedData = new Tuple<float, PieData>[MaxData];

//    private readonly List<PieData> pieDataList = new();
//    public override bool IsDynamicallySized => false;
//    public PieData? CurrentPieDataHovered { get; private set; }

//    private Vector4[] PieShaderData
//    {
//        get
//        {
//            var data = new Vector4[MaxData];
//            for (int i = 0; i < MaxData; i++)
//            {
//                data[i] = new Vector4(calculatedData[i].Item2.Color.ToVector3(), calculatedData[i].Item1);
//            }

//            return data;
//        }
//    }

//    public override bool ContainsPoint(Vector2 point) =>
//        Vector2.Distance(GetDimensions().Center(), point) <= GetDimensions().Width / 2;

//    public void RegisterData(PieData pieData) => pieDataList.Add(pieData);

//    protected override void DrawSelf(SpriteBatch spriteBatch)
//    {
//        spriteBatch.End();
//        PieChartEffect.Value.CurrentTechnique = PieChartEffect.Value.Techniques["Default"];

//        using var whiteRectangle = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
//        whiteRectangle.SetData(new[] { Color.White });

//        PieChartEffect.Value.Parameters["Thresholds"].SetValue(PieShaderData);

//        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp,
//            DepthStencilState.None,
//            RasterizerState.CullNone, PieChartEffect.Value, Main.UIScaleMatrix);

//        spriteBatch.Draw(
//            whiteRectangle,
//            GetDimensions().ToRectangle(),
//            Color.White);
//        spriteBatch.End();
//        BeginDefaultSpriteBatch(spriteBatch);
//    }

//    protected override void UpdateSelf(GameTime gameTime)
//    {
//        CalculateData();

//        if (IsMouseHovering)
//        {
//            Vector2 point =
//                (UserInterface.ActiveInstance.MousePosition - GetInnerDimensions().Center())
//                .SafeNormalize(Vector2.Zero);
//            double rotation = Math.Atan2(point.Y, point.X);

//            CurrentPieDataHovered = calculatedData[0].Item2;

//            for (int i = 0; i < MaxData - 1; ++i)
//            {
//                if (rotation > calculatedData[i].Item1)
//                {
//                    CurrentPieDataHovered = calculatedData[i + 1].Item2;
//                }
//            }

//            Tooltip = $"{CurrentPieDataHovered.Name} ({CurrentPieDataHovered.Percentage * 100:n2}%)";
//        }
//        else
//        {
//            Tooltip = string.Empty;
//        }
//    }

//    private void CalculateData()
//    {
//        pieDataList.Sort((a, b) => a.Percentage.CompareTo(b.Percentage));
//        pieDataList.Reverse();
//        int trackDataCount = pieDataList.Count < MaxData - 1 ? pieDataList.Count : MaxData - 1;

//        float percentIncrement = 0;

//        for (int i = 0; i < trackDataCount; i++)
//        {
//            calculatedData[i] =
//                new Tuple<float, PieData>(pieDataList[i].GetThreshold(ref percentIncrement), pieDataList[i]);
//        }

//        float percentRemaining = 1 - percentIncrement;
//        var otherData = new PieData("Other", Color.Gray, () => percentRemaining);

//        for (int i = trackDataCount; i < MaxData; i++)
//        {
//            calculatedData[i] =
//                new Tuple<float, PieData>(otherData.GetThreshold(ref percentIncrement), otherData);
//        }
//    }

//    public class PieData
//    {
//        private readonly Func<float> percentageProvider;

//        public PieData(string name, Color color, Func<float> percentageProvider)
//        {
//            Name = name;
//            Color = color;
//            this.percentageProvider = percentageProvider;
//        }

//        public float Percentage => percentageProvider.Invoke();
//        public string Name { get; }
//        public Color Color { get; }

//        public float GetThreshold(ref float count)
//        {
//            count += Percentage;
//            return (count * MathHelper.TwoPi) - MathHelper.Pi;
//        }
//    }
//}
