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
    internal class SenbonGuide : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("SenbonGuide");
			
		}

		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.aiStyle = -1;
			Projectile.friendly = false;
			Projectile.hostile = false;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600; 
			Projectile.alpha = 255;
			Projectile.light = 0f; 
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true; 
			Projectile.extraUpdates = 1; 


			AIType = -1;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Vector2 newV = new Vector2();
			newV.X = 0f;
			newV.Y = 0f;
			Vector2 newP = Projectile.position;
			newP.Y -= 30;
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), newP, newV, ModContent.ProjectileType<Projectiles.SenbonBlade>(), 200, 0, Main.myPlayer);
			return true;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			return false;
		}
		public override void AI()
		{
			Projectile.velocity.X = 0;
			Projectile.velocity.Y = 10;
		}
	}
}
