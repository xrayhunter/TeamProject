using GameProject.Core.AIBrains;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Core.Entities
{
    public abstract class Entity
    {
        private IAIBrain aiBrain;

        private Vector2f position;
        
        /// <summary>
        /// Creates a new Entity object.
        /// </summary>
        /// <param name="position"></param>
        public Entity(Vector2f position = new Vector2f()) { }
        
        /// <summary>
        /// Sets the AIBrain to this entity to control it.
        /// </summary>
        /// <param name="aiBrain"></param>
        /// <param name="position"></param>
        public Entity(IAIBrain aiBrain, Vector2f position = new Vector2f())
        {
            this.aiBrain = aiBrain;
            this.aiBrain.SetEntityControlling(this);
        }

        ~Entity()
        {
            Destroy();
        }

        /// <summary>
        /// Initializes the Entity.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Renders the Entity.
        /// </summary>
        public abstract void Render();

        /// <summary>
        /// Updates the Entity.
        /// </summary>
        /// <param name="deltaTime"></param>
        public abstract void Update(float deltaTime);

        /// <summary>
        /// Destroys the Entity.
        /// </summary>
        public abstract void Destroy();

        public IAIBrain GetAIBrain()
        {
            return aiBrain;
        }
    }
}
