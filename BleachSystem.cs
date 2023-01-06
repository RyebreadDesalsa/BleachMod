using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.UI;
using BleachMod.Common.Players;
using Terraria;
using System.Collections.Generic;

namespace BleachMod
{
    internal class BleachSystem : ModSystem
    {
        
        internal UserInterface PressureGauge;


        internal PressureBar PressureUI;


        private GameTime _lastUpdateUiGameTime;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                PressureGauge = new UserInterface();

                PressureUI = new PressureBar();
                PressureUI.Activate();
            }
        }
        public override void Unload()
        {
            base.Unload();
        }


        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            if (PressureGauge?.CurrentState != null)
            {
                PressureGauge.Update(gameTime);
            }
            PressureGauge.SetState(PressureUI);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "MyMod: MyInterface",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && PressureGauge?.CurrentState != null)
                        {
                            PressureGauge.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));
            }
        }
    }
}
