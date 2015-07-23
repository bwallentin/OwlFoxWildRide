using UnityEngine;
using UnityEngine.UI;

public abstract class Actor : MonoBehaviour {

    // The actors name
    public string actorName;
    
    // Default starting health
    public int startingHealth = 100;

    // Our actors level
    public float speed;

    // Default offset on the Y axis for the health panel
    public float healthPanelOffset = 0.35f;

    // Our current health
    public int currentHealth;

    // The canvas we're drawing our health bar on
    public Canvas canvas;

    // The GUI Health bar
    public GameObject healthPrefab;

    // The health panel containing all GUI elements
    protected GameObject healthPanel;

    // The actual health bar
    protected Slider healthSlider;

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

        // we'll hide this for now, and show it in combat
        SetHealthPanelActive(false);
    }

    protected void UpdateHealthBarPosition()
    {
        Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + healthPanelOffset, transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        healthPanel.transform.position = screenPos;
    }

    public void SetHealthPanelActive(bool isActive)
    {
        healthPanel.SetActive(isActive);
    }
}
