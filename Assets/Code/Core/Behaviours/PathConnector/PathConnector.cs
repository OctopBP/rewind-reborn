using System;
using Code.Helpers.Tracker;
using LanguageExt;
using Rewind.Infrastructure;
using Rewind.LogicBuilder;
using Rewind.SharedData;
using Rewind.ViewListeners;
using UniRx;
using UnityEngine;

namespace Rewind.Behaviours
{
	public partial class PathConnector : EntityIdBehaviour, IInitWithTracker
	{
		[Space(10), SerializeField] private TwoPointsWithDirection twoPointsWithDirection;
		[SerializeField] private ConditionGroup conditionGroup;

		private Model model;
		public void Initialize(ITracker tracker) => model = new Model(
			id, twoPointsWithDirection.AsPathPointsPare, tracker, ConnectorState.Opened,
			new IPuzzleValueReceiver[] { new PathConnectorValueReceiver(this) }, conditionGroup
		);

		public new class Model : EntityIdBehaviour.Model, IConnectorStateListener
		{
			public readonly ReactiveProperty<ConnectorState> state;
			
			public static Model fromPathPointsPare(PathPointsPare pathPointsPare, ITracker tracker) => new Model(
				SerializableGuid.Create(), pathPointsPare, tracker, ConnectorState.Opened,
				Array.Empty<IPuzzleValueReceiver>(), Option<ConditionGroup>.None
			);
			
			public Model(
				SerializableGuid guid, PathPointsPare pathPointsPare, ITracker tracker, ConnectorState initState,
				IPuzzleValueReceiver[] puzzleValueReceivers, Option<ConditionGroup> maybeConditionGroup
			) : base(guid, tracker)
			{
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