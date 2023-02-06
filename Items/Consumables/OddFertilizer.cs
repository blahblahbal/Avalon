using ExxoAvalonOrigins.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Consumables;

class OddFertilizer : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 3;
    }

    public override void SetDefaults()
    {
        Item.consumable = true;
        Item.rare = ItemRarityID.LightRed;
        Item.useTime = 45;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.maxStack = 20;
        Item.useAnimation = 45;
        Item.Size = new Vector2(24);
    }

    public override bool CanUseItem(Player player)
    {
        if(!player.ZoneJungle) return false;
        if (NPC.AnyNPCs(NPCID.Plantera)) return false;
        return true;
    }

    public override bool? UseItem(Player player)
    {
        SoundEngine.PlaySound(SoundID.Roar, player.position);
        NPC.SpawnOnPlayer(player.whoAmI, NPCID.Plantera);
        return true;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<LifeDew>(), 5)
            .AddIngredient(ItemID.Stinger, 15)
            .AddIngredient(ItemID.JungleSpores, 15)
            .AddIngredient(ItemID.SoulofMight, 5)
            .AddIngredient(ItemID.SoulofFright, 5)
            .AddTile(TileID.MythrilAnvil);
    }
}
