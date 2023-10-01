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
					Player.GetModPlayer<BleachPlayer>().C_Pressure -= 50;
					Vector2 playerPos = Player.Center;
					Vector2 mousePos = Main.MouseWorld;
					int spdmod =(int) (Player.GetModPlayer<BleachPlayer>().MaxPressure/ 8);
					if (spdmod > 30)
                    {
						spdmod = 30;
                    }

					Vector2 direction = mousePos - playerPos;
					direction.Normalize();
					direction = direction * spdmod;
					
					Player.velocity = direction;
					
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