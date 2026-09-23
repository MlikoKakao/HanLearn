# HanLearn Content Model Notes

This document records the current direction for representing stories, vocabulary, characters, and review items. It is a design note rather than a finalized schema.

## Story structure

A story is a combination of dialogue or narrative lines, sentences, words, and characters. Story content should not be stored only as an unstructured string.

Conceptually, the structure is:

```text
Story
  └─ Lines or sentences
       └─ Segments
            ├─ Displayed text
            └─ Contextual vocabulary-meaning ID
```

When the learner selects a story, the client should load the meanings referenced by that story. Each selectable segment should reference a specific contextual meaning by a stable ID rather than finding content by matching its displayed text.

## Word and character information

Story writers should define the contextual meaning of each word used in a story. Word meanings must not be inferred by combining the meanings of their individual characters, because a multi-character word may have a meaning that cannot be reconstructed character by character.

Character information should be a separate, reusable reference layer:

```text
Vocabulary meaning
  ├─ Written form
  ├─ Contextual definition
  ├─ Pronunciation
  └─ Constituent character IDs

Character
  ├─ Written character
  └─ Possible readings and meanings
```

For example, a story segment containing `你好` should reference the relevant word meaning for `你好`. The learner may then inspect `你` and `好` through their separate character records.

Characters or words with multiple meanings require distinct meaning records. A story segment references the meaning intended in that particular context.

## Reviewable items

HanLearn must support reviewing both individual characters and words. Whether something is a character or a word is a semantic distinction and must not be inferred from the length of its written form.

For example, `好` can be studied either as:

- A character, including its character-level readings and meanings.
- A one-character word with a particular meaning in a sentence.

A reviewable item therefore needs an explicit type, initially represented by an enum with values such as:

- `Character`
- `Word`

A reviewable item needs the following concepts:

- A stable ID.
- Its explicit item type.
- Its written form.
- Pronunciation.
- Meaning.
- A reference to the source story and segment where it was encountered.
- Optional example and audio information.

The authored content model uses `VocabularyEntry` with a neutral `WrittenForm` property because the value may be either a word or a character.

The model should work for at least these examples:

- `你` as a character.
- `好` as either a character or a one-character word.
- `你好` as a multi-character word.
- `銀行` as a multi-character word whose meaning is not derived solely from displaying its characters separately.

## Authored content and learner state

Authored educational content and learner-specific state have different responsibilities.

Authored content describes the written form, pronunciation, contextual meaning, examples, and source context. `VocabularyReviewState` describes the learner's relationship with a reviewable item, such as:

- When the learner added it.
- Review count.
- Last rating.
- Future scheduling information.

`VocabularyReviewState` should reference a vocabulary entry by ID rather than duplicate its authored educational content.

An additional design decision remains: determine whether one vocabulary meaning may generate multiple study-card directions, such as Chinese-to-meaning and meaning-to-Chinese. If multiple card directions are required, vocabulary entries and study cards should be separate concepts. Otherwise, they may remain combined for the first vertical slice.

## Authoring workflow

The eventual authoring workflow should provide a user-friendly way for story writers to create stories, segment their text, select the intended meanings, and add new meanings where required.

Building that UI should wait until the content schema has been exercised with at least two or three real stories. For the initial vertical slice, content can be authored manually as structured, version-controlled data, such as JSON. This follows the MVP decision that a content-management system is outside the initial scope.

## Next modeling exercise

Before implementing an authoring UI or database, model one realistic story in memory:

1. Choose a short story containing at least one multi-character word.
2. Include a character or word that has multiple possible meanings.
3. Identify every entity and stable ID involved.
4. Ensure every selectable story segment references exactly one contextual meaning.
5. Ensure character information can be opened without presenting it as the definition of the complete word.
6. Decide how a reviewable item links back to the story segment where the learner encountered it.
7. Decide whether vocabulary entries and generated study cards need to be separate concepts.

Recommended vocabulary selection for a lesson has not yet been designed.
