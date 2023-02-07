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

        public override void OnInitialize()
        {
            DragableUIPanel = new DragableUIPanel()
            {
                Height = new StyleDimension(1000f, 0),
                Width = new StyleDimension(1500f, 0)
            };

            Scrollbar = new UIScrollbar()
            {
                Left = new StyleDimension(DragableUIPanel.Width.Pixels - 40f, 0),
                Width = new StyleDimension(20, 0),
                Height = DragableUIPanel.Height,
                OverflowHidden = true
            };
            Scrollbar.OnScrollWheel += HotbarScrollFix; // from fargo's souls mod

            SearchBar = new UITextSearchBar(Language.GetTextValue("UI.PlayerNameSlot"))
            {
                OnTextChange = SearchBar_OnTextChange
            };

            NPCList = new UIList()
            {
                Top = new StyleDimension(SearchBar.Height.Pixels, 0),
                Height = new StyleDimension(DragableUIPanel.Height.Pixels - SearchBar.Height.Pixels - 30f, 0),
                Width = new StyleDimension(DragableUIPanel.Width.Pixels, 0)
            };
            NPCList.SetScrollbar(Scrollbar);

            BuildNPCListUI();

            Append(DragableUIPanel);
            DragableUIPanel.Append(NPCList);
            DragableUIPanel.Append(SearchBar);
            DragableUIPanel.Append(Scrollbar);
        }
        private void HotbarScrollFix(UIScrollWheelEvent evt, UIElement listeningElement)
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

        private void SearchBar_OnTextChange(string sortString)
        {
            BuildNPCListUI(sortString);
        }
        private void BuildNPCListUI(string sortString = "")
        {
            NPCList.Clear();

            for (int i = 1; i < Main.maxNPCTypes; i++)
            {
                if (sortString == "" || Lang.GetNPCNameValue(i).Contains(sortString) || i.ToString().Contains(sortString))
                {
                    Asset<Texture2D> texture = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{i}");

                    UIImageButton uIImageButton = new UIImageButton(texture);

                    UIText uiText = new UIText(i + " " + Lang.GetNPCNameValue(i));
                    uiText.Top.Set(uIImageButton.Top.Pixels + uIImageButton.Height.Pixels / 2, 0);
                    uiText.Left.Set(uIImageButton.Left.Pixels + 1200f, 0);

                    uIImageButton.Append(uiText);
                    NPCList.Add(uIImageButton);
                }
            }
        }
    }
}