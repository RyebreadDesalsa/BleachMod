using Terraria;
using Terraria.ModLoader;

namespace BleachMod.Content.Buffs
{
	public class NozarashiBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Nozarashi Buff");
			// Description.SetDefault("Grants +10 defense, +1 life regen, increases movement, and jump speed.");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += 10; 
			player.moveSpeed += 1.5f;
			player.jumpSpeedBoost += 2f;
			player.maxFallSpeed += 2f;
			player.lifeRegen += 1;

		}
	}
}