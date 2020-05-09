using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonMix : MonoBehaviour
{

    Button buttonMix;

    int pushCount;
    float delayCount  = 0.5f;
    float timer;
    // Start is called before the first frame update
    void Start()
    {

        if (gameObject.TryGetComponent(out Button button)) {

            buttonMix = button;
            buttonMix.onClick.AddListener(MixGrid);
            
        }
        
    }

    //Double click protect handler
    private void Update()
    {

        if (pushCount > 0) {

            timer += Time.deltaTime;

        }

        if (timer >= delayCount) {

            timer = 0;
            pushCount = 0;

        }

    }

    public void MixGrid() {
        
        if (pushCount == 0) {
            
            pushCount++;
            GridController.instance.MixGrid();

        }
        
    }

}
