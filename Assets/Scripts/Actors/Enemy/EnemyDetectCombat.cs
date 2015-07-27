using UnityEngine;
using System.Collections;

public class EnemyDetectCombat : MonoBehaviour, IDetectCombat
{
    // Determines if we're detecting enemies or not
    public bool DetectEnemies { get; set; }

    private SphereCollider sphereCollider;

    private GameController gameController;

    void Awake()
    {
        DetectEnemies = true;
        sphereCollider = GetComponent<SphereCollider>();

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !gameController.inCombat && DetectEnemies)
        {
            gameController.AddCombatGroup(gameObject, CombatGroup.Enemy);

            // disable our triggers
            sphereCollider.enabled = false;
            sphereCollider.isTrigger = false;

        }
    }
}
