using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GridController : MonoBehaviour
{

    public static GridController instance = null;

    public GameObject GridCharPrefab;
    public GameObject WordsGridContainer;

    public WordsGameDataSettings DataSettings;

    public float X_Start, Y_Start;
    public int ColumnLength, RowLength;
    public float X_Space, Y_Space;

    Vector3 velocity = Vector3.up;

    public bool mix = false;

    string lettersSource;
    string[] letterArray;
    List<GameObject> lettersDataList = new List<GameObject>();
    List<GameObject> lettersElemetOnGameGrid = new List<GameObject>(); 
    List<Vector3> gridPoints = new List<Vector3>();
    // Start is called before the first frame update

    public int GridWidthSize;
    public int GridHeigthSize;
    private float elementOnGameGrid;
    int letterArrayCount;
    float delayCounter; //delayCounter for reset mix flag for animation

    void Start()
    {
        if (instance == null)
            instance = this;

        InitGridSettings();

    }

    private void InitGridSettings ()
    {

        InputController.instance.SetMaxWidthLenght = DataSettings.GetMaxGridWidhtSize;
        InputController.instance.SetMaxHeightLenght = DataSettings.GetMaxGridHeighSize;

        letterArray = DataSettings.GetLettersDataSource.Split(',');

        letterArrayCount = letterArray.Length;

        for (int i = 0; i < letterArray.Length; i++)
        {

            GameObject go = Instantiate(GridCharPrefab, WordsGridContainer.transform);
            go.GetComponent<LetterElement>().Index = i;

            if (go.TryGetComponent(out TextMeshProUGUI text))
            {

                text.text = letterArray[i];

            }

            lettersDataList.Add(go);

        }

    }

    //Draw grid
    public void DrawGrid()
    {

        mix = false;
        ;
        //validate input for non empty and not string and check for 
        ColumnLength = InputController.instance.InputWidthValidate();
        RowLength = InputController.instance.InputHeightValidate();

        //Check for inputField not empty  
        if (ColumnLength > 0 &&
            RowLength > 0)
        {

            int mulCountColumnRow = ColumnLength * RowLength;
            elementOnGameGrid = mulCountColumnRow;

            for (int k = 0; k < lettersElemetOnGameGrid.Count; k++)
            {

                lettersElemetOnGameGrid[k].GetComponent<LetterElement>().OnGrid = false;
                lettersElemetOnGameGrid[k].GetComponent<RectTransform>().transform.localPosition = GridCharPrefab.transform.localPosition;
                
            }

            lettersElemetOnGameGrid.Clear();

            CheckScreenWidthHeight();

            for (int i = 0; i < mulCountColumnRow; i++)
            {

                var r = RandomNoDuplicate();
                
                if (lettersDataList[r].TryGetComponent(out LetterElement lelement)) {

                    
                    lelement.OnGrid = true;

                }

                Vector2 elementPosition = new Vector2(X_Start + (X_Space * (i % ColumnLength)), Y_Start + (-Y_Space * (i / ColumnLength)));

                if (lettersDataList[r].TryGetComponent(out RectTransform rectTrans)) {

                    rectTrans.localPosition = elementPosition;

                }
                
                lettersElemetOnGameGrid.Add(lettersDataList[r]);

                if (ColumnLength > RowLength)
                {

                    ChangeLettersSize((int)(200 / ColumnLength));

                }
                else
                {

                    ChangeLettersSize((int)(200 / RowLength));

                }



            }

            WordsGridContainer.GetComponent<RectTransform>().localPosition = new Vector3(-lettersElemetOnGameGrid[mulCountColumnRow - 1].transform.localPosition.x / 2,
                (-lettersElemetOnGameGrid[mulCountColumnRow - 1].transform.localPosition.y / 2),
                0);

        }
        else {

           UIController.instance.PopUpWarnings(true,
                    "Введите ширину и высоту не больше 12");
                
        }

    }

    /// <summary>
    /// Pick element from element Data to grid, with diplicate check
    /// </summary>
    /// <returns></returns>
    public int RandomNoDuplicate()
    {

        var random = new System.Random();
        int r = Random.Range(0, lettersDataList.Count - 1);
        if (lettersElemetOnGameGrid.Contains(lettersDataList[r]))
        {
            
            return RandomNoDuplicate();

        }
        else {

            return r;

        }

    }

    void CheckScreenWidthHeight() {

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        
        float GameGridWidth = (ColumnLength * 30) + (X_Space * (ColumnLength - 1));
        float GameGridHeight = (RowLength * 30) + (Y_Space * (RowLength - 1));

        
        float width = screenWidth - GameGridWidth;

        float cellSpace = DataSettings.GetDefaultCellSpace;

        if (ColumnLength > RowLength)
        {

            X_Space = cellSpace / ColumnLength;
            Y_Space = cellSpace / ColumnLength;

        }
        else {

            X_Space = cellSpace / RowLength;
            Y_Space = cellSpace / RowLength;

        }

    }

    void ChangeLettersSize(int fontSize) {

        for (int i = 0; i < lettersElemetOnGameGrid.Count; i++) {

            lettersElemetOnGameGrid[i].GetComponent<TextMeshProUGUI>().fontSize = fontSize;

        }

    }
    
    public void MixGrid()
    {

        mix = false;
        if (lettersElemetOnGameGrid.Count != 0)
        {

            gridPoints.Clear();

            for (int i = 0; i < lettersElemetOnGameGrid.Count; i++)
            {

                Vector3 posPoint = lettersElemetOnGameGrid[i].GetComponent<RectTransform>().transform.localPosition;
                gridPoints.Add(posPoint);

            }
            
            RandomMixPoints();

            mix = true;

        }
        else {

            UIController.instance.PopUpWarnings(true, 
                "Нечего перемешивать");

        }
        
    }

    void RandomMixPoints()
    {

        int listCount = gridPoints.Count;
        int lastElement = listCount - 1;
        List<Vector3> randomList = new List<Vector3>();
        //Shuffle gridPoints;
        for (int i = 0; i < lettersElemetOnGameGrid.Count; i++)
        {

            var random = new System.Random();
            
            int r = random.Next(0, gridPoints.Count);
            
            randomList.Add(gridPoints[r]);
            gridPoints.RemoveAt(r);

        }

        gridPoints = randomList;

        ApplyShufflePointsToGridelemets(randomList);

    }

    /// <summary>
    /// Shuffle character collection
    /// </summary>
    
    //Apply Shuffling list of point position to Letters Grid
    void ApplyShufflePointsToGridelemets(List<Vector3> inputList)
    {

        for (int i = 0; i < lettersElemetOnGameGrid.Count; i++)
        {

            Vector3 posPoint = inputList[i];

        }

    }

    private void Update()
    {
        //delete
        if (mix) {

            delayCounter += Time.deltaTime;
            for (int i = 0; i < lettersElemetOnGameGrid.Count; i++) {

                lettersElemetOnGameGrid[i].GetComponent<RectTransform>().transform.localPosition =
                   Vector3.SmoothDamp(lettersElemetOnGameGrid[i].GetComponent<RectTransform>().transform.localPosition, gridPoints[i], ref velocity, 2f * Time.deltaTime);

            }

        }

    }

    //Letters Grid Mix Animation
    //TODO: Delete
    IEnumerator ElementTakePlaceAfterMix(Vector3 startPos, Vector3 endPos)
    {

        while (Vector3.Distance(startPos, endPos) > 0.01f)
        {
            
            for (int i = 0; i < lettersElemetOnGameGrid.Count; i++)
            {

                if (lettersElemetOnGameGrid[i].GetComponent<LetterElement>().OnGrid)
                    lettersElemetOnGameGrid[i].GetComponent<RectTransform>().transform.localPosition =
                        Vector3.SmoothDamp(lettersElemetOnGameGrid[i].GetComponent<RectTransform>().transform.localPosition, gridPoints[i], ref velocity, 6f * Time.deltaTime);

              //if yield here manimate pattern "Matrix Style"  
            }
            
            //if yield here animte pattern "Blob"
            yield return null;
            
        }

    }

}
