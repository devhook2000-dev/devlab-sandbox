# 008 — ISBN-10 to ISBN-13 conversion

## Objective
Add a conversion from ISBN-10 to ISBN-13 that reuses the existing ISBN-10 validation, covered by unit tests.

## Context
First task of the nightly queue (`tasks/queue.txt`). It builds on `DevlabSandbox.Core.Isbn10` from task 005, which must not change. Task 009 adds the inverse conversion to the same two files.

## Branch
`task/008-isbn10-to-isbn13`

## Files allowed
- `src/DevlabSandbox.Core/Isbn13Converter.cs`
- `tests/DevlabSandbox.Core.Tests/Isbn13ConverterTests.cs`

## Specification
- Public static class `DevlabSandbox.Core.Isbn13Converter` with `public static bool TryConvertFromIsbn10(string? isbn10, [NotNullWhen(true)] out string? isbn13)` (`System.Diagnostics.CodeAnalysis.NotNullWhenAttribute`).
- The input is validated with `Isbn10.IsValid` and nothing else: no trimming, no removal of spaces or hyphens, no case normalization. If it is not valid, the method returns `false` and sets `isbn13` to `null`.
- If it is valid, the method returns `true` and sets `isbn13` to the 13-digit string made of:
  - the prefix `978`;
  - the first 9 characters of the ISBN-10 (its check character is dropped);
  - the EAN-13 check digit computed on those 12 digits: weights 1 and 3 alternating from the left (1 for the first digit), check digit = (10 − (weighted sum mod 10)) mod 10.
- No exceptions for any input, `null` included.

## Acceptance criteria
- [ ] `dotnet build DevlabSandbox.sln -warnaserror` exits with code 0.
- [ ] `dotnet test DevlabSandbox.sln` exits with code 0, and the tests from tasks 001–005 still pass.
- [ ] The tests include at least these cases:
  - converted: `0306406152` → `9780306406157`; `097522980X` → `9780975229804`; `080442957X` → `9780804429573`; `0000000000` → `9780000000002`;
  - not converted (returns `false`, output `null`): `0306406153`, `097522980x`, `030640615`, `03064061522`, `0-306-40615-2`, `030640615A`, `X306406152`, `""`, `null`.
- [ ] `git diff --name-only main...HEAD` lists only the two files allowed.
- [ ] The pull request body is written to `.devlab/pr-body.md` as required by `CLAUDE.md`.

## Stop criteria
- Any requirement above is ambiguous or contradictory.
- Build or tests still failing after 3 fix attempts.
- A needed command is blocked.

## Out of scope
- ISBN-13 to ISBN-10 conversion (task 009), the `979` prefix, hyphenation, changes to `Isbn10`, `Gtin` or other validators, `CLAUDE.md` or `tasks/`.
