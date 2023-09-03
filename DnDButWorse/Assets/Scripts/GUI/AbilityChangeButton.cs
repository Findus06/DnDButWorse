using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityChangeButton : MonoBehaviour, IPointerClickHandler
{
    AbilityPanel abilityPanel;

    [SerializeField]CharacterAbility ability;
    [SerializeField] bool negative;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (negative == false)
        {
            abilityPanel.AddAbilityScore(ability);
        }else
        {
            abilityPanel.RemoveAbilityScore(ability);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        abilityPanel = GetComponentInParent<AbilityPanel>();
    }

   
}
