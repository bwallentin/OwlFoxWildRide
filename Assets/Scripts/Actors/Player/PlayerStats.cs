using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour, IPlayerStats {

    public int Strength { get; set; }
    public int Mana { get; set; }
    
    public PlayerStats()
    {
        Strength = 0;
        Mana = 100;
    }

    public void RecalculateMana(int amountManaUsed)
    {
        Mana -= amountManaUsed;
    }
}
