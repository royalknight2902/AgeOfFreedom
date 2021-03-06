using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationEventFrame
{
    public int KeyFrame;
    public bool RunOnlyOnce;
    public EventDelegate Callback;

    public AnimationEventFrame(int keyFrame, EventDelegate callback, bool runOnlyOnce)
    {
        KeyFrame = keyFrame;
        Callback = callback;
        RunOnlyOnce = runOnlyOnce;
    }
}

public class AnimationStateMachine
{
    public float TimeFrame;
    public Sprite[] Sprites;
    public Dictionary<string, AnimationEventFrame> Events = new Dictionary<string, AnimationEventFrame>();

    public bool isSpecificLoop;
    public int[] SpecificLoopIndex;

    public AnimationStateMachine(float timeFrame, Sprite[] sprites, bool specificLoop)
    {
        TimeFrame = timeFrame;
        Sprites = sprites;
        isSpecificLoop = specificLoop;
    }
}

public class AnimationEventState
{
    public float TimeFrame;
    public string ResourcePath;
    public List<object> listKeyEventFrame = new List<object>();
    public Dictionary<string, object> Values = new Dictionary<string, object>();

    public bool isSpecificLoop;
    public int[] SpecificLoopIndex;

    public AnimationEventState()
    {
        TimeFrame = 0.1f;
        ResourcePath = "";
        isSpecificLoop = false;
        SpecificLoopIndex = null;
    }
}

public class AnimationSpecificLoop
{
    public bool Enable;
    public bool Active;
    public int Count;
    public int[] Values;

    public AnimationSpecificLoop()
    {
        this.Enable = false;
        this.Values = null;
        this.Count = 0;
        this.Active = false;
    }

    public void set(bool enable, int[] values)
    {
        this.Enable = enable;
        this.Values = values;
    }

    public void reset()
    {
        Count = 0;
        Active = false;
        Enable = false;
        Values = null;
    }
}

[RequireComponent(typeof(SpriteRenderer))]
public class AnimationFrames : MonoBehaviour
{
    public Dictionary<object, AnimationStateMachine> listData = new Dictionary<object, AnimationStateMachine>();

    public float timeFrame;
    public int frameLength;
    public int keyStart;
    public int keyEnd;
    public bool isEnable;

    AnimationSpecificLoop special;

