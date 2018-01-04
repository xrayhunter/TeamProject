using SFML.Graphics;
using SFML.System;

namespace GameProject.Core
{
    public abstract class Engine
    {
        #region Delegates/Events

        public delegate void _Initialized_();
        /// <summary>
        /// Initialized is called when before the first update.
        /// </summary>
        public event _Initialized_ Initialized = null;

        public delegate void _Update_(float deltaTime);
        /// <summary>
        /// Update is called every tick.
        /// </summary>
        public event _Update_ Update = null;

        public delegate void _Render_();
        /// <summary>
        /// Render is called every frame.
        /// </summary>
        public event _Render_ Render = null;

        public delegate void _Destroy_();
        /// <summary>
        /// Destroy is called when the engine is being destroyed...
        /// </summary>
        public event _Destroy_ Destroy = null;

        #endregion

        private RenderWindow window;

        public Engine()
        {
            window = new RenderWindow(new SFML.Window.VideoMode(480, 560), "Game");
        }

        public void Construct()
        {
            Clock clock = new Clock();
            Initialized?.Invoke();
            while (window.IsOpen)
            {
                // Receive the current events...
                window.DispatchEvents();

                // Update the engine...
                Update?.Invoke(clock.Restart().AsMilliseconds());

                // Render the engine's screen.
                Render?.Invoke();

                // Update our screen or in OpenGL Swap frame buffers...
                window.Display();
            }
            Destroy?.Invoke();
        }

        public RenderWindow GetRenderWindow()
        {
            return window;
        }
    }
}
