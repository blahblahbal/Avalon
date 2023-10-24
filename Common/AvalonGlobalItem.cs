using System.Collections.Generic;
using Avalon.DropConditions;
using Avalon.Items.Accessories.PreHardmode;
using Avalon.Items.Consumables;
using Avalon.Items.Material;
using Avalon.Items.Weapons.Melee.PreHardmode;
using Avalon.Items.Weapons.Ranged.Hardmode;
using Avalon.Prefixes;
using Avalon.Tiles;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Avalon.Common.Players;
using Avalon.Items.Pets;
using Avalon.Items.Placeable.Tile;
using Avalon.Items.Material.Herbs;
using Avalon.Items.Tools.Superhardmode;
using Terraria.Audio;
using Avalon.Items.Tools;
using Avalon.Items.Tools.Hardmode;
using Avalon.Items.Placeable.Seed;
using Avalon.Items.Material.Ores;
using Avalon.Items.Ammo;
using Avalon.Network;
using Microsoft.Xna.Framework;
using Avalon.Items.Potions.Buff;
using Avalon.Items.Tools.PreHardmode;
using Terraria.DataStructures;

namespace Avalon.Common;

public class AvalonGlobalItem : GlobalItem
{
    private static readonly List<int> nonSolidExceptions = new()
    {
        TileID.Cobweb,
        TileID.LivingCursedFire,
        TileID.LivingDemonFire,
        TileID.LivingFire,
        TileID.LivingFrostFire,
        TileID.LivingIchor,
        TileID.LivingUltrabrightFire,
        TileID.ChimneySmoke,
        TileID.Bubble,
        TileID.Rope,
        TileID.SilkRope,
        TileID.VineRope,
        TileID.WebRope,
        ModContent.TileType<LivingLightning>(),
        //ModContent.TileType<VineRope>(),
    };

