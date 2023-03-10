using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;

namespace BleachMod.Common.GlobalNPCs
{
    public class NPCLoot : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, Terraria.ModLoader.NPCLoot npcLoot)
		{
			// Editing an existing drop rule, but for a boss
			// In addition to this code, we also do similar code in Common/GlobalItems/BossBagLoot.cs to edit the boss bag loot. Remember to do both if your edits should affect boss bags as well.
			if (npc.type == NPCID.WallofFlesh)
			{
				foreach (var rule in npcLoot.Get())
				{
					if (rule is DropBasedOnExpertMode dropBasedOnExpertMode && dropBasedOnExpertMode.ruleForNormalMode is OneFromOptionsNotScaledWithLuckDropRule oneFromOptionsDrop && oneFromOptionsDrop.dropIds.Contains(ItemID.WarriorEmblem))
					{
						var original = oneFromOptionsDrop.dropIds.ToList();
						original.Add(ModContent.ItemType<Content.Items.Accessories.ShinigamiEmblem>());
						oneFromOptionsDrop.dropIds = original.ToArray();
					}
				}
			}

		}
	}
}
