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
	internal class ShinsoBlade : ModProjectile
	{
		public const int FadeInDuration = 7;
		public const int FadeOutDuration = 4;


		// The "width" of the blade
		public float CollisionWidth => 10f * Projectile.scale;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shinso Blade"); // The English name of the projectile
		}

		public override void SetDefaults()
		{
			Projectile.height = 10;
			Projectile.width = 10;
			Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true; // Can the projectile deal damage to enemies?
			Projectile.hostile = false; // Can the projectile deal damage to the player?
			Projectile.DamageType = ModContent.GetInstance<Shinigami>(); // Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = -1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 32; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0f; // How much light emit around the projectile
			Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false; // Can the projectile collide with tiles?
			Projectile.hide = true;
			Projectile.ownerHitCheck = false;
			Projectile.extraUpdates = 3; // Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;

			AIType = ProjectileID.Bullet;

		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			//Projectile.Kill();
			return false;
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.AddBuff(BuffID.Venom,180);
            base.OnHitNPC(target, damage, knockback, crit);
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
