Optimizer aims to provide tools to maximize or minimize configurations of states using algoritms like Hill Climbing and Monte Carlo evaluation.

## Climber

```HillClimber``` implements a versions of the [Hill Climbing Local Search Algorithm](https://en.wikipedia.org/wiki/Hill_climbing).

**To Set Up a Hill Climber:**

* Set up a state system
  1. Create a state class that can calculate a utility score that extends ```EvaluableState```. Provide your concrete implementation with a method of evaluating the state.
  2. Create an implementation of ```ISuccessorGenerator``` that will be responsible for creating a neighborhood of successor states from any given state.
  3. Create an implementation of ```SuccessorPicker``` that will use the successor states that your ```SuccessorGenerator``` created to determine the the next state in the climbing operation.

* Set up a Climber
  1. Choose a ClimberAlgorithm from the included algorithms or create your own implementation of ```ClimberAlgorithm```.
  2. Choose an ```IComparer``` as a comparison strategy. This will determine if the climber is ascending or descending.
  3. Initialize the selected ClimberAlgorithm with the seleted comparer and successor picker. Optionally, you can initialize the algorithm to perform a ```greedy``` (First Search) evaluation.
  4. Choose a HillClimber from the included climbers or create your own implementation of ```HillClimber```.
  5. Initialize the selected HillClimber with the ```ClimberAlgorithm``` created in step 3
  
* Use the HillClimber
  1. Start the hill climber by invoking ```HillClimber.PerformOptimization(TState)``` to optimize the given initial state.
  
