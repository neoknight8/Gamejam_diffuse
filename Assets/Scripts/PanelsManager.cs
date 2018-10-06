using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsManager : SingletonMonoBehaviour<PanelsManager>
{

    Panel[,] panels;
    int computerAttackCount = 0;
    Dictionary<int, ArrayList> computerAttackArea;
    MaterialHolder.MaterialType materialType;
    // Use this for initialization
    void Start()
    {
        InitPanels();
        SetComputerAttackArea();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitPanels()
    {
        panels = new Panel[10, 10];
        Panel[] _panels = GetComponentsInChildren<Panel>();
        for (int i = 0; i < _panels.Length; i++)
        {
            panels[i / 10, i % 10] = _panels[i];
        }
    }

    public Panel GetPanel(int x, int y)
    {
        return panels[x, y];
    }

    public void Select(int playerId, int x, int y)
    {
        ArrayList _panels = new ArrayList();
        if (x > 0)
        {
            _panels.Add(GetPanel(x - 1, y));
        }
        if (x < 9)
        {
            _panels.Add(GetPanel(x + 1, y));
        }
        if (y > 0)
        {
            _panels.Add(GetPanel(x, y - 1));
        }
        if (y < 9)
        {
            _panels.Add(GetPanel(x, y + 1));
        }

        if (playerId == 1)
        {
            materialType = MaterialHolder.MaterialType.RED;
        }
        else if (playerId == 2)
        {
            materialType = MaterialHolder.MaterialType.BLUE;
        }
        foreach (Panel panel in _panels)
        {
            panel.SetOwner(materialType);
        }
    }

    public void ComputerAttack()
    {
        ArrayList attackArea = computerAttackArea[computerAttackCount];
        foreach(Vector2 vector in attackArea){
            PanelsManager.Instance.GetPanel((int)vector.x, (int)vector.y).SetOwner(MaterialHolder.MaterialType.YELLOW);
        }
        computerAttackCount = computerAttackCount == 4 ? 0 : computerAttackCount + 1;
    }

    public void Diffuse()
    {
        int[,] weights = new int[10, 10];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Panel panel = GetPanel(i, j);
                int power = 0;
                if (panel.IsOwnedBy(1))
                {
                    power = 1;
                }
                else if (panel.IsOwnedBy(2))
                {
                    power = -1;
                }
                if (i > 0)
                {
                    weights[i - 1, j] += power;
                }
                if (i < 9)
                {
                    weights[i + 1, j] += power;
                }
                if (j > 0)
                {
                    weights[i, j - 1] += power;
                }
                if (j < 9)
                {
                    weights[i, j + 1] += power;
                }


                if (i > 0 && j > 0)
                {
                    weights[i - 1, j - 1] += power;
                }
                if (i < 9 && j > 0)
                {
                    weights[i + 1, j - 1] += power;
                }
                if (i > 0 && j < 9)
                {
                    weights[i - 1, j + 1] += power;
                }
                if (i < 9 && j < 9)
                {
                    weights[i + 1, j + 1] += power;
                }
            }
        }

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Panel panel = GetPanel(i, j);
                if (weights[i, j] > 0)
                {
                    panel.SetOwner(MaterialHolder.MaterialType.RED);
                }
                if (weights[i, j] < 0)
                {
                    panel.SetOwner(MaterialHolder.MaterialType.BLUE);
                }
            }
        }

        GameManager.Instance.ToNextPhase();
    }

    // あとできれいにする
    void SetComputerAttackArea()
    {
        computerAttackArea = new Dictionary<int, ArrayList>();

        ArrayList array = new ArrayList();
        array.Add(new Vector2(4, 4));
        array.Add(new Vector2(4, 5));
        array.Add(new Vector2(5, 4));
        array.Add(new Vector2(5, 5));

        computerAttackArea.Add(0, array);

        array = new ArrayList();
        for (int i = 3; i <= 6; i++)
        {
            array.Add(new Vector2(3, i));
            array.Add(new Vector2(6, i));
        }
        array.Add(new Vector2(4, 3));
        array.Add(new Vector2(5, 3));
        array.Add(new Vector2(4, 6));
        array.Add(new Vector2(5, 6));

        computerAttackArea.Add(1, array);

        array = new ArrayList();
        for (int i = 2; i <= 7;i++){
            array.Add(new Vector2(2, i));
            array.Add(new Vector2(7, i));
        }

        for (int i = 3; i <= 6; i++)
        {
            array.Add(new Vector2(i, 2));
            array.Add(new Vector2(i, 7));
        }

        computerAttackArea.Add(2, array);

        array = new ArrayList();
        for (int i = 1; i <= 8; i++)
        {
            array.Add(new Vector2(1, i));
            array.Add(new Vector2(8, i));
        }

        for (int i = 2; i <= 7; i++)
        {
            array.Add(new Vector2(i, 1));
            array.Add(new Vector2(i, 8));
        }

        computerAttackArea.Add(3, array);

        array = new ArrayList();
        for (int i = 0; i <= 9; i++)
        {
            array.Add(new Vector2(0, i));
            array.Add(new Vector2(9, i));
        }

        for (int i = 1; i <= 8; i++)
        {
            array.Add(new Vector2(i, 0));
            array.Add(new Vector2(i, 9));
        }

        computerAttackArea.Add(4, array);



    }
}
