using Avalon.Common;
using Avalon.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.Utils;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent.UI.States;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon.Hooks;

public class JungleSelectionMenu : ModHook
{
    protected override void Apply()
    {
		if (ModContent.GetInstance<AvalonClientConfig>().BetaTropicsGen)
		{
			IL_UIWorldCreation.BuildPage += ILBuildPage;
			IL_UIWorldCreation.MakeInfoMenu += ILMakeInfoMenu;
			IL_UIWorldCreation.ShowOptionDescription += ILShowOptionDescription;
			On_UIWorldCreation.SetDefaultOptions += OnSetDefaultOptions;
		}
	}
    public enum WorldJungleSelection
    {
        Random,
        Jungle,
        Tropics
    }

    private static readonly GroupOptionButton<WorldJungleSelection>[] JungleButtons =
            new GroupOptionButton<WorldJungleSelection>[Enum.GetValues<WorldJungleSelection>().Length];

    public static void ILBuildPage(ILContext il)
    {
        var c = new ILCursor(il);

        // Increase world gen container size
        c.GotoNext(i => i.MatchStloc(0));
        c.Emit(OpCodes.Ldc_I4, 48);
        c.Emit(OpCodes.Add);
    }

    public static void ILMakeInfoMenu(ILContext il)
    {
        var c = new ILCursor(il);

        // Getting spacing indexes for copying later
        c.GotoNext(i => i.MatchLdstr("evil"));
        c.GotoNext(i => i.MatchLdloc(1));
        int startOfSpacing = c.Index;
        c.GotoNext(i => i.MatchCall(out _));
        int endOfSpacing = c.Index + 1;

        // Navigate to position to add options 
        c.Index = c.Instrs.Count - 1;
        c.GotoPrev(i => i.MatchLdcR4(48));
        c.GotoNext(i => i.MatchCall(out _));
        c.Index++;

        // Adding Hallowed options
        c.Emit(OpCodes.Ldarg_0); // self
        c.Emit(OpCodes.Ldloc_0); // container
        c.Emit(OpCodes.Ldloc_1); // accumulatedHeight
        c.Emit(OpCodes.Ldloc, 10); // usableWidthPercent
        c.EmitDelegate((UIWorldCreation self, UIElement container, float accumulatedHeight, float usableWidthPercent) =>
            AddJungleOptions(self, container, accumulatedHeight, ClickJungleOption, "jungle",
                usableWidthPercent));

        // Copying IL for spacing and horizontal bar
        c.Instrs.InsertRange(c.Index, c.Instrs.ToArray()[startOfSpacing..endOfSpacing]);
    }

    public static void OnSetDefaultOptions(On_UIWorldCreation.orig_SetDefaultOptions orig, UIWorldCreation self)
    {
        orig(self);

        ModContent.GetInstance<AvalonWorld>().SelectedWorldJungle = WorldJungleSelection.Random;
        foreach (GroupOptionButton<WorldJungleSelection> underworldButton in JungleButtons)
        {
            underworldButton.SetCurrentOption(WorldJungleSelection.Random);
        }
    }

    public static void ILShowOptionDescription(ILContext il)
    {
        var c = new ILCursor(il);

        // Navigate to before final break
        c.Index = c.Instrs.Count - 1;
        c.GotoPrev(i => i.MatchBrfalse(out _));

        // Add description handling logic
        c.Emit(OpCodes.Pop);
        c.Emit(OpCodes.Ldloc_0); // localizedText
        c.Emit(OpCodes.Ldarg_2); // listeningElement
        c.EmitDelegate((LocalizedText localizedText, UIElement listeningElement) =>
            listeningElement is not GroupOptionButton<WorldJungleSelection> underworldButton ? localizedText : underworldButton.Description);
        c.Emit(OpCodes.Stloc_0);
        c.Emit(OpCodes.Ldloc_0);
    }

    private static void AddJungleOptions(UIWorldCreation self, UIElement container, float accumulatedHeight,
                                             UIElement.MouseEvent clickEvent, string tagGroup,
                                             float usableWidthPercent)
    {
        LocalizedText[] titles = {
            Language.GetText(Language.GetTextValue("Mods.Avalon.JungleSelection.Random.Title")),
            Language.GetText(Language.GetTextValue("Mods.Avalon.JungleSelection.Jungle.Title")),
            Language.GetText(Language.GetTextValue("Mods.Avalon.JungleSelection.Tropics.Title")),
        };
        LocalizedText[] descriptions = {
            Language.GetText(Language.GetTextValue("Mods.Avalon.JungleSelection.Random.Description")),
            Language.GetText(Language.GetTextValue("Mods.Avalon.JungleSelection.Jungle.Description")),
            Language.GetText(Language.GetTextValue("Mods.Avalon.JungleSelection.Tropics.Description")),
        };
        Color[] colors = {
            Color.White,
            new Color(107, 182, 0),
            new Color(255, 140, 0),
        };
        Asset<Texture2D>[] icons = {
            Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
            //Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Tags_Shadow", AssetRequestMode.ImmediateLoad),
            ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/WorldIcons/IconJungle"), // change later if we can figure it out
            ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/WorldIcons/IconTropics"),
        };
        for (int i = 0; i < JungleButtons.Length; i++)
        {
            var groupOptionButton = new GroupOptionButton<WorldJungleSelection>(
                Enum.GetValues<WorldJungleSelection>()[i],
                titles[i],
                descriptions[i],
                colors[i],
                icons[i],
                1f,
                1f,
                16f)
            {
                Width = StyleDimension.FromPixelsAndPercent(
                    -4 * (JungleButtons.Length - 1),
                    1f / JungleButtons.Length * usableWidthPercent),
                Left = StyleDimension.FromPercent(1f - usableWidthPercent),
                HAlign = i / (float)(JungleButtons.Length - 1),
            };
            groupOptionButton.Top.Set(accumulatedHeight, 0f);
            groupOptionButton.OnLeftMouseDown += clickEvent;
            groupOptionButton.OnMouseOver += self.ShowOptionDescription;
            groupOptionButton.OnMouseOut += self.ClearOptionDescription;
            groupOptionButton.SetSnapPoint(tagGroup, i);
            container.Append(groupOptionButton);
            JungleButtons[i] = groupOptionButton;
        }
    }

    private static void ClickJungleOption(UIMouseEvent evt, UIElement listeningElement)
    {
        var groupOptionButton = (GroupOptionButton<WorldJungleSelection>)listeningElement;
        ModContent.GetInstance<AvalonWorld>().SelectedWorldJungle = groupOptionButton.OptionValue;

        foreach (GroupOptionButton<WorldJungleSelection> underworldButton in JungleButtons)
        {
            underworldButton.SetCurrentOption(groupOptionButton.OptionValue);
        }
    }
}
