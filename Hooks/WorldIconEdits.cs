using Avalon.Common;
using Avalon.Reflection;
using Avalon.WorldGeneration.Enums;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using ReLogic.Content;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon.Hooks
{
	public class WorldIconEdits : ModHook
	{
		private static Dictionary<string, Asset<Texture2D>> WorldIconTextures = [];
		public override void Load()
		{
			if (Main.netMode == NetmodeID.Server)
			{
				return;
			}
			AddIconTextures("IconEverythingContagion");
			AddIconTextures("IconOverlayContagion");
			AddIconTextures("IconOverlayContagionRetro");
			AddIconTextures("IconOverlayContagionCaves");
			AddIconTextures("IconOverlayContagionFTW");
			AddIconTextures("IconOverlayContagionNotTheBees");
			AddIconTextures("IconOverlayContagionAnniversary");
			AddIconTextures("IconOverlayContagionDST");
			AddIconTextures("IconOverlayContagionRemix");
			AddIconTextures("IconOverlayContagionTraps");
			AddIconTextures("IconOverlaySavanna");
			AddIconTextures("IconOverlayJungle");
			AddIconTextures("IconOverlaySavannaCompletion");
			AddIconTextures("IconOverlayJungleCompletion");
			for (int i = 0; i < 2; i++)
			{
				string specialSeed = i switch
				{
					0 => "Caves",
					1 => "Retro",
					_ => throw new System.NotImplementedException()
				};
				AddIconTextures($"IconCorruption{specialSeed}");
				AddIconTextures($"IconCrimson{specialSeed}");
				AddIconTextures($"IconHallowCorruption{specialSeed}");
				AddIconTextures($"IconHallowCrimson{specialSeed}");
			}
		}

		private static void AddIconTextures(string name)
		{
			WorldIconTextures.Add(name, ModContent.Request<Texture2D>($"Avalon/Assets/Textures/UI/WorldCreation/{name}"));
		}

		private static Asset<Texture2D> GetAvalonSeedIcon(AWorldListItem self, string seed)
		{
			WorldFileData data = self.GetWorldListItemData();
			WorldIconTextures.TryGetValue("Icon" + (data.IsHardMode ? "Hallow" : "") + (data.HasCorruption ? "Corruption" : "Crimson") + seed, out Asset<Texture2D> icon);
			return icon;
		}

		private static Asset<Texture2D> GetAvalonOverlaySeedvariants(WorldFileData _data, string overlayName)
		{
			WorldIconTextures.TryGetValue("IconOverlay" +
				overlayName +
				(_data.ForTheWorthy ? "FTW" : "") +
				(_data.NotTheBees ? "NotTheBees" : "") +
				(_data.Anniversary ? "Anniversary" : "") +
				(_data.DontStarve ? "DST" : "") +
				(_data.RemixWorld ? "Remix" : "") +
				(_data.NoTrapsWorld ? "Traps" : ""),
				out Asset<Texture2D> icon);
			return icon;
		}

		private static Asset<Texture2D> GetJungleOverlay(byte jungleVariant, bool isCompletion)
		{
			WorldIconTextures.TryGetValue("IconOverlay" + (jungleVariant == (byte)WorldJungle.Tropics ? "Savanna" : "Jungle") + (isCompletion ? "Completion" : ""), out Asset<Texture2D> icon);
			return icon;
		}

		protected override void Apply()
		{
			On_AWorldListItem.GetIcon += ReplaceIcon;
			IL_UIWorldListItem.ctor += IconOverlays;
		}

		// Replaces textures
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

		// Draws the overlays
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
					WorldIconTextures.TryGetValue("IconEverythingContagion", out Asset<Texture2D> icon);
					UIImageFramed uIImageFramed = new(icon, icon.Frame(7, 16))
					{
						Left = new StyleDimension(0f, 0f)
					};
					uIImageFramed.OnUpdate += self.UpdateAvalonGlitchAnimation;
					_worldIcon.Append(uIImageFramed);
				}
				else if (_data.DrunkWorld)
				{
					WorldIconTextures.TryGetValue("IconOverlayContagion", out Asset<Texture2D> icon);
					UIImage element = new(icon)
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
							WorldIconTextures.TryGetValue("IconOverlayContagionRetro", out Asset<Texture2D> icon);
							UIImage element = new(icon)
							{
								Top = new StyleDimension(0f, 0f),
								Left = new StyleDimension(0f, 0f),
								IgnoresMouseInteraction = true
							};
							_worldIcon.Append(element);
						}
						else if (_avalonData.GetBool("Avalon:CavesSecretSeed"))
						{
							WorldIconTextures.TryGetValue("IconOverlayContagionCaves", out Asset<Texture2D> icon);
							UIImage element = new(icon)
							{
								Top = new StyleDimension(0f, 0f),
								Left = new StyleDimension(0f, 0f),
								IgnoresMouseInteraction = true
							};
							_worldIcon.Append(element);
						}
						else
						{
							UIImage element = new(GetAvalonOverlaySeedvariants(_data, "Contagion"))
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
						{                                            //if done, I (lion8cake) will make a glitch one for the depths too
							if (!_data.DefeatedMoonlord)
							{
								UIImage element = new(GetJungleOverlay(_avalonData.GetByte("Avalon:WorldJungle"), _data.DefeatedMoonlord))
								{
									Top = new StyleDimension(-4f, 0f),
									Left = new StyleDimension(0f, 0f),
									IgnoresMouseInteraction = true
								};
								_worldIcon.Append(element);
							}
							else
							{
								UIImage element = new(GetJungleOverlay(_avalonData.GetByte("Avalon:WorldJungle"), _data.DefeatedMoonlord))
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
								UIImage element = new(GetJungleOverlay(_avalonData.GetByte("Avalon:WorldJungle"), _data.DefeatedMoonlord))
								{
									Top = new StyleDimension(-4f, 0f),
									Left = new StyleDimension(0f, 0f),
									IgnoresMouseInteraction = true
								};
								_worldIcon.Append(element);
							}
							else
							{
								UIImage element = new(GetJungleOverlay(_avalonData.GetByte("Avalon:WorldJungle"), _data.DefeatedMoonlord))
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
	}
}
