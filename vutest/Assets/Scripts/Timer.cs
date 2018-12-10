using UnityEngine;

/// <summary>
/// 시간(초 단위) 지정하면, 0에 도달할 때 Over()의 상태가 true가 됩니다.
/// </summary>
public class Timer
{
    public float interval;
    public float intervalMax;

    /// <summary>
    /// 시간을 지정하여 새로운 타이머를 생성합니다.
    /// </summary>
    /// <param name="time"></param>
    public Timer(float time) { intervalMax = interval = time; }

    /// <summary>
    /// Update에서 사용하면 시간이 점점 줄어듭니다.
    /// </summary>
    /// <returns></returns>
    public float Tick() { return interval -= Time.deltaTime; }

    /// <summary>
    /// 시간이 지났으면 True를 반환합니다.
    /// </summary>
    /// <returns></returns>
    public bool Over() { return interval <= 0; }

    /// <summary>
    /// 시간이 지났다면 True를 반환하고 타이머를 재시작합니다.
    /// </summary>
    /// <returns></returns>
    public bool OverReset()
    {
        if (interval <= 0)
        { Reset(); return true; }
        else
        { return false; }
    }

    /// <summary>
    /// 타이머를 재시작합니다.
    /// </summary>
    public void Reset()
    {
        interval = Mathf.Max(
            intervalMax - Time.deltaTime, interval + intervalMax);
    }

}

