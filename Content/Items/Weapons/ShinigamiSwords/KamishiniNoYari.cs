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
    internal class KamishiniNoYari : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("KamishiniNoYari");
			Tooltip.SetDefault("A Zanpakuto belonging to a Snake like Captain.");
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;

		}

		public override void SetDefaults()
		{
			Item.damage = 80;
			Item.DamageType = ModContent.GetInstance<Shinigami>();
			Item.width = 24;
			Item.height = 28;
			Item.useTime = 5;
			Item.useAnimation = 5;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = 2;
			Item.useStyle = ItemUseStyleID.Swing;
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
			if (player.altFunctionUse == 0)
			{
				Item.damage = 120;
				Item.useStyle = ItemUseStyleID.Rapier;
				Item.noUseGraphic = true;
				Item.noMelee = true;
			}
			else if (player.altFunctionUse == 2)
			{
				Item.damage = 250;
				Item.useStyle = ItemUseStyleID.Swing;
				Item.noUseGraphic = false;
				Item.noMelee = false;
			}
			return base.CanUseItem(player);
		}
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
			target.AddBuff(BuffID.Venom, 60);
			base.OnHitNPC(player, target, damage, knockBack, crit);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			//only shoots if the player is using right click
			if (player.altFunctionUse == 2)
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
				Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(7));

				// Create a projectile.
				Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
				Projectile.NewProjectileDirect(source, position, newVelocity*100, ModContent.ProjectileType<Projectiles.ShinsoBlade>(), damage, knockback, player.whoAmI);
			

				return false; // Return false because we don't want tModLoader to shoot projectile

			}

		}


		private void OnBankaiSeal(Player player)
		{
			int loc = -1;
			for (int i = 0; i < 10; i++)
			{
				if (player.inventory.GetValue(i).ToString()[8..23].Equals("KamishiniNoYari"))
				{
					
					loc = i;
				}
			}
			if (loc != -1)
			{
				player.inventory.SetValue(new Item(ModContent.ItemType<Shinso>()), loc);
			}
		}
		

		public override void HoldItem(Player player)
		{
			player.GetModPlayer<BleachPlayer>().PressureRegenAmount -= 2;
			if (player.GetModPlayer<BleachPlayer>().C_Pressure < 5 || player.GetModPlayer<BleachPlayer>().MaxPressure < 150)
			{
				OnBankaiSeal(player);
			}

			if (player.altFunctionUse == 3)
			{
				player.altFunctionUse = 0;
			}
			if (player.altFunctionUse == 4)
			{
				OnBankaiSeal(player);
				player.altFunctionUse = 0;
			}
			base.HoldItem(player);
		}
		public override void UpdateInventory(Player player)
		{
			if (player.HeldItem.Name != "KamishiniNoYari")
			{
				OnBankaiSeal(player);
			}

			base.UpdateInventory(player);
		}
	}
}
