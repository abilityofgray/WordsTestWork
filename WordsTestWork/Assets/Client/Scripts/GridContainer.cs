using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


//Delete
public class GridContainer : MonoBehaviour
{

    public GameObject CellPrefab;

    public int ColumnLength, RowLength;
    public int ElementNumbers;
    string lettersSource;
    string[] letterArray;
    List<GameObject> lettersGridElement = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

        //InitGrid();

    }

    private void InitGrid()
    {

        lettersSource = "q,w,e,r,t,y,u,i,o,p,a,s,d,f,g,h,j,k,l,z,x,c,v,b,n,m";

        letterArray = lettersSource.Split(',');

        if (ElementNumbers <= letterArray.Length) {

            for (int i = 0; i < letterArray.Length; i++)
            {

                GameObject go = Instantiate(CellPrefab, transform);
                go.GetComponent<LetterElement>().Index = i;

                if (go.TryGetComponent(out TextMeshProUGUI text))
                {

                    text.text = letterArray[i];

                }

                lettersGridElement.Add(go);

            }

        }

        GridLayoutGroup gridLayout = transform.GetComponent<GridLayoutGroup>();
        //Vector3 cellPos = gridLayout.startAxis;
        
        //Debug.Log(cellPos);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
