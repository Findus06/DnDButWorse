using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUIElement : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI skillValue;
    [SerializeField] TMPro.TextMeshProUGUI trainedText;

    public void Set(Skill skill, Character character)
    {
        skillValue.text = skill.GetSkillValue(character).ToString();
        trainedText.text = skill.trained ? "Trained" : "UnTrained";
    }
}
