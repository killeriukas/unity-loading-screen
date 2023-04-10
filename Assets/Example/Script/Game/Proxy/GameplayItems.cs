using TMI.Pattern;
using UnityEngine;

public class GameplayItems : IProxy
{
   public PaddleBehaviour paddleBehaviour;
   public BallBehaviour ballBehaviour;
   public BrickBehaviour brickPrefab;
   public Transform brickContainerTransform;
   
   public GameController gameController;
}
