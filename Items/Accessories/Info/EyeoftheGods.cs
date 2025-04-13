using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Info;

class EyeoftheGods : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = 34;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 2, 0, 0);
        Item.height = 32;
    }
    public override void UpdateInfoAccessory(Player player)
    {
        player.GetModPlayer<EyeoftheGodsPlayer>().DamageDisplay = true;
        player.GetModPlayer<EyeoftheGodsPlayer>().DefenseDisplay = true;
    }
}
class EyeoftheGodsPlayer : ModPlayer
{
    public bool DefenseDisplay;
    public bool DamageDisplay;

    public int DisplayTimer;
    public int NPCIndex = -1;

    public override void ResetInfoAccessories()
    {
        DefenseDisplay = false;
        DamageDisplay = false;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (target.justHit && hit.DamageType != DamageClass.Generic && damageDone > 0)
        {
            NPCIndex = target.whoAmI;
            target.GetGlobalNPC<AvalonGlobalNPCInstance>().ShowStats = true;
            DisplayTimer = 2400;
        }
    }
    public override void PostUpdate()
    {
        if (NPCIndex > -1 && DisplayTimer == 0)
        {
            if (!Main.npc[NPCIndex].active)
            {
                NPCIndex = -1;
            }
        }
        
        DisplayTimer--;
        if (DisplayTimer < 0)
        {
            //NPCIndex = -1;
            DisplayTimer = 0;
        }
	}
    public override void RefreshInfoAccessoriesFromTeamPlayers(Player otherPlayer)
    {
        if (otherPlayer.GetModPlayer<EyeoftheGodsPlayer>().DefenseDisplay)
        {
            DefenseDisplay = true;
        }
        if (otherPlayer.GetModPlayer<EyeoftheGodsPlayer>().DamageDisplay)
        {
            DamageDisplay = true;
        }
    }
}
class EyeoftheGodsDefInfoDisplay : InfoDisplay
{
    public override string HoverTexture => Texture + "_Hover";
    public override bool Active()
    {
        return Main.LocalPlayer.GetModPlayer<EyeoftheGodsPlayer>().DefenseDisplay;
    }
    public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
    {
        int npc = -1;
        string info = "";

        if (Main.LocalPlayer.GetModPlayer<EyeoftheGodsPlayer>().NPCIndex > -1)
        {
            if (Main.npc[Main.LocalPlayer.GetModPlayer<EyeoftheGodsPlayer>().NPCIndex].GetGlobalNPC<AvalonGlobalNPCInstance>().ShowStats)
            {
                info += Main.npc[Main.LocalPlayer.GetModPlayer<EyeoftheGodsPlayer>().NPCIndex].TypeName;
                info += Language.GetTextValue("Mods.Avalon.InfoDisplays.Defense") + Main.npc[Main.LocalPlayer.GetModPlayer<EyeoftheGodsPlayer>().NPCIndex].defDefense;
            }
            else
            {
                displayColor = InactiveInfoTextColor;
                info = Language.GetTextValue("Mods.Avalon.InfoDisplays.NoInfo");
            }
            
            //Main.LocalPlayer.GetModPlayer<EyeoftheGodsPlayer>().NPCIndex = -1;
        }
        else
        {
            displayColor = InactiveInfoTextColor;
            info = Language.GetTextValue("Mods.Avalon.InfoDisplays.NoInfo");
        }
        return info;
    }
}
class EyeoftheGodsDmgInfoDisplay : InfoDisplay
{
    public override string HoverTexture => Texture + "_Hover";
    public override bool Active()
    {
        return Main.LocalPlayer.GetModPlayer<EyeoftheGodsPlayer>().DamageDisplay;
    }

    public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
    {
        //int npc = -1;
        string info = "";
        //for (int n = 0; n < Main.npc.Length; n++)
        //{
        //    if (Main.npc[n].justHit && Main.npc[n].target == Main.myPlayer)
        //    {
        //        npc = n;
        //        break;
        //    }
        //}

        if (Main.LocalPlayer.GetModPlayer<EyeoftheGodsPlayer>().NPCIndex > -1)
        {
            if (Main.npc[Main.LocalPlayer.GetModPlayer<EyeoftheGodsPlayer>().NPCIndex].GetGlobalNPC<AvalonGlobalNPCInstance>().ShowStats)
            {
                info += Main.npc[Main.LocalPlayer.GetModPlayer<EyeoftheGodsPlayer>().NPCIndex].TypeName;
                info += Language.GetTextValue("Mods.Avalon.InfoDisplays.Damage") + Main.npc[Main.LocalPlayer.GetModPlayer<EyeoftheGodsPlayer>().NPCIndex].defDamage;
            }
            else
            {
                displayColor = InactiveInfoTextColor;
                info = Language.GetTextValue("Mods.Avalon.InfoDisplays.NoInfo");
            }
        }
        else
        {
            displayColor = InactiveInfoTextColor;
            info = Language.GetTextValue("Mods.Avalon.InfoDisplays.NoInfo");
        }
        return info;
    }
}
