//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Octop.ComponentModel.CodeGenerators.ComponentModelGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Rewind.Extensions;
using UnityEngine;
using Octop.ComponentModel;

public class GearTypeAModel : IGameComponentModel {
    public GameEntity Initialize(GameEntity entity) => entity.with(e => e.isGearTypeA = true);
}