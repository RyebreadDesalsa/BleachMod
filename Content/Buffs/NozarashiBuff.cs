using Terraria;
using Terraria.ModLoader;

namespace BleachMod.Content.Buffs
{
	public class NozarashiBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nozarashi Buff");
			Description.SetDefault("Grants +10 defense, +1 life regen and increases movement, and jump speed.");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += 10; // Grant a +4 defense boost to the player while the buff is active.
			player.moveSpeed += 3f;
			player.jumpSpeedBoost += 3f;
			player.maxFallSpeed += 3f;
			player.lifeRegen += 1;

		}
	}
}