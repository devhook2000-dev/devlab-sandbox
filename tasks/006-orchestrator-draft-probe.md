# 006 — Orchestrator draft probe

## Objective
Add one test class with a single test that fails on purpose, so that the orchestrator's independent verification goes red and the pull request is opened as a draft.

## Context
This is a probe of the orchestrator, not a feature: it exercises the draft path of the flow (verification failed → push → `gh pr create --draft` with the reason at the top). The failing test is the expected result of this task. It is never merged: the pull request is closed by a human after the check.

This task overrides the generic stop criterion of `CLAUDE.md` about failures that persist after 3 fix attempts: here the failure is intentional and must not be fixed.

## Branch
`task/006-orchestrator-draft-probe`

## Files allowed
- `tests/DevlabSandbox.Core.Tests/OrchestratorDraftProbeTests.cs`

## Specification
- Public class `DevlabSandbox.Core.Tests.OrchestratorDraftProbeTests` with exactly one `[Fact]`, named `Probe_FailsOnPurpose_ToExerciseTheDraftPath`, whose body is `Assert.Equal(1, 2);`.
- A comment above the test explains that the failure is intentional: the test exists to make the orchestrator's verification fail and open the pull request as a draft, and it must not be fixed.
- No other tests, attributes (`Skip`, traits) or code changes.
- Commit the file with a Conventional Commits message (`test: ...`).
- Write the pull request body to `.devlab/pr-body.md`, stating that this is an orchestrator probe and that the single test fails on purpose.
- Do not fix the test, do not skip it, and do not write `.devlab/STOP.md`.

## Acceptance criteria
- [ ] `dotnet build DevlabSandbox.sln -warnaserror` exits with code 0.
- [ ] `dotnet test DevlabSandbox.sln` exits with a non-zero code, with exactly 1 failed test (`Probe_FailsOnPurpose_ToExerciseTheDraftPath`) and every other test passing.
- [ ] `git log --oneline main..HEAD` shows one commit that adds only `tests/DevlabSandbox.Core.Tests/OrchestratorDraftProbeTests.cs`.
- [ ] The pull request body is written to `.devlab/pr-body.md` as required by `CLAUDE.md` and states that the failure is intentional.

## Stop criteria
- The build fails (a compile error is not the intended failure).
- A test other than `Probe_FailsOnPurpose_ToExerciseTheDraftPath` fails.
- A needed command is blocked.

## Out of scope
- Fixing, skipping or removing the failing test; any other test or source file; `CLAUDE.md` or `tasks/`.
