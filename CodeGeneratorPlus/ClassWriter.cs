using System;
using System.CodeDom.Compiler;

namespace CodeGeneratorPlus
{
    public sealed class ClassWriter:IDisposable
    {
        private readonly IndentedTextWriter _writer;

        internal ClassWriter(IndentedTextWriter writer,string modifiers,string className)
        {
            _writer = writer;
            if (!string.IsNullOrWhiteSpace(modifiers))
            {
                _writer.Write(modifiers);
                _writer.Write(" ");
            }
            _writer.Write("class ");
            _writer.Write(className);
            _writer.WriteLine("{");
            _writer.Indent++;
        }

        public void Dispose()
        {
            _writer.Indent--;
            _writer.WriteLine("}");
        }

        public MethodWriter NewMethod(string methodName,string indicators)
        {
            return new MethodWriter(_writer, methodName, indicators);
        }
    }
}