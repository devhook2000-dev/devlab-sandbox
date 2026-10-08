namespace DevlabSandbox.Core.Tests;

public class OrchestratorDraftProbeTests
{
    // This failure is intentional. The test exists to make the orchestrator's
    // verification fail so that the pull request is opened as a draft.
    // Do not fix, skip or remove it.
    [Fact]
    public void Probe_FailsOnPurpose_ToExerciseTheDraftPath()
    {
        Assert.Equal(1, 2);
    }
}
