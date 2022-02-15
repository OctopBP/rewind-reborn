using System;
using Entitas;

namespace Rewind {
	public class RegisterServiceSystem<TService> : IInitializeSystem {
		readonly TService service;
		readonly Action<TService> initServiceComponent;

		public RegisterServiceSystem(TService saveService, Action<TService> initServiceComponent) {
			service = saveService;
			this.initServiceComponent = initServiceComponent;
		}

		public void Initialize() => initServiceComponent(service);
	}
}