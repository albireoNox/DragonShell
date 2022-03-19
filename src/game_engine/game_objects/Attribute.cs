namespace game_engine.game_objects
{
    public abstract class Attribute
    {
        public readonly string name;
        public readonly AttributeType type;
        public abstract double numVal { get; }
        public abstract object objVal { get; }

        protected Attribute(string name, AttributeType type)
        {
            this.name = name;
            this.type = type;
        }

        public enum AttributeType
        {
            NUM, 
        }
    }
}
