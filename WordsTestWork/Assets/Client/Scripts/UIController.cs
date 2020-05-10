using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject popUpWarnigs;
    [SerializeField]
    private Button ButtonMix;
    [SerializeField]
    private Button ButtonGenerate;

    public static UIController instance = null;

    void Start()
    {

        if (instance == null)
            instance = this;

    }

    //Pop Up Warnings Controller
    public void PopUpWarnings(bool activate, string text) {

        if (text != null) {

            if (popUpWarnigs.transform.GetChild(1).TryGetComponent(out TextMeshProUGUI textValue)) {

                textValue.text = text;

            }
            
        }
        popUpWarnigs.SetActive(activate);

    }

}
