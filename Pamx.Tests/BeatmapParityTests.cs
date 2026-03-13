using Pamx.Tests.Extensions;
using Pamx.Tests.Fixtures;

namespace Pamx.Tests;

[Collection("Beatmap")]
public sealed class BeatmapParityTests(BeatmapFixture fixture)
{
    [Theory]
    [InlineData("checkpoints", TestDisplayName = "Checkpoints")]
    [InlineData("themes", TestDisplayName = "Themes")]
    [InlineData("markers", TestDisplayName = "Markers")]
    public void Beatmap_Collections_AreIdentical(string key)
    {
        var expected = fixture.Expected[key];
        var actual = fixture.Actual[key];

        actual.Should().BeIdenticalTo(expected);
    }
    
    [Theory]
    [InlineData(0, TestDisplayName = "Move")]
    [InlineData(1, TestDisplayName = "Zoom")]
    [InlineData(2, TestDisplayName = "Rotate")]
    [InlineData(3, TestDisplayName = "Shake")]
    [InlineData(4, TestDisplayName = "Theme")]
    [InlineData(5, TestDisplayName = "Chroma")]
    public void Events_Keyframes_AreIdentical(int index)
    {
        var expected = fixture.Expected["events"]![index];
        var actual = fixture.Actual["events"]![index];

        actual.Should().BeIdenticalTo(expected);
    }
}