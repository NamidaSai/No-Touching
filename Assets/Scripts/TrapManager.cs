using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TrapManager : MonoBehaviour
{
    [SerializeField] int maxTrapsActive = 2;
    [SerializeField] bool trapsCanStaySame = false;
    [SerializeField] float switchTrapDelay = 1.1f;
    List<Trap> trapsInScene = new List<Trap>();

    private void Start()
    {
        StartCoroutine(SwitchTraps());
    }

    public void AddTrapToList(Trap trap)
    {
        trapsInScene.Add(trap);
    }

    public IEnumerator SwitchTraps()
    {
        int newTrapActive = 0;
        List<Trap> potentialTraps = new List<Trap>();

        foreach (Trap trap in trapsInScene)
        {
            if (trap.gameObject.activeSelf)
            {
                StartCoroutine(trap.HideTrap());
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

        yield return new WaitForSeconds(switchTrapDelay);

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