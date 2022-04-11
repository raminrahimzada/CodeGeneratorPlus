using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace CodeGeneratorPlus
{
    public sealed class MethodWriter:IDisposable
    {
        private readonly IndentedTextWriter _writer;
        private string _returnType;
        private readonly string _methodName;
        private readonly string _indicators;
        private Action<IndentedTextWriter> _body;
        private readonly List<ParameterInfo> _parameters = new List<ParameterInfo>();

        public MethodWriter(IndentedTextWriter writer, string methodName, string indicators)
        {
            _writer = writer;
            _methodName = methodName;
            _indicators = indicators;
        }

        public MethodWriter Body(Action<IndentedTextWriter> action)
        {
            _body = action;
            return this;
        }

        public ParameterInfo Parameter(string name)
        {
            var p = new ParameterInfo(name);
            _parameters.Add(p);
            return p;
        }

        public MethodWriter Returns(string returnType)
        {
            _returnType = returnType;
            return this;
        }
        public MethodWriter Returns<T>()
        {
            return Returns(typeof(T).FullName);
        }
        
        public void Dispose()
        {
            if (!string.IsNullOrWhiteSpace(_indicators))
            {
                _writer.Write(_indicators);
                _writer.Write(" ");
            }

            _writer.Write(_returnType ?? "void");
            _writer.Write(" ");

            _writer.Write(_methodName);
            _writer.Write("(");
            bool first = true;
            foreach (var p in _parameters)
            {
                if(!first) _writer.Write(",");
                p.Write(_writer);
                if (first) first = false;
            }

            _writer.WriteLine(")");
            _writer.WriteLine("{");
            _writer.Indent++;
            _body?.Invoke(_writer);
            _writer.Indent--;
            _writer.WriteLine("}");
        }
    }
}