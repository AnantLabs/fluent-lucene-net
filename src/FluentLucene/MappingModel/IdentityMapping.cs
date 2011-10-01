using FluentLucene.Mapping;

namespace FluentLucene.MappingModel
{
    /// <summary>
    /// The mapping representing the identity of a document
    /// </summary>
    internal class IdentityMapping : FieldLikeMappingBase
    {
        /// <summary>
        /// Creates an identity mapping backed by the specified member
        /// </summary>
        /// <param name="member">The member backing this identity</param>
        public IdentityMapping(IMember member)
            : base(member)
        { }
    }
}