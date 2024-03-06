using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

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
        Item.maxStack = 9999;
        Item.useAnimation = 45;
        Item.Size = new Vector2(24);
    }

    public override bool CanUseItem(Player player)
    {
        return player.ZoneJungle && !NPC.AnyNPCs(NPCID.Plantera);
    }

    public override bool? UseItem(Player player)
    {
        SoundEngine.PlaySound(SoundID.Roar, player.position);
        NPC.SpawnOnPlayer(player.whoAmI, NPCID.Plantera);
        return true;
    }
    // do not add souls of sight 
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<LifeDew>(), 5)
            .AddIngredient(ItemID.Stinger, 15)
            .AddIngredient(ItemID.JungleSpores, 15)
            .AddIngredient(ItemID.SoulofMight, 5)
            .AddIngredient(ItemID.SoulofFright, 5)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
