# TrailBlazer for .NET Core

## Status

[![CircleCI](https://circleci.com/gh/jlat96/TBCore/tree/master.svg?style=svg&circle-token=a7d863cfb0fe6c6023a12dbf4aa8eca916c52f3b)](https://circleci.com/gh/jlat96/TBCore/tree/master) ![GithubIssues](https://img.shields.io/github/issues/jlat96/TBCore)

|Release|Version|
|-------|-------|
|Latest stable|![GH_Release](https://img.shields.io/github/v/release/jlat96/TBCore)|
|Pre-release|![GH_Pre-Release](https://img.shields.io/github/v/release/jlat96/TBCore?include_prereleases)|



## Introduction

TrailBlazer is a path finding and artificial intelligence library for .NET. Our goal is to provide users with a simple, powerful and expandable suite of path finding tools to easily implement path finding and local search into applications. The core foundation of TrailBlazer is a generic framework so that applications of any scope and scale can make use of efficient tools without each project requiring a complex independent implementation.

## Getting Started

Download libraries using NuGet for .NET Core or download the TrailBlazer source project

|Project  |Download                                  |
|---------|------------------------------------------|
|Optimizer|Comning soon!                             |
|Source   |https://github.com/jlat96/TBCore/releases |

## Library Contents

### Optimizer

Optimizer aims to provide tools to maximize or minimize configurations of states using algoritms like Hill Climbing and Monte Carlo evaluation.

#### Climber

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
  

