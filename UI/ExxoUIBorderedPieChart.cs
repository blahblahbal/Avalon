//using Microsoft.Xna.Framework;
//using Terraria.UI;

//namespace Avalon.UI;

//public class ExxoUIBorderedPieChart : ExxoUIElement
//{
//    private readonly ExxoUICircle backingCircle;

//    private int borderWidth = 5;

//    public ExxoUIBorderedPieChart()
//    {
//        backingCircle = new ExxoUICircle();
//        backingCircle.Width.Set(0, 1);
//        backingCircle.Height.Set(0, 1);
//        Append(backingCircle);
//        PieChart = new ExxoUIPieChart();
//        PieChart.Width.Set(-(BorderWidth * 2), 1);
//        PieChart.Height.Set(-(BorderWidth * 2), 1);
//        PieChart.VAlign = UIAlign.Center;
//        PieChart.HAlign = UIAlign.Center;
//        backingCircle.Append(PieChart);
//    }

//    public override bool IsDynamicallySized => false;
//    public ExxoUIPieChart PieChart { get; }

//    public Color BorderColor
//    {
//        get => backingCircle.Color;
//        set => backingCircle.Color = value;
//    }

//    public int BorderWidth
//    {
//        get => borderWidth;
//        set
//        {
//            borderWidth = value;
//            PieChart.Width.Set(-(BorderWidth * 2), 1);
//            PieChart.Height.Set(-(BorderWidth * 2), 1);
//        }
//    }

//    public override bool ContainsPoint(Vector2 point) => backingCircle.ContainsPoint(point);
//}
