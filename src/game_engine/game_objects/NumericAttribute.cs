namespace game_engine.game_objects
{
    public class NumericAttribute : Attribute
    {
        public override double numVal { get; }

        public override object objVal => numVal;

        public NumericAttribute(string name, double value)
            : base(name, AttributeType.NUM)
        {
            this.numVal = value;
        }

        public override string ToString()
        {
            return numVal.ToString();
        }
    }
}
