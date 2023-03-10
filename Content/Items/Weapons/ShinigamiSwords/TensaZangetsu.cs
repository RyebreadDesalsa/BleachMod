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
	internal class TensaZangetsu : ModItem
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tensa Zangetsu");
			Tooltip.SetDefault("A Zanpakuto belonging to a substitute Shinigami.");
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;

		}

		public override void SetDefaults()
		{
			Item.damage = 150;
			Item.DamageType = ModContent.GetInstance<Shinigami>();
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = 1;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = 2;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.BlackGetsuga>();
			Item.shootSpeed = 7;
		}


		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			//only shoots if the player is using right click
			if (player.altFunctionUse == 0)
			{
				return false;
			}
			else if (player.GetModPlayer<BleachPlayer>().C_Pressure < 5)
			{
				return false;
			}
			else
			{
				player.GetModPlayer<BleachPlayer>().C_Pressure -= 5;
				damage = (int) (damage * 1.5f);
				velocity = (velocity * 1.5f);

				Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
				return false;
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

		public override bool? UseItem(Player player)
		{
			return true;
		}
		public override void HoldItem(Player player)
		{

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
