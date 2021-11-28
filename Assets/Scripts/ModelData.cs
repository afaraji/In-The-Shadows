using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Puzzle", menuName = "Puzzle")]
public class ModelData : ScriptableObject
{
    public new string name;

    public Vector3 startingPos;
    public Vector3 startingRot;

    public Vector3 solvedPos;
    public Vector3 solvedRot;

    public string hint;

    public GameObject model;


}
