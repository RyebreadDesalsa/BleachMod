using BleachMod.Common.Players;
using BleachMod.Content.Buffs;
using BleachMod.Content.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace BleachMod.Content.Items.Weapons.ShinigamiSwords
{
	internal class NozarashiBankai : ModItem
	{
		int timer = 0;
		int speed = 0;
		int delay = 0;
		Vector2 position;
		Vector2 angle;


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
			Item.noUseGraphic = false;

		}
        public override bool AltFunctionUse(Player player)
        {
			return true;
        }
        public override bool CanUseItem(Player player)
        {
			
            if (player.altFunctionUse == 2)
            {
				if (player.GetModPlayer<BleachPlayer>().C_Pressure < 10)
                {
					OnBankaiSeal(player);
                }
                else
                {
					player.GetModPlayer<BleachPlayer>().C_Pressure -= 10;
					position = Main.MouseWorld;
					angle = Main.MouseWorld - player.position;
					if (angle.X > 0)
					{
						player.direction = 1;
					}
					else
					{
						player.direction = -1;
					}
					if (angle.X == 0 && angle.Y == 0)
                    {
						angle.X = 1;
                    }
					speed = (int)(angle.X * angle.X);
					speed += (int)(angle.Y * angle.Y);
					speed = (int)Math.Sqrt(speed);
					if (speed > 450)
						speed = 450;
					angle.Normalize();
					timer = 10;
					Item.noUseGraphic = true;
					Item.noMelee = true;

					return false;
				}
				
			}
			if (player.altFunctionUse == 0)
			{
				Item.noUseGraphic = false;
				Item.noMelee = false;
			}


			return true;
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

			delay--;
			if (timer == 10){
				Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.NozarashiSwing>(), (int)player.GetDamage(ModContent.GetInstance<ShinigamiDamage>()).ApplyTo(1500), 0, Main.myPlayer);
				player.velocity = (angle * speed/(10))-angle*5;
				player.GiveImmuneTimeForCollisionAttack(20);
				timer--;

			}
			else if (timer > 0)
            {
				player.velocity = (angle * speed / (10)) - angle * 5;
				timer--;
				delay = 2;
			}
			else if(delay > 0)
            {
				player.velocity *= 0.25f;
			}
			if (timer == 0 && delay < 1)
            {
				Item.noUseGraphic = false;
				delay = 0;
			}
			player.AddBuff(ModContent.BuffType<NozarashiBuff>(),1);
			player.GetModPlayer<BleachPlayer>().PressureRegenAmount -= 3;
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
