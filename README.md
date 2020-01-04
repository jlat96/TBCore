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

* Create a state class that can calculate a utility score that extends ```EvaluableState```. Provide your concrete implementation with a method of evaluating the state.
* Create an implementation of ```ISuccessorGenerator``` that will be responsible for creating a neighborhood of successor states from any given state.
* Create an implementation of ```SuccessorPicker``` that will use the successor states that your ```SuccessorGenerator``` created to determine the the next state in the climbing operation.
