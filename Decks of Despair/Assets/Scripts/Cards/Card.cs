using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class Card : ScriptableObject
{
    public Sprite cardImage;
    public float speedBonus;
    public float fireRateBonus;
    public float rangeBonus;
    public float shotSpeedBonus;
    public int healthBonus;

}
                              