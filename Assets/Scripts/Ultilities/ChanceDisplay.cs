using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChanceDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI chanceDescription;

    public void Display(string chance)
    {
        chanceDescription.text = chance;
    }
}
