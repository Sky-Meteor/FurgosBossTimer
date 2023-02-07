using Terraria.GameInput;
using Terraria.ModLoader;

namespace FurgosBossTimer.UI
{
    public partial class FBTPlayer : ModPlayer
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (FurgosBossTimer.ToggleUI.JustPressed)
                UIManager.UIVisible = !UIManager.UIVisible;
        }
    }
}
