using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class Player : MonoBehaviour {
    [SerializeField]
    private int id;
    private GamePad.Index gamepadIndex;
	// Use this for initialization
	void Start () {
        if(id == 1){
            gamepadIndex = GamePad.Index.One;
        }else if(id == 2){
            gamepadIndex = GamePad.Index.Two;
        }
        StartCoroutine(Move());
	}
	
	// Update is called once per frame
	void Update () {
        if(GameManager.Instance.IsMyTurn(id)){
            Select();
        }
	}

    public int GetId(){
        return id;
    }

    IEnumerator Move(){
        while(true){
            if (GameManager.Instance.IsMyTurn(id))
            {
                Vector2 axis = GamePad.GetAxis(GamePad.Axis.LeftStick, gamepadIndex);
                if (axis.x > 0.8)
                {
                    CursorManager.Instance.MoveCursor(id, CursorManager.Dir.RIGHT);
                }
                else if (axis.x < -0.8)
                {
                    CursorManager.Instance.MoveCursor(id, CursorManager.Dir.LEFT);
                }
                else if (axis.y > 0.8)
                {
                    CursorManager.Instance.MoveCursor(id, CursorManager.Dir.UP);
                }
                else if (axis.y < -0.8)
                {
                    CursorManager.Instance.MoveCursor(id, CursorManager.Dir.DOWN);
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    void Select(){
        if(GamePad.GetButtonDown(GamePad.Button.B, gamepadIndex)){
            CursorManager.Instance.Select(id);
            GameManager.Instance.ToNextPhase();
        }
    }
}
