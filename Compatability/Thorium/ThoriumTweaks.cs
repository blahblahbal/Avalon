using Avalon.Items.Consumables;
using Avalon.Items.Material;
using Avalon.Items.Material.Herbs;
using Avalon.Items.Material.Ores;
using Avalon.Items.Material.Shards;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.NPCs;
using ThoriumMod.NPCs.Depths;

namespace Avalon.Compatability.Thorium;

public class ThoriumTweaksGlobalNPC : GlobalNPC
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("ThoriumMod");
    }
    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        // undead
        if (npc.type == ModContent.NPCType<AncientArcher>() || npc.type == ModContent.NPCType<AncientCharger>() ||
            npc.type == ModContent.NPCType<AncientPhalanx>() || npc.type == ModContent.NPCType<BigBone>() ||
            npc.type == ModContent.NPCType<Biter>() || npc.type == ModContent.NPCType<Shambler>() ||
            npc.type == ModContent.NPCType<ThoriumMod.NPCs.BloodMoon.Abomination>())
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<UndeadShard>(), 11));
        }
        // frost shards
        if (npc.type == ModContent.NPCType<Coolmera>() || npc.type == ModContent.NPCType<SnowEater>() ||
            npc.type == ModContent.NPCType<FrozenFace>() || npc.type == ModContent.NPCType<FrostWurmHead>() ||
            npc.type == ModContent.NPCType<SnowBall>() || npc.type == ModContent.NPCType<ChilledSpitter>() ||
            npc.type == ModContent.NPCType<Coldling>() || npc.type == ModContent.NPCType<Freezer>() ||
            npc.type == ModContent.NPCType<FrostFang>() || npc.type == ModContent.NPCType<FrostFangWall>() ||
            npc.type == ModContent.NPCType<FrozenGross>() || npc.type == ModContent.NPCType<SnowFlinxMatriarch>() ||
            npc.type == ModContent.NPCType<ThoriumMod.NPCs.Blizzard.BlizzardBat>() ||
            npc.type == ModContent.NPCType<ThoriumMod.NPCs.Blizzard.FrostBurnt>() ||
            npc.type == ModContent.NPCType<ThoriumMod.NPCs.Blizzard.SnowElemental>() ||
            npc.type == ModContent.NPCType<ThoriumMod.NPCs.Blizzard.SnowyOwl>())
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FrostShard>(), 10));
        }
        // breeze shards
        if (npc.type == ModContent.NPCType<Coolmera>() || npc.type == ModContent.NPCType<SnowEater>() ||
            npc.type == ModContent.NPCType<WindElemental>() ||
            npc.type == ModContent.NPCType<ThoriumMod.NPCs.Blizzard.BlizzardBat>())
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BreezeShard>(), 13));
        }
        // fire shards
        if (npc.type == ModContent.NPCType<BoneFlayer>() || npc.type == ModContent.NPCType<EpiDermon>() ||
            npc.type == ModContent.NPCType<SoulCorrupter>() || npc.type == ModContent.NPCType<FlamekinCaster>() ||
            npc.type == ModContent.NPCType<HellBringerMimic>() || npc.type == ModContent.NPCType<InfernalHound>() ||
            npc.type == ModContent.NPCType<MoltenMortar>() || npc.type == ModContent.NPCType<UnderworldPot1>() ||
            npc.type == ModContent.NPCType<UnderworldPot2>())
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FireShard>(), 8));
        }
        // earth shards
        if (npc.type == ModContent.NPCType<EarthenBat>() || npc.type == ModContent.NPCType<EarthenGolem>() ||
            npc.type == ModContent.NPCType<BlackWidow>() || npc.type == ModContent.NPCType<BlackWidowWall>() ||
            npc.type == ModContent.NPCType<BrownRecluse>() || npc.type == ModContent.NPCType<BrownRecluseWall>() ||
            npc.type == ModContent.NPCType<Tarantula>() || npc.type == ModContent.NPCType<TarantulaWall>())
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EarthShard>(), 12));
        }
        // evil shards
        if (npc.type == ModContent.NPCType<Coolmera>() || npc.type == ModContent.NPCType<SnowEater>() ||
            npc.type == ModContent.NPCType<FrozenFace>() || npc.type == ModContent.NPCType<FrostWurmHead>() ||
            npc.type == ModContent.NPCType<TheInnocent>() || npc.type == ModContent.NPCType<ChilledSpitter>() ||
            npc.type == ModContent.NPCType<Coldling>() || npc.type == ModContent.NPCType<Freezer>() ||
            npc.type == ModContent.NPCType<FrozenGross>() || npc.type == ModContent.NPCType<TheStarved>() ||
            npc.type == ModContent.NPCType<VileFloater>() || npc.type == ModContent.NPCType<SoulCorrupter>() ||
            npc.type == ModContent.NPCType<EpiDermon>())
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CorruptShard>(), 9));
        }
        // water shards
        if (npc.type == ModContent.NPCType<Barracuda>() || npc.type == ModContent.NPCType<Blowfish>() ||
            npc.type == ModContent.NPCType<Globee>() || npc.type == ModContent.NPCType<ManofWar>() ||
            npc.type == ModContent.NPCType<Octopus>() || npc.type == ModContent.NPCType<GigaClam>() ||
            npc.type == ModContent.NPCType<Hammerhead>() || npc.type == ModContent.NPCType<Sharptooth>() ||
            npc.type == ModContent.NPCType<AbyssalAngler>() || npc.type == ModContent.NPCType<AbyssalAngler2>() ||
            npc.type == ModContent.NPCType<Blobfish>() || npc.type == ModContent.NPCType<CrownofThorns>() ||
            npc.type == ModContent.NPCType<Kraken>() || npc.type == ModContent.NPCType<PutridSerpent>() ||
            npc.type == ModContent.NPCType<VampireSquid>() || npc.type == ModContent.NPCType<VoltEelHead>() ||
            npc.type == ModContent.NPCType<Whale>() || npc.type == ModContent.NPCType<SubmergedMimic>())
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WaterShard>(), 8));
        }
        // toxin shards
        if (npc.type == ModContent.NPCType<MahoganyEnt>() || npc.type == ModContent.NPCType<MudMan>() ||
            npc.type == ModContent.NPCType<StrangeBulb>() || npc.type == ModContent.NPCType<MossWasp>() ||
            npc.type == ModContent.NPCType<SunPriestess>() || npc.type == ModContent.NPCType<ArmyAnt>())
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ToxinShard>(), 8));
        }
        // arcane shards
        if (npc.type == ModContent.NPCType<DissonanceSeer>() || npc.type == ModContent.NPCType<Spectrumite>())
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ArcaneShard>(), 7));
        }
    }
    public override void ModifyShop(NPCShop shop)
    {
        if (shop.NpcType == ModContent.NPCType<Druid>())
        {
            shop.Add(new Item(ModContent.ItemType<BarfbushSeeds>())
            {
                shopCustomPrice = Item.buyPrice(silver: 10),
            });
            shop.Add(new Item(ModContent.ItemType<BloodberrySeeds>())
            {
                shopCustomPrice = Item.buyPrice(silver: 10),
            });
            shop.Add(new Item(ModContent.ItemType<SweetstemSeeds>())
            {
                shopCustomPrice = Item.buyPrice(silver: 10),
            });
            // add twilight plume later
            /*
            shop.Add(new Item(ModContent.ItemType<TwilightPlumeSeeds>())
            {
                shopCustomPrice = Item.buyPrice(silver: 10),
            });
             */
            if (Main.hardMode)
            {
                shop.Add(new Item(ModContent.ItemType<HolybirdSeeds>())
                {
                    shopCustomPrice = Item.buyPrice(silver: 20),
                });
            }
        }
        if (shop.NpcType == ModContent.NPCType<ConfusedZombie>())
        {
            shop.Add(new Item(ModContent.ItemType<YuckyBit>())
            {
                shopCustomPrice = Item.buyPrice(0, 0, 7, 50),
            });
            if (Main.hardMode)
            {
                shop.Add(new Item(ModContent.ItemType<InfestedCarcass>())
                {
                    shopCustomPrice = Item.buyPrice(0, 10),
                });
            }
        }
        if (shop.NpcType == ModContent.NPCType<Blacksmith>() && Main.hardMode)
        {
            shop.Add(new Item(ModContent.ItemType<BronzeOre>())
            {
                shopCustomPrice = Item.buyPrice(0, 0, 1, 75),
            });
            shop.Add(new Item(ModContent.ItemType<NickelOre>())
            {
                shopCustomPrice = Item.buyPrice(0, 0, 3, 50),
            });
            shop.Add(new Item(ModContent.ItemType<ZincOre>())
            {
                shopCustomPrice = Item.buyPrice(0, 0, 7),
            });
            shop.Add(new Item(ModContent.ItemType<BismuthOre>())
            {
                shopCustomPrice = Item.buyPrice(0, 0, 14),
            });
            if (NPC.downedMechBossAny)
            {
                shop.Add(new Item(ModContent.ItemType<BacciliteOre>())
                {
                    shopCustomPrice = Item.buyPrice(0, 0, 20),
                });
            }
        }
    }
}
public class ThoriumTweaksRecipeSystem : ModSystem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("ThoriumMod");
    }
    public override void AddRecipes()
    {
        Recipe.Create(ModContent.ItemType<ThoriumMod.Items.Misc.EnchantedPickaxe>())
            .AddIngredient(ModContent.ItemType<Avalon.Items.Material.Bars.EnchantedBar>(), 8)
            .AddRecipeGroup("Avalon:GoldPickaxe")
            .AddTile(TileID.Anvils)
            .Register();

        Recipe.Create(ModContent.ItemType<ThoriumMod.Items.ThrownItems.EnchantedKnife>(), 125)
            .AddIngredient(ModContent.ItemType<Avalon.Items.Material.Bars.EnchantedBar>(), 2)
            .AddTile(TileID.Anvils)
            .Register();

        Recipe.Create(ModContent.ItemType<ThoriumMod.Items.SummonItems.EnchantedCane>())
            .AddIngredient(ModContent.ItemType<Avalon.Items.Material.Bars.EnchantedBar>(), 7)
            .AddTile(TileID.Anvils)
            .Register();

        Recipe.Create(ModContent.ItemType<ThoriumMod.Items.MagicItems.EnchantedStaff>())
            .AddIngredient(ModContent.ItemType<Avalon.Items.Material.Bars.EnchantedBar>(), 6)
            .AddTile(TileID.Anvils)
            .Register();
    }
    public override void PostAddRecipes()
    {
        for (int i = 0; i < Recipe.numRecipes; i++)
        { 
            Recipe recipe = Main.recipe[i];

            if (recipe.createItem.type == ModContent.ItemType<ThoriumMod.Items.MagicItems.ChlorophyteStaff>())
            {
                recipe.AddIngredient(ModContent.ItemType<VenomShard>());
            }
            if (recipe.createItem.type == ModContent.ItemType<ThoriumMod.Items.BardItems.TheGreenTambourine>())
            {
                recipe.AddIngredient(ModContent.ItemType<VenomShard>());
            }
            if (recipe.createItem.type == ModContent.ItemType<ThoriumMod.Items.ThrownItems.TechniqueCobraBite>())
            {
                recipe.AddIngredient(ModContent.ItemType<VenomShard>());
            }
        }
    }
}
