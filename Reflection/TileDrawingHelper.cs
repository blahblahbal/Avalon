using System;
using System.Reflection;
using Terraria.GameContent.Drawing;

namespace Avalon.Reflection;

public static class TileDrawingHelper
{
    private delegate void AddSpecialPointDelegate(TileDrawing self, int x, int y, object type);
    private static readonly AddSpecialPointDelegate CachedAddSpecialPoint =
        Utilities.CacheInstanceMethod<AddSpecialPointDelegate>(typeof(TileDrawing).GetMethod("AddSpecialPoint", BindingFlags.Instance | BindingFlags.NonPublic)!);
    private static readonly Type TileCounterTypeType = typeof(TileDrawing).GetNestedType("TileCounterType", BindingFlags.NonPublic)!;
    public static void AddSpecialPoint(this TileDrawing tileDrawing, int x, int y, int type) => CachedAddSpecialPoint(tileDrawing, x, y, Enum.ToObject(TileCounterTypeType, type));

    private delegate void CrawlToTopOfVineAndAddSpecialPointDelegate(TileDrawing self, int j, int i);
    private static readonly CrawlToTopOfVineAndAddSpecialPointDelegate CachedCrawlToTopOfVineAndAddSpecialPoint =
        Utilities.CacheInstanceMethod<CrawlToTopOfVineAndAddSpecialPointDelegate>(typeof(TileDrawing).GetMethod("CrawlToTopOfVineAndAddSpecialPoint", BindingFlags.Instance | BindingFlags.NonPublic)!);
    public static void CrawlToTopOfVineAndAddSpecialPoint(this TileDrawing tileDrawing, int j, int i) => CachedCrawlToTopOfVineAndAddSpecialPoint(tileDrawing, j, i);
}
