using Avalon.Dusts;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Avalon;
using Avalon.NPCs.Contagion;

namespace Avalon.Tiles.Contagion;

public class ContagionPalmTree : ModPalmTree
{
	private Asset<Texture2D> texture;
	private Asset<Texture2D> oasisTopsTexture;
	private Asset<Texture2D> topsTexture;
	public override TreePaintingSettings TreeShaderSettings => new();

	public override void SetStaticDefaults()
	{
		GrowsOnTileId = new[] { ModContent.TileType<Snotsand>() };
		texture = ModContent.Request<Texture2D>("Avalon/Tiles/Contagion/ContagionPalmTree");
		oasisTopsTexture = ModContent.Request<Texture2D>("Avalon/Tiles/Contagion/ContagionOasisTree_Tops");
		topsTexture = ModContent.Request<Texture2D>("Avalon/Tiles/Contagion/ContagionPalmTreeTop");
	}

    public override Asset<Texture2D> GetOasisTopTextures() => oasisTopsTexture;

    public override Asset<Texture2D> GetTexture() => texture;

    public override Asset<Texture2D> GetTopTextures() => topsTexture;
    public override int TreeLeaf() => ModContent.GoreType<ContagionTreeLeaf>();

    public override int DropWood() => ModContent.ItemType<Items.Placeable.Tile.Coughwood>();

    public override int CreateDust() => ModContent.DustType<CoughwoodDust>();
    //public override int TreeLeaf() => ModContent.Find<ModGore>("Avalon/ContagionTreeLeaf").Type;
    public override bool Shake(int x, int y, ref bool createLeaves)
    {
        if (Main.getGoodWorld && Main.rand.NextBool(17))
        {
            Projectile.NewProjectile(new EntitySource_ShakeTree(x, y), x * 16, y * 16, Main.rand.Next(-100, 101) * 0.002f, 0f, ProjectileID.Bomb, 0, 0f, Main.myPlayer, 16f, 16f);
        }
        else if (Main.rand.NextBool(35) && Main.halloween)
        {
            Item.NewItem(new EntitySource_ShakeTree(x, y), x * 16, y * 16, 16, 16, ItemID.RottenEgg, Main.rand.Next(1, 3));
        }
        else if (Main.rand.NextBool(12))
        {
            Item.NewItem(new EntitySource_ShakeTree(x, y), x * 16, y * 16, 16, 16, ModContent.ItemType<Items.Placeable.Tile.Coughwood>(), Main.rand.Next(1, 4));
        }
        else if (Main.rand.NextBool(20))
        {
            int type = ItemID.CopperCoin;
            int chance = Main.rand.Next(50, 100);
            if (Main.rand.NextBool(30))
            {
                type = ItemID.GoldCoin;
                chance = 1;
                if (Main.rand.NextBool(5))
                {
                    chance++;
                }
                if (Main.rand.NextBool(10))
                {
                    chance++;
                }
            }
            else if (Main.rand.NextBool(10))
            {
                type = ItemID.SilverCoin;
                chance = Main.rand.Next(1, 21);
                if (Main.rand.NextBool(3))
                {
                    chance += Main.rand.Next(1, 21);
                }
                if (Main.rand.NextBool(4))
                {
                    chance += Main.rand.Next(1, 21);
                }
            }
            Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), x * 16, y * 16, 16, 16, type, chance);
        }
        else if (Main.rand.NextBool(20) && !WorldGen.IsPalmOasisTree(x))
        {
            NPC.NewNPC(new EntitySource_ShakeTree(x, y), x * 16, y * 16, NPCID.Seagull2);
        }
        else if (Main.rand.NextBool(30))
        {
            NPC.NewNPC(new EntitySource_ShakeTree(x, y), x * 16 + 8, (y - 1) * 16, ModContent.NPCType<Bactus>());
        }
        else if (Main.rand.NextBool(12))
        {
            Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16, !Main.rand.NextBool(2) ? ModContent.ItemType<Items.Food.Durian>() : ModContent.ItemType<Items.Food.Medlar>());
        }
        return false;
    }
    public override int SaplingGrowthType(ref int style)
    {
        style = 0;
        return ModContent.TileType<ContagionPalmSapling>();
    }
}
