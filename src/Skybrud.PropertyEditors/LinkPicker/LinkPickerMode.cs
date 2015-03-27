﻿namespace Skybrud.PropertyEditors.LinkPicker {
    
    /// <summary>
    /// Enum describing the type of the link.
    /// </summary>
    public enum LinkPickerMode {
    
        /// <summary>
        /// Describes a link that is an external URL.
        /// </summary>
        Url,

        /// <summary>
        /// Describes a link that is a reference to an internal content node in Umbraco.
        /// </summary>
        Content,

        /// <summary>
        /// Describes a link that is a referemce to an internal media node in Umbraco.
        /// </summary>
        Media
    
    }

}