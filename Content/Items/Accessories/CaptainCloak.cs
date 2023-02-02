using BleachMod.Common.Players;
using BleachMod.Content.Classes;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace BleachMod.Content.Items.Accessories
{
    internal class CaptainCloak : ModItem    
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("The cloak of a Captain \nAllows for the use of Flash Step\n10% Increased Shinigami Damage\nIncreased spiritual pressure recovery\nDoes not work with the Lieutenant's Badge");

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
            if (player.GetModPlayer<BleachPlayer>().hasBadge == false)
            {
                player.GetModPlayer<BleachPlayer>().PressureRegenAmount += 2;
                player.GetModPlayer<BleachPlayer>().hasCloak = true;
                player.GetDamage(ModContent.GetInstance<Shinigami>()) *= 1.10f;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Ectoplasm, 10);
            recipe.AddIngredient<Content.Items.Accessories.LieutenantBadge>();
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}

