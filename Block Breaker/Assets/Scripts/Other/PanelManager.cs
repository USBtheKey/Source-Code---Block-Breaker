using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{


    [SerializeField] private GameObject[] panels;

    public GameObject[] Panels { get => panels; set => panels = value; }

    public void PanelToggler(int panelIndex)
    {
        Input.ResetInputAxes();
        for(int i = 0; i < Panels.Length; i++)
        {
            Panels[i].SetActive(panelIndex == i);
        }
    }

}
