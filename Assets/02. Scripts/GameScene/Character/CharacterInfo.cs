
// 캐릭터마다 달라지는 속성 정리

[System.Serializable]
public class CharacterInfo 
{
	public CharType charType;

	public int modelID;

	public float maxHp;
	public float power;
	public float defence;

	public CharacterAI charAI;
	public CharacterAttack charAttack;
	 
	public int[] attackIDs;
}
