# devlab-sandbox — agent rules

## Purpose
Test repository for the Oracle DevLab agentic workflow. Each unit of work is described by a task file in `tasks/` and delivered as a pull request.

## Environment
- Linux arm64 (Ubuntu 26.04). Never use x64-only packages, binaries or Docker images.
- .NET SDK 10: target `net10.0`.
- Do not download anything outside NuGet and GitHub. `curl` and `wget` are not available.

## Workflow
1. Read the assigned task file in full before changing anything.
2. Start from an up-to-date `main` and create the branch named in the task file (default: `task/NNN-short-name`).
3. Touch only the files listed under "Files allowed". If you need others, stop and report.
4. Make small, atomic commits with Conventional Commits messages (`feat:`, `fix:`, `test:`, `chore:`, `docs:`).
5. Before opening the PR, every acceptance criterion must be verified by running its command.
6. Push the branch and open the PR with `gh pr create`. Title: `NNN: <task title>`. Body:
   - summary of the change;
   - the acceptance criteria checklist, each item with the command run and its result;
   - test summary (passed/failed/skipped counts);
   - open questions or limitations, if any.
7. Never merge a PR, never push to `main`, never force-push, never rewrite history.

## Stop criteria
Stop and open the PR as a draft (`gh pr create --draft`) with the reason at the top of the body when:
- a requirement is ambiguous or contradictory;
- the same build or test failure persists after 3 fix attempts;
- a needed command is blocked by permissions — do not look for workarounds.

## Code conventions
- C#, English for all identifiers, comments and messages.
- Nullable reference types enabled, file-scoped namespaces, one public type per file.
- Tests with xUnit; test names describe behaviour (`IsValid_ReturnsFalse_WhenCheckDigitIsWrong`).
- Build must be warning-free (`-warnaserror`).
