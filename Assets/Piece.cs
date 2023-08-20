using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Piece : MonoBehaviour
{
    // Start is called before the first frame update        
    //[SerializeField]  int number { get; set; }
    [SerializeField] public int number;
    public bool toggleNum { get; set; }
    Game g;
    bool clicked;
    [SerializeField] Text text;
    void Start()
    {
        g = GameObject.Find("Game").GetComponent<Game>();
        text.text = "";
      
    }

    // Update is called once per frame
    void Update()
    {
        if (toggleNum)
        {
            text.text = ""+number;

        }
        else
        {
            text.text = "";

        }
    }
    private void OnMouseDown()
    {

        g.MovePiece(number);

         Debug.Log("num"+number);


    }
}
