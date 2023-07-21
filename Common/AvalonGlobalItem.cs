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
        //ModContent.TileType<LivingLightning>(),
        //ModContent.TileType<VineRope>(),
    };

    public override void SetStaticDefaults()
    {
        Item.staff[ItemID.Vilethorn] = true;
    }
    public override void AddRecipes()
    {
        // --== Shimmer!!! ==--

        ShimmerTransmuteBothWays(ItemID.AntlionClaw, ModContent.ItemType<DesertLongsword>());

        ShimmerTransmuteBothWays(ItemID.DartRifle, ModContent.ItemType<AncientDartRifle>());
        ShimmerTransmuteBothWays(ItemID.DartPistol, ModContent.ItemType<AncientDartPistol>());
        ShimmerTransmuteBothWays(ModContent.ItemType<DartShotgun>(), ModContent.ItemType<AncientDartShotgun>());

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

    public void ShimmerTransmute(int From, int To)
    {
        Recipe ShimmerTransmute = Recipe.Create(From);
        ShimmerTransmute.AddCustomShimmerResult(To);
        ShimmerTransmute.AddCondition(ShimmerCraftCondition.ShimmerOnly);
        ShimmerTransmute.Register();
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
        #region barbaric prefix logic
        var tempItem = new Item();
        tempItem.netDefaults(item.netID);
        tempItem = item.Clone();
        float kbDiff = 0f;
        if (item.prefix == PrefixID.Superior || item.prefix == PrefixID.Savage || item.prefix == PrefixID.Bulky ||
            item.prefix == PrefixID.Taboo || item.prefix == PrefixID.Celestial ||
            item.prefix == ModContent.PrefixType<Horrific>())
        {
            kbDiff = 0.1f;
        }
        else if (item.prefix == PrefixID.Forceful || item.prefix == PrefixID.Strong ||
                 item.prefix == PrefixID.Unpleasant ||
                 item.prefix == PrefixID.Godly || item.prefix == PrefixID.Heavy || item.prefix == PrefixID.Legendary ||
                 item.prefix == PrefixID.Intimidating || item.prefix == PrefixID.Staunch ||
                 item.prefix == PrefixID.Unreal ||
                 item.prefix == PrefixID.Furious || item.prefix == PrefixID.Mythical)
        {
            kbDiff = 0.15f;
        }
        else if (item.prefix == PrefixID.Broken || item.prefix == PrefixID.Weak || item.prefix == PrefixID.Shameful ||
                 item.prefix == PrefixID.Awkward)
        {
            kbDiff = -0.2f;
        }
        else if (item.prefix == PrefixID.Nasty || item.prefix == PrefixID.Ruthless || item.prefix == PrefixID.Unhappy ||
                 item.prefix == PrefixID.Light || item.prefix == PrefixID.Awful || item.prefix == PrefixID.Deranged ||
                 item.prefix == ModContent.PrefixType<Excited>())
        {
            kbDiff = -0.1f;
        }
        else if (item.prefix == PrefixID.Shoddy || item.prefix == PrefixID.Terrible)
        {
            kbDiff = -0.15f;
        }
        else if (item.prefix == PrefixID.Deadly || item.prefix == PrefixID.Masterful)
        {
            kbDiff = 0.05f;
        }

        item.knockBack = tempItem.knockBack * (1 + kbDiff);
        item.knockBack *= player.GetModPlayer<AvalonPlayer>().BonusKB;
        #endregion barbaric prefix logic
    }
    public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
    {
        if(item.type == ItemID.PlanteraBossBag)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<LifeDew>(), 1, 10, 17));
            itemLoot.Add(ItemDropRule.Common(ItemID.ChlorophyteOre, 1, 75, 130));
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
            case ItemID.MagicMirror:
            case ItemID.IceMirror:
            case ItemID.CellPhone:
            case ItemID.Shellphone:
            case ItemID.MagicConch:
            case ItemID.DemonConch:
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
                item.useStyle = 5;
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
                item.damage = 72;
                break;
            case ItemID.Terrarian:
                item.damage = 144;
                break;
            case ItemID.LunarFlareBook:
                item.damage = 75;
                break;
            case ItemID.RainbowCrystalStaff:
                item.damage = 120;
                break;
            #endregion ML item rebalance
        }
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
        if (tooltipEquip != null)
        {
            if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome)
            {
                tooltips.RemoveAt(tooltips.FindIndex(x => x.Name == "Equipable" && x.Mod == "Terraria"));
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
            if (item.type == ItemID.CoinGun)
            {
                tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.CoinGun");
            }
            if (item.type == ItemID.PurpleMucos)
            {
                tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.PurpleMucus");
            }
            if (item.type == ItemID.HighTestFishingLine)
            {
                tooltipLine.Text = tooltipLine.Text.Replace("Test", "Tensile");
            }
            if (item.type == ItemID.BlueSolution)
            {
                tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Hallow");
            }
            if (item.type == ItemID.DarkBlueSolution)
            {
                tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Mushrooms");
            }
            if (item.type == ItemID.GreenSolution)
            {
                tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Purity");
            }
            if (item.type == ItemID.PurpleSolution)
            {
                tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Corruption");
            }
            if (item.type == ItemID.RedSolution)
            {
                tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Crimson");
            }
            if (item.type == ItemID.SandSolution)
            {
                tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Desert");
            }
            if (item.type == ItemID.SnowSolution)
            {
                tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Snow");
            }
            if (item.type == ItemID.DirtSolution)
            {
                tooltipLine.Text = Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Forest");
            }
            if (item.type == ItemID.FrostsparkBoots)
            {
                tooltipLine.Text = tooltipLine.Text.Replace("Frostspark", "Sparkfrost");
            }
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
            bool inrange =
                (player.position.X / 16f) - Player.tileRangeX - player.inventory[player.selectedItem].tileBoost -
                player.blockRange <= Player.tileTargetX &&
                ((player.position.X + player.width) / 16f) + Player.tileRangeX +
                player.inventory[player.selectedItem].tileBoost - 1f + player.blockRange >= Player.tileTargetX &&
                (player.position.Y / 16f) - Player.tileRangeY - player.inventory[player.selectedItem].tileBoost -
                player.blockRange <= Player.tileTargetY &&
                ((player.position.Y + player.height) / 16f) + Player.tileRangeY +
                player.inventory[player.selectedItem].tileBoost - 2f + player.blockRange >= Player.tileTargetY;
            if (item.createTile > -1 &&
                (Main.tileSolid[item.createTile] || nonSolidExceptions.Contains(item.createTile)) &&
                (Main.tile[Player.tileTargetX, Player.tileTargetY].LiquidType != LiquidID.Lava ||
                 player.HasItemInArmor(ModContent.ItemType<ObsidianGlove>())) &&
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
