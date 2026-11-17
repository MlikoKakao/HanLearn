# HanLearn Content Model Notes

This document records both the implemented version 2 lesson-package model and future model decisions. Authored lesson content uses versioned JSON `LessonPackage` files under `src/HanLearn.Web/Content/Lessons`.

## Implemented package model

A version 2 `LessonPackage` contains one `Story`, the `LexicalSense` records used by its occurrences, the referenced `HanziCharacter` records, and recommended lexical-sense IDs. A story includes selection-page metadata: a summary, difficulty label, estimated reading time in minutes, and image path.

At host startup, `PublishedLessonCatalog` strictly deserializes all JSON packages in `Content/Lessons`, relative to the host content root. `LessonPackageCatalog` validates each complete package and checks it against already-loaded records. Invalid content or a missing directory prevents startup; an existing empty directory is allowed. Only a completely validated catalog is published in memory. Files are included in build and publish output, and changes require a restart or redeployment.

`GET /api/lessons` returns lesson summaries (ID, title, summary, difficulty, estimated reading time, and image path). `GET /api/lessons/{id}` returns the full version 2 package or a JSON `404`. IDs are looked up in the catalog and are not used as filesystem paths.

At WebAssembly startup, `LessonPackageLoader` discovers the published lessons through the list endpoint and downloads their packages. It uses the same strict deserialization and `LessonPackageCatalog` validation rules to populate the browser's story and lexicon catalogs. Content models and validation remain in the client assembly, which is already referenced by the host. There is currently no content database or working authoring UI.

Validation currently checks:

- Schema version 2 and required package collections.
- Required story summary, difficulty, and image path, plus a positive estimated reading time.
- Required IDs and core text fields.
- Duplicate occurrence, lexical-sense, Hanzi-character, and recommendation IDs.
- Exact reconstruction of `Story.Text` from ordered occurrence text.
- Required lexical-sense pronunciations and character references.
- Required character readings and meanings.
- Occurrence-to-sense, sense-to-character, and recommendation-to-sense references.
- Conflicts with reusable senses or characters already in the catalogs, and duplicate story IDs.

## Terminology

- A `StoryOccurrence` is one particular piece of text in a story. It may point to a lexical sense.
- A `LexicalSense` is one specific meaning and pronunciation of a written expression.
- A `HanziCharacter` contains character-level reference information, not a word's contextual definition.
- “Vocabulary” remains a learner-facing umbrella term rather than the name of a single data type.

## Story structure

A story is a combination of dialogue or narrative lines, sentences, words, and characters. Story content should not be stored only as an unstructured string.

Conceptually, the structure is:

```text
Story
  └─ Lines or sentences
       └─ Occurrences
            ├─ Displayed text
            └─ Lexical-sense ID
```

The client currently loads the story and its senses together at application startup. Each selectable occurrence references a specific lexical sense by a stable ID rather than finding content by matching its displayed text. Loading content only when a story is selected is not implemented.

## Word and character information

Story writers should define the contextual meaning of each word used in a story. Word meanings must not be inferred by combining the meanings of their individual characters, because a multi-character word may have a meaning that cannot be reconstructed character by character.

Character information should be a separate, reusable reference layer:

```text
Lexical sense
  ├─ Written form
  ├─ Contextual definition
  ├─ Pronunciation
  └─ Constituent character IDs

Character
  ├─ Written character
  └─ Possible readings and meanings
```

For example, a story occurrence containing `你好` should reference the relevant lexical sense for `你好`. The learner may then inspect `你` and `好` through their separate character records.

Written expressions with multiple meanings require distinct lexical-sense records. A story occurrence references the sense intended in that particular context.

## Reader interaction

The reader uses word-first lookup:

```text
Story word
  └─ Word popover
       ├─ Contextual meaning and pronunciation
       └─ Constituent characters
            └─ Character popover
                 ├─ Character readings
                 └─ Character meanings
```

Hovering, focusing, or selecting a story word opens its word popover. The learner must be able to move the pointer from the word into the popover without closing it. Hovering, focusing, or selecting a character inside that popover opens a nested character popover.

The word and character information remain separate: the character popover supplements the contextual word definition and must not imply that a multi-character word's meaning is the sum of its characters. Repeated characters retain their positions in the word but may reference the same reusable character record.

Hover is an enhancement, not the only interaction. The same information must be usable with keyboard focus and on devices that require selection rather than hover.

## Reviewable items

HanLearn must support reviewing both individual characters and words. Whether something is a character or a word is a semantic distinction and must not be inferred from the length of its written form.

For example, `好` can be studied either as:

- A character, including its character-level readings and meanings.
- A one-character word with a particular meaning in a sentence.

A reviewable item must explicitly distinguish a character from a word. This distinction may be represented by separate types or by a type discriminator; it must not be inferred from the written form's length.

The target reviewable-item model needs the following concepts:

- A stable ID.
- Its explicit item type.
- Its written form.
- Pronunciation.
- Meaning.
- A reference to the source story and occurrence where it was encountered.
- Optional example and audio information.

The model should work for at least these examples:

- `你` as a character.
- `好` as either a character or a one-character word.
- `你好` as a multi-character word.
- `銀行` as a multi-character word whose meaning is not derived solely from displaying its characters separately.

## Authored content and learner state

Authored educational content and learner-specific state have different responsibilities.

Authored content describes the written form, pronunciation, lexical sense, examples, and source context. `LexicalSenseReviewState` describes the learner's relationship with a reviewable lexical sense, such as:

- When the learner added it.
- Review count.
- Last rating.
- Future scheduling information.

Learner review state should reference the ID of its authored lexical sense rather than duplicate the authored educational content.

The current implementation supports lexical-sense review only. `LexicalSenseReviewState` stores a lexical-sense ID and an FSRS card, which are persisted through storage-specific DTOs in browser local storage and JSON backups. Character review, source occurrence/context, review history, and a versioned backup envelope are not implemented. Recommended lexical-sense IDs are retained per story in `StoryCatalog` and populate the editable checklist below the dialogue. Selected new words are saved together; words already in review keep their existing cards and schedules.

## Authoring workflow

The eventual authoring workflow should provide a user-friendly way for story writers to create stories, segment their text, select the intended meanings, and add new meanings where required.

Building that UI should wait until the content schema has been exercised with at least two or three real stories. Content is currently authored manually as structured, version-controlled JSON. The `/author/stories` route is a textarea placeholder, not an authoring workflow. This follows the MVP decision that a content-management system is outside the initial scope.
