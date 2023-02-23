//using ExxoAvalonOrigins.Items.Weapons.Melee.Hardmode;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using System;
//using Terraria;
//using Terraria.DataStructures;
//using Terraria.GameContent;
//using Terraria.Graphics;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace ExxoAvalonOrigins.Common //Gotta make the slashys have the correct color with added items ! ! !
//{
//    public class AvalonZenithProjectile : GlobalProjectile
//    {
//        public override bool InstancePerEntity => true;
//        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
//        {
//            return entity.type == ProjectileID.FinalFractal;
//        }
//        int[] AvailableItems = {ItemID.CopperShortsword,ItemID.LightsBane,ItemID.BloodButcherer,ItemID.Muramasa,ItemID.BladeofGrass,ItemID.FieryGreatsword,ItemID.NightsEdge,ItemID.TrueNightsEdge,ItemID.Excalibur,ItemID.TrueExcalibur,ItemID.TerraBlade,ItemID.Terragrim,ItemID.Starfury,ItemID.EnchantedSword,ItemID.BeeKeeper,ItemID.TheHorsemansBlade,ItemID.Seedler,ItemID.InfluxWaver,ItemID.Meowmere,ItemID.StarWrath,
//        ModContent.ItemType<VertexOfExcalibur>()};
//        public override void OnSpawn(Projectile projectile, IEntitySource source)
//        {
//            if (projectile.ai[1] != 4956)
//            {
//                projectile.ai[1] = AvailableItems[Main.rand.Next(AvailableItems.Length)];
//            }
//        }
//        public override bool PreDraw(Projectile projectile, ref Color lightColor)
//        {
//            float t2 = projectile.localAI[0];
//            float num175 = Utils.GetLerpValue(0f, 20f, t2, clamped: true) * Utils.GetLerpValue(68f, 60f, t2, clamped: true);

//            Color color42 = projectile.GetAlpha(lightColor);
//            float num174 = projectile.scale;
//            float rotation23 = projectile.rotation;
//            float t3 = projectile.localAI[0];
//            float num179 = Utils.GetLerpValue(0f, 20f, t3, clamped: true) * Utils.GetLerpValue(68f, 60f, t3, clamped: true);
//            Main.EntitySpriteDraw(value11, projectile.Center + Vector2.Zero - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle4, new Microsoft.Xna.Framework.Color(255, 255, 255, 127) * projectile.Opacity * num179, rotation23, origin6, num174 * 1.25f, dir);
//            FinalFractalHelper.FinalFractalProfile finalFractalProfile = FinalFractalHelper.GetFinalFractalProfile((int)projectile.ai[1]);
//            Color trailColor = finalFractalProfile.trailColor;
//            trailColor.A /= 2;
//            DrawPrettyStarSparkle(projectile.Opacity, SpriteEffects.None, projectile.Center + Vector2.Zero - Main.screenPosition + new Vector2(0f, projectile.gfxOffY) + (projectile.rotation - (float)Math.PI / 2f).ToRotationVector2() * finalFractalProfile.trailWidth, Microsoft.Xna.Framework.Color.White * num179, trailColor * num179, projectile.localAI[0], 15f, 30f, 30f, 45f, 0f, new Vector2(5f, 2f), Vector2.One);

//            return false;
//        }
//        private void DrawPrettyStarSparkle(float opacity, SpriteEffects dir, Vector2 drawpos, Color drawColor, Color shineColor, float flareCounter, float fadeInStart, float fadeInEnd, float fadeOutStart, float fadeOutEnd, float rotation, Vector2 scale, Vector2 fatness)
//        {
//            Texture2D value = TextureAssets.Extra[98].Value;
//            Color color = shineColor * opacity * 0.5f;
//            color.A = 0;
//            Vector2 origin = value.Size() / 2f;
//            Color color2 = drawColor * 0.5f;
//            float num = Utils.GetLerpValue(fadeInStart, fadeInEnd, flareCounter, clamped: true) * Utils.GetLerpValue(fadeOutEnd, fadeOutStart, flareCounter, clamped: true);
//            Vector2 vector = new Vector2(fatness.X * 0.5f, scale.X) * num;
//            Vector2 vector2 = new Vector2(fatness.Y * 0.5f, scale.Y) * num;
//            color *= num;
//            color2 *= num;
//            Main.EntitySpriteDraw(value, drawpos, null, color, (float)Math.PI / 2f + rotation, origin, vector, dir);
//            Main.EntitySpriteDraw(value, drawpos, null, color, 0f + rotation, origin, vector2, dir);
//            Main.EntitySpriteDraw(value, drawpos, null, color2, (float)Math.PI / 2f + rotation, origin, vector * 0.6f, dir);
//            Main.EntitySpriteDraw(value, drawpos, null, color2, 0f + rotation, origin, vector2 * 0.6f, dir);
//        }
//    }
//}
