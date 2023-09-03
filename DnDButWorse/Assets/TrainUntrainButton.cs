using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrainUntrainButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] CharacterSkill skill;

    SkillPanel skillPanel;

    private void Start() 
    {
        skillPanel = GetComponentInParent<SkillPanel>();    
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        skillPanel.TrainSkill(skill);
    }
}
