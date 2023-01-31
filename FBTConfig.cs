using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace FurgosBossTimer
{
	public class FBTConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		[Header("NPC阶段对应血量录入相关配置")]
		[Label("NPC阶段对应血量录入")]
		[Tooltip("选择NPC后，第一个Key为阶段的序号（从1开始），第二个Value为进入下一阶段对应的血量\n输入1以下的小数为相对血量（百分比），1以上的整数为绝对血量")]
		public Dictionary<NPCDefinition, Dictionary<int, float>> NPCPhases;

		public static Dictionary<int, Dictionary<int, float>> NPCIDPhases;

		public override void OnLoaded()
		{
			OnChanged();
		}

		public override void OnChanged()
		{
            NPCIDPhases = new Dictionary<int, Dictionary<int, float>>();
            if (NPCPhases == null)
				return;
			foreach (NPCDefinition npcDefinition in NPCPhases.Keys)
			{
				if (NPCPhases[npcDefinition][NPCPhases[npcDefinition].Count] != 0)
					NPCPhases[npcDefinition].Add(NPCPhases[npcDefinition].Count + 1, 0);
				NPCIDPhases.Add(npcDefinition.Type, NPCPhases[npcDefinition]);
			}
		}
	}
}