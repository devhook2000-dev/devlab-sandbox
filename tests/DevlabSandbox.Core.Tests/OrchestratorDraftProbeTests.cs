namespace DevlabSandbox.Core.Tests;

public class OrchestratorDraftProbeTests
{
    // This failure is intentional: the test exists to make the orchestrator's
    // verification fail so that the pull request is opened as a draft.
    // It must not be fixed, skipped or removed.
    [Fact]
    public void Probe_FailsOnPurpose_ToExerciseTheDraftPath()
    {
        Assert.Equal(1, 2);
    }
}
