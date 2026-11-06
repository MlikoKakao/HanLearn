# HanLearn

HanLearn is a focused web application for serious learners of Taiwan Mandarin. It brings Traditional-character reading, Zhuyin-first support, contextual vocabulary lookup, and spaced-repetition review into one workflow.

The repository currently contains one structured reading with contextual word and character lookup, learner-selected vocabulary review, FSRS scheduling, browser-local persistence, and review backup export/import. The Zhuyin area is currently an audio keyboard reference plus a small symbol-cycling exercise, not yet a complete lesson.

## Technology

- ASP.NET Core and Blazor WebAssembly
- .NET 10
- C#
- FSRS.Core for review scheduling

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

## Current functionality

- `/dialogues` lists the loaded lesson, and `/dialogues/breakfast-shop-morning` renders it as selectable occurrences.
- Hovering, focusing, or clicking a word opens its contextual lexical sense; character buttons expose reusable Hanzi readings and meanings.
- A lexical sense can be added to review once. Due cards use recall-before-reveal and Again, Hard, Good, and Easy ratings.
- Review state is stored in browser `localStorage` and can be exported or replaced from a JSON backup.
- `/zhuyin` presents the keyboard/audio reference and read-only symbol-family examples, `/zhuyin/info` explains the Zhuyin-first approach, and `/zhuyinTest` currently cycles through six symbols.

The home page and `/author/stories` route remain placeholders. Recommended lesson vocabulary, story and vocabulary audio, lesson completion, personal pasted text, Pinyin settings, progress reporting, accounts, synchronization, analytics, and automated tests are not implemented.

## Lesson content

Authored lessons are JSON packages under
`src/HanLearn.Web.Client/wwwroot/content`. A package contains one story, its
lexical senses, the referenced Hanzi character records, recommended review
items, and a schema version. The WebAssembly client validates the complete
package before adding any of its content to the runtime catalogs. The current
client loads `breakfast-shop-morning.json` at startup. Recommended lexical-sense
IDs and optional audio filenames are represented and validated but are not yet
used by the interface.

See [CONTENT-MODEL.md](CONTENT-MODEL.md) for implemented constraints and future model decisions.

## Contributing

Contributions and corrections are welcome. Learning content is version-controlled, and the maintainer retains final editorial authority for learner-facing material. Unreviewed lessons must be clearly labelled.

Please include source and licence information for every externally sourced asset.

## Licence

Source code is licensed under the [MIT License](LICENSE).

Authored learning content and reusable lesson assets are licensed under [CC BY-SA 4.0](https://creativecommons.org/licenses/by-sa/4.0/), unless a specific asset records a compatible alternative licence. Third-party assets retain their own licences.

## AI-assisted development

HanLearn is developed with AI assistance while retaining human ownership of product requirements, architecture, technical trade-offs, and validation. AI can implement features and tests; the maintainer reviews consequential decisions and verifies behavior. See [AGENTS.md](AGENTS.md) for collaboration rules and [PROJECT-CONTEXT.md](PROJECT-CONTEXT.md) for the current handoff.
