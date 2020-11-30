using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToActivateAtStart;
    private void Start()
    {
        for (int i = 0; i < objectsToActivateAtStart.Length; i++)
        {
            objectsToActivateAtStart[i].SetActive(true);
        }   
    }
}
