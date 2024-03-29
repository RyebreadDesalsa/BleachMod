﻿using BleachMod.Common.Players;
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
			// DisplayName.SetDefault("NozarashiBankai");
			// Tooltip.SetDefault("A Zanpakuto belonging to the strongest Shinigami.\nGives increased armor penetration");

		}

		public override void SetDefaults()
		{
			
			//DisplayName.SetDefault("NozarashiBankai");
			//Tooltip.SetDefault("A Zanpakuto belonging to the strongest Shinigami.\nGives increased armor penetration");
			Item.damage = 190;
			Item.ArmorPenetration = 50;
			Item.DamageType = ModContent.GetInstance<ShinigamiDamage>();
			Item.width = 24;
			Item.height = 28;
			Item.useTime = 16;
			Item.useAnimation = 16;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = 2;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;

		}
        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
			player.GetModPlayer<BleachPlayer>().C_Pressure -= 2;
			base.ModifyHitNPC(player, target, ref modifiers);
        }
        public override bool? UseItem(Player player)
        {
            return base.UseItem(player);
        }


        private void OnBankaiSeal(Player player)
		{
			int loc = -1;
			for (int i = 0; i < 10; i++)
			{
				if (player.inventory.GetValue(i).ToString()[8..24].Equals("Nozarashi Bankai"))
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
			player.GetModPlayer<BleachPlayer>().PressureRegenAmount -= 5;
			if (player.GetModPlayer<BleachPlayer>().C_Pressure < 10 || player.GetModPlayer<BleachPlayer>().MaxPressure < 150)
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
			if (player.HeldItem.Name != "Nozarashi Bankai")
			{
				OnBankaiSeal(player);
			}

			base.UpdateInventory(player);
		}
	}
}
