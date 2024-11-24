namespace BOAppFluentUI.Components.Pages;

public class DebounceDispatcher
{
    private Timer timer;
    private DateTime timerStarted { get; set; } = DateTime.UtcNow.AddYears(-1);
    public void Debounce(int interval, Action<object> action, object param = null)
    {
        timer?.Dispose();
        timer = null;

        timer = new Timer(_ =>
        {
            if ((DateTime.UtcNow - timerStarted).TotalMilliseconds < interval)
                return;
            action.Invoke(param);
        }, null, interval, Timeout.Infinite);

        timerStarted = DateTime.UtcNow;
    }
}
