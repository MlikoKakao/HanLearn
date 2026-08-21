# HanLearn MVP Specification

## Problem Statement

Serious learners of Taiwan Mandarin currently assemble a fragmented study workflow. While reading, they may need separate tools for Traditional-character lookup, Zhuyin, Taiwan-specific vocabulary, example usage, and spaced-repetition review. Mainstream resources often prioritize Mainland Chinese conventions and Pinyin, leaving Zhuyin treated as an obstacle instead of a useful default.

The initial problem to solve is to let a committed beginner or advanced beginner study short, practical Taiwan-Mandarin dialogues in one focused laptop-first experience, then review the vocabulary encountered without manually building a separate flashcard workflow.

## Solution

HanLearn will be a hosted, responsive C# web application. It will teach Taiwan Mandarin with Traditional characters and Zhuyin as the default. The first module will combine a small Zhuyin foundation, short everyday dialogues, contextual word lookup, curated teaching notes, and FSRS-scheduled reviews.

Learners choose whether to begin with Zhuyin or proceed directly to reading. While reading, they can hover or select a word to see a concise Taiwan-aware explanation, Zhuyin, development audio, and relevant context. They can drill into individual characters without sacrificing word-level meaning. At the end of a dialogue, HanLearn recommends a small editable review set. Learners retain control over what enters SRS and how cards are rated.

The same lookup and review flow will also support private pasted plain text as a lightweight personal reader.

## User Stories

1. As a serious English-speaking Mandarin learner, I want the application to teach Taiwan Mandarin, so that the language matches the place and communities I am learning for.
2. As a learner, I want Traditional characters and Zhuyin to be the default notation, so that I build the habits used in Taiwan.
3. As a learner who needs a transition aid, I want to opt into Pinyin without making it the default, so that I can use assistance without changing the teaching approach for everyone.
4. As an absolute beginner, I want to choose a Zhuyin-starting path, so that I can reach the reading material without being assumed to know Pinyin.
5. As a learner who already reads Zhuyin, I want to proceed directly to dialogue material, so that I do not repeat a foundation I already have.
6. As a learner, I want an optional lightweight self-check when choosing a path, so that I can confirm my own starting point without taking a formal placement test.
7. As a learner, I want short everyday dialogues, so that I can study useful Taiwan Mandarin in focused sessions.
8. As a learner, I want the first module to cover connected daily-life situations, so that vocabulary and grammar build into practical reading ability.
9. As a learner, I want a word-level gloss when I hover or select text, so that I can keep reading without opening a separate dictionary tab.
10. As a learner, I want each gloss to show a concise contextual meaning and Zhuyin, so that I understand the word used in that exact dialogue.
11. As a learner, I want access to audio for story and key-vocabulary material, so that I can connect written Taiwan Mandarin with pronunciation.
12. As a learner, I want to see concise notes on high-value grammar, register, and Taiwan usage, so that I do not need to infer important patterns alone.
13. As a learner, I want to inspect characters within a word, so that I can learn individual character meanings, readings, and later writing information without receiving misleading character-by-character word definitions.
14. As a learner, I want to add an unfamiliar word or selected character to review from the reader, so that useful discoveries immediately enter my study workflow.
15. As a learner finishing a dialogue, I want HanLearn to recommend its essential vocabulary for review, so that I do not miss the intended learning targets.
16. As a learner, I want to edit the recommended review set, so that my SRS workload remains mine to control.
17. As a learner, I want reviews scheduled with a proven FSRS model, so that my effort is directed toward material I am likely to forget.
18. As a learner, I want to attempt recall before seeing an answer, so that review measures memory rather than recognition alone.
19. As a learner, I want to self-rate reviews using a simple interface, so that I can follow a low-friction Anki-like workflow.
20. As a learner, I want writing by hand with Zhuyin to remain an optional personal recall strategy, so that the application does not force typed production on every card.
21. As a learner, I want a review card to link back to the dialogue sentence where I met the item, so that I can recover context when needed.
22. As a learner, I want to see completed readings, learned words, active learning items, and review workload, so that I understand my progress without points, streaks, or artificial pressure.
23. As a learner, I want an on-screen Zhuyin keyboard/reference, so that I can start producing Zhuyin without immediately mastering an operating-system input method.
24. As a learner, I want the application to recommend my system Zhuyin IME when appropriate, so that I can use the mature input method on my device.
25. As a learner, I want to paste private plain Chinese or mixed Chinese/English text into a reader, so that I can use the same tools with my own material.
26. As a learner, I want pasted text to remain local by default, so that private messages or work material are not retained by the service.
27. As a learner using pasted text, I want to manually add selected vocabulary to review, so that the personal reader does not automatically create unwanted cards.
28. As a learner, I want my progress to survive browser refreshes, so that I can continue studying without an account.
29. As a learner, I want to export and import my study data, so that I can back it up or move it without being locked into the service.
30. As a laptop-focused learner, I want a clear responsive interface that is also usable on a phone, so that focused study works well while light access remains available on mobile.
31. As a keyboard user, I want core reading and review interactions to be keyboard accessible, so that I can study efficiently without relying entirely on a pointer.
32. As a learner, I want adjustable readable type and status indicators that do not rely only on colour, so that the reading environment remains accessible.
33. As a learner, I want a clear feedback link from each lesson, so that I can report language errors or confusing content with sufficient context.
34. As a contributor, I want version-controlled lesson content and clear asset licences, so that I can propose improvements responsibly.
35. As a contributor, I want unreviewed lessons clearly labelled, so that I do not mistake community drafts for stable teaching material.
36. As a project maintainer, I want final editorial control over learner-facing content, so that Taiwan language accuracy and tone remain accountable.
37. As a project maintainer, I want minimal anonymized, opt-out analytics for lesson completion, review actions, and errors, so that I can improve the workflow without collecting pasted text.

