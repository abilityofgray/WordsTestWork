using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridController : MonoBehaviour
{

    public GameObject LETTER_ELEMENT;
    public GameObject GAME_GRID;



    public float X_Start, Y_Start;
    public int ColumnLength, RowLength;
    public float X_Space, Y_Space;

    public Vector3 velocity = Vector3.up;
    bool mix = false;

    string lettersSource;
    string[] letterArray;
    List<GameObject> lettersGridElement = new List<GameObject>();
    List<Vector3> gridPoints = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {

        lettersSource = "q,w,e,r,t,y,u,i,o,p,a,s,d,f,g,h,j,k,l,z,x,c,v,b,n,m";

        letterArray = lettersSource.Split(',');



        for (int i = 0; i < letterArray.Length; i++)
        {


            GameObject go = Instantiate(LETTER_ELEMENT, GAME_GRID.transform);
            go.GetComponent<LetterElement>().Index = i;

            if (go.TryGetComponent(out TextMeshProUGUI text))
            {

                text.text = letterArray[i];

            }

            lettersGridElement.Add(go);

        }


        DrawGrid();

    }

    //Draw grid
    void DrawGrid()
    {

        for (int i = 0; i < ColumnLength * RowLength; i++)
        {

            Vector2 elementPosition = new Vector2(X_Start + (X_Space * (i % ColumnLength)), Y_Start + (-Y_Space * (i / ColumnLength)));
            lettersGridElement[i].GetComponent<RectTransform>().transform.localPosition = elementPosition;


        }

    }


    //Take grid point as acnhor and mixed this like a hell
    public void MixGrid()
    {

        for (int i = 0; i < lettersGridElement.Count - 1; i++)
        {

            Vector3 posPoint = lettersGridElement[i].GetComponent<RectTransform>().transform.localPosition;
            gridPoints.Add(posPoint);

        }

        RandomMixPoints();

        mix = true;

    }

    void RandomMixPoints()
    {

        int listCount = gridPoints.Count;
        int lastElement = listCount - 1;

        //Shuffle gridPoints;
        for (int i = 0; i < lastElement; i++)
        {

            //TODO: Add check to do not repeat random value
            int r = Random.Range(i, listCount);
            var tmp = gridPoints[i];
            gridPoints[i] = gridPoints[r];
            gridPoints[r] = tmp;

        }

        ApplyShufflePointsToGridelemets();

    }

    //Apply Shuffling list of point position to Letters Grid
    void ApplyShufflePointsToGridelemets()
    {

        for (int i = 0; i < lettersGridElement.Count - 1; i++)
        {

            Vector3 posPoint = gridPoints[i];
            Debug.Log(posPoint);
            //lettersGridElement[i].GetComponent<RectTransform>().transform.localPosition = posPoint;
            /*
            StartCoroutine(ElementTakePlaceAfterMix(lettersGridElement[i].GetComponent<RectTransform>().transform.localPosition
                , posPoint));
             */   
        }

    }

    private void Update()
    {
        if (mix) {

            for (int i = 0; i < lettersGridElement.Count - 1; i++) {

                /*
                lettersGridElement[i].GetComponent<RectTransform>().transform.localPosition =
                    Vector3.Lerp(lettersGridElement[i].GetComponent<RectTransform>().transform.localPosition, gridPoints[i], 2f * Time.deltaTime);*/
                lettersGridElement[i].GetComponent<RectTransform>().transform.localPosition =
                    Vector3.SmoothDamp(lettersGridElement[i].GetComponent<RectTransform>().transform.localPosition, gridPoints[i], ref velocity, 2f * Time.deltaTime);

            }
                
        }


    }

    //Letters Grid Mix Animation
    IEnumerator ElementTakePlaceAfterMix(Vector3 startPos, Vector3 endPos)
    {

        
        while (Vector3.Distance(startPos, endPos) > 0.01f)
        {

            for (int i = 0; i < lettersGridElement.Count - 1; i++)
            {

                Debug.Log(endPos);
                lettersGridElement[i].GetComponent<RectTransform>().transform.localPosition =
                    Vector3.SmoothDamp(lettersGridElement[i].GetComponent<RectTransform>().transform.localPosition, gridPoints[i], ref velocity, 2f * Time.deltaTime);
                yield return null;
            }
            

        }


    }
}
