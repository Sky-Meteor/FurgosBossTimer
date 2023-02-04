/*using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace FurgosBossTimer
{
    public class FBTGlobalNPC : GlobalNPC
	{
        public override bool InstancePerEntity => true;

        public static Dictionary<int, Dictionary<int, float>> NPCIDPhases => FBTConfig.NPCIDPhases;

        public Dictionary<int, int> RecordedPhase = new();
        public Dictionary<int, Dictionary<int, int>> NPCPhaseTimer = new();

        public override void OnSpawn(NPC npc, IEntitySource source)
		{
			if (NPCIDPhases.ContainsKey(npc.type))
			{
                if (!RecordedPhase.ContainsKey(npc.type))
                    RecordedPhase.Add(npc.type, 0);
                else
				    RecordedPhase[npc.type] = 0;

                if (!NPCPhaseTimer.ContainsKey(npc.type))
                {
                    Dictionary<int, int> initTimer = new();
                    for (int i = 1; i <= NPCIDPhases[npc.type].Count; i++)
                        initTimer.Add(i, 0);
                    NPCPhaseTimer.Add(npc.type, initTimer);
                }
                else
                {
                    Dictionary<int, int> initTimer = new();
                    for (int i = 1; i <= NPCIDPhases[npc.type].Count; i++)
                        initTimer.Add(i, 0);
                    NPCPhaseTimer[npc.type] = initTimer;
                }
            }
		}

		public override void PostAI(NPC npc)
		{
            if (!NPCIDPhases.ContainsKey(npc.type))
                return;
            int OnPhase = RecordedPhase[npc.type] + 1;
            NPCPhaseTimer[npc.type][OnPhase]++;
            if (!NPCIDPhases[npc.type].ContainsKey(OnPhase))
                return;
            float PhaseTransitionHealth = NPCIDPhases[npc.type][OnPhase];

            if (int.TryParse(PhaseTransitionHealth.ToString(), out int PhaseTransitionHp))
            {
                if (npc.life <= PhaseTransitionHp)
                {
                    Main.NewText(OnPhase+"!");
                    RecordedPhase[npc.type]++;
                }
            }
            float PhasePercent = PhaseTransitionHealth;
            if (PhasePercent > 1)
                return;
            if (npc.life <= npc.lifeMax * PhasePercent)
            {
                RecordedPhase[npc.type]++;
            }
            Main.NewText(NPCPhaseTimer[npc.type][OnPhase]);
        }

        public override void OnKill(NPC npc)
        {
            if (!RecordedPhase.ContainsKey(npc.type))
                return;
            foreach (KeyValuePair<int, int> phaseTimer in NPCPhaseTimer[npc.type])
            {
                Main.NewText($"{Lang.GetNPCName(npc.type)}: {phaseTimer.Key}, {(float)phaseTimer.Value / 60}");
            }
            //Main.NewText(RecordedPhase[npc.type]);
        }
    }
}*/