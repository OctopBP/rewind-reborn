using LanguageExt;
using Rewind.Extensions;
using Rewind.Helpers.Interfaces.UnityCallbacks;
using UnityEngine;

namespace Rewind.Core {
	public class SingleLevelBootstrap : MonoBehaviour, IStart, IUpdate {
		Option<CoreBootstrap.Init> maybeCoreBootstrapModel;
		
		public void Start() => maybeCoreBootstrapModel = FindObjectOfType<CoreBootstrap>()
			.optionFromNullable()
			.Map(coreBootstrap => new CoreBootstrap.Init(coreBootstrap));

		public void Update() => maybeCoreBootstrapModel.IfSome(m => m.update()); 
	}
}