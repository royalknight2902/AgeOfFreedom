using UnityEngine;
using System.Collections;

public enum ESceneState
{
	ADVENTURE,
	DAILY_QUEST_3MINS,
    BLUETOOTH,
}

public class SceneState
{
	#region 
	static SceneState m_instance;
	public static SceneState Instance
	{
		get
		{
			if(m_instance == null)
				m_instance = new SceneState();
			return m_instance;
		}
	}
	#endregion

	public ESceneState State = ESceneState.ADVENTURE;
}