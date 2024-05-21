using System.ComponentModel;

namespace Frends.AzureDataLake.DeleteDirectory.Definitions
{
    public class Options
    {
        /// <summary>
        /// Throw an error if directory to be deleted doesn't exist.
        /// </summary>
        /// <example>false</example>
        [DefaultValue(false)]
        public bool ThrowErrorIfDirectoryDoesNotExist { get; set; }
    }
}
