
namespace Pripizden.InputSystem
{
    public abstract partial class InputProvider
    {
        /// <summary>
        /// Utility class to hold input information 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected class InputValue<T>
        {
            public T Value;
            public bool bIsDirty;

            /// <summary>
            /// Default constructor, presets fields
            /// </summary>
            /// <param name="action">Action name</param>
            /// <param name="value">Value held</param>
            /// <param name="isDirty">If the value has changed between since last check</param>
            public InputValue(T value, bool isDirty)
            {
                Value = value;
                bIsDirty = isDirty;
            }

            /// <summary>
            /// Sets input value. (also sets dirty flag)
            /// </summary>
            /// <param name="value"></param>
            public void SetValue(T value)
            {
                Value = value;
                bIsDirty = true;
            }

            /// <summary>
            /// Returns input value, also clears dirty flag
            /// </summary>
            /// <returns>Current input value</returns>
            public T UseValue()
            {
                bIsDirty = false;
                return Value;
            }
        }
    }
}