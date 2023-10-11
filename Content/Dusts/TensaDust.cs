using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace BleachMod.Content.Dusts
{
	public class TensaDust : ModDust
	{
        private int countDown;
        public override void OnSpawn(Dust dust)
        {
            countDown = 3;
            dust.noGravity = true;
            dust.frame = new Rectangle(0, 0, 5, 5);
        }
        
        
    }
}
