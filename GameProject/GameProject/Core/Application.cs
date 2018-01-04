using GameProject.Core;


namespace GameProject
{
    public class Application : Engine
    {
        public Application()
        {
            this.Initialized += Application_Initialized;
            this.Update += Application_Update;
            this.Render += Application_Render;
            this.Destroy += Application_Destroy;
        }

        private void Application_Initialized()
        {
        }

        private void Application_Render()
        {
        }

        private void Application_Update(float deltaTime)
        {
        }
        
        private void Application_Destroy()
        {
        }

        static void Main(string[] args)
        {
            new Application();
        }
    }
}
