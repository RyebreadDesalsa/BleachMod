using BleachMod.Common.Players;
using BleachMod.Content.Classes;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace BleachMod.Content.Items.Accessories
{
    internal class LieutenantBadge : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("The badge of a Lieutenant \nAllows for the use of Flash Step\n5% Increased Shinigami Damage\nIncreased spiritual pressure recovery\nDoes not work with the Captains Cloak");

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
            if (player.GetModPlayer<BleachPlayer>().hasCloak == false)
            {
                player.GetModPlayer<BleachPlayer>().PressureRegenAmount += 1;
                player.GetModPlayer<BleachPlayer>().hasBadge = true;
                player.GetDamage(ModContent.GetInstance<ShinigamiDamage>()) *= 1.05f;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddIngredient<Content.Items.Accessories.ShinigamiEmblem>();
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
