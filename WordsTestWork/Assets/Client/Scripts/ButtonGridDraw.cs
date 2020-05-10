using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGridDraw : MonoBehaviour
{

    Button btnDrawGrid;

    // Start is called before the first frame update
    void Start()
    {

        if (gameObject.TryGetComponent(out Button button)) {

            btnDrawGrid = button;
            btnDrawGrid.onClick.AddListener(DrawGrid);

        }

    }

    void DrawGrid() {

        GridController.instance.DrawGrid();

    }

    
}
