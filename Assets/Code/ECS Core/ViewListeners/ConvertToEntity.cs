using UnityEngine;

interface IEntityConfig {
	void init();
}
	public class ConvertToEntity : MonoBehaviour {
		[SerializeReference] IEntityConfig entityConfig;
		
		public class Model {
			readonly ConvertToEntity backing;
			
			public Model(ConvertToEntity backing) {
				this.backing = backing;
				backing.entityConfig.init();
			}
		}
	}
