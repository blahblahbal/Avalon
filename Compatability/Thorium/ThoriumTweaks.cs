using Avalon.Compatability.Thorium.Items.Placeable.Tile;
using Avalon.Items.Consumables;
using Avalon.Items.Material;
using Avalon.Items.Material.Herbs;
using Avalon.Items.Material.Ores;
using Avalon.Items.Material.Shards;
using Avalon.Items.Placeable.Tile;
using Avalon.Items.Potions.Other;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Consumable;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.Thorium;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.NPCs;
using ThoriumMod.NPCs.Depths;

namespace Avalon.Compatability.Thorium;

[ExtendsFromMod("ThoriumMod")]
public class ThoriumHerbologyAdditions
{
    public static void Initialize()
    {
        // add herbs and herb seeds
        Data.HerbologyData.HerbIdByLargeHerbId.Add(ModContent.ItemType<LargeMarineKelp>(), ModContent.ItemType<MarineKelp>());
        Data.HerbologyData.LargeHerbIdByLargeHerbSeedId.Add(ModContent.ItemType<LargeMarineKelpSeed>(), ModContent.ItemType<LargeMarineKelp>());
        Data.HerbologyData.LargeHerbSeedIdByHerbId.Add(ModContent.ItemType<MarineKelp>(), ModContent.ItemType<LargeMarineKelpSeed>());
        Data.HerbologyData.LargeHerbSeedIdByHerbSeedId.Add(ModContent.ItemType<MarineKelpSeeds>(), ModContent.ItemType<LargeMarineKelpSeed>());

        // add potions
        List<int> potions = new()
        {
            ModContent.ItemType<AquaPotion>(), ModContent.ItemType<ArcanePotion>(), ModContent.ItemType<ArtilleryPotion>(),
            ModContent.ItemType<AssassinPotion>(), ModContent.ItemType<BloodPotion>(), ModContent.ItemType<BouncingFlamePotion>(),
            ModContent.ItemType<ConflagrationPotion>(), ModContent.ItemType<CreativityPotion>(), ModContent.ItemType<EarwormPotion>(),
            ModContent.ItemType<FrenzyPotion>(), ModContent.ItemType<GlowingPotion>(), ModContent.ItemType<HolyPotion>(),
            ModContent.ItemType<HydrationPotion>(), ModContent.ItemType<InspirationReachPotion>(), ModContent.ItemType<KineticPotion>(),
            ModContent.ItemType<WarmongerPotion>()
        };
        Data.HerbologyData.PotionIds.AddRange(potions);

    }
}

[ExtendsFromMod("ThoriumMod")]
public class ThoriumTweaksPlayer : ModPlayer
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("ThoriumMod");
    }

    public override void PreUpdateBuffs()
    {
        if (Player.HasBuff(ModContent.BuffType<ThoriumMod.Buffs.SkeletonRepellentBuff>()))
        {
            Player.npcTypeNoAggro[ModContent.NPCType<NPCs.Hardmode.IrateBones>()] = true;
        }
        if (Player.HasBuff(ModContent.BuffType<ThoriumMod.Buffs.ZombieRepellentBuff>()))
        {
            Player.npcTypeNoAggro[ModContent.NPCType<NPCs.PreHardmode.FallenHero>()] = true;
        }
        if (Player.HasBuff(ModContent.BuffType<ThoriumMod.Buffs.InsectRepellentBuff>()))
        {
            Player.npcTypeNoAggro[ModContent.NPCType<NPCs.PreHardmode.Mosquito>()] = true;
            Player.npcTypeNoAggro[ModContent.NPCType<NPCs.PreHardmode.MosquitoDroopy>()] = true;
            Player.npcTypeNoAggro[ModContent.NPCType<NPCs.PreHardmode.MosquitoPainted>()] = true;
            Player.npcTypeNoAggro[ModContent.NPCType<NPCs.PreHardmode.MosquitoSmall>()] = true;
        }
        if (Player.HasBuff(ModContent.BuffType<ThoriumMod.Buffs.FishRepellentBuff>()))
        {
            Player.npcTypeNoAggro[ModContent.NPCType<NPCs.Critters.ContaminatedGoldfish>()] = true;
            Player.npcTypeNoAggro[ModContent.NPCType<NPCs.PreHardmode.RedArowana>()] = true;
            Player.npcTypeNoAggro[ModContent.NPCType<NPCs.PreHardmode.RedArowana2>()] = true;
        }
    }
}

