using Avalon.Common;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Graphics.Capture;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Avalon.Backgrounds;
using Avalon.Waters;
using System.Reflection;
using static Terraria.Graphics.Capture.CaptureInterface;

namespace Avalon.Hooks
{
	public class AddModdedCaptureBiomes : ModHook
	{
		protected override void Apply()
		{
			IL_CaptureInterface.ModeChangeSettings.DrawWaterChoices += DrawModdedCaptureIcons;
			On_CaptureBiome.GetCaptureBiome += makeCaptureBiomeSlot;
			On_CaptureInterface.ModeChangeSettings.GetRect += increaseCaptureSettingsHeight;
			IL_CaptureInterface.ModeChangeSettings.Draw += moveCaptureDefaultsText;
			IL_CaptureInterface.ModeChangeSettings.Update += moveCaptureDefaultsHitbox;
		}

		internal static int biomeCaptureCount = 1; //Contagion only

		internal static int[] biomeCapturesIndexs = new int[biomeCaptureCount];

		/// <summary>
		/// Sets the Icon texture, the texture's frame's X position, and the icon text. Make sure you check if the captureIconID is yours before editing the icon/frame X
		/// </summary>
		/// <param name="captureIconID"></param>
		/// <param name="iconHitbox"></param>
		/// <param name="mouse"></param>
		/// <param name="iconTexture"></param>
		/// <param name="textureXposition"></param>
		internal void CaptureIconSetInfo(int captureIconID, Rectangle iconHitbox, Point mouse, ref Texture2D iconTexture, ref int textureXposition)
		{
			if (captureIconID == biomeCapturesIndexs[0]) //if icon is our 1st icon
			{
				iconTexture = (Texture2D)ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>("Assets/Textures/UI/ContagionCaptureBiomeIcon"); //edit texture to be ours
				textureXposition = 0; //frame X for our texture
				if (iconHitbox.Contains(mouse))
				{
					Main.instance.MouseText(Language.GetTextValue("Mods.Avalon.CaptureBiomeChoice.Contagion"), 0, 0); //text for our icon
				}
			}
		}

		/// <summary>
		/// returns the capture biome of the current icon loaded. Use chosenbiome to get the icon ID of the icon selected. Return null to set to the default capture biome.
		/// </summary>
		/// <param name="chosenBiome"></param>
		/// <returns></returns>
		internal CaptureBiome? CaptureIconBiomeSettings(int chosenBiome)
		{
			if (chosenBiome == biomeCapturesIndexs[0]) //biomeChoice is bacially the icon ID
			{
				return new CaptureBiome(ModContent.GetInstance<ContagionSurfaceBackground>().Slot, ModContent.GetInstance<ContagionWaterStyle>().Slot); //create a new capture biome
			}
			return null;
		}

		private static int maxModdedCaptureCounts = 0;

		private void moveCaptureDefaultsHitbox(ILContext il)
		{
			//offsets the hitbox of the reset to defaults button
			ILCursor c = new(il);
			c.GotoNext(MoveType.After, i => i.MatchMul(), i => i.MatchAdd(), i => i.MatchStfld<Rectangle>("Y"));
			c.EmitLdloc(3); //i
			c.EmitLdloca(1); //ref rect
			c.EmitLdloc(2);
			c.EmitDelegate((int i, ref Rectangle rectangle, int y) =>
			{
				int y2 = y + i * 20;
				if (i == 6)
				{
					rectangle.Y = y2 + 24 * ((int)Math.Ceiling(((double)maxModdedCaptureCounts + 13.00) / 7.00) - 2);
				}
			});
		}

