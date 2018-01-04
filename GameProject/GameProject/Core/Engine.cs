using SFML.Graphics;
using SFML.System;
using System;

namespace GameProject.Core
{
    public abstract class Engine
    {
        #region Delegates/Events

        public delegate void _Initialized_();
        /// <summary>
        /// Initialized is called when before the first update.
        /// </summary>
        protected event _Initialized_ Initialized = null;

        public delegate void _Update_(float deltaTime);
        /// <summary>
        /// Update is called every tick.
        /// </summary>
        protected event _Update_ Update = null;

        public delegate void _Render_();
        /// <summary>
        /// Render is called every frame.
        /// </summary>
        protected event _Render_ Render = null;

        public delegate void _Destroy_();
        /// <summary>
        /// Destroy is called when the engine is being destroyed...
        /// </summary>
        protected event _Destroy_ Destroy = null;

        #endregion

        private RenderWindow window;

        public bool ShouldUpdateNotFocused { get; set; }

        private bool isFocused = false;

        public Engine()
        {
            window = new RenderWindow(new SFML.Window.VideoMode(480, 560), "Game");
            window.Closed += Window_Closed;
            window.GainedFocus += Window_GainedFocus;
            window.LostFocus += Window_LostFocus;
        }

        private void Window_LostFocus(object sender, EventArgs e)
        {
            isFocused = false;
        }

        private void Window_GainedFocus(object sender, EventArgs e)
        {
            isFocused = true;
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Environment.Exit(0);
        }

        public void Construct()
        {
            Clock clock = new Clock();
            Initialized?.Invoke();
            while (window.IsOpen)
            {
                // Receive the current events...
                window.DispatchEvents();

                if (!ShouldUpdateNotFocused && isFocused)
                {
                    // Update the engine...
                    Update?.Invoke(clock.Restart().AsMilliseconds());

                    // Render the engine's screen.
                    Render?.Invoke();
                }
                else
                {
                    clock.Restart().AsMilliseconds();
                }

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
