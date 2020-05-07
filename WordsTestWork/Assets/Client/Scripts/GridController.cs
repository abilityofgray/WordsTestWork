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

    Vector3 velocity = Vector3.up;
    bool mix = false;

    string lettersSource;
    string[] letterArray;
    List<GameObject> lettersGridElement = new List<GameObject>();
    List<Vector3> gridPoints = new List<Vector3>();
    // Start is called before the first frame update

    [SerializeField]
    private TMP_InputField inputFieldWidth;
    [SerializeField]
    private TMP_InputField inputFieldHeight;

    private float elementOnGameGrid;
    int letterArrayCount;

    void Start()
    {

        lettersSource = "q,w,e,r,t,y,u,i,o,p,a,s,d,f,g,h,j,k,l,z,x,c,v,b,n,m";

        letterArray = lettersSource.Split(',');

        letterArrayCount = letterArray.Length;


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


        //DrawGrid();

    }

    //Draw grid
    public void DrawGrid()
    {

        //validate input for non empty and not string
        if (int.TryParse(inputFieldWidth.text, out int column)){

            ColumnLength = column;

        }

        if (int.TryParse(inputFieldHeight.text, out int row))
        {

            RowLength = row;

        }

        int mulCountColumnRow = ColumnLength * RowLength;
        elementOnGameGrid = mulCountColumnRow;

        for (int k = 0; k < letterArrayCount; k++) {

            lettersGridElement[k].GetComponent<LetterElement>().OnGrid = false;
            lettersGridElement[k].GetComponent<RectTransform>().transform.localPosition = LETTER_ELEMENT.transform.localPosition;

        }

        for (int i = 0; i < mulCountColumnRow; i++)
        {

            
            Vector2 elementPosition = new Vector2(X_Start + (X_Space * (i % ColumnLength)), Y_Start + (-Y_Space * (i / ColumnLength)));
            lettersGridElement[i].GetComponent<LetterElement>().OnGrid = true;
            lettersGridElement[i].GetComponent<RectTransform>().transform.localPosition = elementPosition;



        }

    }
    /// <summary>
    /// Represent Count Showing letters element on game field
    /// </summary>
    /// <param name="count"></param>
    void HideLetterFromViewOnGameGrid(int count) {

        
        for (int i = 0; i < count; i++)
        {
            if (!lettersGridElement[i].GetComponent<LetterElement>().OnGrid)
                lettersGridElement[i].GetComponent<RectTransform>().transform.localPosition = LETTER_ELEMENT.transform.localPosition;


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

        //delete
        //mix = true;

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
            
            //lettersGridElement[i].GetComponent<RectTransform>().transform.localPosition = posPoint;
            
            StartCoroutine(ElementTakePlaceAfterMix(lettersGridElement[i].GetComponent<RectTransform>().transform.localPosition, gridPoints[i]));
               
        }

    }

    private void Update()
    {
        //delete
        if (mix) {
            Debug.Log("Test");
            for (int i = 0; i < lettersGridElement.Count - 1; i++) {

                lettersGridElement[i].GetComponent<RectTransform>().transform.localPosition =
                    Vector3.SmoothDamp(lettersGridElement[i].GetComponent<RectTransform>().transform.localPosition, gridPoints[i], ref velocity, 2f * Time.deltaTime);

            }
                
        }


    }

    //Letters Grid Mix Animation
    //TODO: Refactor
    IEnumerator ElementTakePlaceAfterMix(Vector3 startPos, Vector3 endPos)
    {

        while (Vector3.Distance(startPos, endPos) > 0.01f)
        {

            for (int i = 0; i < lettersGridElement.Count - 1; i++)
            {

                if (lettersGridElement[i].GetComponent<LetterElement>().OnGrid)
                    lettersGridElement[i].GetComponent<RectTransform>().transform.localPosition =
                        Vector3.SmoothDamp(lettersGridElement[i].GetComponent<RectTransform>().transform.localPosition, gridPoints[i], ref velocity, 6f * Time.deltaTime);
                
            }
            yield return new WaitForFixedUpdate();

        }


    }
}
