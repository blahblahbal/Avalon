using Avalon.Common;
using Avalon.Data.Sets;
using Avalon.Reflection;
using Avalon.WorldGeneration.Enums;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon.Hooks
{
	public class WorldIconEdits : ModHook
	{
		protected override void Apply()
		{
			On_AWorldListItem.GetIcon += ReplaceIcon;
			IL_UIWorldListItem.ctor += IconOverlays;
		}

		private void IconOverlays(ILContext il)
		{
			ILCursor c = new(il);
			c.GotoNext(MoveType.After, i => i.MatchLdfld<UIWorldListItem>("_worldIcon"), i => i.MatchCall<UIElement>("Append")); //tembling in my boots over the match call (monomod has an issue matching to calls)
			c.EmitLdarg0();
			c.EmitLdarg0();
			c.EmitLdfld(typeof(AWorldListItem).GetField("_data", BindingFlags.NonPublic | BindingFlags.Instance));
			c.EmitLdarg0();
			c.EmitLdfld(typeof(UIWorldListItem).GetField("_worldIcon", BindingFlags.NonPublic | BindingFlags.Instance));
			c.EmitDelegate((UIWorldListItem self, WorldFileData _data, UIElement _worldIcon) =>
			{
				//Underlay
				bool avalonData = _data.TryGetHeaderData(ModContent.GetInstance<AvalonWorld>(), out var _avalonData);
				_worldIcon.RemoveAllChildren();
				if (_data.DrunkWorld && _data.RemixWorld)
				{
					Asset<Texture2D> obj = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/WorldCreation/IconEverythingContagion", AssetRequestMode.ImmediateLoad);
					UIImageFramed uIImageFramed = new UIImageFramed(obj, obj.Frame(7, 16));
					uIImageFramed.Left = new StyleDimension(0f, 0f);
					uIImageFramed.OnUpdate += self.UpdateAvalonGlitchAnimation;
					_worldIcon.Append(uIImageFramed);
				}
				else if (_data.DrunkWorld)
				{
					UIImage element = new UIImage(ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/WorldCreation/IconOverlayContagion", AssetRequestMode.ImmediateLoad))
					{
						Top = new StyleDimension(0f, 0f),
						Left = new StyleDimension(0f, 0f),
						IgnoresMouseInteraction = true
					};
					_worldIcon.Append(element);
				}
				else if (avalonData)
				{
					if (_avalonData.GetByte("Avalon:WorldEvil") == (byte)WorldEvil.Contagion)
					{
						if (_avalonData.GetBool("Avalon:RetroSecretSeed"))
						{
							UIImage element = new UIImage(ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/WorldCreation/IconOverlayContagionRetro", AssetRequestMode.ImmediateLoad))
							{
								Top = new StyleDimension(0f, 0f),
								Left = new StyleDimension(0f, 0f),
								IgnoresMouseInteraction = true
							};
							_worldIcon.Append(element);
						}
						else if (_avalonData.GetBool("Avalon:CavesSecretSeed"))
						{
							UIImage element = new UIImage(ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/WorldCreation/IconOverlayContagionCaves", AssetRequestMode.ImmediateLoad))
							{
								Top = new StyleDimension(0f, 0f),
								Left = new StyleDimension(0f, 0f),
								IgnoresMouseInteraction = true
							};
							_worldIcon.Append(element);
						}
						else
						{
							UIImage element = new UIImage(GetAvalonOverlaySeedvariants(_data, "Contagion"))
							{
								Top = new StyleDimension(0f, 0f),
								Left = new StyleDimension(0f, 0f),
								IgnoresMouseInteraction = true
							};
							_worldIcon.Append(element);
						}
					}
					if (_avalonData.GetByte("Avalon:WorldJungle") <= (byte)WorldJungle.Tropics || _avalonData.GetByte("Avalon:WorldJungle") >= (byte)WorldJungle.Jungle)
					{
						if (!_data.NotTheBees && !_data.ZenithWorld) //I think a zenith version that glitches would be cool
						{											 //if done, I (lion8cake) will make a glitch one for the depths too
							if (!_data.DefeatedMoonlord)
							{
								UIImage element = new UIImage(GetJungleOverlay(_avalonData.GetByte("Avalon:WorldJungle"), _data.DefeatedMoonlord))
								{
									Top = new StyleDimension(-4f, 0f),
									Left = new StyleDimension(0f, 0f),
									IgnoresMouseInteraction = true
								};
								_worldIcon.Append(element);
							}
							else
							{
								UIImage element = new UIImage(GetJungleOverlay(_avalonData.GetByte("Avalon:WorldJungle"), _data.DefeatedMoonlord))
								{
									Top = new StyleDimension(-10f, 0f),
									Left = new StyleDimension(-3f, 0f),
									IgnoresMouseInteraction = true
								};
								//_worldIcon.RemoveChild(element);
								_worldIcon.Append(element);
							}
						}
					}
				}
			});
			c.GotoNext(MoveType.After, i => i.MatchLdcR4(4), i => i.MatchStloc0());
			c.EmitLdarg0();
			c.EmitLdfld(typeof(AWorldListItem).GetField("_data", BindingFlags.NonPublic | BindingFlags.Instance));
			c.EmitLdarg0();
			c.EmitLdfld(typeof(UIWorldListItem).GetField("_worldIcon", BindingFlags.NonPublic | BindingFlags.Instance));
			c.EmitDelegate((WorldFileData _data, UIElement _worldIcon) =>
			{
				//Overlay
				bool avalonData = _data.TryGetHeaderData(ModContent.GetInstance<AvalonWorld>(), out var _avalonData);
				if (avalonData)
				{
					if (_avalonData.GetByte("Avalon:WorldJungle") <= (byte)WorldJungle.Tropics || _avalonData.GetByte("Avalon:WorldJungle") >= (byte)WorldJungle.Jungle)
					{
						if (!_data.NotTheBees && !_data.ZenithWorld) //I think a zenith version that glitches would be cool
						{                                            //if done, I (lion8cake) will make a glitch one for the depths too
							if (!_data.DefeatedMoonlord)
							{
								UIImage element = new UIImage(GetJungleOverlay(_avalonData.GetByte("Avalon:WorldJungle"), _data.DefeatedMoonlord))
								{
									Top = new StyleDimension(-4f, 0f),
									Left = new StyleDimension(0f, 0f),
									IgnoresMouseInteraction = true
								};
								_worldIcon.Append(element);
							}
							else
							{
								UIImage element = new UIImage(GetJungleOverlay(_avalonData.GetByte("Avalon:WorldJungle"), _data.DefeatedMoonlord))
								{
									Top = new StyleDimension(-10f, 0f),
									Left = new StyleDimension(-3f, 0f),
									IgnoresMouseInteraction = true
								};
								//_worldIcon.RemoveChild(element);
								_worldIcon.Append(element);
							}
						}
					}
				}
			});
		}

		//Replaces textures
		private Asset<Texture2D> ReplaceIcon(On_AWorldListItem.orig_GetIcon orig, AWorldListItem self)
		{
			bool hasAvalonData = self.GetWorldListItemData().TryGetHeaderData(ModContent.GetInstance<AvalonWorld>(), out var _data);
			if (hasAvalonData)
			{
				if (_data.GetBool("Avalon:RetroSecretSeed"))
				{
					return GetAvalonSeedIcon(self, "Retro"); //forcibly changes the icon rather than being an overlay
				}
				else if (_data.GetBool("Avalon:CavesSecretSeed"))
				{
					return GetAvalonSeedIcon(self, "Caves");
				}
			}
			return orig.Invoke(self);
		}

		private Asset<Texture2D> GetAvalonSeedIcon(AWorldListItem self, string seed)
		{
			WorldFileData data = self.GetWorldListItemData();
			return ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/WorldCreation/Icon" + (data.IsHardMode ? "Hallow" : "") + (data.HasCorruption ? "Corruption" : "Crimson") + seed, AssetRequestMode.ImmediateLoad);
		}

		private Asset<Texture2D> GetAvalonOverlaySeedvariants(WorldFileData _data, string overlayName)
		{
			return ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/WorldCreation/IconOverlay" + overlayName + 
				(_data.ForTheWorthy ? "FTW" : "") + 
				(_data.NotTheBees ? "NotTheBees" : "") + 
				(_data.Anniversary ? "Anniversary" : "") + 
				(_data.DontStarve ? "DST" : "") +
				(_data.RemixWorld ? "Remix" : "") +
				(_data.NoTrapsWorld ? "Traps" : ""), AssetRequestMode.ImmediateLoad);
		}

		private Asset<Texture2D> GetJungleOverlay(byte jungleVariant, bool isCompletion)
		{
			return ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/WorldCreation/IconOverlay" + (jungleVariant == (byte)WorldJungle.Tropics ? "Savanna" : "Jungle") + (isCompletion ? "Completion" : ""), AssetRequestMode.ImmediateLoad);
		}
	}
}
