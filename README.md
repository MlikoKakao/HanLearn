# HanLearn

HanLearn is an early-stage web application for learning Taiwan Mandarin through Traditional-character reading, Zhuyin, contextual vocabulary lookup, and spaced-repetition review.

The repository currently contains a working development slice: one JSON-authored reading, word- and character-level lookup, learner-selected vocabulary cards, FSRS scheduling, browser-local review persistence, and review backup export/import. The Zhuyin area currently provides a keyboard-shaped audio reference and a small six-symbol cycling exercise; it is not yet a complete lesson.

The product direction is broader than the current implementation. See [MVP-SPEC.md](MVP-SPEC.md) for the product agreement and [PROJECT-CONTEXT.md](PROJECT-CONTEXT.md) for an implementation-status handoff.

## Technology

- .NET 10
- ASP.NET Core Blazor Web App
- Interactive Blazor WebAssembly client
- [FSRS.Core](https://www.nuget.org/packages/FSRS.Core) for review scheduling

The solution contains:

- `src/HanLearn.Web` — ASP.NET Core host and shared static assets.
- `src/HanLearn.Web.Client` — WebAssembly UI, client-side services, data models, and authored lesson content.

## Run locally

Install the .NET 10 SDK, then run from the repository root:

```bash
dotnet run --project src/HanLearn.Web
```

Open the local URL printed by the application. To build without running:

```bash
dotnet build HanLearn.slnx
```

There is currently no automated test project.

## Current routes

- `/` — minimal home page.
- `/zhuyin` — Zhuyin keyboard/audio reference.
- `/zhuyinTest` — basic symbol-cycling exercise.
- `/dialogues` — available reading list.
- `/dialogues/breakfast-shop-morning` — the first authored reading.
- `/review` — due-card review and review-data backup/restore.
- `/author/stories` — placeholder only; no authoring workflow is implemented.

## Lesson content

Authored content is stored as versioned JSON under `src/HanLearn.Web.Client/wwwroot/content`. On client startup, `LessonPackageLoader` deserializes and validates `breakfast-shop-morning.json` before adding its story, lexical senses, and Hanzi records to in-memory catalogs.

The validator currently checks schema version, required values, duplicate IDs, story reconstruction, and internal references. Invalid content prevents client startup. Although packages contain recommended lexical-sense IDs and optional audio filenames, recommendations and story/vocabulary audio are not yet connected to the UI.

See [CONTENT-MODEL.md](CONTENT-MODEL.md) for the implemented schema and remaining model decisions.

## Learner data

Review state is stored in browser `localStorage` under `hanlearn.vocabulary-review-states`. The Review page can export that state as JSON and replace it from a JSON backup of up to 500 KB. There are no accounts, server-side learner records, or cross-device synchronization.

## Contributing

Contributions and corrections are welcome. Learning content is version-controlled, and the maintainer retains final editorial authority for learner-facing material. Unreviewed lessons must be clearly labelled.

Include source and licence information for every externally sourced asset.

## Licence

The intended licensing is MIT for source code and CC BY-SA 4.0 for authored learning content and reusable lesson assets, unless an asset records a compatible alternative. A repository `LICENSE` file has not yet been added.
