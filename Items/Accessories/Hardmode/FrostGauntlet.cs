using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.HandsOn, EquipType.HandsOff)]
class FrostGauntlet : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Cyan;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 10, 0, 0);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.FireGauntlet)
            .AddIngredient(ItemID.FrozenTurtleShell)
            .AddTile(TileID.TinkerersWorkbench).Register();
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Accessories;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (player.statLife <= player.statLifeMax2 * 0.5)
        {
            player.AddBuff(62, 5, true);
        }
        player.GetModPlayer<AvalonPlayer>().FrostGauntlet = true;
        player.kbGlove = true;
        player.GetAttackSpeed(DamageClass.Melee) += 0.12f;
        player.GetDamage(DamageClass.Melee) += 0.12f;
        player.frostArmor = true;
        player.autoReuseGlove = true;
        player.meleeScaleGlove = true;
    }
}
