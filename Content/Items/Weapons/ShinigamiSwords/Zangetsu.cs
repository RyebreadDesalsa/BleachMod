using BleachMod.Common.Players;
using BleachMod.Content.Classes;
using BleachMod.Content.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace BleachMod.Content.Items.Weapons.ShinigamiSwords
{
	internal class Zangetsu : ModItem
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zangetsu");
			Tooltip.SetDefault("A Zanpakuto belonging to a substitute Shinigami.");
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
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.BlueGetsuga>();
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

				return true;
			}

		}
		private void OnBankaiRelease(Player player)
		{
			int loc = -1;
			for (int i = 0; i < 10; i++)
			{
				if (player.inventory.GetValue(i).ToString() == player.HeldItem.ToString() && player.HeldItem.Name == "Zangetsu")
				{
					loc = i;
				}
			}
			if (loc != -1)
			{
				CombatText.NewText(Main.LocalPlayer.getRect(), Color.Black, "Bankai \n Tensa Zangetsu");
				player.inventory.SetValue(new Item(ModContent.ItemType<TensaZangetsu>()), loc);
			}
			
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

		public override bool? UseItem(Player player)
		{
			return true;
		}
		public override void HoldItem(Player player)
		{
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
