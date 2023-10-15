using System.Collections.Generic;
using System.Linq;
using Avalon.Data;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon.Common.Players;

public class AvalonHerbologyPlayer : ModPlayer
{
    public enum HerbTier
    {
        Novice = 0,
        Apprentice = 1,
        Expert = 2,
        Master = 3,
    }

    public Dictionary<int, int> HerbCounts { get; private set; } = new();
    public Dictionary<int, bool> HerbExchangeUnlocked { get; private set; } = new()
    {
        { ItemID.Daybloom, false },
        { ItemID.Moonglow, false },
        { ItemID.Blinkroot, false },
        { ItemID.Deathweed, false },
        { ItemID.Waterleaf, false },
        { ItemID.Fireblossom, false },
        { ItemID.Shiverthorn, false }
    };
    public HerbTier Tier { get; private set; }
    public int HerbTotal { get; private set; }
    public int HerbX { get; set; }
    public int HerbY { get; set; }
    public int PotionTotal { get; private set; }

    public bool DisplayHerbologyMenu { get; set; }

    /// <inheritdoc />
    public override void PostUpdate()
    {
        // Herbology bench distance check
        if (DisplayHerbologyMenu)
        {
            UpdateHerbTier();
            int num9 = (int)((Player.position.X + (Player.width * 0.5)) / 16.0);
            int num10 = (int)((Player.position.Y + (Player.height * 0.5)) / 16.0);
            if (num9 < HerbX - Player.lastTileRangeX || num9 > HerbX + Player.lastTileRangeX + 1 ||
                num10 < HerbY - Player.lastTileRangeY || num10 > HerbY + Player.lastTileRangeY + 1)
            {
                SoundEngine.PlaySound(SoundID.MenuClose);
                DisplayHerbologyMenu = false;
                Player.dropItemCheck();
            }
        }

        if (!Main.playerInventory)
        {
            DisplayHerbologyMenu = false;
        }
    }

    public bool PurchaseItem(Item item, int amount)
    {
        int charge = HerbologyData.GetItemCost(item, amount);

        if (charge <= 0)
        {
            return false;
        }

        if (HerbologyData.ItemIsHerb(item))
        {
            int herbType = ItemID.None;
            bool chargeInventory = false;
            if (HerbologyData.LargeHerbSeedIdByHerbSeedId.ContainsKey(item.type))
            {
                herbType = HerbologyData.HerbIdByLargeHerbId[
                    HerbologyData.LargeHerbIdByLargeHerbSeedId[HerbologyData.LargeHerbSeedIdByHerbSeedId[item.type]]];
            }
            else if (HerbologyData.LargeHerbSeedIdByHerbSeedId.ContainsValue(item.type)) // item.type is the seed ID
            {
                chargeInventory = true;
                // herbType is now the herb ID for the above seed
                herbType = HerbologyData.HerbIdByLargeHerbId[HerbologyData.LargeHerbIdByLargeHerbSeedId[item.type]];
            }

            if (HerbTotal < charge || !HerbExchangeUnlocked[herbType])
            {
                return false;
            }

            if (chargeInventory)
            {
                if (herbType != ItemID.None && HerbCounts.ContainsKey(herbType) &&
                    HerbCounts[herbType] > charge && HerbExchangeUnlocked[herbType])
                {
                    HerbCounts[herbType] -= charge;
                }
                else
                {
                    return false;
                }
            }

            HerbTotal -= charge;
            Main.mouseItem = item.Clone();
            Main.mouseItem.stack = amount;
            return true;
        }

        if (PotionTotal < charge)
        {
            return false;
        }

        PotionTotal -= charge;
        Main.mouseItem = item.Clone();
        Main.mouseItem.stack = amount;
        return true;
    }

