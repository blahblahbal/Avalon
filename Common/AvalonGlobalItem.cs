using ExxoAvalonOrigins.Items.Material;
using ExxoAvalonOrigins.Items.Weapons.Melee.PreHardmode;
using ExxoAvalonOrigins.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Common;

public class AvalonGlobalItem : GlobalItem
{
    public override void AddRecipes()
    {
        // --== Shimmer!!! ==--
        ModContent.GetInstance<DesertLongsword>().CreateRecipe()
            .AddIngredient(ItemID.AntlionClaw)
            .AddCondition(Recipe.Condition.CorruptWorld)
            .AddCondition(Recipe.Condition.CrimsonWorld)
            .Register();
        Recipe MandibleBladeTransmute = Recipe.Create(ItemID.AntlionClaw);
        MandibleBladeTransmute.AddIngredient(ModContent.ItemType<DesertLongsword>());
        MandibleBladeTransmute.AddCondition(Recipe.Condition.CorruptWorld);
        MandibleBladeTransmute.AddCondition(Recipe.Condition.CrimsonWorld);
        MandibleBladeTransmute.Register();
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
                tooltipMat.Text = Language.GetTextValue("Mods.ExxoAvalonOrigins.CommonItemTooltip.TomeMaterial");
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
}
