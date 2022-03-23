using System;
using LanguageExt;
using Sirenix.OdinInspector;

namespace Rewind.ViewListeners {
	public class SelfInitializedViewWithId : SelfInitializedView {
		[Button, Title("$" + nameof(getId))]
		void generateGuid() => maybeId = Guid.NewGuid();

		string getId() => maybeId.Match(id => id.ToString(), () => "No id");

		public Option<Guid> maybeId;
		public Guid id => maybeId.Match(guid => guid, () => {
			var newId = Guid.NewGuid(); 	
			maybeId = newId;
			return newId;
		});

		protected override void onAwake() {
			base.onAwake();
			entity.AddId(id);
		}
	}
}