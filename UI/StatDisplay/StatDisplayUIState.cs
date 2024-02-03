using Avalon.Common.Players;
using Avalon.Data;
using Avalon.Items.Potions.Buff;
using Avalon.UI.Herbology;
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

namespace Avalon.UI.StatDisplay;

public class StatDisplayUIState : ExxoUIState
{
    private HerbologyUIHerbExchange? herbExchange;
    private ExxoUIDraggablePanel? mainPanel;
    private HerbologyUIPotionExchange? potionExchange;
    private HerbologyUIStats? stats;
    private HerbologyUITurnIn? turnIn;
    private HerbologyUIHerbCountAttachment? herbCountAttachment;
    private HerbologyUIPurchaseAttachment? purchaseAttachment;

    private ExxoUIImageButton? herbButton;
    private ExxoUIImageButton? potionButton;

    public override void OnInitialize()
    {
        base.OnInitialize();

        AvalonHerbologyPlayer herbologyPlayer = Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>();

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
        mainPanel.BackgroundColor = Color.Transparent;
        mainPanel.BorderColor = Color.Transparent;

        var statContainer = new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList());

        stats = new HerbologyUIStats();
        statContainer.InnerElement.Append(stats);

        turnIn = new HerbologyUITurnIn();
        statContainer.InnerElement.Append(turnIn);

        turnIn.Button.OnLeftClick += delegate
        {
            Item item = turnIn.ItemSlot.Item;

            AvalonHerbologyPlayer.HerbTier oldTier = Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().Tier;
            if (Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().SellItem(item))
            {
                item.stack = 0;
                if (oldTier != Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().Tier)
                {
                    RefreshContent();
                }
            }
        };

