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
    void Awake()
    {
        characterPanel = GetComponentInParent<CharacterPanel>();
    }

    void Start()
    {
        UpdateNameField();
        UpdateDropDownLists();
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

        raceDropDown.ClearOptions();
        List<string> racesOptions = new List<string>();
        for(int i = 0; i < listOfRaces.races.Count; i++)
        {
            racesOptions.Add(listOfRaces.races[i].Name);
        }
        raceDropDown.AddOptions(racesOptions);
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
