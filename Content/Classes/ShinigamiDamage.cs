using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BleachMod.Content.Classes
{
	public class ShinigamiDamage : DamageClass
	{
        public override void SetStaticDefaults()
        {
			// ((DamageClass)this).DisplayName.SetDefault("Shinigami damage");
			base.SetStaticDefaults();
        }
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass) {
			if (damageClass == DamageClass.Generic)
				return StatInheritanceData.Full;

			return new StatInheritanceData(
				damageInheritance: 0f,
				critChanceInheritance: 0f,
				attackSpeedInheritance: 0f,
				armorPenInheritance: 0f,
				knockbackInheritance: 0f
			);

		}

		public override bool UseStandardCritCalcs => true;

		
	}
}