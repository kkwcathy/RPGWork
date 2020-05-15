
// 캐릭터 사망 상태 클래스
using UnityEngine;

public class DeathState : StateBase
{
	public DeathState(Character character) : base(character)
	{
	}

	public override void StartState()
	{
		_character.PlayAnimation("Death");
		//_character.Die();
	}

	public override void UpdateState()
	{
		if (_character.IsAnimationFinished("Death"))
		{
			_character.Die();
		}
	}
}
