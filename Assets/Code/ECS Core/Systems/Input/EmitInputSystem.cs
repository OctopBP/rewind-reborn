using Entitas;
using UnityEngine;

public class EmitInputSystem : IInitializeSystem, IExecuteSystem {
	readonly InputContext input;
	InputEntity leftMoveEntity;
	InputEntity rightMoveEntity;
	InputEntity intaractEntity;
	InputEntity rewindTimeEntity;

	public EmitInputSystem(Contexts contexts) {
		input = contexts.input;
	}

	public void Initialize() {
		// input.isLeftMove = true;
		// leftMoveEntity = input.leftMoveEntity;
		//
		// input.isRightMove = true;
		// rightMoveEntity = input.rightMoveEntity;
		//
		// input.isInteract = true;
		// intaractEntity = input.interactEntity;
		//
		// input.isRewindTime = true;
		// rewindTimeEntity = input.rewindTimeEntity;
	}

	public void Execute() {
		// // left mouse button
		// if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
		// 	leftMoveEntity.isKeyDown = true;
		// 	leftMoveEntity.ReplaceTick(Time.frameCount);
		// }
		//
		// if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) {
		// 	leftMoveEntity.isKeyDown = false;
		// }
		//
		// // right mouse button
		// if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
		// 	rightMoveEntity.isKeyDown = true;
		// 	rightMoveEntity.ReplaceTick(Time.frameCount);
		// }
		//
		// if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) {
		// 	rightMoveEntity.isKeyDown = false;
		// }
		//
		// // intaract button
		// if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.UpArrow)) {
		// 	intaractEntity.isKeyDown = true;
		// 	intaractEntity.ReplaceTick(Time.frameCount);
		// }
		//
		// if (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.UpArrow)) {
		// 	intaractEntity.isKeyDown = false;
		// }
		//
		// // rewind time button
		// if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.DownArrow)) {
		// 	rewindTimeEntity.isKeyDown = true;
		// 	rewindTimeEntity.ReplaceTick(Time.frameCount);
		// }
		//
		// if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.DownArrow)) {
		// 	rewindTimeEntity.isKeyDown = false;
		// }
	}
}