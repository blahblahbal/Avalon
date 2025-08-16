using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Avalon.Reflection
{
	public static class BackgroundHelper
	{
		public static void Load()
		{
			addbgParallax = typeof(Main).GetField("bgParallax", BindingFlags.NonPublic | BindingFlags.Instance);
			addbgStartX = typeof(Main).GetField("bgStartX", BindingFlags.Instance | BindingFlags.NonPublic);
			addbgLoops = typeof(Main).GetField("bgLoops", BindingFlags.Instance | BindingFlags.NonPublic);
			addbgTopY = typeof(Main).GetField("bgTopY", BindingFlags.Instance | BindingFlags.NonPublic);
			addbgLoopsY = typeof(Main).GetField("bgLoopsY", BindingFlags.Instance | BindingFlags.NonPublic);
			addbgStartY = typeof(Main).GetField("bgStartY", BindingFlags.Instance | BindingFlags.NonPublic);
			addbgScale = typeof(Main).GetField("bgScale", BindingFlags.Static | BindingFlags.NonPublic);
			addscreenOff = typeof(Main).GetField("screenOff", BindingFlags.Instance | BindingFlags.NonPublic);
			addscAdj = typeof(Main).GetField("scAdj", BindingFlags.Instance | BindingFlags.NonPublic);
			addbgWidthScaled = typeof(Main).GetField("bgWidthScaled", BindingFlags.Static | BindingFlags.NonPublic);
			addColorOfSurfaceBackgroundModified = typeof(Main).GetField("ColorOfSurfaceBackgroundsModified", BindingFlags.Static | BindingFlags.NonPublic);
		}

		public static void Unload()
		{
			addbgParallax = null;
			addbgStartX = null;
			addbgLoops = null;
			addbgTopY = null;
			addbgLoopsY = null;
			addbgStartY = null;
			addbgScale = null;
			addscreenOff = null;
			addscAdj = null;
			addbgWidthScaled = null;
			addColorOfSurfaceBackgroundModified = null;
		}

		public static FieldInfo addbgParallax;
		public static FieldInfo addbgStartX;
		public static FieldInfo addbgLoops;
		public static FieldInfo addbgTopY;
		public static FieldInfo addbgLoopsY;
		public static FieldInfo addbgStartY;
		public static FieldInfo addbgScale;
		public static FieldInfo addscreenOff;
		public static FieldInfo addscAdj;
		public static FieldInfo addbgWidthScaled;
		public static FieldInfo addColorOfSurfaceBackgroundModified;

		public static double GetBackgroundParrallax(this Main self)
		{
			if (addbgParallax.GetValue(self) is double bgParallax)
			{
				return bgParallax;
			}
			return 0.0;
		}

		public static int GetBackgroundStartX(this Main self)
		{
			if (addbgStartX.GetValue(self) is int bgStartX)
			{
				return bgStartX;
			}
			return 0;
		}

		public static int GetBackgroundLoops(this Main self)
		{
			if (addbgLoops.GetValue(self) is int bgLoops)
			{
				return bgLoops;
			}
			return 0;
		}

		public static int GetBackgroundTopY(this Main self)
		{
			if (addbgTopY.GetValue(self) is int bgTopY)
			{
				return bgTopY;
			}
			return 0;
		}

		public static int GetBackgroundLoopsY(this Main self)
		{
			if (addbgLoopsY.GetValue(self) is int bgLoopsY)
			{
				return bgLoopsY;
			}
			return 0;
		}

		public static int GetBackgroundStartY(this Main self)
		{
			if (addbgStartY.GetValue(self) is int bgStartY)
			{
				return bgStartY;
			}
			return 0;
		}

		public static float GetBackGroundScale(this Main self)
		{
			return (float)addbgScale.GetValue(null);
		}

		public static float GetScreenOff(this Main self)
		{
			if (addscreenOff.GetValue(self) is float screenOff)
			{
				return screenOff;
			}
			return 0f;
		}

		public static float GetScAdj(this Main self)
		{
			if (addscAdj.GetValue(self) is float scAdj)
			{
				return scAdj;
			}
			return 0f;
		}

		public static int GetBackGroundWidthScaled(this Main self)
		{
			if (addbgWidthScaled.GetValue(self) is int bgWidthScaled)
			{
				return bgWidthScaled;
			}
			return 0;
		}

		public static Color GetColorOfSurfaceBackgroundsModified(this Main self)
		{
			if (addColorOfSurfaceBackgroundModified.GetValue(self) is Color ColorOfSurfaceBackgroundsModified)
			{
				return ColorOfSurfaceBackgroundsModified;
			}
			return Color.White;
		}

		public static void SetBackgroundParrallax(this Main self, double bgParallax)
		{
			if (addbgParallax.GetValue(self) is double)
			{
				addbgParallax.SetValue(self, bgParallax);
			}
		}

		public static void SetBackgroundStartX(this Main self, int bgStartX)
		{
			if (addbgStartX.GetValue(self) is int)
			{
				addbgStartX.SetValue(self, bgStartX);
			}
		}

		public static void SetBackgroundLoops(this Main self, int bgLoops)
		{
			if (addbgLoops.GetValue(self) is int)
			{
				addbgLoops.SetValue(self, bgLoops);
			}
		}

		public static void SetBackgroundTopY(this Main self, int bgTopY)
		{
			if (addbgTopY.GetValue(self) is int)
			{
				addbgTopY.SetValue(self, bgTopY);
			}
		}

		public static void SetBackgroundLoopsY(this Main self, int bgLoopsY)
		{
			if (addbgLoopsY.GetValue(self) is int)
			{
				addbgLoopsY.SetValue(self, bgLoopsY);
			}
		}

		public static void SetBackgroundStartY(this Main self, int bgStartY)
		{
			if (addbgStartY.GetValue(self) is int)
			{
				addbgStartY.SetValue(self, bgStartY);
			}
		}
	}
}
