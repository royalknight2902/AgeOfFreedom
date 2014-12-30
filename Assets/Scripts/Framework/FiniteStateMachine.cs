using UnityEngine;
using System.Collections;

public class FiniteStateMachine <T>
{
	T Owner;
	FSMState<T> currentState;
	FSMState<T> previousState;
	FSMState<T> globalState;

	public void Awake()
	{
		currentState = null;
		previousState = null;
		globalState = null;
	}

	public void Configure(T owner, FSMState<T> initialState)
	{
		Owner = owner;
		Change (initialState);
	}

	public void Update()
	{
		if (globalState != null)
			globalState.Execute (Owner);
		if (currentState != null)
			currentState.Execute (Owner);
	}

	public void Change(FSMState<T> newState)
	{
		previousState = currentState;
		if (currentState != null)
			currentState.Exit (Owner);

		currentState = newState;
		if (currentState != null)
			currentState.Enter (Owner);
	}

	public void Revert()
	{
		if(previousState != null)
			Change(previousState);
	}
}
