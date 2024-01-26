using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHouseColor : MonoBehaviour
{

    public void SetColor(Color color)
    {
        Renderer rendererComponent = GetComponent<Renderer>();

        Material[] materials = rendererComponent.materials;

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = color;
        }

        rendererComponent.materials = materials;
    }

}
