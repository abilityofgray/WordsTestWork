using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDataSettings")]
public class WordsGameDataSettings : ScriptableObject
{
   
    [SerializeField]
    private string lettersDataSource = "Q,W,E,R,T,Y,U,I,O,P,A,S,D,F,G,H,J,K,L,Z,X,C,V,B,N,M," +
            "Q,W,E,R,T,Y,U,I,O,P,A,S,D,F,G,H,J,K,L,Z,X,C,V,B,N,M," +
            "Q,W,E,R,T,Y,U,I,O,P,A,S,D,F,G,H,J,K,L,Z,X,C,V,B,N,M," +
            "Q,W,E,R,T,Y,U,I,O,P,A,S,D,F,G,H,J,K,L,Z,X,C,V,B,N,M," +
            "Q,W,E,R,T,Y,U,I,O,P,A,S,D,F,G,H,J,K,L,Z,X,C,V,B,N,M," +
            "Q,W,E,R,T,Y,U,I,O,P,A,S,D,F,G,H,J,K,L,Z,X,C,V,B,N,M,";

    [SerializeField]
    private float defaultCellSpace = 400;

    [SerializeField]
    private int maxGridWidthSize = 12;
    [SerializeField]
    private int maxGridHeightSize = 12;

    public string GetLettersDataSource { get { return lettersDataSource; } }
    public float GetDefaultCellSpace { get { return defaultCellSpace; } }
    public int GetMaxGridWidhtSize { get { return maxGridWidthSize; } }
    public int GetMaxGridHeighSize { get { return maxGridHeightSize; } }

}
