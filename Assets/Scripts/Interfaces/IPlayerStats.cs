using UnityEngine;
using System.Collections;

public interface IPlayerStats {

    int Armor { get; set; }
    int Damage { get; set; }
    int Crit { get; set; }
    int Dodge { get; set; }
    int Hit { get; set; }
    double Mana { get; set; }
    int Resist { get; set; }
    int Speed { get; set; }
    int XP { get; set; }

    double CalculateBaseMana();

    void RecalculateMana(int amountOfManaUsed);
}
