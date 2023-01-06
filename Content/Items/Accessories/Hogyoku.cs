using BleachMod.Common.Players;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace BleachMod.Content.Items.Accessories
{
    internal class Hogyoku : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Gives vast increases to spiritual pressure");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 40;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<BleachPlayer>().MaxPressure += 1000000;
			player.GetModPlayer<BleachPlayer>().PressureRegenAmount += 10000;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

	}
}
