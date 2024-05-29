namespace Wires;

class Program
{
    static void Main(string[] args)
    {
        Config config = new Config();

        config.SetCellCount(20);

        Field field = new Field();
        field.Run();
    }
}
