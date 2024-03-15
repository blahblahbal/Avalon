using Avalon.Compatability.Thorium.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Compatability.Thorium.Items.Armor;

[AutoloadEquip(EquipType.Body), ExtendsFromMod("ThoriumMod")]
class ChrysoberylRobe : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("ThoriumMod");
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
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 1;
        Item.rare = ItemRarityID.White;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 1, 40, 0);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Robe)
            .AddIngredient(ModContent.ItemType<Chrysoberyl>(), 10)
            .AddTile(TileID.Loom)
            .Register();
    }
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<ChrysoberylRobe>() && (head.type == ItemID.WizardHat || head.type == ItemID.MagicHat);
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
        player.manaCost -= 0.08f;
    }
}
