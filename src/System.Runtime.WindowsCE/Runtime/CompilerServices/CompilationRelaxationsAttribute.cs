namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Method)]
    public class CompilationRelaxationsAttribute : Attribute
    {
        private readonly int _relaxations;

        public CompilationRelaxationsAttribute(int relaxations)
        {
            _relaxations = relaxations;
        }

        public int CompilationRelaxations
            => _relaxations;
    }
}
