using GameProject.Core.Entities;
using System.Collections.Generic;

namespace GameProject.Core.GameStates
{
    public abstract class GameState
    {
        protected int ID;

        protected List<Entity> entities;

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

        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public List<Entity> GetEntities()
        {
            return entities;
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
