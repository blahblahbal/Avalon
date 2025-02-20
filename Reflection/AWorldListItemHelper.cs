using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.IO;
using Terraria.UI;

namespace Avalon.Reflection;

//for both AWorldListItem, UIWorldListItem and UIWorldLoad
public static class AWorldListItemHelper
{
	//saving the reflected variable means that preformance isnt impacted every time the game has to reflect
	public static void Load()
	{
		_addData = typeof(AWorldListItem).GetField("_data", BindingFlags.NonPublic | BindingFlags.Instance);
		_addWorldIcon = typeof(UIWorldListItem).GetField("_worldIcon", BindingFlags.NonPublic | BindingFlags.Instance);
		_addGlitchVariation = typeof(AWorldListItem).GetField("_glitchVariation", BindingFlags.Instance | BindingFlags.NonPublic);
		_addGlitchFrame = typeof(AWorldListItem).GetField("_glitchFrame", BindingFlags.Instance | BindingFlags.NonPublic);
		_addGlitchFrameCounter = typeof(AWorldListItem).GetField("_glitchFrameCounter", BindingFlags.Instance | BindingFlags.NonPublic);
		_addProgressMessage = typeof(UIWorldLoad).GetField("_progressMessage", BindingFlags.Instance | BindingFlags.NonPublic);
	}

	public static void Unload()
	{
		_addData = null;
		_addWorldIcon = null;
		_addGlitchVariation = null;
		_addGlitchFrame = null;
		_addGlitchFrameCounter = null;
		_addProgressMessage = null;
	}

	public static FieldInfo _addData;
	public static FieldInfo _addWorldIcon;
	public static FieldInfo _addGlitchVariation;
	public static FieldInfo _addGlitchFrame;
	public static FieldInfo _addGlitchFrameCounter;
	public static FieldInfo _addProgressMessage;
	internal static Dictionary<int, bool> _isZenithIconContagion = new Dictionary<int, bool>();

	public static WorldFileData GetWorldListItemData(this AWorldListItem self)
	{
		if (_addData.GetValue(self) is WorldFileData _data)
		{
			return _data;
		}
		return null;
	}

	public static UIElement GetWorldIcon(this UIWorldListItem self)
	{
		if (_addWorldIcon.GetValue(self) is UIElement _worldIcon)
		{
			return _worldIcon;
		}
		return null;
	}

	public static int GetGlitchVariation(this UIWorldListItem self)
	{
		if (_addGlitchVariation.GetValue(self) is int _glitchVariation)
		{
			return _glitchVariation;
		}
		return 0;
	}

	public static int GetGlitchFrame(this UIWorldListItem self)
	{
		if (_addGlitchFrame.GetValue(self) is int _glitchFrame)
		{
			return _glitchFrame;
		}
		return 0;
	}

	public static int GetGlitchFrameCounter(this UIWorldListItem self)
	{
		if (_addGlitchFrameCounter.GetValue(self) is int _glitchFrameCounter)
		{
			return _glitchFrameCounter;
		}
		return 0;
	}

	public static UIHeader GetProgressMessage(this UIWorldLoad self)
	{
		if (_addProgressMessage.GetValue(self) is UIHeader _progressMessage)
		{
			return _progressMessage;
		}
		return null;
	}

	internal static void UpdateAvalonGlitchAnimation(this UIWorldListItem self, UIElement affectedElement)
	{
		if (!_isZenithIconContagion.ContainsKey(self.GetWorldIcon().UniqueId))
		{
			_isZenithIconContagion.Add(self.GetWorldIcon().UniqueId, false);
		}
		if (self.GetGlitchFrame() == 0 && self.GetGlitchFrameCounter() == 0 && self.GetGlitchVariation() >= 3 && self.GetGlitchVariation() <= 4)
		{
			_isZenithIconContagion[self.GetWorldIcon().UniqueId] = Main.rand.NextBool(2);
		}
		int width = _isZenithIconContagion[self.GetWorldIcon().UniqueId] ? -255 : self.GetGlitchVariation();
		int height = _isZenithIconContagion[self.GetWorldIcon().UniqueId] ? -255 : self.GetGlitchFrame();
		(affectedElement as UIImageFramed).SetFrame(7, 16, width, height, 0, 0);
	}
}
