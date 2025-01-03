using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityScore", menuName = "Character Creation/AbilityScore")]
[System.Serializable]
public class AbilityScore : ScriptableObject
{
    public string abilityName; // e.g., "Strength", "Dexterity", etc.
}
