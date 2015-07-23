using UnityEngine;
using System.Collections;

public class EnemyDetectCombat : MonoBehaviour, IDetectCombat
{
    // Determines if we're detecting enemies or not
    public bool DetectEnemies { get; set; }

    private GameController gameController;

    void Awake()
    {
        DetectEnemies = true;

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if (gameControllerObject != null)
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
        if (other.gameObject.tag == "Player" && !gameController.inCombat && DetectEnemies)
        {
            gameController.AddCombatGroup(gameObject, CombatGroup.Enemy);
        }
    }
}
