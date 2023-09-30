using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;

namespace BleachMod.Common.GlobalItems
{
	public class BossBagLoot : GlobalItem
	{
		public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
		{
			// In addition to this code, we also do similar code in Common/GlobalNPCs/ExampleNPCLoot.cs to edit the boss loot for non-expert drops. Remember to do both if your edits should affect non-expert drops as well.
			if (item.type == ItemID.WallOfFleshBossBag)
			{
				IItemDropRule emblem1 = ItemDropRule.Common(ModContent.ItemType<Content.Items.Accessories.ShinigamiEmblem>(), 1, 1, 1);
				itemLoot.Add(ItemDropRule.AlwaysAtleastOneSuccess(emblem1));
			}
		}
	}
}