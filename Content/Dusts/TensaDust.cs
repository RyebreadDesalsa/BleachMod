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
        public override bool Update(Dust dust)
        {
            return base.Update(dust);
            dust.position += dust.velocity;
            countDown-=1;
            if (countDown <= 0)
            {
                dust.active = false;
            }
            return false;
        }
        
    }
}
