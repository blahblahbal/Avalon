using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Items.Consumables;

class FalseTreasureMap : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 3;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.consumable = true;
        Item.rare = ItemRarityID.White;
        Item.width = dims.Width;
        Item.useTime = 45;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.value = 0;
        Item.maxStack = 9999;
        Item.useAnimation = 45;
        Item.height = dims.Height;
    }

    public override bool CanUseItem(Player player)
    {
        return Main.invasionSize > 0 && Main.invasionType == InvasionID.PirateInvasion;
    }

    public override bool? UseItem(Player player)
    {
        Main.invasionSize = 0;
        if (ExxoAvalonOrigins.Achievements != null)
        {
            ExxoAvalonOrigins.Achievements.Call("Event", "FalseAlarm");
        }
        SoundEngine.PlaySound(SoundID.Roar, player.position);
        return true;
    }
}
