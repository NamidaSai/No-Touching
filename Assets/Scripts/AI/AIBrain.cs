using Level;
using UnityEngine;

namespace AI
{
    public class AIBrain : MonoBehaviour
    {
        private void Start()
        {
            FindObjectOfType<LevelController>().AddEnemyToList(this);
        }
    }
}