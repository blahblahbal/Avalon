using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Head)]
public class ResistantWoodHelmet : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Headgear;
    }
    public override void SetDefaults() 
    {
        Item.defense = 3;
        Item.Size = new Vector2(16);
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<ResistantWood>(), 25).AddTile(TileID.WorkBenches).Register();
    }
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<ResistantWoodBreastplate>() && legs.type == ModContent.ItemType<ResistantWoodGreaves>();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "10% increased armor penetration and damage reduction";
        player.endurance += 0.1f;
        player.GetArmorPenetration(DamageClass.Generic) += 0.1f;
    }
}
