using UnityEngine;
using System.Collections.Generic;

public class TrapManager : MonoBehaviour
{
    [SerializeField] int maxTrapsActive = 2;
    [SerializeField] bool trapsCanStaySame = false;
    List<Trap> trapsInScene = new List<Trap>();

    private void Start()
    {
        SwitchTraps();
    }

    public void AddTrapToList(Trap trap)
    {
        trapsInScene.Add(trap);
    }

    public void SwitchTraps()
    {
        int newTrapActive = 0;
        List<Trap> potentialTraps = new List<Trap>();

        foreach (Trap trap in trapsInScene)
        {
            if (trap.gameObject.activeSelf)
            {
                trap.gameObject.SetActive(false);
            }
            else if (!trapsCanStaySame)
            {
                potentialTraps.Add(trap);
            }
        }

        if (potentialTraps.Count == 0)
        {
            potentialTraps = trapsInScene;
        }

        while (newTrapActive < maxTrapsActive)
        {
            int randomIndex = Random.Range(0, potentialTraps.Count);
            Trap potentialTrap = potentialTraps[randomIndex];

            if (!potentialTrap.gameObject.activeSelf)
            {
                potentialTrap.gameObject.SetActive(true);
                newTrapActive++;
            }
        }
    }
}