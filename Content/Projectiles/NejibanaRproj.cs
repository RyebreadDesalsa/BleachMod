using BleachMod.Content.Classes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BleachMod.Content.Projectiles
{
	public class NejibanaRproj : ModProjectile
	{
		// Define the range of the Spear Projectile. These are overrideable properties, in case you'll want to make a class inheriting from this one.
		protected virtual float HoldoutRangeMin => 24f;
		protected virtual float HoldoutRangeMax => 96f;
		private bool isStart = true;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spear");
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Spear); // Clone the default values for a vanilla spear. Spear specific values set for width, height, aiStyle, friendly, penetrate, tileCollide, scale, hide, ownerHitCheck, and melee.
			Projectile.width = 66;
			Projectile.height = 66;
			Projectile.DamageType = ModContent.GetInstance<Shinigami>();

		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner]; // Since we access the owner player instance so much, it's useful to create a helper local variable for this
			
			int duration = player.itemAnimationMax; // Define the duration the projectile will exist in frames

			player.heldProj = Projectile.whoAmI; // Update the player's held projectile id

			// Reset projectile time left if necessary
			if (Projectile.timeLeft > duration)
			{
				Projectile.timeLeft = duration;
			}
			Projectile.velocity = Vector2.Normalize(Projectile.velocity);
			if (isStart)
            {
				isStart = false;
				if (player.direction == -1)
				{
					Projectile.velocity = Vector2.Normalize(new Vector2(0.3f, 0.7f));
				}
				else
				{
					Projectile.velocity = Vector2.Normalize(new Vector2(-0.3f,0.7f));
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
				Projectile.velocity = Projectile.velocity.RotatedBy(progress/1.6 * 1);
            }
            else
            {
				Projectile.velocity = Projectile.velocity.RotatedBy(progress/1.6 * -1);
			}
			Projectile.Center = player.MountedCenter + Projectile.velocity * (HoldoutRangeMax);
			

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



			// Avoid spawning dusts on dedicated servers
			if (!Main.dedServ)
			{
				if (Main.rand.NextBool(3))
				{
					Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Water, Projectile.velocity.X * 2f, Projectile.velocity.Y * 2f, Alpha: 128, Scale: 1.2f);
				}

				if (Main.rand.NextBool(4))
				{
					Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Water, Alpha: 128, Scale: 0.3f);
				}
			}

			return false;
		}
		public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			if (Main.player[Projectile.owner].direction == -1)
            {
				hitbox = new Rectangle(hitbox.X + 25, hitbox.Y + 20, hitbox.Width, hitbox.Height);
            }
            else
            {
				hitbox = new Rectangle(hitbox.X - 25, hitbox.Y + 20, hitbox.Width, hitbox.Height);
			}
			
			base.ModifyDamageHitbox(ref hitbox);
		}
	}
}