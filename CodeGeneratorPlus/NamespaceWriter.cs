using System;
using System.CodeDom.Compiler;
using System.IO;
using Microsoft.CodeAnalysis;

namespace CodeGeneratorPlus
{
    public sealed class NamespaceWriter:IDisposable
    {
        private readonly IndentedTextWriter _writer;
        private readonly GeneratorExecutionContext _generatorExecutionContext;
        private readonly string _hintName;

        internal NamespaceWriter(IndentedTextWriter writer, GeneratorExecutionContext generatorExecutionContext,
            string hintName)
        {
            _writer = writer;
            _generatorExecutionContext = generatorExecutionContext;
            _hintName = hintName;
        }

        public NamespaceWriter Using(string nameSpace)
        {
            _writer.WriteLine($"using {nameSpace};");
            return this;
        }

        public NamespaceWriter Namespace(string nameSpace)
        {
            _writer.WriteLine($"namespace {nameSpace};");
            return this;
        }

        public void Dispose()
        {
            var source = (_writer.InnerWriter as StringWriter)?.ToString() ?? string.Empty;
            _generatorExecutionContext.AddSource(_hintName,source);
        }

        public ClassWriter NewClass(string className,string modifiers)
        {
            return new ClassWriter(_writer, modifiers, className);
        }
    }
}