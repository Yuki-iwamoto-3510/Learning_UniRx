using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class TestUniRx : MonoBehaviour
{
    [SerializeField] Text countTextTimer = default;
    [SerializeField] Text countTextTimerFrame = default;
    private int countTimer = 0;
    private int countTimerFrame = 0;

    void Start()
    {
        countTimer = 0;
        countTimerFrame = 0;

        // Observable.Timer/Observabe.TimerFrame
        SubscribeTimer();
    }

    /// <summary>
    /// Observable.Timer/Observabe.TimerFrame
    /// </summary>
    private void SubscribeTimer()
    {
#if true
        //* 一定時間後に処理を実行 *//

        // 3秒後にカウントを1つ増やす
        Observable.Timer(TimeSpan.FromSeconds(3.0f))
            .Subscribe((_) => CountUpTimer());

        // 30フレーム後にカウントを1つ増やす
        Observable.TimerFrame(30)
            .Subscribe((_) => CountUpTimerFrame());
#else
        //* 一定間隔で処理を実行 *//

        // 3秒後に1秒間隔のカウント増加を開始
        Observable.Timer(TimeSpan.FromSeconds(3.0f), TimeSpan.FromSeconds(1.0f))
            .Subscribe((_) => CountUpTimer());

        // 30フレーム後に20フレーム間隔のカウント増加を開始
        Observable.TimerFrame(30, 20)
            .Subscribe((_) => CountUpTimerFrame());
#endif
    }

    private void CountUpTimer()
    {
        countTimer++;
        countTextTimer.text = $"Count_Timer：{countTimer}";
    }

    private void CountUpTimerFrame()
    {
        countTimerFrame++;
        countTextTimerFrame.text = $"Count_TimerFrame：{countTimerFrame}";
    }
}
