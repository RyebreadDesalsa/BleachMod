using BleachMod.Content.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace BleachMod.Content.Projectiles
{
    internal class SenbonBlade : ModProjectile
    {
		private int timer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Petal");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			Projectile.width = 8; 
			Projectile.height = 100;
			Projectile.aiStyle = -1; 
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = ModContent.GetInstance<Shinigami>();
			Projectile.penetrate = 5;
			Projectile.timeLeft = 90;
			Projectile.alpha = 0;
			Projectile.light = 0f;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false; 
			Projectile.extraUpdates = 1; 
			AIType = -1; 
		}

		public override bool PreDraw(ref Color lightColor)
		{

			Main.instance.LoadProjectile(Projectile.type);
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}

			return true;
		}

        public override void Kill(int timeLeft)
        {
			for(int i = 0; i < 10; i++)
            {
				Vector2 newV = new Vector2();
				newV.X = 3f;
				newV.Y = 3f;
				newV = newV.RotatedByRandom(MathHelper.ToRadians(360));
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, newV, ModContent.ProjectileType<Projectiles.Petals>(), 24, 0, Main.myPlayer);
			}
            base.Kill(timeLeft);
        }

    }
}
