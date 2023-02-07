using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace FurgosBossTimer.UI
{
    public class NPCListUI : UIState
    {
        private DragableUIPanel DragableUIPanel;
        private UIList NPCList;
        private UIScrollbar Scrollbar;
        private UITextSearchBar SearchBar;
        public static bool IsMouseHoveringScrollbar = false;
        string oldText;

        public override void OnInitialize()
        {
            DragableUIPanel = new DragableUIPanel();
            DragableUIPanel.Height.Set(1000f, 0);
            DragableUIPanel.Width.Set(1000f, 0);

            Scrollbar = new UIScrollbar();
            Scrollbar.SetView(200f, 1000f);
            Scrollbar.Left.Set(DragableUIPanel.Width.Pixels - 40f, 0);
            Scrollbar.Width.Set(20, 0);
            Scrollbar.Height = DragableUIPanel.Height;
            Scrollbar.OverflowHidden = true;
            Scrollbar.OnScrollWheel += hotbarScrollFix;

            oldText = Language.GetTextValue("UI.PlayerNameSlot");
            SearchBar = new UITextSearchBar(oldText);

            NPCList = new UIList();
            NPCList.Top.Set(SearchBar.Height.Pixels, 0);
            NPCList.Height.Set(DragableUIPanel.Height.Pixels - SearchBar.Height.Pixels, 0);
            NPCList.Width.Set(DragableUIPanel.Width.Pixels, 0);
            NPCList.SetScrollbar(Scrollbar);

            NPCList.Clear();

            for (int i = 1; i < Main.maxNPCTypes; i++)
            {
                Asset<Texture2D> texture = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{i}");

                UIImageButton uIImageButton = new UIImageButton(texture);

                UIText uiText = new UIText(i + " " + Lang.GetNPCNameValue(i));
                uiText.Top.Set(uIImageButton.Top.Pixels + uIImageButton.Height.Pixels / 2, 0);
                uiText.Left.Set(uIImageButton.Left.Pixels + 700f, 0);

                uIImageButton.Append(uiText);
                NPCList.Add(uIImageButton);
            }

            Append(DragableUIPanel);
            DragableUIPanel.Append(SearchBar);
            DragableUIPanel.Append(NPCList);
            DragableUIPanel.Append(Scrollbar);
        }
        private void hotbarScrollFix(UIScrollWheelEvent evt, UIElement listeningElement)
        {
            Main.LocalPlayer.ScrollHotbar(PlayerInput.ScrollWheelDelta / 120);
        }
        public override void Update(GameTime gameTime)
        {
            if (!UIManager.UIVisible)
                return;
            IsMouseHoveringScrollbar = Scrollbar.IsMouseHovering;
            base.Update(gameTime);
        }
    }

}