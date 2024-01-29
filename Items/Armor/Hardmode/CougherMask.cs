using Avalon.Buffs.Debuffs;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Head)]
class CougherMask : ModItem
{
    static SoundStyle Cough = new SoundStyle($"{nameof(Avalon)}/Sounds/NPC/CougherCough")
    {
        Volume = 0.8f,
        Pitch = Main.rand.Next(-1, 1),
        PitchVariance = 0.1f,
        MaxInstances = 10,
    };
    public override bool IsLoadingEnabled(Mod mod)
    {
        return false;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 2);
        Item.height = dims.Height;
        Item.defense = 8;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var thing = new TooltipLine(Mod, "Tooltip0", Language.GetTextValue("Mods.Avalon.CougherMask.Tooltip", Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN")));
        tooltips.Add(thing);
    }
    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<AvalonPlayer>().CougherMask = true;
        if (player.PlayerDoublePressedSetBonusActivateKey() && !player.GetModPlayer<AvalonPlayer>().CoughCooldown)
        {
            if (Main.rand.NextBool(300))
            {
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/NPC/CougherCoughSpecial")
                {
                    Volume = 0.8f,
                    Pitch = Main.rand.NextFloat(-0.3f, 0.3f),
                    PitchVariance = 0.1f,
                    MaxInstances = 10,
                }, player.Center);
            }
            else
            {
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/NPC/CougherCough")
                {
                    Volume = 0.8f,
                    Pitch = Main.rand.NextFloat(-0.3f, 0.3f),
                    PitchVariance = 0.1f,
                    MaxInstances = 10,
                }, player.Center);
            }
            Vector2 CoughSpawnPoint = player.MountedCenter;
            int proj = Projectile.NewProjectile(player.GetSource_Accessory(Item),
                CoughSpawnPoint, CoughSpawnPoint.DirectionTo(Main.MouseWorld) * new Vector2(6,3), ModContent.ProjectileType<Projectiles.PlayerCough>(), 0, 0, player.whoAmI);
            player.AddBuff(ModContent.BuffType<CoughCooldown>(),1800);
            //player.GetModPlayer<AvalonPlayer>().CoughCooldown = 1800 / 30;
        }
    }
}
