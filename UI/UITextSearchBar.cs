using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.UI;

namespace FurgosBossTimer.UI
{
    public class UITextSearchBar : UITextBox
    {
        private readonly string _textToShowWhenEmpty;
        private UITextBox _textBox;
        private bool _focused;
        public UITextSearchBar(string textToShowWhenEmpty, float textScale = 1, bool large = false) : base(textToShowWhenEmpty, textScale, large)
        {
            Height = new StyleDimension(40f, 0);
            Width = new StyleDimension(950f, 0);
            _textToShowWhenEmpty = textToShowWhenEmpty;
            _textBox = new UITextBox(textToShowWhenEmpty)
            {
                Height = new StyleDimension(40f, 0),
                Width = new StyleDimension(950f, 0),
                ShowInputTicker = true,
            };
            _textBox.SetTextMaxLength(100);
            _focused = false;
        }
        public override void Click(UIMouseEvent evt)
        {
            _focused = !_focused;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            if (_focused)
            {
                PlayerInput.WritingText = true;
                Main.instance.HandleIME();
            }
        }
        public override void Update(GameTime gameTime)
        {
            if (_focused)
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            base.Update(gameTime);
        }
    }
}