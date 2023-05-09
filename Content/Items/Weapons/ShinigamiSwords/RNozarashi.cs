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
	internal class RNozarashi : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("RNozarashi");
			Tooltip.SetDefault("A Zanpakuto belonging to the strongest Shinigami.");
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;

		}

		public override void SetDefaults()
		{
			Item.damage = 100;
			Item.DamageType = ModContent.GetInstance<Shinigami>();
			Item.width = 95;
			Item.height = 80;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10;
			Item.value = 10000;
			Item.rare = 2;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				//right click
				//Item.useStyle = ItemUseStyleID.HoldUp;
				//Item.noMelee = true;
			}
			else if (player.altFunctionUse == 0)
			{
				//left click
				Item.useStyle = ItemUseStyleID.Swing;
				Item.noMelee = false;
			}
			return base.CanUseItem(player);
		}

		private void OnSeal(Player player)
		{
			int loc = -1;
			for (int i = 0; i < 10; i++)
			{
				if (player.inventory.GetValue(i).ToString()[8..18].Equals("RNozarashi"))
				{
					loc = i;
				}
			}
			if (loc != -1)
			{
				player.inventory.SetValue(new Item(ModContent.ItemType<Nozarashi>()), loc);
			}
		}
		private void OnBankaiRelease(Player player)
		{
			//CombatText.NewText(Main.LocalPlayer.getRect(), Color.White, "");
			player.inventory.SetValue(new Item(ModContent.ItemType<NozarashiBankai>()), player.selectedItem);
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
			if (player.HeldItem.Name != "RNozarashi")
			{
				OnSeal(player);
			}

			base.UpdateInventory(player);
		}
	}
}
