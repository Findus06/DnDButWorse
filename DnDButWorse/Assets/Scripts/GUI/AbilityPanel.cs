using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPanel : MonoBehaviour
{
    [SerializeField] List<AbilityUIElement> abilityUI;

    CharacterPanel characterPanel;


    
    private void Start()
    {
        characterPanel = GetComponentInParent<CharacterPanel>();
    }

    // Updates the Ability score panel to display the new values
    public void UpdatePanel(List<Ability> abilities)
    {
        for (int i = 0; i < abilityUI.Count; i++)
        {
            abilityUI[i].Set(abilities[i]);
        }
    }

    // the two methods handling adding points and removing points

    public void AddAbilityScore(CharacterAbility ability)
    {
        characterPanel.character.ChangeAbilityScore(1, ability);
    }

    public void RemoveAbilityScore(CharacterAbility ability)
    {
        characterPanel.character.ChangeAbilityScore(-1, ability);
    }

    
    
}
