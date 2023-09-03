using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    public Character character;

    [SerializeField] AbilityPanel abilityPanel;
    [SerializeField] SkillPanel skillPanel;



    private void Update()
    {
        abilityPanel.UpdatePanel(character.abilities);
        skillPanel.UpdatePanel(character);
    }
}
