using BleachMod.Common.Players;
using BleachMod.Content.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace BleachMod.Content.Items.Weapons.ShinigamiSwords
{
	internal class RSenbonzakura : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("RSenbonzakura");
			Tooltip.SetDefault("A Zanpakuto belonging to a Noble Captain.");
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 100;
			Item.DamageType = ModContent.GetInstance<Shinigami>();
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = 2;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<Projectiles.Petals>();
			Item.shootSpeed = 7;
			Item.channel = true;

		}


		public override bool AltFunctionUse(Player player)
		{
			return true;

		}
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Item.channel = false;
				Item.shoot = ModContent.ProjectileType<Projectiles.DumbPetal>(); ;
			}
			else if (player.altFunctionUse == 0)
			{
				Item.channel = true;
				Item.shoot = ModContent.ProjectileType<Projectiles.Petals>(); ;
			}
			return base.CanUseItem(player);
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			damage = 12;
			if (player.GetModPlayer<BleachPlayer>().C_Pressure < 5)
			{
				return false;
			}
			int NumProjectiles = 5;
			int CRotation = -10;
			if (player.altFunctionUse == 0)
			{
				NumProjectiles = 9;
					CRotation = -20;
			}
			for (int i = 0; i < NumProjectiles; i++)
			{
				if (player.GetModPlayer<BleachPlayer>().C_Pressure >= 5)
				{
					Vector2 newVelocity = velocity.RotatedBy(MathHelper.ToRadians(CRotation));
					CRotation += 5;
					Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
					player.GetModPlayer<BleachPlayer>().C_Pressure -= 5;
				}
			}

			return false;

		}
		private void OnSeal(Player player)
		{
			int loc = -1;
			for (int i = 0; i < 10; i++)
			{
				if (player.inventory.GetValue(i).ToString()[8..21].Equals("RSenbonzakura"))
				{
					loc = i;
				}
			}
			if (loc != -1)
			{
				player.inventory.SetValue(new Item(ModContent.ItemType<Senbonzakura>()), loc);
			}
		}
		private void OnBankaiRelease(Player player)
		{

			if (player.GetModPlayer<BleachPlayer>().C_Pressure > 70)
			{
				player.GetModPlayer<BleachPlayer>().C_Pressure -= 70;
				if (player.direction == -1)
				{
					for (int i = 0; i < 120; i += 40)
					{
						Vector2 newV = new Vector2();
						newV.X = 0f;
						newV.Y = 0f;
						Vector2 spV = player.Center;
						spV.X += (50 + i);
						spV.Y += 3;
						Projectile.NewProjectile(player.GetSource_FromAI(), spV, newV, ModContent.ProjectileType<Projectiles.SenbonGuide>(), 0, 0, Main.myPlayer);
					}
				}
				else
				{
					for (int i = 0; i < 120; i += 40)
					{
						Vector2 newV = new Vector2();
						newV.X = 0f;
						newV.Y = 0f;
						Vector2 spV = player.Center;
						spV.X -= (50 + i);
						spV.Y += 3;
						Projectile.NewProjectile(player.GetSource_FromAI(), spV, newV, ModContent.ProjectileType<Projectiles.SenbonGuide>(), 0, 0, Main.myPlayer);
					}
				}
				CombatText.NewText(Main.LocalPlayer.getRect(), Color.Pink, "Bankai \n Senbonzakura Kageyoshi");
				player.inventory.SetValue(new Item(ModContent.ItemType<SenbonzakuraKageyoshi>()), player.selectedItem);
			}

		

		}
		public override void HoldItem(Player player)
		{
            if (player.channel)
            {
				player.GetModPlayer<BleachPlayer>().PressureRegenAmount -= 1;
			}
			player.GetModPlayer<BleachPlayer>().PressureRegenAmount -= 1;
			if (player.GetModPlayer<BleachPlayer>().C_Pressure < 5)
			{
				OnSeal(player);
			}
			
			if (player.altFunctionUse == 3)
			{
				//change between forms
				OnSeal(player);
				player.altFunctionUse = 0;
			}
			if (player.altFunctionUse == 4)
			{
				OnBankaiRelease(player);
				player.altFunctionUse = 0;
			}
			base.HoldItem(player);
		}
		public override void UpdateInventory(Player player)
		{
			if (player.HeldItem.Name != "RSenbonzakura")
			{
				OnSeal(player);
			}

			base.UpdateInventory(player);
		}
	}
}
