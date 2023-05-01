using LanguageExt;
using Rewind.Extensions;
using Rewind.Helpers.Interfaces.UnityCallbacks;
using UnityEngine;

namespace Rewind.Core {
	public class SingleLevelBootstrap : MonoBehaviour, IStart, IUpdate {
		Option<CoreBootstrap.Model> maybeCoreBootstrapModel;
		
		public void Start() => maybeCoreBootstrapModel = FindObjectOfType<CoreBootstrap>()
			.optionFromNullable()
			.Map(coreBootstrap => new CoreBootstrap.Model(coreBootstrap));

		public void Update() => maybeCoreBootstrapModel.IfSome(m => m.update()); 
	}
}