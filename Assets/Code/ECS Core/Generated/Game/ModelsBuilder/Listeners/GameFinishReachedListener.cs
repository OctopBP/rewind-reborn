//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Octop.ComponentModel.CodeGenerators.ComponentListenerGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Rewind.Extensions;
using UnityEngine;
using Octop.ComponentModel;

public class GameFinishReachedListener : IGameComponentListener, IFinishReachedListener {
	public void Register(GameEntity entity) => entity.AddFinishReachedListener(this);
	public void Unregister(GameEntity entity) => entity.RemoveFinishReachedListener(this);

	public void OnFinishReached(GameEntity entity) { }
}