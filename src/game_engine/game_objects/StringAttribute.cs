using System;

namespace game_engine.game_objects
{
    public class StringAttribute : Attribute
    {
        public override double numVal =>
            throw new NotSupportedException("String attributes do not have a numeric value");
        public override string stringVal { get; }
        public override object objVal => stringVal;

        public StringAttribute(string name, string value) : base(name, AttributeType.TEXT)
        {
            this.stringVal = value;
        }

        public override string ToString()
        {
            return stringVal;
        }
    }
}
