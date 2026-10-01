# 001 — Italian VAT number validator

## Objective
Create the .NET solution skeleton and implement a validator for Italian VAT numbers (partita IVA), covered by unit tests.

## Context
First task of the DevLab agentic workflow: it exercises the task file → branch → pull request cycle. Keep the change small and focused.

## Branch
`task/001-vat-number-validator`

## Files allowed
- `DevlabSandbox.sln`
- `src/DevlabSandbox.Core/**`
- `tests/DevlabSandbox.Core.Tests/**`
- `.gitignore`
- `README.md` (only to add a short "Build and test" section)

## Specification
- Solution `DevlabSandbox.sln` at the repository root.
- Class library `src/DevlabSandbox.Core` targeting `net10.0`, nullable enabled.
- xUnit test project `tests/DevlabSandbox.Core.Tests` targeting `net10.0`, referencing the library.
- `.gitignore` generated with `dotnet new gitignore`.
- Public static class `DevlabSandbox.Core.ItalianVatNumber` with `public static bool IsValid(string? value)`.
- A value is valid only if it is exactly 11 ASCII digits (`0`–`9`), with no trimming or normalization, and its last digit matches the check digit:
  - X = sum of the digits in positions 1, 3, 5, 7, 9 (1-based);
  - Y = for the digits in positions 2, 4, 6, 8, 10: double each one, subtract 9 if the result is greater than 9, then sum;
  - check digit = (10 − (X + Y) mod 10) mod 10, compared with digit 11.

## Acceptance criteria
- [ ] `dotnet build DevlabSandbox.sln -warnaserror` exits with code 0.
- [ ] `dotnet test DevlabSandbox.sln` exits with code 0.
- [ ] The tests include at least these cases:
  - valid: `12345678903`, `10000000009`, `01234567897`;
  - invalid: `12345678901` (wrong check digit), `1234567890` (10 digits), `123456789030` (12 digits), `1234567890A` (non-digit), `" 12345678903"` (leading space), `""` (empty), `null`.
- [ ] A pull request is open from `task/001-vat-number-validator` to `main`, with the body required by `CLAUDE.md`.

## Stop criteria
- Any requirement above is ambiguous or contradictory.
- Build or tests still failing after 3 fix attempts.
- A needed command is blocked by permissions.

## Out of scope
- CI workflows, other validators, NuGet packaging, changes to `CLAUDE.md` or `tasks/`.
