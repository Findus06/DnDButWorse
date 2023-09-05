using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    public Character character;

    [SerializeField] AbilityPanel abilityPanel;
    [SerializeField] SkillPanel skillPanel;
    [SerializeField] CharacterStatisticsPanel statisticsPanel;


    // Updates panels inside character panel
    private void Update()
    {
        abilityPanel.UpdatePanel(character.abilities);
        skillPanel.UpdatePanel(character);
        statisticsPanel.UpdatePanel(character);
    }
}
