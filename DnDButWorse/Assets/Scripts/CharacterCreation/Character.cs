using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// stat list where to add ability points
public enum CharacterAbility
{
    Strength,
    Dexterity,
    Constitution,
    Intelligence,
    Wisdom,
    Charisma,
    None
}

public enum CharacterStatistic
{
    HP,
    AC,
    AbilityPoints
}

[Serializable]

// how the whole ability system works
public class Ability
{
    public int abilityScore;
    public CharacterAbility ability;
    public int bonus;

    public int AbilityScore
    {
        get
        {
            return abilityScore + bonus;
        }
    }

    public int Mod
    {
        get
        {
            int mod = abilityScore - 10;
            mod += bonus;
            mod += mod < 0 ? -1 : 0;
            return mod / 2;
        }
    }

    public Ability(CharacterAbility ability, int abilityScore)
    {
        this.ability = ability;
        this.abilityScore = abilityScore;
    }
}

// List of possible skills
public enum CharacterSkill 
{
    Athletics,
    Acrobatics,
    SleightOfHand,
    Stealth,
    Arcana,
    History,
    Investigation,
    Nature,
    Religion,
    AnimalHandling,
    Insight,
    Medicine,
    Perception,
    Survival,
    Deception,
    Intimidation,
    Performance,
    Persuasion
}

[Serializable]
public class Skill
{
public CharacterSkill skill;
public CharacterAbility abilityMod;
public bool trained;
public bool canBeTrained = false;
public int bonus;

public Skill(CharacterSkill skill, CharacterAbility ability, bool trained = false,bool canBeTrained = false, int bonus = 0)
{
  this.skill = skill;
  this.abilityMod = ability;
  this.trained = trained;
  this.bonus = bonus;
  this.canBeTrained = canBeTrained;
}

// get the ability mod value for the skill
public int GetSkillValue(Character character)
{
    int v = character.GetAbilityMod(abilityMod);
    v += bonus;
    v += trained ? 4 : 0;
    return v;
}

}

[Serializable]
public class Statistic
{
    public CharacterStatistic statistic;
    public CharacterAbility abilityMod;
    public int bonus;

    public int currentValue;

    public Statistic(CharacterStatistic statistic, CharacterAbility abilityMod,int bonus = 0)
    {
        this.statistic = statistic;
        this.abilityMod = abilityMod;
        this.bonus = bonus;
    }

    public int GetStatisticValue(Character character)
    {
        int v = character.GetAbilityMod(this.abilityMod);
        v += bonus;
        v += currentValue;

        return v;
    }
}

[CreateAssetMenu]

public class Character : ScriptableObject
{
    public string Name;

    public Race race;
    public Class classOfCharacter;

    public List<Ability> abilities;
    public List<Skill> skills;
    public List<Statistic> statistics;

    [SerializeField] SkillList SkillBaseStructure;

    const int DefaultAbilityValue = 8;
    const int MinAbilityValue = 8;
    const int MaxAbilityValue = 15;

    // changes ability score by 1
    public void ChangeAbilityScore(int by, CharacterAbility ability)
    {
        Ability a = abilities[(int)ability];

        if(a.abilityScore + by > MaxAbilityValue || a.abilityScore + by < MinAbilityValue)
        {
            return;
        }

        Statistic abilityPoints = statistics[(int)CharacterStatistic.AbilityPoints];
        if (by > 0)
        {
            #region increase ability score
            if (a.abilityScore+1 > 13)
            {
                if (abilityPoints.currentValue >= 2)
                {
                    abilityPoints.bonus -=2;
                }
            }
            else
            {
                if (abilityPoints.currentValue >= 1)
                {
                    abilityPoints.bonus -= 1;
                }
                else
                {
                    return;
                }
            }
            #endregion
        }
        else
        {
            #region decrease ability score
            if(a.abilityScore > 13)
            {
                abilityPoints.bonus += 2;
            }
            else
            {
                abilityPoints.bonus += 1;
            }
            #endregion
        }
        a.abilityScore += by;
        
    }

    [ContextMenu("Generate Character")]

    // generates the initial stats for the character and the skill list
    public void GenerateCharacterBase()
    {
        abilities = new List<Ability>();
        abilities.Add(new Ability(CharacterAbility.Strength, DefaultAbilityValue));
        abilities.Add(new Ability(CharacterAbility.Dexterity, DefaultAbilityValue));
        abilities.Add(new Ability(CharacterAbility.Constitution, DefaultAbilityValue));
        abilities.Add(new Ability(CharacterAbility.Intelligence, DefaultAbilityValue));
        abilities.Add(new Ability(CharacterAbility.Wisdom, DefaultAbilityValue));
        abilities.Add(new Ability(CharacterAbility.Charisma, DefaultAbilityValue));

        skills = new List<Skill>();
        for(int i = 0; i < SkillBaseStructure.skills.Count; i++)
        {
            Skill s = SkillBaseStructure.skills[i];
            skills.Add(new Skill(s.skill, s.abilityMod));
        }

        statistics = new List<Statistic>();
        statistics.Add(new Statistic(CharacterStatistic.HP, CharacterAbility.Constitution));
        statistics.Add(new Statistic(CharacterStatistic.AC, CharacterAbility.Dexterity));
        statistics.Add(new Statistic(CharacterStatistic.AbilityPoints, CharacterAbility.None, 20));
    } 

    // gets the ability mod
    public int GetAbilityMod(CharacterAbility abilityMod)
    {
        if(abilityMod == CharacterAbility.None)
        {
            return 0;
        }

        return abilities[(int)abilityMod].Mod;
    }


    // gets the skill level
    internal Skill GetSkill(CharacterSkill skill)
    {
        return skills[(int)skill];
    }

    public Statistic GetStats(CharacterStatistic stat)
    {
        return statistics[(int)stat];
    }


    // trains the skill to the character
    internal void TrainSkill(CharacterSkill skill)
    {
      Skill skillToTrain = GetSkill(skill);
      skillToTrain.trained = !skillToTrain.trained;
    }

    internal void SetTrainableSkills(List<CharacterSkill> skillAvailableToTrainOnSelectedClass, bool set = true)
    {
        for(int i = 0; i < skillAvailableToTrainOnSelectedClass.Count; i++)
        {
            CharacterSkill skill = skillAvailableToTrainOnSelectedClass[i];
            Skill skillToTrain = GetSkill(skill);
            skillToTrain.canBeTrained = set;
        } 
    }

    public void AddBonusToAbility(List<Ability> abilityBonuses, bool subtract = false)
    {
       for(int i = 0; i < abilityBonuses.Count; i++)
       {
            Ability a = abilities[(int)abilityBonuses[i].ability];
            if(subtract == false)
            {
                a.bonus += abilityBonuses[i].abilityScore;
            }
            else 
            {
                a.bonus -= abilityBonuses[i].abilityScore;
            }
            
       }
    }
}
