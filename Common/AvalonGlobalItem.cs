using System.Collections.Generic;
using Avalon.DropConditions;
using Avalon.Items.Accessories.PreHardmode;
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
    public override void AddRecipes()
    {
        // --== Shimmer!!! ==--
        ModContent.GetInstance<DesertLongsword>().CreateRecipe()
            .AddCustomShimmerResult(ItemID.AntlionClaw)
            .AddCondition(ShimmerCraftCondition.ShimmerOnly)
            .Register();
        Recipe MandibleBladeTransmute = Recipe.Create(ItemID.AntlionClaw);
        MandibleBladeTransmute.AddCustomShimmerResult(ModContent.ItemType<DesertLongsword>());
        MandibleBladeTransmute.AddCondition(ShimmerCraftCondition.ShimmerOnly);
        MandibleBladeTransmute.Register();

        Recipe DartRifleTransmute = Recipe.Create(ItemID.DartRifle);
        DartRifleTransmute.AddCustomShimmerResult(ModContent.ItemType<AncientDartRifle>());
        DartRifleTransmute.AddCondition(ShimmerCraftCondition.ShimmerOnly);
        DartRifleTransmute.Register();

        Recipe DartRifleTransmute2 = Recipe.Create(ModContent.ItemType<AncientDartRifle>());
        DartRifleTransmute2.AddCustomShimmerResult(ItemID.DartRifle);
        DartRifleTransmute2.AddCondition(ShimmerCraftCondition.ShimmerOnly);
        DartRifleTransmute2.Register();

        Recipe DartPistolTransmute = Recipe.Create(ItemID.DartPistol);
        DartPistolTransmute.AddCustomShimmerResult(ModContent.ItemType<AncientDartPistol>());
        DartPistolTransmute.AddCondition(ShimmerCraftCondition.ShimmerOnly);
        DartPistolTransmute.Register();

        Recipe DartPistolTransmute2 = Recipe.Create(ModContent.ItemType<AncientDartPistol>());
        DartPistolTransmute2.AddCustomShimmerResult(ItemID.DartPistol);
        DartPistolTransmute2.AddCondition(ShimmerCraftCondition.ShimmerOnly);
        DartPistolTransmute2.Register();

        Recipe DartShotgunTransmute = Recipe.Create(ModContent.ItemType<DartShotgun>());
        DartShotgunTransmute.AddCustomShimmerResult(ModContent.ItemType<AncientDartShotgun>());
        DartShotgunTransmute.AddCondition(ShimmerCraftCondition.ShimmerOnly);
        DartShotgunTransmute.Register();

        Recipe DartShotgunTransmute2 = Recipe.Create(ModContent.ItemType<AncientDartShotgun>());
        DartShotgunTransmute2.AddCustomShimmerResult(ModContent.ItemType<DartShotgun>());
        DartShotgunTransmute2.AddCondition(ShimmerCraftCondition.ShimmerOnly);
        DartShotgunTransmute2.Register();
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
                tooltipLine.Text = "Bloody Amulet";
            }
        }
        if (tooltipLine != null && ModContent.GetInstance<AvalonConfig>().VanillaRenames)
        {
            if (item.type == ItemID.CoinGun)
            {
                tooltipLine.Text = "Spend Shot";
            }
            if (item.type == ItemID.PurpleMucos)
            {
                tooltipLine.Text = "Purple Mucus";
            }
            if (item.type == ItemID.HighTestFishingLine)
            {
                tooltipLine.Text = tooltipLine.Text.Replace("Test", "Tensile");
            }
            if (item.type == ItemID.BlueSolution)
            {
                tooltipLine.Text = "Cyan Solution";
            }
            if (item.type == ItemID.DarkBlueSolution)
            {
                tooltipLine.Text = "Blue Solution";
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
                        tooltip.Text = "Can mine Rhodium, Osmium, and Iridium";
                    }
                }
                break;
            case ItemID.HighTestFishingLine:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = "Fishing line will never break\nWorks in the vanity slot";
                    }
                }
                break;
            case ItemID.Seed:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = "For use with Blowpipes";
                    }
                }
                break;
            case ItemID.TempleKey:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = "Opens the lihzahrd temple door";
                    }
                }
                break;
            case ItemID.PoisonDart:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip1")
                    {
                        tooltip.Text = "For use with Blowpipes and Blowgun";
                    }
                }
                break;
            case ItemID.CoinGun:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = "Uses coins for ammo - Higher valued coins do more damage";
                    }
                    if (tooltip.Name == "Tooltip1")
                    {
                        tooltip.Text = "'Knocks some cents into your enemies'";
                    }
                }
                break;
            case ItemID.PickaxeAxe:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = "'Not to be confused with a hamdrill'";
                    }
                    if (tooltip.Name == "Tooltip1")
                    {
                        tooltip.Text = "Can mine Chlorophyte, Xanthophyte, and Caesium Ore";
                    }
                }
                break;
            case ItemID.Drax:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = "'Not to be confused with a picksaw'";
                    }
                    if (tooltip.Name == "Tooltip1")
                    {
                        tooltip.Text = "Can mine Chlorophyte, Xanthophyte, and Caesium Ore";
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
