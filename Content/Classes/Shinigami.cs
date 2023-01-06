using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BleachMod.Content.Classes
{
	public class Shinigami : DamageClass
	{
        public override void SetStaticDefaults()
        {
			((DamageClass)this).ClassName.SetDefault("Shinigami damage");
			base.SetStaticDefaults();
        }
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass) {
			if (damageClass == DamageClass.Generic)
				return StatInheritanceData.Full;

			return new StatInheritanceData(
				damageInheritance: 1f,
				critChanceInheritance: 1f,
				attackSpeedInheritance: 1f,
				armorPenInheritance: 1f,
				knockbackInheritance: 1f
			);

		}

		public override bool UseStandardCritCalcs => true;

		
	}
}