using System.Collections.Generic;

namespace FurgosBossTimer
{
    public struct BossPhasesSet
    {
        public BossPhasesSet(Dictionary<int, float> bossPhases)
        {
            BossPhases = bossPhases;
        }
        public Dictionary<int, float> BossPhases;
        public int PhaseCount => BossPhases.Keys.Count;
        public float GetPhaseTransitonHP(int phase)
        {
            if (BossPhases.TryGetValue(phase, out float phaseTransitionHP))
                return phaseTransitionHP;
            return -1;
        }
    }
}
