using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int[] tempPuzzle;
    [SerializeField] TextMeshProUGUI t;
    [SerializeField]  List<Piece> pieces = new List<Piece>();
    [SerializeField] public int moveCount;
    [SerializeField] Text win;
    public int emptyPiece;
    bool togglePicture;


    void Start()
    {
        win.text = "";
        t.text = "Moves: " + moveCount;
        emptyPiece = 15;
        moveCount = 0;
        tempPuzzle = new int[16];
        for (int r = 0; r < tempPuzzle.Length; r++)
        {
            Piece p = GameObject.Find("P" + r).GetComponent<Piece>();
            pieces.Add(p);
            tempPuzzle[r] = r;

        }



        Shuffle();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (IsWin())
        {
            Debug.Log("Win");
            win.text = "You Won! Moves: "+moveCount;

        }
        else
        {
            win.text = "";

        }
        t.text = "Moves: " + moveCount;

    }

    public void MovePiece(int pieceNum)
    {
        emptyPiece = FindIndex(15);
        int index = FindIndex(pieceNum);

        if (index %4 !=0 && index - 1 == emptyPiece)
        {
            SwapPieces(index, emptyPiece);
            moveCount++;

        }
        else if ((index+1) % 4 != 0 &&index + 1 == emptyPiece)
        {
            SwapPieces(index, emptyPiece);
            moveCount++;

        }
        else if (index - 4 == emptyPiece)
        {
            SwapPieces(index, emptyPiece);
            moveCount++;


        }
        else if (index + 4 == emptyPiece)
        {
            SwapPieces(index, emptyPiece);
            moveCount++;

        }
        else
        {
           //Debug.Log("Piece cannot move +"+ pieceNum + " i: "+index);
        }


    }
    public void Shuffle()
    {
        moveCount = 0;

        for (int i = 0; i < 20; i++)
        {
            for (int p = 0; p < 16; p++)
            {
                int random = UnityEngine.Random.Range(0, 4);
                emptyPiece = FindIndex(15);

                if (random == 0 && p + 4 == emptyPiece)
                {
                    SwapPieces(p, emptyPiece);
                    p =+4;

                }
                if (random == 1 && p - 4 == emptyPiece) {
                    SwapPieces(p, emptyPiece);
                    p =-4;
                }
                else if (random == 2 && ((p + 1) % 4 != 0 )&& p + 1 == emptyPiece)
                {
                    SwapPieces(p, emptyPiece);
                    p = +1;
                }
                else if (random == 3 && (p % 4 != 0) && p - 1 == emptyPiece)
                {
                    SwapPieces(p, emptyPiece);
                    p =-1;
                }
            }

        }
        emptyPiece = FindIndex(15);
    }
        int FindIndex(int pieceNum)
        {
            for(int i = 0; i < 16; i++) {
                if (tempPuzzle[i] == pieceNum)
                {
                    return i;
                }
               
            }
            return -1;
        }
    void SwapPieces(int pIndex,int emptyPiece)
    {

        int tempValue = tempPuzzle[pIndex];
        int emptyValue = tempPuzzle[emptyPiece];

        GameObject tempP = GameObject.Find("P" + tempValue);
        GameObject emptyP = GameObject.Find("P"+ emptyValue);
        Vector3 tempPos = tempP.transform.position;
        tempP.transform.position = emptyP.transform.position;
        emptyP.transform.position = tempPos;
       
        tempPuzzle[pIndex] = emptyValue;
        tempPuzzle[emptyPiece] = tempValue;
    }
    bool IsWin()
    {
        for (int r = 0; r < tempPuzzle.Length; r++)
        {

            if (tempPuzzle[r] != r)
                return false; 
        }
        return true;
    }
    public void ToggleNum()
    {
        if (!togglePicture)
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                pieces[i].toggleNum = true;
        }

        togglePicture = true;
    }
    
        else
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                pieces[i].toggleNum = false;

            }
            togglePicture = false;

        }

    }

        }
   


            //choose to swap up down left or right
            //while loop, until the is a place where it can be swapped
            //swap places


        


