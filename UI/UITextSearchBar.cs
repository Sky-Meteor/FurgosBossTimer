using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.UI;

namespace FurgosBossTimer.UI
{
    public class UITextSearchBar : UITextBox
    {
        public Action<string> OnTextChange;
        private readonly string _textToShowWhenEmpty;
        private string _oldText;
        private bool _focused;
        public UITextSearchBar(string textToShowWhenEmpty) : base(textToShowWhenEmpty)
        {
            Height = new StyleDimension(40f, 0);
            Width = new StyleDimension(1150f, 0);
            _textToShowWhenEmpty = textToShowWhenEmpty;
            ShowInputTicker = false;
            SetTextMaxLength(100);
            _focused = false;
            _oldText = "";
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

        public override void Click(UIMouseEvent evt)
        {
            _focused = !_focused;
            if (_focused)
                Main.clrInput();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (PlayerInput.Triggers.Current.MouseLeft && !ContainsPoint(Main.MouseScreen))
            {
                _focused = false;
            }

            if (_focused)
            {
                if (Text == _textToShowWhenEmpty)
                    SetText("");
                Main.LocalPlayer.mouseInterface = true;
                string newText = Main.GetInputText(_oldText);
                SetText(newText);
                if (newText != _oldText)
                {
                    _oldText = newText;
                    OnTextChange?.Invoke(newText);
                }
                _color = Color.White;
            }
            else
            {
                if (string.IsNullOrEmpty(Text))
                    SetText(_textToShowWhenEmpty);
                _color = Color.Gray;
            }

            ShowInputTicker = _focused;
        }
    }
}