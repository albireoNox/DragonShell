namespace game_engine.math.expression
{
    // This is just a bit of a placeholder for now. TODO: Keep this or just use int_s everywhere?
    public readonly struct ExpressionResult
    {
        public int intVal { get; init; }

        public static implicit operator ExpressionResult(int i) => new() { intVal = i };
        public static implicit operator int(ExpressionResult r) => r.intVal;
    }
}
