using System.CodeDom.Compiler;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;

namespace CodeGeneratorPlus
{
    public static class CodeGeneratorExtensions
    {
        public static NamespaceWriter CreateNewSourceFile(this GeneratorExecutionContext context,string hintName)
        {
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var writer = new IndentedTextWriter(stringWriter);
            var sourceWriter = new NamespaceWriter(writer, context, hintName);
            return sourceWriter;
        }
    }
}