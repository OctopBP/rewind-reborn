using ExhaustiveMatching;
using Rewind.Extensions;
using Rewind.SharedData;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
  [SerializeField] private Transform container;
  [SerializeField] private Animator animator;

  private static readonly int RunKey = Animator.StringToHash("Run");
  private static readonly int StopKey = Animator.StringToHash("Stop");
  private static readonly int OpenKey = Animator.StringToHash("Open");
  private static readonly int PlaySpeedKey = Animator.StringToHash("PlaySpeed");
  private static readonly int LadderKey = Animator.StringToHash("Ladder");

  public class Init : ICharacterStateListener, ICharacterLookDirectionListener, IAnyClockStateListener
  {
    private readonly CharacterAnimator backing;
    private readonly GameSettingsData gameSettings;
    
    public Init(CharacterAnimator backing, GameSettingsData gameSettings)
    {
      this.backing = backing;
      this.gameSettings = gameSettings;

      SetAnimatorSpeed(speed: gameSettings._clockNormalSpeed);
    }
    
    public void OnCharacterState(GameEntity _, CharacterState value)
    {
      var trigger = value switch
      {
        CharacterState.Idle => StopKey,
        CharacterState.Walk => RunKey,
        CharacterState.Ladder => LadderKey,
        _ => throw ExhaustiveMatch.Failed(value)
      };
      backing.animator.SetTrigger(trigger);
    }

    public void OnCharacterLookDirection(GameEntity _, CharacterLookDirection value) =>
      backing.container.localScale = Vector3.one.WithX(value.Value());

    public void OnAnyClockState(GameEntity _, ClockState value)
    {
      var speed = value.IsRewind()
        ? gameSettings._clockRewindSpeed
        : gameSettings._clockNormalSpeed;

      SetAnimatorSpeed(speed);
    }

    private void SetAnimatorSpeed(float speed) => backing.animator.SetFloat(PlaySpeedKey, speed);
  }
}
