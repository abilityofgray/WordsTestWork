using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputWidth;

    [SerializeField]
    private TMP_InputField inputHeight;

    int widthMaxLenght;   //data from setting
    int heighMaxtLenght;   //data from setting

    public int SetMaxWidthLenght {set { widthMaxLenght = value; }}
    public int SetMaxHeightLenght { set { heighMaxtLenght = value; }}

    public static InputController instance = null;
    // Start is called before the first frame update
    void Awake()
    {

        if (instance == null)
            instance = this;

    }

    public int InputWidthValidate() {

        string inputText = inputWidth.text;
        
        //check for null or empty
        if (!string.IsNullOrEmpty(inputText))
        {

            if (int.TryParse(inputText, out int result))
            {
                //check for input int less than possible widht
                if (result <= widthMaxLenght)
                {

                    return result;

                }
                else
                {

                    //Int is more than exist
                    return 0;

                }

            }
            else {

                return 0;

            }

        }
        else
        {

            //Field is Empty
            return 0;

        }

    }

    public int InputHeightValidate()
    {

        string inputText = inputHeight.text;

        //Check for Empty or Null
        if (!string.IsNullOrEmpty(inputText))
        {
            //Check for character no bigger then exist row
            if (int.TryParse(inputText, out int result))
            {

                if (result <= widthMaxLenght)
                {

                    return result;

                }
                else
                {

                    return 0;

                }

            }
            else
            {

                return 0;

            }

        }
        else
        {

            return 0;

        }

    }
}
