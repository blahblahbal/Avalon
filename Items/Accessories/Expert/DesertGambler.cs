using Avalon.Buffs;
using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Expert;

class DesertGambler : ModItem
{
    public override void Load()
    {
        if (Main.netMode == NetmodeID.Server)
            return;
        EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Head}", EquipType.Head, this);


    }
    private void SetupDrawing()
    {
        // Since the equipment textures weren't loaded on the server, we can't have this code running server-side
        if (Main.netMode == NetmodeID.Server)
            return;
        int equipSlotHead = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Head);
        ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = true;
        ArmorIDs.Head.Sets.IsTallHat[equipSlotHead] = true;
        ArmorIDs.Head.Sets.DrawHatHair[equipSlotHead] = true;
    }
    public override void SetStaticDefaults()
    {
        SetupDrawing();
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.width = 30;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 2);
        Item.height = 24;
        Item.expert = true;
        Item.accessory = true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().DesertGamblerVisible = !hideVisual;
        player.GetModPlayer<AvalonPlayer>().DesertGambler = true;
        if (player.statLife <= player.statLifeMax2 * 0.33f)
            player.AddBuff(ModContent.BuffType<Deadeye>(), 2);
    }
    public override void UpdateVanity(Player player)
    {
        player.GetModPlayer<AvalonPlayer>().ForceGambler = true;
    }
}
