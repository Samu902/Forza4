using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public enum SlotState
    {
        Empty, Red, Yellow
    }

    public GameObject redC;
    public GameObject yellowC;

    public GameObject redLight;
    public GameObject yellowLight;

    public GameObject resetButton;

    public float[] columnsX;
    private int w = 7;
    private int h = 6;
    private SlotState[,] grid;

    [HideInInspector]
    public int turn;
    [HideInInspector]
    public bool busy;
    [HideInInspector]
    public bool gameOver;

    void Start()
    {
        turn = 0;
        busy = false;
        gameOver = false;
        grid = new SlotState[w, h];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (turn % 2 == 1)
                GenerateCoin(0);
            else
                GenerateCoin(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (turn % 2 == 1)
                GenerateCoin(1);
            else
                GenerateCoin(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (turn % 2 == 1)
                GenerateCoin(2);
            else
                GenerateCoin(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (turn % 2 == 1)
                GenerateCoin(3);
            else
                GenerateCoin(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (turn % 2 == 1)
                GenerateCoin(4);
            else
                GenerateCoin(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (turn % 2 == 1)
                GenerateCoin(5);
            else
                GenerateCoin(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (turn % 2 == 1)
                GenerateCoin(6);
            else
                GenerateCoin(0);
        }
    }

    public int GenerateCoin(int index)
    {
        //-1=busy; 0:colonna piena; 1: tutto ok

        //Se la moneta non ha ancora finito di cadere, esci
        if (busy)
            return -1;

        //Se la colonna è piena, esci
        if (grid[index, h - 1] != SlotState.Empty)
            return 0;

        //Occupato ON
        busy = true;

        //Avanzamento del turno
        turn++;
        GameObject g;
        SlotState state;

        //Scelta della moneta in base al turno
        if (turn % 2 == 1)
        {
            g = redC;
            state = SlotState.Red;
        }
        else
        {
            g = yellowC;
            state = SlotState.Yellow;
        }

        //Inserimento, oltre che nella scena, nell'array
        Instantiate(g, new Vector3(columnsX[index], 7/*4.5f*/, 0), Quaternion.Euler(0, 0, 0)/*Quaternion.identity*/);
        
        int y = 0;
        for (int i = 0; i < 6; i++)
        {
            if (grid[index, i] == SlotState.Empty)
            {
                y = i;
                break;
            }
        }
        grid[index, y] = state;

        return 1;
    }

    public Vector2Int PosInGrid(GameObject g)
    {
        Vector2Int pos = Vector2Int.zero;
        for (int i = 0; i < 7; i++)
        {
            if (columnsX[i] == g.transform.position.x)
                pos.x = i;
        }
        if (grid[pos.x, h - 1] != SlotState.Empty)
        {
            pos.y = h - 1;
        }
        else
        {
            for (int y = 1; y < h; y++)
            {
                if (grid[pos.x, y] == SlotState.Empty)
                {
                    //E' già stato inserito dal metodo GenerateCoin, quindi prende quello sotto
                    pos.y = y - 1;
                    break;
                }
            }
        }
        return pos;
    }

    public void CheckMove(string name, Vector2Int pos)
    {
        //Debug.Log(pos);
        int streak = 0;
        //Verticale
        for (int i = -3; i <= 3; i++)
        {
            if (streak >= 4)
                break;

            if (pos.y + i < 0 || pos.y + i >= h)
            {
                streak = 0;
            }
            else if (grid[pos.x, pos.y + i] == SlotState.Empty)
            {
                streak = 0;
            }
            else if (grid[pos.x, pos.y + i] == SlotState.Red)
            {
                if (name.Contains("Red"))
                    streak++;
                else
                    streak = 0;
            }
            else if (grid[pos.x, pos.y + i] == SlotState.Yellow)
            {
                if (name.Contains("Yellow"))
                    streak++;
                else
                    streak = 0;
            }
        }
        if (streak >= 4)
        {
            Win(name);
            return;
        }

        //Se arrivi qui, significa che il verticale non ha trovato nulla
        //Orizzontale
        streak = 0;
        for (int i = -3; i <= 3; i++)
        {
            if (streak >= 4)
                break;

            if (pos.x + i < 0 || pos.x + i >= w)
            {
                streak = 0;
            }
            else if (grid[pos.x + i, pos.y] == SlotState.Empty)
            {
                streak = 0;
            }
            else if (grid[pos.x + i, pos.y] == SlotState.Red)
            {
                if (name.Contains("Red"))
                    streak++;
                else
                    streak = 0;
            }
            else if (grid[pos.x + i, pos.y] == SlotState.Yellow)
            {
                if (name.Contains("Yellow"))
                    streak++;
                else
                    streak = 0;
            }
        }
        if (streak >= 4)
        {
            Win(name);
            return;
        }

        //Se arrivi qui, significa che il verticale e l'orizzontale non hanno trovato nulla
        //Obliquo a destra
        streak = 0;
        for (int i = -3; i <= 3; i++)
        {
            if (streak >= 4)
                break;

            //Debug.Log("X=" + pos.x + " Y=" + pos.y + " I=" + i);
            if (pos.x + i < 0 || pos.x + i >= w || pos.y + i < 0 || pos.y + i >= h)
            {
                streak = 0;
            }
            else if (grid[pos.x + i, pos.y + i] == SlotState.Empty)
            {
                streak = 0;
            }
            else if (grid[pos.x + i, pos.y + i] == SlotState.Red)
            {
                if (name.Contains("Red"))
                    streak++;
                else
                    streak = 0;
            }
            else if (grid[pos.x + i, pos.y + i] == SlotState.Yellow)
            {
                if (name.Contains("Yellow"))
                    streak++;
                else
                    streak = 0;
            }
        }
        if (streak >= 4)
        {
            Win(name);
            return;
        }

        //Se arrivi qui, significa che il verticale, l'orizzontale e l'obliquo a destra non hanno trovato nulla
        //Obliquo a sinistra
        streak = 0;
        for (int i = -3; i <= 3; i++)
        {
            if (streak >= 4)
                break;

            if (pos.x + i < 0 || pos.x + i >= w || pos.y - i < 0 || pos.y - i >= h)
            {
                streak = 0;
            }
            else if (grid[pos.x + i, pos.y - i] == SlotState.Empty)
            {
                streak = 0;
            }
            else if (grid[pos.x + i, pos.y - i] == SlotState.Red)
            {
                if (name.Contains("Red"))
                    streak++;
                else
                    streak = 0;
            }
            else if (grid[pos.x + i, pos.y - i] == SlotState.Yellow)
            {
                if (name.Contains("Yellow"))
                    streak++;
                else
                    streak = 0;
            }
        }
        if (streak >= 4)
        {
            Win(name);
            return;
        }
    }

    public void Win(string name)
    {
        if(name.Contains("Red"))
        {
            //Accendi luce rossa
            redLight.GetComponent<Renderer>().material.color = Color.red;
            redLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.2f, 0, 0));
        }
        else if(name.Contains("Yellow"))
        {
            //Accendi luce gialla
            yellowLight.GetComponent<Renderer>().material.color = Color.yellow;
            yellowLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.4f, 0.3681275f, 0));
        }
        resetButton.GetComponent<Renderer>().material.color = Color.green;
        gameOver = true;
    }
}
