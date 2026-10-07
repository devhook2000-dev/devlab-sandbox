# NNN — Title

## Objective
What must exist or work at the end, in one or two sentences.

## Context
Why this task exists and anything the agent needs to know that is not in the code.

## Branch
`task/NNN-short-name`

## Files allowed
- exact paths only, one per line (no globs), that the agent may create or modify

## Specification
Precise requirements: signatures, rules, data, edge cases.

## Acceptance criteria
- [ ] `command` → expected result (every item must be checkable by running a command)

## Stop criteria
- conditions under which the agent stops and writes the reason to `.devlab/STOP.md` (not committed) instead of continuing

## Out of scope
- things the agent must not do in this task
