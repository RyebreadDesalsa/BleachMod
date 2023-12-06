using BleachMod.Common.Players;
using BleachMod.Content.Classes;
using BleachMod.Content.Items;
using BleachMod.Content.Projectiles;
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
	internal class Zangetsu : ModItem
	{
		private int charge = 0;
		private int chargeTool = 0;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Zangetsu");
			// Tooltip.SetDefault("A Zanpakuto belonging to a substitute Shinigami.");
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;

		}

		public override void SetDefaults()
		{
			Item.damage = 100;
			Item.DamageType = ModContent.GetInstance<ShinigamiDamage>();
			Item.width = 64;
			Item.height = 64;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = 2;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.ZangetsuBlade>();
			Item.shootSpeed = 7;
			Item.channel = true;
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
				Item.shoot = ModContent.ProjectileType<ZangetsuBlade>();


			}
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.GetModPlayer<BleachPlayer>().C_Pressure < 10 || player.altFunctionUse==0)
			{
				return false;
			}
			else
			{
				return true;
			}

		}
		private void OnBankaiRelease(Player player)
		{
			CombatText.NewText(Main.LocalPlayer.getRect(), Color.Black, "Bankai \n Tensa Zangetsu");
			player.inventory.SetValue(new Item(ModContent.ItemType<TensaZangetsu>()), player.selectedItem);
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SoulofNight, 10);
			recipe.AddIngredient(ItemID.SoulofLight, 10);
			recipe.AddIngredient<Content.Items.Weapons.ShinigamiSwords.Zanpakuto>();
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}

		public override void HoldItem(Player player)
		{
			if (player.altFunctionUse == 2)
            {
				chargeTool = 5;
            } else
            {
				chargeTool--;
            }
			if (chargeTool > 0)
            {
				if (charge > 50 && !Main.dedServ)
                {
					Dust.NewDust(player.Center + new Vector2(player.direction * 7, -20), 1, 1, DustID.GlowingSnail, 0, -5);
				}
				if (charge > 100 && !Main.dedServ)
                {
					Dust.NewDust(player.Center + new Vector2(player.direction * 7, -20), 1, 1, DustID.GlowingSnail, 0, -5);
					Dust.NewDust(player.Center + new Vector2(player.direction * 7, -20), 1, 1, DustID.GlowingSnail, 0, -5);
					Dust.NewDust(player.Center + new Vector2(player.direction * 7, -20), 1, 1, DustID.GlowingSnail, 0, -5);
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
					player.GetModPlayer<BleachPlayer>().C_Pressure -= 10;
					Vector2 Vel = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero) * 10f;
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vel,ModContent.ProjectileType<Projectiles.BlueGetsuga>(), (int)player.GetDamage(ModContent.GetInstance<ShinigamiDamage>()).ApplyTo(25),0, Main.myPlayer);
                }
				else if (charge > 50)
                {
					player.GetModPlayer<BleachPlayer>().C_Pressure -= 6;
					Vector2 Vel = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero) * 7f;
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vel, ModContent.ProjectileType<Projectiles.BlueGetsuga>(), (int)player.GetDamage(ModContent.GetInstance<ShinigamiDamage>()).ApplyTo(25), 0,Main.myPlayer);
				} 
				else if (charge > 5)
                {
					player.GetModPlayer<BleachPlayer>().C_Pressure -= 3;
					Vector2 Vel = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero) * 5f;
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vel, ModContent.ProjectileType<Projectiles.BlueGetsuga>(), (int)player.GetDamage(ModContent.GetInstance<ShinigamiDamage>()).ApplyTo(25), 0, Main.myPlayer);
				}
				charge = 0;
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
				OnBankaiRelease(player);
				
				player.altFunctionUse = 0;
			}
			base.HoldItem(player);
		}

    }
}
