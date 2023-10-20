using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.UI;

namespace Avalon.UI.PotionSlots
{
    internal class PotionsSlots : ExxoUIPanelWrapper<ExxoUIList>
    {
        public PotionsSlots() : base(new ExxoUIList())
        {
            Height.Set(0, 1);

            InnerElement.Height.Set(0, 1);
            InnerElement.Justification = Justification.Center;
            InnerElement.FitWidthToContent = true;
            InnerElement.ContentHAlign = UIAlign.Center;

            //for (int i = 0; i < 4; i++)
            //{
            //    ItemSlots[i] = new ExxoUIItemSlot(TextureAssets.InventoryBack, ItemID.None);
            //    ItemSlots[i].PaddingTop = 105 + (i % 4) * 34;
            //    ItemSlots[i].PaddingLeft = 572;
            //    ItemSlots[i].Scale = 0.75f;
            //    InnerElement.Append(ItemSlots[i]);
            //    ItemSlots[i].OnLeftClick += delegate
            //    {
            //        if ((Main.mouseItem.type == ItemID.None && ItemSlots[i].Item.type != ItemID.None && ItemSlots[i].Item.stack > 0) ||
            //            (Main.mouseItem.stack >= 1 && Main.mouseItem.IsPotion()))
            //        {
            //            SoundEngine.PlaySound(SoundID.Grab);
            //            (Main.mouseItem, ItemSlots[i].Item) = (ItemSlots[i].Item, Main.mouseItem);
            //            Recipe.FindRecipes();
            //        }
            //    };
            //}
            ItemSlots = new ExxoUIItemSlot(TextureAssets.InventoryBack, ItemID.None);
            ItemSlots.PaddingTop = 105 + 0 * 34;
            ItemSlots.PaddingLeft = 572;
            ItemSlots.Scale = 0.65f;
            InnerElement.Append(ItemSlots);
            ItemSlots.OnLeftClick += delegate
            {
                if ((Main.mouseItem.type == ItemID.None && ItemSlots.Item.type != ItemID.None && ItemSlots.Item.stack > 0) ||
                    (Main.mouseItem.stack >= 1 && Main.mouseItem.IsPotion()))
                {
                    SoundEngine.PlaySound(SoundID.Grab);
                    (Main.mouseItem, ItemSlots.Item) = (ItemSlots.Item, Main.mouseItem);
                    Recipe.FindRecipes();
                }
            };

            ItemSlot2 = new ExxoUIItemSlot(TextureAssets.InventoryBack, ItemID.None);
            ItemSlot2.PaddingTop = 105 + 1 % 4 * 34;
            ItemSlot2.PaddingLeft = 572;
            ItemSlot2.Scale = 0.65f;
            InnerElement.Append(ItemSlot2);
            ItemSlot2.OnLeftClick += delegate
            {
                if ((Main.mouseItem.type == ItemID.None && ItemSlot2.Item.type != ItemID.None && ItemSlot2.Item.stack > 0) ||
                    (Main.mouseItem.stack >= 1 && Main.mouseItem.IsPotion()))
                {
                    SoundEngine.PlaySound(SoundID.Grab);
                    (Main.mouseItem, ItemSlot2.Item) = (ItemSlot2.Item, Main.mouseItem);
                    Recipe.FindRecipes();
                }
            };
            ItemSlot3 = new ExxoUIItemSlot(TextureAssets.InventoryBack, ItemID.None);
            ItemSlot3.PaddingTop = 105 + 3 % 4 * 34;
            ItemSlot3.PaddingLeft = 572;
            ItemSlot3.Scale = 0.65f;
            InnerElement.Append(ItemSlot3);
            ItemSlot3.OnLeftClick += delegate
            {
                if ((Main.mouseItem.type == ItemID.None && ItemSlot3.Item.type != ItemID.None && ItemSlot3.Item.stack > 0) ||
                    (Main.mouseItem.stack >= 1 && Main.mouseItem.IsPotion()))
                {
                    SoundEngine.PlaySound(SoundID.Grab);
                    (Main.mouseItem, ItemSlot3.Item) = (ItemSlot3.Item, Main.mouseItem);
                    Recipe.FindRecipes();
                }
            };

            ItemSlot4 = new ExxoUIItemSlot(TextureAssets.InventoryBack, ItemID.None);
            ItemSlot4.PaddingTop = 105 + 1 % 4 * 34;
            ItemSlot4.PaddingLeft = 572;
            ItemSlot4.Scale = 0.65f;
            InnerElement.Append(ItemSlot4);
            ItemSlot4.OnLeftClick += delegate
            {
                if ((Main.mouseItem.type == ItemID.None && ItemSlot4.Item.type != ItemID.None && ItemSlot4.Item.stack > 0) ||
                    (Main.mouseItem.stack >= 1 && Main.mouseItem.IsPotion()))
                {
                    SoundEngine.PlaySound(SoundID.Grab);
                    (Main.mouseItem, ItemSlot4.Item) = (ItemSlot4.Item, Main.mouseItem);
                    Recipe.FindRecipes();
                }
            };
        }

        public ExxoUIItemSlot ItemSlots;
        public ExxoUIItemSlot ItemSlot2;
        public ExxoUIItemSlot ItemSlot3;
        public ExxoUIItemSlot ItemSlot4;
    }
}
