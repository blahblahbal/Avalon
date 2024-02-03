using System.Collections.Generic;
using Avalon.Common.Players;
using Avalon.Data;
using Avalon.Items.Potions.Buff;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon.UI.Herbology;

public class HerbologyUIState : ExxoUIState
{
    private HerbologyUIHelpAttachment? helpAttachment;
    private ExxoUIImageButtonToggle? helpToggle;
    private HerbologyUIHerbCountAttachment? herbCountAttachment;
    private HerbologyUIHerbExchange? herbExchange;
    private ExxoUIDraggablePanel? mainPanel;
    private HerbologyUIPotionExchange? potionExchange;
    private HerbologyUIPurchaseAttachment? purchaseAttachment;
    private HerbologyUIStats? stats;
    private HerbologyUITurnIn? turnIn;

    // new
    private Vector2 clickDelta;
    private bool isMouseHeld;

    private ExxoUIImageButton? herbButton;
    private ExxoUIImageButton? potionButton;
    // end new
    public override void OnInitialize()
    {
        base.OnInitialize();

        AvalonHerbologyPlayer herbologyPlayer = Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>();

        helpAttachment = new HerbologyUIHelpAttachment();

        mainPanel = new ExxoUIDraggablePanel
        {
            Width = StyleDimension.FromPixels(720),
            Height = StyleDimension.FromPixels(400),
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

        var herbContainer = new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList());
        herbContainer.Width.Set(0, 1);
        herbContainer.InnerElement.Direction = Direction.Horizontal;
        herbContainer.BackgroundColor = Color.Transparent;
        herbContainer.BorderColor = Color.Transparent;
        mainContainer.Append(herbContainer, new ExxoUIList.ElementParams(true, false));

        stats = new HerbologyUIStats();
        herbContainer.InnerElement.Append(stats);

        //turnIn = new HerbologyUITurnIn();
        //herbContainer.InnerElement.Append(turnIn);

        stats.Button.OnLeftClick += delegate
        {
            Item item = stats.ItemSlot.Item;

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

        #region herbs
        herbExchange = new HerbologyUIHerbExchange();
        herbContainer.InnerElement.Append(herbExchange, new ExxoUIList.ElementParams(true, false));

        herbExchange.Toggle.OnToggle += (_, args) => RefreshHerbList(args.Toggled);
        herbExchange.Scrollbar.OnViewPositionChanged += delegate
        {
            purchaseAttachment?.AttachTo(null);
            herbCountAttachment?.AttachTo(null);
        };

        Append(new ExxoUIContentLockPanel(herbExchange.Toggle,
            () => Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().Apprentice,
            Language.GetTextValue("Mods.Avalon.Herbology.ContentLocked.Title") + Language.GetTextValue("Mods.Avalon.Herbology.ContentLocked.Apprentice")));
        #endregion

        #region potion
        potionExchange = new HerbologyUIPotionExchange();
        herbContainer.InnerElement.Append(potionExchange, new ExxoUIList.ElementParams(true, false));

        potionExchange.Toggle.OnToggle += (_, args) => RefreshPotionList(args.Toggled);
        potionExchange.Scrollbar.OnViewPositionChanged += delegate
        {
            purchaseAttachment?.AttachTo(null);
            herbCountAttachment?.AttachTo(null);
        };

        //var potionLock = new ExxoUIContentLockPanel(potionExchange,
        //    () => Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().Tier >=
        //          AvalonHerbologyPlayer.HerbTier.Expert,
        //    Language.GetTextValue("Mods.Avalon.Herbology.ContentLocked.Title") + Language.GetTextValue("Mods.Avalon.Herbology.ContentLocked.Expert"));
        //Append(potionLock);
        //potionLock.OnLockStatusChanged += (_, args) => potionExchange.Scrollbar.Active = !args.Locked;

        potionExchange.Hidden = true;
        #endregion

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

        #region button toggles
        herbButton =
            new ExxoUIImageButton(Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconRandomSeed"))
            { Scale = 1, Tooltip = "Herbs", HAlign = 0.38f, VAlign = 0.02f };
        herbContainer.Append(herbButton);
        herbButton.Selected = true;
        herbButton.OnLeftClick += (_, args) =>
        {
            potionExchange.Hidden = true;
            herbExchange.Hidden = false;
            SoundEngine.PlaySound(SoundID.Grab);
        };

        potionButton =
            new ExxoUIImageButton(ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>("Assets/Textures/UI/HerbPotion"))
            { Scale = 1, Tooltip = "Potions", HAlign = 0.45f, VAlign = 0.04f };
        herbContainer.Append(potionButton);

        potionButton.Hidden = true;

        potionButton.OnLeftClick += (_, args) =>
        {
            potionExchange.Hidden = false;
            herbExchange.Hidden = true;
            SoundEngine.PlaySound(SoundID.Grab);
        };
        #endregion

        herbCountAttachment = new HerbologyUIHerbCountAttachment();
        Append(herbCountAttachment);

        RefreshContent();

        #region old code
        /*base.OnInitialize();

        AvalonHerbologyPlayer herbologyPlayer = Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>();

        helpAttachment = new HerbologyUIHelpAttachment();

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
            Width = StyleDimension.Fill, Height = StyleDimension.Fill, ContentHAlign = UIAlign.Center,
        };
        mainPanel.Append(mainContainer);
        mainPanel.BackgroundColor = Color.Transparent;
        mainPanel.BorderColor = Color.Transparent;

        var titleRow = new ExxoUIList
        {
            Width = StyleDimension.Fill,
            Direction = Direction.Horizontal,
            Justification = Justification.Center,
            FitHeightToContent = true,
            ContentVAlign = UIAlign.Center,
        };
        //mainContainer.Append(titleRow);
        var titleText = new ExxoUITextPanel(Language.GetTextValue("Mods.Avalon.Herbology.BenchName"), 0.8f, true);
        titleRow.Append(titleText);

        helpToggle =
            new ExxoUIImageButtonToggle(Main.Assets.Request<Texture2D>("Images/UI/ButtonRename"),
                Color.White * 0.7f, Color.White) { Scale = 2, Tooltip = Language.GetTextValue("Mods.Avalon.Herbology.Help") };
        titleRow.Append(helpToggle);
        helpToggle.OnToggle += (_, args) =>
        {
            helpAttachment.Enabled = args.Toggled;
            helpToggle.MouseOver(new UIMouseEvent(helpToggle, UserInterface.ActiveInstance.MousePosition));
        };
        helpAttachment.Register(helpToggle,
            Language.GetTextValue("Mods.Avalon.Herbology.HelpToggle"));

        var herbContainer = new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList());
        herbContainer.Width.Set(0, 1);
        herbContainer.InnerElement.Direction = Direction.Horizontal;
        mainContainer.Append(herbContainer, new ExxoUIList.ElementParams(true, false));

        stats = new HerbologyUIStats();
        herbContainer.InnerElement.Append(stats);
        helpAttachment.Register(stats, Language.GetTextValue("Mods.Avalon.Herbology.Stats.Description"));
        helpAttachment.Register(stats.RankTitleText, Language.GetTextValue("Mods.Avalon.Herbology.Stats.RankTitle"));
        helpAttachment.Register(stats.HerbTierText, Language.GetTextValue("Mods.Avalon.Herbology.Stats.CurrentTier"));
        helpAttachment.Register(stats.HerbTotalContainer, Language.GetTextValue("Mods.Avalon.Herbology.Stats.HerbTotal"));
        helpAttachment.Register(stats.PotionTotalContainer,
            Language.GetTextValue("Mods.Avalon.Herbology.Stats.PotionTotal"));

        turnIn = new HerbologyUITurnIn();
        herbContainer.InnerElement.Append(turnIn);
        helpAttachment.Register(turnIn, Language.GetTextValue("Mods.Avalon.Herbology.TurnIn.Description"));
        helpAttachment.Register(turnIn.ItemSlot, Language.GetTextValue("Mods.Avalon.Herbology.TurnIn.ItemSlot"));
        helpAttachment.Register(turnIn.Button, Language.GetTextValue("Mods.Avalon.Herbology.TurnIn.Button"));

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

        herbExchange = new HerbologyUIHerbExchange();
        herbContainer.InnerElement.Append(herbExchange, new ExxoUIList.ElementParams(true, false));
        helpAttachment.Register(herbExchange, Language.GetTextValue("Mods.Avalon.Herbology.HerbExchange.Description"));
        helpAttachment.Register(herbExchange.Toggle, Language.GetTextValue("Mods.Avalon.Herbology.HerbExchange.Toggle"));
        helpAttachment.Register(herbExchange.Grid, Language.GetTextValue("Mods.Avalon.Herbology.HerbExchange.Grid"));

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

        var potionContainer = new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList());
        potionContainer.Width.Set(0, 1);
        potionContainer.InnerElement.Direction = Direction.Horizontal;
        mainContainer.Append(potionContainer, new ExxoUIList.ElementParams(true, false));

        potionExchange = new HerbologyUIPotionExchange();
        potionContainer.InnerElement.Append(potionExchange, new ExxoUIList.ElementParams(true, false));
        helpAttachment.Register(potionExchange, Language.GetTextValue("Mods.Avalon.Herbology.PotionExchange.Description"));
        helpAttachment.Register(potionExchange.Toggle, Language.GetTextValue("Mods.Avalon.Herbology.PotionExchange.Toggle"));
        helpAttachment.Register(potionExchange.Grid, Language.GetTextValue("Mods.Avalon.Herbology.PotionExchange.Grid"));

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

        purchaseAttachment = new HerbologyUIPurchaseAttachment();
        Append(purchaseAttachment);
        helpAttachment.Register(purchaseAttachment.NumberInputWithButtons,
            Language.GetTextValue("Mods.Avalon.Herbology.Purchase.NumberInput"));
        helpAttachment.Register(purchaseAttachment.DifferenceContainer,
            Language.GetTextValue("Mods.Avalon.Herbology.Purchase.Difference"));
        helpAttachment.Register(purchaseAttachment.Button, Language.GetTextValue("Mods.Avalon.Herbology.Purchase.Button"));

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

        herbCountAttachment = new HerbologyUIHerbCountAttachment();
        Append(herbCountAttachment);
        helpAttachment.Register(herbCountAttachment.AttachmentElement,
            Language.GetTextValue("Mods.Avalon.Herbology.HerbCount.Attachment"));

        Append(helpAttachment);

        RefreshContent();*/
        #endregion
    }

    public override void RightDoubleClick(UIMouseEvent evt) => base.RightDoubleClick(evt);

    // if (Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().Tier ==
    //     AvalonHerbologyPlayer.HerbTier.Master)
    // {
    //     Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().Tier =
    //         AvalonHerbologyPlayer.HerbTier.Novice;
    // }
    // else
    // {
    //     Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().Tier++;
    // }
    //
    // RefreshContent();
    public override void OnActivate()
    {
        base.OnActivate();
        Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().UpdateHerbTier();
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
        
        if (isMouseHeld)
        {
            Vector2 mouseDelta = UserInterface.ActiveInstance.MousePosition -
                                 (new Vector2(GetInnerDimensions().X, GetInnerDimensions().Y) + clickDelta);


            Left.Set(Left.Pixels + mouseDelta.X, 0);
            Top.Set(Top.Pixels + mouseDelta.Y, 0);
        }

        Player player = Main.LocalPlayer;
        AvalonHerbologyPlayer modPlayer = player.GetModPlayer<AvalonHerbologyPlayer>();

        if (modPlayer.Expert)
        {
            if (potionButton != null)
            {
                potionButton.Hidden = false;
            }
        }

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

        if (herbExchange != null && potionExchange != null)
        {
            if (potionButton?.ContainsPoint(evt.MousePosition) == true && herbButton != null && potionButton?.Hidden == false)
            {
                herbExchange.Hidden = true;
                potionExchange.Hidden = false;
                potionButton.Selected = true;
                herbButton.Selected = false;
            }
            if (herbButton?.ContainsPoint(evt.MousePosition) == true && potionButton != null)
            {
                herbExchange.Hidden = false;
                potionExchange.Hidden = true;
                herbButton.Selected = true;
                potionButton.Selected = false;
            }
        }
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
    public override void LeftMouseDown(UIMouseEvent evt)
    {
        if (evt.Target is ExxoUIEmpty or Terraria.GameContent.UI.Elements.UIPanel or Terraria.GameContent.UI.Elements.UIText)
        {
            clickDelta = UserInterface.ActiveInstance.MousePosition -
                         new Vector2(GetInnerDimensions().X, GetInnerDimensions().Y);
            isMouseHeld = true;
        }
        base.LeftMouseDown(evt);
    }
    public override void LeftMouseUp(UIMouseEvent evt)
    {
        isMouseHeld = false;
        base.LeftMouseUp(evt);
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
        if (Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().Master)
        {
            items.AddRange(HerbologyData.SuperRestorationIDs);
        }

        if (Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().Expert ||
            Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().Master)
        {
            items.AddRange(HerbologyData.RestorationIDs);
        }

        items.AddRange(displayElixirs ? HerbologyData.ElixirIds : HerbologyData.PotionIds);

        if (Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().Master)
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