		private void moveCaptureDefaultsText(ILContext il)
		{
			ILCursor c = new(il);
			c.GotoNext(MoveType.After, i => i.MatchCall<Color>("get_White"), i => i.MatchStloc(4)); //offsets the visual placement of the reset to defaults button
			c.EmitLdloc(1); //i
			c.EmitLdloca(0); //ref rect
			c.EmitLdarg(0);
			c.EmitDelegate((int i, ref Rectangle rect, object self) =>
			{
				Rectangle rect2 = GetRect();
				if (i == 6)
				{
					rect.Y = rect2.Y + 24 * ((int)Math.Ceiling(((double)maxModdedCaptureCounts + 13.00) / 7.00) - 2);
				}
			});
			//in code, the icons for the biome settings is actually placed AFTER the reset to defaults button but is visually displaced to be above
			//so here we have to reset back to the original Y so the icons aren't offset
			//c.GotoNext(MoveType.After, i => i.MatchCall("Terraria.UI.Chat.ChatManager", "DrawColorCodedStringWithShadow"), i => i.MatchPop()); //due to an issue with monomod, we are unable to match to these instructions
			c.GotoNext(MoveType.Before, i => i.MatchLdloc1(), i => i.MatchLdcI4(1), i => i.MatchAdd(), i => i.MatchStloc(1));
			c.EmitLdloc(1);
			c.EmitLdloca(0);
			c.EmitLdarg(0);
			c.EmitDelegate((int i, ref Rectangle rect, object self) =>
			{
				Rectangle rect2 = GetRect();
				if (i == 6)
				{
					rect.Y = rect2.Y;
				}
			});
		}

		private Rectangle GetRect()
		{
			Rectangle result = new(0, 0, 224, 170);
			if (Settings.ScreenAnchor == 0)
			{
				result.X = 227 - result.Width / 2;
				result.Y = 80;
			}
			if (maxModdedCaptureCounts > 0)
			{
				result.Height = 170 + 24 * ((int)Math.Ceiling(((double)maxModdedCaptureCounts + 13.00) / 7.00) - 2); //offset the back panel
			}
			return result;
		}

		private Rectangle increaseCaptureSettingsHeight(On_CaptureInterface.ModeChangeSettings.orig_GetRect orig, object self)
		{
			Rectangle rect = orig.Invoke(self);
			if (maxModdedCaptureCounts > 0)
			{
				rect.Height = 170 + 24 * ((int)Math.Ceiling(((double)maxModdedCaptureCounts + 13.00) / 7.00) - 2); //offset the back panel
			}
			return rect;
		}

		private CaptureBiome makeCaptureBiomeSlot(On_CaptureBiome.orig_GetCaptureBiome orig, int biomeChoice)
		{
			CaptureBiome? capture = CaptureIconBiomeSettings(biomeChoice);
			if (capture != null)
			{
				return capture;
			}
			return orig.Invoke(biomeChoice);
		}

