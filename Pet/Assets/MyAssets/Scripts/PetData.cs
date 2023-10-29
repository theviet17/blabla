using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PetData", menuName = "PetData")]
public class PetData : ScriptableObject
{
    public int level = 1;
    public int hp = 80;
    public int damage = 3;
    public GameObject marbles;
}
