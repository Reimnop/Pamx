using System.Numerics;
using Pamx.Editor;
using Pamx.Keyframes;

namespace Pamx.Objects;

/// <summary>
/// Represent a beatmap object within the game.
/// </summary>
public sealed class BeatmapObject : IIdentifiable<string>
{
    /// <summary>
    /// The ID of the object.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The ID of the object's parent.
    /// </summary>
    public string ParentId { get; set; } = string.Empty;

    /// <summary>
    /// The ID of the prefab this object belongs to.
    /// </summary>
    public string PrefabId { get; set; } = string.Empty;

    /// <summary>
    /// The ID of the prefab instance this object belongs to.
    /// </summary>
    public string PrefabInstanceId { get; set; } = string.Empty;

    /// <summary>
    /// The name of the object.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The time at which the object is spawned.
    /// </summary>
    public float StartTime { get; set; } = 0.0f;

    /// <summary>
    /// The type of the time at which the object is killed.
    /// </summary>
    public AutoKillType AutoKillType { get; set; } = AutoKillType.NoAutoKill;

    /// <summary>
    /// The offset from the base time at which the object is killed.
    /// </summary>
    public float AutoKillOffset { get; set; } = 0.0f;

    /// <summary>
    /// The object's transform center.
    /// </summary>
    public Vector2 Origin { get; set; } = Vector2.Zero;

    /// <summary>
    /// The type of the object.
    /// </summary>
    public ObjectType Type { get; set; } = ObjectType.LegacyNormal;

    /// <summary>
    /// The shape of the object.
    /// </summary>
    public ObjectShape Shape { get; set; } = ObjectShape.SquareSolid;

    /// <summary>
    /// The custom shape parameters of the object.
    /// </summary>
    public CustomShapeParams? CustomShapeParams { get; set; }

    /// <summary>
    /// The object's text value. Only has an effect when <see cref="Shape"/> is set to <see cref="ObjectShape.Text"/>
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// The beatmap object's gradient params.
    /// </summary>
    public GradientParams Gradient { get; set; } = new();

    /// <summary>
    /// The specific transform components that the object inherits from its parent.
    /// </summary>
    public ParentType ParentType { get; set; } = ParentType.Position | ParentType.Rotation;

    /// <summary>
    /// The object's parent animation time offset.
    /// </summary>
    public ParentOffset ParentOffset { get; set; } = ParentOffset.Zero;

    /// <summary>
    /// The object's render depth.
    /// </summary>
    public float RenderDepth { get; set; } = 20.0f;

    /// <summary>
    /// The object's editor settings.
    /// </summary>
    public ObjectEditorSettings EditorSettings { get; set; } = new();

    /// <summary>
    /// The object's position keyframes.
    /// </summary>
    public List<RandomKeyframe<Vector2>> PositionEvents { get; set; } = [new(Vector2.Zero)];

    /// <summary>
    /// The object's scale keyframes.
    /// </summary>
    public List<RandomKeyframe<Vector2>> ScaleEvents { get; set; } = [new(Vector2.One)];

    /// <summary>
    /// The object's rotation keyframes.
    /// </summary>
    public List<ObjectRotationKeyframe> RotationEvents { get; set; } = [new(0.0f)];

    /// <summary>
    /// The object's color keyframes.
    /// </summary>
    public List<FixedKeyframe<ObjectColorValue>> ColorEvents { get; set; } = [new(ObjectColorValue.Default)];
}