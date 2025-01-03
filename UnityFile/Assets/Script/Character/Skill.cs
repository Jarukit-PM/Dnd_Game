using UnityEngine;
using System.Collections.Generic;
public enum SkillType { Damage, Buff, Debuff, RacePassive }
[CreateAssetMenu(fileName = "NewSkill", menuName = "Character Creation/Skill")]
public class Skill : ScriptableObject
{
    public string skillName;
    public SkillType skillType;
    public List<hitDice> hitDices;
    public List<damageDice> damageDices;
    
    [System.Serializable]
    public class hitDice
    {
        public Dice dice;
        public int numberOfDice;
    }
    [System.Serializable]
    public class damageDice
    {
        public Dice dice; 
        public int numberOfDice;      
    }
    
    // Ability modifier applied to the skill instead of damage modifier
    public List<AbilityScore> associatedAbility;

    public string detail;

    public List<Dice> GetHitDiceLists(int moredice)
    {
        List<Dice> dices = new List<Dice>();

        foreach (var hitDice in hitDices)
        {
            for (int i = 0; i < hitDice.numberOfDice + moredice; i++)
            {
                dices.Add(hitDice.dice);
            }
        }

        return dices;
    }

    // This method retrieves all the damage dice from the skill
    public List<Dice> GetDamageDiceLists(int moredice)
    {
        List<Dice> dices = new List<Dice>();

        foreach (var damageDice in damageDices)
        {
            for (int i = 0; i < damageDice.numberOfDice + moredice; i++)
            {
                dices.Add(damageDice.dice);
            }
        }

        return dices;
    }

    public string GetHitDiceData()
    {
        List<string> diceStrings = new List<string>();

        foreach (var hitDice in hitDices)
        {
            diceStrings.Add($"{hitDice.numberOfDice}D{(int)hitDice.dice.diceType}");
        }
        if (diceStrings.Count == 0 || skillType != SkillType.Damage) return "-";
        return string.Join("+ ", diceStrings);
    }

    public string GetDmgDiceData()
    {
        List<string> diceStrings = new List<string>();

        foreach (var dmgDice in damageDices)
        {
            diceStrings.Add($"{dmgDice.numberOfDice}D{(int)dmgDice.dice.diceType}");
        }
        if (diceStrings.Count == 0 || skillType != SkillType.Damage || skillName == "Heal") return "-";
        return string.Join("+ ", diceStrings);
    }
}
