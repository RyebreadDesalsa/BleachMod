using BleachMod.Common.Players;
using BleachMod.Content.Classes;
using BleachMod.Content.Items.Weapons.ShinigamiSwords;
using BleachMod.Content.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace BleachMod.Content.Items.Weapons.ShinigamiSwords
{
	public class RNejibana : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("RNejibana");
			Tooltip.SetDefault("This is a modded spear");

			ItemID.Sets.SkipsInitialUseSound[Item.type] = true; // This skips use animation-tied sound playback, so that we're able to make it be tied to use time instead in the UseItem() hook.
			ItemID.Sets.Spears[Item.type] = true; // This allows the game to recognize our new item as a spear.
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
		}

		public override void SetDefaults()
		{
			// Common Properties
			Item.rare = ItemRarityID.Pink; // Assign this item a rarity level of Pink
			Item.value = Item.sellPrice(silver: 10); // The number and type of coins item can be sold for to an NPC

			// Use Properties
			Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
			Item.useAnimation = 18; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useTime = 18; // The length of the item's use time in ticks (60 ticks == 1 second.)
			Item.UseSound = SoundID.Item21; // The sound that this item plays when used.
			Item.autoReuse = true; // Allows the player to hold click to automatically use the item again. Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			// Weapon Properties
			Item.damage = 40;
			Item.knockBack = 6.5f;
			Item.noUseGraphic = true; // When true, the item's sprite will not be visible while the item is in use. This is true because the spear projectile is what's shown so we do not want to show the spear sprite as well.
			Item.DamageType = ModContent.GetInstance<Shinigami>();
			Item.noMelee = true; // Allows the item's animation to do damage. This is important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.

			// Projectile Properties
			Item.shootSpeed = 3.7f; // The speed of the projectile measured in pixels per frame.
			Item.shoot = ModContent.ProjectileType<NejibanaLproj>(); // The projectile that is fired from this weapon
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			if (player.altFunctionUse == 2)
			{
				Item.damage = 60;
				Item.useAnimation = 24; // The length of the item's use animation in ticks (60 ticks == 1 second.)
				Item.useTime = 24; // The length of the item's use time in ticks (60 ticks == 1 second.)
				Item.shoot = ModContent.ProjectileType<NejibanaRproj>();


			}
			else if (player.altFunctionUse == 0)
			{
				Item.damage = 40;
				Item.useAnimation = 14; // The length of the item's use animation in ticks (60 ticks == 1 second.)
				Item.useTime = 14; // The length of the item's use time in ticks (60 ticks == 1 second.)
				Item.shoot = ModContent.ProjectileType<NejibanaLproj>();


			}
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}

		public override bool? UseItem(Player player)
		{
			if (!Main.dedServ && Item.UseSound.HasValue)
			{
				SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
			}
            if (player.altFunctionUse == 0)
            {
				player.GetModPlayer<BleachPlayer>().C_Pressure -= 2;
            }
            else
            {
				player.GetModPlayer<BleachPlayer>().C_Pressure -= 5;
			}

			return null;
		}
		private void OnSeal(Player player)
		{
			int loc = -1;
			for (int i = 0; i < 10; i++)
			{
				
				if (player.inventory.GetValue(i).ToString()[8..17].Equals("RNejibana"))
				{
					loc = i;
				}
			}
			if (loc != -1)
			{
				player.inventory.SetValue(new Item(ModContent.ItemType<Nejibana>()), loc);
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
			if (player.HeldItem.Name != "RNejibana")
			{
				OnSeal(player);
			}

			base.UpdateInventory(player);
		}

	}
}