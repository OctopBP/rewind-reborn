using Code.Helpers.Tracker;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Rewind.Behaviours {
	// TODO: Rename to PathConnector
	public partial class Connector : MonoBehaviour, IInitWithTracker {
		[Space(10), SerializeField] TwoPointsWithDirection twoPointsWithDirection;
		[Space(10), PropertyOrder(1), SerializeField] float activateDistance;

		Model model;
		public void initialize(ITracker tracker) => model = new Model(this, tracker);

		public class Model : TrackedEntityModel<GameEntity>, IConnectorStateListener {
			public readonly ReactiveProperty<ConnectorState> state = new(ConnectorState.Closed);

			public Model(Connector connector, ITracker tracker) : base(tracker) => entity
				.with(e => e.isConnector = true)
				.with(e => e.AddPathPointsPare(connector.twoPointsWithDirection.asPathPointsPare))
				.with(e => e.AddActivateDistance(connector.activateDistance))
				.with(e => e.AddConnectorState(state.Value))
				.with(e => e.AddConnectorStateListener(this));

			public void OnConnectorState(GameEntity _, ConnectorState value) => state.Value = value;
		}
	}
}