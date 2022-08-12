using System;
using System.Linq;
using Entitas;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Services {
	public static partial class CleanCodeExtensions {
		public static GameObject createController<T>
			(this GameObject view, GameContext @in, IEntity @bindTo)
			where T : Component, IViewController {
			view
				.AddComponent<T>()
				.initializeView(@in: @in, with: @bindTo);

			return view;
		}

		public static GameObject addListeners(this GameObject view, IEntity @from) {
			foreach (string component in @from.GetComponents().Select(c => c.GetType().Name)) {
				var type = Type.GetType(
					$"Rewind.ViewListeners.{component.Replace("Component", "")}Listener"
				);
				if (type != null) view.AddComponent(type);
			}

			return view;
		}

		public static GameObject registerListeners(this GameObject view, Entity with) {
			foreach (var listener in view.GetComponents<IEventListener>())
				listener.registerListeners();
		
			return view;
		}
	}
}