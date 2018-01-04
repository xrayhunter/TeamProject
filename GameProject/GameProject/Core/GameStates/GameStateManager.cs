using GameProject.Core.Entities;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Core.GameStates
{
    public class GameStateManager
    {
        private Stack<GameState> states = new Stack<GameState>();

        ~GameStateManager()
        {
            Destroy();
        }

        /// <summary>
        /// Adds a state to the top of the list, which means this new state becomes the current one, and under ones will stay.
        /// </summary>
        /// <param name="state"></param>
        public void AddState(GameState state, bool removeWith = false)
        {
            if (StateExists(state))
            {
#if DEBUG
                Console.WriteLine("Failed to load GameState, due to it already existing or using a pre-existing state.");
#endif
                return;
            }

            if (removeWith)
                RemoveState();

            state.Initialize();

            // Add it to the top.
            states.Push(state);
        }

        /// <summary>
        /// Removes the top/current state off the screen.
        /// </summary>
        public void RemoveState()
        {
            // Make sure that it has items inside to pop off the top.
            if(states.Count != 0)
                states.Pop();

            // Reset the memory just for the states stack.
            if (states.Count == 0)
            {
                states.Clear();
                states = new Stack<GameState>();
            }
        }
        
        /// <summary>
        /// Gets the current screen that we are looking at.
        /// </summary>
        /// <returns></returns>
        public GameState GetCurrentState()
        {
            try
            {
                return states.Peek();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Finds the state by an ID.
        /// </summary>
        /// <param name="stateID"></param>
        /// <returns></returns>
        public GameState FindState(int stateID = -1)
        {
            // Default to the current state...
            if(stateID < 0)
                return GetCurrentState();

            foreach (GameState state in states.ToArray())
            {
                if(state.GetID() == stateID)
                {
                    return state;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks, if a state exists.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool StateExists(GameState state)
        {
            return StateExists(state.GetID());
        }

        /// <summary>
        /// Checks, if a state exists by an ID.
        /// </summary>
        /// <param name="stateID"></param>
        /// <returns></returns>
        public bool StateExists(int stateID)
        {
            if ( FindState(stateID) != null )
                return true;
            return false;
        }

        /// <summary>
        /// Adds an Entity to the current State.
        /// </summary>
        /// <param name="ent"></param>
        public void AddEntityToState(Entity ent)
        {
            if(GetCurrentState() != null)
                AddEntityToState(ent, GetCurrentState());
        }

        /// <summary>
        /// Adds an Entity to a state.
        /// </summary>
        /// <param name="ent"></param>
        /// <param name="state"></param>
        public void AddEntityToState(Entity ent, GameState state)
        {
            if (state != null)
                AddEntityToState(ent, state.GetID());
        }

        /// <summary>
        /// Adds an Entity to a state by an ID.
        /// </summary>
        /// <param name="ent"></param>
        /// <param name="stateID"></param>
        public void AddEntityToState(Entity ent, int stateID)
        {
            foreach(GameState state in states)
            {
                if (state.GetID() == stateID)
                {
                    state.AddEntity(ent);
                    break;
                }
            }
        }

        /// <summary>
        /// Updates the current state, and updates its entities.
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime)
        {
            GameState state = FindState(-1);
            if (state != null)
            {
                state.Update(deltaTime);

                foreach (Entity ent in state.GetEntities().ToArray())
                {
                    ent.Update(deltaTime);
                }
            }

        }

        /// <summary>
        /// Renders the current state, and renders its entities.
        /// </summary>
        public void Render()
        {
            GameState state = FindState(-1);
            if (state != null)
            {
                state.Render();

                foreach (Entity ent in state.GetEntities().ToArray())
                {
                    ent.Render();
                }
            }
        }

        public enum MobileInputTypes
        {
            TouchBegan,
            TouchEnded,
            TouchMoved
        }

        /// <summary>
        /// Passes Mobile data to the current state.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="e"></param>
        public void PassMobileInputs(MobileInputTypes type, TouchEventArgs e)
        {
            GameState state = FindState(-1);

            if (state != null)
                state.PassMobileInputs(type, e);
        }

        /// <summary>
        /// Passes Keyboard data to the current state.
        /// </summary>
        /// <param name="Pressed"></param>
        /// <param name="e"></param>
        public void PassKeyboardInput(bool Pressed, KeyEventArgs e)
        {
            GameState state = FindState(-1);

            if (state != null)
                state.PassKeyboardInputs(Pressed, e);
        }

        /// <summary>
        /// Destroys everything inside GameStateManager.
        /// </summary>
        public void Destroy()
        {
            foreach(GameState state in states.ToArray())
            {
                state.Destroy();
            }
            states.Clear();
            states = null;
        }
    }
}
