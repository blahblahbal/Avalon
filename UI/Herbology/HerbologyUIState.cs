using System.Collections.Generic;
using ExxoAvalonOrigins.Common;
using ExxoAvalonOrigins.Data;
using ExxoAvalonOrigins.Items.Potions.Buff;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace ExxoAvalonOrigins.UI.Herbology;

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

    public override void OnInitialize()
    {
        base.OnInitialize();

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

        var titleRow = new ExxoUIList
        {
            Width = StyleDimension.Fill,
            Direction = Direction.Horizontal,
            Justification = Justification.Center,
            FitHeightToContent = true,
            ContentVAlign = UIAlign.Center,
        };
        mainContainer.Append(titleRow);
        var titleText = new ExxoUITextPanel("Herbology Bench", 0.8f, true);
        titleRow.Append(titleText);

        helpToggle =
            new ExxoUIImageButtonToggle(Main.Assets.Request<Texture2D>("Images/UI/ButtonRename"),
                Color.White * 0.7f, Color.White) { Scale = 2, Tooltip = "Help" };
        titleRow.Append(helpToggle);
        helpToggle.OnToggle += (_, args) =>
        {
            helpAttachment.Enabled = args.Toggled;
            helpToggle.MouseOver(new UIMouseEvent(helpToggle, UserInterface.ActiveInstance.MousePosition));
        };
        helpAttachment.Register(helpToggle,
            "When this button is active, hovering over elements provides a description of their purpose");

        var herbContainer = new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList());
        herbContainer.Width.Set(0, 1);
        herbContainer.InnerElement.Direction = Direction.Horizontal;
        mainContainer.Append(herbContainer, new ExxoUIList.ElementParams(true, false));

        stats = new HerbologyUIStats();
        herbContainer.InnerElement.Append(stats);
        helpAttachment.Register(stats, "A list of herbology stats relating to the player");
        helpAttachment.Register(stats.RankTitleText, "Title of the current herbology tier");
        helpAttachment.Register(stats.HerbTierText, "Current herbology tier");
        helpAttachment.Register(stats.HerbTotalContainer, "Herb credits used to purchase seeds in the herb exchange");
        helpAttachment.Register(stats.PotionTotalContainer,
            "Potion credits used to purchase potions and elixirs in the potion exchange");

        turnIn = new HerbologyUITurnIn();
        herbContainer.InnerElement.Append(turnIn);
        helpAttachment.Register(turnIn, "Turn in herbs and potions in exchange for credits");
        helpAttachment.Register(turnIn.ItemSlot, "Place items here to be exchanged for credits");
        helpAttachment.Register(turnIn.Button, "Converts the current items in the item slot into credits");

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
        helpAttachment.Register(herbExchange, "Purchase herbs - each herb's exchange unlocks after 50 collected");
        helpAttachment.Register(herbExchange.Toggle, "Toggle listing between seeds and large seeds");
        helpAttachment.Register(herbExchange.Grid, "Select an item to purchase");

        herbExchange.Toggle.OnToggle += (_, args) => RefreshHerbList(args.Toggled);
        herbExchange.Scrollbar.OnViewPositionChanged += delegate
        {
            purchaseAttachment?.AttachTo(null);
            herbCountAttachment?.AttachTo(null);
        };

        Append(new ExxoUIContentLockPanel(herbExchange.Toggle,
            () => Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().Tier >=
                  AvalonHerbologyPlayer.HerbTier.Apprentice,
            $"Content locked: Must be Herbology {AvalonHerbologyPlayer.HerbTier.Apprentice}"));

        var potionContainer = new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList());
        potionContainer.Width.Set(0, 1);
        potionContainer.InnerElement.Direction = Direction.Horizontal;
        mainContainer.Append(potionContainer, new ExxoUIList.ElementParams(true, false));

        potionExchange = new HerbologyUIPotionExchange();
        potionContainer.InnerElement.Append(potionExchange, new ExxoUIList.ElementParams(true, false));
        helpAttachment.Register(potionExchange, "Purchase potions using potion credits");
        helpAttachment.Register(potionExchange.Toggle, "Toggle listing between potions and elixirs");
        helpAttachment.Register(potionExchange.Grid, "Select an item to purchase");

        potionExchange.Toggle.OnToggle += (_, args) => RefreshPotionList(args.Toggled);
        potionExchange.Scrollbar.OnViewPositionChanged += delegate
        {
            purchaseAttachment?.AttachTo(null);
            herbCountAttachment?.AttachTo(null);
        };

        var potionLock = new ExxoUIContentLockPanel(potionExchange,
            () => Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().Tier >=
                  AvalonHerbologyPlayer.HerbTier.Expert,
            $"Content locked: Must be Herbology {AvalonHerbologyPlayer.HerbTier.Expert}");
        Append(potionLock);
        potionLock.OnLockStatusChanged += (_, args) => potionExchange.Scrollbar.Active = !args.Locked;

        purchaseAttachment = new HerbologyUIPurchaseAttachment();
        Append(purchaseAttachment);
        helpAttachment.Register(purchaseAttachment.NumberInputWithButtons,
            "Select amount of the selected item to purchase");
        helpAttachment.Register(purchaseAttachment.DifferenceContainer,
            "How the following purchase will affect your token balance");
        helpAttachment.Register(purchaseAttachment.Button, "Click to purchase items");

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
            "The amount of herbs of that type available, needed to purchase large herb seeds");

        Append(helpAttachment);

        RefreshContent();
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
