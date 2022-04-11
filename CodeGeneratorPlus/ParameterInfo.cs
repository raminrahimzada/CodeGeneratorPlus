using System.CodeDom.Compiler;

namespace CodeGeneratorPlus
{
    public sealed class ParameterInfo
    {
        private readonly string _name;
        private string _valueType;
        private string _indicator;
        private string _defaultValue;

        public ParameterInfo(string name)
        {
            _name = name;
        }

        public void Write(IndentedTextWriter writer)
        {
            if(!string.IsNullOrWhiteSpace(_indicator))
            {
                writer.Write(_indicator);
                writer.Write(" ");
            }
            writer.Write(_valueType);
            writer.Write(" ");
            writer.Write(_name);
            if(!string.IsNullOrWhiteSpace(_defaultValue))
            {
                writer.Write("=");
                writer.Write(_defaultValue);
            }
        }

        public ParameterInfo OfType<T>()
        {
            return OfType(typeof(T).FullName);
        }
        public ParameterInfo OfType(string type)
        {
            this._valueType = type;
            return this;
        }

        public ParameterInfo Default(string defaultValue)
        {
            _defaultValue = defaultValue;
            return this;
        }

        public ParameterInfo In()
        {
            _indicator = "in";
            return this;
        }
        
        public ParameterInfo Out()
        {
            _indicator = "out";
            return this;
        }

        public ParameterInfo Ref()
        {
            _indicator = "ref";
            return this;
        }
    }
}