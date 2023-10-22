using Avalon.NPCs.Hardmode;
using Avalon.NPCs.PreHardmode;
using Avalon.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class MonsterBanner : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
        TileObjectData.newTile.Height = 3;
        TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom | AnchorType.PlanterBox, TileObjectData.newTile.Width, 0);
        TileObjectData.newTile.StyleWrapLimit = 111;
        TileObjectData.newTile.DrawYOffset = -2;
        TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
        TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.Platform, TileObjectData.newTile.Width, 0);
        TileObjectData.newAlternate.DrawYOffset = -10;
        TileObjectData.addAlternate(0);
        TileObjectData.addTile(Type);
        DustType = -1;
        TileID.Sets.DisableSmartCursor[Type] = true;
        AddMapEntry(new Color(13, 88, 130));
    }
    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        Tile tile = Main.tile[i, j];
        int topLeftX = i - tile.TileFrameX / 18 % 1;
        int topLeftY = j - tile.TileFrameY / 18 % 3;
        if (WorldGen.IsBelowANonHammeredPlatform(topLeftX, topLeftY))
        {
            offsetY -= -2;
        }
    }
    public override void NearbyEffects(int i, int j, bool closer)
    {
        if (closer)
        {
            Player player = Main.LocalPlayer;
            int style = Main.tile[i, j].TileFrameX / 18;
            int t = 1;
            switch (style)
            {
                //case 0:
                //    t = ModContent.NPCType<NPCs.Mime>();
                //    break;
                //case 1:
                //    t = ModContent.NPCType<NPCs.DarkMatterSlime>();
                //    break;
                //case 2:
                //    t = ModContent.NPCType<NPCs.CursedMagmaSkeleton>();
                //    break;
                //case 4:
                //    t = ModContent.NPCType<NPCs.VampireHarpy>();
                //    break;
                //case 5:
                //    t = ModContent.NPCType<NPCs.ArmoredWraith>();
                //    break;
                //case 6:
                //    t = ModContent.NPCType<NPCs.RedAegisBonesHelmet>();
                //    break;
                case 7:
                    t = ModContent.NPCType<BloodshotEye>();
                    break;
                //case 8:
                //    t = ModContent.NPCType<NPCs.Dragonfly>();
                //    break;
                case 9:
                    t = ModContent.NPCType<Blaze>();
                    break;
                //case 10:
                //    t = ModContent.NPCType<NPCs.ArmoredHellTortoise>();
                //    break;
                //case 13:
                //    t = ModContent.NPCType<NPCs.ImpactWizard>();
                //    break;
                //case 14:
                //    t = ModContent.NPCType<NPCs.MechanicalDiggerHead>();
                //    break;
                case 15:
                    t = ModContent.NPCType<Cougher>();
                    break;
                case 16:
                    t = ModContent.NPCType<Bactus>();
                    break;
                case 17:
                    t = ModContent.NPCType<Ickslime>();
                    break;
                //case 18:
                //    t = ModContent.NPCType<NPCs.GrossyFloat>();
                //    break;
                //case 19:
                //    t = ModContent.NPCType<NPCs.PyrasiteHead>();
                //    break;
                //case 20:
                //    t = ModContent.NPCType<NPCs.EyeBones>();
                //    break;
                //case 21:
                //    t = ModContent.NPCType<NPCs.Ectosphere>();
                //    break;
                //case 22:
                //    t = ModContent.NPCType<NPCs.BombSkeleton>();
                //    break;
                //case 23:
                //    t = ModContent.NPCType<NPCs.CopperSlime>();
                //    break;
                //case 24:
                //    t = ModContent.NPCType<NPCs.TinSlime>();
                //    break;
                //case 25:
                //    t = ModContent.NPCType<NPCs.IronSlime>();
                //    break;
                //case 26:
                //    t = ModContent.NPCType<NPCs.LeadSlime>();
                //    break;
                //case 27:
                //    t = ModContent.NPCType<NPCs.SilverSlime>();
                //    break;
                //case 28:
                //    t = ModContent.NPCType<NPCs.TungstenSlime>();
                //    break;
                //case 29:
                //    t = ModContent.NPCType<NPCs.GoldSlime>();
                //    break;
                //case 30:
                //    t = ModContent.NPCType<NPCs.PlatinumSlime>();
                //    break;
                //case 31:
                //    t = ModContent.NPCType<NPCs.CobaltSlime>();
                //    break;
                //case 32:
                //    t = ModContent.NPCType<NPCs.PalladiumSlime>();
                //    break;
                //case 33:
                //    t = ModContent.NPCType<NPCs.MythrilSlime>();
                //    break;
                //case 34:
                //    t = ModContent.NPCType<NPCs.OrichalcumSlime>();
                //    break;
                //case 35:
                //    t = ModContent.NPCType<NPCs.AdamantiteSlime>();
                //    break;
                //case 36:
                //    t = ModContent.NPCType<NPCs.TitaniumSlime>();
                //    break;
                //case 37:
                //    t = ModContent.NPCType<NPCs.RhodiumSlime>();
                //    break;
                //case 38:
                //    t = ModContent.NPCType<NPCs.OsmiumSlime>();
                //    break;
                //case 39:
                //    t = ModContent.NPCType<NPCs.DurantiumSlime>();
                //    break;
                //case 40:
                //    t = ModContent.NPCType<NPCs.NaquadahSlime>();
                //    break;
                //case 41:
                //    t = ModContent.NPCType<NPCs.TroxiniumSlime>();
                //    break;
                //case 42:
                //    t = ModContent.NPCType<NPCs.UnstableAnomaly>();
                //    break;
                //case 43:
                //    t = ModContent.NPCType<NPCs.MatterMan>();
                //    break;
                //case 44:
                //    t = ModContent.NPCType<NPCs.BronzeSlime>();
                //    break;
                //case 45:
                //    t = ModContent.NPCType<NPCs.NickelSlime>();
                //    break;
                //case 46:
                //    t = ModContent.NPCType<NPCs.ZincSlime>();
                //    break;
                //case 47:
                //    t = ModContent.NPCType<NPCs.BismuthSlime>();
                //    break;
                //case 48:
                //    t = ModContent.NPCType<NPCs.IridiumSlime>();
                //    break;
                //case 49:
                //    t = ModContent.NPCType<NPCs.Hallowor>();
                //    break;
                case 51:
                    t = ModContent.NPCType<IrateBones>();
                    break;
                case 55:
                    t = ModContent.NPCType<CursedScepter>();
                    break;
                case 56:
                    t = ModContent.NPCType<EctoHand>();
                    break;
                //case 57:
                //    t = ModContent.NPCType<NPCs.CloudBat>();
                //    break;
                //case 58:
                //    t = ModContent.NPCType<NPCs.Valkyrie>();
                //    break;
                //case 59:
                //    t = ModContent.NPCType<NPCs.CaesiumSeekerHead>();
                //    break;
                //case 60:
                //    t = ModContent.NPCType<NPCs.CaesiumBrute>();
                //    break;
                //case 61:
                //    t = ModContent.NPCType<NPCs.CaesiumStalker>();
                //    break;
                //case 62:
                //    t = ModContent.NPCType<NPCs.Rafflesia>();
                //    break;
                //case 63:
                //    t = ModContent.NPCType<NPCs.PoisonDartFrog>();
                //    break;
                //case 64:
                //    t = ModContent.NPCType<NPCs.CometTail>();
                //    break;
                //case 65:
                //    t = ModContent.NPCType<NPCs.EvilVulture>();
                //    break;
                //case 66:
                //    t = ModContent.NPCType<NPCs.CrystalBones>();
                //    break;
                //case 67:
                //    t = ModContent.NPCType<NPCs.CrystalSpectre>();
                //    break;
                case 68:
                    t = ModContent.NPCType<CursedFlamer>();
                    break;
                case 69:
                    t = ModContent.NPCType<FallenHero>();
                    break;
                //case 70:
                //    t = ModContent.NPCType<NPCs.QuickCaribe>();
                //    break;
                //case 71:
                //    t = ModContent.NPCType<NPCs.VorazylcumMite>();
                //    break;
                //case 72:
                //    t = ModContent.NPCType<NPCs.UnvolanditeMite>();
                //    break;
                //case 73:
                //    t = ModContent.NPCType<NPCs.JuggernautSorcerer>();
                //    break;
                //case 74:
                //    t = 0; // ModContent.NPCType<NPCs.JuggernautSwordsman>();
                //    break;
                //case 75:
                //    t = 0; // ModContent.NPCType<NPCs.JuggernautBrute>();
                //    break;
                //case 76:
                //    t = ModContent.NPCType<NPCs.GuardianCorruptor>();
                //    break;
                //case 77:
                //    t = ModContent.NPCType<NPCs.TropicalSlime>();
                //    break;
                case 78:
                    t = ModContent.NPCType<ViralMummy>();
                    break;
                case 79:
                    t = ModContent.NPCType<Viris>();
                    break;
                case 80:
                    t = ModContent.NPCType<BoneFish>();
                    break;
                default:
                    t = 0;
                    return;
            }
            //Main.SceneMetrics.NPCBannerBuff[Mod.Find<ModNPC>(type).Type] = true;
            Main.SceneMetrics.NPCBannerBuff[t] = true;
            Main.SceneMetrics.hasBanner = true;
            //player.hasBannerBuff = true;
        }
    }

    public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
    {
        if (i % 2 == 1)
        {
            spriteEffects = SpriteEffects.FlipHorizontally;
        }
    }

    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
    {
        bool intoRenderTargets = true;
        bool flag = intoRenderTargets || Main.LightingEveryFrame;

        if (Main.tile[i, j].TileFrameX % 18 == 0 && Main.tile[i, j].TileFrameY % 54 == 0 && flag)
        {
            Main.instance.TilesRenderer.AddSpecialPoint(i, j, 5);
        }

        return false;
    }
}
