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
	internal class  Nejibana: ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Nejibana");
			// Tooltip.SetDefault("A Zanpakuto belonging to a lieutenant from a once noble clan.");
		}

		public override void SetDefaults()
		{
			Item.damage = 25;
			Item.DamageType = ModContent.GetInstance<ShinigamiDamage>();
			Item.width = 24;
			Item.height = 28;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = 2;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		private void OnRelease(Player player)
		{
			CombatText.NewText(Main.LocalPlayer.getRect(), Color.Aqua, "      Surge\nWater and Heaven \n     Nejibana");
			player.inventory.SetValue(new Item(ModContent.ItemType<RNejibana>()), player.selectedItem);
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
