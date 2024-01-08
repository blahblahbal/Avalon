using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Body)]
class TourmalineRobe : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 1;
        Item.rare = ItemRarityID.White;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 75, 0);
        Item.height = dims.Height;
    }
    public override void Load()
    {
        if (Main.netMode == NetmodeID.Server) return;
        EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Legs}", EquipType.Legs, this);
    }
    public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
    {
        robes = true;
        equipSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Legs);
    }
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<TourmalineRobe>() && (head.type == ItemID.WizardHat || head.type == ItemID.MagicHat);
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.Robe).AddIngredient(ModContent.ItemType<Material.Ores.Tourmaline>(), 10).AddTile(TileID.Loom).Register();
    }
    public override void UpdateArmorSet(Player player)
    {
        if (player.head == 14)
        {
            player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Robe1");
            player.GetCritChance(DamageClass.Magic) += 10;
        }
        else if (player.head == 159)
        {
            player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Robe2");
            player.statManaMax2 += 60;
        }
    }
    public override void UpdateEquip(Player player)
    {
        player.statManaMax2 += 40;
        player.manaCost -= 0.8f;
    }
}
