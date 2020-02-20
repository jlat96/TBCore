# Hill Climber Components

## EvaluableState

An Evaluable, represented in TrailBlazer by the abstract class ```EvaluableState<TState, TEvaluation>``` is the primary representation of state through the Optimization process. An EvaluableState contains an evaluation function ```Evaluate<TEvaluation>``` which will lazily evaluate the state for comparison.

### EvaluableState Example

In this example ```MultaplicativeMatrixState``` is an ```EvaluableState``` that represents a *n x n* 2D matrix. The evaluation function calculates the value of each row of the matrix where the value of each index *y* is multiplied by the value of the value of non-negative *y - 1* (or 1 if *y - 1* is represented by a negative index). Concrete implementations of  ```EvaluableState``` are comparable to eachother by their evaluation by default.

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

Example Matrix
```cs
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

## Comparer

A comparer of type ```T``` will determine how an ```EvaluableState<T>``` will be optimized. TBOptimizer includes two built in comparers: ```MaximizingComparer``` which will climb "uphill" to reach a local maximum, and ```MinimizingComparer``` which will climb "downhill" to reach a local minimum.

## SuccessorGenerator

A SuccessorGenerator is a component responsible for creating neighbor or successor states from a given state.

A SuccessorGenerator for the ```MultaplicativeMatrixState``` above would yield n^4 neighbor states, one for each (x,y) position swapped with every other (x,y) position in the matrix. 

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

## ClimberSuccessorPicker

## Hill Climber