[ExtendsFromMod("ThoriumMod")]
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
[ExtendsFromMod("ThoriumMod")]
public class ThoriumTweaksRecipeSystem : ModSystem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("ThoriumMod");
    }
    public override void AddRecipeGroups()
    {
        string any = Language.GetTextValue("LegacyMisc.37");

        var groupLifeCrystalOre = new RecipeGroup(() => $"{any} Life Crystal Ore", new int[]
        {
            ModContent.ItemType<LifeQuartz>(),
            ModContent.ItemType<Heartstone>()
        });
        RecipeGroup.RegisterGroup("Avalon:LifeCrystalOre", groupLifeCrystalOre);
    }
    public override void AddRecipes()
    {
        // Enchanted items
        Recipe.Create(ModContent.ItemType<EnchantedPickaxe>())
            .AddIngredient(ModContent.ItemType<Avalon.Items.Material.Bars.EnchantedBar>(), 8)
            .AddRecipeGroup("Avalon:GoldPickaxe")
            .AddTile(TileID.Anvils)
            .Register();

        Recipe.Create(ModContent.ItemType<EnchantedKnife>(), 125)
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
        // End Enchanted items

        // EGG
        Recipe.Create(ModContent.ItemType<FabergeEgg>())
            .AddIngredient(ItemID.Cloud, 25)
            .AddIngredient(ModContent.ItemType<MoonplateBlock>(), 10)
            .AddTile(TileID.SkyMill)
            .Register();

        Recipe.Create(ModContent.ItemType<FabergeEgg>())
            .AddIngredient(ItemID.Cloud, 25)
            .AddIngredient(ModContent.ItemType<DuskplateBlock>(), 10)
            .AddTile(TileID.SkyMill)
            .Register();
        // END EGG

        Recipe.Create(ModContent.ItemType<LifeQuartz>())
            .AddIngredient(ModContent.ItemType<Heartstone>())
            .AddTile(TileID.Furnaces)
            .Register();

        Recipe.Create(ModContent.ItemType<Heartstone>())
            .AddIngredient(ModContent.ItemType<LifeQuartz>())
            .AddTile(TileID.Furnaces)
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
            if (recipe.createItem.type == ModContent.ItemType<TheGreenTambourine>())
            {
                recipe.AddIngredient(ModContent.ItemType<VenomShard>());
            }
            if (recipe.createItem.type == ModContent.ItemType<TechniqueCobraBite>())
            {
                recipe.AddIngredient(ModContent.ItemType<VenomShard>());
            }

            #region life quartz/heartstone
            if (recipe.createItem.type == ModContent.ItemType<LifeQuartz>())
            {
                if (recipe.TryGetIngredient(ItemID.LifeCrystal, out Item ing))
                {
                    recipe.DisableRecipe();
                }
            }
            if (recipe.createItem.type == ItemID.LifeCrystal)
            {
                if (recipe.TryGetIngredient(ModContent.ItemType<LifeQuartz>(), out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddRecipeGroup("Avalon:LifeCrystalOre", 45);
                }
                if (recipe.TryGetIngredient(ModContent.ItemType<Heartstone>(), out Item ing2))
                {
                    recipe.RemoveIngredient(ing2);
                    recipe.AddRecipeGroup("Avalon:LifeCrystalOre", 45);
                }
            }
            if (recipe.createItem.type == ModContent.ItemType<LifeGem>())
            {
                if (recipe.TryGetIngredient(ModContent.ItemType<LifeQuartz>(), out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddRecipeGroup("Avalon:LifeCrystalOre", 15);
                }
            }
            if (recipe.createItem.type == ModContent.ItemType<AphrodisiacVial>())
            {
                if (recipe.TryGetIngredient(ModContent.ItemType<LifeQuartz>(), out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddRecipeGroup("Avalon:LifeCrystalOre");
                }
            }
            if (recipe.createItem.type == ModContent.ItemType<TemplarsGrace>())
            {
                if (recipe.TryGetIngredient(ModContent.ItemType<LifeQuartz>(), out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddRecipeGroup("Avalon:LifeCrystalOre", 4);
                }
            }
            if (recipe.createItem.type == ModContent.ItemType<Prophecy>())
            {
                if (recipe.TryGetIngredient(ModContent.ItemType<LifeQuartz>(), out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddRecipeGroup("Avalon:LifeCrystalOre", 5);
                }
            }
            if (recipe.createItem.type == ModContent.ItemType<LifeQuartzClaymore>())
            {
                if (recipe.TryGetIngredient(ModContent.ItemType<LifeQuartz>(), out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddRecipeGroup("Avalon:LifeCrystalOre", 10);
                }
            }
            if (recipe.createItem.type == ModContent.ItemType<TemplarJudgment>())
            {
                if (recipe.TryGetIngredient(ModContent.ItemType<LifeQuartz>(), out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddRecipeGroup("Avalon:LifeCrystalOre", 4);
                }
            }
            if (recipe.createItem.type == ModContent.ItemType<TemplarsCirclet>())
            {
                if (recipe.TryGetIngredient(ModContent.ItemType<LifeQuartz>(), out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddRecipeGroup("Avalon:LifeCrystalOre", 3);
                }
            }
            if (recipe.createItem.type == ModContent.ItemType<TemplarsTabard>())
            {
                if (recipe.TryGetIngredient(ModContent.ItemType<LifeQuartz>(), out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddRecipeGroup("Avalon:LifeCrystalOre", 5);
                }
            }
            if (recipe.createItem.type == ModContent.ItemType<TemplarsLeggings>())
            {
                if (recipe.TryGetIngredient(ModContent.ItemType<LifeQuartz>(), out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddRecipeGroup("Avalon:LifeCrystalOre", 4);
                }
            }
            if (recipe.createItem.type == ModContent.ItemType<HeartWand>())
            {
                if (recipe.TryGetIngredient(ModContent.ItemType<LifeQuartz>(), out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddRecipeGroup("Avalon:LifeCrystalOre", 6);
                }
            }
            if (recipe.createItem.type == ModContent.ItemType<LifeQuartzShield>())
            {
                if (recipe.TryGetIngredient(ModContent.ItemType<LifeQuartz>(), out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddRecipeGroup("Avalon:LifeCrystalOre", 6);
                }
            }
            if (recipe.createItem.type == ModContent.ItemType<HeadMirror>())
            {
                if (recipe.TryGetIngredient(ModContent.ItemType<LifeQuartz>(), out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddRecipeGroup("Avalon:LifeCrystalOre", 4);
                }
            }
            if (recipe.createItem.type == ItemID.HeartStatue)
            {
                if (recipe.TryGetIngredient(ModContent.ItemType<LifeQuartz>(), out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddRecipeGroup("Avalon:LifeCrystalOre", 5);
                }
            }
            if (recipe.createItem.type == ModContent.ItemType<CupidString>())
            {
                if (recipe.TryGetIngredient(ModContent.ItemType<LifeQuartz>(), out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddRecipeGroup("Avalon:LifeCrystalOre", 10);
                }
            }
            if (recipe.createItem.type == ModContent.ItemType<Violin>())
            {
                if (recipe.TryGetIngredient(ModContent.ItemType<LifeQuartz>(), out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddRecipeGroup("Avalon:LifeCrystalOre", 6);
                }
            }
            if (recipe.createItem.type == ModContent.ItemType<StrawberryHeart>())
            {
                if (recipe.TryGetIngredient(ModContent.ItemType<LifeQuartz>(), out Item ing))
                {
                    recipe.RemoveIngredient(ing);
                    recipe.AddRecipeGroup("Avalon:LifeCrystalOre", 5);
                }
            }
            #endregion
        }
    }
}
[ExtendsFromMod("ThoriumMod")]
public class ThoriumTweaksGlobalItem : GlobalItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("ThoriumMod");
    }
    public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
    {
        // TODO: ADD AVALON RINGS TO SCARLET AND SINISTER CRATES
        if (item.type == ModContent.ItemType<ScarletCrate>() || item.type == ModContent.ItemType<SinisterCrate>() ||
            item.type == ModContent.ItemType<AquaticDepthsCrate>() || item.type == ModContent.ItemType<AbyssalCrate>() ||
            item.type == ModContent.ItemType<StrangeCrate>() || item.type == ModContent.ItemType<WondrousCrate>())
        {
            itemLoot.Add(new CommonDropNotScalingWithLuck(ModContent.ItemType<StaminaPotion>(), 5, 2, 4));
        }
    }
}