## Implementation Decisions

- The product is a hosted website with a low barrier to entry, not a native desktop or mobile application. It is laptop-first and responsive rather than designed around casual mobile use.
- The implementation will use ASP.NET Core and Blazor WebAssembly. The learner interface will run primarily in the browser; backend services remain small and are introduced only where hosted functionality requires them.
- The initial product is English-interface only. Interface copy and lesson metadata must be structured so future localization remains possible.
- Curated learning material is version-controlled structured content. A content-management system is not part of the MVP.
- The first publishable module contains 6–10 connected short daily-life dialogues, with a smaller first vertical slice consisting of one Zhuyin mini-lesson, one dialogue, vocabulary selection, scheduled review, and progress export/import.
- Dialogues use word-first segmentation. A word card is the primary lookup unit; a user can then open character details from that card.
- Curated dialogue vocabulary includes human-authored Taiwan-aware glosses, Zhuyin, contextual examples or notes, and development audio. General automatic lookup is reserved for pasted personal text.
- Development audio may be synthetic. Human-recorded Taiwan Mandarin audio is a later quality upgrade and must not be presented as an equivalent authority.
- Zhuyin is shown by default throughout guided learning. Pinyin is an explicit future opt-in setting.
- The SRS module uses FSRS and maintains transparent review history. Card interactions support recall-before-reveal and learner self-rating. The product does not require typing answers for every review.
- At lesson completion, the application recommends an editable set of essential vocabulary. Incidental words found during reading are added only through learner action.
- Learner progress is stored locally in browser storage for the MVP. Accounts and cross-device sync are deferred. Import/export is a supported first-class escape hatch.
- The personal-text reader accepts pasted plain text only, including limited mixed Chinese/English text. Text remains local by default and is not silently sent to a server, analytics provider, or AI provider.
- Initial hosted analytics are minimal, anonymized, explicitly described, and opt-out. They cover aggregate lesson completion, review actions, and errors; they never capture pasted text.
- Initial feedback and correction handling use GitHub Issues or Discussions. In-app links include the relevant lesson identifier rather than requiring a separate feedback service.
- The application will be keyboard navigable, responsive, use adjustable readable typography, and avoid colour-only state indicators. Screen-reader support will be improved incrementally without blocking its structure.
- Stroke order is planned only as a future visual character reference. Handwriting tracing, scoring, and feedback are separate future products.
- Future AI is optional and provider/plugin based. It must be explicitly enabled, clearly disclose any text leaving the device, and never be necessary for core study.
- Code will use the MIT License. Authored learning content and reusable lesson assets will use CC BY-SA 4.0 unless an asset requires a compatible alternative. Every externally sourced asset needs a source and licence record.
- The project welcomes public code and content contributions, while the maintainer retains final editorial authority. Content awaiting review must state that status clearly.

## Testing Decisions

- The primary acceptance-test seam is the complete learner workflow: choose a path, study a dialogue, look up a word, select vocabulary, complete a review, refresh or reopen, and recover preserved progress. Tests should observe this public behavior rather than component internals.
- The initial repository has no existing tests or prior-art test suite. New tests should establish one high-level browser-based workflow seam before adding lower-level tests.
- Lesson-content validation tests should verify externally meaningful authoring constraints: valid word segmentation references, required curated fields, valid asset/source metadata, and links from vocabulary to relevant dialogue context.
- SRS tests should verify observable scheduling outcomes for representative self-ratings and due-card selection. They should not couple tests to private implementation details of an FSRS library.
- Browser-storage tests should verify that progress persists across reload, and that exported data can be imported to recover the same learner state.
- Personal-reader tests should verify that pasted text is annotated locally, selected items can be added manually to SRS, and the text is excluded from analytics/network payloads by default.
- Accessibility tests should cover keyboard completion of the primary learner workflow, readable responsive layout at common viewport sizes, and non-colour-only status communication.

## Out of Scope

- Required accounts, login, cross-device synchronization, and social/accountability features.
- Native desktop and mobile applications.
- Gamification such as points, streaks, rewards, or pressure-based engagement loops.
- Human-recorded audio as a launch requirement.
- Stroke tracing, handwriting recognition, writing scoring, and forced typed production reviews.
- AI chat, hosted AI, automatic AI content generation, or AI dependency in core learning.
- PDF, EPUB, URL, OCR, browser-extension, and rich-document imports.
- A full general-purpose dictionary or a replacement for Pleco-style exhaustive reference tools.
- A CMS, formal placement examination, full screen-reader optimization, or broad interface localization in the first module.
- A full TOCFL course, broad narrative/story library, news, songs, or interest-specific paths; TOCFL guides progression but does not define the MVP scope.

## Further Notes

- The product is deliberately for focused learners, including its creator, rather than optimized for the largest casual-learning market.
- Initial material may be authored and published before professional review, but its review status must be unambiguous and corrections should be welcome.
- The success signal is first personal ease of use, then a small group of serious learners completing the first module, returning for reviews across two weeks, and reporting reduced tool-switching while reading.
- The next implementation decision after this specification should be the concrete lesson-content schema, because it anchors the reader, lookup, audio, review recommendations, and content validation.
