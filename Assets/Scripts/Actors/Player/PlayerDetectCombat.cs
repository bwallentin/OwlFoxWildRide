using UnityEngine;
using System.Collections;

public class PlayerDetectCombat : MonoBehaviour, IDetectCombat {

    // Determines if we're detecting enemies or not
    public bool DetectEnemies { get; set; }

    private GameController gameController;

    void Awake()
    {
        DetectEnemies = true;

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Can't find Game Controller");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !gameController.inCombat)
        {
            gameController.AddCombatGroup(gameObject, CombatGroup.PartyMember);
        }
    }
}
