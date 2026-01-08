using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Terraria.GameContent.Drawing;

namespace Avalon.Reflection;
internal static class MassTileRenderingsILHook
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static List<ILHook> ILHook_TileRendering = null;

	public static event ILContext.Manipulator TileRenderingMethods
	{
		add
		{
			MethodInfo[] DrawingMethods = typeof(TileDrawing).GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
			ILHook_TileRendering = new List<ILHook>();
			for (int i = 0; i < DrawingMethods.Length; i++)
			{
				if (DrawingMethods[i].Name is nameof(TileDrawing.ToString) or nameof(TileDrawing.GetType) or nameof(TileDrawing.GetHashCode) or nameof(TileDrawing.Equals))
				{
					continue;
				}
				ILHook womp = new ILHook(DrawingMethods[i], value);
				if (womp != null)
				{
					womp.Apply();
					ILHook_TileRendering.Add(womp);
				}
			}
		}
		remove
		{
			if (ILHook_TileRendering != null && ILHook_TileRendering.Count > 0)
			{
				for (int i = 0; i < ILHook_TileRendering.Count; i++)
				{
					ILHook_TileRendering[i].Dispose();
				}
			}
		}
	}
}
