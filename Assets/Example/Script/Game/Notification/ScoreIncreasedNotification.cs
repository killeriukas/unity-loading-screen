using TMI.Notification;

public class ScoreIncreasedNotification : BaseValueChangeNotification<int>
{
    public ScoreIncreasedNotification(int oldValue, int newValue) : base(oldValue, newValue) {
    }
}
