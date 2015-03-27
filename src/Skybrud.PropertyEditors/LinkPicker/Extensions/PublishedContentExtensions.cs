using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Skybrud.PropertyEditors.LinkPicker.Extensions {

    /// <summary>
    /// Various extension methods for <code>IPublishedContent</code> and the LinkPicker.
    /// </summary>
    public static class PublishedContentExtensions {

        /// <summary>
        /// Gets the first link item from a LinkPicker model from the property with the specified <code>propertyAlias</code>.
        /// </summary>
        /// <param name="content">The published content to read the property from.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        public static LinkPickerItem GetLinkPickerItem(this IPublishedContent content, string propertyAlias) {
            return content == null ? null : GetLinkPickerModel(content, propertyAlias).Items.FirstOrDefault();
        }

        /// <summary>
        /// Gets the LinkPicker model from the property with the specified <code>propertyAlias</code>.
        /// </summary>
        /// <param name="content">The published content to read the property from.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        public static LinkPickerModel GetLinkPickerModel(this IPublishedContent content, string propertyAlias) {
            return (content == null ? null : content.GetPropertyValue<LinkPickerModel>(propertyAlias)) ?? new LinkPickerModel {
                Items = new LinkPickerItem[0]
            };
        }

    }

}