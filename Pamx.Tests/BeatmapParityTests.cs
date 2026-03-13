using Pamx.Tests.Extensions;
using Pamx.Tests.Fixtures;

namespace Pamx.Tests;

[Collection("Beatmap")]
public sealed class BeatmapParityTests(BeatmapFixture fixture)
{
    [Fact]
    public void Checkpoints_AreIdentical()
    {
        var expectedCheckpoints = fixture.Expected["checkpoints"];
        var actualCheckpoints = fixture.Actual["checkpoints"];

        actualCheckpoints.Should().BeIdenticalTo(expectedCheckpoints);
    }
    
    [Fact]
    public void Themes_AreIdentical()
    {
        var expectedThemes = fixture.Expected["themes"];
        var actualThemes = fixture.Actual["themes"];

        actualThemes.Should().BeIdenticalTo(expectedThemes);
    }
    
    [Fact]
    public void Markers_AreIdentical()
    {
        var expectedMarkers = fixture.Expected["markers"];
        var actualMarkers = fixture.Actual["markers"];

        actualMarkers.Should().BeIdenticalTo(expectedMarkers);
    }
}