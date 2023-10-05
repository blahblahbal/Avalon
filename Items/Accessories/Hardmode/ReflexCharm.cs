using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

internal class ReflexCharm : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 2;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 1, 8);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.CobaltShield)
            .AddIngredient(ItemID.SoulofSight, 8)
            .AddIngredient(ItemID.LightShard, 3)
            .AddTile(TileID.TinkerersWorkbench).Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        var playerWS = new Rectangle((int)player.Center.X - 32, (int)player.Center.Y - 32, 64, 64);
        foreach (Projectile Pr in Main.projectile)
        {
            if (!Pr.friendly && !Pr.bobber && !Data.Sets.Projectile.DontReflect[Pr.type])
            {
                var proj2 = new Rectangle((int)Pr.position.X, (int)Pr.position.Y, Pr.width, Pr.height);
                bool reflect = false, check = false;
                int rn = Main.rand.Next(4);
                if (rn == 0)
                {
                    if (proj2.Intersects(playerWS) && !reflect)
                    {
                        reflect = true;
                    }
                }
                else
                {
                    check = true;
                }

                if (reflect && !check)
                {
                    for (int thingy = 0; thingy < 5; thingy++)
                    {
                        int dust = Dust.NewDust(Pr.position, Pr.width, Pr.height, DustID.MagicMirror, 0f, 0f, 100);
                        Main.dust[dust].noGravity = true;
                    }

                    Pr.hostile = false;
                    Pr.friendly = true;
                    Pr.velocity.X *= -1f;
                    Pr.velocity.Y *= -1f;
                }
            }
        }

        foreach (NPC N in Main.npc)
        {
            if (!N.friendly && N.aiStyle == 9)
            {
                var npc = new Rectangle((int)N.position.X, (int)N.position.Y, N.width, N.height);
                bool reflect = false, check = false;
                int rn = Main.rand.Next(4);
                if (rn == 0)
                {
                    if (npc.Intersects(playerWS) && !reflect)
                    {
                        reflect = true;
                    }
                }
                else
                {
                    check = true;
                }

                if (reflect && !check)
                {
                    for (int varlex = 0; varlex < 5; varlex++)
                    {
                        int dust = Dust.NewDust(N.position, N.width, N.height, DustID.MagicMirror, 0f, 0f, 100);
                        Main.dust[dust].noGravity = true;
                    }

                    N.friendly = true;
                    N.velocity.X *= -1f;
                    N.velocity.Y *= -1f;
                }
            }
        }
    }
}
