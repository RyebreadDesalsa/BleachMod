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
    internal class Shinso : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shinso");
			// Tooltip.SetDefault("A Zanpakuto belonging to a Snake like Captain.");
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;

		}

		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = ModContent.GetInstance<ShinigamiDamage>();
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
			CombatText.NewText(Main.LocalPlayer.getRect(), Color.White, "Shoot to kill \n Shinso");
			player.inventory.SetValue(new Item(ModContent.ItemType<RShinso>()), player.selectedItem);
		}
		private void OnBankaiRelease(Player player)
		{
			CombatText.NewText(Main.LocalPlayer.getRect(), Color.White, "Bankai \n KamishiniNoYari");
			player.inventory.SetValue(new Item(ModContent.ItemType<KamishiniNoYari>()), player.selectedItem);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<Content.Items.Weapons.ShinigamiSwords.Zanpakuto>();
			recipe.AddIngredient(ItemID.VialofVenom, 5);
			recipe.AddTile(TileID.MythrilAnvil);
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
