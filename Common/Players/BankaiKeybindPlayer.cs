using Terraria;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;
using BleachMod.Common.Systems;
using BleachMod.Content.Classes;

namespace BleachMod.Common.Players
{
	
	public class BankaiKeybindPlayer : ModPlayer
	{
		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			if (KeybindSystem.BankaiKeybind.JustPressed)
			{
				//checks weather or not the player is using a shinigami weapon and has the required spiritual pressure for bankai
				if (Player.HeldItem.CountsAsClass(ModContent.GetInstance<ShinigamiDamage>()) && Player.GetModPlayer<BleachPlayer>().MaxPressure >= 150)
				{
					Player.altFunctionUse = 4;
				}

			}
		}
	}
}