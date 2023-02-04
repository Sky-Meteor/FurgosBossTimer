using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static FurgosBossTimer.PhaseTimerCommand;

namespace FurgosBossTimer
{
    public class FFBTGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public int Phase;
        public Dictionary<int, int> BossPhaseTimer = new Dictionary<int, int>();

        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            int count;
            if (npc.type < Main.maxNPCTypes && BossPhasesDict.ContainsKey(npc.type.ToString()))
            {
                count = BossPhasesDict[npc.type.ToString()].PhaseCount;
            }
            else if (npc.ModNPC != null && BossPhasesDict.ContainsKey(npc.ModNPC.FullName))
            {
                count = BossPhasesDict[npc.ModNPC.FullName].PhaseCount;
            }
            else
                return;
            for (int i = 1; i <= count; i++)
                BossPhaseTimer.Add(i, 0);
            Phase = 1;
        }

        public override void PostAI(NPC npc)
        {
            float phaseTransitionHP;
            if (npc.type < Main.maxNPCTypes && BossPhasesDict.ContainsKey(npc.type.ToString()))
            {
                if (BossPhasesDict[npc.type.ToString()].GetPhaseTransitonHP(Phase) == -1)
                    return;
                phaseTransitionHP = BossPhasesDict[npc.type.ToString()].GetPhaseTransitonHP(Phase);
            }
            else if (npc.ModNPC != null && BossPhasesDict.ContainsKey(npc.ModNPC.FullName))
            {
                if (BossPhasesDict[npc.ModNPC.FullName].GetPhaseTransitonHP(Phase) == -1)
                    return;
                phaseTransitionHP = BossPhasesDict[npc.ModNPC.FullName].GetPhaseTransitonHP(Phase);
            }
            else
                return;
            if (npc.life <= npc.lifeMax * phaseTransitionHP)
                Phase++;
            if (BossPhaseTimer.ContainsKey(Phase))
                BossPhaseTimer[Phase]++;
        }

        public override void OnKill(NPC npc)
        {
            if (npc.type < Main.maxNPCTypes && BossPhasesDict.ContainsKey(npc.type.ToString()) || npc.ModNPC != null && BossPhasesDict.ContainsKey(npc.ModNPC.FullName))
            {
                Main.NewText(Lang.GetNPCNameValue(npc.type) + "：", Color.Lerp(Color.YellowGreen, Color.Red, 0.65f));
                foreach (KeyValuePair<int, int> phaseTimer in BossPhaseTimer)
                {
                    PrintTimer(phaseTimer.Key, phaseTimer.Value);
                }
            }
        }
        #region Utils
        public void RegisterNPCTimer(NPC npc, int npcType, int phaseCount)
        {
            if (npc.type == npcType)
            {
                for (int i = 1; i <= phaseCount; i++)
                    BossPhaseTimer.Add(i, 0);
            }
        }
        public static void PrintTimer(int phase, int time)
        {
            float second = (float)Math.Round((double)time / 60, 2);
            int minute = (int)(second / 60);
            string secondDisplay = Math.Round(second % 60, 2).ToString();
            if (!secondDisplay.Contains('.'))
                secondDisplay += ".00";
            string[] splitSecond = secondDisplay.Split(".");
            if (splitSecond[0].Length == 1)
                secondDisplay = "0" + secondDisplay;
            if (splitSecond[1].Length == 1)
                secondDisplay += "0";
            string minuteDisplay = minute.ToString();
            if (minuteDisplay.Length == 1)
                minuteDisplay = "0" + minuteDisplay;
            Main.NewText($"第{phase}阶段：{minuteDisplay}:{secondDisplay}", Color.Lerp(Color.Lime, Color.LightBlue, 0.5f));
        }
        #endregion
    }
}