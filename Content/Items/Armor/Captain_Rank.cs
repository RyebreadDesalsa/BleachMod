using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using BleachMod.Common.Players;

namespace BleachMod.Content.Items.Armor
{

	[AutoloadEquip(EquipType.Head)]
	public class Captain_Rank : ModItem
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
			DisplayName.SetDefault("Captain Rank");
			Tooltip.SetDefault("The rank of a masturful Shinigami.");
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
			return head.type == ModContent.ItemType<Captain_Rank>() && body.type == ModContent.ItemType<Shihakusho>() && legs.type == ModContent.ItemType<Shinigami_Sandals>();
		}

		// UpdateArmorSet allows you to give set bonuses to the armor.
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Increases spiritual pressure by 175 \nIncreases Defense by 31";
			player.GetModPlayer<BleachPlayer>().MaxPressure += 175;
			player.statDefense += 31;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Ectoplasm, 20);
			recipe.AddIngredient<Content.Items.Armor.Lieutenant_Rank>();
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}