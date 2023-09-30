using BleachMod.Common.Players;
using BleachMod.Content.Classes;
using BleachMod.Content.Items.Weapons.ShinigamiSwords;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BleachMod.Content.Projectiles
{
	public class ZangetsuBlade : ModProjectile
	{
		private int chargeTool = 0;
		protected virtual float HoldoutRangeMax => 100f;
		private bool isStart = true;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Zangetsu");
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Spear); // Clone the default values for a vanilla spear. Spear specific values set for width, height, aiStyle, friendly, penetrate, tileCollide, scale, hide, ownerHitCheck, and melee.
			Projectile.width = 66;
			Projectile.height = 66;
			Projectile.DamageType = ModContent.GetInstance<ShinigamiDamage>();

		}

		public override bool PreAI()
		{
			
			Player player = Main.player[Projectile.owner]; // Since we access the owner player instance so much, it's useful to create a helper local variable for this
			if (player.altFunctionUse == 2)
			{
				chargeTool = 5;
			}
			else
			{
				chargeTool--;
			}
			if (player.altFunctionUse == 4)
            {
				Projectile.Kill();
            }
			int duration = player.itemAnimationMax; // Define the duration the projectile will exist in frames

			player.heldProj = Projectile.whoAmI; // Update the player's held projectile id
			if (chargeTool > 0)
			{
				Projectile.velocity = Vector2.Normalize(new Vector2(0f, -1f));
				Projectile.Center = player.Center + new Vector2(0f,-85f);
				// Apply proper rotation to the sprite.
				if (Projectile.spriteDirection == -1)
				{
					// If sprite is facing left, rotate 45 degrees
					Projectile.rotation += MathHelper.ToRadians(45f);
				}
				else
				{
					// If sprite is facing right, rotate 135 degrees
					Projectile.rotation += MathHelper.ToRadians(135f);
				}
			}
			
			else
			{
				// Reset projectile time left if necessary
				if (Projectile.timeLeft > duration)
				{
					Projectile.timeLeft = duration;
				}
				if (isStart)
				{
					isStart = false;
					if (player.direction == -1)
					{
						Projectile.velocity = Vector2.Normalize(new Vector2(0.3f, 0f));
					}
					else
					{
						Projectile.velocity = Vector2.Normalize(new Vector2(-0.3f, 0f));
					}

				}

				float halfDuration = duration * 2f;
				float progress;

				if (Projectile.timeLeft < halfDuration)
				{
					progress = Projectile.timeLeft / halfDuration;
				}
				else
				{
					progress = (duration - Projectile.timeLeft) / halfDuration;
				}

				if (player.direction == -1)
				{
					Projectile.velocity = Projectile.velocity.RotatedBy(progress / 1.6 * -1);
					player.SetCompositeArmFront(true, 0, progress +20);

				}
				else
				{
					Projectile.velocity = Projectile.velocity.RotatedBy(progress / 1.6 * 1);
					player.SetCompositeArmFront(true, 0, -(progress) - 20);
				}
				Projectile.Center = player.Center + Projectile.velocity * (HoldoutRangeMax);
				//player.SetCompositeArmFront(true, 0, -(progress)-20);


				// Apply proper rotation to the sprite.
				if (Projectile.spriteDirection == -1)
				{
					// If sprite is facing left, rotate 45 degrees
					Projectile.rotation += MathHelper.ToRadians(135f);
				}
				else
				{
					// If sprite is facing right, rotate 135 degrees
					Projectile.rotation += MathHelper.ToRadians(45f);
				}
			}
			
			return false;
		}
		public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
            if (chargeTool > 0)
            {
				hitbox = new Rectangle(hitbox.X, hitbox.Y + 50, hitbox.Width, hitbox.Height);
			}
            else
            {
				if (Main.player[Projectile.owner].direction == -1)
				{
					hitbox = new Rectangle(hitbox.X + 50, hitbox.Y, hitbox.Width, hitbox.Height);
				}
				else
				{
					hitbox = new Rectangle(hitbox.X - 50, hitbox.Y, hitbox.Width, hitbox.Height);
				}
            }
			

				base.ModifyDamageHitbox(ref hitbox);
		}
	}
}