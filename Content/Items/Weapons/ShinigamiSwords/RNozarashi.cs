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
	internal class RNozarashi : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("RNozarashi");
			// Tooltip.SetDefault("A Zanpakuto belonging to the strongest Shinigami.\nGives increased armor penetration\nBuffs the user on enemy hit");

		}

		public override void SetDefaults()
		{
			Item.damage = 150;
			Item.ArmorPenetration = 25;
			Item.DamageType = ModContent.GetInstance<ShinigamiDamage>();
			Item.width = 95;
			Item.height = 80;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10;
			Item.value = 10000;
			Item.rare = 2;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
			player.AddBuff(ModContent.BuffType<NozarashiBuff>(), 60);
			base.ModifyHitNPC(player, target, ref modifiers);
        }

		private void OnSeal(Player player)
		{
			int loc = -1;
			for (int i = 0; i < 10; i++)
			{
				if (player.inventory.GetValue(i).ToString()[8..19].Equals("R Nozarashi"))
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
			player.GetModPlayer<BleachPlayer>().C_Pressure -= 1;
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
			if (player.HeldItem.Name != "R Nozarashi")
			{
				OnSeal(player);
			}

			base.UpdateInventory(player);
		}
	}
}
