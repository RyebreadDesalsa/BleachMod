using BleachMod.Common.Players;
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
    internal class Petals : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Petal");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			Projectile.width = 8; 
			Projectile.height = 8; 
			Projectile.aiStyle = 0; 
			Projectile.friendly = true;
			Projectile.hostile = false; 
			Projectile.DamageType = ModContent.GetInstance<ShinigamiDamage>();
			Projectile.penetrate = -1; 
			Projectile.timeLeft = 300; 
			Projectile.alpha = 0; 
			Projectile.light = 0f; 
			Projectile.ignoreWater = false;
			Projectile.tileCollide = true; 
			Projectile.extraUpdates = 1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
			Projectile.ArmorPenetration = 50;


			AIType = 0; 
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			oldVelocity.RotatedBy(MathHelper.ToRadians(180));
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

        public override void AI()
        {
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] >= 30f)
			{
				Projectile.ai[0] = 0f;
				Projectile.netUpdate = true;
                if(Main.player[Projectile.owner].channel)
                {
					Projectile.velocity = (Main.MouseWorld - Projectile.Center).SafeNormalize(Vector2.Zero) * 15f;
				}
				
			}
		}
	}
}
