using ExxoAvalonOrigins.Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Consumables
{
    public class EnergyCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.AegisCrystal);
            Item.Size = new Vector2(16, 16);
        }
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<AvalonStaminaPlayer>().EnergyCrystal = true;
            return base.UseItem(player);
        }
    }
}
