using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Head)]
class ViruthornHelmet : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 6;
        Item.rare = ItemRarityID.Blue;
        Item.width = 18;
        Item.height = 18;
        Item.value = Item.sellPrice(0, 0, 54, 0);
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.Bars.BacciliteBar>(), 15)
            .AddIngredient(ModContent.ItemType<Material.Booger>(), 8)
            .AddTile(TileID.Anvils)
            .Register();
    }
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<ViruthornScalemail>() && legs.type == ModContent.ItemType<ViruthornGreaves>();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Viruthorn");
        player.GetCritChance(DamageClass.Generic) += 10;
    }
    public override void UpdateVanitySet(Player player)
    {
        if (Main.rand.NextBool(7))
        {
            Dust d = Dust.NewDustDirect(player.position, player.width, player.height, ModContent.DustType<ContagionWeapons>());
            d.noGravity = true;
            d.scale *= 0.1f;
            d.alpha = 128;
            d.velocity *= 0.1f;
            d.fadeIn = 1;
            d.velocity += player.velocity + new Vector2(0,0.4f);
        }
    }
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Generic) += 0.03f;
    }
}
