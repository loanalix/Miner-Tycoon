using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuOption : MonoBehaviour
{
    public GameObject OptionMenu;

    //show option screen
    public void OpenOption()
    {
        OptionMenu.SetActive(true);
    }

    //hide option screen
    public void CloseOption()
    {
        OptionMenu.SetActive(false);
    }

}
