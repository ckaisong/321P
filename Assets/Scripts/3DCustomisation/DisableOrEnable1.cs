using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOrEnable1 : MonoBehaviour
{

    public GameObject Reset;
    public GameObject Dropdown;
    public GameObject Spawn;
    public GameObject Indicator;
    public GameObject Guide;

    public void whenButtonClicked()
    {
        if (Reset.activeInHierarchy == true)
            Reset.SetActive(false);
        else
            Reset.SetActive(true);
        if (Dropdown.activeInHierarchy == true)
            Dropdown.SetActive(false);
        else
            Dropdown.SetActive(true);

        if (Spawn.activeInHierarchy == true)
            Spawn.SetActive(false);
        else
            Spawn.SetActive(true);

        if (Reset.activeInHierarchy == true)
            Indicator.SetActive(true);
        else
            Indicator.SetActive(false);

        if (Guide.activeInHierarchy == true)
            Guide.SetActive(false);
        else
            Guide.SetActive(true);

    }
}

