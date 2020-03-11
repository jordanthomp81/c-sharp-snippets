using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject ButtonToToggle;
    private bool isActive;

    public void toggleButton()
    {
        isActive = !isActive;
        ButtonToToggle.SetActive(isActive);
    }
}
