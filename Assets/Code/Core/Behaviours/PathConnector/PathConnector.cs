using System;
using Code.Helpers.Tracker;
using LanguageExt;
using Rewind.Infrastructure;
using Rewind.LogicBuilder;
using Rewind.SharedData;
using Rewind.ViewListeners;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class PathConnector : EntityIdBehaviour, IInitWithTracker {
		[Space(10), SerializeField] TwoPointsWithDirection twoPointsWithDirection;
		[SerializeField] ConditionGroup conditionGroup;
		[SerializeField] PathConnectorValueReceiver pathConnectorValueReceiver;

		Model model;
		public void initialize(ITracker tracker) => model = new Model(
			id, twoPointsWithDirection.asPathPointsPare, tracker, ConnectorState.Opened,
			new IPuzzleValueReceiver[] { pathConnectorValueReceiver }, conditionGroup
		);

		public class Model : EntityIdBehaviour.Model, IConnectorStateListener {
			public readonly ReactiveProperty<ConnectorState> state;
			
			public static Model fromPathPointsPare(PathPointsPare pathPointsPare, ITracker tracker) => new Model(
				SerializableGuid.create(), pathPointsPare, tracker, ConnectorState.Opened,
				Array.Empty<IPuzzleValueReceiver>(), Option<ConditionGroup>.None
			);
			
			public Model(
				SerializableGuid guid,PathPointsPare pathPointsPare, ITracker tracker, ConnectorState initState,
				IPuzzleValueReceiver[] puzzleValueReceivers, Option<ConditionGroup> maybeConditionGroup
			) : base(guid, tracker) {
				state = new(initState);
				entity
					.SetConnector(true)
					.AddPuzzleValueReceiver(puzzleValueReceivers)
					.AddPathPointsPare(pathPointsPare)
					.AddConnectorState(state.Value)
					.AddConnectorStateListener(this);

				maybeConditionGroup.IfSome(cg => entity.AddConditionGroup(cg));
			}

			public void OnConnectorState(GameEntity _, ConnectorState value) => state.Value = value;
		}
	}
}