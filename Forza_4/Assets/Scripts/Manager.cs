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

    public float[] columnsX;
    private int w = 7;
    private int h = 6;
    private SlotState[,] grid;

    private int turn;
    [HideInInspector]
    public bool busy;
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
            GenerateCoin(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            GenerateCoin(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            GenerateCoin(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            GenerateCoin(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            GenerateCoin(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            GenerateCoin(5);
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            GenerateCoin(6);
    }

    public void GenerateCoin(int index)
    {
        //Se la moneta non ha ancora finito di cadere, esci
        if (busy)
            return;

        //Se la colonna è piena, esci
        if (grid[index, h - 1] != SlotState.Empty)
            return;

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
        Instantiate(g, new Vector3(columnsX[index], 4.5f, 0), Quaternion.identity);
        
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
        Debug.Log(pos);
        int streak = 0;
        //Verticale
        //for (int x = 0; x < w; x++)
        //{
        //    for (int y = 0; y < h - 3; y++)
        //    {
        //        switch (grid[x, y])
        //        {
        //            case SlotState.Empty:
        //                streak = 0;
        //                break;
        //            case SlotState.Red:
        //                streak = name.Contains("Red") ? streak + 1 : 0;
        //                break;
        //            case SlotState.Yellow:
        //                streak = name.Contains("Yellow") ? streak + 1 : 0;
        //                break;
        //        }
        //        if (streak >= 4)
        //        {
        //            Win(name);
        //            gameOver = true;
        //            return;
        //        }
        //    }
        //}

        //Se arrivi qui, significa che il verticale non ha trovato nulla
        //Orizzontale
        //streak = 1;
        //for (int i = 1; i <= 3; i++)
        //{
        //    if (streak >= 4)
        //        break;
        //    try
        //    {
        //        if (grid[pos.x - i, pos.y] == SlotState.Empty)
        //        {
        //            break;
        //        }
        //        else if (grid[pos.x - i, pos.y] == SlotState.Red)
        //        {
        //            if (name.Contains("Red"))
        //                streak++;
        //            else
        //                break;
        //        }
        //        else
        //        {
        //            if (name.Contains("Yellow"))
        //                streak++;
        //            else
        //                break;
        //        }
        //    }
        //    catch (System.IndexOutOfRangeException)
        //    {
        //        break;
        //    }
        //}
        //for (int i = 1; i <= 3; i++)
        //{
        //    if (streak >= 4)
        //        break;
        //    try
        //    {
        //        if (grid[pos.x + i, pos.y] == SlotState.Empty)
        //        {
        //            break;
        //        }
        //        else if (grid[pos.x + i, pos.y] == SlotState.Red)
        //        {
        //            if (name.Contains("Red"))
        //                streak++;
        //            else
        //                break;
        //        }
        //        else
        //        {
        //            if (name.Contains("Yellow"))
        //                streak++;
        //            else
        //                break;
        //        }
        //    }
        //    catch (System.IndexOutOfRangeException)
        //    {
        //        break;
        //    }
        //}
        //if (streak >= 4)
        //{
        //    Win(name);
        //    return;
        //}

        //Nuovo metodo più compatto
        streak = 0;
        for (int i = -3; i <= 3; i++)
        {
            if (streak >= 4)
                break;
            try
            {
                if (grid[pos.x + i, pos.y] == SlotState.Empty)
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
                else
                {
                    if (name.Contains("Yellow"))
                        streak++;
                    else
                        streak = 0;
                }
            }
            catch (System.IndexOutOfRangeException)
            {
                streak = 0;
            }
        }
        if (streak >= 4)
        {
            Win(name);
            return;
        }
        //Così non funziona, al posto di continue/break mettere streak = 0-----FATTO

        //Se arrivi qui, significa che il verticale e l'orizzontale non hanno trovato nulla
        //Obliquo a destra
        //streak = 0;
        //for (int x = 0; x < w - 3; x++)
        //{
        //    for (int y = 0; y < h - 3; y++)
        //    {
        //        for (int i = 0; i < 4; i++)
        //        {
        //            switch (grid[x + i, y + i])
        //            {
        //                case SlotState.Empty:
        //                    streak = 0;
        //                    break;
        //                case SlotState.Red:
        //                    streak = name.Contains("Red") ? streak + 1 : 0;
        //                    break;
        //                case SlotState.Yellow:
        //                    streak = name.Contains("Yellow") ? streak + 1 : 0;
        //                    break;
        //            }
        //        }
        //        if (streak >= 4)
        //        {
        //            Win(name);
        //            gameOver = true;
        //            return;
        //        }
        //    }
        //}

        ////Se arrivi qui, significa che il verticale, l'orizzontale e l'obliquo a destra non hanno trovato nulla
        ////Obliquo a sinistra
        //streak = 0;
        //for (int x = 3; x < w; x++)
        //{
        //    for (int y = 0; y < h - 3; y++)
        //    {
        //        for (int i = 0; i < 4; i++)
        //        {
        //            switch (grid[x - i, y + i])
        //            {
        //                case SlotState.Empty:
        //                    streak = 0;
        //                    break;
        //                case SlotState.Red:
        //                    streak = name.Contains("Red") ? streak + 1 : 0;
        //                    break;
        //                case SlotState.Yellow:
        //                    streak = name.Contains("Yellow") ? streak + 1 : 0;
        //                    break;
        //            }
        //        }
        //        if (streak >= 4)
        //        {
        //            Win(name);
        //            gameOver = true;
        //            return;
        //        }
        //    }
        //}
    }

    private void Win(string name)
    {
        Debug.Log(name + " wins");
    }
}