    public override void SetStaticDefaults()
    {
        ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<BismuthOre>()] = ItemID.PlatinumOre;
        ItemID.Sets.ShimmerTransformToItem[ItemID.ChlorophyteOre] = ModContent.ItemType<TroxiniumOre>();
        ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<TroxiniumOre>()] = ItemID.TitaniumOre;
        ItemID.Sets.ShimmerTransformToItem[ItemID.AdamantiteOre] = ModContent.ItemType<NaquadahOre>();
        ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<NaquadahOre>()] = ItemID.OrichalcumOre;
        ItemID.Sets.ShimmerTransformToItem[ItemID.MythrilOre] = ModContent.ItemType<DurataniumOre>();
        ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<DurataniumOre>()] = ItemID.PalladiumOre;
        ItemID.Sets.ShimmerTransformToItem[ItemID.CobaltOre] = ModContent.ItemType<IridiumOre>();
        ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<IridiumOre>()] = ModContent.ItemType<OsmiumOre>();
        ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<OsmiumOre>()] = ModContent.ItemType<RhodiumOre>();
        ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<RhodiumOre>()] = ModContent.ItemType<BismuthOre>();
        ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<BismuthOre>()] = ItemID.PlatinumOre;
        ItemID.Sets.ShimmerTransformToItem[ItemID.GoldOre] = ModContent.ItemType<ZincOre>();
        ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<ZincOre>()] = ItemID.TungstenOre;
        ItemID.Sets.ShimmerTransformToItem[ItemID.SilverOre] = ModContent.ItemType<NickelOre>();
        ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<NickelOre>()] = ItemID.LeadOre;
        ItemID.Sets.ShimmerTransformToItem[ItemID.IronOre] = ModContent.ItemType<BronzeOre>();
        ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<BronzeOre>()] = ItemID.TinOre;

        ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Items.Placeable.Tile.ApocalyptusWood>()] = ItemID.Wood;
        ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Coughwood>()] = ItemID.Wood;
        ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Items.Placeable.Tile.BleachedEbony>()] = ItemID.Wood;

        ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Items.Placeable.Tile.OrangeBrick>()] = ModContent.ItemType<Items.Placeable.Tile.Ancient.AncientOrangeBrick>();
        ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Items.Placeable.Tile.PurpleBrick>()] = ModContent.ItemType<Items.Placeable.Tile.Ancient.AncientPurpleBrick>();
        ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<Items.Placeable.Tile.YellowBrick>()] = ModContent.ItemType<Items.Placeable.Tile.Ancient.AncientYellowBrick>();
        Item.staff[ItemID.Vilethorn] = true;
    }
    public override bool CanRightClick(Item item)
    {
        if (Main.mouseRightRelease && Main.mouseRight)
        {
            if (item.type == ModContent.ItemType<Breakdawn>())
            {
                SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
                item.ChangeItemType(ModContent.ItemType<Breakdawn3x3>());
                return false;
            }
            if (item.type == ModContent.ItemType<Breakdawn3x3>())
            {
                SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
                item.ChangeItemType(ModContent.ItemType<Breakdawn>());
                return false;
            }
            if (item.type == ModContent.ItemType<PickaxeofDusk>())
            {
                SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
                item.ChangeItemType(ModContent.ItemType<PickaxeofDusk3x3>());
                return false;
            }
            if (item.type == ModContent.ItemType<PickaxeofDusk3x3>())
            {
                SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
                item.ChangeItemType(ModContent.ItemType<PickaxeofDusk>());
                return false;
            }
            if (item.type == ModContent.ItemType<AccelerationPickaxe>())
            {
                SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
                item.ChangeItemType(ModContent.ItemType<AccelerationPickaxeSpeed>());
                return false;
            }
            if (item.type == ModContent.ItemType<AccelerationPickaxeSpeed>())
            {
                SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
                item.ChangeItemType(ModContent.ItemType<AccelerationPickaxe>());
                return false;
            }
            if (item.type == ModContent.ItemType<AccelerationDrill>())
            {
                SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
                item.ChangeItemType(ModContent.ItemType<AccelerationDrillSpeed>());
                return false;
            }
            if (item.type == ModContent.ItemType<AccelerationDrillSpeed>())
            {
                SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
                item.ChangeItemType(ModContent.ItemType<AccelerationDrill>());
                return false;
            }
            if (item.type == ModContent.ItemType<ShadowPhone>())
            {
                SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
                item.ChangeItemType(ModContent.ItemType<ShadowPhoneSurface>());
                return false;
            }
            if (item.type == ModContent.ItemType<ShadowPhoneSurface>())
            {
                SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
                item.ChangeItemType(ModContent.ItemType<ShadowPhoneHome>());
                return false;
            }
            if (item.type == ModContent.ItemType<ShadowPhoneHome>())
            {
                SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
                item.ChangeItemType(ModContent.ItemType<ShadowPhoneDungeon>());
                return false;
            }
            if (item.type == ModContent.ItemType<ShadowPhoneDungeon>())
            {
                SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
                item.ChangeItemType(ModContent.ItemType<ShadowPhoneJungleTropics>());
                return false;
            }
            if (item.type == ModContent.ItemType<ShadowPhoneJungleTropics>())
            {
                SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
                item.ChangeItemType(ModContent.ItemType<ShadowPhoneOcean>());
                return false;
            }
            if (item.type == ModContent.ItemType<ShadowPhoneOcean>())
            {
                SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
                item.ChangeItemType(ModContent.ItemType<ShadowPhoneHell>());
                return false;
            }
            if (item.type == ModContent.ItemType<ShadowPhoneHell>())
            {
                SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
                item.ChangeItemType(ModContent.ItemType<ShadowPhoneRandom>());
                return false;
            }
            if (item.type == ModContent.ItemType<ShadowPhoneRandom>())
            {
                SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
                item.ChangeItemType(ModContent.ItemType<ShadowPhone>());
                return false;
            }
            if (item.type == ModContent.ItemType<PortablePylonMkIIPoint1>())
            {
                SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
                item.ChangeItemType(ModContent.ItemType<PortablePylonMkIIPoint2>());
                return false;
            }
            if (item.type == ModContent.ItemType<PortablePylonMkIIPoint2>())
            {
                SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
                item.ChangeItemType(ModContent.ItemType<PortablePylonMkIIPoint3>());
                return false;
            }
            if (item.type == ModContent.ItemType<PortablePylonMkIIPoint3>())
            {
                SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
                item.ChangeItemType(ModContent.ItemType<PortablePylonMkIIPoint1>());
                return false;
            }
        }
        return base.CanRightClick(item);
    }
    public override void AddRecipes()
    {
        // --== Shimmer!!! ==--

        ShimmerTransmuteBothWays(ItemID.AntlionClaw, ModContent.ItemType<DesertLongsword>());

        //ShimmerTransmuteBothWays(ItemID.DartRifle, ModContent.ItemType<AncientDartRifle>());
        //ShimmerTransmuteBothWays(ItemID.DartPistol, ModContent.ItemType<AncientDartPistol>());
        //ShimmerTransmuteBothWays(ModContent.ItemType<DartShotgun>(), ModContent.ItemType<AncientDartShotgun>());

        ShimmerTransmuteBothWays(ModContent.ItemType<ArgusLantern>(), ItemID.MagicLantern);

        ShimmerTransmute(ModContent.ItemType<StaminaCrystal>(), ModContent.ItemType<EnergyCrystal>());

        ShimmerTransmute(ModContent.ItemType<Items.Material.Ores.Zircon>(), ModContent.ItemType<Items.Material.Ores.Peridot>());
        ShimmerTransmute(ModContent.ItemType<Items.Material.Ores.Peridot>(), ModContent.ItemType<Items.Material.Ores.Tourmaline>());
        ShimmerTransmute(ModContent.ItemType<Items.Material.Ores.Tourmaline>(), ItemID.Diamond);

        //Dungeon bricks
        ShimmerTransmute(ModContent.ItemType<Items.Placeable.Tile.OrangeBrick>(), ModContent.ItemType<Items.Placeable.Tile.Ancient.AncientOrangeBrick>());
        ShimmerTransmute(ModContent.ItemType<Items.Placeable.Tile.PurpleBrick>(), ModContent.ItemType<Items.Placeable.Tile.Ancient.AncientPurpleBrick>());
        ShimmerTransmute(ModContent.ItemType<Items.Placeable.Tile.YellowBrick>(), ModContent.ItemType<Items.Placeable.Tile.Ancient.AncientYellowBrick>());
        ShimmerTransmute(ModContent.ItemType<Items.Placeable.Wall.OrangeBrickWall>(), ModContent.ItemType<Items.Placeable.Wall.OrangeBrickWallUnsafe>());
        ShimmerTransmute(ModContent.ItemType<Items.Placeable.Wall.OrangeSlabWall>(), ModContent.ItemType<Items.Placeable.Wall.OrangeSlabWallUnsafe>());
        ShimmerTransmute(ModContent.ItemType<Items.Placeable.Wall.OrangeTiledWall>(), ModContent.ItemType<Items.Placeable.Wall.OrangeTiledWallUnsafe>());
        ShimmerTransmute(ModContent.ItemType<Items.Placeable.Wall.PurpleBrickWall>(), ModContent.ItemType<Items.Placeable.Wall.PurpleBrickWallUnsafe>());
        ShimmerTransmute(ModContent.ItemType<Items.Placeable.Wall.PurpleSlabWall>(), ModContent.ItemType<Items.Placeable.Wall.PurpleSlabWallUnsafe>());
        ShimmerTransmute(ModContent.ItemType<Items.Placeable.Wall.PurpleTiledWall>(), ModContent.ItemType<Items.Placeable.Wall.PurpleTiledWallUnsafe>());
        ShimmerTransmute(ModContent.ItemType<Items.Placeable.Wall.YellowBrickWall>(), ModContent.ItemType<Items.Placeable.Wall.YellowBrickWallUnsafe>());
        ShimmerTransmute(ModContent.ItemType<Items.Placeable.Wall.YellowSlabWall>(), ModContent.ItemType<Items.Placeable.Wall.YellowSlabWallUnsafe>());
        ShimmerTransmute(ModContent.ItemType<Items.Placeable.Wall.YellowTiledWall>(), ModContent.ItemType<Items.Placeable.Wall.YellowTiledWallUnsafe>());

        ShimmerTransmute(ModContent.ItemType<Items.Placeable.Wall.ImperviousBrickWallItem>(), ModContent.ItemType<Items.Placeable.Wall.ImperviousBrickWallUnsafe>());

        Recipe.Create(ItemID.DesertTorch, 3).AddIngredient(ItemID.Torch, 3).AddIngredient(ModContent.ItemType<HardenedSnotsandBlock>());

        Recipe.Create(ItemID.IceTorch, 3).AddIngredient(ItemID.Torch, 3).AddIngredient(ModContent.ItemType<YellowIceBlock>());
    }
    public override void ExtractinatorUse(int extractType, int extractinatorBlockType, ref int resultType, ref int resultStack)
    {
        if (extractType == 0 || extractType == ItemID.DesertFossil)
        {
            if (Main.rand.NextBool(3))
            {
                resultStack = 1;
                if (Main.rand.NextBool(20))
                {
                    resultStack += Main.rand.Next(0, 2);
                }
                if (Main.rand.NextBool(30))
                {
                    resultStack += Main.rand.Next(0, 3);
                }
                if (Main.rand.NextBool(40))
                {
                    resultStack += Main.rand.Next(0, 4);
                }
                if (Main.rand.NextBool(50))
                {
                    resultStack += Main.rand.Next(0, 5);
                }
                if (Main.rand.NextBool(60))
                {
                    resultStack += Main.rand.Next(0, 6);
                }
                switch (Main.rand.Next(13))
                {
                    case 0:
                        resultType = ModContent.ItemType<Items.Material.Ores.BronzeOre>();
                        break;
                    case 1:
                        resultType = ModContent.ItemType<Items.Material.Ores.NickelOre>();
                        break;
                    case 2:
                        resultType = ModContent.ItemType<Items.Material.Ores.ZincOre>();
                        break;
                    case 3:
                        resultType = ModContent.ItemType<Items.Material.Ores.BismuthOre>();
                        break;
                    case 4:
                        resultType = ModContent.ItemType<Items.Material.Ores.RhodiumOre>();
                        break;
                    case 5:
                        resultType = ModContent.ItemType<Items.Material.Ores.OsmiumOre>();
                        break;
                    case 6:
                        resultType = ModContent.ItemType<Items.Material.Ores.IridiumOre>();
                        break;
                    case 7:
                        resultType = ModContent.ItemType<Items.Material.Ores.Tourmaline>();
                        break;
                    case 8:
                        resultType = ModContent.ItemType<Items.Material.Ores.Peridot>();
                        break;
                    case 9:
                        resultType = ModContent.ItemType<Items.Material.Ores.Zircon>();
                        break;
                    case 10:
                        resultType = ModContent.ItemType<Items.Material.Ores.Heartstone>();
                        break;
                    case 11:
                        resultType = ModContent.ItemType<Items.Material.Ores.Starstone>();
                        break;
                    case 12:
                        resultType = ModContent.ItemType<Items.Material.Ores.Boltstone>();
                        break;
                }
            }
        }
    }
    public void ShimmerTransmute(int From, int To)
    {
        Recipe ShimmerTransmute = Recipe.Create(From);
        ShimmerTransmute.AddCustomShimmerResult(To);
        ShimmerTransmute.AddCondition(ShimmerCraftCondition.ShimmerOnly);
        ShimmerTransmute.Register();
    }
    public override void ModifyWeaponCrit(Item item, Player player, ref float crit)
    {
        Item curItem = item;

        Item storedItem = new Item();
        storedItem.SetDefaults(curItem.type);

        float cMagic = 0f;
        float cMelee = 0f;
        float cRanged = 0f;
        if (storedItem.DamageType == DamageClass.Magic)
        {
            cMagic = item.crit + player.GetTotalCritChance(DamageClass.Magic);
        }
        if (storedItem.DamageType == DamageClass.Melee)
        {
            cMelee = item.crit + player.GetTotalCritChance(DamageClass.Melee);
        }
        if (storedItem.DamageType == DamageClass.Ranged)
        {
            cRanged = item.crit + player.GetTotalCritChance(DamageClass.Ranged);
        }

        // magic
        if (player.GetModPlayer<AvalonPlayer>().MaxMagicCrit < 100)
        {
            if (item.CountsAsClass(DamageClass.Magic))
            {
                if (crit > player.GetModPlayer<AvalonPlayer>().MaxMagicCrit)
                    crit = player.GetModPlayer<AvalonPlayer>().MaxMagicCrit;
            }
        }
        else
        {
            if (item.DamageType == DamageClass.Magic)
            {
                crit = (int)cMagic;
            }
        }
        // melee
        if (player.GetModPlayer<AvalonPlayer>().MaxMeleeCrit < 100)
        {
            if (item.CountsAsClass(DamageClass.Melee))
            {
                if (crit > player.GetModPlayer<AvalonPlayer>().MaxMeleeCrit)
                    crit = player.GetModPlayer<AvalonPlayer>().MaxMeleeCrit;
            }
        }
        else
        {
            if (item.DamageType == DamageClass.Melee)
            {
                crit = (int)cMelee;
            }
        }
        // ranged
        if (player.GetModPlayer<AvalonPlayer>().MaxRangedCrit < 100)
        {
            if (item.CountsAsClass(DamageClass.Ranged))
            {
                if (crit > player.GetModPlayer<AvalonPlayer>().MaxRangedCrit)
                    crit = player.GetModPlayer<AvalonPlayer>().MaxRangedCrit;
            }
        }
        else
        {
            if (item.DamageType == DamageClass.Ranged)
            {
                crit = (int)cRanged;
            }
        }
    }
    public void ShimmerTransmuteBothWays(int From, int To)
    {
        Recipe ShimmerTransmute = Recipe.Create(From);
        ShimmerTransmute.AddCustomShimmerResult(To);
        ShimmerTransmute.AddCondition(ShimmerCraftCondition.ShimmerOnly);
        ShimmerTransmute.Register();
        Recipe ShimmerTransmute2 = Recipe.Create(To);
        ShimmerTransmute2.AddCustomShimmerResult(From);
        ShimmerTransmute2.AddCondition(ShimmerCraftCondition.ShimmerOnly);
        ShimmerTransmute2.Register();
    }
    public override void HoldItem(Item item, Player player)
    {
        #region extractinator prefix removal
        if (item.prefix > 0)
        {
            Point p = player.GetModPlayer<AvalonPlayer>().MousePosition.ToTileCoordinates();
            Tile t = Framing.GetTileSafely(p);
            if (player.IsInTileInteractionRange(Player.tileTargetX, Player.tileTargetY, TileReachCheckSettings.Simple) &&
                (t.TileType == TileID.Extractinator || t.TileType == TileID.ChlorophyteExtractinator))
            {
                player.noThrow = 2;
                player.cursorItemIconEnabled = true;
                player.cursorItemIconID = item.type;
                if (Main.mouseRight && Main.mouseRightRelease)
                {
                    Item item2 = new Item();
                    item2.SetDefaults(item.type);
                    if (item2.value < item.value)
                    {
                        int money = (int)(item.value / 4 * Main.rand.NextFloat(0.5f, 1.5f)) / 5;
                        ClassExtensions.DropCoinsProperly(money, (int)player.position.X, (int)player.position.Y);
                    }
                    SoundEngine.PlaySound(SoundID.Item37);
                    bool favorited = item.favorited;
                    item.SetDefaults(item.type);
                    item.favorited = favorited;
                }
            }
        }
        #endregion

        #region herb seed block swap
        /*if (Data.Sets.Item.HerbSeeds[item.type])
        {
            Point mpTile = player.GetModPlayer<AvalonPlayer>().MousePosition.ToTileCoordinates();

            if ((Main.tile[mpTile.X, mpTile.Y].TileType == TileID.BloomingHerbs ||
                 (Main.tile[mpTile.X, mpTile.Y].TileType == ModContent.TileType<Tiles.Herbs.Barfbush>() &&
                  Main.tile[mpTile.X, mpTile.Y].TileFrameX == 36) ||
                 (Main.tile[mpTile.X, mpTile.Y].TileType == ModContent.TileType<Tiles.Herbs.Bloodberry>() &&
                  Main.tile[mpTile.X, mpTile.Y].TileFrameX == 36) ||
                 (Main.tile[mpTile.X, mpTile.Y].TileType == ModContent.TileType<Tiles.Herbs.Sweetstem>() &&
                  Main.tile[mpTile.X, mpTile.Y].TileFrameX == 36) ||
                 (Main.tile[mpTile.X, mpTile.Y].TileType == ModContent.TileType<Tiles.Herbs.Holybird>() &&
                  Main.tile[mpTile.X, mpTile.Y].TileFrameX == 36)) &&
                (Main.tile[mpTile.X, mpTile.Y + 1].TileType == TileID.ClayPot ||
                 Main.tile[mpTile.X, mpTile.Y + 1].TileType == TileID.PlanterBox ||
                 Main.tile[mpTile.X, mpTile.Y + 1].TileType == ModContent.TileType<PlanterBoxes>()) && Main.mouseLeft)
            {
                WorldGen.KillTile(mpTile.X, mpTile.Y);
                if (!Main.tile[mpTile.X, mpTile.Y].HasTile && Main.netMode != NetmodeID.SinglePlayer)
                {
                    NetMessage.SendData(Terraria.ID.MessageID.TileManipulation, -1, -1, null, 0, mpTile.X, mpTile.Y);
                }

                WorldGen.PlaceTile(mpTile.X, mpTile.Y, item.createTile, style: item.placeStyle);
                if (Main.tile[mpTile.X, mpTile.Y].HasTile && Main.netMode != NetmodeID.SinglePlayer)
                {
                    NetMessage.SendData(Terraria.ID.MessageID.TileManipulation, -1, -1, null, 1, mpTile.X, mpTile.Y,
                        item.createTile, item.placeStyle);
                }

                item.stack--;
            }
        }*/
        #endregion herb seed block swap
    }
    public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
    {
        if (item.type == ItemID.PlanteraBossBag)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<LifeDew>(), 1, 10, 17));
            itemLoot.Add(ItemDropRule.Common(ItemID.ChlorophyteOre, 1, 75, 130));
        }
        if (item.type == ItemID.WallOfFleshBossBag)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<FleshyTendril>(), 1, 25, 36));
        }
        if (item.type == ItemID.EyeOfCthulhuBossBag)
        {
            var contagionCondition = new IsContagion();
            var corruptionCondition = new Conditions.IsCorruption();
            var crimson = new CrimsonNotContagion();
            var corruptionNotContagion = new Combine(true, null, new Invert(contagionCondition), corruptionCondition);

            // remove corruption loot
            itemLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.DemoniteOre);
            itemLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.CorruptSeeds);
            itemLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.UnholyArrow);

            // remove crimson loot
            itemLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.CrimtaneOre);
            itemLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.CrimsonSeeds);

            // add corruption loot back
            LeadingConditionRule corruptionRule = new LeadingConditionRule(corruptionNotContagion);
            corruptionRule.OnSuccess(ItemDropRule.Common(ItemID.DemoniteOre, 1, 30, 90));
            corruptionRule.OnSuccess(ItemDropRule.Common(ItemID.CorruptSeeds, 1, 1, 3));
            corruptionRule.OnSuccess(ItemDropRule.Common(ItemID.UnholyArrow, 1, 20, 50));
            corruptionRule.OnSuccess(ItemDropRule.Common(ItemID.BloodMoonStarter, 12));
            itemLoot.Add(corruptionRule);

            // add crimson loot back
            LeadingConditionRule crimsonRule = new LeadingConditionRule(crimson);
            crimsonRule.OnSuccess(ItemDropRule.Common(ItemID.CrimtaneOre, 1, 30, 90));
            crimsonRule.OnSuccess(ItemDropRule.Common(ItemID.CrimsonSeeds, 1, 1, 3));
            crimsonRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BloodyArrow>(), 1, 20, 50));
            crimsonRule.OnSuccess(ItemDropRule.Common(ItemID.BloodMoonStarter, 12));
            itemLoot.Add(crimsonRule);

            // add contagion loot
            LeadingConditionRule contagionRule = new LeadingConditionRule(contagionCondition);
            contagionRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BacciliteOre>(), 1, 30, 90));
            contagionRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ContagionSeeds>(), 1, 1, 3));
            contagionRule.OnSuccess(ItemDropRule.Common(ItemID.BloodMoonStarter, 12));
            itemLoot.Add(contagionRule);
        }
    }
    public override void SetDefaults(Item item)
    {
        if (item.IsArmor())
        {
            ItemID.Sets.CanGetPrefixes[item.type] = true;
        }
        if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome)
        {
            item.accessory = true;
        }
        switch (item.type)
        {
            case ItemID.CactusHelmet:
            case ItemID.CactusBreastplate:
            case ItemID.CactusLeggings:
                item.defense = 2;
                break;
            case ItemID.GravitationPotion:
                item.buffTime = 3600 * 6;
                break;
            case ItemID.MagicPowerPotion:
                item.buffTime = 3600 * 5;
                break;
            case ItemID.CellPhone:
            case ItemID.Shellphone:
            case ItemID.ShellphoneHell:
            case ItemID.ShellphoneOcean:
            case ItemID.ShellphoneSpawn:
                item.useTime = item.useAnimation = 30;
                break;
            case ItemID.RottenChunk:
                item.useStyle = ItemUseStyleID.Swing;
                item.useAnimation = 15;
                item.useTime = 10;
                item.consumable = true;
                item.useTurn = true;
                item.autoReuse = true;
                item.createTile = ModContent.TileType<RottenChunk>();
                break;
            case ItemID.Vertebrae:
                item.useStyle = ItemUseStyleID.Swing;
                item.useAnimation = 15;
                item.useTime = 10;
                item.consumable = true;
                item.useTurn = true;
                item.autoReuse = true;
                item.createTile = ModContent.TileType<Vertebrae>();
                break;
            case ItemID.Ectoplasm:
                item.useStyle = ItemUseStyleID.Swing;
                item.useAnimation = 15;
                item.useTime = 10;
                item.consumable = true;
                item.useTurn = true;
                item.autoReuse = true;
                item.createTile = ModContent.TileType<Ectoplasm>();
                break;
            #region miscellaneous changes
            case ItemID.ShroomiteDiggingClaw:
                item.pick = 205;
                break;
            case ItemID.Picksaw:
                item.tileBoost++;
                break;
            case ItemID.HeatRay:
                item.mana = 5;
                break;
            case ItemID.TheAxe:
                item.hammer = 95;
                break;
            case ItemID.BonePickaxe:
                item.pick = 55;
                break;
            case ItemID.LaserDrill:
                item.pick = 220;
                break;
            case ItemID.Hellstone:
                item.value = Item.sellPrice(0, 0, 13, 30);
                break;
            case ItemID.NightmarePickaxe:
                item.pick = 60;
                break;
            case ItemID.DeathbringerPickaxe:
                item.pick = 64;
                break;
            case ItemID.Vilethorn:
                item.useStyle = ItemUseStyleID.Shoot;
                break;
            #endregion miscellaneous changes
            #region ML item rebalance
            case ItemID.Meowmere:
                item.damage = 145;
                break;
            case ItemID.SDMG:
                item.damage = 49;
                break;
            case ItemID.StarWrath:
                item.damage = 85;
                break;
            case ItemID.LastPrism:
                item.damage = 68;
                break;
            case ItemID.Terrarian:
                item.damage = 144;
                break;
            case ItemID.LunarFlareBook:
                item.damage = 75;
                break;
            case ItemID.RainbowCrystalStaff:
                item.damage = 115;
                break;
            case ItemID.MoonlordTurretStaff:
                item.damage = 85;
                break;
            #endregion ML item rebalance
        }
    }
    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (player.GetModPlayer<AvalonPlayer>().SplitProj && Main.rand.NextBool(7) && item.DamageType == DamageClass.Ranged && item.useAmmo > 0)
        {
            for (int num122 = 0; num122 < 2; num122++)
            {
                float num123 = velocity.X;
                float num124 = velocity.Y;
                num123 += Main.rand.Next(-30, 31) * 0.05f;
                num124 += Main.rand.Next(-30, 31) * 0.05f;
                Projectile.NewProjectile(player.GetSource_FromThis(), position.X, position.Y, num123, num124, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
        return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
    }
    public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
    {
        if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome && slot < 19 && !modded)
        {
            return false;
        }

        return !item.IsArmor() && base.CanEquipAccessory(item, player, slot, modded);
    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        TooltipLine? tooltipLine = tooltips.Find(x => x.Name == "ItemName" && x.Mod == "Terraria");
        TooltipLine? tooltipMat = tooltips.Find(x => x.Name == "Material" && x.Mod == "Terraria");
        TooltipLine? tooltipEquip = tooltips.Find(x => x.Name == "Equipable" && x.Mod == "Terraria");
        TooltipLine? healLife = tooltips.Find(x => x.Name == "HealLife" && x.Mod == "Terraria");
        TooltipLine? buffTime = tooltips.Find(x => x.Name == "BuffTime" && x.Mod == "Terraria");
        if (healLife != null && item.healLife > 0)
        {
            if (Main.LocalPlayer.GetModPlayer<AvalonPlayer>().ThePill)
            {
                healLife.Text = Language.GetTextValue("CommonItemTooltip.RestoresLife", (int)(item.healLife * Items.Accessories.Hardmode.ThePill.LifeBonusAmount));
            }
        }
        if (tooltipEquip != null)
        {
            if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome)
            {
                tooltips.RemoveAt(tooltips.FindIndex(x => x.Name == "Equipable" && x.Mod == "Terraria"));
            }
        }
        if (item.type == ModContent.ItemType<VomitWater>() || item.type is ItemID.BloodWater or ItemID.UnholyWater or ItemID.HolyWater)
        {
            int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") && tt.Name.Equals("CritChance")));
            if (index != -1)
            {
                tooltips.RemoveAt(index);
            }
        }
        if (item.type is ItemID.Extractinator)
        {
            int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") && tt.Name.Equals("Tooltip0")));
            if (index != -1)
            {
                tooltips.Insert(index + 1, new TooltipLine(Mod, "Tooltip0", NetworkText.FromKey("Mods.Avalon.TooltipEdits.Extractinator").ToString()));
            }
        }
        if (item.type is ItemID.ChlorophyteExtractinator)
        {
            int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") && tt.Name.Equals("Tooltip2")));
            if (index != -1)
            {
                tooltips.Insert(index + 1, new TooltipLine(Mod, "Tooltip2", NetworkText.FromKey("Mods.Avalon.TooltipEdits.Extractinator").ToString()));
            }
        }
        if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome)
        {
            int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                && tt.Name.Equals("ItemName"));
            if (index != -1)
            {
                tooltips.Insert(index + 1, new TooltipLine(Mod, "TomeTooltip", NetworkText.FromKey("Mods.Avalon.CommonItemTooltip.Tome").ToString()));
                tooltips.Insert(index + 2, new TooltipLine(Mod, "TomeGradeTooltip", NetworkText.FromKey("Mods.Avalon.CommonItemTooltip.TomeGrade") + " " + item.GetGlobalItem<AvalonGlobalItemInstance>().TomeGrade));
            }
        }
        if (item.IsTool())
        {
            if (item.prefix == ModContent.PrefixType<Efficient>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                        && (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixTool", Language.GetTextValue("Mods.Avalon.PrefixTooltips.Efficiency"))
                    {
                        IsModifier = true
                    });
                }
                index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                        && tt.Name.Equals("PrefixSpeed"));
                if (index != -1)
                {
                    tooltips.RemoveAt(index);
                }
            }
        }
        if (item.IsArmor() && !item.social)
        {
            if (item.prefix == ModContent.PrefixType<Busted>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                        && (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccDefense", "-1 defense")
                    {
                        IsModifier = true,
                        IsModifierBad = true
                    });
                }
            }
            if (item.prefix == ModContent.PrefixType<Loaded>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                        && (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccDefense", "+1 defense")
                    {
                        IsModifier = true
                    });
                }
            }
            if (item.prefix == ModContent.PrefixType<Protective>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                        && (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccDefense", "+2 defense")
                    {
                        IsModifier = true
                    });
                }
            }
            if (item.prefix == ModContent.PrefixType<Disgusting>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                        && (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccDefense", "-2 defense")
                    {
                        IsModifier = true,
                        IsModifierBad = true
                    });
                }
            }
        }
        if (item.accessory && !item.social)
        {
            if (item.prefix == ModContent.PrefixType<Enchanted>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                        && (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable") || tt.Name.Equals("Expert")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccDefense", "+1 defense")
                    {
                        IsModifier = true
                    });
                }
            }
            if (item.prefix == ModContent.PrefixType<Hoarding>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                        && (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccAmmo", Language.GetTextValue("Mods.Avalon.PrefixTooltips.Ammo"))
                    {
                        IsModifier = true
                    });
                }
            }
            if (item.prefix == ModContent.PrefixType<Greedy>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                        && (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccMoney", Language.GetTextValue("Mods.Avalon.PrefixTooltips.Money"))
                    {
                        IsModifier = true
                    });
                }
            }
            if (item.prefix == ModContent.PrefixType<Motivated>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                        && (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccStamRegen", Language.GetTextValue("Mods.Avalon.PrefixTooltips.StamRegen"))
                    {
                        IsModifier = true
                    });
                }
            }
        }

        if (tooltipMat != null)
        {
            if (item.GetGlobalItem<AvalonGlobalItemInstance>().TomeMaterial)
            {
                tooltipMat.Text = Language.GetTextValue("Mods.Avalon.CommonItemTooltip.TomeMaterial");
            }
        }
        if (tooltipLine != null && (ModContent.GetInstance<AvalonConfig>().VanillaRenames || ModContent.GetInstance<AvalonConfig>().VanillaTextureReplacement))
        {
            if (item.type == ItemID.BloodMoonStarter)
            {
                tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.BloodyAmulet");
            }
        }
        if (tooltipLine != null && ModContent.GetInstance<AvalonConfig>().VanillaRenames)
        {
            //if (item.type == ItemID.CoinGun)
            //{
            //    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.CoinGun");
            //}
            //if (item.type == ItemID.FieryGreatsword)
            //{
            //    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.FieryGreatsword");
            //}
            //if (item.type == ItemID.PurpleMucos)
            //{
            //    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.PurpleMucus");
            //}
            //if (item.type == ItemID.HighTestFishingLine)
            //{
            //    tooltipLine.Text = tooltipLine.Text.Replace("Test", "Tensile");
            //}
            //if (item.type == ItemID.BlueSolution)
            //{
            //    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Hallow");
            //}
            //if (item.type == ItemID.DarkBlueSolution)
            //{
            //    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Mushrooms");
            //}
            //if (item.type == ItemID.GreenSolution)
            //{
            //    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Purity");
            //}
            //if (item.type == ItemID.PurpleSolution)
            //{
            //    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Corruption");
            //}
            //if (item.type == ItemID.RedSolution)
            //{
            //    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Crimson");
            //}
            //if (item.type == ItemID.SandSolution)
            //{
            //    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Desert");
            //}
            //if (item.type == ItemID.SnowSolution)
            //{
            //    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Snow");
            //}
            //if (item.type == ItemID.DirtSolution)
            //{
            //    tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Forest");
            //}
            //if (item.type == ItemID.FrostsparkBoots)
            //{
            //    tooltipLine.Text = tooltipLine.Text.Replace("Frostspark", "Sparkfrost");
            //}
        }

        if (!item.social && PrefixLoader.GetPrefix(item.prefix) is ExxoPrefix exxoPrefix)
        {
            if (exxoPrefix.Category is PrefixCategory.Accessory or PrefixCategory.Custom)
            {
                tooltips.AddRange(exxoPrefix.TooltipLines);
            }
        }

        switch (item.type)
        {
            //case ItemID.Vine:
            //    tooltips.Add(new TooltipLine(Mod, "Rope", "Can be climbed on"));
            //    break;
            case ItemID.IceBlade:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.IceBlade");
                    }
                }
                break;
            case ItemID.AntlionClaw:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.MandibleBlade");
                    }
                }
                break;
            case ItemID.Frostbrand:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.Frostbrand");
                    }
                }
                break;
            case ItemID.DeathbringerPickaxe:
            case ItemID.NightmarePickaxe:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.EvilPickaxe");
                    }
                }
                break;
            case ItemID.HighTestFishingLine:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.FishingLine");
                    }
                }
                break;
            case ItemID.Seed:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.Blowpipes");
                    }
                }
                break;
            case ItemID.TempleKey:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.TempleKey");
                    }
                }
                break;
            case ItemID.PoisonDart:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip1")
                    {
                        tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.PoisonDart");
                    }
                }
                break;
            case ItemID.CoinGun:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.CoinGun.PartOne");
                    }
                    if (tooltip.Name == "Tooltip1")
                    {
                        tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.CoinGun.PartTwo");
                    }
                }
                break;
            case ItemID.PickaxeAxe:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.PickaxeAxe.PartOne");
                    }
                    if (tooltip.Name == "Tooltip1")
                    {
                        tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.PickaxeAxe.PartTwo");
                    }
                }
                break;
            case ItemID.Drax:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.Drax.PartOne");
                    }
                    if (tooltip.Name == "Tooltip1")
                    {
                        tooltip.Text = Language.GetTextValue("Mods.Avalon.TooltipEdits.Drax.PartTwo");
                    }
                }
                break;
        }
    }
    public override void UpdateVanity(Item item, Player player)
    {
        if (item.type == ItemID.HighTestFishingLine)
        {
            player.accFishingLine = true;
        }
    }
    public override bool AllowPrefix(Item item, int pre)
    {
        if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome)
        {
            return false;
        }
        return base.AllowPrefix(item, pre);
    }
    public override bool? PrefixChance(Item item, int pre, UnifiedRandom rand)
    {
        if (item.IsArmor() && pre == -3)
        {
            return true;
        }
        if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome && (pre == -1 || pre == -3))
        {
            return false;
        }

        return base.PrefixChance(item, pre, rand);
    }
    public override bool? UseItem(Item item, Player player)
    {
        if (player.GetModPlayer<AvalonPlayer>().CloudGlove && player.whoAmI == Main.myPlayer)
        {
            bool inrange = player.IsInTileInteractionRange(Player.tileTargetX, Player.tileTargetY, Terraria.DataStructures.TileReachCheckSettings.Simple);
            if (item.createTile > -1 &&
                (Main.tileSolid[item.createTile] || nonSolidExceptions.Contains(item.createTile)) &&
                (Main.tile[Player.tileTargetX, Player.tileTargetY].LiquidType != LiquidID.Lava ||
                 player.GetModPlayer<AvalonPlayer>().ObsidianGlove) &&
                !Main.tile[Player.tileTargetX, Player.tileTargetY].HasTile && inrange)
            {
                bool subtractFromStack = WorldGen.PlaceTile(Player.tileTargetX, Player.tileTargetY, item.createTile);
                if (Main.tile[Player.tileTargetX, Player.tileTargetY].HasTile &&
                    Main.netMode != NetmodeID.SinglePlayer && subtractFromStack)
                {
                    NetMessage.SendData(Terraria.ID.MessageID.TileManipulation, -1, -1, null, 1, Player.tileTargetX,
                        Player.tileTargetY, item.createTile);
                }

                if (subtractFromStack)
                {
                    item.stack--;
                }
            }

            if (item.createWall > 0 && Main.tile[Player.tileTargetX, Player.tileTargetY].WallType == 0 && inrange)
            {
                WorldGen.PlaceWall(Player.tileTargetX, Player.tileTargetY, item.createWall);
                if (Main.tile[Player.tileTargetX, Player.tileTargetY].WallType != 0 &&
                    Main.netMode != NetmodeID.SinglePlayer)
                {
                    NetMessage.SendData(Terraria.ID.MessageID.TileManipulation, -1, -1, null, 3, Player.tileTargetX,
                        Player.tileTargetY, item.createWall);
                }

                //Main.PlaySound(0, Player.tileTargetX * 16, Player.tileTargetY * 16, 1);
                item.stack--;
            }
        }

        return base.UseItem(item, player);
    }

    public override int ChoosePrefix(Item item, UnifiedRandom rand) => item.IsArmor()
        ? Main.rand.Next(ExxoPrefix.ExxoCategoryPrefixes[ExxoPrefixCategory.Armor]).Type
        : base.ChoosePrefix(item, rand);
}
