using BleachMod.Common.Players;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace BleachMod.Content.Items.Accessories
{
	internal class Gikon : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Gives a small increase to spiritual pressure");

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
			player.GetModPlayer<BleachPlayer>().MaxPressure += 25;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SoulofLight, 5);
			recipe.AddIngredient(ItemID.SoulofNight, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

	}
}
