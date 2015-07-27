using UnityEngine;
using System.Collections;

public interface IItem {

    string ItemName { get; set; }
    string ItemDescription { get; set; }
    Texture2D ItemIcon { get; set; }
    int ItemPower { get; set; }
    int ItemSpeed { get; set; }

}
