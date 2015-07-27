using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour, IPlayerStats {

    public int Armor { get; set; }
    public int Damage { get; set; }
    public int Crit { get; set; }
    public int Dodge { get; set; }
    public int Hit { get; set; }
    public double Mana { get; set; }
    public int Resist { get; set; }
    public int Speed { get; set; }
    public int XP { get; set; }

    private double baseMana;

    void Start()
    {
        Armor = 10;
        Damage = 10;
        Crit = 0;
        Dodge = 10;
        Hit = 0;
        Mana = CalculateBaseMana();
        Resist = 10;
        Speed = 0;
        XP = 0;
    }

    public void UpdatePlayerStatsByItem(List<Item> items)
    {
        foreach (var item in items)
        {
            if (item.ItemName != null)
            {
                Armor = item.ItemStats.Armor;
                Damage = item.ItemStats.BaseDamage;
                Dodge = item.ItemStats.Dodge;
                Hit = item.ItemStats.HitRating;
                Resist = item.ItemStats.Resist;
            }
        }
    }

    public double CalculateBaseMana()
    {
        //baseMana = Mana + (Intellect * 0.5);
        //return baseMana;

        return 100;
    }

    public void RecalculateMana(int amountOfManaUsed)
    {
        Mana -= amountOfManaUsed;
    }
}
