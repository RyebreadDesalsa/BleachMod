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
    internal class Gegetsuburi : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gegetsuburi");
			Tooltip.SetDefault("A Zanpakuto belonging to a rich coward.");
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;

		}

		public override void SetDefaults()
		{
			Item.damage = 50;
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
		}

		private void OnRelease(Player player)
		{
			int loc = -1;
			for (int i = 0; i < 10; i++)
			{
				if (player.inventory.GetValue(i).ToString() == player.HeldItem.ToString() && player.HeldItem.Name == "Gegetsuburi")
				{
					loc = i;
				}
			}
			if (loc != -1)
			{
				CombatText.NewText(Main.LocalPlayer.getRect(), Color.Blue, "Crush \n Gegetsuburi");

				player.inventory.SetValue(new Item(ModContent.ItemType<RGegetsuburi>()), loc);
			}

		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Diamond, 1);
			recipe.AddIngredient(ItemID.DemoniteBar, 10);
			recipe.AddIngredient<Content.Items.Weapons.ShinigamiSwords.Zanpakuto>();
			recipe.AddTile(TileID.Anvils);
			recipe.Register();

			Recipe recipe2 = CreateRecipe();
			recipe2.AddIngredient(ItemID.Diamond, 1);
			recipe2.AddIngredient(ItemID.CrimtaneBar, 10);
			recipe2.AddIngredient<Content.Items.Weapons.ShinigamiSwords.Zanpakuto>();
			recipe2.AddTile(TileID.Anvils);
			recipe2.Register();
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
				player.altFunctionUse = 0;
			}
			base.HoldItem(player);
		}
	}
}