//pick a random piece
//pick a random piece around it
//swap places
// repeat for 1000 times
/* THIS IS MY SAD CODE
 * 
 *   rowEmpty = 3;
    colEmpty = 3;
    for (int i = 0; i < 1; i++)
    {
        Vector2 tempPos;
        Piece temp;
        //  if(rowEmpty ==0)
        if (rowEmpty == 0 && colEmpty == 0) 
        {
            int randDir = Random.Range(0, 2);
            Debug.Log("Shuffle1");

            switch (randDir)
            {
                case 0:
                    temp = puzzle[rowEmpty+1, colEmpty];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty + 1, colEmpty] = empty;
                    empty.row = empty.row + 1;
                    rowEmpty = empty.row;
                    colEmpty = empty.col;
                     tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty-1, colEmpty].transform.position = tempPos;
                    break;
                case 1:
                    temp = puzzle[rowEmpty, colEmpty + 1];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty, colEmpty + 1] = empty;
                    rowEmpty = empty.row;
                    empty.col = empty.col + 1;
                    colEmpty = empty.col;
                     tempPos = puzzle[rowEmpty, colEmpty].transform.position;

                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty, colEmpty-1].transform.position = tempPos;
                    break;

            }
        }
       else if (rowEmpty == 0 && colEmpty == 3)
        {
            Debug.Log("Shuffle2");

            int randDir = Random.Range(0, 2);
            switch (randDir)
            {

                case 0:
                     temp = puzzle[rowEmpty + 1, colEmpty];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty + 1, colEmpty] = empty;
                    empty.row = empty.row + 1;
                    rowEmpty = empty.row;
                    colEmpty = empty.col;
                     tempPos = puzzle[rowEmpty, colEmpty].transform.position;

                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty-1, colEmpty ].transform.position = tempPos;
                    break;
                case 1:
                    temp = puzzle[rowEmpty, colEmpty - 1];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty, colEmpty - 1] = empty;
                    rowEmpty = empty.row;
                    empty.col = empty.col - 1;
                    colEmpty= empty.col;
                     tempPos = puzzle[rowEmpty, colEmpty].transform.position;

                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty, colEmpty+1].transform.position = tempPos;
                    break;

            }
        }
        else if (rowEmpty == 3 && colEmpty == 0)
        {
            Debug.Log("Shuffle3");

            int randDir = Random.Range(0, 2);
            switch (randDir)
            {

                case 0:
                    temp = puzzle[rowEmpty - 1, colEmpty];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty - 1, colEmpty] = empty;
                    empty.row = empty.row - 1;
                    rowEmpty = empty.row;
                    colEmpty = empty.col;
                     tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty+1, colEmpty ].transform.position = tempPos;
                    break;
                case 1:
                    temp = puzzle[rowEmpty, colEmpty +1];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty, colEmpty + 1] = empty;
                    rowEmpty = empty.row;
                    empty.col = empty.col + 1;
                    colEmpty= empty.col;
                     tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty, colEmpty-1].transform.position = tempPos;

                    break;

            }
        }
        else if (rowEmpty == 3 && colEmpty == 3)
        {
            Debug.Log("Shuffle4");

           int randDir = Random.Range(0, 2);
            switch (randDir)
            {

                case 0:
                    temp = puzzle[rowEmpty - 1, colEmpty];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty - 1, colEmpty] = empty;
                    empty.row = empty.row - 1;
                    rowEmpty = empty.row;
                    empty.col = colEmpty;
                    //puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                     tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty+1, colEmpty ].transform.position = tempPos;
                    break;
                case 1:
                    temp = puzzle[rowEmpty, colEmpty - 1];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty, colEmpty - 1] = empty;
                   empty.row = rowEmpty;
                    empty.col = empty.col - 1;
                    colEmpty = empty.col;
                     tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty, colEmpty+1].transform.position = tempPos;
                    break;

            }
        }
        else if (rowEmpty == 0 && colEmpty > 0 && colEmpty < 3) //top
        {
            Debug.Log("Shuffle5");

            int randDir = Random.Range(0, 3);

            switch (randDir)
            {

                case 0:
                     temp = puzzle[rowEmpty + 1, colEmpty];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty + 1, colEmpty] = empty;
                    empty.row = empty.row + 1;
                    rowEmpty = empty.row;
                    colEmpty = empty.col;
                     tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty-1, colEmpty ].transform.position = tempPos;
                    break;
                case 1:
                    temp = puzzle[rowEmpty, colEmpty - 1];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty, colEmpty - 1] = empty;
                    rowEmpty = empty.row;
                    empty.col = empty.col - 1;
                    colEmpty= empty.col; 
                     tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty, colEmpty+1].transform.position = tempPos;
                    break;
                case 2:
                    temp = puzzle[rowEmpty, colEmpty + 1];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty, colEmpty + 1] = empty;
                    rowEmpty = empty.row;
                    empty.col = empty.col + 1;
                    colEmpty = empty.col;
                     tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty, colEmpty-1].transform.position = tempPos;
                    break;
                default:
                    Debug.Log("Error 1");
                    break;
            }

        }
        else if (rowEmpty == 3 && colEmpty > 0 && colEmpty < 3) // bot
        {
            int randDir = Random.Range(0, 3);
            Debug.Log("Shuffle6");

            switch (randDir)
            {
                case 0:
                    temp = puzzle[rowEmpty - 1, colEmpty];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty - 1, colEmpty] = empty;
                    empty.row = empty.row - 1;
                    rowEmpty = empty.row;
                    colEmpty = empty.col;
                     tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty+1, colEmpty ].transform.position = tempPos;
                    break;

                case 1:
                    temp = puzzle[rowEmpty, colEmpty - 1];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty, colEmpty - 1] = empty;
                    rowEmpty = empty.row;
                    empty.col = empty.col - 1;
                    colEmpty= empty.col;
                     tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty, colEmpty+1].transform.position = tempPos;
                    break;
                case 2:
                    Debug.Log("Shuffle6 Less than 3? " + colEmpty);

                    temp = puzzle[rowEmpty, colEmpty + 1];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty, colEmpty + 1] = empty;
                    rowEmpty = empty.row;
                    empty.col = empty.col + 1;
                    colEmpty = empty.col;
                     tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty, colEmpty-1].transform.position = tempPos;

                    break;
                default:
                    Debug.Log("Error 1");
                    break;
            }

        }
        else if (colEmpty == 0 && rowEmpty > 0 && rowEmpty < 3) // left 
        {
            Debug.Log("Shuffle7");

            int randDir = Random.Range(0, 3);

            switch (randDir)
            {
                case 0:
                    temp = puzzle[rowEmpty - 1, colEmpty];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty - 1, colEmpty] = empty;
                    empty.row = empty.row - 1;
                    rowEmpty = empty.row;
                    colEmpty = empty.col;
                    tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty+1, colEmpty ].transform.position = tempPos;
                    break;
                case 1:
                     temp = puzzle[rowEmpty + 1, colEmpty];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty + 1, colEmpty] = empty;
                    empty.row = empty.row + 1;
                    rowEmpty = empty.row;
                    colEmpty = empty.col;
                    tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty-1, colEmpty ].transform.position = tempPos;
                    break;
                case 2:
                    temp = puzzle[rowEmpty, colEmpty + 1];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty, colEmpty + 1] = empty;
                    rowEmpty = empty.row;
                    empty.col = empty.col + 1;
                    colEmpty = empty.col;
                    tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty, colEmpty-1].transform.position = tempPos;

                    break;
                default:
                    Debug.Log("Error 1");
                    break;
            }
        }
        else if (colEmpty == 3 && rowEmpty > 0 && rowEmpty < 3) // right 
        {
            Debug.Log("Shuffle8");
            int randDir = Random.Range(0, 3);

            switch (randDir)
            {
                case 0:
                    temp = puzzle[rowEmpty - 1, colEmpty];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty - 1, colEmpty] = empty;
                    empty.row = empty.row - 1;
                    rowEmpty = empty.row;
                    colEmpty = empty.col;
                    tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty+1, colEmpty ].transform.position = tempPos;
                    break;
                case 1:
                     temp = puzzle[rowEmpty + 1, colEmpty];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty + 1, colEmpty] = empty;
                    empty.row = empty.row + 1;
                    rowEmpty = empty.row;
                    colEmpty = empty.col;
                    tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty-1, colEmpty ].transform.position = tempPos;
                    break;
                case 2:
                    temp = puzzle[rowEmpty, colEmpty - 1];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty, colEmpty - 1] = empty;
                    rowEmpty = empty.row;
                    empty.col = empty.col - 1;
                    colEmpty= empty.col;
                    tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty, colEmpty+1].transform.position = tempPos;
                    break;
                default:
                    Debug.Log("Error 1");
                    break;
            }

        }
        else if( rowEmpty > 0 && rowEmpty < 3 && colEmpty>0 && colEmpty < 3 )
        {
            Debug.Log("Shuffle9");

            int randDir = Random.Range(0, 4);

            switch (randDir)
            {
                case 0:
                    temp = puzzle[rowEmpty - 1, colEmpty];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty - 1, colEmpty] = empty;
                    empty.row = empty.row - 1;
                    rowEmpty = empty.row;
                    colEmpty = empty.col;
                    tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty+1, colEmpty ].transform.position = tempPos;
                    break;
                case 1:
                    temp = puzzle[rowEmpty + 1, colEmpty];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty + 1, colEmpty] = empty;
                    empty.row = empty.row + 1;
                    rowEmpty = empty.row;
                    colEmpty = empty.col;
                    tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty-1, colEmpty ].transform.position = tempPos;
                    break;
                case 2:
                    temp = puzzle[rowEmpty, colEmpty - 1];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty, colEmpty - 1] = empty;
                    rowEmpty = empty.row;
                    colEmpty = empty.col - 1;
                    tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty, colEmpty+1].transform.position = tempPos;
                    break;
                case 3:
                    temp = puzzle[rowEmpty, colEmpty + 1];
                    puzzle[rowEmpty, colEmpty] = temp;
                    puzzle[rowEmpty, colEmpty + 1] = empty;
                    rowEmpty = empty.row;
                    empty.col = empty.col + 1;
                    colEmpty = empty.col;
                    tempPos = puzzle[rowEmpty, colEmpty].transform.position;
                    puzzle[rowEmpty, colEmpty].transform.position = temp.transform.position;
                    puzzle[rowEmpty, colEmpty -1].transform.position = tempPos;
                    break;
                default:
                    Debug.Log("Error 1");
                    break;
            }

        }
        else
        {
            Debug.Log("Bad Piece " + rowEmpty + " " + colEmpty);

        }*/

