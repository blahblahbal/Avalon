using System;
using System.Linq;
using Avalon.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Avalon.UI.Herbology;

internal class HerbologyUITurnIn : ExxoUIPanelWrapper<ExxoUIList>
{
    public HerbologyUITurnIn() : base(new ExxoUIList())
    {
        Height.Set(0, 1);

        InnerElement.Height.Set(0, 1);
        InnerElement.Justification = Justification.Center;
        InnerElement.FitWidthToContent = true;
        InnerElement.ContentHAlign = UIAlign.Center;

        Button = new ExxoUIImageButton(ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>("Assets/Textures/UI/HerbButton"))
        {
            Tooltip = Language.GetTextValue("Mods.Avalon.Herbology.ConsumeHerbPotion"),
        };
        InnerElement.Append(Button);

        ItemSlot = new ExxoUIItemSlot(TextureAssets.InventoryBack7, ItemID.None);
        InnerElement.Append(ItemSlot);
        ItemSlot.OnLeftClick += delegate
        {
            if ((Main.mouseItem.type == ItemID.None && ItemSlot.Item.type != ItemID.None && ItemSlot.Item.stack > 0) ||
                (Main.mouseItem.stack >= 1 &&
                 (HerbologyData.LargeHerbIdByLargeHerbSeedId.ContainsValue(Main.mouseItem.type) ||
                  HerbologyData.LargeHerbIdByLargeHerbSeedId.ContainsKey(Main.mouseItem.type) ||
                  HerbologyData.HerbIdByLargeHerbId.ContainsValue(Main.mouseItem.type) ||
                  HerbologyData.PotionIds.Contains(Main.mouseItem.type) ||
                  HerbologyData.ElixirIds.Contains(Main.mouseItem.type))))
            {
                SoundEngine.PlaySound(SoundID.Grab);
                (Main.mouseItem, ItemSlot.Item) = (ItemSlot.Item, Main.mouseItem);
                Recipe.FindRecipes();
            }
        };
    }

    public ExxoUIImageButton Button { get; }
    public ExxoUIItemSlot ItemSlot { get; }

    protected override void UpdateSelf(GameTime gameTime)
    {
        base.UpdateSelf(gameTime);
        Button.LocalScale = 1 + ((1 + (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds)) * 0.15f);
        Button.LocalRotation = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds + 1) * 0.25f;
    }
}
