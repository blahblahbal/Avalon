using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ExxoAvalonOrigins.Tiles;

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
        TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
        TileObjectData.newTile.StyleWrapLimit = 111;
        TileObjectData.addTile(Type);
        DustType = -1;
        TileID.Sets.DisableSmartCursor[Type] = true;
        AddMapEntry(new Color(13, 88, 130));
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        int style = frameX / 18;
        int item;
        switch (style)
        {
            case 0:
                item = ModContent.ItemType<Items.Banners.MimeBanner>();
                break;
            case 1:
                item = ModContent.ItemType<Items.Banners.DarkMatterSlimeBanner>();
                break;
            case 2:
                item = ModContent.ItemType<Items.Banners.CursedMagmaSkeletonBanner>();
                break;
            case 4:
                item = ModContent.ItemType<Items.Banners.VampireHarpyBanner>();
                break;
            case 5:
                item = ModContent.ItemType<Items.Banners.ArmoredWraithBanner>();
                break;
            case 6:
                item = ModContent.ItemType<Items.Banners.RedAegisBonesBanner>();
                break;
            case 7:
                item = ModContent.ItemType<Items.Banners.BloodshotEyeBanner>();
                break;
            case 8:
                item = ModContent.ItemType<Items.Banners.DragonflyBanner>();
                break;
            case 9:
                item = ModContent.ItemType<Items.Banners.BlazeBanner>();
                break;
            case 10:
                item = ModContent.ItemType<Items.Banners.ArmoredHellTortoiseBanner>();
                break;
            case 13:
                item = ModContent.ItemType<Items.Banners.ImpactWizardBanner>();
                break;
            case 14:
                item = ModContent.ItemType<Items.Banners.MechanicalDiggerBanner>();
                break;
            case 15:
                item = ModContent.ItemType<Items.Banners.CougherBanner>();
                break;
            case 16:
                item = ModContent.ItemType<Items.Banners.BactusBanner>();
                break;
            case 17:
                item = ModContent.ItemType<Items.Banners.IckslimeBanner>();
                break;
            case 18:
                item = ModContent.ItemType<Items.Banners.GrossyFloatBanner>();
                break;
            case 19:
                item = ModContent.ItemType<Items.Banners.PyrasiteBanner>();
                break;
            case 20:
                item = ModContent.ItemType<Items.Banners.EyeBonesBanner>();
                break;
            case 21:
                item = ModContent.ItemType<Items.Banners.EctosphereBanner>();
                break;
            case 22:
                item = ModContent.ItemType<Items.Banners.BombSkeletonBanner>();
                break;
            case 23:
                item = ModContent.ItemType<Items.Banners.CopperSlimeBanner>();
                break;
            case 24:
                item = ModContent.ItemType<Items.Banners.TinSlimeBanner>();
                break;
            case 25:
                item = ModContent.ItemType<Items.Banners.IronSlimeBanner>();
                break;
            case 26:
                item = ModContent.ItemType<Items.Banners.LeadSlimeBanner>();
                break;
            case 27:
                item = ModContent.ItemType<Items.Banners.SilverSlimeBanner>();
                break;
            case 28:
                item = ModContent.ItemType<Items.Banners.TungstenSlimeBanner>();
                break;
            case 29:
                item = ModContent.ItemType<Items.Banners.GoldSlimeBanner>();
                break;
            case 30:
                item = ModContent.ItemType<Items.Banners.PlatinumSlimeBanner>();
                break;
            case 31:
                item = ModContent.ItemType<Items.Banners.CobaltSlimeBanner>();
                break;
            case 32:
                item = ModContent.ItemType<Items.Banners.PalladiumSlimeBanner>();
                break;
            case 33:
                item = ModContent.ItemType<Items.Banners.MythrilSlimeBanner>();
                break;
            case 34:
                item = ModContent.ItemType<Items.Banners.OrichalcumSlimeBanner>();
                break;
            case 35:
                item = ModContent.ItemType<Items.Banners.AdamantiteSlimeBanner>();
                break;
            case 36:
                item = ModContent.ItemType<Items.Banners.TitaniumSlimeBanner>();
                break;
            case 37:
                item = ModContent.ItemType<Items.Banners.RhodiumSlimeBanner>();
                break;
            case 38:
                item = ModContent.ItemType<Items.Banners.OsmiumSlimeBanner>();
                break;
            case 39:
                item = ModContent.ItemType<Items.Banners.DurataniumSlimeBanner>();
                break;
            case 40:
                item = ModContent.ItemType<Items.Banners.NaquadahSlimeBanner>();
                break;
            case 41:
                item = ModContent.ItemType<Items.Banners.TroxiniumSlimeBanner>();
                break;
            case 42:
                item = ModContent.ItemType<Items.Banners.UnstableAnomalyBanner>();
                break;
            case 43:
                item = ModContent.ItemType<Items.Banners.MatterManBanner>();
                break;
            case 44:
                item = ModContent.ItemType<Items.Banners.BronzeSlimeBanner>();
                break;
            case 45:
                item = ModContent.ItemType<Items.Banners.NickelSlimeBanner>();
                break;
            case 46:
                item = ModContent.ItemType<Items.Banners.ZincSlimeBanner>();
                break;
            case 47:
                item = ModContent.ItemType<Items.Banners.BismuthSlimeBanner>();
                break;
            case 48:
                item = ModContent.ItemType<Items.Banners.IridiumSlimeBanner>();
                break;
            case 49:
                item = ModContent.ItemType<Items.Banners.HalloworBanner>();
                break;
            case 51:
                item = ModContent.ItemType<Items.Banners.IrateBonesBanner>();
                break;
            case 52:
                item = ModContent.ItemType<Items.Banners.AegisHalloworBanner>();
                break;
            case 55:
                item = ModContent.ItemType<Items.Banners.CursedScepterBanner>();
                break;
            case 56:
                item = ModContent.ItemType<Items.Banners.EctoHandBanner>();
                break;
            case 57:
                item = ModContent.ItemType<Items.Banners.CloudBatBanner>();
                break;
            case 58:
                item = ModContent.ItemType<Items.Banners.ValkyrieBanner>();
                break;
            case 59:
                item = ModContent.ItemType<Items.Banners.CaesiumSeekerBanner>();
                break;
            case 60:
                item = ModContent.ItemType<Items.Banners.CaesiumBruteBanner>();
                break;
            case 61:
                item = ModContent.ItemType<Items.Banners.CaesiumStalkerBanner>();
                break;
            case 62:
                item = ModContent.ItemType<Items.Banners.RafflesiaBanner>();
                break;
            case 63:
                item = ModContent.ItemType<Items.Banners.PoisonDartFrogBanner>();
                break;
            case 64:
                item = ModContent.ItemType<Items.Banners.CometTailBanner>();
                break;
            case 65:
                item = ModContent.ItemType<Items.Banners.EvilVultureBanner>();
                break;
            case 66:
                item = ModContent.ItemType<Items.Banners.CrystalBonesBanner>();
                break;
            case 67:
                item = ModContent.ItemType<Items.Banners.CrystalSpectreBanner>();
                break;
            case 68:
                item = ModContent.ItemType<Items.Banners.CursedFlamerBanner>();
                break;
            case 69:
                item = ModContent.ItemType<Items.Banners.FallenHeroBanner>();
                break;
            case 70:
                item = ModContent.ItemType<Items.Banners.QuickCaribeBanner>();
                break;
            case 71:
                item = ModContent.ItemType<Items.Banners.VorazylcumMiteBanner>();
                break;
            case 72:
                item = ModContent.ItemType<Items.Banners.UnvolanditeMiteBanner>();
                break;
            case 73:
                item = ModContent.ItemType<Items.Banners.JuggernautSorcererBanner>();
                break;
            case 74:
                item = ModContent.ItemType<Items.Banners.JuggernautSwordsmanBanner>();
                break;
            case 75:
                item = ModContent.ItemType<Items.Banners.JuggernautBruteBanner>();
                break;
            case 76:
                item = ModContent.ItemType<Items.Banners.GuardianCorruptorBanner>();
                break;
            case 77:
                item = ModContent.ItemType<Items.Banners.TropicalSlimeBanner>();
                break;
            default:
                return;
        }
        Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 48, item);
    }

    public override void NearbyEffects(int i, int j, bool closer)
    {
        if (closer)
        {
            Player player = Main.LocalPlayer;
            int style = Main.tile[i, j].TileFrameX / 18;
            string type;
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
                    t = ModContent.NPCType<NPCs.BloodshotEye>();
                    break;
                //case 8:
                //    t = ModContent.NPCType<NPCs.Dragonfly>();
                //    break;
                //case 9:
                //    t = ModContent.NPCType<NPCs.Blaze>();
                //    break;
                //case 10:
                //    t = ModContent.NPCType<NPCs.ArmoredHellTortoise>();
                //    break;
                //case 13:
                //    t = ModContent.NPCType<NPCs.ImpactWizard>();
                //    break;
                //case 14:
                //    t = ModContent.NPCType<NPCs.MechanicalDiggerHead>();
                //    break;
                //case 15:
                //    t = ModContent.NPCType<NPCs.Cougher>();
                //    break;
                //case 16:
                //    t = ModContent.NPCType<NPCs.Bactus>();
                //    break;
                //case 17:
                //    t = ModContent.NPCType<NPCs.Ickslime>();
                //    break;
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
                //case 51:
                //    t = ModContent.NPCType<NPCs.IrateBones>();
                //    break;
                //case 55:
                //    t = ModContent.NPCType<NPCs.CursedScepter>();
                //    break;
                //case 56:
                //    t = ModContent.NPCType<NPCs.EctoHand>();
                //    break;
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
                //case 68:
                //    t = ModContent.NPCType<NPCs.CursedFlamer>();
                //    break;
                case 69:
                    t = ModContent.NPCType<NPCs.FallenHero>();
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
}
