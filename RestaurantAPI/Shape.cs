namespace RestaurantAPI
{
    public abstract class Shape
    {
        public virtual void Draw()
        {
            Console.WriteLine("This is from Shape class");
        }
    }

    public class Triangle : Shape
    {

    }

    public class Rectangle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("This is Rectangle class");
        }
    }

    public class TestClass
    {
        public static void Display() { 
        List<Shape> shapes = new List<Shape>()
        {
            new Triangle(), new Rectangle()
        };

        foreach(var shape in shapes)
            shape.Draw();

        }
    }
}
