using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace FurgosBossTimer.UI
{
    public class UIManager : ModSystem
    {
        private NPCListUI NPCListUI;
        private UserInterface UserInterface;
        private GameTime gameTime;
        public static bool UIVisible = false;
        public override void Load()
        {
            NPCListUI = new NPCListUI();
            NPCListUI.Activate();
            UserInterface = new UserInterface();
            UserInterface.SetState(NPCListUI);
        }
        public override void UpdateUI(GameTime gameTime)
        {
            this.gameTime = gameTime;
            if (UserInterface != null && UIVisible)
                UserInterface.Update(gameTime);
            base.UpdateUI(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (MouseTextIndex != -1)
            {
                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                   "CombatUtil: NPCList",
                   delegate
                   {
                       if (UIVisible)
                           UserInterface.Draw(Main.spriteBatch, gameTime);
                       return true;
                   },
                   InterfaceScaleType.UI)
               );
            }
        }
        public override void Unload()
        {
            NPCListUI = null;
            UserInterface = null;
        }
    }
}