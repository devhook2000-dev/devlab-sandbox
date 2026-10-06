# 004 — GTIN check digit validator

## Objective
Add a validator for GTIN codes (EAN-8, UPC-A, EAN-13, GTIN-14) based on the GS1 check digit, covered by unit tests.

## Context
Fourth task of the DevLab agentic workflow. Same conventions as tasks 001–003 (`ItalianVatNumber`, `ItalianFiscalCode`, `ItalianIban`).

## Branch
`task/004-gtin-validator`

## Files allowed
- `src/DevlabSandbox.Core/Gtin.cs`
- `tests/DevlabSandbox.Core.Tests/GtinTests.cs`

## Specification
- Public static class `DevlabSandbox.Core.Gtin` with `public static bool IsValid(string? value)`.
- No trimming, no removal of spaces or hyphens: the value is validated exactly as given.
- A value is valid only if:
  - its length is exactly 8, 12, 13 or 14;
  - every character is an ASCII digit (`0`–`9`);
  - the last digit equals the GS1 check digit computed from the other digits.
- GS1 check digit: starting from the rightmost digit before the check digit and moving left, multiply digits alternately by 3 and 1 (the rightmost one by 3); sum the products; check digit = (10 − (sum mod 10)) mod 10.
- Do not interpret prefixes (country, company, packaging indicator): only length, alphabet and check digit.

## Acceptance criteria
- [ ] `dotnet build DevlabSandbox.sln -warnaserror` exits with code 0.
- [ ] `dotnet test DevlabSandbox.sln` exits with code 0, and the tests from tasks 001–003 still pass.
- [ ] The tests include at least these cases:
  - valid: `4006381333931` (EAN-13), `5901234123457` (EAN-13), `96385074` (EAN-8), `036000291452` (UPC-A), `10012345678902` (GTIN-14);
  - invalid, wrong check digit: `4006381333932`, `96385075`, `036000291453`, `8051234567893`;
  - invalid, other: `400638133393` (12 digits, wrong check digit), `40063813339311` (14 digits, wrong check digit), `1234567` (7 digits), `4006381333 931` (space), `400638133393A` (letter), `""` (empty), `null`.
- [ ] A ready-for-review (not draft) pull request is open from `task/004-gtin-validator` to `main`, with the body required by `CLAUDE.md`.

## Stop criteria
- Any requirement above is ambiguous or contradictory.
- Build or tests still failing after 3 fix attempts.
- A needed command is blocked by permissions.

## Out of scope
- GTIN prefix interpretation, ISBN/ISSN, check digit generation helpers, changes to other validators, `CLAUDE.md` or `tasks/`.
