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
	internal class BlackGetsuga : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Black Getsugatensho");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0; 
		}

		public override void SetDefaults()
		{
			Projectile.width = 64; 
			Projectile.height = 40;
			Projectile.aiStyle = 1; 
			Projectile.friendly = true;
			Projectile.hostile = false; 
			Projectile.DamageType = ModContent.GetInstance<Shinigami>(); 
			Projectile.penetrate = 5;
			Projectile.timeLeft = 600; 
			Projectile.alpha = 255; 
			Projectile.light = 1f; 
			Projectile.ignoreWater = true; 
			Projectile.tileCollide = true;
			Projectile.extraUpdates = 1;


			AIType = ProjectileID.Bullet;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0)
			{
				Projectile.Kill();
			}
			else
			{
				Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);


				if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
				{
					if (Projectile.velocity.Y == 0)
					{
						Projectile.Kill();
					}
					else
					{
						Projectile.velocity.X = 0;
					}
				}

				//if the projectile hits the top of a tile slide along it
				if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
				{
					if (Projectile.velocity.X == 0)
					{
						Projectile.Kill();
					}
					else
					{
						Projectile.velocity.Y = 0;
					}
				}
			}

			return false;
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

	}
}
