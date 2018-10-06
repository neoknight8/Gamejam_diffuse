using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    Phase currentPhase;
    int turn;
    [SerializeField]
    TextMeshProUGUI turnsText, winnerText;

    public enum Phase
    {
        Ph1,
        Ph2,
        Ph3
    }

    // Use this for initialization
    void Start()
    {
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitGame()
    {
        PanelsManager.Instance.GetPanel(0, 0).SetOwner(MaterialHolder.MaterialType.RED);
        PanelsManager.Instance.GetPanel(9, 9).SetOwner(MaterialHolder.MaterialType.BLUE);

        currentPhase = Phase.Ph1;

        turn = 1;
        SetTurnsText(Color.red);
    }

    public void ToNextPhase()
    {
        switch (currentPhase)
        {
            case Phase.Ph1:
                currentPhase = Phase.Ph2;
                SetTurnsText(Color.blue);
                break;
            case Phase.Ph2:
                currentPhase = Phase.Ph3;
                StartCoroutine(Diffuse());
                SetTurnsText(Color.white);
                break;
            case Phase.Ph3:
                currentPhase = Phase.Ph1;
                if (turn == 10)
                {
                    GameEnd();
                }
                else
                {
                    turn++;
                    SetTurnsText(Color.red);
                }
                break;
        }
    }

    public bool IsMyTurn(int playerId)
    {
        if ((playerId == 1 && currentPhase == Phase.Ph1) || (playerId == 2 && currentPhase == Phase.Ph2))
        {
            return true;
        }
        return false;
    }

    IEnumerator Diffuse()
    {
        yield return new WaitForSeconds(1);
        PanelsManager.Instance.Diffuse();
        yield return new WaitForSeconds(1);
        PanelsManager.Instance.ComputerAttack();
    }

    public void SetTurnsText(Color color)
    {
        turnsText.text = "Turn " + turn;
        turnsText.color = color;
    }

    void GameEnd()
    {
        int player1 = 0;
        int player2 = 0;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Panel panel = PanelsManager.Instance.GetPanel(i, j);
                if(panel.IsOwnedBy(1)){
                    player1++;
                }
                if (panel.IsOwnedBy(2))
                {
                    player2++;
                }
            }
        }
        if(player1 >= player2){
            winnerText.text = "player 1 wins!";
            winnerText.color = Color.red;
        }else{
            winnerText.text = "player 2 wins!";
            winnerText.color = Color.blue;
        }
    }
}
