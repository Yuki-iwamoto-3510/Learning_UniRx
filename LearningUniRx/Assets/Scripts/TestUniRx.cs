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

    [SerializeField] Button button = default;
    [SerializeField] Button button2 = default;
    [SerializeField] Button button3 = default;

    void Start()
    {
        countTimer = 0;
        countTimerFrame = 0;



        // Observable.Timer/Observabe.TimerFrame
        UniRx_Timer_TimerFrame();
    }

    #region Observable.Timer/Observabe.TimerFrame
    /// <summary>
    /// Observable.Timer/Observabe.TimerFrame
    /// </summary>
    private void UniRx_Timer_TimerFrame()
    {
        //* 一定時間後に処理を実行 *//

        // 3秒後にカウントを1つ増やす
        Observable.Timer(TimeSpan.FromSeconds(3.0f))
            .Subscribe((_) => CountUpTimer());

        // 30フレーム後にカウントを1つ増やす
        Observable.TimerFrame(30)
            .Subscribe((_) => CountUpTimerFrame());
    }

    /// <summary>
    /// Observable.Timer/Observabe.TimerFrame
    /// </summary>
    private void UniRx_Timer_TimerFrame_Interval()
    {
        //* 一定間隔で処理を実行 *//

        // 3秒後に1秒間隔のカウント増加を開始
        Observable.Timer(TimeSpan.FromSeconds(3.0f), TimeSpan.FromSeconds(1.0f))
            .Subscribe((_) => CountUpTimer());

        // 30フレーム後に20フレーム間隔のカウント増加を開始
        Observable.TimerFrame(30, 20)
            .Subscribe((_) => CountUpTimerFrame());
    }
    #endregion

    #region ThrottleFirst/ThrottleFirstFrame

    /// <summary>
    /// Observable.ThrottleFirst/Observable.ThrottleFirstFrame
    /// </summary>
    private void UniRx_ThrottleFirst_ThrottleFirstFrame()
    {
        //* 指定した時間通知を弾く *//
        
        // 1度押下してから0.5秒間は押下を受け付けない
        button.OnClickAsObservable()
        .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
        .Subscribe((_) => 
        {
            Debug.Log("Push");
        });

        // 1度押下してから30フレームは押下を受け付けない
        button2.OnClickAsObservable()
        .ThrottleFirstFrame(30)
        .Subscribe((_) =>
        {
            Debug.Log("Push");
        });
    }

    #endregion

    #region Return/Where

    /// <summary>
    /// Observable.Return/Observable.Where
    /// </summary>
    private void UniRx_Return_Where()
    {
        int[] numList = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        // 偶数の値のみログ出力
        foreach(var num in numList)
        {
            Observable.Return(num)
                .Where(num => num % 2 == 0)
                .Subscribe(num => Debug.Log(num));
        }
    }

    #endregion

    #region TakeWhile/First

    /// <summary>
    /// Observable.TakeWhile
    /// </summary>
    private void UniRx_TakeWhile()
    {
        int count = 100;

        // カウントが0になるまで、1秒毎に1つ減少させる
        Observable.Timer(TimeSpan.FromSeconds(0.0f), TimeSpan.FromSeconds(1.0f))
            .TakeWhile(_ => count > 0)
            .Subscribe(_ => count--);
    }

    /// <summary>
    /// Observable.First
    /// </summary>
    private void UniRx_First()
    {
        // 一度しか押下できないボタン
        button3.OnClickAsObservable()
            .First()
            .Subscribe(_ => Debug.Log("Push"));
    }

    #endregion

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
