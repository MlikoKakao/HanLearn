# HanLearn

HanLearn is a focused web application for serious learners of Taiwan Mandarin. It brings Traditional-character reading, Zhuyin-first support, contextual vocabulary lookup, and spaced-repetition review into one workflow.

The current MVP begins with short, practical dialogues and a small Zhuyin foundation. It is deliberately laptop-first and responsive, with a clear English interface. Pinyin is planned as an opt-in aid rather than the default notation.

## Technology

- ASP.NET Core and Blazor WebAssembly
- .NET 10
- C#

The solution has two projects:

- `src/HanLearn.Web` — ASP.NET Core host application.
- `src/HanLearn.Web.Client` — interactive Blazor WebAssembly user interface.

## Run locally

Install the .NET 10 SDK, then from the repository root run:

```bash
dotnet run --project src/HanLearn.Web
```

Use the local URL printed by the application. To build the full solution without running it:

```bash
dotnet build HanLearn.slnx
```

## Project direction

HanLearn teaches Taiwan Mandarin with Traditional characters and Zhuyin by default. The intended first vertical slice includes one Zhuyin mini-lesson, one dialogue, contextual word lookup, learner-selected vocabulary review, and local progress export/import.

The full product agreement is in [MVP-SPEC.md](MVP-SPEC.md).

## Contributing

Contributions and corrections are welcome. Learning content is version-controlled, and the maintainer retains final editorial authority for learner-facing material. Unreviewed lessons must be clearly labelled.

Please include source and licence information for every externally sourced asset.

## Licence

Source code is licensed under the [MIT License](LICENSE).

Authored learning content and reusable lesson assets are licensed under [CC BY-SA 4.0](https://creativecommons.org/licenses/by-sa/4.0/), unless a specific asset records a compatible alternative licence. Third-party assets retain their own licences.
