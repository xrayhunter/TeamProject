using GameProject.Core.Entities;
using SFML.Window;
using System.Collections.Generic;

namespace GameProject.Core.GameStates
{
    public abstract class GameState
    {
        #region Events
        public delegate void _KeyPressed_(KeyEventArgs e);
        protected event _KeyPressed_ keyPressed;

        public delegate void _KeyReleased_(KeyEventArgs e);
        protected event _KeyReleased_ keyReleased;

        public delegate void _TouchBegan_(TouchEventArgs e);
        protected event _TouchBegan_ touchBegan;

        public delegate void _TouchEnd_(TouchEventArgs e);
        protected event _TouchEnd_ touchEnd;

        public delegate void _TouchMoved_(TouchEventArgs e);
        protected event _TouchMoved_ touchMoved;
        #endregion

        protected int ID;

        protected List<Entity> entities;

        /// <summary>
        /// Constructs the GameState.
        /// </summary>
        /// <param name="ID"></param>
        public GameState(int ID)
        {
            this.ID = ID;
            entities = new List<Entity>();
        }

        /// <summary>
        /// De-structure... [Called when this object being deleted]
        /// </summary>
        ~GameState()
        {
            // Remove the list officially from memory...
            entities.Clear();
            entities = null;

            Destroy();
        }

        /// <summary>
        /// Initializes the gamestate
        /// </summary>
        public abstract void Initialize();
        
        /// <summary>
        /// Updates the gamestate
        /// </summary>
        /// <param name="deltaTime"></param>
        public abstract void Update(float deltaTime);

        /// <summary>
        /// Renders the gamestate
        /// </summary>
        public abstract void Render();

        /// <summary>
        /// Destroys the gamestate
        /// </summary>
        public abstract void Destroy();

        /// <summary>
        /// Adds a Entity to the state.
        /// </summary>
        /// <param name="entity"></param>
        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        /// <summary>
        /// Grabs the entities that are inside the state.
        /// </summary>
        /// <returns></returns>
        public List<Entity> GetEntities()
        {
            return entities;
        }

        /// <summary>
        /// Invokes the according touch data to the state.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="e"></param>
        public void PassMobileInputs(GameStateManager.MobileInputTypes type, TouchEventArgs e)
        {
            switch(type)
            {
                case GameStateManager.MobileInputTypes.TouchBegan:
                    touchBegan?.Invoke(e);
                    break;
                case GameStateManager.MobileInputTypes.TouchEnd:
                    touchEnd?.Invoke(e);
                    break;
                case GameStateManager.MobileInputTypes.TouchMoved:
                    touchMoved?.Invoke(e);
                    break;
            }
        }

        /// <summary>
        /// Invokes the according keyboard data to the state.
        /// </summary>
        /// <param name="Pressed"></param>
        /// <param name="e"></param>
        public void PassKeyboardInputs(bool Pressed, KeyEventArgs e)
        {
            if (Pressed)
                keyPressed?.Invoke(e);
            else
                keyReleased?.Invoke(e);
        }

        /// <summary>
        /// Grabs the Identification of the gamestate.
        /// </summary>
        /// <returns></returns>
        public int GetID()
        {
            return ID;
        }
    }
}
