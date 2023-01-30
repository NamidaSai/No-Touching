using UnityEngine;

namespace Utilities
{
    public class PersistentObjects : MonoBehaviour
    {
        private static PersistentObjects _instance = null;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}