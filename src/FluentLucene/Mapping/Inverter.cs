using System;

namespace FluentLucene.Mapping
{
    /// <summary>
    /// Implements basic inversion 
    /// </summary>
    internal class Inverter
    {
        /// <summary>
        /// Gets or sets whether the next call should be inverted
        /// </summary>
        public bool Inverted { get; set; }

        /// <summary>
        /// Toggles the value of <see cref="Inverted"/>
        /// </summary>
        public void Toggle()
        {
            Inverted = !Inverted;
        }

        /// <summary>
        /// Peeks the current values of <see cref="Inverted"/> and resets the value to <c>false</c>
        /// </summary>
        /// <returns>Whether <see cref="Inverted"/> is set or not</returns>
        public bool PeekAndReset()
        {
            var value = Inverted;
            Inverted = false;
            return value;
        }

        /// <summary>
        /// Sets a value and inverses the value when <see cref="Inverted"/> is set
        /// </summary>
        /// <param name="setValue">The function used to set the value</param>
        /// <param name="value">The value used to set the value</param>
        public void SetAndReset(Action<bool> setValue, bool value)
        {
            // Determines the value to set
            var valueToSet = value;
            if (PeekAndReset()) valueToSet = !valueToSet;

            // Set the value
            setValue(valueToSet);
        }
    }
}