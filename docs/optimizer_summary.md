Optimizer aims to provide tools to maximize or minimize configurations of states using algoritms like Hill Climbing and Monte Carlo evaluation.

## Climber

```HillClimber``` implements a versions of the [Hill Climbing Local Search Algorithm](https://en.wikipedia.org/wiki/Hill_climbing).

**To Set Up a Hill Climber:**

* Set up a state system
  1. Create a state class that can calculate a utility score that extends ```EvaluableState```. Provide your concrete implementation with a method of evaluating the state.
  
  ```cs
  public class MultaplicativeMatrixState : EvaluableState<AddativeMatrixState, int>
  {
      public MultaplicativeMatrixState(int[,] matrix) 
      {
          Matrix = matrix;
      }

      public int[,] Matrix { get; private set; }

      protected override int Evaluate()
      {
          int horizontalSum = 0;
          for (int row = 0; row < Matrix.GetLength(0); row++)
          {
              for (int col = 0; col < Matrix.GetLength(1); col++)
              {
                  horizontalSum += GetIndexScore(row, col);
              }
          }

          return horizontalSum;
      }

      private int GetIndexScore(int x, int y)
      {
          int current = Matrix[x, y];
          int multiplier;

          if (y > 0)
          {
              multiplier = Matrix[x, y - 1];
          }
          else
          {
              multiplier = 1;
          }

          return current * multiplier;
      }
  }
  ```
  
  ```
  int[,] matrix = new int[,]
  {
      { 1,2,3,4,5},
      { 6,7,8,9,10},
      { 1,2,3,4,5},
      { 6,7,8,9,1},
      { 10,2,3,4,5},
  };

  AddativeMatrixState matrixState = new AddativeMatrixState(initialMatrix);
  matrixState.GetEvaluation(); // returns 601
  ```
  
  2. Create an implementation of ```ISuccessorGenerator``` that will be responsible for creating a neighborhood of successor states from any given state. 
  
  ```cs
    public class MatrixSwapGenerator : ISuccessorGenerator<MultaplicativeMatrixState, int> // generates MuntaplicativeMatrixState that will evaluate to int
  {
      public IEnumerable<MultaplicativeMatrixState> GetSuccessors(MultaplicativeMatrixState state)
      {
          List<MultaplicativeMatrixState> successors = new List<MultaplicativeMatrixState>();
          List<MultaplicativeMatrixState> newSwaps;
          for (int row = 0; row < state.Matrix.GetLength(0); row++)
          {
              for (int col = 0; col < state.Matrix.GetLength(1); col++)
              {
                  newSwaps = state.GetSwappedMatrices(row, col);
                  successors.AddRange(newSwaps);
              }
          }
          return successors;
      }
  }
  ```
  
  Alternitively, a successor generation function can be used in cases where a full concrete ```SuccessorGenerator``` is not needed.
  
  ```cs
  // e is the state from which to generate successors, returns an enumerable of successor states
  Func<TState, IEnumerable<TState> successorGenerationFunction = (e) => 
  {
    List<MultaplicativeMatrixState> successors = new List<MultaplicativeMatrixState>();
    List<MultaplicativeMatrixState> newSwaps;
    for (int row = 0; row < state.Matrix.GetLength(0); row++)
    {
        for (int col = 0; col < e.Matrix.GetLength(1); col++)
        {
            newSwaps = e.GetSwappedMatrices(row, col);
            successors.AddRange(newSwaps);
        }
    }
    return successors;
  }
  ```

* Set up a Climber  
  1. Create a HillClimber using ```ClimberConfiguration```. ```ClimberConfiguration``` creates a ```HillClimber``` that will climb using the default ```ClimberAlgorithm```.
  
  
  ```cs
  // using a successor function
  var climberConfig = new ClimberConfiguration<AddativeMatrixState, int>()
    .ComparesUsing(new MaximizingComparer<int>())
    .GeneratesSuccessorsWith(c =>
    {
        List<AddativeMatrixState> successors = new List<AddativeMatrixState>();
        List<AddativeMatrixState> newSwaps;
        for (int row = 0; row < c.Matrix.GetLength(0); row++)
        {
            for (int col = 0; col < c.Matrix.GetLength(1); col++)
            {
                newSwaps = c.GetSwappedMatrices(row, col);
                successors.AddRange(newSwaps);
            }
        }
        return successors;
    });
    
  //using an existing climber algorithm
  var climberConfig = new ClimberConfiguration<AddativeMatrixState, int>().UsingAlgorithm(climberAlgorithm);

  IHillClimber<AddativeMatrixState, int> climber = climberConfig.Build();
  ```
  
* Use the HillClimber
  1. Start the hill climber by invoking ```HillClimber.PerformOptimization(TState)``` to optimize the given initial state.
  
