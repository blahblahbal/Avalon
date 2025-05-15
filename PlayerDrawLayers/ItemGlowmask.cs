using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.PlayerDrawLayers;

public class ItemGlowmask : GlobalItem
{
	public static Dictionary<int, Asset<Texture2D>> GlowTextures = [];
	public static Dictionary<int, Color> GlowColors = [];
	public static void AddGlow(ModItem item)
	{
		AddGlow(item, Color.White);
	}
	public static void AddGlow(ModItem item, byte alpha)
	{
		AddGlow(item, new Color(255, 255, 255, alpha));
	}
	public static void AddGlow(ModItem item, Color color)
	{
		if (!Main.dedServ)
		{
			GlowTextures.Add(item.Type, ModContent.Request<Texture2D>(item.Texture + "_Glow"));
			GlowColors.Add(item.Type, color);
		}
	}
	public override bool InstancePerEntity => true;

	public int glowOffsetX = 10; // defaults to 10 for vanilla holdout offset
	public int glowOffsetY;
	public bool CustomPostDrawInWorld = false;

	public override void PostDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		if (!CustomPostDrawInWorld && GlowTextures.TryGetValue(item.type, out var texture))
		{
			Color color = GlowColors.GetValueOrDefault(item.type, Color.White);
			Vector2 vector = texture.Size() / 2f;
			Vector2 value = new((float)(item.width / 2) - vector.X, item.height - texture.Height());
			Vector2 vector2 = item.position - Main.screenPosition + vector + value;
			spriteBatch.Draw(texture.Value, vector2, new Rectangle(0, 0, texture.Width(), texture.Height()), color, rotation, vector, scale, SpriteEffects.None, 0f);
		}
	}
}

public class PlayerUseItemGlowmask : PlayerDrawLayer
{
	public override Position GetDefaultPosition() => new AfterParent(Terraria.DataStructures.PlayerDrawLayers.HeldItem);
	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		if (drawInfo.shadow != 0)
			return;

		Player drawPlayer = drawInfo.drawPlayer;
		Item heldItem = drawPlayer.HeldItem;

		if (!heldItem.IsAir)
		{
			Color color = ItemGlowmask.GlowColors.GetValueOrDefault(heldItem.type, Color.White);
			if (drawPlayer.itemAnimation > 0 && ItemGlowmask.GlowTextures.TryGetValue(heldItem.type, out var texture))
			{
				Vector2 basePosition = drawPlayer.itemLocation - Main.screenPosition;
				basePosition = new Vector2((int)basePosition.X, (int)basePosition.Y) + (drawPlayer.RotatedRelativePoint(drawPlayer.Center) - drawPlayer.Center);
				if (heldItem.useStyle == ItemUseStyleID.Shoot)
				{
					if (Item.staff[heldItem.type])
					{
						if (heldItem.type == ModContent.ItemType<Items.Material.PointingLaser>())
						{
							color = Items.Material.PointingLaser.TeamColor(drawPlayer);
						}

						float rotationMod = MathHelper.PiOver4 * -drawPlayer.direction * drawPlayer.gravDir;
						DrawData staffDraw = new(
							texture.Value,                                                      // texture
							basePosition,                                                       // position
							default,                                                            // texture coords
							color,                                                                      // color
							drawPlayer.itemRotation - rotationMod,                              // rotation
							new Vector2(drawPlayer.direction == -1 ? texture.Value.Width : 0,   // origin X
							drawPlayer.gravDir == 1 ? texture.Value.Height : 0),                        // origin Y
							drawPlayer.GetAdjustedItemScale(heldItem),                      // scale
							drawInfo.itemEffect                                                 // sprite effects
							);
						drawInfo.DrawDataCache.Add(staffDraw);
					}
					else
					{
						Vector2 offsetFix = new(0, texture.Value.Height / 2 + (heldItem.GetGlobalItem<ItemGlowmask>().glowOffsetY * drawPlayer.gravDir));
						int glowOffsetXInvert = -heldItem.GetGlobalItem<ItemGlowmask>().glowOffsetX;
						Vector2 positionFix = new(drawPlayer.direction == -1 ? texture.Value.Width - glowOffsetXInvert : glowOffsetXInvert, texture.Value.Height / 2);

						DrawData horizontalStaffDraw = new(
							texture.Value,                                  // texture
							basePosition + offsetFix,                       // position
							default,                                        // texture coords
							color,                                                  // color
							drawPlayer.itemRotation,                            // rotation
							positionFix,                                        // origin
							drawPlayer.GetAdjustedItemScale(heldItem),  // scale
							drawInfo.itemEffect                             // sprite effects
							);
						drawInfo.DrawDataCache.Add(horizontalStaffDraw);
					}
				}
				else
				{
					DrawData swingDraw = new(
						texture.Value,                                                      // texture
						basePosition,                                                       // position
						default,                                                            // texture coords
						color,                                                                      // color
						drawPlayer.itemRotation,                                                // rotation
						new Vector2(drawPlayer.direction == -1 ? texture.Value.Width : 0,   // origin X
						drawPlayer.gravDir == 1 ? texture.Value.Height : 0),                        // origin Y
						drawPlayer.GetAdjustedItemScale(heldItem),                      // scale
						drawInfo.itemEffect                                                 // sprite effects
						);
					drawInfo.DrawDataCache.Add(swingDraw);
				}
			}
		}
	}
}
