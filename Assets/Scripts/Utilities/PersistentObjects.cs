using UnityEngine;

namespace Utilities
{
    public class PersistentObjects : MonoBehaviour
    {

        public static PersistentObjects instance = null;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}