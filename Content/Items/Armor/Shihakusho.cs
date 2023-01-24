using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using BleachMod.Common.Players;

namespace BleachMod.Content.Items.Armor
{

	[AutoloadEquip(EquipType.Body)]
	public class Shihakusho : ModItem
	{
		public override void Load() {
			if (Main.netMode == NetmodeID.Server) {
				return;
			}

			EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Legs}", EquipType.Legs, this);
		}

        public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			DisplayName.SetDefault("Shinigami Shihakusho");
			Tooltip.SetDefault("The standard uniform of a Shinigami.");
		}

		public override void SetDefaults() {
			Item.width = 18;
			Item.height = 14;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 6;
		}

		public override void SetMatch(bool male, ref int equipSlot, ref bool robes) {
			robes = true;
			equipSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Legs);
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Silk, 10);
			recipe.AddTile(TileID.Loom);
			recipe.Register();
		}
	}
}