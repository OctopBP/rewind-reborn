using ExhaustiveMatching;
using Rewind.Extensions;
using Rewind.SharedData;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour {
  [SerializeField] Transform container;
  [SerializeField] Animator animator;

  static readonly int RunKey = Animator.StringToHash("Run");
  static readonly int StopKey = Animator.StringToHash("Stop");
  static readonly int OpenKey = Animator.StringToHash("Open");
  static readonly int PlaySpeedKey = Animator.StringToHash("PlaySpeed");
  static readonly int LadderKey = Animator.StringToHash("Ladder");

  public class Init : ICharacterStateListener, ICharacterLookDirectionListener, IAnyClockStateListener {
    readonly CharacterAnimator backing;
    readonly GameSettingsData gameSettings;
    
    public Init(CharacterAnimator backing, GameSettingsData gameSettings) {
      this.backing = backing;
      this.gameSettings = gameSettings;

      SetAnimatorSpeed(speed: gameSettings._clockNormalSpeed);
    }
    
    public void OnCharacterState(GameEntity _, CharacterState value) {
      var trigger = value switch {
        CharacterState.Idle => StopKey,
        CharacterState.Walk => RunKey,
        CharacterState.Ladder => LadderKey,
        _ => throw ExhaustiveMatch.Failed(value)
      };
      backing.animator.SetTrigger(trigger);
    }

    public void OnCharacterLookDirection(GameEntity _, CharacterLookDirection value) =>
      backing.container.localScale = Vector3.one.withX(value.value());

    public void OnAnyClockState(GameEntity _, ClockState value) {
      var speed = value.isRewind()
        ? gameSettings._clockRewindSpeed
        : gameSettings._clockNormalSpeed;

      SetAnimatorSpeed(speed);
    }

    void SetAnimatorSpeed(float speed) => backing.animator.SetFloat(PlaySpeedKey, speed);
  }
}
