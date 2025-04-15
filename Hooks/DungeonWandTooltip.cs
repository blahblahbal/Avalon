using Avalon.Buffs;
using Avalon.Buffs.Debuffs;
using Avalon.Common;
using Avalon.Common.Players;
using Terraria.UI;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;
using Avalon.Items.Tools.PreHardmode;
using Terraria.UI.Chat;
using Terraria.GameContent;
using Microsoft.Xna.Framework;

namespace Avalon.Hooks;

internal class DungeonWandTooltip : ModHook
{
    protected override void Apply()
    {
        On_ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color += On_ItemSlot_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color;
    }

    private void On_ItemSlot_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color(On_ItemSlot.orig_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color orig, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Item[] inv, int context, int slot, Vector2 position, Color lightColor)
    {
        orig.Invoke(spriteBatch, inv, context, slot, position, lightColor);
        int amt = -1;
        if (context == 13)
        {
            if (inv[slot].type == ModContent.ItemType<DungeonWand>())
            {
                amt = 0;
                for (int j = 0; j < 58; j++)
                {
                    if (inv[j].stack > 0 && Data.Sets.ItemSets.DungeonWallItems[inv[j].type])
                    {
                        amt += inv[j].stack;
                    }
                }
            }
        }
        if (amt != -1)
        {
            ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, amt.ToString(), position + new Vector2(8f, 30f) * Main.inventoryScale, Color.White, 0f, Vector2.Zero, new Vector2(Main.inventoryScale * 0.8f), -1f, Main.inventoryScale);
        }
    }
}
