using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class BestialBand : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 2;
        Item.rare = ItemRarityID.Cyan;
        Item.width = 30;
        Item.value = Item.sellPrice(0, 10);
        Item.accessory = true;
        Item.height = 36;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.CelestialShell)
            .AddIngredient(ModContent.ItemType<HadesCross>())
            .AddTile(TileID.TinkerersWorkbench).Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (hideVisual)
        {
            player.hideMerman = true;
            player.hideWolf = true;
            player.GetModPlayer<AvalonPlayer>().HideVarefolk = true;
        }
        player.GetModPlayer<AvalonPlayer>().AccLavaMerman = true;
        player.fireWalk = true;
        player.ignoreWater = true;
        player.accMerman = true;
        player.accFlipper = true;
        player.wolfAcc = true;
        player.lavaImmune = true;
        player.waterWalk = true;
        player.lifeRegen += 2;
        player.statDefense += 4;
        player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
        player.GetDamage(DamageClass.Generic) += 0.1f;
        player.GetCritChance(DamageClass.Generic) += 2;
        player.pickSpeed -= 0.15f;
        player.GetKnockback(DamageClass.Summon) += 0.5f;
    }
}
