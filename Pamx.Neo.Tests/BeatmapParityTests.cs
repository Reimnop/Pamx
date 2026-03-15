using Pamx.Tests.Extensions;
using Pamx.Tests.Fixtures;

namespace Pamx.Tests;

[Collection("Beatmap")]
public sealed class BeatmapParityTests(BeatmapFixture fixture, ITestOutputHelper output)
{
    // [Fact]
    // public void Beatmap_IsIdentical()
    // {
    //     fixture.Actual.Should().BeIdenticalTo(fixture.Expected);
    // }
    
    [Theory]
    // [InlineData("editor")]
    // [InlineData("editor_prefab_spawn")]
    // [InlineData("parallax_settings")]
    // [InlineData("background_objects")]
    // [InlineData("checkpoints")]
    // [InlineData("objects")]
    // [InlineData("prefab_objects")]
    // [InlineData("prefabs")]
    [InlineData("themes")]
    // [InlineData("markers")]
    public void Beatmap_Collections_AreIdentical(string key)
    {
        var expected = fixture.Expected[key];
        var actual = fixture.Actual[key];
    
        actual.Should().BeIdenticalTo(expected);
    }
    
    // [Theory]
    // [InlineData(0, TestDisplayName = "Move")]
    // [InlineData(1, TestDisplayName = "Zoom")]
    // [InlineData(2, TestDisplayName = "Rotate")]
    // [InlineData(3, TestDisplayName = "Shake")]
    // [InlineData(4, TestDisplayName = "Theme")]
    // [InlineData(5, TestDisplayName = "Chroma")]
    // [InlineData(6, TestDisplayName = "Bloom")]
    // [InlineData(7, TestDisplayName = "Vignette")]
    // [InlineData(8, TestDisplayName = "LensDistortion")]
    // [InlineData(9, TestDisplayName = "Grain")]
    // [InlineData(10, TestDisplayName = "Gradient")]
    // [InlineData(11, TestDisplayName = "Glitch")]
    // [InlineData(12, TestDisplayName = "Hue")]
    // [InlineData(13, TestDisplayName = "Player")]
    // public void Events_Keyframes_AreIdentical(int index)
    // {
    //     var expected = fixture.Expected["events"]![index];
    //     var actual = fixture.Actual["events"]![index];
    //
    //     actual.Should().BeIdenticalTo(expected);
    // }
}