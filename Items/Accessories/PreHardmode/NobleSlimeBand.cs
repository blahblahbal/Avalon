using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

class NobleSlimeBand : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = 200000;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.RoyalGel)
            .AddIngredient(ModContent.ItemType<BandofSlime>())
            .AddTile(TileID.TinkerersWorkbench).Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.endurance += 0.05f;
        player.noFallDmg = true;
        player.npcTypeNoAggro[1] = true;
        player.npcTypeNoAggro[16] = true;
        player.npcTypeNoAggro[59] = true;
        player.npcTypeNoAggro[71] = true;
        player.npcTypeNoAggro[81] = true;
        player.npcTypeNoAggro[138] = true;
        player.npcTypeNoAggro[121] = true;
        player.npcTypeNoAggro[122] = true;
        player.npcTypeNoAggro[141] = true;
        player.npcTypeNoAggro[147] = true;
        player.npcTypeNoAggro[183] = true;
        player.npcTypeNoAggro[184] = true;
        player.npcTypeNoAggro[204] = true;
        player.npcTypeNoAggro[225] = true;
        player.npcTypeNoAggro[244] = true;
        player.npcTypeNoAggro[302] = true;
        player.npcTypeNoAggro[333] = true;
        player.npcTypeNoAggro[335] = true;
        player.npcTypeNoAggro[334] = true;
        player.npcTypeNoAggro[336] = true;
        player.npcTypeNoAggro[537] = true;
        player.npcTypeNoAggro[667] = true;
        player.npcTypeNoAggro[676] = true;
        player.npcTypeNoAggro[ModContent.NPCType<NPCs.PreHardmode.OreSlime>()] = true;
        player.npcTypeNoAggro[ModContent.NPCType<NPCs.Hardmode.MineralSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.TinSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.BronzeSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.IronSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.LeadSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.NickelSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.SilverSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.TungstenSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.ZincSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.GoldSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.PlatinumSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.BismuthSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.RhodiumSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.OsmiumSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.IridiumSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.CobaltSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.PalladiumSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.DurantiumSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.MythrilSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.OrichalcumSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.NaquadahSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.AdamantiteSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.TitaniumSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.TroxiniumSlime>()] = true;
        //player.npcTypeNoAggro[ModContent.NPCType<NPCs.DarkMatterSlime>()] = true;
    }
}
