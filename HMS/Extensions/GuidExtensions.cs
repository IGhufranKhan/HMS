using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HMS.Extensions
{
    public static class GuidExtensions
    {
        public static Guid NewGuid(this Guid guid)
        {
            return Guid.NewGuid();
        }
    }
}
