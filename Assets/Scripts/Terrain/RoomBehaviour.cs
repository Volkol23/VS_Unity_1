using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Walls; // 0 - Up / 1 - Down / 2 - Right / 3 - Left

    [SerializeField]
    private GameObject[] Doors; // 0 - Up / 1 - Down / 2 - Right / 3 - Left

    public void UpdateRoom(bool[] Status)
    {
        // Status True = Door Open / False = Door Closed
        for(int i = 0; i < Status.Length; i++)
        {
            Doors[i].SetActive(Status[i]);
            Walls[i].SetActive(!Status[i]);
        }

    }
}
