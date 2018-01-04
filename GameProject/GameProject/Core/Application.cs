using GameProject.Core;
using GameProject.Core.GameStates;


namespace GameProject
{
    public class Application : Engine
    {
        private GameStateManager gsm;
        public Application()
        {
            this.Initialized += Application_Initialized;
            this.Update += Application_Update;
            this.Render += Application_Render;
            this.Destroy += Application_Destroy;

            this.GetRenderWindow().KeyPressed += Application_KeyPressed;
            this.GetRenderWindow().KeyReleased += Application_KeyReleased;

#if __MOBILE__ || __IOS__ || __ANDROID__
            this.GetRenderWindow().TouchBegan += Application_TouchBegan;
            this.GetRenderWindow().TouchEnded += Application_TouchEnded;
            this.GetRenderWindow().TouchMoved += Application_TouchMoved;
#endif
            gsm = new GameStateManager();


            Construct();
        }

        private void Application_TouchMoved(object sender, SFML.Window.TouchEventArgs e)
        {
            gsm.PassMobileInputs(GameStateManager.MobileInputTypes.TouchMoved, e);
        }

        private void Application_TouchEnded(object sender, SFML.Window.TouchEventArgs e)
        {
            gsm.PassMobileInputs(GameStateManager.MobileInputTypes.TouchEnded, e);
        }

        private void Application_TouchBegan(object sender, SFML.Window.TouchEventArgs e)
        {
            gsm.PassMobileInputs(GameStateManager.MobileInputTypes.TouchBegan, e);
        }

        private void Application_KeyReleased(object sender, SFML.Window.KeyEventArgs e)
        {
            gsm.PassKeyboardInput(false, e);
        }

        private void Application_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            gsm.PassKeyboardInput(true, e);
        }

        private void Application_Initialized()
        {
#if __MOBILE__ || __IOS__ || __ANDROID__
            this.ShouldUpdateNotFocused = true;
#endif
        }

        private void Application_Render()
        {
            gsm.Render();
        }

        private void Application_Update(float deltaTime)
        {
            gsm.Update(deltaTime);
        }
        
        private void Application_Destroy()
        {
            gsm.Destroy();
        }

        static void Main(string[] args)
        {
            new Application();
        }
    }
}
