using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

class FlaskOfPathogens : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[2]
        {
            Color.Purple,
            Color.MediumPurple
        };
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.FlaskofCursedFlames);
        Item.buffType = ModContent.BuffType<Buffs.ImbuePathogen>();
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
    }

    public override bool? UseItem(Player player)
    {
        for (int i = 0; i < player.buffType.Length; i++)
        {
            if (BuffID.Sets.IsAFlaskBuff[player.buffType[i]] && player.buffType[i] != ModContent.BuffType<Buffs.ImbuePathogen>())
            {
                player.DelBuff(i);
                break;
            }
        }
        return base.UseItem(player);
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Pathogen>(),2).AddTile(TileID.ImbuingStation).Register();
    }
}
