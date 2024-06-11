using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using System;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Drawing;
using Avalon.Particles;

namespace Avalon.Tiles.Furniture
{
    public class StarTorch : SpecialLight
    {
        public override Vector3 LightColor => new Vector3(1f, 0.945f, 0.2f);
        public override int TorchItem => ModContent.ItemType<Items.Placeable.Furniture.StarTorch>();
        public override int dustType => DustID.YellowTorch;
        public override bool WaterDeath => false;

        public override void SetStaticDefaults()
        {
            RegisterItemDrop(TorchItem);

            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileSolid[Type] = false;
            //Main.tileNoAttach[Type] = true;
            Main.tileNoFail[Type] = true;
            Main.tileWaterDeath[Type] = false;
            //TileID.Sets.Torch[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.StyleTorch);
            TileObjectData.newTile.WaterPlacement = (LiquidPlacement)Convert.ToInt32(WaterDeath);
            TileObjectData.newTile.WaterDeath = WaterDeath;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.EmptyTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
            TileObjectData.newAlternate.WaterPlacement = (LiquidPlacement)Convert.ToInt32(WaterDeath);
            TileObjectData.newAlternate.WaterDeath = WaterDeath;
            TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile | AnchorType.EmptyTile, TileObjectData.newTile.Height, 0);
            TileObjectData.newAlternate.AnchorAlternateTiles = new int[7] {
                124,
                561,
                574,
                575,
                576,
                577,
                578
            };
            TileObjectData.addAlternate(1);
            TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
            TileObjectData.newAlternate.WaterPlacement = (LiquidPlacement)Convert.ToInt32(WaterDeath);
            TileObjectData.newAlternate.WaterDeath = WaterDeath;
            TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile | AnchorType.EmptyTile, TileObjectData.newTile.Height, 0);
            TileObjectData.newAlternate.AnchorAlternateTiles = new int[7] {
                124,
                561,
                574,
                575,
                576,
                577,
                578
            };
            TileObjectData.addAlternate(2);
            TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
            TileObjectData.newAlternate.WaterPlacement = (LiquidPlacement)Convert.ToInt32(WaterDeath);
            TileObjectData.newAlternate.WaterDeath = WaterDeath;
            //TileObjectData.newAlternate.AnchorWall = true;
            TileObjectData.addAlternate(0);
            TileObjectData.addTile(Type);
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
            AddMapEntry(new Color(253, 221, 3), Language.GetText("ItemName.Torch"));
            //DustType = DustID.JungleSpore;
            AdjTiles = new int[] { TileID.Torches };
        }

        public override bool CreateDust(int i, int j, ref int type)
        {
            type = dustType;
            return true;
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
			if (Main.rand.NextBool(40) && Main.tile[i, j].TileFrameX < 66 && Main.hasFocus)
			{
				//ParticleOrchestraSettings pos = new ParticleOrchestraSettings();
				//pos.PositionInWorld = new Vector2(i, j) * 16;
				//pos.MovementVector = new Vector2(Main.rand.Next(-2, 3), Main.rand.Next(1, 4));
				////pos.IndexOfPlayerWhoInvokedThis = 255;
				//ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.SilverBulletSparkle, pos);
				ParticleSystem.AddParticle(new Particles.StarTorch(),
					new Vector2(i, j) * 16 + new Vector2(Main.rand.Next(5, 16), Main.rand.Next(-1, 4)), // position
					new Vector2(Main.rand.NextFloat(-0.2f, 0.21f), Main.rand.NextFloat(0.3f, 0.5f)), // velocity
					Color.LightYellow, // color
					scale: 0.12f); // scale
			}
            if (Main.rand.NextBool(40) && Main.tile[i, j].TileFrameX < 66)
            {
                Dust d = Dust.NewDustDirect(new Vector2(i * 16, j * 16) + new Vector2(6, -6), 0, 0, dustType, 0, 0, 100, default, Main.rand.NextFloat(0.5f, 1));
                if (!Main.rand.NextBool(3))
                {
                    d.noGravity = NoDustGravity;
                }
                d.noLightEmittence = true;
                d.velocity *= 0.3f;
                d.velocity.Y -= 1.5f;
                d.noGravity = false;
            }
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = Main.rand.Next(10, 15);
        }
        public override bool TileFrame(int x, int y, ref bool resetFrame, ref bool noBreak)
        {
            Tile tile = Main.tile[x, y];
            Tile tile3 = Main.tile[x, y + 1];
            Tile tile4 = Main.tile[x - 1, y];
            Tile tile5 = Main.tile[x + 1, y];
            Tile tile6 = Main.tile[x - 1, y + 1];
            Tile tile7 = Main.tile[x + 1, y + 1];
            Tile tile8 = Main.tile[x - 1, y - 1];
            Tile tile9 = Main.tile[x + 1, y - 1];
            short num = 0;
            if (tile.TileFrameX >= 66)
                num = 66;

            int num2 = -1;
            int num3 = -1;
            int num4 = -1;
            int tree = -1;
            int tree2 = -1;
            int tree3 = -1;
            int tree4 = -1;

            if (tile3.HasTile && ((TileID.Sets.Platforms[tile3.TileType] && WorldGen.TopEdgeCanBeAttachedTo(x, y + 1)) || (!tile3.IsHalfBlock && !tile3.TopSlope)))
                num2 = tile3.TileType;

            if (tile4.HasTile && (tile4.Slope == 0 || (int)tile4.Slope % 2 != 1))
                num3 = tile4.TileType;

            if (tile5.HasTile && (tile5.Slope == 0 || (int)tile5.Slope % 2 != 0))
                num4 = tile5.TileType;

            if (tile6.HasTile)
                tree = tile6.TileType;

            if (tile7.HasTile)
                tree2 = tile7.TileType;

            if (tile8.HasTile)
                tree3 = tile8.TileType;

            if (tile9.HasTile)
                tree4 = tile9.TileType;

            if (num2 >= 0 && Main.tileSolid[num2] && (!Main.tileNoAttach[num2] || TileID.Sets.Platforms[num2]))
                tile.TileFrameX = num;
            else if ((num3 >= 0 && Main.tileSolid[num3] && !Main.tileNoAttach[num3]) || (num3 >= 0 && TileID.Sets.IsBeam[num3]) || (WorldGen.IsTreeType(num3) && WorldGen.IsTreeType(tree3) && WorldGen.IsTreeType(tree)))
                tile.TileFrameX = (short)(22 + num);
            else if ((num4 >= 0 && Main.tileSolid[num4] && !Main.tileNoAttach[num4]) || (num4 >= 0 && TileID.Sets.IsBeam[num4]) || (WorldGen.IsTreeType(num4) && WorldGen.IsTreeType(tree4) && WorldGen.IsTreeType(tree2)))
                tile.TileFrameX = (short)(44 + num);
            else if (tile.WallType > 0)
                tile.TileFrameX = num;
            else
                tile.TileFrameX = num;
            return false;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            var tile = Main.tile[i, j];
            if (tile.TileFrameX < 66)
            {
                r = LightColor.X;
                g = LightColor.Y;
                b = LightColor.Z;
            }
        }
        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
        {
            offsetY = 0;
            if (WorldGen.InWorld(i, j - 1) && WorldGen.SolidTile(i, j - 1))
            {
                offsetY = 2;
                if (WorldGen.InWorld(i - 1, j + 1) && WorldGen.SolidTile(i - 1, j + 1) || WorldGen.InWorld(i + 1, j + 1) && WorldGen.SolidTile(i + 1, j + 1))
                {
                    offsetY = 4;
                }
            }
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            var randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)(ulong)i);
            var color = new Color(100, 100, 100, 0);
            int frameX = Main.tile[i, j].TileFrameX;
            int frameY = Main.tile[i, j].TileFrameY;
            var width = 20;
            var offsetY = 0;
            var height = 20;
            if (WorldGen.SolidTile(i, j - 1))
            {
                offsetY = 2;
                if (WorldGen.SolidTile(i - 1, j + 1) || WorldGen.SolidTile(i + 1, j + 1))
                {
                    offsetY = 4;
                }
            }
            var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            for (var k = 0; k < 7; k++)
            {
                var x = Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
                var y = Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;
                Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_Flame").Value, new Vector2(i * 16 - (int)Main.screenPosition.X - (width - 16f) / 2f + x, j * 16 - (int)Main.screenPosition.Y + offsetY + y) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            }
        }
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = TorchItem;
        }
        public override bool RightClick(int i, int j)
        {
            WorldGen.KillTile(i, j, false, false, false);
            if (!Main.tile[i, j].HasTile && Main.netMode != NetmodeID.SinglePlayer)
            {
                NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, i, j);
            }
            return true;
        }
    }
}
