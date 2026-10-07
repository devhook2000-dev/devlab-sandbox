Execution mode: container (orchestrator). Generated 2026-10-07, version 2.0.

# devlab-sandbox — agent rules

## Purpose
Test repository for the Oracle DevLab agentic workflow. Each unit of work is described by a task file in `tasks/` and delivered as a pull request.

## Environment
- Linux arm64 (Ubuntu 26.04). Never use x64-only packages, binaries or Docker images.
- .NET SDK 10: target `net10.0`.
- Network access goes through a proxy that allows only NuGet; GitHub and any other site are blocked. Do not try to work around it.
- The agent runs in a container without access to GitHub. Pushing branches and opening pull requests are done by the orchestrator, never by the agent.

## Workflow
1. Read the assigned task file in full before changing anything.
2. Work only on the branch named in the task file (default: `task/NNN-short-name`).
3. Touch only the files listed under "Files allowed". If you need others, stop and report.
4. Make small, atomic commits with Conventional Commits messages (`feat:`, `fix:`, `test:`, `chore:`, `docs:`).
5. Before finishing, every acceptance criterion must be verified by running its command.
6. Never push and never open a pull request: the orchestrator does both.
7. Write the PR body to `.devlab/pr-body.md` with the file tools and do not commit it. It contains:
   - summary of the change;
   - the acceptance criteria checklist, each item with the command run and its result;
   - test summary (passed/failed/skipped counts);
   - open questions or limitations, if any.
8. Never modify `.github/`, `.devlab/` (except `.devlab/pr-body.md` and `.devlab/STOP.md`), `CLAUDE.md` or `tasks/`.
9. Never rewrite history.

## Stop criteria
Stop and write the reason to `.devlab/STOP.md` with the file tools (do not commit it) when:
- a requirement is ambiguous or contradictory;
- the same build or test failure persists after 3 fix attempts;
- a needed command or network resource is blocked — do not look for workarounds.

## Code conventions
- C#, English for all identifiers, comments and messages.
- Nullable reference types enabled, file-scoped namespaces, one public type per file.
- Tests with xUnit; test names describe behaviour (`IsValid_ReturnsFalse_WhenCheckDigitIsWrong`).
- Build must be warning-free (`-warnaserror`).
