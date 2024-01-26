using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPrefabs : MonoBehaviour
{
    public GameObject house;

    void Start()
    {
         GameObject gameObject = Instantiate(house, transform.position, transform.rotation);
    }
}
