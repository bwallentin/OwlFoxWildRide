using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class PlayerDecision : IDecision {

    // Determines if the decision has been made yet
    public bool IsReady { get; private set; }

    // The actual that will be performed
    public IAction Action { get; private set; }

    // The actor responsible for the decision
    private GameObject actor;

    // Determines if we're selecting a target 
    private bool selectingTarget = false;

    // The ability the player picks
    private Ability ability;

    // The HUD ability panel
    private GameObject abilityPanel;

    // The layer our actors resides on
    private int actorMask;

    public PlayerDecision(GameObject _actor, List<GameObject> targets)
    {
        actor = _actor;
        abilityPanel = GameObject.FindGameObjectWithTag("HUDAbilityPanel");

        // Draw the current actors action bar
        DrawActorAbilities();

        // Get the actors layer mask, so we can raycast it
        actorMask = LayerMask.GetMask("Actors");
    }

    public void Update()
    {
        if (selectingTarget)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                // TODO: Implement friendly target spells as well
                // Cast a ray from camera to any object on our Actors layer
                // and if we hit an enemy, attack it!
                if (Physics.Raycast(ray, out hit, 100, actorMask) && hit.transform.tag == "Enemy")
                {
                    Action = new Action(actor, hit.transform.gameObject, ability);
                    IsReady = true;
                }
            }
        }

        if (IsReady)
        {
            // Make sure we reset and empty the action panel for this actor
            ResetActorAbilities();
        }
    }

    private void DrawActorAbilities()
    {
        AbilityList abilityList = actor.GetComponent<AbilityList>();

        if (abilityList.abilities.Count > 0)
        {
            foreach (var ability in abilityList.abilities)
            {
                // Instantiate our button prefab
                GameObject buttonPrefab = GameObject.Instantiate(abilityList.templateButton);
                buttonPrefab.transform.SetParent(abilityPanel.transform);
  
                // We use this script to hold default information about a action button
                // we can then overwrite these values with the current ability we're iterating
                DefaultActionButton template = buttonPrefab.GetComponent<DefaultActionButton>();
                template.name = ability.name;
                template.icon.sprite = ability.sprite;
                template.description = ability.description;

                // Get a reference to the actual button we need to attach the listener to
                Button button = buttonPrefab.GetComponent<Button>();
                AddListener(button, ability);
            }
        }
    }

    private void ResetActorAbilities()
    {
        // Loop over the buttons in our action panel and destroy them
        foreach (Transform ability in abilityPanel.transform)
        {
            Object.Destroy(ability.gameObject);
        }
    }

    private void AddListener(Button b, Ability ability)
    {
        b.onClick.AddListener(() => SelectAbility(ability));
    }

    private void SelectAbility(Ability currentAbility)
    {
        selectingTarget = true;
        ability = currentAbility;
    }
}
