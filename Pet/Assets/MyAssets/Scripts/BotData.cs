using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BotData", menuName = "BotData")]
public class BotData : ScriptableObject
{
    public int Hp = 9;
    public int Damage = 8;
    public int Gold = 2;
}
