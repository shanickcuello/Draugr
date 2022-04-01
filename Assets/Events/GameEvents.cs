using System;
using UnityEngine;

namespace Events
{
    public class GameEvents : MonoBehaviour
    {
        public static GameEvents instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this);
        }

        public event Action<string> OnUserCollisionWithMessage;

        public void UserCollisionWithMessage(string message)
        {
            OnUserCollisionWithMessage?.Invoke(message);
        }

    }
}
