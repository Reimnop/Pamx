using Pamx.Common.Enum;

namespace Pamx.Common;

public static class ObjectShapeUtil
{
    public static ObjectShape FromSeparate(int shape, int shapeOption) =>
        (ObjectShape)((shape & 0xffff) | shapeOption << 16);

    public static void ToSeparate(this ObjectShape shapeEnum, out int shape, out int shapeOption)
    {
        shape = (int)shapeEnum & 0xffff;
        shapeOption = (int)shapeEnum >> 16;
    }
}