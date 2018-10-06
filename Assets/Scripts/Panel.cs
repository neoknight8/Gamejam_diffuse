using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{

    int owner;
    // Use this for initialization
    void Start()
    {
        owner = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetOwner(MaterialHolder.MaterialType type)
    {
        GetComponent<Renderer>().material = MaterialHolder.Instance.GetMaterial(type);
        owner = (int)type + 1;
    }

    public bool IsOwnedBy(int playerId)
    {
        return owner == playerId;
    }

}
