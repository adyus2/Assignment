using System;
using System.Timers;

// 事件参数类
public class TickEventArgs : EventArgs
{
    public DateTime CurrentTime { get; set; }
}

public class AlarmEventArgs : EventArgs
{
    public DateTime AlarmTime { get; set; }
}

// 闹钟类
public class AlarmClock
{
    private readonly System.Timers.Timer _timer;
    private DateTime _currentTime;
    private DateTime _alarmTime;

    public event EventHandler<TickEventArgs> Tick;
    public event EventHandler<AlarmEventArgs> Alarm;

    public AlarmClock()
    {
        _timer = new System.Timers.Timer(1000); // 1秒间隔
        _timer.Elapsed += OnTimerElapsed;
        _timer.AutoReset = true;
    }

    public void SetAlarm(DateTime alarmTime)
    {
        _alarmTime = alarmTime;
    }

    public void Start()
    {
        _currentTime = DateTime.Now;
        _timer.Start();
    }

    public void Stop()
    {
        _timer.Stop();
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        _currentTime = _currentTime.AddSeconds(1);

        // 触发Tick事件
        Tick?.Invoke(this, new TickEventArgs { CurrentTime = _currentTime });

        // 检查是否触发Alarm
        if (_currentTime >= _alarmTime)
        {
            Alarm?.Invoke(this, new AlarmEventArgs { AlarmTime = _alarmTime });
            Stop();
        }
    }
}

class Program
{
    static void Main()
    {
        var alarmClock = new AlarmClock();

        // 设置如果不主动关闭闹钟，则5秒后响铃
        alarmClock.Start();
        alarmClock.SetAlarm(DateTime.Now.AddSeconds(5));

        alarmClock.Tick += (s, e) =>
            Console.WriteLine($"[Tick] Current Time: {e.CurrentTime:HH:mm:ss}");

        alarmClock.Alarm += (s, e) =>
            Console.WriteLine($"[ALARM!] Target Time: {e.AlarmTime:HH:mm:ss}");

        Console.WriteLine("Alarm clock started. Press any key to exit...");
        Console.ReadKey();
    }
}