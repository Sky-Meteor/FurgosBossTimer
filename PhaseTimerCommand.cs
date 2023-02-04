using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;

namespace FurgosBossTimer
{
	public class PhaseTimerCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;

		public override string Command => "SetPhaseTimer";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if ((args.Length < 3) || (!int.TryParse((Convert.ToSingle(args.Length - 1) / 2).ToString(), out _)))
			{
				Main.NewText("参数数量错误");
				return;
			}
			BossPhasesDict.Remove(args[0]);
			Dictionary<int, float> bossPhases = new Dictionary<int, float>();
			for (int i = 1; i < args.Length; i += 2)
			{
				if (!int.TryParse(args[i], out int phase))
				{
					Main.NewText("输入格式错误");
					return;
				}
				bossPhases.Add(phase, Convert.ToSingle(args[i + 1]));
			}
			BossPhasesDict.Add(args[0], new BossPhasesSet(bossPhases));
			BossPhases.Put("BossPhasesDict", BossPhasesDict);
			BossPhases.Save();
		}
		Preferences BossPhases;
		public static string BossPhasesPath = Path.Combine(Main.SavePath, "ModConfigs", "CombatUtil_BossPhases.json");
		public override void Load()
		{
			BossPhasesDict = new Dictionary<string, BossPhasesSet>();
			BossPhases = new Preferences(BossPhasesPath);
			BossPhases.Load();
			BossPhasesDict = BossPhases.Get("BossPhasesDict", new Dictionary<string, BossPhasesSet>());
			BossPhases.Put("BossPhasesDict", BossPhasesDict);
			BossPhases.Save();
		}
		public override void Unload()
		{
			BossPhases.Save();
			BossPhases = null;
			BossPhasesDict = null;
		}
		public static Dictionary<string, BossPhasesSet> BossPhasesDict;
		#region Utils

		#endregion
	}
}