    object currentState;
    int currentKeyFrame;
    SpriteRenderer render;
    Sprite[] frames;
    float elapsedTime;
    bool isLoop;
    bool isForced;

    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        frames = new Sprite[0];
        isForced = false;
        special = new AnimationSpecificLoop();
    }

    public void createAnimation(object state, string _path, float _timeFrame, bool looping,
        bool _isSpecificLoop = false, int[] _specificLoopIndex = null)
    {
        currentState = state;
        isLoop = looping;
        isForced = false;
        special.reset();

        if (listData.ContainsKey(state))
        {
            AnimationStateMachine stateMachine = listData[state];
            frames = stateMachine.Sprites;

            timeFrame = _timeFrame;
            frameLength = stateMachine.Sprites.Length;
            keyStart = currentKeyFrame = 0;
            keyEnd = frames.Length - 1;
            if (frameLength > 1)
            {
                render.sprite = frames[currentKeyFrame];
                isEnable = true;
            }
            else if (frameLength == 1)
            {
                render.sprite = frames[currentKeyFrame];
                isEnable = false;
            }

            if (stateMachine.isSpecificLoop)
            {
                special.set(true, stateMachine.SpecificLoopIndex);
            }
        }
        else
        {
            Resources.UnloadUnusedAssets();
            // frames = Resources.LoadAll<Sprite>(_path);
            Sprite[] temps = Resources.LoadAll<Sprite>(_path);
            frameLength = temps.Length;
            frames = new Sprite[frameLength];
            int index = 0, lenArr = 0;

            for (int i = 0; i < frameLength; i++)
            {
                lenArr = temps[i].name.ToString().Split('_').Length;
                if (lenArr == 1)
                {
                    lenArr = temps[i].name.ToString().Split('-').Length;
                    index = int.Parse((temps[i].name.ToString().Split('-')[lenArr - 1]).ToString());
                    frames[index - 1] = temps[i];
                }
                else
                {
                    index = int.Parse((temps[i].name.ToString().Split('_')[lenArr - 1]).ToString());
                    frames[index - 1] = temps[i];
                }

            }
            timeFrame = _timeFrame;
            keyStart = currentKeyFrame = 0;
            keyEnd = frameLength - 1;

            if (frameLength > 1)
            {
                render.sprite = frames[currentKeyFrame];
                listData.Add(state, new AnimationStateMachine(_timeFrame, frames, _isSpecificLoop));
                isEnable = true;
            }
            else if (frameLength == 1)
            {
                render.sprite = frames[currentKeyFrame];
                listData.Add(state, new AnimationStateMachine(_timeFrame, frames, _isSpecificLoop));
                isEnable = false;
            }

            if (_isSpecificLoop)
            {
                listData[state].SpecificLoopIndex = _specificLoopIndex;
                special.set(true, _specificLoopIndex);
            }
        }

        checkEventFrame();
    }

    public void createAnimation(string _path, float _timeFrame, bool looping)
    {
        isLoop = looping;
        isForced = true;
        special.reset();

        Resources.UnloadUnusedAssets();
        // frames = Resources.LoadAll<Sprite>(_path);
        Sprite[] temps = Resources.LoadAll<Sprite>(_path);
        frameLength = temps.Length;
        frames = new Sprite[frameLength];
        int index = 0, lenArr = 0;
        for (int i = 0; i < frameLength; i++)
        {
            lenArr = temps[i].name.ToString().Split('_').Length;
            if (lenArr == 1)
            {
                lenArr = temps[i].name.ToString().Split('-').Length;
                index = int.Parse((temps[i].name.ToString().Split('-')[lenArr - 1]).ToString());
                frames[index - 1] = temps[i];
            }
            else
            {
                index = int.Parse((temps[i].name.ToString().Split('_')[lenArr - 1]).ToString());
                frames[index - 1] = temps[i];
            }

        }
        timeFrame = _timeFrame;
        keyStart = currentKeyFrame = 0;
        keyEnd = frameLength - 1;

        if (frameLength > 1)
        {
            render.sprite = frames[currentKeyFrame];
            isEnable = true;
        }
        else if (frameLength == 1)
        {
            render.sprite = frames[currentKeyFrame];
            isEnable = false;
        }
    }

    void Update()
    {
        if (!isEnable)
            return;
        if (frameLength > 0)
        {
            if (!special.Enable) //loop from a-z
            {
                if (currentKeyFrame >= keyEnd && !isLoop)
                {
                    return;
                }

                elapsedTime += Time.deltaTime;

                //Next frame
                if (elapsedTime >= timeFrame)
                {
                    if (currentKeyFrame >= keyEnd && isLoop)
                        currentKeyFrame = keyStart;
                    else
                        currentKeyFrame++;

                    render.sprite = frames[currentKeyFrame];
                    elapsedTime -= timeFrame;

                    if (!isForced)
                        checkEventFrame();
                }
            }
            else // loop the specific index
            {
                elapsedTime += Time.deltaTime;

                if (elapsedTime >= timeFrame)
                {
                    if (special.Active)
                    {
                        if (special.Count >= special.Values.Length - 1 && isLoop)
                        {
                            special.Count = 0;
                            currentKeyFrame = special.Values[0];
                        }
                        else
                        {
                            special.Count++;
                            currentKeyFrame = special.Values[special.Count];
                        }
                    }
                    else
                    {
                        foreach (int i in special.Values)
                        {
                            if (currentKeyFrame == i)
                            {
                                special.Active = true;
                                break;
                            }
                        }
                        if (!special.Active)
                            currentKeyFrame++;
                        else
                        {
                            special.Count++;
                            currentKeyFrame = special.Values[special.Count];
                        }
                    }
                    render.sprite = frames[currentKeyFrame];
                    elapsedTime -= timeFrame;

                    if (!isForced)
                        checkEventFrame();
                }
            }
        }
    }

    public void checkEventFrame()
    {
        while (true)
        {
            bool repeat = false;
            if (listData.ContainsKey(currentState))
            {
                foreach (KeyValuePair<string, AnimationEventFrame> iterator in listData[currentState].Events)
                {
                    if (iterator.Value.KeyFrame == currentKeyFrame)
                    {
                        iterator.Value.Callback.Execute();

                        if (iterator.Value.RunOnlyOnce)
                        {
                            listData.Remove(listData[currentState].Events.Remove(iterator.Key));
                            repeat = true;
                            break;
                        }
                    }
                }
            }
            if (!repeat)
                return;
        }
    }

    public void addEvent(object[] events, EventDelegate callback, bool runOnlyOnce)
    {
        foreach (object key in events)
        {
            int keyEventFrame = System.Convert.ToInt32(key);
            string keyDictionary = keyEventFrame + "_" + callback.methodName;

            if (!listData[currentState].Events.ContainsKey(keyDictionary))
                listData[currentState].Events.Add(keyDictionary, new AnimationEventFrame(keyEventFrame, callback, runOnlyOnce));
        }
    }

    public void addEventLastKey(EventDelegate callback, bool runOnlyOnce)
    {
        string keyDictionary = frames.Length - 1 + "_" + callback.methodName;

        if (!listData[currentState].Events.ContainsKey(keyDictionary))
            listData[currentState].Events.Add(keyDictionary, new AnimationEventFrame(frames.Length - 1, callback, runOnlyOnce));
    }

    public void Reset()
    {
        currentKeyFrame = keyStart;
        render.sprite = frames[currentKeyFrame];
        elapsedTime = 0;
    }

    public void Pause()
    {
        if (isEnable)
            isEnable = false;
    }

    public void Play()
    {
        if (!isEnable)
            isEnable = true;
    }

    public int Count
    {
        get
        {
            return (keyEnd - keyStart) + 1;
        }
    }

    //Index of frame hien tai, bat dau tu 0
    public int KeyIndex
    {
        get
        {
            return currentKeyFrame - keyStart;
        }
    }

    public float TotalTimeAnimation
    {
        get
        {
            return timeFrame * ((keyEnd - keyStart) + 1);
        }
    }

    //Dung neu ket thuc chuoi frame
    public bool EndOfFrames
    {
        get
        {
            if (currentKeyFrame == keyEnd)
                return true;
            else
                return false;
        }
    }

    //Thay doi key frame
    public void changeFrame(int frameStart = 0, int frameEnd = -1)
    {
        currentKeyFrame = keyStart = frameStart;
        render.sprite = frames[currentKeyFrame];
        if (frameEnd == -1 || frameEnd >= frames.Length)
        {
            keyEnd = frames.Length - 1;
        }
        else
            keyEnd = frameEnd;

    }
}

