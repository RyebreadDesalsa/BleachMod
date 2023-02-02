using Terraria.ModLoader.IO;
using Terraria.ModLoader;

namespace BleachMod.Common.Players{
    public class BleachPlayer : ModPlayer{
        public const int basePressure = 0;
        public int MaxPressure;
        public int C_Pressure;
        public float P_RechargeTimer = 0;
        public float P_RechargeDelay = 20;
        public float PressureRegenRate = 1f;
        public float PressureCostModifier = 1f;
        public int PressureRegenAmount = 1;
        public bool hasBadge = false;
        public bool hasCloak = false;
        public override void Initialize() {
            MaxPressure = 0;
            C_Pressure = 0;
            
		}
        public override void LoadData(TagCompound tag)
        {
            MaxPressure = tag.GetInt("MaxPressure");
            C_Pressure = MaxPressure;
    }
        public override void SaveData(TagCompound tag)
        {
            tag.Add("MaxPressure", MaxPressure);
            tag.Add("Regen Rate", PressureRegenAmount);
        }

        public override void ResetEffects()
        {
            MaxPressure = basePressure;
            PressureCostModifier = 1f;
            PressureRegenRate = 1f;
            PressureRegenAmount = 1;
            hasBadge = false;
            hasCloak = false;
        }

        public override void clientClone(ModPlayer clientClone)
        {
            BleachPlayer ps = clientClone as BleachPlayer;
            ps.MaxPressure = MaxPressure;
            ps.C_Pressure = C_Pressure;
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            BleachPlayer ps = clientPlayer as BleachPlayer;
            base.SendClientChanges(clientPlayer);
        }
        public override void PostUpdate() => HandlePressure();
            
        public void HandlePressure()
        {
            
            P_RechargeTimer += PressureRegenRate;
            if (P_RechargeDelay > 180)
            {
                P_RechargeDelay = 160;
            }

            if (P_RechargeDelay > 5) 
            {
                P_RechargeDelay -= 0.4f;
            }

            if (P_RechargeDelay < 6)
            {
                P_RechargeDelay = 12;
            }

            if (P_RechargeTimer >= P_RechargeDelay)
            {
                P_RechargeTimer = 0;
                if (C_Pressure < MaxPressure)
                {
                    C_Pressure += PressureRegenAmount;
                }
            }

            if (C_Pressure > MaxPressure)
            {
                C_Pressure = MaxPressure;
            }


        }


    }
}