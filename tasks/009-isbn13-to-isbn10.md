# 009 — ISBN-13 to ISBN-10 conversion

## Objective
Add the inverse conversion, from ISBN-13 to ISBN-10, to the converter of task 008, covered by unit tests.

## Context
Second task of the nightly queue (`tasks/queue.txt`, `after=008`): it starts only when the pull request of task 008 is merged, so `Isbn13Converter` and its tests already exist on `main`. Only ISBN-13 codes with the `978` prefix have an ISBN-10 equivalent.

## Branch
`task/009-isbn13-to-isbn10`

## Files allowed
- `src/DevlabSandbox.Core/Isbn13Converter.cs`
- `tests/DevlabSandbox.Core.Tests/Isbn13ConverterTests.cs`

## Specification
- Add to `DevlabSandbox.Core.Isbn13Converter` the method `public static bool TryConvertToIsbn10(string? isbn13, [NotNullWhen(true)] out string? isbn10)`.
- No trimming, no removal of spaces or hyphens: the value is checked exactly as given. It is convertible only if:
  - it is exactly 13 characters long, all ASCII digits (`0`–`9`);
  - its last digit is the correct EAN-13 check digit (weights 1 and 3 alternating from the left, check digit = (10 − (weighted sum of the first 12 digits mod 10)) mod 10); `Gtin.IsValid` may be reused for this, after the length check;
  - it starts with `978`.
- If it is convertible, the method returns `true` and sets `isbn10` to characters 4–12 of the ISBN-13 followed by the ISBN-10 check character computed on those 9 digits: weights 10, 9, …, 2 from the left, value = (11 − (weighted sum mod 11)) mod 11, written as a digit, or as uppercase `X` when it is 10. Otherwise it returns `false` and sets `isbn10` to `null`.
- The result must satisfy `Isbn10.IsValid`. No exceptions for any input, `null` included.
- `TryConvertFromIsbn10` and its existing tests must not change.

## Acceptance criteria
- [ ] `dotnet build DevlabSandbox.sln -warnaserror` exits with code 0.
- [ ] `dotnet test DevlabSandbox.sln` exits with code 0, and the tests from tasks 001–005 and 008 still pass.
- [ ] The tests include at least these cases:
  - converted: `9780306406157` → `0306406152`; `9780975229804` → `097522980X`; `9780804429573` → `080442957X`; `9780000000002` → `0000000000`;
  - not converted (returns `false`, output `null`): `9791000000008` (valid ISBN-13 with the `979` prefix), `9780306406158` (wrong check digit), `978030640615` (12 digits), `null`, `""`.
- [ ] `git diff --name-only main...HEAD` lists only the two files allowed.
- [ ] The pull request body is written to `.devlab/pr-body.md` as required by `CLAUDE.md`.

## Stop criteria
- `Isbn13Converter` or its tests from task 008 are missing on `main`.
- Any requirement above is ambiguous or contradictory.
- Build or tests still failing after 3 fix attempts.
- A needed command is blocked.

## Out of scope
- The `979` prefix (no ISBN-10 equivalent), hyphenation, changes to `TryConvertFromIsbn10`, `Isbn10`, `Gtin` or other validators, `CLAUDE.md` or `tasks/`.
