using UnityEngine;
using System.Collections;

public interface IItem {

    int ItemID { get; set; }
    string ItemName { get; set; }
    string ItemDescription { get; set; }
    Texture2D ItemIcon { get; set; }

}
