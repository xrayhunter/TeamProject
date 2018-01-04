using GameProject.Core.AIBrains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Core.Entities
{
    public abstract class Entity
    {
        public IAIBrain aiBrain;

        /// <summary>
        /// Blank Overload constructor.
        /// </summary>
        public Entity() { }

        /// <summary>
        /// Sets the AIBrain controller to this entity, and it takes over.
        /// </summary>
        /// <param name="aiBrain"></param>
        public Entity(IAIBrain aiBrain)
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
