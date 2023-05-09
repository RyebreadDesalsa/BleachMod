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
	internal class Nozarashi : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nozarashi");
			Tooltip.SetDefault("A Zanpakuto belonging to the strongest Shinigami.");
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;

		}

		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = ModContent.GetInstance<Shinigami>();
			Item.width = 24;
			Item.height = 28;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = 2;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		private void OnRelease(Player player)
		{
			CombatText.NewText(Main.LocalPlayer.getRect(), Color.Yellow, "Drink \n Nozarashi");
			player.inventory.SetValue(new Item(ModContent.ItemType<RNozarashi>()), player.selectedItem);
		}
		private void OnBankaiRelease(Player player)
		{
			//CombatText.NewText(Main.LocalPlayer.getRect(), Color.White, "");
			player.inventory.SetValue(new Item(ModContent.ItemType<NozarashiBankai>()), player.selectedItem);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<Content.Items.Weapons.ShinigamiSwords.Zanpakuto>();
			recipe.AddIngredient(ItemID.FragmentSolar, 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}

		public override void HoldItem(Player player)
		{
			if (player.altFunctionUse == 3)
			{
				//change between forms
				OnRelease(player);
				player.altFunctionUse = 0;
			}
			if (player.altFunctionUse == 4)
			{
				OnBankaiRelease(player);
				player.altFunctionUse = 0;
			}
			base.HoldItem(player);
		}
	}
}