        var herbContainer = new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList());
        herbContainer.Width.Set(0, 1);
        herbContainer.InnerElement.Direction = Direction.Horizontal;
        mainContainer.Append(herbContainer, new ExxoUIList.ElementParams(true, false));

        herbExchange = new HerbologyUIHerbExchange();
        if (herbExchange.IsVisible)
        {
            herbContainer.InnerElement.Append(herbExchange, new ExxoUIList.ElementParams(true, false));
            herbExchange.Toggle.OnToggle += (_, args) => RefreshHerbList(args.Toggled);
            herbExchange.Scrollbar.OnViewPositionChanged += delegate
            {
                purchaseAttachment?.AttachTo(null);
                herbCountAttachment?.AttachTo(null);
            };

            Append(new ExxoUIContentLockPanel(herbExchange.Toggle,
                () => Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().Tier >=
                      AvalonHerbologyPlayer.HerbTier.Apprentice,
                Language.GetTextValue("Mods.Avalon.Herbology.ContentLocked.Title") + Language.GetTextValue("Mods.Avalon.Herbology.ContentLocked.Apprentice")));
        }

        var potionContainer = new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList());
        potionContainer.Width.Set(0, 1);
        potionContainer.InnerElement.Direction = Direction.Horizontal;
        mainContainer.Append(potionContainer, new ExxoUIList.ElementParams(true, false));

        potionExchange = new HerbologyUIPotionExchange();
        if (potionExchange.IsVisible)
        {
            potionContainer.InnerElement.Append(potionExchange, new ExxoUIList.ElementParams(true, false));

            potionExchange.Toggle.OnToggle += (_, args) => RefreshPotionList(args.Toggled);
            potionExchange.Scrollbar.OnViewPositionChanged += delegate
            {
                purchaseAttachment?.AttachTo(null);
                herbCountAttachment?.AttachTo(null);
            };

            var potionLock = new ExxoUIContentLockPanel(potionExchange,
                () => Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().Tier >=
                      AvalonHerbologyPlayer.HerbTier.Expert,
                Language.GetTextValue("Mods.Avalon.Herbology.ContentLocked.Title") + Language.GetTextValue("Mods.Avalon.Herbology.ContentLocked.Expert"));
            Append(potionLock);
            potionLock.OnLockStatusChanged += (_, args) => potionExchange.Scrollbar.Active = !args.Locked;
        }

        purchaseAttachment = new HerbologyUIPurchaseAttachment();
        Append(purchaseAttachment);

        purchaseAttachment.NumberInputWithButtons.NumberInput.OnKeyboardUpdate += (_, args) =>
        {
            if (args.KeyboardState.IsKeyDown(Keys.Escape))
            {
                purchaseAttachment.AttachTo(null);
                herbCountAttachment?.AttachTo(null);
            }
            else if (args.KeyboardState.IsKeyDown(Keys.Enter))
            {
                if (purchaseAttachment?.AttachmentHolder != null && herbologyPlayer.PurchaseItem(
                        purchaseAttachment.AttachmentHolder.Item,
                        purchaseAttachment.NumberInputWithButtons.NumberInput.Number))
                {
                    purchaseAttachment.AttachTo(null);
                    herbCountAttachment?.AttachTo(null);
                }
            }
        };

        purchaseAttachment.Button.OnLeftClick += delegate
        {
            if (purchaseAttachment?.AttachmentHolder != null && herbologyPlayer.PurchaseItem(
                    purchaseAttachment.AttachmentHolder.Item,
                    purchaseAttachment.NumberInputWithButtons.NumberInput.Number))
            {
                purchaseAttachment.AttachTo(null);
                herbCountAttachment?.AttachTo(null);
            }
        };

        herbButton =
            new ExxoUIImageButton(Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconRandomSeed"))
            { Scale = 1, Tooltip = "Herbs", HAlign = UIAlign.Left };
        mainContainer.Append(herbButton);
        herbButton.OnLeftClick += (_, args) =>
        {
            potionExchange.Hidden = true;
            herbExchange.Hidden = false;
            Main.NewText(Language.GetTextValue("Mods.Avalon.Jukebox.StoppedPlaying"));
            SoundEngine.PlaySound(SoundID.Grab);
        };

        potionButton =
            new ExxoUIImageButton(Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilCorruption"))
            { Scale = 1, Tooltip = "Herbs", HAlign = UIAlign.Left };
        mainContainer.Append(potionButton);
        potionButton.OnLeftClick += (_, args) =>
        {
            potionExchange.Hidden = false;
            herbExchange.Hidden = true;
            Main.NewText(Language.GetTextValue("Mods.Avalon.Jukebox.StoppedPlaying"));
            SoundEngine.PlaySound(SoundID.Grab);
        };


        herbCountAttachment = new HerbologyUIHerbCountAttachment();
        Append(herbCountAttachment);

        RefreshContent();


        //Player p = Main.LocalPlayer;

        //mainPanel = new ExxoUIDraggablePanel
        //{
        //    Width = StyleDimension.FromPixels(720),
        //    Height = StyleDimension.FromPixels(660),
        //    VAlign = UIAlign.Center,
        //    HAlign = UIAlign.Center,
        //};
        //mainPanel.SetPadding(15);
        //Append(mainPanel);

        //var mainContainer = new ExxoUIList
        //{
        //    Width = StyleDimension.Fill,
        //    Height = StyleDimension.Fill,
        //    ContentHAlign = UIAlign.Center,
        //};
        //mainPanel.Append(mainContainer);

        //var titleRow = new ExxoUIList
        //{
        //    Width = StyleDimension.Fill,
        //    Direction = Direction.Horizontal,
        //    Justification = Justification.Center,
        //    FitHeightToContent = true,
        //    ContentVAlign = UIAlign.Center,
        //};
        //mainContainer.Append(titleRow);
        //var titleText = new ExxoUITextPanel("Player Stats", 0.8f, true);
        //titleRow.Append(titleText);

        //var statsContainer = new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList());
        //statsContainer.Width.Set(0, 1);
        //statsContainer.InnerElement.Direction = Direction.Horizontal;
        //mainContainer.Append(statsContainer, new ExxoUIList.ElementParams(true, false));

        //statDisplay = new StatDisplayUIThing();
        //statsContainer.InnerElement.Append(statDisplay, new ExxoUIList.ElementParams(true, false));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        Player player = Main.LocalPlayer;
        AvalonHerbologyPlayer modPlayer = player.GetModPlayer<AvalonHerbologyPlayer>();

        if (player.chest != -1 || Main.npcShop != 0)
        {
            modPlayer.DisplayHerbologyMenu = false;
            player.dropItemCheck();
            Recipe.FindRecipes();
        }
    }

    public override void LeftClick(UIMouseEvent evt)
    {
        base.LeftClick(evt);
        if (purchaseAttachment != null && herbCountAttachment != null &&
            !purchaseAttachment.ContainsPoint(evt.MousePosition) &&
            !herbCountAttachment.ContainsPoint(evt.MousePosition) &&
            purchaseAttachment.AttachmentHolder?.ContainsPoint(evt.MousePosition) == false)
        {
            purchaseAttachment.AttachTo(null);
            herbCountAttachment.AttachTo(null);
        }
    }


    public override void OnActivate()
    {
        base.OnActivate();
        SoundEngine.PlaySound(SoundID.MenuOpen);
    }

    public override void OnDeactivate()
    {
        base.OnDeactivate();
        SoundEngine.PlaySound(SoundID.MenuClose);
    }

    private void RefreshContent()
    {
        if (herbExchange == null || potionExchange == null)
        {
            return;
        }

        RefreshHerbList(herbExchange.Toggle.Toggled);
        RefreshPotionList(potionExchange.Toggle.Toggled);
    }

    private void RefreshHerbList(bool displayLargeSeed)
    {
        herbExchange?.Grid.Clear();
        var items = new List<int>();
        if (displayLargeSeed)
        {
            items.AddRange(HerbologyData.LargeHerbSeedIdByHerbSeedId.Values);
        }
        else
        {
            items.AddRange(HerbologyData.LargeHerbSeedIdByHerbSeedId.Keys);
        }

        herbExchange?.Grid.RemoveAllChildren();
        herbExchange?.Grid.Clear();

        var elements = new List<UIElement>();
        foreach (int itemID in items)
        {
            var herbItem = new ExxoUIItemSlot(TextureAssets.InventoryBack7, itemID);
            herbItem.OnLeftClick += (_, listeningElement) =>
            {
                herbCountAttachment?.AttachTo(listeningElement as ExxoUIItemSlot);
                if (purchaseAttachment == null)
                {
                    return;
                }

                purchaseAttachment.AttachTo(listeningElement as ExxoUIItemSlot);
                purchaseAttachment.NumberInputWithButtons.NumberInput.MaxNumber = herbItem.Item.maxStack;
            };
            herbExchange?.Grid.Append(herbItem);
        }

        //herbExchange.Grid.InnerElement.AddRange(elements);
    }

    private void RefreshPotionList(bool displayElixirs)
    {
        potionExchange?.Grid.Clear();
        var items = new List<int>();
        if (Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().Tier >=
            AvalonHerbologyPlayer.HerbTier.Master)
        {
            items.AddRange(HerbologyData.SuperRestorationIDs);
        }

        if (Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().Tier >=
            AvalonHerbologyPlayer.HerbTier.Expert)
        {
            items.AddRange(HerbologyData.RestorationIDs);
        }

        items.AddRange(displayElixirs ? HerbologyData.ElixirIds : HerbologyData.PotionIds);

        if (Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().Tier >=
            AvalonHerbologyPlayer.HerbTier.Master)
        {
            items.Add(ModContent.ItemType<BlahPotion>());
        }

        potionExchange?.Grid.RemoveAllChildren();
        potionExchange?.Grid.Clear();

        foreach (int itemID in items)
        {
            var potionItem = new ExxoUIItemSlot(TextureAssets.InventoryBack7, itemID);
            potionItem.OnLeftClick += (_, listeningElement) =>
            {
                herbCountAttachment?.AttachTo(null);
                if (purchaseAttachment == null)
                {
                    return;
                }

                purchaseAttachment.AttachTo(listeningElement as ExxoUIItemSlot);
                purchaseAttachment.NumberInputWithButtons.NumberInput.MaxNumber = potionItem.Item.maxStack;
            };
            //if (itemID == ModContent.ItemType<BlahPotion>())
            //{
            //    potionItem.SetImage(TextureAssets.InventoryBack7);
            //}

            potionExchange?.Grid.Append(potionItem);
        }

        //potionExchange.Grid.InnerElement.AddRange(elements);
    }
}
