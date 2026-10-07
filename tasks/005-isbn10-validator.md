# 005 — ISBN-10 validator

## Objective
Add a validator for ISBN-10 codes based on the mod 11 check character, covered by unit tests.

## Context
Fifth task of the DevLab agentic workflow and the first one executed by the orchestrator in a container. Same conventions as tasks 001–004.

## Branch
`task/005-isbn10-validator`

## Files allowed
- `src/DevlabSandbox.Core/Isbn10.cs`
- `tests/DevlabSandbox.Core.Tests/Isbn10Tests.cs`

## Specification
- Public static class `DevlabSandbox.Core.Isbn10` with `public static bool IsValid(string? value)`.
- No trimming, no removal of spaces or hyphens, no case normalization: the value is validated exactly as given.
- A value is valid only if:
  - it is exactly 10 characters long;
  - characters 1–9 are ASCII digits (`0`–`9`);
  - character 10 is an ASCII digit or uppercase `X` (worth 10);
  - the weighted sum of the 10 values, with weights 10, 9, …, 1 from left to right, is divisible by 11.
- Do not convert to or validate ISBN-13, and do not interpret group or publisher prefixes.

## Acceptance criteria
- [ ] `dotnet build DevlabSandbox.sln -warnaserror` exits with code 0.
- [ ] `dotnet test DevlabSandbox.sln` exits with code 0, and the tests from tasks 001–004 still pass.
- [ ] The tests include at least these cases:
  - valid: `0306406152`, `097522980X`, `080442957X`, `0000000000`;
  - invalid, wrong check character: `0306406153`;
  - invalid, other: `097522980x` (lowercase x), `030640615` (9 characters), `03064061522` (11 characters), `0-306-40615-2` (hyphens), `030640615A` (letter other than X), `X306406152` (X not in last position), `""` (empty), `null`.
- [ ] The pull request body is written to `.devlab/pr-body.md` as required by `CLAUDE.md`.

## Stop criteria
- Any requirement above is ambiguous or contradictory.
- Build or tests still failing after 3 fix attempts.
- A needed command is blocked.

## Out of scope
- ISBN-13, hyphenation, check character generation, changes to other validators, `CLAUDE.md` or `tasks/`.
