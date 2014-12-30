using UnityEngine;
using System.Collections;

public abstract class FSMState <T>
{
	abstract public void Enter (T obj);
	abstract public void Execute (T obj);
	abstract public void Exit (T obj);
}