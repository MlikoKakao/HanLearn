# HanLearn — AI-Assisted Development Policy

HanLearn is both a real product and a place to improve engineering judgment. AI-assisted implementation is accepted and encouraged when it helps deliver a useful, maintainable application. The owner remains accountable for product direction, consequential technical decisions, and validation of the result.

## Default behavior

- For implementation requests, inspect the code, make a short plan when useful, and implement. Do not require the owner to write the code first.
- AI may write C#, Razor, UI, tests, configuration, and documentation.
- Make routine, reversible implementation decisions without interrupting progress.
- Ask before making a consequential product or architecture choice when the requirement does not already determine it.
- Prefer simple solutions and small, working increments. Follow `MVP-SPEC.md` and `CONTENT-MODEL.md`.

## Engineering ownership

- Surface the contract, ownership, important trade-offs, failure cases, and verification strategy for nontrivial work.
- Present meaningful alternatives before implementation when choosing one would materially affect the product, data model, architecture, security, or compatibility.
- After substantial implementation, explain the consequential choices and assumptions so the owner can review and challenge them.
- The owner should be able to explain and maintain accepted code, but does not need to author every line personally.
- When asked to coach, review, or give hints, follow that mode instead of implementing.

## Quality

- Inspect existing code before changing it; avoid unnecessary abstractions.
- Run relevant checks when possible, and state what was actually verified.
- Protect local learner data; do not send private text to external services without approval.
- Distinguish required work from reasonable follow-up and unnecessary complexity.
- After substantial changes, state what changed, what was tested, what remains uncertain, and any decision the owner should revisit.

## Learning without blocking delivery

- Explain reusable principles and unfamiliar APIs where they matter; avoid line-by-line tutorials unless requested.
- When reviewing code, separate correctness defects from maintainability suggestions and style preferences.
- Offer focused exercises only when useful, never as a prerequisite for completing requested work.
- If generated code appears uncertain or depends on an unverified API, say so explicitly rather than presenting it as settled.
