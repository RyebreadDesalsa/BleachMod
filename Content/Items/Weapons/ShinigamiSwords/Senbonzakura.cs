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
	internal class Senbonzakura : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Senbonzakura");
			// Tooltip.SetDefault("A Zanpakuto belonging to a Noble Captain.");
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;

		}

		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = ModContent.GetInstance<ShinigamiDamage>();
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
			CombatText.NewText(Main.LocalPlayer.getRect(), Color.Pink, "Scatter \n Senbonzakura");
			player.inventory.SetValue(new Item(ModContent.ItemType<RSenbonzakura>()), player.selectedItem);
		}
		private void OnBankaiRelease(Player player)
		{
			
			if (player.GetModPlayer<BleachPlayer>().C_Pressure > 70)
			{
				
				player.GetModPlayer<BleachPlayer>().C_Pressure -= 70;
				Vector2 newV = new Vector2();
				newV.X = 0f;
				newV.Y = 0f;
				Vector2 spV = player.Center;
				spV.X += 50;
				spV.Y += 3;
				Projectile.NewProjectile(player.GetSource_FromAI(), spV, newV, ModContent.ProjectileType<Projectiles.SenbonTransition>(), 0, 0, Main.myPlayer);

				CombatText.NewText(Main.LocalPlayer.getRect(), Color.Pink, "Bankai \n Senbonzakura Kageyoshi");
				player.inventory.SetValue(new Item(ModContent.ItemType<SenbonzakuraKageyoshi>()), player.selectedItem);
			}

		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<Content.Items.Weapons.ShinigamiSwords.Zanpakuto>();
			recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
			recipe.AddIngredient(ItemID.JungleRose, 1);
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
