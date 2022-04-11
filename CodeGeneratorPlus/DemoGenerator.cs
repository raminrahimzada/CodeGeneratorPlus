using Microsoft.CodeAnalysis;

namespace CodeGeneratorPlus
{
    [Generator]
    public class DemoGenerator:ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            //System.Diagnostics.Debugger.Launch();
        }

        public void Execute(GeneratorExecutionContext context)
        {
            using (var file=context.CreateNewSourceFile("MyLibrary.cs"))
            {
                file.Using("System")
                    .Using("System.IO")
                    .Namespace("MathLibrary");

                using (var c = file.NewClass("MathHelper", "public"))
                {
                    using (var method = c.NewMethod("Add", "public static"))
                    {
                        method.Parameter("a").OfType<int>().Default("0").In();
                        method.Parameter("b").OfType<int>().Default("0");
                        method.Returns<int>();

                        method.Body(b =>
                        {
                            //TODO find better way of method design
                            b.WriteLine("//return sum of two parameters");
                            b.WriteLine("return a + b;");
                        });
                    }
                }
            }
        }
    }
}
