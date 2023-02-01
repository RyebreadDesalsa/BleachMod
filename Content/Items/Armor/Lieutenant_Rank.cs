using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using BleachMod.Common.Players;

namespace BleachMod.Content.Items.Armor
{

	[AutoloadEquip(EquipType.Head)]
	public class Lieutenant_Rank : ModItem
	{
		public override void Load()
		{
			if (Main.netMode == NetmodeID.Server)
			{
				return;
			}
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Lieutenant Rank");
			Tooltip.SetDefault("The rank of a proficient Shinigami.");
			ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 14;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 5;
		}

		// IsArmorSet determines what armor pieces are needed for the setbonus to take effect
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return head.type == ModContent.ItemType<Lieutenant_Rank>() && body.type == ModContent.ItemType<Shihakusho>() && legs.type == ModContent.ItemType<Shinigami_Sandals>();
		}

		// UpdateArmorSet allows you to give set bonuses to the armor.
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Increases spiritual pressure by 150 \nIncreases Defense by 18";
			player.GetModPlayer<BleachPlayer>().MaxPressure += 150;
			player.statDefense += 18;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SoulofLight, 20);
			recipe.AddIngredient(ItemID.SoulofNight, 20);
			recipe.AddIngredient<Content.Items.Armor.Third_Seat_Rank>();
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}