using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    private ExxoUIImageButton? stopButton;
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

        stopButton =
            new ExxoUIImageButton(ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/JukeboxStop"))
            { Scale = 1, Tooltip = Language.GetTextValue("Mods.Avalon.Jukebox.Stop"), HAlign = UIAlign.Center };
        mainContainer.Append(stopButton);
        stopButton.OnLeftClick += (_, args) =>
        {
            Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrack = false;
            Main.NewText(Language.GetTextValue("Mods.Avalon.Jukebox.StoppedPlaying"));
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
                Main.SceneMetrics.ActiveMusicBox = AvalonJukeboxPlayer.JukeboxTracks.FindIndex(j => j == i);
                Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrack = true;
                Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrackID = AvalonJukeboxPlayer.JukeboxTracks.FindIndex(j => j == i);
                Main.NewText(Language.GetTextValue("Mods.Avalon.Jukebox.NowPlaying") +
                    Lang.GetItemName(AvalonJukeboxPlayer.JukeboxTracks[AvalonJukeboxPlayer.JukeboxTracks.FindIndex(j => j == i)])
                    .ToString().Replace(Language.GetTextValue("ItemName.MusicBox") + " (", "").Replace(")", ""));
            };
            interFace?.Grid.Append(jukeboxTrack);
        }
        if (ExxoAvalonOrigins.MusicMod != null)
        {
            foreach (int i in AvalonJukeboxPlayer.AvalonTracks)
            {
                var jukeboxTrack = new ExxoUIItemSlot(TextureAssets.InventoryBack7, i);
                jukeboxTrack.OnLeftClick += (_, listeningElement) =>
                {
                    string str = Language.GetTextValue("Mods.Avalon.Jukebox.NowPlaying");
                    if (i == ModContent.ItemType<Items.MusicBoxes.MusicBoxArmageddonSlime>())
                    {
                        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrack = true;
                        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrackID = MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/ArmageddonSlime");
                        str += Language.GetTextValue("Mods.Avalon.Items.MusicBoxArmageddonSlime.DisplayName")
                            .Replace(Language.GetTextValue("ItemName.MusicBox") + " (", "").Replace(")", "");
                    }
                    else if (i == ModContent.ItemType<Items.MusicBoxes.MusicBoxBacteriumPrime>())
                    {
                        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrack = true;
                        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrackID = MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/BacteriumPrime");
                        str += Language.GetTextValue("Mods.Avalon.Items.MusicBoxBacteriumPrime.DisplayName")
                            .Replace(Language.GetTextValue("ItemName.MusicBox") + " (", "").Replace(")", "");
                    }
                    else if (i == ModContent.ItemType<Items.MusicBoxes.MusicBoxContagion>())
                    {
                        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrack = true;
                        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrackID = MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/Contagion");
                        str += Language.GetTextValue("Mods.Avalon.Items.MusicBoxContagion.DisplayName")
                            .Replace(Language.GetTextValue("ItemName.MusicBox") + " (", "").Replace(")", "");
                    }
                    else if (i == ModContent.ItemType<Items.MusicBoxes.MusicBoxDarkMatter>())
                    {
                        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrack = true;
                        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrackID = MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/DarkMatter");
                        str += Language.GetTextValue("Mods.Avalon.Items.MusicBoxDarkMatter.DisplayName")
                            .Replace(Language.GetTextValue("ItemName.MusicBox") + " (", "").Replace(")", "");
                    }
                    else if (i == ModContent.ItemType<Items.MusicBoxes.MusicBoxDesertBeak>())
                    {
                        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrack = true;
                        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrackID = MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/DesertBeak");
                        str += Language.GetTextValue("Mods.Avalon.Items.MusicBoxDesertBeak.DisplayName")
                            .Replace(Language.GetTextValue("ItemName.MusicBox") + " (", "").Replace(")", "");
                    }
                    else if (i == ModContent.ItemType<Items.MusicBoxes.MusicBoxHellCastle>())
                    {
                        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrack = true;
                        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrackID = MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/Hellcastle");
                        str += Language.GetTextValue("Mods.Avalon.Items.MusicBoxHellCastle.DisplayName")
                            .Replace(Language.GetTextValue("ItemName.MusicBox") + " (", "").Replace(")", "");
                    }
                    else if (i == ModContent.ItemType<Items.MusicBoxes.MusicBoxPhantasm>())
                    {
                        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrack = true;
                        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrackID = MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/Phantasm");
                        str += Language.GetTextValue("Mods.Avalon.Items.MusicBoxPhantasm.DisplayName")
                            .Replace(Language.GetTextValue("ItemName.MusicBox") + " (", "").Replace(")", "");
                    }
                    else if (i == ModContent.ItemType<Items.MusicBoxes.MusicBoxSkyFortress>())
                    {
                        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrack = true;
                        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrackID = MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/SkyFortress");
                        str += Language.GetTextValue("Mods.Avalon.Items.MusicBoxSkyFortress.DisplayName")
                            .Replace(Language.GetTextValue("ItemName.MusicBox") + " (", "").Replace(")", "");
                    }
                    else if (i == ModContent.ItemType<Items.MusicBoxes.MusicBoxTuhrtlOutpost>())
                    {
                        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrack = true;
                        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrackID = MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/TuhrtlOutpost");
                        str += Language.GetTextValue("Mods.Avalon.Items.MusicBoxTuhrtlOutpost.DisplayName")
                            .Replace(Language.GetTextValue("ItemName.MusicBox") + " (", "").Replace(")", "");
                    }
                    else if (i == ModContent.ItemType<Items.MusicBoxes.MusicBoxUndergroundContagion>())
                    {
                        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrack = true;
                        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrackID = MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/UndergroundContagion");
                        str += Language.GetTextValue("Mods.Avalon.Items.MusicBoxUndergroundContagion.DisplayName")
                            .Replace(Language.GetTextValue("ItemName.MusicBox") + " (", "").Replace(")", "");
                    }
                    Main.NewText(str);
                };
                interFace?.Grid.Append(jukeboxTrack);
            }
        }    
    }
}
