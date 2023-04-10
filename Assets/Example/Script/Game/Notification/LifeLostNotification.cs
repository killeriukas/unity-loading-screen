using TMI.Notification;

public class LifeLostNotification : BaseValueChangeNotification<int>
{
    public LifeLostNotification(int oldValue, int newValue) : base(oldValue, newValue) {
    }
}
