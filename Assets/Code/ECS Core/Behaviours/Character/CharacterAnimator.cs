using System;
using Rewind.Data;
using Rewind.ECSCore.Enums;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour {
  [SerializeField] Transform container;
  [SerializeField] Animator animator;

  static readonly int runKey = Animator.StringToHash("Run");
  static readonly int stopKey = Animator.StringToHash("Stop");
  static readonly int openKey = Animator.StringToHash("Open");
  static readonly int playSpeedKey = Animator.StringToHash("PlaySpeed");

  public class Init : ICharacterStateListener, ICharacterLookDirectionListener, IAnyClockStateListener {
    readonly CharacterAnimator backing;
    readonly GameSettingsData gameSettings;
    
    public Init(CharacterAnimator backing, GameSettingsData gameSettings) {
      this.backing = backing;
      this.gameSettings = gameSettings;
    }
    
    public void OnCharacterState(GameEntity _, CharacterState value) {
      var trigger = value switch {
        CharacterState.Idle => stopKey,
        CharacterState.Walk => runKey,
        _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
      };
      backing.animator.SetTrigger(trigger);
    }

    public void OnCharacterLookDirection(GameEntity _, CharacterLookDirection value) =>
      backing.container.localScale = new Vector3(value.value(), 1, 1);

    public void OnAnyClockState(GameEntity _, ClockState value) {
      var speed = gameSettings.characterSpeed * (value.isRewind()
          ? gameSettings.clockRewindSpeed
          : gameSettings.clockNormalSpeed
        );

      backing.animator.SetFloat(playSpeedKey, speed);
    }
  }
}
