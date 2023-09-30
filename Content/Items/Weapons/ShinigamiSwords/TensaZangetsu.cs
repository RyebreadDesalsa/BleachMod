using BleachMod.Common.Players;
using BleachMod.Content.Classes;
using BleachMod.Content.Projectiles;
using BleachMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace BleachMod.Content.Items.Weapons.ShinigamiSwords
{
	internal class TensaZangetsu : ModItem
	{
		private int charge = 0;
		private int chargeTool = 0;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Tensa Zangetsu");
			// Tooltip.SetDefault("A Zanpakuto belonging to a substitute Shinigami.");
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;

		}

		public override void SetDefaults()
		{
			Item.damage = 150;
			Item.DamageType = ModContent.GetInstance<ShinigamiDamage>();
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = 2;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.TensaZangetsuBlade>();
			Item.shootSpeed = 7;
			Item.noUseGraphic = true;
			Item.noMelee = true;

		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 0)
			{
				Item.noUseGraphic = false;
				Item.noMelee = false;

			}
			else if (player.altFunctionUse == 2)
			{
				Item.noUseGraphic = true;
				Item.noMelee = true;
				Item.shoot = ModContent.ProjectileType<TensaZangetsuBlade>();


			}
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.GetModPlayer<BleachPlayer>().C_Pressure < 11 || player.altFunctionUse == 0)
			{
				chargeTool = 0;
				return false;
			}
			else
			{
				return true;
			}

		}
		private void OnBankaiSeal(Player player)
		{
			int loc = -1;
			for (int i = 0; i < 10; i++)
			{
				if (player.inventory.GetValue(i).ToString()[8..22].Equals("Tensa Zangetsu"))
				{
					loc = i;
				}
			}
			if (loc != -1)
			{
				player.inventory.SetValue(new Item(ModContent.ItemType<Zangetsu>()), loc);
			}
			
		
		}

		
		public override void HoldItem(Player player)
		{
			if (player.altFunctionUse == 2)
            {
				chargeTool = 5;
            }
            else
            {
				chargeTool--;
            }
			if (chargeTool>0)
			{
				
				if (charge > 50 && !Main.dedServ)
				{
					Dust.NewDust(player.Center + new Vector2(player.direction * 8, -20), 1, 1, ModContent.DustType<TensaDust>(), 0, -5);
				}
				if (charge > 100 && !Main.dedServ)
				{
					Dust.NewDust(player.Center + new Vector2(player.direction * 8, -20), 1, 1, ModContent.DustType<TensaDust>(), 0, -5);
					Dust.NewDust(player.Center + new Vector2(player.direction * 8, -20), 1, 1, ModContent.DustType<TensaDust>(), 0, -5);
					Dust.NewDust(player.Center + new Vector2(player.direction * 8, -20), 1, 1, ModContent.DustType<TensaDust>(), 0, -5);
				}
				if (charge == 100 && !Main.dedServ)
				{
					SoundEngine.PlaySound(SoundID.MaxMana);
				}
				charge++;
				if (player.direction == 1)
                {
					player.SetCompositeArmFront(true, 0, 180f);
				}
                else
                {
					player.SetCompositeArmFront(true, 0, -180f);
				}
			}
			else
			{
				
				if (charge > 100)
				{
					player.GetModPlayer<BleachPlayer>().C_Pressure -= 25;
					Vector2 Vel = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero) * 15f;
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vel, ModContent.ProjectileType<Projectiles.BlackGetsuga>(), 25, 0, Main.myPlayer);
				}
				else if (charge > 50)
				{
					player.GetModPlayer<BleachPlayer>().C_Pressure -= 15;
					Vector2 Vel = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero) * 10f;
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vel, ModContent.ProjectileType<Projectiles.BlackGetsuga>(), 25, 0, Main.myPlayer);
				}
				else if (charge > 5)
				{
					player.GetModPlayer<BleachPlayer>().C_Pressure -= 5;
					Vector2 Vel = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero) * 7f;
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vel, ModContent.ProjectileType<Projectiles.BlackGetsuga>(), 25, 0, Main.myPlayer);
				}
				charge = 0;
			}
			
			//reduces the spiritual pressure of the user while bankai is enabled and ends it when it gets low enough
			player.GetModPlayer<BleachPlayer>().PressureRegenAmount -= 1;
			if (player.GetModPlayer<BleachPlayer>().C_Pressure < 5 || player.GetModPlayer<BleachPlayer>().MaxPressure < 150)
			{
				OnBankaiSeal(player);
			}

			// if the player tries to release the shikai it will fail since zangetsu is permanantly in shikai
			if (player.altFunctionUse == 3)
			{
				player.altFunctionUse = 0;
			}
			// releases bankai
			if (player.altFunctionUse == 4)
			{
				//change between forms	
				OnBankaiSeal(player);
				player.altFunctionUse = 0;
			}
			base.HoldItem(player);
		}
		public override void UpdateInventory(Player player)
		{
			//checks if the user is holding tensa zangetsu and if not seals it
			if (player.HeldItem.Name != "Tensa Zangetsu")
			{
				OnBankaiSeal(player);
			}
			

			base.UpdateInventory(player);
		}
	}
}
