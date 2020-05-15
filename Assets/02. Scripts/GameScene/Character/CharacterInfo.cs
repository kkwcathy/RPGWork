
// 캐릭터마다 달라지는 속성 정리

[System.Serializable]
public class CharacterInfo 
{
	public Character.eCharType charType;

	public int modelID;
	//public string charName;
	//public string prefabName;

	public float maxHp;
	public float power;
	public float defence;

	public CharacterAI charAI;
	public CharacterAttack charAttack;
	 
	public int[] attackIDs;
}
