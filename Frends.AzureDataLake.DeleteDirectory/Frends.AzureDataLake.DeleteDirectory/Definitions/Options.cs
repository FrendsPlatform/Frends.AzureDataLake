using System.ComponentModel;

namespace Frends.AzureDataLake.DeleteDirectory.Definitions
{
    public class Options
    {
        /// <summary>
        /// Throw an error if directory to be deleted doesn't exists.
        /// </summary>
        /// <example>false</example>
        [DefaultValue(false)]
        public bool ThrowErrorIfDirectoryDoesNotExists { get; set; }
    }
}
