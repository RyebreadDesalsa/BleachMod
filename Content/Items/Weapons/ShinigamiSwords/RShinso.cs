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
    internal class RShinso : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("RShinso");
			Tooltip.SetDefault("A Zanpakuto belonging to a Snake like Captain.");
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = false;

		}

		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.DamageType = ModContent.GetInstance<Shinigami>();
			Item.width = 20;
			Item.height = 20;
			Item.useTime = 7;
			Item.useAnimation = 7;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = 2;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.ShinsoHandle>();
			Item.shootSpeed = 2.1f;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Item.width = 254;
				Item.height = 254;
				Item.damage = 120;
				Item.useStyle = ItemUseStyleID.Rapier;
				Item.noUseGraphic = true;
				Item.noMelee = true;
			}
			else if (player.altFunctionUse == 0)
			{
				Item.width = 20;
				Item.height = 20;
				Item.damage = 60;
				Item.noUseGraphic = false;
				Item.noMelee = false;
				Item.useStyle = ItemUseStyleID.Swing;
			}
			return base.CanUseItem(player);
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
				Projectile.NewProjectileDirect(source, position, velocity*100, ModContent.ProjectileType<Projectiles.ShinsoBlade>(), damage, knockback, player.whoAmI);
				return true;

			}

		}

		private void OnSeal(Player player)
		{
			int loc = -1;
			for (int i = 0; i < 10; i++)
			{
				if (player.inventory.GetValue(i).ToString()[8..15].Equals("RShinso"))
				{
					loc = i;
				}
			}
			if (loc != -1)
			{
				player.inventory.SetValue(new Item(ModContent.ItemType<Shinso>()), loc);
			}
		}
		private void OnBankaiRelease(Player player)
		{
			CombatText.NewText(Main.LocalPlayer.getRect(), Color.Pink, "Bankai \n KamishiniNoYari");
			player.inventory.SetValue(new Item(ModContent.ItemType<KamishiniNoYari>()), player.selectedItem);
		}

		public override void HoldItem(Player player)
		{
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
			if (player.HeldItem.Name != "RShinso")
			{
				OnSeal(player);
			}

			base.UpdateInventory(player);
		}
	}
}
