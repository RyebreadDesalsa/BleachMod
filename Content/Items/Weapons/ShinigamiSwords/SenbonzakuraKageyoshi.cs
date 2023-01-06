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
    internal class SenbonzakuraKageyoshi : ModItem
    {
		private int ShotTimer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Senbonzakura Kageyoshi");
			Tooltip.SetDefault("A Zanpakuto belonging to a Noble Captain.");

		}

		public override void SetDefaults()
		{
			Item.damage = 150;
			Item.DamageType = ModContent.GetInstance<Shinigami>();
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = 2;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.channel = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;
		}

		private void OnBankaiSeal(Player player)
		{
			int loc = -1;
			for (int i = 0; i < 10; i++)
			{
				if (player.inventory.GetValue(i).ToString()[8..30].Equals("Senbonzakura Kageyoshi"))
				{
					loc = i;
				}
			}
			if (loc != -1)
			{
				player.inventory.SetValue(new Item(ModContent.ItemType<Senbonzakura>()), loc);
			}
		}
		public override void HoldItem(Player player)
		{
			if (player.channel)
			{
				player.GetModPlayer<BleachPlayer>().PressureRegenAmount -= 1;
				if(ShotTimer == 10)
                {
					ShotTimer = 0;
					player.GetModPlayer<BleachPlayer>().C_Pressure -= 1;
					Projectile.NewProjectileDirect(player.GetSource_FromAI(), player.Center, (Main.MouseWorld - player.position).SafeNormalize(Vector2.Zero) * 15f, ModContent.ProjectileType<Projectiles.Petals>(), 24, 0, player.whoAmI);
				}
				ShotTimer++;
			}
			player.GetModPlayer<BleachPlayer>().PressureRegenAmount -= 2;
			if (player.GetModPlayer<BleachPlayer>().C_Pressure < 5 || player.GetModPlayer<BleachPlayer>().MaxPressure < 150)
			{
				OnBankaiSeal(player);
			}

			if (player.altFunctionUse == 3)
			{
				//change between forms
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
			if (player.HeldItem.Name != "Senbonzakura Kageyoshi")
			{
				OnBankaiSeal(player);
			}

			base.UpdateInventory(player);
		}
	}

	
}
