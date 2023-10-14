using Avalon.Common.Players;
using Avalon.Data;
using Avalon.Items.Potions.Buff;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon.UI.Jukebox;

internal class JukeboxUIState : ExxoUIState
{
    private ExxoUIDraggablePanel? mainPanel;
    private JukeboxUIInterface? interFace;
    public override void OnInitialize()
    {
        base.OnInitialize();

        mainPanel = new ExxoUIDraggablePanel
        {
            Width = StyleDimension.FromPixels(720),
            Height = StyleDimension.FromPixels(660),
            VAlign = UIAlign.Center,
            HAlign = UIAlign.Center,
        };
        mainPanel.SetPadding(15);
        Append(mainPanel);

        var mainContainer = new ExxoUIList
        {
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill,
            ContentHAlign = UIAlign.Center,
        };
        mainPanel.Append(mainContainer);

        var titleRow = new ExxoUIList
        {
            Width = StyleDimension.Fill,
            Direction = Direction.Horizontal,
            Justification = Justification.Center,
            FitHeightToContent = true,
            ContentVAlign = UIAlign.Center,
        };
        mainContainer.Append(titleRow);
        var titleText = new ExxoUITextPanel(Language.GetTextValue("Mods.Avalon.Jukebox.Name"), 0.8f, true);
        titleRow.Append(titleText);

        var jukeboxContainer = new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList());
        jukeboxContainer.Width.Set(0, 1);
        jukeboxContainer.InnerElement.Direction = Direction.Horizontal;
        mainContainer.Append(jukeboxContainer, new ExxoUIList.ElementParams(true, false));

        interFace = new JukeboxUIInterface();
        jukeboxContainer.InnerElement.Append(interFace, new ExxoUIList.ElementParams(true, false));

        interFace.Scrollbar.OnViewPositionChanged += delegate
        {
        };

        RefreshTrackList();
    }

    public override void OnActivate()
    {
        base.OnActivate();
        //Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().UpdateHerbTier();
        SoundEngine.PlaySound(SoundID.MenuOpen);
    }
    public override void OnDeactivate()
    {
        base.OnDeactivate();
        SoundEngine.PlaySound(SoundID.MenuClose);
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        Player player = Main.LocalPlayer;
        AvalonJukeboxPlayer modPlayer = player.GetModPlayer<AvalonJukeboxPlayer>();

        if (player.chest != -1 || Main.npcShop != 0)
        {
            modPlayer.DisplayJukeboxInterface = false;
            player.dropItemCheck();
            Recipe.FindRecipes();
        }
        RefreshTrackList();
    }
    private void RefreshTrackList()
    {
        interFace?.Grid.Clear();

        //interFace?.Grid.RemoveAllChildren();
        //interFace?.Grid.Clear();
        foreach (int i in AvalonJukeboxPlayer.JukeboxTracks)
        {
            var jukeboxTrack = new ExxoUIItemSlot(TextureAssets.InventoryBack7, i);
            jukeboxTrack.OnLeftClick += (_, listeningElement) =>
            {
                if (i == ModContent.ItemType<Items.Other.STOP>())
                {
                    Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrack = false;
                    Main.NewText("Stopped playing tunes");
                }
                else
                {
                    Main.SceneMetrics.ActiveMusicBox = AvalonJukeboxPlayer.JukeboxTracks.FindIndex(j => j == i);
                    Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrack = true;
                    Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrackID = AvalonJukeboxPlayer.JukeboxTracks.FindIndex(j => j == i);
                    Main.NewText("Now playing: " +
                        Lang.GetItemName(AvalonJukeboxPlayer.JukeboxTracks[AvalonJukeboxPlayer.JukeboxTracks.FindIndex(j => j == i)])
                        .ToString().Replace("Music Box (", "").Replace(")", ""));
                }
            };
            interFace?.Grid.Append(jukeboxTrack);
        }
    }
}
