# TrailBlazer for .NET Core

## Status

|[![CircleCI](https://circleci.com/gh/jlat96/TBCore/tree/master.svg?style=svg&circle-token=a7d863cfb0fe6c6023a12dbf4aa8eca916c52f3b)](https://circleci.com/gh/jlat96/TBCore/tree/master) |![GithubIssues](https://img.shields.io/github/issues/jlat96/TBCore)|

|Release|Version|
|-------|-------|
|Latest stable|![GH_Release](https://img.shields.io/github/v/release/jlat96/TBCore)|
|Pre-release|!GH_Pre-Release](https://img.shields.io/github/v/release/jlat96/TBCore?include_prereleases)|



## Introduction

TrailBlazer is a path finding and artificial intelligence library for .NET. Our goal is to provide users with a simple, powerful set of path finding tools to easily implement path finding and local search into their applications. The core foundation of TrailBlazer is a generic framework so that applications of any scope and scale can make use of efficient tools without each project requiring a complex independent implementation.

## Getting Started

|Project  |Download       |
|---------|---------------|
|Optimizer|Comning soon!  |
|Source   |Coming Soon!   |

## Package Contents

### Optimizer

Optimizer aims to provide tools to maximize or minimize configurations of states using algoritms like Hill Climbing and Monte Carlo evaluation.

#### Usage

* Create a state class that can calculate a utility score that implements ```IEvaluable```, ```ITypedClonable```, and ```IComparable```. 
* Create an ```ISuccessorGenerator``` implentation that will create neighbor states for a your ```IEvaluable``` type (Tip: extend ```AbstractEvaluable``` to simplify the implementation). 
* Create an instance of Climber with the an ```IComparer``` that will yield your desired result (uphill or downhill climing). 
* Create an instance of your hill climber and invoke ```Optimize```
