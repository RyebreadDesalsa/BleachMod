using BleachMod.Common.Players;
using BleachMod.Content.Buffs;
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
	internal class NozarashiBankai : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("NozarashiBankai");
			Tooltip.SetDefault("A Zanpakuto belonging to the strongest Shinigami.");

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
				//left click
			}
			else if (player.altFunctionUse == 2)
			{
				//right click
			}
			return base.CanUseItem(player);
		}
        public override bool? UseItem(Player player)
        {
			player.GetModPlayer<BleachPlayer>().C_Pressure -= 1;
            return base.UseItem(player);
        }


        private void OnBankaiSeal(Player player)
		{
			int loc = -1;
			for (int i = 0; i < 10; i++)
			{
				if (player.inventory.GetValue(i).ToString()[8..23].Equals("NozarashiBankai"))
				{

					loc = i;
				}
			}
			if (loc != -1)
			{
				player.inventory.SetValue(new Item(ModContent.ItemType<Nozarashi>()), loc);
			}
		}


		public override void HoldItem(Player player)
		{
			player.AddBuff(ModContent.BuffType<NozarashiBuff>(),1);
			player.GetModPlayer<BleachPlayer>().PressureRegenAmount -= 3;
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
			if (player.HeldItem.Name != "NozarashiBankai")
			{
				OnBankaiSeal(player);
			}

			base.UpdateInventory(player);
		}
	}
}
