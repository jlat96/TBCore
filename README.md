# TrailBlazer for .NET Core

## Introduction

TrailBlazer is a path finding and artificial intelligence library for .NET. Our goal is to provide users with a simple, powerful set of path finding tools to easily implement path finding and local search into their applications. The core foundation of TrailBlazer is a generic framework so that applications of any scope and scale can make use of efficient tools without each project requiring a complex independent implementation.

## Package Contents

### Optimizer

Optimizer aims to provide tools to maximize or minimize configurations of states using algoritms like Hill Climbing and Monte Carlo evaluation.

#### Usage

* Create a state class that can calculate a utility score that implements ```IEvaluable```, ```ITypedClonable```, and ```IComparable```. 
* Create an ```ISuccessorGenerator``` implentation that will create neighbor states for a your ```IEvaluable``` type (Tip: extend ```AbstractEvaluable``` to simplify the implementation). 
* Create an instance of Climber with the an ```IComparer``` that will yield your desired result (uphill or downhill climing). 
* Create an instance of your hill climber and invoke ```Optimize```
