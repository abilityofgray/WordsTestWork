using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpWarning : MonoBehaviour
{
    [SerializeField]
    private Button btn_Ok;
    [SerializeField]
    private TextMeshProUGUI textComponent;
    // Start is called before the first frame update
    void Start()
    {

        btn_Ok.onClick.AddListener(ClosedPopUp);

    }

    public void OpenPopUp() {

        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
          
    }

    public void ClosedPopUp() {

        if (gameObject.activeSelf)
            gameObject.SetActive(false);

    }

    public void SetTextComponent(string value) {

        textComponent.text = value;

    }
}
