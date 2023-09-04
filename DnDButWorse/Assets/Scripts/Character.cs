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
    Charisma
}

[Serializable]

// how the whole ability system works
public class Ability
{
    public int abilityScore;
    public CharacterAbility ability;

    public int Mod
    {
        get
        {
            int mod = abilityScore - 10;
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
public int bonus;

public Skill(CharacterSkill skill, CharacterAbility ability, bool trained = false, int bonus = 0)
{
  this.skill = skill;
  this.abilityMod = ability;
  this.trained = trained;
  this.bonus = bonus;
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



[CreateAssetMenu]

public class Character : ScriptableObject
{
    public string Name;

    public List<Ability> abilities;
    public List<Skill> skills;

    [SerializeField] SkillList SkillBaseStructure;

    // changes ability score by 1
    public void ChangeAbilityScore(int by, CharacterAbility ability)
    {
        Ability a = abilities[(int)ability];
        a.abilityScore += by;
        if(a.abilityScore < 1)
        {
            a.abilityScore = 1;
        }
    }

    [ContextMenu("Generate Character")]

    // generates the initial stats for the character and the skill list
    public void GenerateCharacterBase()
    {
        abilities = new List<Ability>();
        abilities.Add(new Ability(CharacterAbility.Strength, 10));
        abilities.Add(new Ability(CharacterAbility.Dexterity, 10));
        abilities.Add(new Ability(CharacterAbility.Constitution, 10));
        abilities.Add(new Ability(CharacterAbility.Intelligence, 10));
        abilities.Add(new Ability(CharacterAbility.Wisdom, 10));
        abilities.Add(new Ability(CharacterAbility.Charisma, 10));

        skills = new List<Skill>();
        for(int i = 0; i < SkillBaseStructure.skills.Count; i++)
        {
            Skill s = SkillBaseStructure.skills[i];
            skills.Add(new Skill(s.skill, s.abilityMod));
        }
    } 

    // gets the ability mod
    public int GetAbilityMod(CharacterAbility abilityMod)
    {
        return abilities[(int)abilityMod].Mod;
    }


    // gets the skill level
    internal Skill GetSkill(CharacterSkill skill)
    {
        return skills[(int)skill];
    }


    // trains the skill to the character
    internal void TrainSkill(CharacterSkill skill)
    {
      Skill skillToTrain = GetSkill(skill);
      skillToTrain.trained = !skillToTrain.trained;
    }
}
