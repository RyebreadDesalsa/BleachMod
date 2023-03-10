using BleachMod.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;
using BleachMod.Common.Systems;
using BleachMod.Content.Classes;
using Microsoft.Xna.Framework;

namespace BleachMod.Common.Players
{
	public class FlashStepKeybindPlayer : ModPlayer
	{
		
		public override void ProcessTriggers(TriggersSet triggersSet)
		{

			if (KeybindSystem.FSKeybind.JustPressed)
			{
				if (Player.GetModPlayer<BleachPlayer>().C_Pressure >= 50 && (Player.GetModPlayer<BleachPlayer>().hasBadge || Player.GetModPlayer<BleachPlayer>().hasCloak))
				{
					Microsoft.Xna.Framework.Vector2 tploc = default(Microsoft.Xna.Framework.Vector2);
					tploc.X = (float)Main.mouseX + Main.screenPosition.X;
					if (Player.gravDir == 1f)
					{
						tploc.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)Player.height;
					}
					else
					{
						tploc.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
					}
					tploc.X -= Player.width / 2;
					this.LimitPointToPlayerReachableArea(ref tploc);
					if (!(tploc.X > 50f) || !(tploc.X < (float)(Main.maxTilesX * 16 - 50)) || !(tploc.Y > 50f) || !(tploc.Y < (float)(Main.maxTilesY * 16 - 50)))
					{
						return;
					}
					int num = (int)(tploc.X / 16f);
					int num2 = (int)(tploc.Y / 16f);
					if (!((Main.tile[num, num2].WallType == 87 && (double)num2 > Main.worldSurface && !NPC.downedPlantBoss) || Collision.SolidCollision(tploc, Player.width, Player.height)))
					{
						Player.Teleport(tploc, 3);
						NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, Player.whoAmI, tploc.X, tploc.Y, 1);
						Player.GetModPlayer<BleachPlayer>().C_Pressure -= 50;
					}
				}
			}
			
		}
		public void LimitPointToPlayerReachableArea(ref Microsoft.Xna.Framework.Vector2 pointPoisition)
		{
			Microsoft.Xna.Framework.Vector2 center = Player.Center;
			Microsoft.Xna.Framework.Vector2 vector = pointPoisition - center;
			float num = System.Math.Abs(vector.X);
			float num2 = System.Math.Abs(vector.Y);
			float num3 = 1f;
			if (num > 960f)
			{
				float num4 = 960f / num;
				if (num3 > num4)
				{
					num3 = num4;
				}
			}
			if (num2 > 600f)
			{
				float num5 = 600f / num2;
				if (num3 > num5)
				{
					num3 = num5;
				}
			}
			Microsoft.Xna.Framework.Vector2 value2 = vector * num3;
			pointPoisition = center + value2;
		}
	}
}