		private void DrawModdedCaptureIcons(ILContext il)
		{
			ILCursor c = new ILCursor(il);
			ILLabel IL_01ca = c.DefineLabel();
			ILLabel IL_0000 = c.DefineLabel();

			//add your mod's icons
			//Bit choppy as it uses X of the icon frame to get the mod icon count from all the mods, any other mod that doesnt use this solution may break this edit
			c.GotoNext(MoveType.After, i => i.MatchCall<Rectangle>(".ctor")); //go after the rectangle to save the modded icons to the X, the X gets rewritten down the line but this allows us to get a full collection of all the modded icons added by other mods
			c.EmitLdloca(0); //ref r
			c.EmitDelegate((ref Rectangle r) =>
			{
				int prevCount = r.X; //get the current modded icon count from the rectangle's X
				if (biomeCaptureCount > 0) //check if this mod has more than 0 icons
				{
					for (int p = 0; p < biomeCaptureCount; p++)
					{
						r.X++; //incriment the modded icon count for every icon added
						biomeCapturesIndexs[p] = r.X + 12; //index is saved as modded icon number + 12 (12 is the vanilla icon count
					}
				}
			});

			//add continue loop
			c.GotoNext(MoveType.After, i => i.MatchBeq(out IL_01ca)); //get the continue (ie the step the continue jumps to
			c.MarkLabel(IL_0000); //mark the lable for where the if statement should jump out to 
			c.GotoPrev(MoveType.Before, i => i.MatchLdcI4(1), i => i.MatchBneUn(out _)); //find the i == 1 condition, seperate the i from the i == 1, use the i in the delegate and then inject a new i to not break opcodes
			c.EmitLdloc(0); //r
			c.EmitLdloc(2); //j (or x)
			c.EmitDelegate((int i, Rectangle r, int j) => {
				return i >= ((int)Math.Ceiling(((double)r.X + 13.00) / 7.00) - 1) && j >= (r.X + 13) - (i * 7); //the if statement for the continue statement, this makes all other icons outside of the bounds of whats added to be not drawn
			});
			c.EmitBrtrue(IL_01ca); //if the return was true, continue
			c.EmitBr(IL_0000); //jump to the rest of the code, skipping the 'if (i == 1 && j == 6) { continue; }'
			c.EmitLdloc(1); //readd the i used by the delegate to not break opcodes, despite this code now being unreachable

			//save icon count
			c.GotoNext(MoveType.Before, i => i.MatchLdloca(0), i => i.MatchLdarg(2), i => i.MatchLdfld<Point>("X")); //before r.X is overritten
			c.EmitLdloc(0); //r
			c.EmitDelegate((Rectangle r) =>
			{
				maxModdedCaptureCounts = r.X; //save the maximum number of modded icons, this is to readd this number later as well as to get a local count of all modded icons added
			});

			//Add offset
			//This is to manually add an offset for added icons, vanilla manually adds a 12 pixel offset for the second row but here we use a calculation to offset the X if there is not enough in a row
			c.GotoNext(MoveType.After, i => i.MatchLdcI4(12), i => i.MatchLdloc1(), i => i.MatchMul()); //find where 12 * i is in the r.X calculation
			c.EmitLdloc(1); //i (or y)
			c.EmitDelegate((int outdatedOffset, int i) => //the old offset is here, this is for any other mod as their offset delegate will be used here if modded icons are already added
			{
				if (i >= (int)Math.Ceiling(((double)maxModdedCaptureCounts + 13.00) / 7.00) - 1) //if at the last row
				{
					return (int)(24 * ((double)(7 - ((maxModdedCaptureCounts + 13) - (i * 7))) / 2.00)); //return offset based on the final row count
				}
				return 0; //otherwise return 0 to not offset
			});

			//Modify Textures
			//here mods modify their assets, text and their frame X position of the image, frame X is included for any mod that was to string their icons in a row like Extra 130
			c.GotoNext(MoveType.After, i => i.MatchCall<Color>("get_White"), i => i.MatchPop()); //go after the unused color local variable
			c.EmitLdloc(3); //num2
			c.EmitLdloc(0); //r
			c.EmitLdarg(3); //mouse
			c.EmitLdloca(5); //value
			c.EmitLdloca(6); //x
			c.EmitDelegate((int captureIconID, Rectangle iconHitbox, Point mouse, ref Texture2D iconTexture, ref int textureXposition) =>
			{
				CaptureIconSetInfo(captureIconID, iconHitbox, mouse, ref iconTexture, ref textureXposition);
			});

			//reset mod icon count
			//c.GotoNext(MoveType.After, i => i.MatchCall<Color>("op_Multiply"), i => i.MatchCallvirt<SpriteBatch>("Draw")); //after the last draw call //due to a monomod issue, these instruction matchings do not work well
			c.GotoNext(MoveType.Before, i => i.MatchLdloc2(), i => i.MatchLdcI4(1), i => i.MatchAdd(), i => i.MatchStloc2());
			c.EmitLdloca(0); //ref r
			c.EmitDelegate((ref Rectangle r) =>
			{
				r.X = maxModdedCaptureCounts; //readd the modded icon count to the r.X
			});

			//modify the Y loop
			c.GotoNext(MoveType.After, i => i.MatchLdloc(1), i => i.MatchLdcI4(2)); //find the i < 2
			c.EmitLdloc(0); //r
			c.EmitDelegate((int oldMaxY, Rectangle r) => //as the same as the offset, we get the old maximum Y number as it will also get other mod maximum Y's
			{
				return (int)Math.Ceiling(((double)r.X + 13.00) / 7.00); //calculate the number of rows there are depending on the count of icons
			});
		}
	}
}
