using ExhaustiveMatching;
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
  
  class State {
    enum LookDirection { Right, Left }
    
  }

  public class Init : ICharacterStateListener, ICharacterLookDirectionListener, IAnyClockStateListener {
    readonly CharacterAnimator backing;
    readonly GameSettingsData gameSettings;
    
    public Init(CharacterAnimator backing, GameSettingsData gameSettings) {
      this.backing = backing;
      this.gameSettings = gameSettings;
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
      backing.container.localScale = new Vector3(value.value(), 1, 1);

    public void OnAnyClockState(GameEntity _, ClockState value) {
      var speed = gameSettings._characterSpeed * (value.isRewind()
          ? gameSettings._clockRewindSpeed
          : gameSettings._clockNormalSpeed
        );

      backing.animator.SetFloat(PlaySpeedKey, speed);
    }
  }
}
