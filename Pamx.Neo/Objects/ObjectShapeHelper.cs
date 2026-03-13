namespace Pamx.Neo.Objects;

public static class ObjectShapeHelper
{
    public static ObjectShape FromSeparate(int shape, int shapeOption) =>
        (ObjectShape)((shape & 0xffff) | shapeOption << 16);

    extension(ObjectShape shapeEnum)
    {
        public void ToSeparate(out int shape, out int shapeOption)
        {
            shape = shapeEnum.GetShape();
            shapeOption = shapeEnum.GetShapeOption();
        }

        public int GetShape() => (int)shapeEnum & 0xffff;
        public int GetShapeOption() => (int)shapeEnum >> 16;
    }
}