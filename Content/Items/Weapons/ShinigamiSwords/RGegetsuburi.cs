using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BleachMod.Content.Projectiles;
using BleachMod.Common.Players;
using BleachMod.Content.Items.Weapons.ShinigamiSwords;
using BleachMod.Content.Classes;

namespace BleachMod.Content.Items.Weapons.ShinigamiSwords
{
	public class RGegetsuburi : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("RGegetsuburi");
			Tooltip.SetDefault("");

			// This line will make the damage shown in the tooltip twice the actual Item.damage. This multiplier is used to adjust for the dynamic damage capabilities of the projectile.
			// When thrown directly at enemies, the flail projectile will deal double Item.damage, matching the tooltip, but deals normal damage in other modes.
			ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
		}

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
			Item.useAnimation = 45; // The item's use time in ticks (60 ticks == 1 second.)
			Item.useTime = 45; // The item's use time in ticks (60 ticks == 1 second.)
			Item.knockBack = 5.5f; // The knockback of your flail, this is dynamically adjusted in the projectile code.
			Item.width = 32; // Hitbox width of the item.
			Item.height = 32; // Hitbox height of the item.
			Item.damage = 35; // The damage of your flail, this is dynamically adjusted in the projectile code.
			Item.noUseGraphic = true; // This makes sure the item does not get shown when the player swings his hand
			Item.shoot = ModContent.ProjectileType<GegetsuburiProjectile>(); // The flail projectile
			Item.shootSpeed = 24f; // The speed of the projectile measured in pixels per frame.
			Item.UseSound = SoundID.Item1; // The sound that this item makes when used
			Item.rare = ItemRarityID.Green; // The color of the name of your item
			Item.value = Item.sellPrice(gold: 1, silver: 50); // Sells for 1 gold 50 silver
			Item.DamageType = ModContent.GetInstance<Shinigami>();
			Item.channel = true;
			Item.noMelee = true; // This makes sure the item does not deal damage from the swinging animation
		}

		private void OnSeal(Player player)
		{
			int loc = -1;
			for (int i = 0; i < 10; i++)
			{
				if (player.inventory.GetValue(i).ToString()[8..20].Equals("RGegetsuburi"))
				{
					loc = i;
				}
			}
			if (loc != -1)
			{
				player.inventory.SetValue(new Item(ModContent.ItemType<Gegetsuburi>()), loc);
			}
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
				player.altFunctionUse = 0;
			}
			base.HoldItem(player);
		}
		public override void UpdateInventory(Player player)
		{
			if (player.HeldItem.Name != "RGegetsuburi")
			{
				OnSeal(player);
			}

			base.UpdateInventory(player);
		}
	}
}