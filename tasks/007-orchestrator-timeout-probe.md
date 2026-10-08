# 007 — Orchestrator timeout probe

## Objective
Start with a long wait, then add one trivial passing test, so that the orchestrator's agent timeout can be exercised with a short configured limit.

## Context
This is a probe of the orchestrator, not a feature: it is run with `agent.timeoutMinutes` set to 1 in a temporary configuration, so the orchestrator is expected to stop and remove the agent container during the wait, before any commit. If the timeout does not fire, the task still produces a small, valid pull request that a human closes.

## Branch
`task/007-orchestrator-timeout-probe`

## Files allowed
- `tests/DevlabSandbox.Core.Tests/OrchestratorTimeoutProbeTests.cs`

## Specification
- The very first action, before reading other files or running any other command, is `sleep 300` in the shell. If the command is interrupted or times out, continue with the next steps.
- Then add the public class `DevlabSandbox.Core.Tests.OrchestratorTimeoutProbeTests` with exactly one `[Fact]`, named `Probe_Passes_AfterTheWait`, whose body is `Assert.True(true);`, and a comment explaining that the test exists only for the orchestrator timeout probe.
- Commit the file with a Conventional Commits message (`test: ...`).
- Write the pull request body to `.devlab/pr-body.md`, stating that this is an orchestrator timeout probe.

## Acceptance criteria
- [ ] `sleep 300` was run as the first action.
- [ ] `dotnet build DevlabSandbox.sln -warnaserror` exits with code 0.
- [ ] `dotnet test DevlabSandbox.sln` exits with code 0.
- [ ] `git log --oneline main..HEAD` shows one commit that adds only `tests/DevlabSandbox.Core.Tests/OrchestratorTimeoutProbeTests.cs`.
- [ ] The pull request body is written to `.devlab/pr-body.md` as required by `CLAUDE.md`.

## Stop criteria
- Build or tests failing after 3 fix attempts.
- A needed command is blocked.

## Out of scope
- Skipping or shortening the wait; any other test or source file; `CLAUDE.md` or `tasks/`.
