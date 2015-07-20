using UnityEngine;
using UnityEngine.UI;

public abstract class Actor : MonoBehaviour {

    // The actors name
    public string actorName;
    
    // Default starting health
    public int startingHealth = 100;

    // Our actors level
    public int level;

    // Default offset on the Y axis for the health panel
    public float healthPanelOffset = 0.35f;

    // Our current health
    public int currentHealth;

    // Determines if we're an NPC
    public bool isNPC;

    // The canvas we're drawing our health bar on
    public Canvas canvas;

    // The GUI Health bar
    public GameObject healthPrefab;

    // The health panel containing all GUI elements
    protected GameObject healthPanel;

    // The actual health bar
    protected Slider healthSlider;

    // Determines if we're dead or not
    protected bool isDead;

    // If we're damaged, we'll animate accordingly
    protected bool damaged;

    // We'll force every actor to implement the die function
    protected abstract void Die();

    public virtual void Start()
    {
        UpdateHealthBarPosition();
    }

    public virtual void Awake()
    {
        currentHealth = startingHealth;

        healthPanel = Instantiate(healthPrefab) as GameObject;
        healthPanel.transform.SetParent(canvas.transform, false);
        healthSlider = healthPanel.GetComponentInChildren<Slider>();

        Text healthPanelName = healthPanel.GetComponentInChildren<Text>();
        healthPanelName.text = actorName;
    }

    protected void UpdateHealthBarPosition()
    {
        Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + healthPanelOffset, transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        healthPanel.transform.position = screenPos;
    }
}
