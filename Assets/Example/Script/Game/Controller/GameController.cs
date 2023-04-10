using TMI.Core;
using TMI.Notification;

public class GameController : BaseNotificationObject {


    public const int STARTING_SCORE = 0;
    public const int STARTING_LIVES = 3;

    public const int COLUMN_AMOUNT = 22; //22
    public const int ROW_AMOUNT = 10; //10

    private readonly int TOTAL_BRICKS_AMOUNT;
    private int currentBricksAvailable;
    
    private int currentLivesAvailable;
    public int currentScore { get; private set; }

    public bool isAlive => currentLivesAvailable > 0;
    public bool anyBricksLeft => currentBricksAvailable > 0;

    public GameController(IInitializer initializer) : base(initializer) {
        currentLivesAvailable = STARTING_LIVES;
        currentScore = STARTING_SCORE;
        TOTAL_BRICKS_AMOUNT = COLUMN_AMOUNT * ROW_AMOUNT;
        currentBricksAvailable = TOTAL_BRICKS_AMOUNT;
    }

    public void RemoveLife() {
        int oldValue = currentLivesAvailable;
        
        if(currentLivesAvailable > 0) {
            --currentLivesAvailable;
        }

        Trigger(new LifeLostNotification(oldValue, currentLivesAvailable));
    }

    public void RemoveBrick() {
        int oldScore = currentScore;
        ++currentScore;
        
        --currentBricksAvailable;
        
        Trigger(new ScoreIncreasedNotification(oldScore, currentScore));
    }
    
}
