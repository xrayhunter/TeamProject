using GameProject.Core.Entities;

namespace GameProject.Core.AIBrains
{
    public interface IAIBrain
    {
        /// <summary>
        /// Initializes the AIBrain script.
        /// </summary>
        void Initialize();
        
        /// <summary>
        /// Updates the AIBrain script.
        /// </summary>
        /// <param name="delta"></param>
        void Update(float delta);

        /// <summary>
        /// Sets the entity that the AIBrain should target and control.
        /// </summary>
        /// <param name="ent"></param>
        void SetEntityControlling(Entity ent);

        /// <summary>
        /// Destroys the AIBrain.
        /// </summary>
        void Destroy();
    }
}
