using System;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Provides access to all game state that is local to this client
    /// </summary>
    public class LocalGameService : MonoBehaviour
    {
        public static LocalGameService Get
        {
            get
            {
                if (instance == null) throw new Exception("There is no isntance of LocalGameService enabled!");
                return instance;
            }
        }
        private static LocalGameService instance;

        public PlayerScript Player;

        public void OnEnable()
        {
            if (instance != null) Debug.LogError("There is already an instance of LocalGameService enabled!!");
            instance = this;
        }
        public void OnDisable()
        {
            instance = null;
        }
    }
}