using System;
using Avalon.Common;
using Avalon.UI;
using Avalon.UI.Next;
using Avalon.UI.Next.Enums;
using Avalon.UI.Next.Structs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.States;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
public class ContagionSelectionMenu : ModHook {
    public enum WorldEvilSelection {
        Random,
        Corruption,
        Crimson,
        Contagion,
    }

    private const int PageSizeIncrease = 48;

    public WorldEvilSelection SelectedWorldEvil { get; private set; } = WorldEvilSelection.Random;

    private readonly ExxoUIGroupOptionButton<WorldEvilSelection>[] evilButtons =
        new ExxoUIGroupOptionButton<WorldEvilSelection>[Enum.GetValues<WorldEvilSelection>().Length];

    /// <inheritdoc />
    protected override void Apply() {
        IL_UIWorldCreation.BuildPage += ILBuildPage;
        IL_UIWorldCreation.MakeInfoMenu += ILMakeInfoMenu;
        IL_UIWorldCreation.ShowOptionDescription += ILShowOptionDescription;
        On_UIWorldCreation.SetDefaultOptions += OnSetDefaultOptions;
        On_UIWorldCreation.AddWorldEvilOptions += OnAddWorldEvilOptions;
    }

    private static void ILBuildPage(ILContext il) {
        var c = new ILCursor(il);

        // Increase page size
        c.GotoNext(i => i.MatchStloc(0));
        c.Emit(OpCodes.Ldc_I4, PageSizeIncrease);
        c.Emit(OpCodes.Add);
    }

    private static void ILMakeInfoMenu(ILContext il) {
        var c = new ILCursor(il);

        c.GotoNext(i => i.MatchLdstr("evil"));

        c.Emit(OpCodes.Ldloc_1);
        c.Emit(OpCodes.Ldc_R4, 38f);
        c.Emit(OpCodes.Add);
        c.Emit(OpCodes.Stloc_1);
    }

    private void OnAddWorldEvilOptions(On_UIWorldCreation.orig_AddWorldEvilOptions orig, UIWorldCreation self,
                                       UIElement container, float accumulatedHeight, UIElement.MouseEvent clickEvent,
                                       string tagGroup, float usableWidthPercent) {
        orig(self, new UIElement(), accumulatedHeight, clickEvent, tagGroup, usableWidthPercent);
        AddEvilOptions(self, container, accumulatedHeight, ClickEvilOption, tagGroup, usableWidthPercent);
    }

    private void OnSetDefaultOptions(On_UIWorldCreation.orig_SetDefaultOptions orig, UIWorldCreation self) {
        orig(self);

        SelectedWorldEvil = WorldEvilSelection.Random;
        foreach (ExxoUIGroupOptionButton<WorldEvilSelection> evilButton in evilButtons) {
            evilButton.SetCurrentOption(SelectedWorldEvil);
        }
    }

    private static void ILShowOptionDescription(ILContext il) {
        var c = new ILCursor(il);

        // Navigate to before final break
        c.Index = c.Instrs.Count - 1;
        c.GotoPrev(i => i.MatchBrfalse(out _));

        // Add description handling logic
        c.Emit(OpCodes.Pop);
        c.Emit(OpCodes.Ldloc_0); // localizedText
        c.Emit(OpCodes.Ldarg_2); // listeningElement
        c.EmitDelegate((LocalizedText localizedText, UIElement listeningElement) => {
            if (listeningElement is ExxoUIGroupOptionButton<WorldEvilSelection> evilButton) {
                localizedText = evilButton.Description;
            }

            return localizedText;
        });
        c.Emit(OpCodes.Stloc_0);
        c.Emit(OpCodes.Ldloc_0);
    }

    private void AddEvilOptions(UIWorldCreation self, UIElement container, float accumulatedHeight,
                                UIElement.MouseEvent clickEvent, string tagGroup,
                                float usableWidthPercent) {
        LocalizedText[] titles = {
            Lang.misc[103],
            Lang.misc[101],
            Lang.misc[102],
            Language.GetText(ExxoAvalonOrigins.Mod.GetLocalizationKey("World.EvilSelection.Contagion.Title")),
        };
        LocalizedText[] descriptions = {
            Language.GetText("UI.WorldDescriptionEvilRandom"),
            Language.GetText("UI.WorldDescriptionEvilCorrupt"),
            Language.GetText("UI.WorldDescriptionEvilCrimson"),
            Language.GetText(ExxoAvalonOrigins.Mod.GetLocalizationKey("World.EvilSelection.Contagion.Description")),
        };
        Color[] colors = {
            Color.White,
            Color.MediumPurple,
            Color.IndianRed,
            Color.Green,
        };
        Asset<Texture2D>[] icons = {
            Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
            Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilCorruption"),
            Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilCrimson"),
            null!,
        };

        var buttonGrid = new UI.Next.ExxoUIElement {
            Width = UIDimension.Fill,
            Position = new Point(0, (int)(accumulatedHeight + 0.5f)),
            DisplayMode = UI.Next.Enums.DisplayMode.Grid,
            GridCols = 2,
        };
        container.Append(new ExxoToVanillaUIAdapter(buttonGrid));
        buttonGrid.Gap = new UIDimension(4, ScreenUnit.Pixels);

        for (int i = 0; i < evilButtons.Length; i++) {
            var groupOptionButton = new ExxoUIGroupOptionButton<WorldEvilSelection>(
                Enum.GetValues<WorldEvilSelection>()[i],
                titles[i],
                descriptions[i],
                colors[i],
                icons[i],
                1f,
                1f,
                16f);

            groupOptionButton.OnLeftMouseDown += clickEvent;
            groupOptionButton.OnMouseOver += self.ShowOptionDescription;
            groupOptionButton.OnMouseOut += self.ClearOptionDescription;

            buttonGrid.Children.Add(new VanillaToExxoUIAdapter(groupOptionButton)
                { SnapNode = new SnapNode(tagGroup, i) });
            evilButtons[i] = groupOptionButton;
        }
    }

    private void ClickEvilOption(UIMouseEvent evt, UIElement listeningElement) {
        var groupOptionButton = (ExxoUIGroupOptionButton<WorldEvilSelection>)listeningElement;

        SelectedWorldEvil = groupOptionButton.OptionValue;
        foreach (ExxoUIGroupOptionButton<WorldEvilSelection> evilButton in evilButtons) {
            evilButton.SetCurrentOption(SelectedWorldEvil);
        }
    }
}
