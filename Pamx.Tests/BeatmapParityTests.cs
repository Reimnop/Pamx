using Pamx.Tests.Extensions;
using Pamx.Tests.Fixtures;

namespace Pamx.Tests;

[Collection("Beatmap")]
public sealed class BeatmapParityTests(BeatmapFixture fixture)
{
    [Fact]
    public void Themes_AreIdentical()
    {
        var expectedThemes = fixture.Expected["themes"];
        var actualThemes = fixture.Actual["themes"];

        actualThemes.Should().BeIdenticalTo(expectedThemes);
    }
}