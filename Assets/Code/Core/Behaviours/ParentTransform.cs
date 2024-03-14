using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.ECSCore
{
	public class ParentTransform : MonoBehaviour
	{
		[SerializeField] private Transform parent;

		public void Initialize() => new Model(parent);

		private class Model : EntityModel<GameEntity>
		{
			public Model(Transform parent) => entity.AddParentTransform(parent);
		}
	}
}