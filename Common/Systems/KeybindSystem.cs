using Terraria.ModLoader;

namespace BleachMod.Common.Systems
{
	public class KeybindSystem : ModSystem
	{
		public static ModKeybind ShikaiKeybind { get; private set; }
		public static ModKeybind BankaiKeybind { get; private set; }

		public static ModKeybind FSKeybind { get; private set; }
		public override void Load()
		{
			ShikaiKeybind = KeybindLoader.RegisterKeybind(Mod, "Shikai", "C");
			BankaiKeybind = KeybindLoader.RegisterKeybind(Mod, "Bankai", "V");
			FSKeybind = KeybindLoader.RegisterKeybind(Mod, "Flash Step", "O");
		}

		public override void Unload()
		{
			ShikaiKeybind = null;
			BankaiKeybind = null;
			FSKeybind = null;
		}
	}
}