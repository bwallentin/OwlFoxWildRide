using UnityEngine;
using System.Collections;

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

    public PlayerStats()
    {
        Armor = 0;
        Damage = 10;
        Crit = 0;
        Dodge = 0;
        Hit = 100;
        Mana = CalculateBaseMana();
        Resist = 0;
        Speed = 0;
        XP = 0;
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
