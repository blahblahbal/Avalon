using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Avalon.Items.Material;
using System.Collections.Generic;

namespace Avalon.Items.Accessories.Hardmode;

class LightninginaBottle : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 3);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().LightningInABottle = true;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Bottle)
            .AddIngredient(ModContent.ItemType<Material.Shards.TornadoShard>(), 4)
            .AddIngredient(ModContent.ItemType<Material.LivingLightningBlock>(), 30)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
public class AngryNimbusDropsHook : ModHook
{
    protected override void Apply()
    {
        On_ItemDropDatabase.RegisterToNPC += On_ItemDropDatabase_RegisterToNPC;
    }

    private IItemDropRule On_ItemDropDatabase_RegisterToNPC(On_ItemDropDatabase.orig_RegisterToNPC orig, ItemDropDatabase self, int type, IItemDropRule entry)
    {
        if (type == NPCID.AngryNimbus)
        {
            entry = new AllFromOptionsDropRule(ModContent.ItemType<LivingLightningBlock>(), ItemID.Cloud, ItemID.RainCloud, ItemID.SnowCloudBlock);
        }
        return orig.Invoke(self, type, entry);
    }
}
public class AllFromOptionsDropRule : IItemDropRule
{
    public int[] dropIds;
    public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

    public AllFromOptionsDropRule(params int[] options)
    {
        dropIds = options;
        ChainedRules = new List<IItemDropRuleChainAttempt>();
    }

    public bool CanDrop(DropAttemptInfo info)
    {
        return true;
    }

    public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
    {
        float num = 1f;
        float num2 = num * ratesInfo.parentDroprateChance;
        float dropRate = 1f / ((float)dropIds.Length + 3.83f) * num2;
        for (int i = 0; i < dropIds.Length; i++)
        {
            drops.Add(new DropRateInfo(dropIds[i], 1, 1, dropRate, ratesInfo.conditions));
        }
        Chains.ReportDroprates(ChainedRules, num, drops, ratesInfo);
    }

    public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
    {
        int amount = 3;
        for (int i = 0; i < amount; i++)
        {
            int stack = Main.rand.Next(8, 16);
            if (Main.rand.NextBool(3))
            {
                stack += Main.rand.Next(1, 5);
            }
            CommonCode.DropItem(info, dropIds[i], stack);
        }
        ItemDropAttemptResult result = default(ItemDropAttemptResult);
        result.State = ItemDropAttemptResultState.Success;
        return result;
    }
}
