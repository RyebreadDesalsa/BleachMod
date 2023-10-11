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
					int x = 0;
					int y = 0;
					if(Main.keyState.IsKeyDown(key: Microsoft.Xna.Framework.Input.Keys.W))
                    {
						y -= 1;
                    }
					if (Main.keyState.IsKeyDown(key: Microsoft.Xna.Framework.Input.Keys.A))
                    {
						x -= 1;
                    }
					if (Main.keyState.IsKeyDown(key: Microsoft.Xna.Framework.Input.Keys.S))
                    {
						y += 1;
                    }
					if (Main.keyState.IsKeyDown(key: Microsoft.Xna.Framework.Input.Keys.D))
                    {
						x += 1;
                    }

					if(x==0 && y == 0)
                    {
						return;
                    }
					Player.GetModPlayer<BleachPlayer>().C_Pressure -= 50;

					Vector2 direction = new Vector2(x, y);
					direction.Normalize();
					
					int spdmod =(int) (Player.GetModPlayer<BleachPlayer>().MaxPressure/ 8);
					if (spdmod > 30)
                    {
						spdmod = 30;
                    }
					

					
					direction*=spdmod;
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