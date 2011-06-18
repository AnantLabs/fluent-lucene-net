namespace FluentLucene.Mapping
{
    /// <summary>
    /// Represents a builder for the identity of a target object
    /// </summary>
    public class IdentityBuilder
    {
        /// <summary>
        /// Gets the member used to get and set the value for the identity
        /// </summary>
        internal IMember Member { get; private set; }

        /// <summary>
        /// Gets the field name for the identity
        /// </summary>
        internal string FieldName { get; private set; }

        /// <summary>
        /// Creates an identity part for a given member
        /// </summary>
        /// <param name="member">The member used to get and set the value</param>
        internal IdentityBuilder(IMember member)
        {
            Member = member;
        }

        /// <summary>
        /// Specifies the name of the identity field in the index
        /// </summary>
        /// <param name="field">The name of the identity field</param>
        /// <returns>The builder itself</returns>
        public IdentityBuilder Field(string field)
        {
            // Sets the field name
            FieldName = field;

            // Return the identity part itself
            return this;
        }
    }
}