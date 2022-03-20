using System;

namespace game_engine.game_objects
{
    public class NumericAttribute : Attribute
    {
        public override double numVal { get; }

        public override string stringVal => 
            throw new NotSupportedException("Numeric attributes do not have a string value.");

        public override object objVal => numVal;

        public NumericAttribute(string name, double value)
            : base(name, AttributeType.NUMBER)
        {
            this.numVal = value;
        }

        public override string ToString()
        {
            return numVal.ToString();
        }
    }
}
