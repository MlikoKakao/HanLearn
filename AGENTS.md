# AI Usage and Learning Policy

HanLearn is primarily a learning project for improving my C#/.NET programming skills. The goal is not to build the application as quickly as possible. The process of figuring out how to implement features is part of the project.

## Core Principle

**Do not write C#/.NET or Razor implementations for me unless I explicitly ask you to.**

Prefer teaching, guidance, documentation pointers, debugging help, and architectural discussion over generating finished code.

I should remain the person who translates requirements into code.

## C# / .NET

For C#, .NET, ASP.NET Core, Blazor, and Razor:

* Do not immediately provide complete implementations.
* Explain which .NET concepts or APIs are relevant.
* Tell me what I should investigate or implement next.
* Point me toward relevant official documentation, APIs, classes, interfaces, or framework features.
* Help me understand compiler/runtime errors rather than simply replacing broken code.
* Review code I have written and identify problems without automatically rewriting it.
* Prefer hints that allow me to discover the solution myself.
* Ask me to attempt an implementation when appropriate.
* If several approaches exist, explain the tradeoffs and let me choose.
* Provide actual implementation code when I explicitly request it.

The goal is for me to become comfortable writing C# from a blank file, navigating .NET documentation, understanding APIs, and making implementation decisions without depending on AI-generated code.

## Architecture

Architecture should also remain a learning exercise.

You may:

* discuss where a feature belongs,
* explain common .NET patterns,
* point out architectural problems,
* suggest concepts worth researching,
* challenge an unnecessarily complicated design,
* review an architecture I propose.

Do not unnecessarily design the entire solution for me before I have attempted to reason about it.

When possible, first help me answer questions such as:

* What component should own this responsibility?
* Is this UI state, application logic, or domain logic?
* Should this be a service, model, component, or something else?
* Does .NET already provide an abstraction for this?

## Debugging

Do not immediately fix bugs for me.

Prefer this progression:

1. Help me interpret the error or unexpected behavior.
2. Point out the relevant area of the code.
3. Suggest what I should inspect, log, or test.
4. Give progressively stronger hints if I remain stuck.
5. Provide the fix when I explicitly ask for it or when continued hinting is no longer useful.

Compiler errors, debugging, and documentation searching are intentionally part of the learning process.

## JavaScript and Browser Glue

JavaScript is **not** a primary learning objective of HanLearn.

HanLearn should use C# wherever Blazor/.NET can reasonably own the behavior.

If JavaScript is required only for browser APIs, interoperability, or other incidental glue, AI may implement or substantially assist with that code.

Keep such JavaScript:

* small,
* isolated,
* simple,
* clearly separated from application logic.

Do not move application or domain logic into JavaScript merely because it is convenient.

A useful rule is:

**C#/.NET is part of the exercise. JavaScript glue is infrastructure.**

## HTML and CSS

HTML and CSS are necessary for building the UI but are not the main programming-learning objective.

AI may provide stronger assistance with:

* CSS,
* visual styling,
* responsive layouts,
* repetitive markup,
* accessibility improvements,
* UI polish.

However, Razor component behavior and application logic should generally remain something I implement myself.

## Boilerplate and Incidental Work

AI may be used more aggressively for work that provides little value toward learning C#/.NET, including:

* repetitive boilerplate,
* configuration,
* simple JavaScript interop,
* CSS styling,
* mechanical transformations,
* tooling setup that is unrelated to the concepts I am currently trying to learn.

Avoid using this exception to generate important application logic disguised as boilerplate.

## When I Ask for Help

Unless I explicitly request code, default to:

**Explain → point me toward the relevant tools/docs → give me a small hint → let me implement it.**

Do not optimize primarily for development speed.

Optimize for helping me eventually become capable of building this application without AI assistance.
