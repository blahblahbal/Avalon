using Avalon.Common;
using Avalon.ModSupport;
using Avalon.Tiles.Contagion.Chunkstone;
using Avalon.Tiles.Contagion.ContagionGrasses;
using Avalon.Tiles.Contagion.HardenedSnotsand;
using Avalon.Tiles.Contagion.Snotsand;
using Avalon.Tiles.Contagion.Snotsandstone;
using Avalon.Tiles.Contagion.YellowIce;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
    internal class DryadTextDetour : ModHook
    {
		public override bool IsLoadingEnabled(Mod mod) => !AltLibrarySupport.Enabled;
        protected override void Apply()
        {
			IL_Lang.GetDryadWorldStatusDialog += DryadWorldStatusEdit;
			IL_WorldGen.AddUpAlignmentCounts += AddUpAligmenttmodEvilsandGoods;
			IL_WorldGen.CountTiles += SettmodvilsandGoods;
		}

        public override void Unload()
        {
			IL_Lang.GetDryadWorldStatusDialog -= DryadWorldStatusEdit;
			IL_WorldGen.AddUpAlignmentCounts -= AddUpAligmenttmodEvilsandGoods;
			IL_WorldGen.CountTiles -= SettmodvilsandGoods;
		}

		private void SettmodvilsandGoods(ILContext il)
		{
			ILCursor c = new(il);
			c.GotoNext(MoveType.After, i => i.MatchLdsfld<WorldGen>("totalGood2"), i => i.MatchStsfld<WorldGen>("totalGood"));
			c.EmitDelegate(() =>
			{
				AvalonWorld.totalSick = AvalonWorld.totalSick2;
			});
			c.GotoNext(MoveType.After, i => i.MatchCall("System.Math", "Round"), i => i.MatchConvU1(), i => i.MatchStsfld<WorldGen>("tBlood"));
			c.EmitDelegate(() =>
			{
				AvalonWorld.tSick = (byte)Math.Round(AvalonWorld.totalSick / (double)WorldGen.totalSolid * 100.0);
				if (AvalonWorld.tSick == 0 && AvalonWorld.totalSick > 0)
				{
					AvalonWorld.tSick = 1;
				}
			});
			c.GotoNext(MoveType.After, i => i.MatchLdcI4(0), i => i.MatchStsfld<WorldGen>("totalGood2"));
			c.EmitDelegate(() =>
			{
				AvalonWorld.totalSick2 = 0;
			});
		}

		private void AddUpAligmenttmodEvilsandGoods(ILContext il)
		{
			ILCursor c = new(il);
			c.GotoNext(MoveType.After, i => i.MatchLdcI4(0), i => i.MatchStsfld<WorldGen>("totalGood2"), i => i.MatchLdcI4(0), i => i.MatchStsfld<WorldGen>("totalBlood2"));
			c.EmitDelegate(() =>
			{
				AvalonWorld.totalSick2 = 0;
			});
			c.GotoNext(MoveType.After, i => i.MatchLdloc2(), i => i.MatchLdsfld("Terraria.ID.TileID/Sets", "CrimsonCountCollection"), i => i.MatchCallvirt<List<int>>("get_Count"), i => i.MatchBlt(out _));
			c.EmitDelegate(() =>
			{
				for (int i = 0; i < AvalonWorld.ContagionCountCollection.Count; i++)
				{
					AvalonWorld.totalSick2 += WorldGen.tileCounts[AvalonWorld.ContagionCountCollection[i]];
				}
			});
			c.GotoNext(MoveType.Before, i => i.MatchLdsfld<WorldGen>("tileCounts"), i => i.MatchLdcI4(0), i => i.MatchLdsfld<WorldGen>("tileCounts"));
			c.EmitDelegate(() =>
			{
				WorldGen.totalSolid2 +=
					WorldGen.tileCounts[ModContent.TileType<Chunkstone>()] +
					WorldGen.tileCounts[ModContent.TileType<Ickgrass>()] +
					WorldGen.tileCounts[ModContent.TileType<ContagionJungleGrass>()] +
					WorldGen.tileCounts[ModContent.TileType<Snotsand>()] +
					WorldGen.tileCounts[ModContent.TileType<YellowIce>()] +
					WorldGen.tileCounts[ModContent.TileType<Snotsandstone>()] +
					WorldGen.tileCounts[ModContent.TileType<HardenedSnotsand>()];
			});
		}

		private void DryadWorldStatusEdit(ILContext il)
		{
			ILCursor c = new(il);
			ILLabel IL_01b2 = c.DefineLabel();
			ILLabel IL_013f = c.DefineLabel();
			c.GotoNext(MoveType.Before, i => i.MatchLdloc0(), i => i.MatchRet());
			c.EmitBr(IL_013f);
			c.GotoNext(MoveType.After, i => i.MatchLdstr("DryadSpecialText.WorldStatusHallow"), i => i.MatchLdsfld<Main>("worldName"), i => i.MatchLdloc1(), i => i.MatchBox(out _), i => i.MatchCall("Terraria.Localization.Language", "GetTextValue"), i => i.MatchStloc0());
			c.MarkLabel(IL_013f);
			c.GotoNext(MoveType.Before, i => i.MatchLdloc0(), i => i.MatchLdstr(" "), i => i.MatchLdloc(4));
			c.EmitLdarg0();
			c.EmitLdindU1();
			c.EmitBrfalse(IL_01b2);
			c.EmitLdloc0();
			c.EmitRet();
			c.MarkLabel(IL_01b2);
			c.Goto(0);

			//This adds the tgoods and tbads to the dialogs so dialogs such as "the world is in balance" or "you have more work to do" can function properly with modded edits to the dryad dialog system
			c.GotoNext(MoveType.Before, i => i.MatchConvR8(), i => i.MatchLdcR8(1.2), i => i.MatchMul());
			c.EmitDelegate(() =>
			{
				return tmodGoodCount() * 1.2;
			});
			c.EmitConvR8();
			c.EmitAdd(); //(tGood + tmodGood) * 1.2
			c.GotoNext(MoveType.Before, i => i.MatchConvR8(), i => i.MatchLdcR8(0.8), i => i.MatchMul());
			c.EmitDelegate(() =>
			{
				return tmodGoodCount() * 0.8;
			});

			c.EmitConvR8();
			c.EmitAdd(); //(tGood + tmodGood) * 0.8
			c.GotoNext(MoveType.After, i => i.MatchAdd(), i => i.MatchConvR8(), i => i.MatchBle(out _), i => i.MatchLdloc1());
			c.EmitDelegate(tmodGoodCount);
			c.EmitAdd(); //(tGood + tmodGood) >= tEvil + tBlood 
			c.GotoNext(MoveType.After, i => i.MatchLdloc1(), i => i.MatchLdcI4(20), i => i.MatchAdd());
			c.EmitDelegate(tmodGoodCount);
			c.EmitAdd(); //(tEvil + tBlood > tGood + 20 + tmodGood)
			c.Goto(0);

			//Applies the addition of tmodEvil to tEvil and tBlood for each boolean check
			for (int j = 0; j < 5; j++)
			{
				c.GotoNext(MoveType.After, i => i.MatchLdloc2(), i => i.MatchLdloc3(), i => i.MatchAdd());
				c.EmitDelegate(tmodEvilCount);
				c.EmitAdd();
			}

			//We add extra code for both the percentages display but also displaying our own world description too
			c.GotoNext(MoveType.After, i => i.MatchLdstr("DryadSpecialText.WorldDescriptionBalanced"), i => i.MatchCall("Terraria.Localization.Language", "GetTextValue"), i => i.MatchStloc(4));
			c.EmitLdarg(0);
			c.EmitLdloca(0);
			c.EmitLdarg(0);
			c.EmitLdindU1();
			//Percentage adder/editor
			c.EmitDelegate((ref string text, bool worldIsEntirelyPure) =>
			{
				int tSick = AvalonWorld.tSick;
				bool flag = worldIsEntirelyPure;
				if (flag)
				{
					if (tSick > 0)
					{
						text = Language.GetTextValue("Mods.Avalon.DryadSpecialText.WorldStatusSick", Main.worldName, tSick);
						flag = false;
					}
				}
				else
				{
					if (tSick > 0)
					{
						string localText;
						string textStart = Main.worldName + Language.GetTextValue("Mods.Avalon.DryadSpecialText.WorldStatusWorldNameIs");
						string textEnd = text.Substring(textStart.Length);
						if (text.Contains(Language.GetTextValue("Mods.Avalon.DryadSpecialText.WorldStatusContainsAnd")))
						{
							localText = Language.GetTextValue("Mods.Avalon.DryadSpecialText.WorldStatusSickComma", tSick);
						}
						else
						{
							localText = Language.GetTextValue("Mods.Avalon.DryadSpecialText.WorldStatusSickAnd", tSick);
						}
						text = textStart + localText + " " + textEnd;
					}
				}
				return flag;
			});
			c.EmitStindI1();
			c.EmitLdloca(4);
			//description editor
			c.EmitDelegate((ref string arg) =>
			{
				//Used for replacing the description text, set 'arg' to any text with a condition around your tmodEvil/Good
			});
		}

		private int tmodEvilCount()
		{
			int tSick = AvalonWorld.tSick;
			return tSick;
		}

		private int tmodGoodCount()
		{
			//How this works is that you combine all your tmodGood stats into the return
			//for example:
			//return tGood2 + tCandy + tMusic + tMech;
			return 0;
		}
	}
}
