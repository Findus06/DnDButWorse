using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSettingPanel : MonoBehaviour
{
   [SerializeField] TMPro.TMP_InputField nameTextField;
   CharacterPanel characterPanel;
    void Awake()
    {
        characterPanel = GetComponentInParent<CharacterPanel>();
    }

    void Start()
    {
        UpdateNameField();
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
