using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatisticsPanel : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI hpText;
    [SerializeField] TMPro.TextMeshProUGUI acText;

    public void UpdatePanel(Character character)
    {
        Statistic hpStat = character.GetStats(CharacterStatistic.HP);
        hpText.text = hpStat.GetStatisticValue(character).ToString();

        Statistic acStat = character.GetStats(CharacterStatistic.AC);
        acText.text = acStat.GetStatisticValue(character).ToString();
    }
}
