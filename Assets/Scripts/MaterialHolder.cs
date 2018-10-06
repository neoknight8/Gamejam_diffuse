using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialHolder : SingletonMonoBehaviour<MaterialHolder> {

    [SerializeField]
    Material[] materials;

    public enum MaterialType{
        RED = 0,
        BLUE = 1,
        YELLOW = 2
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Material GetMaterial(MaterialType type){
        return materials[(int)type];
    }
}
