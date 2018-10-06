using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : SingletonMonoBehaviour<CursorManager>
{

    Cursor[] cursors;
    public enum Dir
    {
        LEFT,
        RIGHT,
        DOWN,
        UP
    }
    // Use this for initialization
    void Start()
    {
        cursors = new Cursor[2]{
            new Cursor(0,0),
            new Cursor(9,9)
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveCursor(int playerId, Dir dir)
    {
        Cursor currentCursor = new Cursor(cursors[playerId - 1].x, cursors[playerId - 1].y);
        switch (dir)
        {
            case Dir.LEFT:
                if (currentCursor.x <= 0)
                {
                    currentCursor.x = 9;
                }
                else
                {
                    currentCursor.x--;
                }
                break;
            case Dir.RIGHT:
                if (currentCursor.x >= 9)
                {
                    currentCursor.x = 0;
                }
                else
                {
                    currentCursor.x++;
                }
                break;
            case Dir.DOWN:
                if (currentCursor.y <= 0)
                {
                    currentCursor.y = 9;
                }
                else
                {
                    currentCursor.y--;
                }
                break;
            case Dir.UP:
                if (currentCursor.y >= 9)
                {
                    currentCursor.y = 0;
                }
                else
                {
                    currentCursor.y++;
                }
                break;
        }
        if(PanelsManager.Instance.GetPanel(currentCursor.x, currentCursor.y).IsOwnedBy(playerId)){
            cursors[playerId - 1] = currentCursor;
            Panel nextPanel = PanelsManager.Instance.GetPanel(currentCursor.x, currentCursor.y);
            PlayersManager.Instance.GetPlayer(playerId).transform.position = nextPanel.transform.position + new Vector3(0, 0.1f, 0);
        }
       
    }

    public void Select(int playerId)
    {
        Cursor cursor = cursors[playerId - 1];
        PanelsManager.Instance.Select(playerId, cursor.x, cursor.y);
    }
}

public class Cursor
{
    public int x, y;
    public Cursor(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}
