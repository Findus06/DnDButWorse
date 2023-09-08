using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSettingPanel : MonoBehaviour
{
   [SerializeField] TMPro.TMP_InputField nameTextField;
   CharacterPanel characterPanel;

    [SerializeField] TMPro.TMP_Dropdown classDropDown;
    [SerializeField] TMPro.TMP_Dropdown raceDropDown;

   [SerializeField] ListOfRaces listOfRaces;
   [SerializeField] ListOfClasses listOfClasses;

    [SerializeField] List<GameObject> trainSkillButtons;

    void Awake()
    {
        characterPanel = GetComponentInParent<CharacterPanel>();
    }

    void Start()
    {
        UpdateNameField();
        UpdateDropDownLists();
        UpdateCharacterRace(false);
        UpdateSkillButtons();
    }

    private void UpdateDropDownLists()
    {
        classDropDown.ClearOptions();
        List<string> classesOptions = new List<string>();
        for(int i = 0; i < listOfClasses.classes.Count; i++)
        {
            classesOptions.Add(listOfClasses.classes[i].Name);
        }
        classDropDown.AddOptions(classesOptions);
        if (characterPanel.character.classOfCharacter != null)
        {
             classDropDown.value = listOfClasses.classes.IndexOf(characterPanel.character.classOfCharacter);
        }
       

        raceDropDown.ClearOptions();
        List<string> racesOptions = new List<string>();
        for(int i = 0; i < listOfRaces.races.Count; i++)
        {
            racesOptions.Add(listOfRaces.races[i].Name);
        }
        raceDropDown.AddOptions(racesOptions);
        if (characterPanel.character.race != null)
        {
            raceDropDown.value = listOfRaces.races.IndexOf(characterPanel.character.race);
        }
        
    }

    public void UpdateCharacterClass()
    {   
        if(characterPanel.character.classOfCharacter != null)
        {
            List<CharacterSkill> originalClassSkills = characterPanel.character.classOfCharacter.availableSkillsToTrain;
            characterPanel.character.SetTrainableSkills(originalClassSkills, false);
        }
        

        characterPanel.character.classOfCharacter = listOfClasses.classes[classDropDown.value];
        List<CharacterSkill> skillAvailableToTrainOnSelectedClass = listOfClasses.classes[classDropDown.value].availableSkillsToTrain;
        characterPanel.character.SetTrainableSkills(skillAvailableToTrainOnSelectedClass);

        UpdateSkillButtons();
    }

    private void UpdateSkillButtons()
    {
       for(int i = 0; i < characterPanel.character.skills.Count; i++)
       {
            trainSkillButtons[i].SetActive(characterPanel.character.skills[i].canBeTrained);
       }
    }

    public void UpdateCharacterRace(bool first = false)
    {
        if(characterPanel.character.race != null)
        {
            if(first == false)
            {
                characterPanel.character.AddBonusToAbility(characterPanel.character.race.abilityBonuses, true);
            }
         }
            

        characterPanel.character.race = listOfRaces.races[raceDropDown.value];

        characterPanel.character.AddBonusToAbility(characterPanel.character.race.abilityBonuses);
    }

    private void UpdateNameField()
    {
        nameTextField.text = characterPanel.character.Name;
    }

    public void UpdateCharacterName()
    {
        characterPanel.character.Name = nameTextField.text;
    }

   
}
