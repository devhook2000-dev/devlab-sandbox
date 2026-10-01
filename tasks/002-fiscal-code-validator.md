# 002 — Italian fiscal code check character validator

## Objective
Add a validator for the check character of Italian fiscal codes (codice fiscale), covered by unit tests.

## Context
Second task of the DevLab agentic workflow. It follows the same conventions as task 001 (`ItalianVatNumber`).

## Branch
`task/002-fiscal-code-validator`

## Files allowed
- `src/DevlabSandbox.Core/ItalianFiscalCode.cs`
- `tests/DevlabSandbox.Core.Tests/ItalianFiscalCodeTests.cs`

## Specification
- Public static class `DevlabSandbox.Core.ItalianFiscalCode` with `public static bool IsValid(string? value)`.
- A value is valid only if:
  - it is exactly 16 characters long;
  - every character is an uppercase ASCII letter (`A`–`Z`) or an ASCII digit (`0`–`9`), with no trimming or case normalization;
  - character 16 equals the check character computed from characters 1–15.
- Do not validate the internal structure (surname, name, date, place): only length, alphabet and check character.
- Check character algorithm, positions 1-based over characters 1–15:
  - characters in odd positions (1, 3, …, 15) are converted with this table:
    - `0`/`A`=1, `1`/`B`=0, `2`/`C`=5, `3`/`D`=7, `4`/`E`=9, `5`/`F`=13, `6`/`G`=15, `7`/`H`=17, `8`/`I`=19, `9`/`J`=21,
    - `K`=2, `L`=4, `M`=18, `N`=20, `O`=11, `P`=3, `Q`=6, `R`=8, `S`=12, `T`=14, `U`=16, `V`=10, `W`=22, `X`=25, `Y`=24, `Z`=23;
  - characters in even positions (2, 4, …, 14): digits `0`–`9` are worth 0–9, letters `A`–`Z` are worth 0–25;
  - check character = the letter at index (sum of all values mod 26), where `A`=0 … `Z`=25.

## Acceptance criteria
- [ ] `dotnet build DevlabSandbox.sln -warnaserror` exits with code 0.
- [ ] `dotnet test DevlabSandbox.sln` exits with code 0, and the tests from task 001 still pass.
- [ ] The tests include at least these cases:
  - valid: `RSSMRA80A01H501U`, `BNCLRA85T41F205Y`, `VRDGPP90E15L219K`, `RSSMRA80A01H50QA`;
  - invalid: `RSSMRA80A01H501X` (wrong check character), `rssmra80a01h501u` (lowercase), `RSSMRA80A01H501` (15 characters), `RSSMRA80A01H501UU` (17 characters), `RSSMRA80A01H501-` (invalid character), `""` (empty), `null`.
- [ ] A ready-for-review (not draft) pull request is open from `task/002-fiscal-code-validator` to `main`, with the body required by `CLAUDE.md`.

## Stop criteria
- Any requirement above is ambiguous or contradictory.
- Build or tests still failing after 3 fix attempts.
- A needed command is blocked by permissions.

## Out of scope
- Structural validation (dates, months, place codes), omocodia handling, changes to `ItalianVatNumber`, `CLAUDE.md` or `tasks/`.
