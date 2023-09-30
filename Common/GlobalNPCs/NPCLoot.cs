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

			if (npc.type == NPCID.WallofFlesh)
			{
				IItemDropRule emblem1 = ItemDropRule.Common(ModContent.ItemType<Content.Items.Accessories.ShinigamiEmblem>(),1,1,1);
				npcLoot.Add(ItemDropRule.AlwaysAtleastOneSuccess(emblem1));
			}
			

		}
	}
}
