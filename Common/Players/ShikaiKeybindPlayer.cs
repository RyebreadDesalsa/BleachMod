using Terraria;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;
using BleachMod.Common.Systems;
using BleachMod.Content.Classes;

namespace BleachMod.Common.Players
{
	public class ShikaiKeybindPlayer : ModPlayer
	{
		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			if (KeybindSystem.ShikaiKeybind.JustPressed)
			{	
				if (Player.HeldItem.CountsAsClass(ModContent.GetInstance<Shinigami>()) && Player.GetModPlayer<BleachPlayer>().C_Pressure >=50)
                {
					Player.altFunctionUse = 3;
				}
				
			}
		}
	}
}