    public bool SellItem(Item item)
    {
        if (item.stack <= 0 || item.type == ItemID.None)
        {
            return false;
        }

        int herbAddition = 0;
        int herbType = ItemID.None;
        if (HerbologyData.HerbIdByLargeHerbId.ContainsValue(item.type))
        {
            herbAddition = HerbologyData.HerbSellPrice;
            herbType = item.type;
        }
        else if (HerbologyData.LargeHerbSeedIdByHerbId.ContainsValue(item.type))
        {
            herbAddition = HerbologyData.LargeHerbSeedSellPrice;
            herbType = HerbologyData.HerbIdByLargeHerbId[HerbologyData.LargeHerbIdByLargeHerbSeedId[item.type]];
        }
        else if (HerbologyData.LargeHerbIdByLargeHerbSeedId.ContainsValue(item.type))
        {
            herbAddition = HerbologyData.LargeHerbSellPrice;
            herbType = HerbologyData.HerbIdByLargeHerbId[item.type];
        }

        if (herbAddition > 0 && herbType != ItemID.None)
        {
            if (!HerbCounts.ContainsKey(herbType))
            {
                HerbCounts.Add(herbType, 0);
                
            }

            HerbCounts[herbType] += herbAddition * item.stack;
            HerbTotal += herbAddition * item.stack;

            if (HerbCounts[herbType] > 50)
            {
                HerbExchangeUnlocked[herbType] = true;
            }
        }

        int potionAddition = 0;

        
        if (HerbologyData.PotionIds.Contains(item.type))
        {
            potionAddition = HerbologyData.PotionSellPrice;
        }
        else if (HerbologyData.ElixirIds.Contains(item.type))
        {
            potionAddition = HerbologyData.ElixirSellPrice;
        }
        //else if (item.type == ModContent.ItemType<BlahPotion>())
        //{
        //    potionAddition = HerbologyData.BlahPotionSellPrice;
        //}

        if (item.type is ItemID.HealingPotion or ItemID.ManaPotion)
        {
            potionAddition = 1;
        }
        else if (HerbologyData.RestorationIDs.Contains(item.type) && item.type != ItemID.HealingPotion && item.type != ItemID.ManaPotion)
        {
            potionAddition = HerbologyData.RestorationPotionCost;
        }
        if (HerbologyData.SuperRestorationIDs.Contains(item.type))
        {
            potionAddition = HerbologyData.RestorationPotionCost * 3;
        }

        if (potionAddition > 0)
        {
            PotionTotal += potionAddition * item.stack;
        }

        UpdateHerbTier();

        PopupText.NewText(PopupTextContext.Advanced, item, item.stack);
        SoundEngine.PlaySound(new SoundStyle("Avalon/Sounds/Item/HerbConsume"));
        return true;
    }

    public void UpdateHerbTier()
    {
        HerbTier newHerbTier;
        //if (HerbTotal >= HerbologyData.HerbTier4Threshold && Main.hardMode &&
        //    ModContent.GetInstance<AvalonWorld>().SuperHardmode)
        //{
        //    newHerbTier = HerbTier.Master; // tier 4; Blah Potion exchange
        //}
        //else
        if (HerbTotal >= HerbologyData.HerbTier3Threshold && Main.hardMode)
        {
            newHerbTier = HerbTier.Expert; // tier 3; allows you to obtain elixirs
        }
        else if (HerbTotal >= HerbologyData.HerbTier2Threshold)
        {
            newHerbTier = HerbTier.Apprentice; // tier 2; allows for large herb seeds
        }
        else
        {
            newHerbTier =
                HerbTier.Novice; // tier 1; allows for exchanging one herb for another
        }

        if (newHerbTier > Tier)
        {
            Tier = newHerbTier;
        }
    }

    /// <inheritdoc />
    public override void SaveData(TagCompound tag)
    {
        tag["Avalon:Tier"] = (int)Tier;
        tag["Avalon:HerbTotal"] = HerbTotal;
        tag["Avalon:PotionTotal"] = PotionTotal;
        tag["Avalon:HerbCounts"] = HerbCounts.Save();
        tag["Avalon:HerbExchangeUnlocked"] = HerbExchangeUnlocked.Save();
    }

    /// <inheritdoc />
    public override void LoadData(TagCompound tag)
    {
        if (tag.ContainsKey("Avalon:Tier"))
        {
            Tier = (HerbTier)tag.GetAsInt("Avalon:Tier");
        }
        if (tag.ContainsKey("Avalon:HerbTotal"))
        {
            HerbTotal = tag.GetAsInt("Avalon:HerbTotal");
        }
        if (tag.ContainsKey("Avalon:PotionTotal"))
        {
            PotionTotal = tag.GetAsInt("Avalon:PotionTotal");
        }
        if (tag.ContainsKey("Avalon:HerbCounts"))
        {
            try
            {
                HerbCounts.Load(tag.Get<TagCompound>("Avalon:HerbCounts"));
            }
            catch
            {
                HerbCounts = new Dictionary<int, int>();
            }
        }
        if (tag.ContainsKey("Avalon:HerbExchangeUnlocked"))
        {
            try
            {
                HerbExchangeUnlocked.Load(tag.Get<TagCompound>("Avalon:HerbExchangeUnlocked"));
            }
            catch
            {
                HerbExchangeUnlocked = new Dictionary<int, bool>()
                {
                    { ItemID.Daybloom, false },
                    { ItemID.Moonglow, false },
                    { ItemID.Blinkroot, false },
                    { ItemID.Deathweed, false },
                    { ItemID.Waterleaf, false },
                    { ItemID.Fireblossom, false },
                    { ItemID.Shiverthorn, false }
                };
            }
        }
    }
}
