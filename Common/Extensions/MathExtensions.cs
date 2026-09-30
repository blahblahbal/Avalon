using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Avalon.Common.Extensions;

public static class MathExtensions
{
	extension(Color color)
	{
		public static Color operator *(Color c1, Color c2)
		{
			return c1.MultiplyRGBA(c2);
		}
	}
}
