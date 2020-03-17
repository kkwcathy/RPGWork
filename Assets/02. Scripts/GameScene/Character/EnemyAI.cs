public class EnemyAI : CharacterAI
{
	public EnemyAI(Character character) : base(character)
	{

	}

	public override void Init()
	{
		base.Init();

		_charStateDic.Add(Character.eStateType.NoTarget, new IdleState(_character));
	}
}
