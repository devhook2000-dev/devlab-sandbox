# 003 — Italian IBAN validator

## Objective
Add a validator for Italian IBANs (format and ISO 7064 mod 97-10 check digits), covered by unit tests.

## Context
Third task of the DevLab agentic workflow, also used to measure Pro quota consumption per task. Same conventions as tasks 001 (`ItalianVatNumber`) and 002 (`ItalianFiscalCode`).

## Branch
`task/003-italian-iban-validator`

## Files allowed
- `src/DevlabSandbox.Core/ItalianIban.cs`
- `tests/DevlabSandbox.Core.Tests/ItalianIbanTests.cs`

## Specification
- Public static class `DevlabSandbox.Core.ItalianIban` with `public static bool IsValid(string? value)`.
- No trimming, no removal of spaces, no case normalization: the value is validated exactly as given.
- A value is valid only if all of the following hold (positions 1-based):
  - it is exactly 27 characters long;
  - characters 1–2 are `IT`;
  - characters 3–4 are ASCII digits (`0`–`9`);
  - character 5 (CIN) is an uppercase ASCII letter (`A`–`Z`);
  - characters 6–10 (ABI) and 11–15 (CAB) are ASCII digits;
  - characters 16–27 (account number) are uppercase ASCII letters or ASCII digits;
  - the mod 97 check passes: move characters 1–4 to the end, replace every letter with two digits (`A`=10 … `Z`=35), and the resulting number modulo 97 equals 1.
- Do not validate the CIN against ABI, CAB and account number, and do not check that ABI or CAB exist.
- The mod 97 computation must not overflow: process the digits incrementally (e.g. remainder = (remainder × 10 + digit) mod 97, or in chunks); do not parse the whole number into `long` or `decimal`. `System.Numerics.BigInteger` is not allowed.

## Acceptance criteria
- [ ] `dotnet build DevlabSandbox.sln -warnaserror` exits with code 0.
- [ ] `dotnet test DevlabSandbox.sln` exits with code 0, and the tests from tasks 001 and 002 still pass.
- [ ] The tests include at least these cases:
  - valid: `IT60X0542811101000000123456`, `IT56A0300203280000400162854`, `IT34Z0306909606100000063512`, `IT98K0100503382000000218020`, `IT37X0542811101000000ABC123`;
  - invalid, wrong check digits: `IT61X0542811101000000123456`;
  - invalid, format (these pass mod 97 but break a format rule): `it60x0542811101000000123456` (lowercase), `IT2910542811101000000123456` (CIN is a digit), `SM88X0542811101000000123456` (country is not IT), `IT32X05428A1101000000123456` (letter in ABI);
  - invalid, other: `IT60X054281110100000012345` (26 characters), `IT60X05428111010000001234567` (28 characters), `IT60X0542811101000000123-56` (invalid character), `IT60 X054 2811 1010 0000 0123 456` (spaces), `""` (empty), `null`.
- [ ] A ready-for-review (not draft) pull request is open from `task/003-italian-iban-validator` to `main`, with the body required by `CLAUDE.md`.

## Stop criteria
- Any requirement above is ambiguous or contradictory.
- Build or tests still failing after 3 fix attempts.
- A needed command is blocked by permissions.

## Out of scope
- IBANs of other countries, CIN validation, ABI/CAB lookup, formatting helpers, changes to `ItalianVatNumber`, `ItalianFiscalCode`, `CLAUDE.md` or `tasks/`.
