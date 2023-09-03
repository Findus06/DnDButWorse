using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : MonoBehaviour
{

    [SerializeField] List<SkillUIElement> skillUIElements;

    CharacterPanel characterPanel;

    void Awake()
    {
        characterPanel = GetComponentInParent<CharacterPanel>();
    }

    public void UpdatePanel(Character character)
    {
        for (int i = 0; i < skillUIElements.Count; i++)
        {
            CharacterSkill cs = (CharacterSkill)i;
            skillUIElements[i].Set(character.GetSkill(cs), character);
        }
    }

    internal void TrainSkill(CharacterSkill skill)
    {
       characterPanel.character.TrainSkill(skill);
    }


}
