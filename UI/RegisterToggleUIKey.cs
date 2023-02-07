using Terraria.ModLoader;

namespace FurgosBossTimer
{
    public partial class FurgosBossTimer
    {
        internal static ModKeybind ToggleUI;
        public override void Load()
        {
            ToggleUI = KeybindLoader.RegisterKeybind(this, "UI", "P");
        }

        public override void Unload()
        {
            ToggleUI = null;
        }
    }
}
